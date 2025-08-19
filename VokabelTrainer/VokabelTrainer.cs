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
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;


namespace VokabelTrainer
{
    //*******************************************************************************************************
    /// <summary>
    /// Main form of the application
    /// </summary>
    //*******************************************************************************************************
    public partial class VokabelTrainer : Form
    {
        //===================================================================================================
        /// <summary>
        /// First language of the vocabulary dictionary
        /// </summary>
        string m_strFirstLanguage;
        //===================================================================================================
        /// <summary>
        /// Second language of the vocabulary dictionary
        /// </summary>
        string m_strSecondLanguage;
        //===================================================================================================
        /// <summary>
        /// Current path of the vocabulary file
        /// </summary>
        string m_strCurrentPath;
        //===================================================================================================
        /// <summary>
        /// A random number generator for choosing words 
        /// </summary>
        Random m_oRnd;
        //===================================================================================================
        /// <summary>
        /// A second random number generator for even more randomness
        /// </summary>
        Random m_oRnd2;
        //===================================================================================================
        /// <summary>
        /// Current vocabulary document
        /// </summary>
        System.Xml.XmlDocument m_oCurrentVocabularyDoc;
        //===================================================================================================
        /// <summary>
        /// Indicates that first language has right-to-left writing direction
        /// </summary>
        bool m_bFirstLanguageRtl;
        //===================================================================================================
        /// <summary>
        /// Indicates that second language has right-to-left writing direction
        /// </summary>
        bool m_bSecondLanguageRtl;

        //===================================================================================================
        /// <summary>
        /// Holds training results of the first language. 1 for correct 0 for error. 
        /// </summary>
        SortedDictionary<string, string> m_oTrainingResultsFirstLanguage;
        //===================================================================================================
        /// <summary>
        /// Holds training results of the second language. 1 for correct 0 for error. 
        /// </summary>
        SortedDictionary<string, string> m_oTtrainingResultsSecondLanguage;
        //===================================================================================================
        /// <summary>
        /// Holds count of correct answers for the first language
        /// </summary>
        SortedDictionary<string, int> m_oCorrectAnswersFirstLanguage;
        //===================================================================================================
        /// <summary>
        /// Holds count of correct answers for the second language
        /// </summary>
        SortedDictionary<string, int> m_oCorrectSecondLanguage;
        //===================================================================================================
        /// <summary>
        /// Mapping from first language to all meanings in second
        /// </summary>
        SortedDictionary<string, SortedDictionary<string,bool>> m_oFirstToSecond;
        //===================================================================================================
        /// <summary>
        /// Mapping from second language to all meanings in firs
        /// </summary>
        SortedDictionary<string, SortedDictionary<string,bool>> m_oSecondToFirst;

        //===================================================================================================
        /// <summary>
        /// List of recently trained words
        /// </summary>
        LinkedList<string> m_oRecentlyTrainedWords = new LinkedList<string>();

        //===================================================================================================
        /// <summary>
        /// Holds total number of errors in first language, for correct calculation of random number in
        /// training
        /// </summary>
        int m_nTotalNumberOfErrorsFirstLanguage;
        //===================================================================================================
        /// <summary>
        /// Holds total number of errors in second language, for correct calculation of random number in
        /// training
        /// </summary>
        int m_nTotalNumberOfErrorsSecondLanguage;

        //===================================================================================================
        /// <summary>
        /// Provides indication, if last asked words must be skipped
        /// </summary>
        bool m_bSkipLast;
        //===================================================================================================
        /// <summary>
        /// The text of the license for the vocabulary file
        /// </summary>
        string m_strLicense;

        //===================================================================================================
        /// <summary>
        /// Provides indication, if the vocabulary file is modifiable
        /// </summary>
        bool m_bModifiable;
        //===================================================================================================
        /// <summary>
        /// Provides indication, what shall be written into the modifiable flag in vocabulary file
        /// </summary>
        bool m_bIsModifiableFlagForXml;
        //===================================================================================================
        /// <summary>
        /// Provides indication, if save of the vocabulary file is possible
        /// </summary>
        bool m_bSavePossible;

        //===================================================================================================
        /// <summary>
        /// The data for total exercises
        /// </summary>
        private SortedDictionary<DateTime, int> m_oTotalGraphData;
        /// <summary>
        /// The data for the number or words
        /// </summary>
        private SortedDictionary<DateTime, int> m_oWordsGraphData;
        /// <summary>
        /// The data for learned words
        /// </summary>
        private SortedDictionary<DateTime, int> m_oLearnedWordsGraphData;

        //===================================================================================================
        /// <summary>
        /// Ramadan start dates
        /// </summary>
        private static readonly Dictionary<int, DateTime> s_oRamadanStartDates = new Dictionary<int, DateTime>
        {
            { 2025, new DateTime(2025, 3, 1) },
            { 2026, new DateTime(2026, 2, 18) },
            { 2027, new DateTime(2027, 2, 8) },
            { 2028, new DateTime(2028, 1, 28) },
            { 2029, new DateTime(2029, 1, 16) },
            { 2030, new DateTime(2030, 1, 5) },
            { 2031, new DateTime(2031, 12, 15) },
            { 2032, new DateTime(2032, 12, 4) },
            { 2033, new DateTime(2033, 11, 23) },
            { 2034, new DateTime(2034, 11, 12) },
            { 2035, new DateTime(2035, 11, 1) }
        };


        //===================================================================================================
        /// <summary>
        /// Predefined Diwali dates (using Hindu lunar calendar)
        /// </summary> 
        private static readonly Dictionary<int, DateTime> s_oDiwaliDates = new Dictionary<int, DateTime>
        {
            { 2025, new DateTime(2025, 10, 21) },
            { 2026, new DateTime(2026, 11, 9) },
            { 2027, new DateTime(2027, 10, 29) },
            { 2028, new DateTime(2028, 10, 17) },
            { 2029, new DateTime(2029, 11, 5) },
            { 2030, new DateTime(2030, 10, 26) },
            { 2031, new DateTime(2031, 10, 15) },
            { 2032, new DateTime(2032, 11, 2) },
            { 2033, new DateTime(2033, 10, 23) },
            { 2034, new DateTime(2034, 11, 11) }
        };


        //===================================================================================================
        /// <summary>
        /// Exact known Chinese New Year dates 2025..2044
        /// </summary>
        private static Dictionary<int, DateTime> s_oChineseNewYearDates = new Dictionary<int, DateTime>
        {
            { 2025, new DateTime(2025, 1, 29) },
            { 2026, new DateTime(2026, 2, 17) },
            { 2027, new DateTime(2027, 2, 6) },
            { 2028, new DateTime(2028, 1, 26) },
            { 2029, new DateTime(2029, 2, 13) },
            { 2030, new DateTime(2030, 2, 2) },
            { 2031, new DateTime(2031, 1, 22) },
            { 2032, new DateTime(2032, 2, 10) },
            { 2033, new DateTime(2033, 1, 29) },
            { 2034, new DateTime(2034, 2, 18) },
            { 2035, new DateTime(2035, 2, 7) },
            { 2036, new DateTime(2036, 1, 28) },
            { 2037, new DateTime(2037, 2, 15) },
            { 2038, new DateTime(2038, 2, 4) },
            { 2039, new DateTime(2039, 1, 24) },
            { 2040, new DateTime(2040, 2, 12) },
            { 2041, new DateTime(2041, 2, 1) },
            { 2042, new DateTime(2042, 1, 22) },
            { 2043, new DateTime(2043, 2, 10) },
            { 2044, new DateTime(2044, 1, 30) }
        };


        //===================================================================================================
        /// <summary>
        /// Known Hajj dates for 2025–2034
        /// </summary>
        private static Dictionary<int, DateTime> s_oKnownHajjDates = new Dictionary<int, DateTime>
        {
            { 2025, new DateTime(2025, 6, 5) },
            { 2026, new DateTime(2026, 5, 26) },
            { 2027, new DateTime(2027, 5, 16) },
            { 2028, new DateTime(2028, 5, 5) },
            { 2029, new DateTime(2029, 4, 24) },
            { 2030, new DateTime(2030, 4, 14) },
            { 2031, new DateTime(2031, 4, 3) },
            { 2032, new DateTime(2032, 3, 23) },
            { 2033, new DateTime(2033, 3, 12) },
            { 2034, new DateTime(2034, 3, 2) }
        };


        //===================================================================================================
        /// <summary>
        /// Exact dates of Rosh Hashanah
        /// </summary>
        private static readonly Dictionary<int, DateTime> s_oRoshHashanahDates = new Dictionary<int, DateTime>
        {
            {2025, new DateTime(2025, 9, 22)},
            {2026, new DateTime(2026, 9, 11)},
            {2027, new DateTime(2027, 10, 1)},
            {2028, new DateTime(2028, 9, 20)},
            {2029, new DateTime(2029, 9, 9)},
            {2030, new DateTime(2030, 9, 28)},
            {2031, new DateTime(2031, 9, 18)},
            {2032, new DateTime(2032, 9, 6)},
            {2033, new DateTime(2033, 9, 24)},
            {2034, new DateTime(2034, 9, 14)},
            {2035, new DateTime(2035, 10, 4)},
            {2036, new DateTime(2036, 9, 22)},
            {2037, new DateTime(2037, 9, 11)},
            {2038, new DateTime(2038, 10, 1)},
            {2039, new DateTime(2039, 9, 21)},
            {2040, new DateTime(2040, 9, 10)},
            {2041, new DateTime(2041, 9, 28)},
            {2042, new DateTime(2042, 9, 18)},
            {2043, new DateTime(2043, 9, 6)},
            {2044, new DateTime(2044, 9, 24)}
        };


        //===================================================================================================
        /// <summary>
        /// Results of Pow Calculation
        /// </summary>
        private static readonly Dictionary<KeyValuePair<int, double>, double> s_oPowResults =
            new Dictionary<KeyValuePair<int, double>, double>();


        //===================================================================================================
        /// <summary>
        /// Calculates Power of x to y. Reuses already known results.
        /// </summary>
        /// <param name="nX">x</param>
        /// <param name="dblY">y</param>
        /// <returns>Power of x to y</returns>
        //===================================================================================================
        static double Pow(int nX, double dblY)
        {
            KeyValuePair<int,double> oKey = new KeyValuePair<int,double>(nX,dblY);
            if (s_oPowResults.ContainsKey(oKey))
                return s_oPowResults[oKey];

            return s_oPowResults[oKey] = Math.Pow(nX, dblY);
        }


