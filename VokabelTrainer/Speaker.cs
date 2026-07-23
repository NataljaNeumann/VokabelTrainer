// VokabelTrainer v1.7
// Copyright (C) 2019-2026 NataljaNeumann@gmx.de
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Collections.Generic;
using System.Text;

namespace VokabelTrainer
{
    //*******************************************************************************************************
    /// <summary>
    /// Outputs text to speakers
    /// </summary>
    //*******************************************************************************************************
    class Speaker
    {
        //===================================================================================================
        /// <summary>
        /// Transforms unicode text to XML-Format
        /// </summary>
        /// <param name="strInput">Input text</param>
        /// <returns>Text in XML format</returns>
        //===================================================================================================
        public static string TransformToXml(string strInput)
        {
            if (strInput == null)
                return string.Empty;

            StringBuilder oResult = new StringBuilder(strInput.Length*2);

            foreach (char c in strInput)
            {
                switch (c)
                {
                    case '<': oResult.Append("&lt;"); break;
                    case '>': oResult.Append("&gt;"); break;
                    case '&': oResult.Append("&amp;"); break;
                    case '"': oResult.Append("&quot;"); break;
                    case '\'': oResult.Append("&apos;"); break;
                    default:
                        if (c > 127 || c<=0) // Non-ASCII
                            oResult.Append("&#" + ((int)c) + ";");
                        else
                            oResult.Append(c);
                        break;
                }
            }

            return oResult.ToString();
        }

        //===================================================================================================
        /// <summary>
        /// Says the text, in case the language is supported
        /// </summary>
        /// <param name="strLanguage">Language name, possibly the native name</param>
        /// <param name="strText">Text to say</param>
        /// <param name="bAsync">Synchronously or asynchronously</param>
        /// <param name="bUseESpeak">Indicates if eSpeak shall be used</param>
        /// <param name="strESpeakPath">Path of eSpeak</param>
        //===================================================================================================
        public static void Say(string strLanguage, string strText, bool bAsync, bool bUseESpeak, string strESpeakPath)
        {
            string strLanguageFirstTwo = strLanguage.Length >= 2 ? strLanguage.Substring(0, 2) : strLanguage;
            string strLanguageFirstThree = strLanguage.Length >= 3 ? strLanguage.Substring(0, 3) : strLanguage;
            System.Speech.Synthesis.SpeechSynthesizer reader = new System.Speech.Synthesis.SpeechSynthesizer();

            // decide, if we support speaking this language
            string strSSMLLanguageToSpeak = "xml:lang='" + Program.LanguageCodeFromName(strLanguage) + "'";

            if (!string.IsNullOrEmpty(strSSMLLanguageToSpeak))
            {
                string strSsml = string.Format(@"<speak version='1.0' " +
                                                        "xmlns='http://www.w3.org/2001/10/synthesis' {0}>{1}</speak>",
                                                        strSSMLLanguageToSpeak,
                                                        TransformToXml(strText));
                bool bFallback = true;
                try
                {
                    bFallback = !bUseESpeak || string.IsNullOrEmpty(strESpeakPath) || !System.IO.File.Exists(strESpeakPath);

                    if (!bFallback)
                    {
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = strESpeakPath, // Replace with your executable file
                            Arguments = "-m \"" + strSsml + "\"", // Replace with any arguments if needed
                            RedirectStandardOutput = true,
                            RedirectStandardInput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                        };

                        // Start the process
                        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(startInfo))
                        {
                            process.StandardInput.Write(strSsml);
                            process.StandardInput.Close();

                            if (!bAsync)
                            {
                                // Wait for the process to exit
                                process.WaitForExit();

                                // Read the output if needed
                                string output = process.StandardOutput.ReadToEnd();
                            }
                            bFallback = false;

                        }
                    }
                }
                catch
                {
                    bFallback = true;
                }

                if (bFallback)
                {
                    try
                    {
                        if (bAsync)
                            reader.SpeakSsmlAsync(strSsml);
                        else
                            reader.SpeakSsml(strSsml);
                    }
                    catch
                    {
                        // ignore
                    }
                }

            }

        }
    }
}
