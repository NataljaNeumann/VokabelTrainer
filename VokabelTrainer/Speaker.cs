// VokabelTrainer v1.4
// Copyright (C) 2019-2025 NataljaNeumann@gmx.de
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
    class Speaker
    {
        public static void Say(string strLanguage, string strText, bool bAsync, bool bUseESpeak, string strESpeakPath)
        {
            string strLanguageFirstTwo = strLanguage.Length>=2?strLanguage.Substring(0, 2):strLanguage;
            string strLanguageFirstThree = strLanguage.Length>=3?strLanguage.Substring(0, 3):strLanguage;
            System.Speech.Synthesis.SpeechSynthesizer reader = new System.Speech.Synthesis.SpeechSynthesizer();

            if ((!bUseESpeak || string.IsNullOrEmpty(strESpeakPath)) &&
                ("Rus".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ||
                "Рус".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase)))
            {
                strText = (strText + " ").Replace(",", " ").Replace("; ", " ").Replace(". ", " ").Replace("?", " ")
                    .Replace("،", ",").Replace("、", ",").Replace("，", ",").Replace("!", " ").Replace("  ", " ").Replace("  ", " ")
                    .Replace("ться", "ца").Replace("тся", "ца").Replace("шого ", "шова ").Replace("того ", "това ").Replace("кого ", "кова ")
                    .Replace("шего ", "шева ").Replace("чего ", "чева ").Replace("рого ", "рова ").Replace("чого ", "чово ").Replace("его ", "ево ")
                    .Replace("чу ", "чю ").Replace("щу ", "щю ").Replace("бъё", "бё").Replace("ое ", "оэ ").Replace("ного ", "нова ").Trim();
                StringBuilder b = new StringBuilder();
                StringBuilder b2 = new StringBuilder();
                b.Append("<phoneme alphabet=\"ups\" ph=\"");
                foreach(char c in strText)
                {
                    switch (c)
                    {
                        case 'А':
                            b.Append("A ");
                            b2.Append("a");
                            break;
                        case 'а':
                            b.Append("A ");
                            b2.Append("a");
                            break;
                        case 'Б':
                            b.Append("B ");
                            b2.Append("b");
                            break;
                        case 'б':
                            b.Append("B ");
                            b2.Append("b");
                            break;
                        case 'В':
                            b.Append("W ");
                            b2.Append("w");
                            break;
                        case 'в':
                            b.Append("W ");
                            b2.Append("w");
                            break;
                        case 'Г':
                            b.Append("G ");
                            b2.Append("g");
                            break;
                        case 'г':
                            b.Append("G ");
                            b2.Append("g");
                            break;
                        case 'Д':
                            b.Append("D ");
                            b2.Append("d");
                            break;
                        case 'д':
                            b.Append("D ");
                            b2.Append("d");
                            break;
                        case 'Е':
                            b.Append("E ");
                            b2.Append("e");
                            break;
                        case 'е':
                            b.Append("E ");
                            b2.Append("e");
                            break;
                        case 'Ё':
                            b.Append("J O ");
                            b2.Append("jö");
                            break;
                        case 'ё':
                            b.Append("J O ");
                            b2.Append("jö");
                            break;
                        case 'Ж':
                            b.Append("ZH pal ");
                            b2.Append("sch");
                            break;
                        case 'ж':
                            b.Append("ZH pal ");
                            b2.Append("sch");
                            break;
                        case 'З':
                            b.Append("Z ");
                            b2.Append("s");
                            break;
                        case 'з':
                            b.Append("Z ");
                            b2.Append("s");
                            break;
                        case 'И':
                            b.Append("I ");
                            b2.Append("i");
                            break;
                        case 'и':
                            b.Append("I ");
                            b2.Append("i");
                            break;
                        case 'Й':
                            b.Append("J ");
                            b2.Append("j");
                            break;
                        case 'й':
                            b.Append("J ");
                            b2.Append("j");
                            break;
                        case 'К':
                            b.Append("K ");
                            b2.Append("k");
                            break;
                        case 'к':
                            b.Append("K ");
                            b2.Append("k");
                            break;
                        case 'Л':
                            b.Append("L ");
                            b2.Append("l");
                            break;
                        case 'л':
                            b.Append("L ");
                            b2.Append("l");
                            break;
                        case 'М':
                            b.Append("M ");
                            b2.Append("m");
                            break;
                        case 'м':
                            b.Append("M ");
                            b2.Append("m");
                            break;
                        case 'Н':
                            b.Append("N ");
                            b2.Append("n");
                            break;
                        case 'н':
                            b.Append("N ");
                            b2.Append("n");
                            break;
                        case 'О':
                            b.Append("O ");
                            b2.Append("o");
                            break;
                        case 'о':
                            b.Append("O ");
                            b2.Append("o");
                            break;
                        case 'П':
                            b.Append("P ");
                            b2.Append("p");
                            break;
                        case 'п':
                            b.Append("P ");
                            b2.Append("p");
                            break;
                        case 'Р':
                            b.Append("RR ");
                            b2.Append("r");
                            break;
                        case 'р':
                            b.Append("RR ");
                            b2.Append("r");
                            break;
                        case 'С':
                            b.Append("S ");
                            b2.Append("s");
                            break;
                        case 'с':
                            b.Append("S ");
                            b2.Append("s");
                            break;
                        case 'Т':
                            b.Append("T ");
                            b2.Append("t");
                            break;
                        case 'т':
                            b.Append("T ");
                            b2.Append("t");
                            break;
                        case 'У':
                            b.Append("U ");
                            b2.Append("u");
                            break;
                        case 'у':
                            b.Append("U ");
                            b2.Append("u");
                            break;
                        case 'Ф':
                            b.Append("PH ");
                            b2.Append("f");
                            break;
                        case 'ф':
                            b.Append("PH ");
                            b2.Append("f");
                            break;
                        case 'Х':
                            b.Append("X ");
                            b2.Append("ch");
                            break;
                        case 'х':
                            b.Append("X ");
                            b2.Append("ch");
                            break;
                        case 'Ц':
                            b.Append("TS ");
                            b2.Append("ts");
                            break;
                        case 'ц':
                            b.Append("TS ");
                            b2.Append("ts");
                            break;
                        case 'Ч':
                            b.Append("SH ");
                            b2.Append("sch");
                            break;
                        case 'ч':
                            b.Append("SH ");
                            b2.Append("sch");
                            break;
                        case 'Ш':
                            b.Append("SH ");
                            b2.Append("sch");
                            break;
                        case 'ш':
                            b.Append("SH ");
                            b2.Append("sch");
                            break;
                        case 'Щ':
                            b.Append("SH pal ");
                            b2.Append("sch");
                            break;
                        case 'щ':
                            b.Append("SH pal ");
                            b2.Append("sch");
                            break;
                        case 'Ы':
                            b.Append("YX ");
                            b2.Append("i");
                            break;
                        case 'ы':
                            b.Append("YX ");
                            b2.Append("i");
                            break;
                        case 'Э':
                            b.Append("AX ");
                            b2.Append("ä");
                            break;
                        case 'э':
                            b.Append("AX ");
                            b2.Append("ä");
                            break;
                        case 'Ю':
                            b.Append("J U ");
                            b2.Append("jü");
                            break;
                        case 'ю':
                            b.Append("J U ");
                            b2.Append("jü");
                            break;
                        case 'Я':
                            b.Append("J A ");
                            b2.Append("ja");
                            break;
                        case 'я':
                            b.Append("J A ");
                            b2.Append("ja");
                            break;
                        case 'ъ':
                            b.Append(". J ");
                            b2.Append("'j");
                            break;
                        case 'ь':
                            b.Append("pal ");
                            b2.Append("j");
                            break;
                        case ' ':
                            b.Append("\">"+b2.ToString());
                            b2 = new StringBuilder();
                            b.Append("</phoneme>\r\n<phoneme alphabet=\"ups\" ph=\"");
                            break;
                    }
                }
                b.Append("\">"+b2.ToString());
                b2 = new StringBuilder();
                b.Append("</phoneme>\r\n");

                try
                {
                    if (bAsync)
                        reader.SpeakSsmlAsync(string.Format(@"<speak version='1.0' " +
                                                "xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='de-DE'>{0}</speak>",
                        b.ToString()
                        ));
                    else
                        reader.SpeakSsml(string.Format(@"<speak version='1.0' " +
                                                "xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='de-DE'>{0}</speak>",
                        b.ToString()
                        ));
                }
                catch
                {
                    // ignore
                }
                

            }
            else
            {
                // decide, if we support speaking this language
                string strSSMLLanguageToSpeak = "xml:lang='" + Program.LanguageCodeFromName( strLanguage ) + "'";

                if (!string.IsNullOrEmpty(strSSMLLanguageToSpeak))
                {
                    string strSsml = string.Format(@"<speak version='1.0' " +
                                                            "xmlns='http://www.w3.org/2001/10/synthesis' {0}>{1}</speak>",
                                                            strSSMLLanguageToSpeak,
                                                            strText);
                    bool bFallback = true;
                    try
                    {
                        bFallback = !bUseESpeak || string.IsNullOrEmpty(strESpeakPath) || !System.IO.File.Exists(strESpeakPath);

                        if (!bFallback)
                        {
                            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = strESpeakPath, // Replace with your executable file
                                Arguments = "-m \""+strSsml+"\"", // Replace with any arguments if needed
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
}