        //===================================================================================================
        /// <summary>
        /// Constructs a new vocabulary trainer object
        /// </summary>
        //===================================================================================================
        public VokabelTrainer()
        {
            InitializeComponent();

            // init random with current time
            m_oRnd = new Random(((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);
            m_oRnd2 = new Random((((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond)*365+DateTime.UtcNow.DayOfYear);
            m_cbxReader.SelectedIndex = 0;
            m_btnStats.Enabled = false;

            // Variations of main header, depending on dates, let's start with Easter, other will follow
            DateTime dtmNow = DateTime.Now;
            if (dtmNow >= GetEasterStart() && dtmNow < GetEasterEnd())
            {
                if (ReadyToUseImageInjection("Images\\EasterHeader.jpg"))
                    return;
            };

            // Christmas
            if (dtmNow >= GetChristmasStart() && dtmNow < GetChristmasEnd())
            {
                if (ReadyToUseImageInjection("Images\\ChristmasHeader.jpg"))
                    return;
            };

            // New year
            if (dtmNow >= GetNewYearStart() || dtmNow < GetNewYearEnd())
            {
                if (ReadyToUseImageInjection("Images\\NewYearHeader.jpg"))
                    return;
            };

            // Ramadan
            if (dtmNow >= GetRamadanStart() && dtmNow < GetRamadanEnd())
            {
                if (ReadyToUseImageInjection("Images\\RamadanHeader.jpg"))
                    return;
            };


            // Diwali (Hindu Light celebration for truth winning over lies)
            if (dtmNow >= GetDiwaliStart() && dtmNow < GetDiwaliEnd())
            {
                if (ReadyToUseImageInjection("Images\\DiwaliHeader.jpg"))
                    return;
            };

            // Chinese new year
            if (dtmNow >= GetChineseNewYearStart() && dtmNow < GetChineseNewYearEnd())
            {
                if (ReadyToUseImageInjection("Images\\ChineseNewYearHeader.jpg"))
                    return;
            };

            // Orthodox Christmas
            if (dtmNow >= GetOrthodoxChristmasStart() && dtmNow < GetOrthodoxChristmasEnd())
            {
                if (ReadyToUseImageInjection("Images\\OrthodoxChristmasHeader.jpg"))
                    return;
            };


            // Muslim Hajj pilgrimage
            if (dtmNow >= GetHajjStart() && dtmNow < GetHajjEnd())
            {
                if (ReadyToUseImageInjection("Images\\HajjHeader.jpg"))
                    return;
            };


            // Rosh Hashanah (Israeli new year) 
            if (dtmNow >= GetRoshHashanahStart() && dtmNow < GetRoshHashanahEnd())
            {
                if (ReadyToUseImageInjection("Images\\RoshHashanahHeader.jpg"))
                    return;
            };

            // Halloween 
            if (dtmNow >= GetHalloweenStart() && dtmNow < GetHalloweenEnd())
            {
                if (ReadyToUseImageInjection("Images\\HalloweenHeader.jpg"))
                    return;
            };

            // Japanese children celebration and Korean Buddha birthday celebration 
            if (dtmNow >= GetChildrenAndBuddhaStart() && dtmNow < GetChildrenAndBuddhaEnd())
            {
                if (ReadyToUseImageInjection("Images\\ChildrenAndBuddhaHeader.jpg"))
                    return;
            };

            // Day of Science
            if (dtmNow >= GetScienceStart() && dtmNow < GetScienceEnd())
            {
                if (ReadyToUseImageInjection("Images\\ScienceDayHeader.jpg"))
                    return;
            };

            // Philosophy day
            if (dtmNow >= GetPhilosophyStart() && dtmNow < GetPhilosophyEnd())
            {
                if (ReadyToUseImageInjection("Images\\PhilosophyDayHeader.jpg"))
                    return;
            };

            // Psychology day
            if (dtmNow >= GetPsychologyStart() && dtmNow < GetPsychologyEnd())
            {
                if (ReadyToUseImageInjection("Images\\PsychologyDayHeader.jpg"))
                    return;
            };

            // Reading day
            if (dtmNow >= GetReadingDayStart() && dtmNow < GetReadingDayEnd())
            {
                if (ReadyToUseImageInjection("Images\\ReadingDayHeader.jpg"))
                    return;
            };

            // Valentine day
            if (dtmNow >= GetValentineStart() && dtmNow < GetValentineEnd())
            {
                if (ReadyToUseImageInjection("Images\\ValentineHeader.jpg"))
                    return;
            };

            // If there is no special header, then use default
            ReadyToUseImageInjection("Images\\VokabelTrainerMainHeader.jpg");
        }

        //===================================================================================================
        /// <summary>
        /// Enables or disables buttons, depending on the current situation
        /// </summary>
        //===================================================================================================
        public void EnableDisableButtons()
        {
            m_btnNewLanguageFile.Enabled = true;
            m_btnEnterVocabulary.Enabled = (m_strCurrentPath != null) && m_bModifiable;
            m_btnExerciseSecondToFirst.Enabled = (m_strCurrentPath != null) && m_oFirstToSecond.Count > 0;
            m_btnExerciseFirstToSecond.Enabled = (m_strCurrentPath != null) && m_oSecondToFirst.Count > 0;
            if (string.IsNullOrEmpty(m_strFirstLanguage) || string.IsNullOrEmpty(m_strSecondLanguage))
            {
                m_btnExerciseSecondToFirst.Text = 
                    m_btnExerciseFirstToSecond.Text = 
                    m_btnIntensiveSecondToFirst.Text = 
                    m_btnIntensiveFirstToSecond.Text = 
                    m_btnMostIntensiveSecondToFirst.Text = 
                    m_btnMostIntensiveFirstToSecond.Text = "";

                m_btnExerciseSecondToFirst.Enabled = 
                    m_btnExerciseFirstToSecond.Enabled = 
                    m_btnIntensiveSecondToFirst.Enabled = 
                    m_btnIntensiveFirstToSecond.Enabled = 
                    m_btnMostIntensiveSecondToFirst.Enabled = 
                    m_btnMostIntensiveFirstToSecond.Enabled = false;

                m_cbxReader.Enabled = false;
                m_lblReader.Enabled = false;

                m_lblDontLearnAquire.Enabled = false;
                m_lblDontLearnAquire.Visible = false;
                m_btnStats.Enabled = false;
            }
            else
            {
                m_btnExerciseSecondToFirst.Text = 
                    string.Format(Properties.Resources.Exercise, m_strSecondLanguage, m_strFirstLanguage);
                m_btnExerciseFirstToSecond.Text = 
                    string.Format(Properties.Resources.Exercise, m_strFirstLanguage, m_strSecondLanguage);
                m_btnIntensiveSecondToFirst.Enabled = 
                    m_btnExerciseSecondToFirst.Enabled = m_oTrainingResultsFirstLanguage.Count > 0;
                m_btnIntensiveFirstToSecond.Enabled = 
                    m_btnExerciseFirstToSecond.Enabled = m_oTtrainingResultsSecondLanguage.Count > 0;
                m_lblReader.Enabled = m_cbxReader.Enabled = 
                    m_oTrainingResultsFirstLanguage.Count > 0 || m_oTtrainingResultsSecondLanguage.Count > 0;


                m_btnIntensiveSecondToFirst.Text = 
                    string.Format(Properties.Resources.Intensive, m_strSecondLanguage, m_strFirstLanguage);
                m_btnIntensiveFirstToSecond.Text = 
                    string.Format(Properties.Resources.Intensive, m_strFirstLanguage, m_strSecondLanguage);


                m_btnMostIntensiveSecondToFirst.Text = 
                    string.Format(Properties.Resources.MostIntensive, m_strSecondLanguage, m_strFirstLanguage );
                m_btnMostIntensiveFirstToSecond.Text = 
                    string.Format(Properties.Resources.MostIntensive, m_strFirstLanguage, m_strSecondLanguage );


                m_btnMostIntensiveSecondToFirst.Enabled = m_nTotalNumberOfErrorsSecondLanguage > 0;
                m_btnMostIntensiveFirstToSecond.Enabled = m_nTotalNumberOfErrorsFirstLanguage > 0;

                List<string> oEasyLearn = GetEasyLanguageAquireList();
                if (oEasyLearn != null && oEasyLearn.Count > 0)
                {
                    m_lblDontLearnAquire.Enabled = true;
                    m_lblDontLearnAquire.Visible = true;
                } else
                {
                    m_lblDontLearnAquire.Enabled = false;
                    m_lblDontLearnAquire.Visible = false;
                }

                m_btnStats.Enabled = m_oTotalGraphData != null && m_oTotalGraphData.Count > 1;
            }
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user wants to load a language file
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnLoadLanguageFileClick(
            object oSender, 
            EventArgs oArgs
            )
        {
            int nTotalCountCorrectAnswers = 0;

            m_dlgOpenFileDialog.InitialDirectory = 
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag.StartsWith("de"))
                m_dlgOpenFileDialog.DefaultExt = "Vokabeln.xml";
            if (m_dlgOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_strCurrentPath = m_dlgOpenFileDialog.FileName;
                    m_oCurrentVocabularyDoc = new System.Xml.XmlDocument();
                    m_oCurrentVocabularyDoc.Load(m_strCurrentPath);

                    m_oTrainingResultsFirstLanguage = new SortedDictionary<string, string>();
                    m_oTtrainingResultsSecondLanguage = new SortedDictionary<string, string>();
                    m_oFirstToSecond = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    m_oSecondToFirst = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    m_oCorrectAnswersFirstLanguage = new SortedDictionary<string, int>();
                    m_oCorrectSecondLanguage = new SortedDictionary<string, int>();
                    m_nTotalNumberOfErrorsFirstLanguage = 0;
                    m_nTotalNumberOfErrorsSecondLanguage = 0;

                    m_strSecondLanguage = m_oCurrentVocabularyDoc.SelectSingleNode("/vokabeln/zweite-sprache-name").InnerText;
                    m_strFirstLanguage = m_oCurrentVocabularyDoc.SelectSingleNode("/vokabeln/erste-sprache-name").InnerText;
                    m_bFirstLanguageRtl = m_oCurrentVocabularyDoc.SelectSingleNode("/vokabeln/erste-sprache-rtl") != null ?
                        m_oCurrentVocabularyDoc.SelectSingleNode("/vokabeln/erste-sprache-rtl").InnerText.Equals("ja") : false;
                    m_bSecondLanguageRtl = m_oCurrentVocabularyDoc.SelectSingleNode("/vokabeln/zweite-sprache-rtl") != null ?
                        m_oCurrentVocabularyDoc.SelectSingleNode("/vokabeln/zweite-sprache-rtl").InnerText.Equals("ja") : false;
                    m_bModifiable = true;
                    m_strLicense = "";
                    foreach (System.Xml.XmlElement e in m_oCurrentVocabularyDoc.SelectNodes("/vokabeln/lizenz"))
                    {
                        m_bIsModifiableFlagForXml = m_bModifiable = 
                                e.SelectSingleNode("modifikationen").InnerText.Equals("Unter Lizenzbedingungen", 
                                StringComparison.CurrentCultureIgnoreCase);

                        m_bSavePossible = m_bIsModifiableFlagForXml || 
                            e.SelectSingleNode("modifikationen").InnerText.Equals(
                            "Keine neuen Wörter und keine Lizenzänderungen", StringComparison.CurrentCultureIgnoreCase);

                        m_strLicense = e.SelectSingleNode("text").InnerText;
                    }

                    foreach (System.Xml.XmlElement e in m_oCurrentVocabularyDoc.SelectNodes("/vokabeln/erste-sprache"))
                    {
                        string strTrainingProgress = e.SelectSingleNode("training-vorgeschichte").InnerText;
                        if (strTrainingProgress.Length > 6)
                            strTrainingProgress = strTrainingProgress.Substring(0, 6);
                        else
                            while (strTrainingProgress.Length < 6)
                                strTrainingProgress = strTrainingProgress + "1";

                        m_oTrainingResultsFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = strTrainingProgress;
                        m_nTotalNumberOfErrorsFirstLanguage += strTrainingProgress.Length - strTrainingProgress.Replace("0", "").Length; 
                        m_oFirstToSecond[e.SelectSingleNode("vokabel").InnerText] = new SortedDictionary<string, bool>();

                        System.Xml.XmlNode n = e.SelectSingleNode("richtige-antworten");
                        if (n != null)
                        {
                            string strCorrectAnswers = n.InnerText;
                            int nCorrectAnswers = 0;
                            if (!int.TryParse(strCorrectAnswers, out nCorrectAnswers))
                                nCorrectAnswers = 0;
                            else
                                if (nCorrectAnswers < 0)
                                    nCorrectAnswers = 0;
                            nTotalCountCorrectAnswers += nCorrectAnswers;
                            m_oCorrectAnswersFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = nCorrectAnswers;
                        } else
                            m_oCorrectAnswersFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = 0;
                    }

                    foreach (System.Xml.XmlElement e in m_oCurrentVocabularyDoc.SelectNodes("/vokabeln/zweite-sprache"))
                    {
                        string strTrainingProgress = e.SelectSingleNode("training-vorgeschichte").InnerText;
                        if (strTrainingProgress.Length > 6)
                            strTrainingProgress = strTrainingProgress.Substring(0, 6);
                        else
                            while (strTrainingProgress.Length < 6)
                                strTrainingProgress = strTrainingProgress + "1";
                        m_oTtrainingResultsSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = strTrainingProgress;
                        m_oSecondToFirst[e.SelectSingleNode("vokabel").InnerText] = new SortedDictionary<string, bool>();
                        m_nTotalNumberOfErrorsSecondLanguage += strTrainingProgress.Length - strTrainingProgress.Replace("0", "").Length;

                        System.Xml.XmlNode n = e.SelectSingleNode("richtige-antworten");
                        if (n != null)
                        {
                            string strCorrectAnswers = n.InnerText;
                            int nCorrectAnswers = 0;
                            if (!int.TryParse(strCorrectAnswers, out nCorrectAnswers))
                                nCorrectAnswers = 0;
                            else
                                if (nCorrectAnswers < 0)
                                    nCorrectAnswers = 0;
                            nTotalCountCorrectAnswers += nCorrectAnswers;
                            m_oCorrectSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = nCorrectAnswers;
                        } else
                            m_oCorrectSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = 0;

                    }


                    foreach (System.Xml.XmlElement e in m_oCurrentVocabularyDoc.SelectNodes("/vokabeln/vokabel-paar"))
                    {
                        string strFirstLanguage = e.SelectSingleNode("erste-sprache").InnerText;
                        string strSecondLanguage = e.SelectSingleNode("zweite-sprache").InnerText;

                        if (!m_oTrainingResultsFirstLanguage.ContainsKey(strFirstLanguage))
                        {
                            m_oTrainingResultsFirstLanguage[strFirstLanguage] = "111110";
                            m_nTotalNumberOfErrorsFirstLanguage += 1;
                            m_oFirstToSecond[strFirstLanguage] = new SortedDictionary<string, bool>();
                        }

                        if (!m_oTtrainingResultsSecondLanguage.ContainsKey(strSecondLanguage))
                        {
                            m_oTtrainingResultsSecondLanguage[strSecondLanguage] = "111110";
                            m_nTotalNumberOfErrorsSecondLanguage += 1;
                            m_oSecondToFirst[strSecondLanguage] = new SortedDictionary<string, bool>();
                        }

                        if (!m_oCorrectAnswersFirstLanguage.ContainsKey(strFirstLanguage))
                            m_oCorrectAnswersFirstLanguage[strFirstLanguage] = 0;

                        if (!m_oCorrectSecondLanguage.ContainsKey(strSecondLanguage))
                            m_oCorrectSecondLanguage[strSecondLanguage] = 0;


                        if (!m_oFirstToSecond[strFirstLanguage].ContainsKey(strSecondLanguage))
                            m_oFirstToSecond[strFirstLanguage][strSecondLanguage] = false;

                        if (!m_oSecondToFirst[strSecondLanguage].ContainsKey(strFirstLanguage))
                            m_oSecondToFirst[strSecondLanguage][strFirstLanguage] = false;
                    }

                }
                catch (Exception ex)
                {
                    NewMessageBox.Show(this, ex.Message, Properties.Resources.ErrorLoadingXmlFileHeader, 
                        string.Format(Properties.Resources.ErrorLoadingXmlFileMessage, ex.Message));

                    m_strCurrentPath = null;
                    m_oCurrentVocabularyDoc = null;

                    m_oTrainingResultsFirstLanguage = new SortedDictionary<string, string>();
                    m_oTtrainingResultsSecondLanguage = new SortedDictionary<string, string>();
                    m_oFirstToSecond = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    m_oSecondToFirst = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    m_oCorrectAnswersFirstLanguage = new SortedDictionary<string, int>();
                    m_oCorrectSecondLanguage = new SortedDictionary<string, int>();
                    m_nTotalNumberOfErrorsFirstLanguage = 0;
                    m_nTotalNumberOfErrorsSecondLanguage = 0;
                }

                // continue with loading of training file, after vocabulary has been loaded
                if (m_strCurrentPath!=null)
                try {

                    string strCurrentPath = m_strCurrentPath.Replace(".Vokabeln.xml",".Training.xml")
                        .Replace("Vocabulary.xml",".Training.xml");

                    System.IO.FileInfo fi = new System.IO.FileInfo(strCurrentPath);
                    if (fi.Exists)
                    {
                        System.Xml.XmlDocument oCurrentDoc = new System.Xml.XmlDocument();
                        oCurrentDoc.Load(strCurrentPath);

                        foreach (System.Xml.XmlElement e in oCurrentDoc.SelectNodes("/training/erste-sprache"))
                        {
                            string strTrainingProgress = e.SelectSingleNode("training-vorgeschichte").InnerText;
                            if (strTrainingProgress.Length > 6)
                                strTrainingProgress = strTrainingProgress.Substring(0, 6);
                            else
                                while (strTrainingProgress.Length < 6)
                                    strTrainingProgress = strTrainingProgress + "1";

                            if (m_oTrainingResultsFirstLanguage.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                            {
                                m_nTotalNumberOfErrorsFirstLanguage += strTrainingProgress.Length - strTrainingProgress.Replace("0", "").Length
                                      - (m_oTrainingResultsFirstLanguage[e.SelectSingleNode("vokabel").InnerText].Length -
                                          m_oTrainingResultsFirstLanguage[e.SelectSingleNode("vokabel").InnerText].Replace("0", "").Length);

                                m_oTrainingResultsFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = strTrainingProgress;

                                if (!m_oFirstToSecond.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                                    m_oFirstToSecond[e.SelectSingleNode("vokabel").InnerText] = new SortedDictionary<string, bool>();

                                System.Xml.XmlNode n = e.SelectSingleNode("richtige-antworten");
                                if (n != null)
                                {
                                    string strCorrectAnswers = n.InnerText;
                                    int nCorrectAnswers = 0;
                                    if (!int.TryParse(strCorrectAnswers, out nCorrectAnswers))
                                        nCorrectAnswers = 0;
                                    else
                                        if (nCorrectAnswers < 0)
                                            nCorrectAnswers = 0;

                                    nTotalCountCorrectAnswers += nCorrectAnswers;
                                    m_oCorrectAnswersFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = nCorrectAnswers;
                                }
                                else
                                    m_oCorrectAnswersFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = 0;
                            }
                        }

                        foreach (System.Xml.XmlElement e in oCurrentDoc.SelectNodes("/training/zweite-sprache"))
                        {
                            string strTrainingProgress = e.SelectSingleNode("training-vorgeschichte").InnerText;
                            if (strTrainingProgress.Length > 6)
                                strTrainingProgress = strTrainingProgress.Substring(0, 6);
                            else
                                while (strTrainingProgress.Length < 6)
                                    strTrainingProgress = strTrainingProgress + "1";

                            if (m_oTtrainingResultsSecondLanguage.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                            {
                                m_nTotalNumberOfErrorsSecondLanguage += strTrainingProgress.Length - strTrainingProgress.Replace("0", "").Length
                                   - (m_oTtrainingResultsSecondLanguage[e.SelectSingleNode("vokabel").InnerText].Length -
                                      m_oTtrainingResultsSecondLanguage[e.SelectSingleNode("vokabel").InnerText].Replace("0", "").Length);

                                m_oTtrainingResultsSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = strTrainingProgress;

                                if (!m_oSecondToFirst.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                                    m_oSecondToFirst[e.SelectSingleNode("vokabel").InnerText] = new SortedDictionary<string, bool>();

                                System.Xml.XmlNode n = e.SelectSingleNode("richtige-antworten");
                                if (n != null)
                                {
                                    string strCorrectAnswers = n.InnerText;
                                    int nCorrectAnswers = 0;
                                    if (!int.TryParse(strCorrectAnswers, out nCorrectAnswers))
                                        nCorrectAnswers = 0;
                                    else
                                        if (nCorrectAnswers < 0)
                                            nCorrectAnswers = 0;

                                    nTotalCountCorrectAnswers += nCorrectAnswers;
                                    m_oCorrectSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = nCorrectAnswers;
                                }
                                else
                                    m_oCorrectSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = 0;
                            }

                        }

                        m_oTotalGraphData = new SortedDictionary<DateTime, int>();
                        m_oWordsGraphData = new SortedDictionary<DateTime, int>();
                        m_oLearnedWordsGraphData = new SortedDictionary<DateTime, int>();

                        DateTime dtmLastStatsDate = DateTime.MinValue;

                        // load the training stats over time
                        foreach (System.Xml.XmlElement e in oCurrentDoc.SelectNodes("/training/zustand"))
                        {
                            try
                            {
                                DateTime dtmStatsDate = DateTime.Parse(e.SelectSingleNode("datum").InnerText);
                                int nNumberWords = int.Parse(e.SelectSingleNode("woerter").InnerText);
                                int nNumberErrors = int.Parse(e.SelectSingleNode("fehler").InnerText);
                                int nCorrectAnswers = int.Parse(e.SelectSingleNode("richtige-antworten").InnerText);

                                m_oTotalGraphData[dtmStatsDate] = nCorrectAnswers + nNumberWords;
                                m_oWordsGraphData[dtmStatsDate] = nNumberWords;
                                m_oLearnedWordsGraphData[dtmStatsDate] = nNumberWords - nNumberErrors;

                                if (dtmLastStatsDate < dtmStatsDate)
                                    dtmLastStatsDate = dtmLastStatsDate;
                            }
                            catch
                            {
                                // ignore
                            }
                        }

                        DateTime dtmStatsDate2 = DateTime.Now.Date;
                        int nNumberWords2 = m_oFirstToSecond.Keys.Count + m_oSecondToFirst.Keys.Count;
                        int nNumberErrors2 = m_nTotalNumberOfErrorsFirstLanguage + m_nTotalNumberOfErrorsSecondLanguage;

                        if (dtmLastStatsDate.Month != DateTime.Now.Month || dtmLastStatsDate.Year != DateTime.Now.Year)
                        {
                            m_oTotalGraphData[DateTime.Now.Date] = nTotalCountCorrectAnswers + nNumberWords2;
                            m_oWordsGraphData[DateTime.Now.Date] = nNumberWords2;
                            m_oLearnedWordsGraphData[DateTime.Now.Date] = nNumberWords2 - nNumberErrors2;

                            if (m_oTotalGraphData.Count == 1)
                            {
                                m_oTotalGraphData[DateTime.Now.Date.AddDays(-1)] = nTotalCountCorrectAnswers + nNumberWords2;
                                m_oWordsGraphData[DateTime.Now.Date.AddDays(-1)] = nNumberWords2;
                                m_oLearnedWordsGraphData[DateTime.Now.Date.AddDays(-1)] = nNumberWords2 - nNumberErrors2;
                            }
                        }
                        else
                        {
                            // if same month then remove the old date and put the new stats in last row
                            m_oTotalGraphData.Remove(dtmLastStatsDate);
                            m_oWordsGraphData.Remove(dtmLastStatsDate);
                            m_oLearnedWordsGraphData.Remove(dtmLastStatsDate);


                            m_oTotalGraphData[DateTime.Now.Date] = nTotalCountCorrectAnswers + nNumberWords2;
                            m_oWordsGraphData[DateTime.Now.Date] = nNumberWords2;
                            m_oLearnedWordsGraphData[DateTime.Now.Date] = nNumberWords2 - nNumberErrors2;
                        }

                    }
                }
                catch (Exception ex)
                {
                    NewMessageBox.Show(this, ex.Message, Properties.Resources.ErrorLoadingTrainingFileHeader,
                        string.Format(Properties.Resources.ErrorLoadingTrainingFileMessage, ex.Message));

                    m_strCurrentPath = null;
                    m_oCurrentVocabularyDoc = null;

                    m_oTrainingResultsFirstLanguage = new SortedDictionary<string, string>();
                    m_oTtrainingResultsSecondLanguage = new SortedDictionary<string, string>();
                    m_oFirstToSecond = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    m_oSecondToFirst = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    m_oCorrectAnswersFirstLanguage = new SortedDictionary<string, int>();
                    m_oCorrectSecondLanguage = new SortedDictionary<string, int>();
                    m_nTotalNumberOfErrorsFirstLanguage = 0;
                    m_nTotalNumberOfErrorsSecondLanguage = 0;
                }
            }

            EnableDisableButtons();
        }

        //===================================================================================================
        /// <summary>
        /// This is executed, when user decides to create a new language file
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnNewLanguageClick(
            object oSender, 
            EventArgs oArgs
            )
        {
            using (NewLanguageFile form = new NewLanguageFile())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    m_oTotalGraphData = new SortedDictionary<DateTime, int>();
                    m_oWordsGraphData = new SortedDictionary<DateTime, int>();
                    m_oLearnedWordsGraphData = new SortedDictionary<DateTime, int>();

                    m_oTotalGraphData[DateTime.Now.Date.AddDays(-1)] = 0;
                    m_oWordsGraphData[DateTime.Now.Date.AddDays(-1)] = 0;
                    m_oLearnedWordsGraphData[DateTime.Now.Date.AddDays(-1)] = 0;

                    m_oTotalGraphData[DateTime.Now.Date] = 0;
                    m_oWordsGraphData[DateTime.Now.Date] = 0;
                    m_oLearnedWordsGraphData[DateTime.Now.Date] = 0;


                    string strNewPath;


                    strNewPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                        form.m_tbxFirstLanguage.Text + "-" + form.m_tbxSecondLanguage.Text);

                    if (System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag.StartsWith("de"))
                        strNewPath += ".Vokabeln.xml";
                    else
                        strNewPath += ".Vocabulary.xml";

                    System.IO.FileInfo fi = new System.IO.FileInfo(strNewPath);
                    if (fi.Exists)
                    {
                        if (System.Windows.Forms.MessageBox.Show(this, 
                                string.Format(Properties.Resources.LanguageFileAlreadyExistsMessage, strNewPath), 
                                Properties.Resources.LanguageFileAlreadyExistsHeader,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                                != DialogResult.Yes)
                            return;
                    };
                    m_strCurrentPath = strNewPath;
                    m_oCurrentVocabularyDoc = new System.Xml.XmlDocument();
                    m_oCurrentVocabularyDoc.PreserveWhitespace = false;
                    m_bIsModifiableFlagForXml = form.m_chkLanguageFileModifiable.Checked;
                    m_bModifiable = true;
                    m_bSavePossible = true;
                    if (form.m_chkLanguageFileUnderGPL2.Checked)
                    {
                        m_strLicense = "Copyright (C) " + System.DateTime.Now.Year + " " + 
                                    Environment.GetEnvironmentVariable("USERNAME") + "\r\n\r\n" +
                                   "This program is free software; you can redistribute it and/or\r\n" +
                                   "modify it under the terms of the GNU General Public License\r\n" +
                                   "as published by the Free Software Foundation; either version 2\r\n" +
                                   "of the License, or (at your option) any later version.\r\n\r\n" +
                                   "This program is distributed in the hope that it will be useful,\r\n" +
                                   "but WITHOUT ANY WARRANTY; without even the implied warranty of\r\n" +
                                   "MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\r\n" +
                                   "GNU General Public License for more details.\r\n\r\n" +
                                   "You should have received a copy of the GNU General Public License\r\n" +
                                   "along with this program; if not, write to the Free Software\r\n" +
                                   "Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.";
                    }
                    else
                    {
                        m_strLicense = "Copyright (C) " + System.DateTime.Now.Year + " " + 
                            Environment.GetEnvironmentVariable("USERNAME") + ", all rights reserved";
                    }

                    m_oCurrentVocabularyDoc.LoadXml("<?xml version=\"1.0\" ?>\r\n<vokabeln>\r\n  <erste-sprache-name>" + 
                        form.m_tbxFirstLanguage.Text + "</erste-sprache-name>\r\n"+
                        "  <zweite-sprache-name>" + form.m_tbxSecondLanguage.Text + "</zweite-sprache-name>\r\n"+
                        "  <erste-sprache-rtl>"+(form.m_chkFirstLanguageRTL.Checked?"ja":"nein")+"</erste-sprache-rtl>\r\n"+
                        "  <zweite-sprache-rtl>" + (form.m_chkSecondLanguageRTL.Checked ? "ja" : "nein") + "</zweite-sprache-rtl>\r\n" +
                        "<lizenz><modifikationen>" + (m_bIsModifiableFlagForXml ? "Unter Lizenzbedingungen" : 
                        "Keine neuen Wörter und keine Lizenzänderungen") + "</modifikationen><text>" + m_strLicense + "</text></lizenz></vokabeln>\r\n");
                    m_oCurrentVocabularyDoc.Save(m_strCurrentPath);

                    m_bFirstLanguageRtl = form.m_chkFirstLanguageRTL.Checked;
                    m_bSecondLanguageRtl = form.m_chkSecondLanguageRTL.Checked;

                    m_oTrainingResultsFirstLanguage = new SortedDictionary<string,string>();
                    m_oTtrainingResultsSecondLanguage = new SortedDictionary<string,string>();
                    m_oFirstToSecond = new SortedDictionary<string,SortedDictionary<string,bool>>();
                    m_oSecondToFirst = new SortedDictionary<string,SortedDictionary<string,bool>>();
                    m_oCorrectAnswersFirstLanguage = new SortedDictionary<string, int>();
                    m_oCorrectSecondLanguage = new SortedDictionary<string, int>();
                    m_nTotalNumberOfErrorsFirstLanguage = 0;
                    m_nTotalNumberOfErrorsSecondLanguage = 0;
                    m_strFirstLanguage = form.m_tbxFirstLanguage.Text;
                    m_strSecondLanguage = form.m_tbxSecondLanguage.Text;
                }
            }
            EnableDisableButtons();
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user decides to enter new vocabulary
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_btnEnterVocabulary_Click(
            object oSender, 
            EventArgs oArgs
            )
        {
            bool bRepeat = true;
            bool bSave = false;

            string strFirstLanguageCode = null;
            string strSecondLanguageCode = null;

            while (bRepeat)
            {
                string strFirstText = "";
                string strSecondText = "";


                bool bRepeat2 = true;
                while (bRepeat2)
                {
                    bRepeat2 = false;
                    using (NewDictionaryPair pair =
                        new NewDictionaryPair(
                            m_chkUseESpeak.Checked,
                            m_tbxESpeakPath.Text,
                            m_strFirstLanguage,
                            m_strSecondLanguage))
                    {
                        if (strFirstLanguageCode != null)
                            pair.FirstLanguageCode = strFirstLanguageCode;
                        if (strSecondLanguageCode != null)
                            pair.SecondLanguageCode = strSecondLanguageCode;

                        char[] separators = { ',', ';' };
                        //pair.m_lblFirstLanguage.Text = m_strFirstLanguage + ":";
                        //pair.m_lblSecondLanguage.Text = m_strSecondLanguage + ":";
                        pair.m_tbxFirstLanguage.Text = strFirstText;
                        pair.m_tbxSecondLanguage.Text = strSecondText;
                        pair.m_lblFirstLanguage.RightToLeft = m_bFirstLanguageRtl?RightToLeft.Yes:RightToLeft.No;
                        pair.m_lblSecondLanguage.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        pair.m_tbxFirstLanguage.RightToLeft = m_bFirstLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        pair.m_tbxSecondLanguage.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        switch (pair.ShowDialog())
                        {
                            case DialogResult.Retry:
                                strFirstLanguageCode = pair.FirstLanguageCode;
                                strSecondLanguageCode = pair.SecondLanguageCode;
                                if (string.IsNullOrEmpty(pair.m_tbxFirstLanguage.Text.Trim()))
                                {
                                    strFirstText = "";
                                    strSecondText = pair.m_tbxSecondLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }
                                if (string.IsNullOrEmpty(pair.m_tbxSecondLanguage.Text.Trim()))
                                {
                                    strSecondText = "";
                                    strFirstText = pair.m_tbxFirstLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }

                                string sss1 = pair.m_tbxFirstLanguage.Text
                                    .Replace("?", "?,").Replace("!", "!,").Replace("،", ",").Replace("、", ",").Replace("，", ",")
                                    .Replace(";", ",").Replace(",,", ",").Replace(",,", ",");
                                string sss2 = pair.m_tbxSecondLanguage.Text
                                    .Replace("?", "?,").Replace("!", "!,").Replace("،", ",").Replace("、", ",").Replace("，", ",")
                                    .Replace(";", ",").Replace(",,", ",").Replace(",,", ",");

                                foreach (string ss1 in sss1.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                                    foreach (string ss2 in sss2.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        string s1 = ss1.Trim();
                                        string s2 = ss2.Trim();

                                        if (!m_oTrainingResultsFirstLanguage.ContainsKey(s1))
                                        {
                                            m_oTrainingResultsFirstLanguage[s1] = "111110";
                                            m_nTotalNumberOfErrorsFirstLanguage += 1;
                                            m_oFirstToSecond[s1] = new SortedDictionary<string, bool>();
                                        }
                                        else
                                        {
                                            if ("1".Equals(m_oTrainingResultsFirstLanguage[s1].Substring(5, 1)))
                                            {
                                                m_nTotalNumberOfErrorsFirstLanguage += 1;
                                                m_oTrainingResultsFirstLanguage[s1] = 
                                                    m_oTrainingResultsFirstLanguage[s1].Substring(0, 5) + "0"; 
                                                // +_trainingFirstLanguage[s1].Substring(6);
                                            }
                                        }

                                        if (!m_oTtrainingResultsSecondLanguage.ContainsKey(s2))
                                        {
                                            m_nTotalNumberOfErrorsSecondLanguage += 1;
                                            m_oTtrainingResultsSecondLanguage[s2] = "111110";

                                            m_oSecondToFirst[s2] = new SortedDictionary<string, bool>();
                                        }
                                        else
                                        {
                                            if ("1".Equals(m_oTtrainingResultsSecondLanguage[s2].Substring(5, 1)))
                                            {
                                                m_nTotalNumberOfErrorsSecondLanguage += 1;
                                                m_oTtrainingResultsSecondLanguage[s2] = 
                                                    m_oTtrainingResultsSecondLanguage[s2].Substring(0, 5) + "0"; 
                                                // +_trainingSecondLanguage[s2].Substring(6);
                                            }
                                        }

                                        if (!m_oCorrectAnswersFirstLanguage.ContainsKey(s1))
                                            m_oCorrectAnswersFirstLanguage[s1] = 0;
                                        else
                                            m_oCorrectAnswersFirstLanguage[s1] = 
                                                Math.Max(0,m_oCorrectAnswersFirstLanguage[s1]-3);


                                        if (!m_oCorrectSecondLanguage.ContainsKey(s2))
                                            m_oCorrectSecondLanguage[s2] = 0;
                                        else
                                            m_oCorrectSecondLanguage[s2] = 
                                                Math.Max(0, m_oCorrectSecondLanguage[s2] - 3);

                                        if (!m_oFirstToSecond[s1].ContainsKey(s2))
                                            m_oFirstToSecond[s1][s2] = false;
                                        if (!m_oSecondToFirst[s2].ContainsKey(s1))
                                            m_oSecondToFirst[s2][s1] = false;
                                    }
                                bSave = true;
                                bRepeat = true;
                                break;

                            case DialogResult.OK:
                                if (string.IsNullOrEmpty(pair.m_tbxFirstLanguage.Text.Trim()))
                                {
                                    strFirstText = "";
                                    strSecondText = pair.m_tbxSecondLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }
                                if (string.IsNullOrEmpty(pair.m_tbxSecondLanguage.Text.Trim()))
                                {
                                    strSecondText = "";
                                    strFirstText = pair.m_tbxFirstLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }

                                sss1 = pair.m_tbxFirstLanguage.Text
                                    .Replace("?", "?,").Replace("!", "!,").Replace("،", ",").Replace("、", ",").Replace("，", ",")
                                    .Replace(";", ",").Replace(",,", ",").Replace(",,", ",");
                                sss2 = pair.m_tbxSecondLanguage.Text
                                    .Replace("?", "?,").Replace("!", "!,").Replace("،", ",").Replace("、", ",").Replace("，", ",")
                                    .Replace(";", ",").Replace(",,", ",").Replace(",,", ",");

                                foreach (string ss1 in sss1.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                                    foreach (string ss2 in sss2.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        string s1 = ss1.Trim();
                                        string s2 = ss2.Trim();

                                        if (!m_oTrainingResultsFirstLanguage.ContainsKey(s1))
                                        {
                                            m_oTrainingResultsFirstLanguage[s1] = "111110";
                                            m_nTotalNumberOfErrorsFirstLanguage += 1;
                                            m_oFirstToSecond[s1] = new SortedDictionary<string, bool>();
                                        }
                                        else
                                        {
                                            if ("1".Equals(m_oTrainingResultsFirstLanguage[s1].Substring(5, 1)))
                                            {
                                                m_nTotalNumberOfErrorsFirstLanguage += 1;
                                                m_oTrainingResultsFirstLanguage[s1] = 
                                                    m_oTrainingResultsFirstLanguage[s1].Substring(0, 5) + "0"; 
                                                // +_trainingFirstLanguage[s1].Substring(6);
                                            }
                                        }

                                        if (!m_oTtrainingResultsSecondLanguage.ContainsKey(s2))
                                        {
                                            m_nTotalNumberOfErrorsSecondLanguage += 1;
                                            m_oTtrainingResultsSecondLanguage[s2] = "111110";

                                            m_oSecondToFirst[s2] = new SortedDictionary<string, bool>();
                                        }
                                        else
                                        {
                                            if ("1".Equals(m_oTtrainingResultsSecondLanguage[s2].Substring(5, 1)))
                                            {
                                                m_nTotalNumberOfErrorsSecondLanguage += 1;
                                                m_oTtrainingResultsSecondLanguage[s2] = 
                                                    m_oTtrainingResultsSecondLanguage[s2].Substring(0, 5) + "0"; 
                                                // +_trainingSecondLanguage[s2].Substring(6);
                                            }
                                        }

                                        if (!m_oCorrectAnswersFirstLanguage.ContainsKey(s1))
                                            m_oCorrectAnswersFirstLanguage[s1] = 0;
                                        else
                                            m_oCorrectAnswersFirstLanguage[s1] = 
                                                Math.Max(0, m_oCorrectAnswersFirstLanguage[s1] - 3);


                                        if (!m_oCorrectSecondLanguage.ContainsKey(s2))
                                            m_oCorrectSecondLanguage[s2] = 0;
                                        else
                                            m_oCorrectSecondLanguage[s2] = 
                                                Math.Max(0, m_oCorrectSecondLanguage[s2] - 3);


                                        if (!m_oFirstToSecond[s1].ContainsKey(s2))
                                            m_oFirstToSecond[s1][s2] = false;
                                        if (!m_oSecondToFirst[s2].ContainsKey(s1))
                                            m_oSecondToFirst[s2][s1] = false;
                                    }

                                bSave = true;
                                bRepeat = false;
                                break;

                            default:
                                bRepeat = false;
                                break;
                        }
                    }
                }
            }

            if (bSave)
            {
                if (SaveTrainingProgress())
                    SaveVokabulary();
            }

            EnableDisableButtons();
        }

        //===================================================================================================
        /// <summary>
        /// This is executed, when user decides to exercise from second to first
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnExerciseSecondToFirstClick(
            object oSender, 
            EventArgs oArgs
            )
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train one of the words randomly. Words with errors get higher weight
                int nRnd2 = m_oRnd2.Next();
                m_oRnd2 = new Random(nRnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                int nSelectedError = nRnd2 % (m_nTotalNumberOfErrorsSecondLanguage + m_oTtrainingResultsSecondLanguage.Count);

                m_bSkipLast = m_oTtrainingResultsSecondLanguage.Count > 10;

                int nWordIndex = -1;
                using (SortedDictionary<string,string>.ValueCollection.Enumerator values = 
                    m_oTtrainingResultsSecondLanguage.Values.GetEnumerator())
                {
                    while (nSelectedError >= 0 && values.MoveNext())
                    {
                        nWordIndex += 1;
                        if (values.Current.Contains("0"))
                        {
                            nSelectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
                        }
                        else
                            nSelectedError -= 1;
                    }

                    bRepeat = TrainSecondToFirstLanguage(nWordIndex);
                };
            }
            SaveTrainingProgress();
        }


        //===================================================================================================
        /// <summary>
        /// This is called when user wants to train from second language to first intensively
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnIntensiveSecondToFirstClick(
            object oSender, 
            EventArgs oArgs
            )
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // decide, if we will train one word randomly, or one that needs additional training
                int nRnd = m_oRnd.Next();
                m_oRnd = new Random(nRnd + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);

                if ((m_nTotalNumberOfErrorsSecondLanguage>0) && (nRnd % 100 < 50))
                {
                    // there we train one of the words that need training
                    int nRnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(nRnd + nRnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int nSelectedError = nRnd2 % m_nTotalNumberOfErrorsSecondLanguage;

                    m_bSkipLast = m_oTtrainingResultsSecondLanguage.Count > 20;

                    int nWordIndex = -1;
                    using (SortedDictionary<string, string>.ValueCollection.Enumerator values = 
                        m_oTtrainingResultsSecondLanguage.Values.GetEnumerator())
                    {
                        while (nSelectedError >= 0 && values.MoveNext())
                        {
                            nWordIndex += 1;
                            if (values.Current.Contains("0"))
                            {
                                nSelectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
                            }
                        }

                        bRepeat = TrainSecondToFirstLanguage(nWordIndex);
                    }
                }
                else
                {
                    // there we train one of the words
                    int nRnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(nRnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    // with few words as before, based on the mean
                    if (m_oCorrectSecondLanguage.Count < 300)
                    {
                        // calculate mean of correct answers
                        long lTotal = 0;
                        foreach (int i in m_oCorrectSecondLanguage.Values)
                            lTotal += i;

                        int nMean = (int)((lTotal + m_oCorrectSecondLanguage.Count / 2) / m_oCorrectSecondLanguage.Count);

                        // now calculate the sum of weights of all words
                        int nTotalWeights = 0;
                        foreach (int i in m_oCorrectSecondLanguage.Values)
                        {
                            int nWeight = (i >= nMean + 3) ? 0 : (nMean + 3 - i) * (nMean + 3 - i);
                            nTotalWeights += nWeight;
                        };

                        int nSelectedWeight = nRnd2 % nTotalWeights;

                        int nWordIndex = -1;
                        using (SortedDictionary<string, int>.ValueCollection.Enumerator values =
                            m_oCorrectSecondLanguage.Values.GetEnumerator())
                        {
                            while (nSelectedWeight >= 0 && values.MoveNext())
                            {
                                nWordIndex += 1;

                                int nWeight = (values.Current >= nMean + 3) ? 0 :
                                    (nMean + 3 - values.Current) * (nMean + 3 - values.Current);

                                nSelectedWeight -= nWeight;
                            }

                            m_bSkipLast = m_oTtrainingResultsSecondLanguage.Count > 10;

                            bRepeat = TrainSecondToFirstLanguage(nWordIndex);
                        }
                    }
                    else
                    {
                        // with many words - based on the equalizing power
                        // the formula equalizes the probability of 50 half as much trained words
                        // with the equally trained rest
                        double dblPower = Math.Log(50.0 / (m_oCorrectSecondLanguage.Values.Count-50), 2.0);

                        // now calculate the sum of weights of all words
                        double dblTotalWeights = 0;
                        foreach (int i in m_oCorrectSecondLanguage.Values)
                        {
                            double dblWeight = Pow(i+1, dblPower);
                            dblTotalWeights += dblWeight;
                        };


                        double dblSelectedWeight = nRnd2 * dblTotalWeights / int.MaxValue;

                        int nWordIndex = -1;
                        using (SortedDictionary<string, int>.ValueCollection.Enumerator values =
                            m_oCorrectSecondLanguage.Values.GetEnumerator())
                        {
                            while (dblSelectedWeight >= 0 && values.MoveNext())
                            {
                                nWordIndex += 1;

                                double dblWeight = Pow(values.Current+1, dblPower);

                                dblSelectedWeight -= dblWeight;
                            }

                            m_bSkipLast = m_oTtrainingResultsSecondLanguage.Count > 10;

                            bRepeat = TrainSecondToFirstLanguage(nWordIndex);
                        }
                    }

                    /*
                    int wordIndex = rnd2 % _trainingSecondLanguage.Count;

                    bRepeat = TrainSecondLanguage(wordIndex);
                     */
                }
            }
            SaveTrainingProgress();
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user wants to do most intensive training from second to first language
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnMostIntensiveSecondToFirstClick(
            object oSender, 
            EventArgs oArgs
            )
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train only the words with errors, and we start with those that had errors recently
                int nBestIndex = -1;
                int nBestCount = 0;
                int nBestTime = 16;
                int nWordIndex = -1;
                foreach(string s in m_oTtrainingResultsSecondLanguage.Values)
                {
                    ++nWordIndex;
                    int nTime = s.IndexOf('0');
                    if (nTime >= 0)
                    {
                        if (nBestTime > nTime)
                        {
                            nBestTime = nTime;
                            nBestCount = 1;
                        }
                        else
                            if (nBestTime == nTime)
                                ++nBestCount;
                    }
                }


                if (nBestCount > 0)
                {
                    int nRnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(nRnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int nSelectedBest = nRnd2 % nBestCount;

                    nWordIndex = -1;
                    foreach (string s in m_oTtrainingResultsSecondLanguage.Values)
                    {
                        ++nWordIndex;
                        int nTime = s.IndexOf('0');
                        if (nTime >= 0)
                        {
                            if (nBestTime == nTime)
                            {
                                if (--nSelectedBest < 0)
                                {
                                    nBestIndex = nWordIndex;
                                    break;
                                }
                            }
                        }
                    }

                    if (nBestIndex >= 0)
                    {
                        m_bSkipLast = false;
                        bRepeat = TrainSecondToFirstLanguage(nBestIndex);
                    }
                }
            }
            SaveTrainingProgress();
        }


        //===================================================================================================
        /// <summary>
        /// Trains from second to first language
        /// </summary>
        /// <param name="nIndex">Index of the word to train</param>
        /// <returns>true iff the training shall continue</returns>
        //===================================================================================================
        private bool TrainSecondToFirstLanguage(
            int nIndex
            )
        {
            bool bRepeat = false;
            bool bVerify = false;
            foreach(KeyValuePair<string,string> oPair in m_oTtrainingResultsSecondLanguage)
            {
                if (0 == nIndex-- )
                {
                    if (m_bSkipLast)
                    {
                        foreach (string s in m_oRecentlyTrainedWords)
                        {
                            // if we trained this word recently, then try to skip it
                            if (s.Equals(oPair.Key))
                                if (m_oRnd2.Next(100) > 0)
                                    return true;
                        };

                        // add the word to the list of recently trained words, rotate the list
                        if (m_oRecentlyTrainedWords.Count > 10)
                            m_oRecentlyTrainedWords.RemoveLast();
                        m_oRecentlyTrainedWords.AddFirst(oPair.Key);

                    }
                    else
                    {
                        foreach (string s in m_oRecentlyTrainedWords)
                        {
                            // if we trained this word recently, then try to skip it, 
                            // but not with that high probability
                            if (s.Equals(oPair.Key))
                                if (m_oRnd2.Next(3) > 0)
                                    return true;
                        }

                        // add the word to the list of recently trained words, rotate the list
                        while (m_oRecentlyTrainedWords.Count > 3)
                            m_oRecentlyTrainedWords.RemoveLast();
                        m_oRecentlyTrainedWords.AddFirst(oPair.Key);

                    }


                    using (WordTest oTestDlg = new WordTest())
                    {
                        oTestDlg.m_lblShownText.Text = m_strSecondLanguage + ": " + oPair.Key;
                        oTestDlg.m_lblAskedTranslation.Text = m_strFirstLanguage + ":";
                        oTestDlg.m_lblShownText.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        oTestDlg.m_lblAskedTranslation.RightToLeft = m_bFirstLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        oTestDlg.m_tbxAskedTranslation.RightToLeft = m_bFirstLanguageRtl ? RightToLeft.Yes : RightToLeft.No;

                        oTestDlg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VokabelTrainer_MouseMove);

                        if (m_cbxReader.SelectedIndex == 0 || m_cbxReader.SelectedIndex == 3)
                            Speaker.Say(m_strSecondLanguage,oPair.Key,true,m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);

                        switch (oTestDlg.ShowDialog())
                        {
                            case DialogResult.Retry:
                                bRepeat = true;
                                bVerify = true;
                                break;
                            case DialogResult.OK:
                                bRepeat = false;
                                bVerify = true;
                                break;
                            default:
                                bRepeat = false;
                                bVerify = false;
                                break;
                        }

                        if (bVerify)
                        {

                            char[] separators = { ',' };

                            Dictionary<string, bool> typedIn = new Dictionary<string, bool>();
                            foreach (string s in oTestDlg.m_tbxAskedTranslation.Text.Trim()
                                .Replace("?", "?,").Replace("!", "!,").Replace(";", ",").Replace("،", ",")
                                .Replace("、", ",").Replace("，", ",")
                                .Replace(",,", ",").Replace(",,", ",").Split(separators, 
                                    StringSplitOptions.RemoveEmptyEntries))
                            {
                                typedIn[s.Trim()] = false;
                            }

                            Dictionary<string, bool> missing = new Dictionary<string, bool>();
                            Dictionary<string, bool> wrong = new Dictionary<string, bool>();

                            foreach (string s in m_oSecondToFirst[oPair.Key].Keys)
                            {
                                if (!typedIn.ContainsKey(s))
                                    missing[s] = false;
                            }

                            foreach (string s in typedIn.Keys)
                            {
                                if (!m_oSecondToFirst[oPair.Key].ContainsKey(s))
                                {
                                    wrong[s] = false;
                                }
                            }

                            if (missing.Count > 0 || wrong.Count > 0)
                            {
                                string strErrorMessage = "";
                                if (missing.Count > 1)
                                {
                                    string strTextToSpeak = "";

                                    strErrorMessage = Properties.Resources.FollowingMeaningWereMissing;
                                    //errorMessage = "Folgende Bedeutungen haben gefehlt: ";
                                    bool bFirst = true;
                                    foreach (string s in missing.Keys)
                                    {
                                        if (!bFirst)
                                        {
                                            strTextToSpeak = strTextToSpeak + ", ";
                                            strErrorMessage = strErrorMessage + ", ";
                                        }
                                        else
                                            bFirst = false;

                                        strErrorMessage = strErrorMessage + s;
                                        strTextToSpeak = strTextToSpeak + s;
                                    }
                                    strErrorMessage = strErrorMessage + ". ";

                                    Speaker.Say(m_strFirstLanguage, strTextToSpeak, true, 
                                        m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);

                                }
                                else
                                    if (missing.Count == 1)
                                    {
                                        strErrorMessage = Properties.Resources.FollowingMeaningWasMissing;
                                        //errorMessage = "Folgende Bedeutung hat gefehlt: ";
                                        foreach (string s in missing.Keys)
                                        {
                                            strErrorMessage = strErrorMessage + s + ". ";

                                            Speaker.Say(m_strFirstLanguage, s, true, 
                                                m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);

                                        }
                                    }

                                if (wrong.Count > 0 && !string.IsNullOrEmpty(strErrorMessage))
                                    strErrorMessage = strErrorMessage + "\r\n";

                                if (wrong.Count > 1)
                                {

                                    strErrorMessage = strErrorMessage + Properties.Resources.FollowingMeaningsWereWrong;
                                    bool bFirst = true;
                                    foreach (string s in wrong.Keys)
                                    {
                                        if (!bFirst)
                                            strErrorMessage = strErrorMessage + ", ";
                                        else
                                            bFirst = false;

                                        strErrorMessage = strErrorMessage + s;

                                        if (m_oTrainingResultsFirstLanguage.ContainsKey(s))
                                            RememberResultFirstLanguage(s, false);
                                    }
                                    strErrorMessage = strErrorMessage + ". ";
                                }
                                else
                                    if (wrong.Count == 1)
                                    {
                                        strErrorMessage = strErrorMessage + Properties.Resources.FollowingMeaningWasWrong;
                                        foreach (string s in wrong.Keys)
                                        {
                                            strErrorMessage = strErrorMessage + s + ". ";
                                            if (m_oTrainingResultsFirstLanguage.ContainsKey(s))
                                                RememberResultFirstLanguage(s, false);
                                        }
                                    }

                                if (missing.Count > 0)
                                    NewMessageBox.Show(this, strErrorMessage, Properties.Resources.Mistake, null);
                                else
                                    MessageBox.Show(strErrorMessage, Properties.Resources.Mistake,
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                                RememberResultSecondLanguage(oPair.Key, false);
                            }
                            else
                            {
                                if (m_cbxReader.SelectedIndex == 1 || m_cbxReader.SelectedIndex == 2)
                                    Speaker.Say(m_strFirstLanguage, oTestDlg.m_tbxAskedTranslation.Text.Trim(), 
                                        true, m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);
                                RememberResultSecondLanguage(oPair.Key, true);
                            }
                        }
                    }

                    return bRepeat;
                }
            }
            return bRepeat;
        }

        //===================================================================================================
        /// <summary>
        /// Remembers the result of the test of first to second language
        /// </summary>
        /// <param name="strWord">Tested word in first language</param>
        /// <param name="bCorrect">Indicates, if the result was correct</param>
        //===================================================================================================
        void RememberResultFirstLanguage(
            string strWord, 
            bool bCorrect
            )
        {
            string strPrevResults = m_oTrainingResultsFirstLanguage[strWord];
            string strNewResults = (bCorrect ? "1" : "0") + 
                strPrevResults.Substring(0, strPrevResults.Length<5?strPrevResults.Length:5);
            m_oTrainingResultsFirstLanguage[strWord] = strNewResults;
            m_nTotalNumberOfErrorsFirstLanguage += strNewResults.Length - strNewResults.Replace("0", "").Length - 
                (strPrevResults.Length - strPrevResults.Replace("0", "").Length);

            // if the result was correct and we didn't repeat it because of earlier 
            // mistakes, then increment the number of correct answers
            if (bCorrect && strPrevResults.IndexOf('0')<0)
                m_oCorrectAnswersFirstLanguage[strWord]++;

            EnableDisableButtons();
        }


        //===================================================================================================
        /// <summary>
        /// Remembers the result of the test of second to first language
        /// </summary>
        /// <param name="strWord">Tested word in second language</param>
        /// <param name="bCorrect">Indicates, if the result was correct</param>
        //===================================================================================================
        void RememberResultSecondLanguage(
            string strWord, 
            bool bCorrect
            )
        {
            string strPrevResults = m_oTtrainingResultsSecondLanguage[strWord];
            string strNewResults = (bCorrect ? "1" : "0") + 
                strPrevResults.Substring(0, strPrevResults.Length < 5 ? strPrevResults.Length : 5);
            m_oTtrainingResultsSecondLanguage[strWord] = strNewResults;
            m_nTotalNumberOfErrorsSecondLanguage += strNewResults.Length - strNewResults.Replace("0", "").Length - 
                (strPrevResults.Length - strPrevResults.Replace("0", "").Length);

            // if the result was correct and we didn't repeat it because of 
            // earlier mistakes, then increment the number of correct answers
            if (bCorrect && strPrevResults.IndexOf('0') < 0)
                m_oCorrectSecondLanguage[strWord]++;

            EnableDisableButtons();
        }


        //===================================================================================================
        /// <summary>
        /// Saves vocabulary file
        /// </summary>
        //===================================================================================================
        void SaveVokabulary()
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(m_strCurrentPath);

            try
            {
                if (fi.Exists)
                {
                    System.IO.FileInfo fi4 = new System.IO.FileInfo(m_strCurrentPath + ".bak");
                    if (fi4.Exists)
                        fi4.Delete();

                    fi.MoveTo(fi.FullName + ".bak");
                }

                using (System.IO.StreamWriter w = new System.IO.StreamWriter(
                    m_strCurrentPath, false, System.Text.Encoding.UTF8))
                {
                    string[] spaces = {
                        "                         ",
                        "                        ",
                        "                       ",
                        "                      ",
                        "                     ",
                        "                    ",
                        "                   ",
                        "                  ",
                        "                 ",
                        "                ",
                        "               ",
                        "              ",
                        "             ",
                        "            ",
                        "           ",
                        "          ",
                        "         ",
                        "        ",
                        "       ",
                        "      ",
                        "     ",
                        "    ",
                        "   ",
                        "  ",
                        " "};
                    w.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                    w.WriteLine("<vokabeln>");
                    w.WriteLine();
                    w.WriteLine("  <!-- Allgemeiner Teil: Die Namen der Sprachen im Vokabelheft und deren links- oder rechtsläufigkeit -->");
                    w.WriteLine("  <erste-sprache-name>{0}</erste-sprache-name>", m_strFirstLanguage);
                    w.WriteLine("  <zweite-sprache-name>{0}</zweite-sprache-name>", m_strSecondLanguage);
                    w.WriteLine("  <erste-sprache-rtl>{0}</erste-sprache-rtl>", m_bFirstLanguageRtl ? "ja" : "nein");
                    w.WriteLine("  <zweite-sprache-rtl>{0}</zweite-sprache-rtl>", m_bSecondLanguageRtl ? "ja" : "nein");;

                    w.WriteLine();
                    w.WriteLine("  <!-- Allgemeiner Teil: Lizenz für das Vokabelheft -->");
                    w.WriteLine("  <lizenz><modifikationen>{0}</modifikationen>", 
                        m_bIsModifiableFlagForXml ? "Unter Lizenzbedingungen" : "Keine neuen Wörter und keine Lizenzänderungen");
                    w.WriteLine("  <text xml:space=\"preserve\">{0}</text></lizenz>", m_strLicense.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;"));
                    w.WriteLine();
                    w.WriteLine("  <!-- Die Verbindung zwischen den Vokabeln der zwei Sprachen ({0}-{1}) -->", m_strFirstLanguage, m_strSecondLanguage);
                    /*
                    foreach (KeyValuePair<string, SortedDictionary<string, bool>> second in _secondToFirst)
                        foreach (string first in second.Value.Keys)
                            if (first.Length >= spaces.Length)
                                w.WriteLine("  <vokabel-paar><erste-sprache>{0}</erste-sprache><zweite-sprache>{1}</zweite-sprache></vokabel-paar>", first.Trim(), second.Key.Trim());
                            else
                                w.WriteLine("  <vokabel-paar><erste-sprache>{0}</erste-sprache>{2}<zweite-sprache>{1}</zweite-sprache></vokabel-paar>", first.Trim(), second.Key.Trim(), spaces[first.Trim().Length]);
                     */
                    foreach (KeyValuePair<string, SortedDictionary<string, bool>> oFirst in m_oFirstToSecond)
                        foreach (string strSecond in oFirst.Value.Keys)
                            if (strSecond.Length >= spaces.Length)
                                w.WriteLine("  <vokabel-paar><erste-sprache>{0}</erste-sprache><zweite-sprache>{1}</zweite-sprache></vokabel-paar>", oFirst.Key.Trim(), strSecond.Trim());
                            else
                                w.WriteLine("  <vokabel-paar><erste-sprache>{0}</erste-sprache>{2}<zweite-sprache>{1}</zweite-sprache></vokabel-paar>", oFirst.Key.Trim(), strSecond.Trim(), spaces[oFirst.Key.Trim().Length]);

                    w.WriteLine();
                    w.WriteLine("</vokabeln>");
                    w.Close();
                }
            }
            catch (Exception oEx)
            {
                try
                {
                    System.IO.FileInfo fi3 = new System.IO.FileInfo(m_strCurrentPath);
                    if (fi3.Exists)
                        fi3.Delete();
                }
                catch (Exception)
                {
                };

                try
                {
                    System.IO.FileInfo fi2 = new System.IO.FileInfo(m_strCurrentPath + ".bak");
                    if (fi2.Exists)
                        fi.MoveTo(m_strCurrentPath);
                }
                catch (Exception)
                {
                };

                NewMessageBox.Show(this, oEx.Message, Properties.Resources.Error, 
                    string.Format(Properties.Resources.ErrorWhileSavingVocabularyFile, oEx.Message));
            }
        }

        //===================================================================================================
        /// <summary>
        /// Saves training progress
        /// </summary>
        /// <returns>true iff success</returns>
        //===================================================================================================
        bool SaveTrainingProgress()
        {
            string strCurrentPath = m_strCurrentPath.Replace(".Vokabeln.xml",".Training.xml")
                .Replace(".Vocabulary.xml",".Training.xml");
            System.IO.FileInfo fi = new System.IO.FileInfo(strCurrentPath);

            try
            {

                if (fi.Exists)
                {

                    System.IO.FileInfo fi4 = new System.IO.FileInfo(strCurrentPath + ".bak");
                    if (fi4.Exists)
                        fi4.Delete();


                    fi.MoveTo(fi.FullName + ".bak");
                }

                using (System.IO.StreamWriter w = new System.IO.StreamWriter(
                    strCurrentPath, false, System.Text.Encoding.UTF8))
                {
                    string[] spaces = {
                        "                         ",
                        "                        ",
                        "                       ",
                        "                      ",
                        "                     ",
                        "                    ",
                        "                   ",
                        "                  ",
                        "                 ",
                        "                ",
                        "               ",
                        "              ",
                        "             ",
                        "            ",
                        "           ",
                        "          ",
                        "         ",
                        "        ",
                        "       ",
                        "      ",
                        "     ",
                        "    ",
                        "   ",
                        "  ",
                        " "};
                    w.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                    w.WriteLine("<training>");
                    w.WriteLine();
                    w.WriteLine("  <!-- Die Vokabeln der ersten Sprache ({0}), und deren Trainingsfortschritt 1=richtig 0=falsch -->", m_strFirstLanguage);
                    w.WriteLine("  <!-- Das letzte Training ist am Anfang der Zahl, die jeweils früheren jeweils danach -->");
                    foreach (KeyValuePair<string, string> training in m_oTrainingResultsFirstLanguage)
                        if (training.Key.Length >= spaces.Length)
                            w.WriteLine("  <erste-sprache><vokabel>{0}</vokabel>"+
                                "<training-vorgeschichte>{1}</training-vorgeschichte>"+
                                "<richtige-antworten>{2}</richtige-antworten></erste-sprache>", 
                                training.Key.Trim(), training.Value, m_oCorrectAnswersFirstLanguage[training.Key]);
                        else
                            w.WriteLine("  <erste-sprache><vokabel>{0}</vokabel>"+
                                "{3}<training-vorgeschichte>{1}</training-vorgeschichte>"+
                                "<richtige-antworten>{2}</richtige-antworten></erste-sprache>", 
                                training.Key.Trim(), training.Value, m_oCorrectAnswersFirstLanguage[training.Key], 
                                spaces[training.Key.Trim().Length]);
                    w.WriteLine();
                    w.WriteLine("  <!-- Die Vokabeln der zweiten Sprache ({0}), und deren Trainingsfortschritt 1=richtig 0=falsch -->",
                        m_strSecondLanguage);
                    w.WriteLine("  <!-- Das letzte Training ist am Anfang der Zahl, die jeweils früheren jeweils danach -->");
                    foreach (KeyValuePair<string, string> training in m_oTtrainingResultsSecondLanguage)
                        if (training.Key.Length >= spaces.Length)
                            w.WriteLine("  <zweite-sprache><vokabel>{0}</vokabel>"+
                                "<training-vorgeschichte>{1}</training-vorgeschichte>"+
                                "<richtige-antworten>{2}</richtige-antworten></zweite-sprache>", 
                                training.Key.Trim(), training.Value, m_oCorrectSecondLanguage[training.Key]);
                        else
                            w.WriteLine("  <zweite-sprache><vokabel>{0}</vokabel>"+
                                "{3}<training-vorgeschichte>{1}</training-vorgeschichte>"+
                                "<richtige-antworten>{2}</richtige-antworten></zweite-sprache>", 
                                training.Key.Trim(), training.Value, m_oCorrectSecondLanguage[training.Key], 
                                spaces[training.Key.Trim().Length]);
                    w.WriteLine();
                    w.WriteLine();
                    w.WriteLine("  <!-- Graphen des Trainingsfortschritts -->");
                    if (m_oTotalGraphData != null)
                    {
                        foreach (DateTime dtmGraphPoint in m_oTotalGraphData.Keys)
                        {
                            w.WriteLine("  <zustand><datum>{0}</datum><woerter>{1}</woerter><richtige-antworten>{2}</richtige-antworten><fehler>{3}</fehler></zustand>",
                                dtmGraphPoint.ToString("yyyy-MM-dd"),
                                m_oWordsGraphData[dtmGraphPoint],
                                m_oTotalGraphData[dtmGraphPoint] - m_oWordsGraphData[dtmGraphPoint],
                                m_oWordsGraphData[dtmGraphPoint] - m_oLearnedWordsGraphData[dtmGraphPoint]);
                        }
                    }

                    w.WriteLine("</training>");
                    w.Close();
                }
            }
            catch (Exception oEx)
            {
                try
                {
                    System.IO.FileInfo fi3 = new System.IO.FileInfo(strCurrentPath);
                    if (fi3.Exists)
                        fi3.Delete();
                }
                catch (Exception)
                {
                };

                try
                {
                    System.IO.FileInfo fi2 = new System.IO.FileInfo(strCurrentPath + ".bak");
                    if (fi2.Exists)
                        fi.MoveTo(strCurrentPath);
                }
                catch (Exception)
                {
                };

                NewMessageBox.Show(this, oEx.Message, Properties.Resources.Error, 
                    string.Format(Properties.Resources.ErrorWhileSavingTrainingFile,oEx.Message));

                return false;
            }
            return true;
        }


        //===================================================================================================
        /// <summary>
        /// Trains first to second language
        /// </summary>
        /// <param name="nIndex">Index of the word in first language</param>
        /// <returns>true iff the training shall continue</returns>
        //===================================================================================================
        private bool TrainFirstLanguage(
            int nIndex
            )
        {
            bool bRepeat = false;
            bool bVerify = false;
            foreach (KeyValuePair<string, string> pair in m_oTrainingResultsFirstLanguage)
            {
                if (0 == nIndex--)
                {
                    if (m_bSkipLast)
                    {
                        foreach (string s in m_oRecentlyTrainedWords)
                        {
                            // if we trained this word recently, then try to skip it
                            if (s.Equals(pair.Key))
                                if (m_oRnd2.Next(100) > 0)
                                    return true;
                        };

                        // add the word to the list of recently trained words, rotate the list
                        if (m_oRecentlyTrainedWords.Count > 10)
                            m_oRecentlyTrainedWords.RemoveLast();
                        m_oRecentlyTrainedWords.AddFirst(pair.Key);
                    }
                    else
                    {
                        foreach (string s in m_oRecentlyTrainedWords)
                        {
                            // if we trained this word recently, then try to skip it, but not with that high probability
                            if (s.Equals(pair.Key))
                                if (m_oRnd2.Next(3) > 0)
                                    return true;
                        }

                        // add the word to the list of recently trained words, rotate the list
                        while (m_oRecentlyTrainedWords.Count > 3)
                            m_oRecentlyTrainedWords.RemoveLast();
                        m_oRecentlyTrainedWords.AddFirst(pair.Key);

                    }


                    using (WordTest test = new WordTest())
                    {
                        test.m_lblShownText.Text = m_strFirstLanguage + ": " + pair.Key;
                        test.m_lblAskedTranslation.Text = m_strSecondLanguage + ":";
                        test.m_lblShownText.RightToLeft = m_bFirstLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        test.m_lblAskedTranslation.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        test.m_tbxAskedTranslation.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        test.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VokabelTrainer_MouseMove);

                        if (m_cbxReader.SelectedIndex == 0 || m_cbxReader.SelectedIndex == 2)
                            Speaker.Say(m_strFirstLanguage, pair.Key, true, m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);


                        switch (test.ShowDialog())
                        {
                            case DialogResult.Retry:
                                bRepeat = true;
                                bVerify = true;
                                break;
                            case DialogResult.OK:
                                bRepeat = false;
                                bVerify = true;
                                break;
                            default:
                                bRepeat = false;
                                bVerify = false;
                                break;
                        }

                        if (bVerify)
                        {

                            char[] separators = { ',', ';' };

                            Dictionary<string, bool> typedIn = new Dictionary<string, bool>();
                            foreach (string s in test.m_tbxAskedTranslation.Text.Trim()
                                .Replace("?", "?,").Replace("!", "!,").Replace(";", ",").Replace("،", ",")
                                .Replace("、", ",").Replace("，", ",")
                                .Replace(",,", ",").Replace(",,", ",")
                                .Split(separators, StringSplitOptions.RemoveEmptyEntries))
                            {
                                typedIn[s.Trim()] = false;
                            }

                            Dictionary<string, bool> missing = new Dictionary<string, bool>();
                            Dictionary<string, bool> wrong = new Dictionary<string, bool>();

                            foreach (string s in m_oFirstToSecond[pair.Key].Keys)
                            {
                                if (!typedIn.ContainsKey(s))
                                    missing[s] = false;
                            }

                            foreach (string s in typedIn.Keys)
                            {
                                if (!m_oFirstToSecond[pair.Key].ContainsKey(s))
                                {
                                    wrong[s] = false;
                                }
                            }

                            if (missing.Count > 0 || wrong.Count > 0)
                            {
                                string strErrorMessage = "";
                                if (missing.Count > 1)
                                {
                                    string strTextToSpeak = "";
                                    strErrorMessage = Properties.Resources.FollowingMeaningsWereMissing;
                                    bool bFirst = true;
                                    foreach (string s in missing.Keys)
                                    {
                                        if (!bFirst)
                                        {
                                            strTextToSpeak = strTextToSpeak + ", ";
                                            strErrorMessage = strErrorMessage + ", ";
                                        }
                                        else
                                            bFirst = false;

                                        strErrorMessage = strErrorMessage + s;
                                        strTextToSpeak = strTextToSpeak + s;
                                    }
                                    strErrorMessage = strErrorMessage + ". ";

                                    Speaker.Say(m_strSecondLanguage, strTextToSpeak, true, 
                                        m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);

                                }
                                else
                                    if (missing.Count == 1)
                                    {
                                        strErrorMessage = Properties.Resources.FollowingMeaningWasMissing;
                                        foreach (string s in missing.Keys)
                                        {
                                            strErrorMessage = strErrorMessage + s + ". ";


                                            Speaker.Say(m_strSecondLanguage, s, true, 
                                                m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);


                                        }
                                    }

                                if (wrong.Count > 0 && !string.IsNullOrEmpty(strErrorMessage))
                                    strErrorMessage = strErrorMessage + "\r\n";

                                if (wrong.Count > 1)
                                {

                                    strErrorMessage = strErrorMessage + Properties.Resources.FollowingMeaningsWereWrong;
                                    bool bFirst = true;
                                    foreach (string s in wrong.Keys)
                                    {
                                        if (!bFirst)
                                            strErrorMessage = strErrorMessage + ", ";
                                        else
                                            bFirst = false;

                                        strErrorMessage = strErrorMessage + s;

                                        if (m_oTtrainingResultsSecondLanguage.ContainsKey(s))
                                            RememberResultSecondLanguage(s, false);
                                    }
                                    strErrorMessage = strErrorMessage + ". ";
                                }
                                else
                                    if (wrong.Count == 1)
                                    {
                                        strErrorMessage = strErrorMessage + Properties.Resources.FollowingMeaningWasWrong;
                                        foreach (string s in wrong.Keys)
                                        {
                                            strErrorMessage = strErrorMessage + s + ". ";
                                            if (m_oTtrainingResultsSecondLanguage.ContainsKey(s))
                                                RememberResultSecondLanguage(s, false);
                                        }
                                    }


                                if (missing.Count > 0)
                                    NewMessageBox.Show(this, strErrorMessage, Properties.Resources.Mistake, null);
                                else
                                    MessageBox.Show(strErrorMessage, Properties.Resources.Mistake, 
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                                RememberResultFirstLanguage(pair.Key, false);
                            }
                            else
                            {
                                if (m_cbxReader.SelectedIndex == 1 || m_cbxReader.SelectedIndex == 3)
                                    Speaker.Say(m_strSecondLanguage, test.m_tbxAskedTranslation.Text.Trim(), 
                                        true, m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);
                                RememberResultFirstLanguage(pair.Key, true);
                            }
                        }
                    }

                    return bRepeat;
                }
            }
            return bRepeat;


        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user wants to make an exercise from first to second
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnExerciseFirstToSecondClick(
            object oSender, 
            EventArgs oArgs
            )
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train one of the words randomly. The words with errors get higher weight.
                int nRnd2 = m_oRnd2.Next();
                m_oRnd2 = new Random(nRnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                int nSelectedError = nRnd2 % (m_nTotalNumberOfErrorsFirstLanguage + m_oTrainingResultsFirstLanguage.Count);

                m_bSkipLast = m_oTrainingResultsFirstLanguage.Count > 10;

                int nWordIndex = -1;
                using (SortedDictionary<string, string>.ValueCollection.Enumerator values = 
                    m_oTrainingResultsFirstLanguage.Values.GetEnumerator())
                {
                    while (nSelectedError >= 0 && values.MoveNext())
                    {
                        nWordIndex += 1;
                        if (values.Current.Contains("0"))
                        {
                            nSelectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
                        }
                        else
                            nSelectedError -= 1;
                    }

                    bRepeat = TrainFirstLanguage(nWordIndex);
                };
            }
            SaveTrainingProgress();
        }



        //===================================================================================================
        /// <summary>
        /// This is executed when the user wants to do an intensive training from first to second language
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnIntensiveFirstToSecondClick(
            object oSender, 
            EventArgs oArgs
            )
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // decide, if we will train one word randomly, or one that needs additional training
                int nRnd = m_oRnd.Next();
                m_oRnd = new Random(nRnd + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);

                if ( (m_nTotalNumberOfErrorsFirstLanguage>0) && (nRnd % 100 < 50) )
                {
                    // there we train one of the words that need training
                    int nRnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(nRnd + nRnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int nSelectedError = nRnd2 % m_nTotalNumberOfErrorsFirstLanguage;

                    m_bSkipLast = m_oTrainingResultsFirstLanguage.Count > 20;

                    int nWordIndex = -1;
                    using (SortedDictionary<string, string>.ValueCollection.Enumerator values = 
                        m_oTrainingResultsFirstLanguage.Values.GetEnumerator())
                    {
                        while (nSelectedError >= 0 && values.MoveNext())
                        {
                            nWordIndex += 1;
                            if (values.Current.Contains("0"))
                            {
                                nSelectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
                            }
                        }

                        bRepeat = TrainFirstLanguage(nWordIndex);
                    }
                }
                else
                {
                    // there we train one of the words
                    int nRnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(nRnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    // if not as many words then do as before
                    if (m_oCorrectAnswersFirstLanguage.Values.Count<300)
                    {
                        // calculate mean of correct answers
                        long lTotal = 0;
                        foreach (int i in m_oCorrectAnswersFirstLanguage.Values)
                            lTotal += i;

                        int nMean = (int)( (lTotal + m_oCorrectAnswersFirstLanguage.Count/2) / m_oCorrectAnswersFirstLanguage.Count);

                        // now calculate the sum of weights of all words
                        int nTotalWeights = 0;
                        foreach (int i in m_oCorrectAnswersFirstLanguage.Values)
                        {
                            int nWeight = (i > nMean + 3) ? 0 : (nMean + 3 - i) * (nMean + 3 - i);

                            nTotalWeights += nWeight;
                        };

                        int nSelectedWeight = nRnd2 % nTotalWeights;

                        int nWordIndex = -1;
                        using (SortedDictionary<string, int>.ValueCollection.Enumerator values = 
                            m_oCorrectAnswersFirstLanguage.Values.GetEnumerator())
                        {
                            while (nSelectedWeight >= 0 && values.MoveNext())
                            {
                                nWordIndex += 1;

                                int nWeight = (values.Current > nMean + 3) ? 0 : 
                                    (nMean + 3 - values.Current) * (nMean + 3 - values.Current);

                                nSelectedWeight -= nWeight;
                            }

                            m_bSkipLast = m_oTrainingResultsFirstLanguage.Count > 10;

                            bRepeat = TrainFirstLanguage(nWordIndex);
                        }
                    }
                    else
                    {
                        // with many words - based on the equalizing power
                        // the formula equalizes the probability of 50 half as much trained words
                        // with the equally trained rest
                        double dblPower = Math.Log(50.0 / (m_oCorrectAnswersFirstLanguage.Values.Count - 50), 2.0);

                        // now calculate the sum of weights of all words
                        double dblTotalWeights = 0;
                        foreach (int i in m_oCorrectAnswersFirstLanguage.Values)
                        {
                            double dblWeight = Pow(i + 1, dblPower);
                            dblTotalWeights += dblWeight;
                        };


                        double dblSelectedWeight = nRnd2 * dblTotalWeights / int.MaxValue;

                        int nWordIndex = -1;
                        using (SortedDictionary<string, int>.ValueCollection.Enumerator values =
                            m_oCorrectAnswersFirstLanguage.Values.GetEnumerator())
                        {
                            while (dblSelectedWeight >= 0 && values.MoveNext())
                            {
                                nWordIndex += 1;

                                double dblWeight = Pow(values.Current + 1, dblPower);

                                dblSelectedWeight -= dblWeight;
                            }

                            m_bSkipLast = m_oTrainingResultsFirstLanguage.Count > 10;

                            bRepeat = TrainFirstLanguage(nWordIndex);
                        }
                    }

                    /*
                    int wordIndex = rnd2 % _trainingFirstLanguage.Count;

                    bRepeat = TrainFirstLanguage(wordIndex);
                     */
                }
            }
            SaveTrainingProgress();
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user wants to perform most intensive training from first to second
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnMostIntensiveFirstToSecondClick(
            object oSender, 
            EventArgs oArgs
            )
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train only the words that with errors, 
                // and we start with those that had errors recently
                int nBestIndex = -1;
                int nBestTime = 16;
                int nWordIndex = -1;
                int nBestCount = 0;
                foreach (string s in m_oTrainingResultsFirstLanguage.Values)
                {
                    ++nWordIndex;
                    int nTime = s.IndexOf('0');
                    if (nTime >= 0)
                    {
                        if (nBestTime > nTime)
                        {
                            nBestTime = nTime;
                            nBestCount = 1;
                        }
                        else
                            if (nBestTime == nTime)
                                ++nBestCount;
                    }
                }

                if (nBestCount > 0)
                {
                    int nRnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(nRnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int nSelectedBest = nRnd2 % nBestCount;

                    nWordIndex = -1;
                    foreach (string s in m_oTrainingResultsFirstLanguage.Values)
                    {
                        ++nWordIndex;
                        int nTime = s.IndexOf('0');
                        if (nTime >= 0)
                        {
                            if (nBestTime == nTime)
                            {
                                if (--nSelectedBest < 0)
                                {
                                    nBestIndex = nWordIndex;
                                    break;
                                }
                            }
                        }
                    }

                    if (nBestIndex >= 0)
                    {
                        m_bSkipLast = false;

                        bRepeat = TrainFirstLanguage(nBestIndex);
                    }
                }
            }
            SaveTrainingProgress();
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user wants to see the licence
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_lblShowLicence_LinkClicked(object oSender, LinkLabelLinkClickedEventArgs oArgs)
        {
            System.Diagnostics.Process.Start("https://www.gnu.org/licenses/gpl-2.0.html");
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user wants to see information about the application
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_lblShowAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (AboutForm oAboutForm = new AboutForm())
            {
                oAboutForm.ShowDialog(this);
            }
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user wants to show the desktop keyboard
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_btnShowDesktopKeyboard_Click(object oSender, EventArgs oArgs)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        //===================================================================================================
        /// <summary>
        /// This is called when user moves mouse, in order to improve random numbers
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void VokabelTrainer_MouseMove(object oSender, MouseEventArgs oArgs)
        {
            // make randoms less deterministic, whenever possible
            if (m_oRnd != null)
                m_oRnd = new Random(m_oRnd.Next() + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond + (oArgs.X & 3) * 256);
            if (m_oRnd2 != null)
                m_oRnd2 = new Random(m_oRnd2.Next() + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear + 
                    (oArgs.Y & 3) * 256);
        }


        //===================================================================================================
        /// <summary>
        /// This is called when user wants to download eSpeak
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_lblDownloadESpeak_LinkClicked(object oSender, LinkLabelLinkClickedEventArgs oArgs)
        {
            System.Diagnostics.Process.Start("https://espeak.sourceforge.net/");
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user toggles the eSpeak Checkbox
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_chkUseESpeak_CheckedChanged(object oSender, EventArgs oArgs)
        {
            m_tbxESpeakPath.Enabled = m_chkUseESpeak.Checked;
            m_btnSearchESpeak.Enabled = m_chkUseESpeak.Checked;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks on the [...] button besides the eSpeak
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_btnSearchESpeak_Click(object oSender, EventArgs oArgs)
        {
            using (System.Windows.Forms.OpenFileDialog oDlg = new OpenFileDialog())
            {
                oDlg.Filter = "espeak|espeak.exe";
                oDlg.FileName = "espeak.exe";
                oDlg.CheckFileExists = true;
                if (oDlg.ShowDialog() == DialogResult.OK)
                {
                    m_tbxESpeakPath.Text = oDlg.FileName;
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when the form is loaded. It tests presence of eSpeak
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void VokabelTrainer_Load(object oSender, EventArgs oArgs)
        {
            m_lblDontLearnAquire.Visible = false;
            m_tbxESpeakPath.Text = "C:\\Program Files (x86)\\eSpeak\\command_line\\espeak.exe";
            m_btnSearchESpeak.Enabled = 
                m_tbxESpeakPath.Enabled = 
                    m_chkUseESpeak.Checked = System.IO.File.Exists(m_tbxESpeakPath.Text);

            string strWindowsVersion = Environment.OSVersion.Version.ToString();
            if (strWindowsVersion.StartsWith("10.") || strWindowsVersion.StartsWith("11.") ||
                strWindowsVersion.StartsWith("6.3") || strWindowsVersion.StartsWith("6.2") ||
                strWindowsVersion.StartsWith("6.1"))
                m_btnOsLanguageAndKeyboardSettings.Visible = true;
            else
                m_btnOsLanguageAndKeyboardSettings.Visible = false;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when the form is loaded. It tests, if some links can be provided for user
        /// </summary>
        //===================================================================================================
        private List<string> GetEasyLanguageAquireList()
        {
            List<string> oResult = GetEasyLanguageAquireList(m_strFirstLanguage);
            oResult.AddRange(GetEasyLanguageAquireList(m_strSecondLanguage));
            return oResult;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when the form is loaded. It tests, if some links can be provided for user
        /// </summary>
        /// <param name="strLanguage">Language to check</param>
        //===================================================================================================
        private List<string> GetEasyLanguageAquireList(string strLanguage)
        {

            List<string> oResult = new List<string>();
            string strLanguageFirst2 = strLanguage.Length > 2 ? strLanguage.Substring(0, 2) : strLanguage;
            string strLanguageFirst3 = strLanguage.Length > 3 ? strLanguage.Substring(0, 3) : strLanguage;
            // if current culture isn't german
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("de"))
            {
                // and one of the trained languages is german
                if (strLanguageFirst2.Equals("De", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Не", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst3.Equals("All", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst3.Equals("Ale", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst3.Equals("Jer", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst3.Equals("Ger", StringComparison.InvariantCultureIgnoreCase)
                    )
                {
                    // then add german resources for language aquisition without explicit learning
                    oResult.Add(@"https://www.youtube.com/watch?v=abFz6JgOMCk&list=PLs7zUO7VPyJ5DV1iBRgSw2uDl832n0bLg");
                    oResult.Add(@"https://www.youtube.com/@Der_Postillon");
                    oResult.Add(@"https://www.youtube.com/results?search_query=ganzer+film+deutsch");
                    oResult.Add(@"https://www.kraehseite.de/");
                    oResult.Add(@"https://www.welt.de/satire/");
                    oResult.Add(@"https://www.radio.de/country/germany");
                    oResult.Add(@"https://de.wikipedia.org/wiki/Spezial:Zuf%C3%A4llige_Seite");
                    oResult.Add(@"https://www.schlagerradio.de/");
                    oResult.Add(@"https://www.dw.com/de");
                    oResult.Add(@"https://www.rtl.de/unterhaltung/");
                    oResult.Add(@"https://www.tagesschau.de/multimedia");
                    oResult.Add(@"https://de.euronews.com/");
                    oResult.Add(@"https://www.der-postillon.com/");
                    oResult.Add(@"https://www.youtube.com/@WinxClubDE");
                    oResult.Add(@"https://www.deutschlandfunk.de/");
                    oResult.Add(@"https://www.amazon.de/s?k=das+philosophie+buch");
                    oResult.Add(@"https://www.amazon.de/s?k=Wirtschaft+verstehen+mit+Infografiken");
                    oResult.Add(@"https://www.amazon.de/s?k=dtv+atlas");
                    oResult.Add(@"https://www.amazon.de/s?k=eine+kurze+weltgeschichte+f%C3%BCr+junge+leser+ernst+h.+gombrich");
                    oResult.Add(@"https://www.amazon.de/s?k=glutenfreie+rezepte");
                    oResult.Add(@"https://www.amazon.de/s?k=laktosefreie+rezepte");
                    oResult.Add(@"https://www.amazon.de/s?k=low+carb+ngv");
                    oResult.Add(@"https://www.amazon.de/s?k=friedenspreis+des+deutschen+buchhandels+-Ansprachen+-Verleihung");

                    // sometimes also add the critical book
                    if (m_oRnd.Next(10) == 7)
                    {
                        oResult.Add(@"https://www.amazon.de/s?k=die+kunst+der+ehezerr%C3%BCttung");
                    }
                }
            }

            // if current culture isn't french
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("fr"))
            {
                // and one of the trained languages is french
                if (strLanguageFirst2.Equals("Fr", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Фр", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add french resources for language aquisition without explicit learning
                    oResult.Add(@"https://www.youtube.com/results?search_query=Film+complet+en+fran%C3%A7ais");
                    oResult.Add(@"https://www.youtube.com/@CinemaCinemas");
                    oResult.Add(@"https://www.youtube.com/results?search_query=Satire+fran%C3%A7aise");
                    oResult.Add(@"https://www.radio-en-ligne.fr/");
                    oResult.Add(@"https://fr.wikipedia.org/wiki/Wikip%C3%A9dia:Accueil_principal");
                    oResult.Add(@"https://www.france24.com/fr/");
                    oResult.Add(@"https://fr.euronews.com/");
                    oResult.Add(@"https://www.youtube.com/@WinxClubFR");
                    oResult.Add(@"https://www.amazon.fr/s?k=psychologie+livre");

                    // for language speakers that aren't muslim add visionary christian songs
                    switch (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "he":
                        case "de":
                        case "es":
                        case "en":
                        case "pt":
                        case "it":
                        case "po":
                        case "af":
                        case "am":
                        case "bg":
                        case "bs":
                        case "ca":
                        case "el":
                        case "fi":
                        case "hi":
                        case "zh":
                        case "hr":
                        case "uk":
                        case "ru":
                        case "hy":
                        case "ig":
                        case "is":
                        case "ka":
                        case "km":
                        case "ko":
                        case "lt":
                        case "lv":
                        case "mk":
                        case "mn":
                        case "ro":
                        case "sk":
                        case "sl":
                        case "so":
                        case "sr":
                        case "sv":
                        case "th":
                        case "vi":
                        case "wo":
                        case "yo":
                            oResult.Add(@"https://www.youtube.com/results?search_query=MARYLINE+ORCEL+Digne+est+l%27Agneau+%E2%80%93+Louange+et+Adoration");
                            //https://www.youtube.com/results?search_query=Agence+pour+le+ciel+fran%C3%A7aise
                            //https://www.youtube.com/results?search_query=DANS+LE+CIEL+AVEC+JESUS.+Du+ciel+bient%C3%B4t.+Jesus+va+faire+son+apparition.+Es+tu+prets%3F+HOSANA+BENI+SOIT+L%27ETERNEL.+UN+GRAND+JOUR+DANS+LE+CIEL.+Ce+que+sa+bouche+a+dit%2C+Sa+main+I%E2%80%99accomplira+Alleluia%21+Alleluia%21+Car+iI+est+notre+Dieu.+JESUS+ROIS+DES+ROIS
                            break;
                    }
                    

                    // for german speakers: Witch huckla / French
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("de"))
                    {
                        oResult.Add(@"https://www.amazon.de/s?k=hexe+huckla+französisch");
                        oResult.Add(@"https://www.amazon.de/Die-Gro%C3%9Fe-Franz%C3%B6sisch-Lernen-Box-3-CD-Hspbox/dp/B07N3P5B1N");
                    }
                }
            }


            // if current culture isn't spanish
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("es"))
            {
                // and one of the trained languages is spanish
                if (strLanguageFirst2.Equals("Es", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Ис", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Sp", StringComparison.InvariantCultureIgnoreCase)
                    )
                {
                    // then add spanish resources for language aquisition without explicit learning
                    oResult.Add(@"https://emisoras.com.mx/");
                    oResult.Add(@"https://www.amazon.com/-/es/mejores-3000-chistes-espa%C3%B1ol-Spanish/dp/B0B6XRZF2R");
                    oResult.Add(@"https://www.youtube.com/results?search_query=pel%C3%ADcula+en+espa%C3%B1ol");
                    oResult.Add(@"https://elpais.com/noticias/espana/");
                    oResult.Add(@"https://www.bbc.com/mundo");
                    oResult.Add(@"https://www.dw.com/es");
                    oResult.Add(@"https://www.youtube.com/@WinxClubES");
                    oResult.Add(@"https://www.amazon.es/s?k=libro+de+psicologia");
                    

                    // for language speakers that aren't muslim add visionary christian songs
                    switch (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "he":
                        case "de":
                        case "fr":
                        case "en":
                        case "pt":
                        case "it":
                        case "po":
                        case "af":
                        case "am":
                        case "bg":
                        case "bs":
                        case "ca":
                        case "el":
                        case "fi":
                        case "hi":
                        case "zh":
                        case "hr":
                        case "uk":
                        case "ru":
                        case "hy":
                        case "ig":
                        case "is":
                        case "ka":
                        case "km":
                        case "ko":
                        case "lt":
                        case "lv":
                        case "mk":
                        case "mn":
                        case "ro":
                        case "sk":
                        case "sl":
                        case "so":
                        case "sr":
                        case "sv":
                        case "th":
                        case "vi":
                        case "wo":
                        case "yo":
                            oResult.Add(@"https://www.youtube.com/results?search_query=M%C3%BAsica+Cristiana+Celestial");
                            break;
                    }

                }
            }

            // if current culture isn't english
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("en"))
            {
                // and one of the trained languages is english
                if (strLanguageFirst2.Equals("En", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Ан", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("In", StringComparison.InvariantCultureIgnoreCase)
                    )
                {
                    // then add english resources for language aquisition without explicit learning
                    oResult.Add(@"https://www.youtube.com/results?search_query=English+movie");
                    oResult.Add(@"https://www.bbc.com");
                    oResult.Add(@"https://www.cnn.com");
                    oResult.Add(@"https://www.the-postillon.com/");
                    oResult.Add(@"https://www.youtube.com/results?search_query=just+for+laughs+gags");
                    oResult.Add(@"https://www.dw.com/en/");
                    oResult.Add(@"https://www.youtube.com/results?search_query=nightcore+lyrics");
                    oResult.Add(@"https://www.abc.net.au");
                    oResult.Add(@"https://www.euronews.com/");
                    oResult.Add(@"https://en.wikipedia.org/wiki/Main_Page");
                    oResult.Add(@"https://www.youtube.com/@winxclub");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=The+economics+book");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=the+intelligent+investor+by+benjamin+graham");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=the+wealth+of+nations");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=irrational+exuberance");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=Mary+Buffett");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=Death+of+Stalin");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=The+dictator+dvd");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=inception+dvd");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=matrix+dvd");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=harry+potter+and+the+philosopher's+stone");

                    // also sometimes add the business dynamics sometimes that is probably useful only 
                    // for a small percentage of users
                    if (m_oRnd.Next(10) == 7)
                    {
                        oResult.Add(@"https://www.amazon.co.uk/s?k=business+dynamics+sterman");
                    }

                    // for german speakers: Witch huckla / English
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("de"))
                    {
                        oResult.Add(@"https://www.amazon.de/s?k=hexe+huckla+englisch");
                    }

                    // for language speakers that aren't muslim add visionary christian songs with lyrics
                    switch (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "he":
                            oResult.Add(@"https://www.youtube.com/results?search_query=angelic+hymns+of+heaven+lyrics");
                            goto case "de";
                        case "de":
                        case "fr":
                        case "es":
                        case "pt":
                        case "it":
                        case "po":
                        case "af":
                        case "am":
                        case "bg":
                        case "bs":
                        case "ca":
                        case "el":
                        case "fi":
                        case "hi":
                        case "zh":
                        case "hr":
                        case "uk":
                        case "ru":
                        case "hy":
                        case "ig":
                        case "is":
                        case "ka":
                        case "km":
                        case "ko":
                        case "lt":
                        case "lv":
                        case "mk":
                        case "mn":
                        case "ro":
                        case "sk":
                        case "sl":
                        case "so":
                        case "sr":
                        case "sv":
                        case "th":
                        case "vi":
                        case "wo":
                        case "yo":
                            oResult.Add(@"https://www.youtube.com/results?search_query=divine+harmony+choir+lyrics");
                            break;
                    }
                }
            }


            // if current culture isn't portugese
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("pt"))
            {
                // and one of the trained languages is portugese
                if (strLanguageFirst3.Equals("Por", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst3.Equals("Пор", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add Portugese resources for language aquisition without explicit learning
                    oResult.Add(@"https://www.youtube.com/results?search_query=filme+em+portugu%C3%AAs");
                    oResult.Add(@"https://www.bbc.com/portuguese");
                    oResult.Add(@"https://www.dn.pt/");
                    oResult.Add(@"https://radioonline.com.pt/");
                    oResult.Add(@"https://www.dw.com/pt-br/");
                    oResult.Add(@"https://pt.euronews.com/");
                    oResult.Add(@"https://www.jn.pt/");
                    oResult.Add(@"https://estrelaseouricos.sapo.pt/atividade/61-piadas-secas-para-impressionar-os-miudos/");
                    oResult.Add(@"https://www.youtube.com/@WinxClubPT");
                }
            }


            // if current culture isn't russian
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("ru"))
            {
                // and one of the trained languages is russian
                if (strLanguageFirst3.Equals("Rus", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst3.Equals("Рус", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add resources in russian for language aquisition without explicit learning (mostly western resources, not from russia)
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D0%A4%D0%B8%D0%BB%D1%8C%D0%BC%D1%8B");
                    oResult.Add(@"https://www.bbc.com/russian");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D1%81%D1%82%D1%8D%D0%BD%D0%B4%D0%B0%D0%BF");
                    oResult.Add(@"https://www.golosameriki.com/");
                    oResult.Add(@"https://www.dw.com/ru/");
                    oResult.Add(@"https://ru.euronews.com/");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D0%94%D0%B8%D0%B7%D0%B5%D0%BB%D1%8C+%D1%88%D0%BE%D1%83+%D0%94%D0%B8%D0%B7%D0%B5%D0%BB%D1%8C+c%D1%82%D1%83%D0%B4%D0%B8%D0%BE+%D0%BF%D0%BE+%D1%80%D1%83%D1%81%D1%81%D0%BA%D0%B8");
                    oResult.Add(@"https://news.google.com/home?hl=ru&gl=RU&ceid=RU:ru");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D0%9C%D0%B8%D1%82%D1%8F%D0%B9");
                    oResult.Add(@"https://www.anekdot.ru/");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D0%9C%D0%B0%D1%88%D0%B0+%D0%B8+%D0%9C%D0%B5%D0%B4%D0%B2%D0%B5%D0%B4%D1%8C");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D0%9D%D1%83+%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B8");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D1%81%D0%BB%D1%83%D0%B3%D0%B0+%D0%BD%D0%B0%D1%80%D0%BE%D0%B4%D0%B0");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D0%BA%D0%B0%D0%BC%D0%B5%D0%B4%D0%B8+%D0%BA%D0%BB%D0%B0%D0%B1");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D0%BD%D0%B5+%D1%80%D0%BE%D0%B4%D0%B8%D1%81%D1%8C+%D0%BA%D1%80%D0%B0%D1%81%D0%B8%D0%B2%D0%BE%D0%B9");
                    oResult.Add(@"https://www.youtube.com/@WinxClubRU");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D0%BC%D1%83%D0%BB%D1%8C%D1%82%D1%84%D0%B8%D0%BB%D1%8C%D0%BC%D1%8B+%D0%BD%D0%B0+%D1%80%D1%83%D1%81%D1%81%D0%BA%D0%BE%D0%BC+%D1%8F%D0%B7%D1%8B%D0%BA%D0%B5");
                }
            }



            // if current culture isn't italian
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("it"))
            {
                // and one of the trained languages is italian
                if (strLanguageFirst2.Equals("It", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Ит", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add resources in italian for language aquisition without explicit learning 
                    oResult.Add(@"https://www.radio.de/language/italian");
                    oResult.Add(@"https://www.corriere.it/");
                    oResult.Add(@"https://www.ansa.it/");
                    oResult.Add(@"https://www.learnitalianpod.com/2023/07/13/italian-jokes/");
                    oResult.Add(@"https://it.euronews.com/");
                    oResult.Add(@"https://www.youtube.com/results?search_query=film+completo");
                    oResult.Add(@"https://www.youtube.com/@WinxClubIT");
                }
            }


            // if current culture isn't korean
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("ko"))
            {
                // and one of the trained languages is korean
                if (strLanguageFirst3.Equals("Kor", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst3.Equals("Кор", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("한국", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add resources in korean for language aquisition without explicit learning
                    oResult.Add(@"https://www.bbc.com/korean");
                    oResult.Add(@"https://www.donga.com/");
                    oResult.Add(@"https://www.joongang.co.kr/");
                    oResult.Add(@"https://www.chosun.com/");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%ED%95%9C%EA%B5%AD%EC%98%81%ED%99%94");
                    oResult.Add(@"https://www.youtube.com/watch?v=u2U4Qb5uASk");
                }
            }


            // if current culture isn't japanese
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("ja"))
            {
                // and one of the trained languages is Japanese
                if (strLanguageFirst2.Equals("Ja", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Яп", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("日本", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add resources in japanese for language aquisition without explicit learning
                    oResult.Add(@"https://www.bbc.com/japanese");
                    oResult.Add(@"https://www3.nhk.or.jp/news/easy/");
                    oResult.Add(@"https://www.asahi.com/");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%E6%97%A5%E6%9C%AC%E6%98%A0%E7%94%BB");
                }
            }


            // if current culture isn't chinese
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("zh"))
            {
                // and one of the trained languages is chinese
                if (strLanguageFirst2.Equals("Ch", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Ки", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("中文", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add resources in chinese for language aquisition without explicit learning
                    oResult.Add(@"https://www.dw.com/zh/");
                    oResult.Add(@"https://www.bbc.com/zhongwen/simp");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%E5%AD%97%E5%B9%95%E4%BB%98%E3%81%8D%E3%81%AE%E4%B8%AD%E5%9B%BD%E6%98%A0%E7%94%BB");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%E4%B8%AD%E5%9B%BD%E7%94%B5%E5%BD%B1%E6%9C%89%E5%AD%97%E5%B9%95");
                    oResult.Add(@"https://www.youtube.com/@RainbowJuniorCN");
                }
            }
        
            // if current culture isn't turkish
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("tr"))
            {
                // and one of the trained languages is turkish
                if (strLanguageFirst2.Equals("Tü", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Ту", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Tu", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add resources in turkish for language aquisition without explicit learning
                    oResult.Add(@"https://www.dw.com/tr/");
                    oResult.Add(@"https://www.bbc.com/turkce");
                    oResult.Add(@"https://www.radio.de/language/turkish");
                    oResult.Add(@"https://www.youtube.com/results?search_query=Türkçe+Dublaj+Film");
                    oResult.Add(@"https://www.youtube.com/@WinxClubTr");
                }
            }

            // if current culture isn't arabic
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("ar"))
            {
                // and one of the trained languages is arabic
                if (strLanguageFirst2.Equals("Ar", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Ар", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("عر", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add resources in arabic for language aquisition without explicit learning
                    oResult.Add(@"https://www.dw.com/ar/");
                    oResult.Add(@"https://www.bbc.com/arabic");
                    oResult.Add(@"https://www.radio.de/language/arabic");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D8%A7%D9%81%D9%84%D8%A7%D9%85+%D8%B9%D8%B1%D8%A8%D9%8A%D8%A9");
                    oResult.Add(@"https://www.aljazeera.net/");
                }
            }

            // if current culture isn't hebrew
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("he"))
            {
                // and one of the trained languages is hebrew
                if (strLanguageFirst2.Equals("He", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Ив", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("עב", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("עִ", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add resources in hebrew for language aquisition without explicit learning
                    oResult.Add(@"https://hebrewnews.com/");
                    oResult.Add(@"https://www.haaretz.co.il/");
                    oResult.Add(@"https://www.calcalist.co.il");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D7%A1%D7%A8%D7%98+%D7%91%D7%A2%D7%91%D7%A8%D7%99%D7%AA+%D7%A2%D7%9D+%D7%9B%D7%AA%D7%95%D7%91%D7%99%D7%95%D7%AA");
                    oResult.Add(@"https://www.radio.de/language/hebrew");
                }
            }

            // if current culture isn't greek
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("el"))
            {
                // and one of the trained languages is greek
                if (strLanguageFirst2.Equals("Gr", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Гр", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("ελ", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Ελ", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add resources in greek for language aquisition without explicit learning
                    oResult.Add(@"https://live24.gr/");
                    oResult.Add(@"https://www.cnn.gr/");
                    oResult.Add(@"https://www.news247.gr/");
                    oResult.Add(@"https://www.youtube.com/results?search_query=Ελληνική+ταινία");
                    oResult.Add(@"https://www.ert.gr/");
                }
            }


            // if current culture isn't hindi
            if (!System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("hi"))
            {
                // and one of the trained languages is hindi
                if (strLanguageFirst2.Equals("Hi", StringComparison.InvariantCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("Ин", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("द्", StringComparison.CurrentCultureIgnoreCase) ||
                    strLanguageFirst2.Equals("भा", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // then add resources in hindi for language aquisition without explicit learning
                    oResult.Add(@"https://www.dw.com/hi/");
                    oResult.Add(@"https://www.bbc.com/hindi");
                    oResult.Add(@"https://www.radioindia.in/");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%E0%A4%B9%E0%A4%BF%E0%A4%82%E0%A4%A6%E0%A5%80+%E0%A4%AB%E0%A4%BF%E0%A4%B2%E0%A5%8D%E0%A4%AE");
                    oResult.Add(@"https://www.indiatv.in/");
                    oResult.Add(@"https://www.aajtak.in/livetv");
                }
            }

            return oResult;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks on easy language aquisition button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_lblDontLearnAquire_LinkClicked(object oSender, LinkLabelLinkClickedEventArgs oArgs)
        {
            List<string> oEasyAquireList = GetEasyLanguageAquireList();
            if (oEasyAquireList != null && oEasyAquireList.Count > 0)
            {
                System.Diagnostics.Process.Start(oEasyAquireList[m_oRnd.Next(oEasyAquireList.Count)]);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Shows OS keyboard and language settings (is called when gear is clicked)
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oRags">Event args</param>
        //===================================================================================================
        private void m_btnOsLanguageAndKeyboardSettings_Click(object oSender, EventArgs oArgs)
        {
            string strWindowsVersion = Environment.OSVersion.Version.ToString();

            try
            {
                if (strWindowsVersion.StartsWith("10.") || strWindowsVersion.StartsWith("11."))
                {
                    // Windows 10 and 11
                    Process.Start(new ProcessStartInfo("cmd", "/c start ms-settings:regionlanguage") 
                    { 
                        CreateNoWindow = true 
                    });
                }
                else 
                if (strWindowsVersion.StartsWith("6.3"))
                {
                    // Windows 8.1
                    Process.Start("control.exe", "/name Microsoft.Language");
                }
                else if (strWindowsVersion.StartsWith("6.2"))
                {
                    // Windows 8
                    Process.Start("control.exe", "/name Microsoft.Language");
                }
                else if (strWindowsVersion.StartsWith("6.1")) 
                {
                    // Windows 7
                    Process.Start("control.exe", "/name Microsoft.RegionAndLanguage");
                }
            }
            catch (Exception oEx)
            {
                MessageBox.Show(oEx.Message);
            }
        }

        //===================================================================================================
        /// <summary>
        /// This is execute when user presses F1 key
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Even args</param>
        //===================================================================================================
        private void OnHelpRequested(object oSender, HelpEventArgs oEventArgs)
        {
            System.Diagnostics.Process.Start(System.IO.Path.Combine(Application.StartupPath, "Readme.html"));
        }


        #region image injection part
        //===================================================================================================
        /// <summary>
        /// Picture box control
        /// </summary>
        private PictureBox m_ctlPictureBox;
        //===================================================================================================
        /// <summary>
        /// Image
        /// </summary>
        private System.Drawing.Image m_oLoadedImage;
        //===================================================================================================
        /// <summary>
        /// A dictionary with positions of other elements
        /// </summary>
        private Dictionary<Control, int> m_oOriginalPositions;

        //===================================================================================================
        /// <summary>
        /// Loads an image from application startup path and shows it at the top of the window
        /// </summary>
        /// <param name="strName">Name of the image, without directory specifications</param>
        //===================================================================================================
        private bool ReadyToUseImageInjection(string strImageName)
        {
            string strImagePath = System.IO.Path.Combine(Application.StartupPath, strImageName);
            if (System.IO.File.Exists(strImagePath))
            {
                m_oOriginalPositions = new Dictionary<Control, int>();
                foreach (Control ctl in Controls)
                {
                    m_oOriginalPositions[ctl] = ctl.Top;
                }

                m_ctlPictureBox = new PictureBox();
                m_ctlPictureBox.Location = this.ClientRectangle.Location;
                m_ctlPictureBox.Size = new Size(0, 0);
                Controls.Add(m_ctlPictureBox);

                LoadAndResizeImage(strImagePath);

                this.Resize += new EventHandler(ResizeImageAlongWithForm);
                return true;
            }
            else
            {
                return false;
            }
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image along with the form
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void ResizeImageAlongWithForm(object oSender, EventArgs oEventArgs)
        {
            ResizeImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Loads an image and resizes it to the width of client area
        /// </summary>
        /// <param name="strImagePath"></param>
        //===================================================================================================
        private void LoadAndResizeImage(string strImagePath)
        {
            m_oLoadedImage = Image.FromFile(strImagePath);
            ResizeImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image and shifts other elements
        /// </summary>
        //===================================================================================================
        private void ResizeImageAndShiftElements()
        {
            if (m_oLoadedImage != null)
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    float fAspectRatio = (float)m_oLoadedImage.Width / (float)m_oLoadedImage.Height;

                    int nNewWidth = this.ClientSize.Width;
                    if (nNewWidth != 0)
                    {
                        int nNewHeight = (int)(nNewWidth / fAspectRatio);

                        int nHeightChange = nNewHeight - m_ctlPictureBox.Height;

                        this.m_ctlPictureBox.Image = new Bitmap(m_oLoadedImage, nNewWidth, nNewHeight);
                        this.m_ctlPictureBox.Size = new Size(nNewWidth, nNewHeight);

                        ShiftOtherElementsUpOrDown(nHeightChange);
                        this.Height += nHeightChange;
                    }
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Shifts elements, apart from the image box up or down
        /// </summary>
        /// <param name="nHeightChange"></param>
        //===================================================================================================
        private void ShiftOtherElementsUpOrDown(int nHeightChange)
        {
            foreach (Control ctl in m_oOriginalPositions.Keys)
            {
                if ((ctl.Anchor & AnchorStyles.Bottom) == AnchorStyles.None)
                    ctl.Top += nHeightChange;
            }
        }
        #endregion

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the Stats button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnButtonStatsClick(object oSender, MouseEventArgs oArgs)
        {
            // Open the graphs window
            GraphsForm graphsForm = new GraphsForm(
                m_oTotalGraphData,
                m_oWordsGraphData,
                m_oLearnedWordsGraphData
                );
            graphsForm.Show();
        }



        //===================================================================================================
        /// <summary>
        /// Calculates the day of eastern for current year
        /// </summary>
        /// <returns>The day of eastern sunday</returns>
        //===================================================================================================
        static DateTime GetEasterSunday()
        {
            int nYear = System.DateTime.Now.Year;

            int a = nYear % 19;
            int b = nYear / 100;
            int c = nYear % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateTime(nYear, month, day);
        }

        //===================================================================================================
        /// <summary>
        /// Calculates the day of eastern for current year
        /// </summary>
        /// <returns>The day of eastern sunday</returns>
        //===================================================================================================
        static DateTime GetEasterStart()
        {
            return GetEasterSunday().AddDays(-3);
        }


        //===================================================================================================
        /// <summary>
        /// Calculates the day of eastern for current year
        /// </summary>
        /// <returns>The day of eastern sunday</returns>
        //===================================================================================================
        static DateTime GetEasterEnd()
        {
            return GetEasterSunday().AddDays(+2);
        }



        //===================================================================================================
        /// <summary>
        /// Gets approximate ramadan start data
        /// </summary>
        /// <returns>The start or ramadan heading</returns>
        //===================================================================================================
        static DateTime GetRamadanStart()
        {
            int nYear = System.DateTime.Now.Year;

            // if there is a ready date then use it
            if (s_oRamadanStartDates.ContainsKey(nYear))
                return s_oRamadanStartDates[nYear];

            // in other case estimate it
            double dblShiftPerYear = -10.875;
            double dblTotalShift = (nYear - 2035) * dblShiftPerYear;

            int nRoundedShift = (int)Math.Round(dblTotalShift);

            DateTime dtmReferenceRamadan2035 = new DateTime(2035, 11, 1);
            DateTime dtmTemp = dtmReferenceRamadan2035.AddDays(nRoundedShift);
            DateTime dtmEstimatedRamadan = new DateTime(
                System.DateTime.Now.Year, dtmTemp.Month, dtmTemp.Day);

            return dtmEstimatedRamadan;
        }




        //===================================================================================================
        /// <summary>
        /// Gets a date for end of showing ramadan header
        /// </summary>
        /// <returns>5 days after start of ramadan header</returns>
        //===================================================================================================
        static DateTime GetRamadanEnd()
        {
            return GetRamadanStart().AddDays(5);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of christmas header
        /// </summary>
        //===================================================================================================
        static DateTime GetChristmasStart()
        {
            return new DateTime(DateTime.Now.Year, 12, 22);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of christmas header
        /// </summary>
        //===================================================================================================
        static DateTime GetChristmasEnd()
        {
            return new DateTime(DateTime.Now.Year, 12, 27);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of orthodox christmas header
        /// </summary>
        //===================================================================================================
        static DateTime GetOrthodoxChristmasStart()
        {
            return new DateTime(DateTime.Now.Year, 1, 5);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of orthodox christmas header
        /// </summary>
        //===================================================================================================
        static DateTime GetOrthodoxChristmasEnd()
        {
            return new DateTime(DateTime.Now.Year, 1, 9);
        }

        //===================================================================================================
        /// <summary>
        /// The New Years eve header start date
        /// </summary>
        /// <returns>Last day of the year</returns>
        //===================================================================================================
        static DateTime GetNewYearStart()
        {
            return new DateTime(DateTime.Now.Year, 12, 31);
        }

        //===================================================================================================
        /// <summary>
        /// The New Years eve header end date
        /// </summary>
        /// <returns>Last day of the year</returns>
        //===================================================================================================
        static DateTime GetNewYearEnd()
        {
            return new DateTime(DateTime.Now.Year, 1, 2);
        }


        //===================================================================================================
        /// <summary>
        /// Estimate Diwali dates after known data
        /// </summary>
        /// <returns>The diwali start date for current year</returns>
        //===================================================================================================
        public static DateTime GetDiwaliStart()
        {
            int nYear = System.DateTime.Now.Year;

            if (s_oDiwaliDates.ContainsKey(nYear))
            {
                return s_oDiwaliDates[nYear].AddDays(-2);
            }
            else
            {
                // Estimate based on past trends (Diwali usually falls between October and November)
                int nEstimatedMonth = (nYear % 3 == 0) ? 10 : 11; // Alternating between Oct and Nov
                int nEstimatedDay = 20 + (nYear % 5); // Rough day estimation
                return new DateTime(nYear, nEstimatedMonth, nEstimatedDay).AddDays(-2);
            }
        }


        //===================================================================================================
        /// <summary>
        /// Estimate Diwali dates after known data
        /// </summary>
        /// <returns>The diwali end date for current year</returns>
        //===================================================================================================
        public static DateTime GetDiwaliEnd()
        {
            return GetDiwaliStart().AddDays(5);
        }


        //===================================================================================================
        /// <summary>
        /// Estimates the chinese new year dates
        /// </summary>
        /// <returns>The start of chinese new year header</returns>
        //===================================================================================================
        public static DateTime GetChineseNewYearStart()
        {
            int nYear = System.DateTime.Now.Year;

            // if after the last year then shift back using 19 year meton cycle
            while (nYear > 2044)
                nYear -= 19;

            // if before the first year then shift forward using 19 year meton cycle
            while (nYear < 2025)
                nYear += 19;

            // if known then return exact date
            if (s_oChineseNewYearDates.ContainsKey(nYear))
            {
                DateTime dtmKnown = s_oChineseNewYearDates[nYear];
                return new DateTime(System.DateTime.Now.Year, dtmKnown.Month, dtmKnown.Day).AddDays(-2);
            }

            // now known? that's strange! return something
            return new DateTime(System.DateTime.Now.Year, 1, 29);
        }


        //===================================================================================================
        /// <summary>
        /// Estimates the chinese new year dates
        /// </summary>
        /// <returns>The start of chinese new year header</returns>
        //===================================================================================================
        public static DateTime GetChineseNewYearEnd()
        {
            return GetChineseNewYearStart().AddDays(5);
        }


        //===================================================================================================
        /// <summary>
        /// Returns the beginning of Hajj Header start
        /// </summary>
        /// <returns>Start of Hajj header</returns>
        //===================================================================================================
        static DateTime GetHajjStart()
        {

            int nYear = System.DateTime.Now.Year;

            // If year is within known dates, return exact date
            if (s_oKnownHajjDates.ContainsKey(nYear))
            {
                return s_oKnownHajjDates[nYear].AddDays(-2);
            }

            // in other case estimate it
            double dblShiftPerYear = -10.875;
            double dblTotalShift = (nYear - 2035) * dblShiftPerYear;

            int nRoundedShift = (int)Math.Round(dblTotalShift);

            DateTime dtmReferenceHajj2035 = new DateTime(2034, 3, 2);
            DateTime dtmTemp = dtmReferenceHajj2035.AddDays(nRoundedShift);
            DateTime dtmEstimatedHajj = new DateTime(
                System.DateTime.Now.Year, dtmTemp.Month, dtmTemp.Day);

            return dtmEstimatedHajj.AddDays(-2);
        }


        //===================================================================================================
        /// <summary>
        /// Returns the end of Hajj Header 
        /// </summary>
        /// <returns>End of Hajj header</returns>
        //===================================================================================================
        static DateTime GetHajjEnd()
        {
            return GetHajjStart().AddDays(5);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of Halloween
        /// </summary>
        //===================================================================================================
        static DateTime GetHalloweenStart()
        {
            return new DateTime(DateTime.Now.Year, 10, 26);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of Halloween header
        /// </summary>
        //===================================================================================================
        static DateTime GetHalloweenEnd()
        {
            return new DateTime(DateTime.Now.Year, 11, 2);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of children and Buddha header
        /// </summary>
        //===================================================================================================
        static DateTime GetChildrenAndBuddhaStart()
        {
            return new DateTime(DateTime.Now.Year, 5, 2);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of children and Buddha header
        /// </summary>
        //===================================================================================================
        static DateTime GetChildrenAndBuddhaEnd()
        {
            return new DateTime(DateTime.Now.Year, 5, 6);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of science day header
        /// </summary>
        //===================================================================================================
        static DateTime GetScienceStart()
        {
            return new DateTime(DateTime.Now.Year, 11, 7);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of science header
        /// </summary>
        //===================================================================================================
        static DateTime GetScienceEnd()
        {
            return new DateTime(DateTime.Now.Year, 11, 11);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the beginning of philosophy header
        /// </summary>
        //===================================================================================================
        static DateTime GetPhilosophyStart()
        {
            return new DateTime(DateTime.Now.Year, 11, 17);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of philosophy header
        /// </summary>
        //===================================================================================================
        static DateTime GetPhilosophyEnd()
        {
            return new DateTime(DateTime.Now.Year, 11, 21);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of psychology header
        /// </summary>
        //===================================================================================================
        static DateTime GetPsychologyStart()
        {
            return new DateTime(DateTime.Now.Year, 10, 7);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of psychology header
        /// </summary>
        //===================================================================================================
        static DateTime GetPsychologyEnd()
        {
            return new DateTime(DateTime.Now.Year, 10, 11);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of reading day header
        /// </summary>
        //===================================================================================================
        static DateTime GetReadingDayStart()
        {
            return new DateTime(DateTime.Now.Year, 4, 20);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of reading day header
        /// </summary>
        //===================================================================================================
        static DateTime GetReadingDayEnd()
        {
            return new DateTime(DateTime.Now.Year, 4, 24);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the beginning of valentine header
        /// </summary>
        //===================================================================================================
        static DateTime GetValentineStart()
        {
            return new DateTime(DateTime.Now.Year, 2, 13);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of valentine header
        /// </summary>
        //===================================================================================================
        static DateTime GetValentineEnd()
        {
            return new DateTime(DateTime.Now.Year, 2, 15);
        }

        //===================================================================================================
        /// <summary>
        /// Estimates the Rosh Hashanah Israeli new year dates
        /// </summary>
        /// <returns>The start of Rosh Hashanah Israeli new year header</returns>
        //===================================================================================================
        public static DateTime GetRoshHashanahStart()
        {
            int nYear = System.DateTime.Now.Year;

            // if after the last year then shift back using 19 year meton cycle
            while (nYear > 2044)
                nYear -= 19;

            // if before the first year then shift forward using 19 year meton cycle
            while (nYear < 2025)
                nYear += 19;

            // if known then return exact date
            if (s_oRoshHashanahDates.ContainsKey(nYear))
            {
                DateTime dtmKnown = s_oRoshHashanahDates[nYear];
                return new DateTime(System.DateTime.Now.Year, dtmKnown.Month, dtmKnown.Day).AddDays(-4);
            }

            // now known? that's strange! return something
            return new DateTime(System.DateTime.Now.Year, 9, 20);
        }

        //===================================================================================================
        /// <summary>
        /// Estimates the Rosh Hashanah Israeli new year dates
        /// </summary>
        /// <returns>The end of Rosh Hashanah Israeli new year header</returns>
        //===================================================================================================
        public static DateTime GetRoshHashanahEnd()
        {
            return GetRoshHashanahStart().AddDays(5);
        }

    }
}
