// VokabelTrainer v1.6
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
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using VokabelTrainer.Forms;
using VokabelTrainer.Properties;


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
        /// Mode of the vocabulary book. 
        /// "normal" is user-filled, no units/steps
        /// "kontinuierlich" means the vocabulary will be included automatically
        /// </summary>
        string m_strMode;
        //===================================================================================================
        /// <summary>
        /// The current level for the continuous training mode 
        /// </summary>
        int m_nStep;
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
        SortedDictionary<string, string> m_oTrainingResultsSecondLanguage;
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
        /// Predefined Diwali dates (using Hindu lunar calendar)
        /// </summary> 
        private static readonly Dictionary<int, DateTime> s_oDiwaliDates = new Dictionary<int, DateTime>
        {
            { 2026, new DateTime(2026, 11, 9) },
            { 2027, new DateTime(2027, 10, 29) },
            { 2028, new DateTime(2028, 10, 17) },
            { 2029, new DateTime(2029, 11, 5) },
            { 2030, new DateTime(2030, 10, 26) },
            { 2031, new DateTime(2031, 10, 15) },
            { 2032, new DateTime(2032, 11, 2) },
            { 2033, new DateTime(2033, 10, 23) },
            { 2034, new DateTime(2034, 11, 11) },
            { 2046, new DateTime(2046, 10, 23) },
            { 2047, new DateTime(2047, 11, 12) },
            { 2048, new DateTime(2048, 11, 1) },
            { 2049, new DateTime(2046, 10, 21) },
            { 2050, new DateTime(2050, 11, 9) },
            { 2051, new DateTime(2051, 10, 29) },
            { 2052, new DateTime(2052, 11, 17) }
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

            // let's see, if there are expected collisions within the next 2 years:
            for (int i=1; i<=366*2; ++i)
            {
                DateTime dtmTestedDay = DateTime.Now.AddDays(i);
                List<string> astrEventsThatDay = new List<string>();
                const string strDateFormat = "yyyy-MM-dd";

                if (dtmTestedDay >= GetEasterStart(dtmTestedDay) && dtmTestedDay < GetEasterEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetEasterStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetEasterEnd(dtmTestedDay).ToString(strDateFormat) + " Easter");
                }

                // Christmas
                if (dtmTestedDay >= GetChristmasStart(dtmTestedDay) && dtmTestedDay < GetChristmasEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetChristmasStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetChristmasEnd(dtmTestedDay).ToString(strDateFormat) + " Christmas");
                }

                // New year
                if (dtmTestedDay >= GetNewYearStart(dtmTestedDay) && dtmTestedDay < GetNewYearStart(dtmTestedDay).AddDays(2))
                {
                    astrEventsThatDay.Add(GetNewYearStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetNewYearEnd(dtmTestedDay).ToString(strDateFormat) + " Western New Year");
                }

                // Ramadan
                if (dtmTestedDay >= GetRamadanStart(dtmTestedDay) && dtmTestedDay < GetRamadanEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetRamadanStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetRamadanEnd(dtmTestedDay).ToString(strDateFormat) + " Ramadan");
                }


                // Diwali (Hindu light celebration for truth winning over lies)
                if (dtmTestedDay >= GetDiwaliStart(dtmTestedDay) && dtmTestedDay < GetDiwaliEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetDiwaliStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetDiwaliEnd(dtmTestedDay).ToString(strDateFormat) + " Diwali");
                }

                // Chinese new year
                if (dtmTestedDay >= GetChineseNewYearStart(dtmTestedDay) && dtmTestedDay < GetChineseNewYearEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetChineseNewYearStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetChineseNewYearEnd(dtmTestedDay).ToString(strDateFormat) + " Chinese New Year");
                }

                // Orthodox Christmas
                if (dtmTestedDay >= GetOrthodoxChristmasStart(dtmTestedDay) && dtmTestedDay < GetOrthodoxChristmasEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetOrthodoxChristmasStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetOrthodoxChristmasEnd(dtmTestedDay).ToString(strDateFormat) + " Orthodox Christmas");
                }


                // Muslim Hajj pilgrimage
                if (dtmTestedDay >= GetHajjStart(dtmTestedDay) && dtmTestedDay < GetHajjEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetHajjStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetHajjEnd(dtmTestedDay).ToString(strDateFormat) + " Hajj");
                }


                // Rosh Hashanah (Israeli new year) 
                if (dtmTestedDay >= GetRoshHashanahStart(dtmTestedDay) && dtmTestedDay < GetRoshHashanahEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetRoshHashanahStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetRoshHashanahEnd(dtmTestedDay).ToString(strDateFormat) + " Rosh Hashanah");
                }

                // Halloween 
                if (dtmTestedDay >= GetHalloweenStart(dtmTestedDay) && dtmTestedDay < GetHalloweenEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetHalloweenStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetHalloweenEnd(dtmTestedDay).ToString(strDateFormat) + " Halloween");
                }

                // Japanese children celebration and Korean Buddha birthday celebration 
                if (dtmTestedDay >= GetChildrenAndBuddhaStart(dtmTestedDay) && dtmTestedDay < GetChildrenAndBuddhaEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetChildrenAndBuddhaStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetChildrenAndBuddhaEnd(dtmTestedDay).ToString(strDateFormat) + " Children And Buddha");
                }

                // Day of Science
                if (dtmTestedDay >= GetScienceStart(dtmTestedDay) && dtmTestedDay < GetScienceEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetScienceStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetScienceEnd(dtmTestedDay).ToString(strDateFormat) + " World Science Day");
                }

                // Philosophy day
                if (dtmTestedDay >= GetPhilosophyStart(dtmTestedDay) && dtmTestedDay < GetPhilosophyEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetPhilosophyStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetPhilosophyEnd(dtmTestedDay).ToString(strDateFormat) + " Philosophy Day");
                }

                // Psychology day
                if (dtmTestedDay >= GetPsychologyStart(dtmTestedDay) && dtmTestedDay < GetPsychologyEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetPsychologyStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetPsychologyEnd(dtmTestedDay).ToString(strDateFormat) + " Psychology Day");
                }

                // Reading day
                if (dtmTestedDay >= GetReadingDayStart(dtmTestedDay) && dtmTestedDay < GetReadingDayEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetReadingDayStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetReadingDayEnd(dtmTestedDay).ToString(strDateFormat) + " Reading Day");
                }

                // Valentine day
                if (dtmTestedDay >= GetValentineStart(dtmTestedDay) && dtmTestedDay < GetValentineEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetValentineStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetValentineEnd(dtmTestedDay).ToString(strDateFormat) + " Valentine");
                }

                // World Savings Day
                if (dtmTestedDay >= GetWorldSavingsDayStart(dtmTestedDay) && dtmTestedDay < GetWorldSavingsDayEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetWorldSavingsDayStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetWorldSavingsDayEnd(dtmTestedDay).ToString(strDateFormat) + " World Savings Day");
                }

                // World Peace Day
                if (dtmTestedDay >= GetWorldPeaceDayStart(dtmTestedDay) && dtmTestedDay < GetWorldPeaceDayEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetWorldPeaceDayStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetWorldPeaceDayEnd(dtmTestedDay).ToString(strDateFormat) + " World Peace Day");
                }

                // World Dancing Day
                if (dtmTestedDay >= GetWorldDancingDayStart(dtmTestedDay) && dtmTestedDay < GetWorldDancingDayEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetWorldDancingDayStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetWorldDancingDayEnd(dtmTestedDay).ToString(strDateFormat) + " World Dancing Day");
                }


                // Olymbic summer games
                if (dtmTestedDay >= GetOlympicSummerStart(dtmTestedDay) && dtmTestedDay < GetOlympicSummerEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetOlympicSummerStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetOlympicSummerEnd(dtmTestedDay).ToString(strDateFormat) + " Olympic Summer Games");
                }

                // Olymbic winter games
                if (dtmTestedDay >= GetOlympicWinterStart(dtmTestedDay) && dtmTestedDay < GetOlympicWinterEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetOlympicWinterStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetOlympicWinterEnd(dtmTestedDay).ToString(strDateFormat) + " Olympic Winter Games");
                }

                // Olymbic winter games
                if (dtmTestedDay >= GetSoccerStart(dtmTestedDay) && dtmTestedDay < GetSoccerEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetSoccerStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetSoccerEnd(dtmTestedDay).ToString(strDateFormat) + " Soccer");
                }

                // Olymbic winter games
                if (dtmTestedDay >= GetSoccerChampionsStart(dtmTestedDay) && dtmTestedDay < GetSoccerChampionsEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetSoccerChampionsStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetSoccerChampionsEnd(dtmTestedDay).ToString(strDateFormat) + " Soccer Champions");
                }


                // Nobel Prize
                if (dtmTestedDay >= GetNobelPrizeStart(dtmTestedDay) && dtmTestedDay < GetNobelPrizeEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetNobelPrizeStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetNobelPrizeEnd(dtmTestedDay).ToString(strDateFormat) + " Nobel Prize");
                }

                // Oscar - Header
                if (dtmTestedDay >= GetOscarStart(dtmTestedDay) && dtmTestedDay < GetOscarEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetOscarStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetOscarEnd(dtmTestedDay).ToString(strDateFormat) + " Oscar");
                }

                // Cannes - Header
                if (dtmTestedDay >= GetCannesStart(dtmTestedDay) && dtmTestedDay < GetCannesEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetCannesStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetCannesEnd(dtmTestedDay).ToString(strDateFormat) + " Cannes");
                }

                // Berlinale - Header
                if (dtmTestedDay >= GetBerlinaleStart(dtmTestedDay) && dtmTestedDay < GetBerlinaleEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetBerlinaleStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetBerlinaleEnd(dtmTestedDay).ToString(strDateFormat) + " Berlinale");
                }

                // Durban - Header
                if (dtmTestedDay >= GetDurbanStart(dtmTestedDay) && dtmTestedDay < GetDurbanEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetDurbanStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetDurbanEnd(dtmTestedDay).ToString(strDateFormat) + " Durban");
                }

                // Timkat - Header
                if (dtmTestedDay >= GetTimkatStart(dtmTestedDay) && dtmTestedDay < GetTimkatEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetTimkatStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetTimkatEnd(dtmTestedDay).ToString(strDateFormat) + " Timkat");
                }

                // Grad Prix de la Chanson - ESC - Header
                if (dtmTestedDay >= GetEscStart(dtmTestedDay) && dtmTestedDay < GetEscEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetEscStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetEscEnd(dtmTestedDay).ToString(strDateFormat) + " ESC");
                }


                // Festima - Header (African Masks)
                if (dtmTestedDay >= GetFestimaStart(dtmTestedDay) && dtmTestedDay < GetFestimaEnd(dtmTestedDay))
                {
                    astrEventsThatDay.Add(GetFestimaStart(dtmTestedDay).ToString(strDateFormat) + ".." +
                        GetFestimaEnd(dtmTestedDay).ToString(strDateFormat) + " Festima");
                }

                if (astrEventsThatDay.Count>1)
                {
                    Console.WriteLine();
                    Console.WriteLine("There is a foreseeable event collision at " + dtmTestedDay.ToString(strDateFormat) + ":");
                    foreach(string strEvent in astrEventsThatDay)
                    {
                        Console.WriteLine(strEvent);
                    }


                }
            }

            // Variations of main header, depending on dates, let's start with Easter, other will follow
            DateTime dtmNow = DateTime.Now;
            string strStartPath = "Images" + Path.DirectorySeparatorChar;

            // currently running events, if any
            List<(string Header, string ToolTip, string Url)> oEvents = 
                new List<(string Header, string ToolTip, string Url)>(3);

            if (dtmNow >= GetEasterStart() && dtmNow < GetEasterEnd())
            {
                oEvents.Add((
                    strStartPath + "EasterHeader.jpg",
                    Resources.CelebrationEaster,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationEaster)));
            }

            // Christmas
            if (dtmNow >= GetChristmasStart() && dtmNow < GetChristmasEnd())
            {
                oEvents.Add((
                    strStartPath + "ChristmasHeader.jpg",
                    Resources.CelebrationChristmas,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationChristmas)));
            }

            // New year
            if (dtmNow >= GetNewYearStart() || dtmNow < GetNewYearEnd())
            {
                oEvents.Add((
                    strStartPath + "NewYearHeader.jpg",
                    Resources.CelebrationNewYear,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationNewYear)));

                if (ReadyToUseImageInjection(strStartPath + "NewYearHeader.jpg"))
                    return;
            }

            // Ramadan
            if (dtmNow >= GetRamadanStart() && dtmNow < GetRamadanEnd())
            {
                oEvents.Add((
                    strStartPath + "RamadanHeader.jpg",
                    Resources.CelebrationRamadan,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationRamadan)));
            }


            // Diwali (Hindu light celebration for truth winning over lies)
            if (dtmNow >= GetDiwaliStart() && dtmNow < GetDiwaliEnd())
            {
                oEvents.Add((
                    strStartPath + "DiwaliHeader.jpg",
                    Resources.CelebrationDiwali,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationDiwali)));

            }

            // Chinese new year
            if (dtmNow >= GetChineseNewYearStart() && dtmNow < GetChineseNewYearEnd())
            {
                oEvents.Add((
                    strStartPath + "ChineseNewYearHeader.jpg",
                    Resources.CelebrationChineseNewYear,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationChineseNewYear)));
            }

            // Orthodox Christmas
            if (dtmNow >= GetOrthodoxChristmasStart() && dtmNow < GetOrthodoxChristmasEnd())
            {
                oEvents.Add((
                    strStartPath + "OrthodoxChristmasHeader.jpg",
                    Resources.CelebrationOrthodoxChristmas,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationOrthodoxChristmas)));
            }


            // Muslim Hajj pilgrimage
            if (dtmNow >= GetHajjStart() && dtmNow < GetHajjEnd())
            {
                oEvents.Add((
                    strStartPath + "HajjHeader.jpg",
                    Resources.CelebrationHajj,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationHajj)));
            }


            // Rosh Hashanah (Israeli new year) 
            if (dtmNow >= GetRoshHashanahStart() && dtmNow < GetRoshHashanahEnd())
            {
                oEvents.Add((
                    strStartPath + "RoshHashanahHeader.jpg",
                    Resources.CelebrationRoshHashanah,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationRoshHashanah)));
            }

            // Halloween 
            if (dtmNow >= GetHalloweenStart() && dtmNow < GetHalloweenEnd())
            {
                oEvents.Add((
                    strStartPath + "HalloweenHeader.jpg",
                    Resources.CelebrationsHalloween,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationChristmas)));
            }

            // Japanese children celebration and Korean Buddha birthday celebration 
            if (dtmNow >= GetChildrenAndBuddhaStart() && dtmNow < GetChildrenAndBuddhaEnd())
            {
                oEvents.Add((
                    strStartPath + "ChildrenAndBuddhaHeader.jpg",
                    Resources.CelebrationChildrenAndBuddha,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationChildrenAndBuddha)));
            }

            // Day of Science
            if (dtmNow >= GetScienceStart() && dtmNow < GetScienceEnd())
            {
                oEvents.Add((
                    strStartPath + "ScienceDayHeader.jpg",
                    Resources.CelebrationScienceDay,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationScienceDay)));
            }

            // Philosophy day
            if (dtmNow >= GetPhilosophyStart() && dtmNow < GetPhilosophyEnd())
            {
                oEvents.Add((
                    strStartPath + "PhilosophyDayHeader.jpg",
                    Resources.CelebrationPhilosophyDay,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationPhilosophyDay)));
            }

            // Psychology day
            if (dtmNow >= GetPsychologyStart() && dtmNow < GetPsychologyEnd())
            {
                oEvents.Add((
                    strStartPath + "PsychologyDayHeader.jpg",
                    Resources.CelebrationPsychology,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationPsychology)));
            }

            // Reading day
            if (dtmNow >= GetReadingDayStart() && dtmNow < GetReadingDayEnd())
            {
                oEvents.Add((
                    strStartPath + "ReadingDayHeader.jpg",
                    Resources.CelebrationReadingDay,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationReadingDay)));
            }

            // Valentine day
            if (dtmNow >= GetValentineStart() && dtmNow < GetValentineEnd())
            {
                oEvents.Add((
                    strStartPath + "ValentineHeader.jpg",
                    Resources.CelebrationValentine,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationValentine)));
            }

            // World Savings Day
            if (dtmNow >= GetWorldSavingsDayStart() && dtmNow < GetWorldSavingsDayEnd())
            {
                oEvents.Add((
                    strStartPath + "WorldSavingsDayHeader.jpg",
                    Resources.CelebrationSavingsDay,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationSavingsDay)));
            }

            // World Peace Day
            if (dtmNow >= GetWorldPeaceDayStart() && dtmNow < GetWorldPeaceDayEnd())
            {
                oEvents.Add((
                    strStartPath + "WorldPeaceDayHeader.jpg",
                    Resources.CelebrationPeaceDay,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationPeaceDay)));
            }

            // World Dancing Day
            if (dtmNow >= GetWorldDancingDayStart() && dtmNow < GetWorldDancingDayEnd())
            {
                oEvents.Add((
                    strStartPath + "WorldDancingDayHeader.jpg",
                    Resources.CelebrationDancingDay,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationDancingDay)));
            }


            // Olymbic summer games
            if (dtmNow >= GetOlympicSummerStart() && dtmNow < GetOlympicSummerEnd())
            {
                oEvents.Add((
                    strStartPath + "OlympicGamesHeader.jpg",
                    Resources.CelebrationOlympics,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationOlympics)));
            }

            // Olymbic winter games
            if (dtmNow >= GetOlympicWinterStart() && dtmNow < GetOlympicWinterEnd())
            {
                oEvents.Add((
                    strStartPath + "OlympicWinterGamesHeader.jpg",
                    Resources.CelebrationsWinterOlympics,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationsWinterOlympics)));
            }

            // Olymbic winter games
            if (dtmNow >= GetSoccerStart() && dtmNow < GetSoccerEnd())
            {
                oEvents.Add((
                    strStartPath + "SoccerHeader.jpg",
                    Resources.CelebrationSoccer,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationSoccer)));
            }

            // Olymbic winter games
            if (dtmNow >= GetSoccerChampionsStart() && dtmNow < GetSoccerChampionsEnd())
            {
                oEvents.Add((
                    strStartPath + "SoccerHeader.jpg",
                    Resources.CelebrationSoccer,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationSoccer)));
            }


            // Nobel Prize
            if (dtmNow >= GetNobelPrizeStart() && dtmNow < GetNobelPrizeEnd())
            {
                oEvents.Add((
                    strStartPath + "NobelPrizeHeader.jpg",
                    Resources.CelebrationNobelPrize,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationNobelPrize)));

            }

            // Oscar - Header
            if (dtmNow >= GetOscarStart() && dtmNow < GetOscarEnd())
            {
                oEvents.Add((
                    strStartPath + "MovieFestivalHeader.jpg",
                    Resources.CelebrationOscar,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationOscar)));

                if (ReadyToUseImageInjection(strStartPath + "MovieFestivalHeader.jpg"))
                    return;
            }

            // Cannes - Header
            if (dtmNow >= GetCannesStart() && dtmNow < GetCannesEnd())
            {
                oEvents.Add((
                    strStartPath + "MovieFestivalHeader.jpg",
                    Resources.CelebrationCannes,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationCannes)));
            }

            // Berlinale - Header
            if (dtmNow >= GetBerlinaleStart() && dtmNow < GetBerlinaleEnd())
            {
                oEvents.Add((
                    strStartPath + "MovieFestivalHeader.jpg",
                    Resources.CelebrationBerlinale,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationBerlinale)));
            }

            // Durban - Header
            if (dtmNow >= GetDurbanStart() && dtmNow < GetDurbanEnd())
            {
                oEvents.Add((
                    strStartPath + "MovieFestivalHeader.jpg",
                    Resources.CelebrationDurban,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationDurban)));
            }

            // Timkat - Header
            if (dtmNow >= GetTimkatStart() && dtmNow < GetTimkatEnd())
            {
                oEvents.Add((
                    strStartPath + "TimkatHeader.jpg",
                    Resources.CelebrationTimkat,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationTimkat)));
            }

            // Grad Prix de la Chanson - ESC - Header
            if (dtmNow >= GetEscStart() && dtmNow < GetEscEnd())
            {
                oEvents.Add((
                    strStartPath + "EscHeader.jpg",
                    Resources.CelebrationEsc,
                    SearchEngineRouter.GetSearchUrl(Resources.CelebrationEsc)));
            }


            // Festima - Header (African Masks)
            if (dtmNow >= GetFestimaStart() && dtmNow < GetFestimaEnd())
            {
                oEvents.Add((
                    strStartPath + "FestimaHeader.jpg",
                    "FESTIMA Masks",
                    SearchEngineRouter.GetSearchUrl("FESTIMA Masks")));
            }

            // Choose one of the events randomly
            Random oRandom = new Random(DateTime.Now.DayOfYear * 1000 + 
                DateTime.Now.Year * 365000 + DateTime.Now.Millisecond);
            while (oEvents.Count > 0)
            {
                int nEvent = oRandom.Next(oEvents.Count);
                var oEvent = oEvents[nEvent];
                if (ReadyToUseImageInjection(oEvent.Header))
                {
                    // got header, then init the tooltip and the click event handler
                    m_oToolTip = new ToolTip();
                    m_oToolTip.SetToolTip(m_ctlPictureBox, oEvent.ToolTip);
                    m_strUrlForImage = oEvent.Url;
                    m_ctlPictureBox.Click += OnPictureBox_Click;

                    // we alreade have a header, so get out of the function
                    return;
                } else
                {
                    // Couldn't load header? - remove from list and try another one
                    oEvents.RemoveAt(nEvent);
                }

            }


            // If there is no special header, then use default
            ReadyToUseImageInjection(strStartPath + "VokabelTrainerMainHeader.jpg");
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when picture box is clicked
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnPictureBox_Click(object oSender, EventArgs oArgs)
        {
            if (!string.IsNullOrEmpty(m_strUrlForImage))
            {
                System.Diagnostics.Process.Start(m_strUrlForImage);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Enables or disables buttons, depending on the current situation
        /// </summary>
        //===================================================================================================
        public void EnableDisableButtons()
        {
            m_btnNewLanguageFile.Enabled = true;
            m_btnEnterVocabulary.Enabled = ((m_strCurrentPath != null) && m_bModifiable)&&(!"kontinuierlich".Equals(m_strMode));
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
                    m_btnExerciseFirstToSecond.Enabled = m_oTrainingResultsSecondLanguage.Count > 0;
                m_lblReader.Enabled = m_cbxReader.Enabled = 
                    m_oTrainingResultsFirstLanguage.Count > 0 || m_oTrainingResultsSecondLanguage.Count > 0;


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
            m_dlgOpenFileDialog.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag.StartsWith("de"))
                m_dlgOpenFileDialog.DefaultExt = "Vokabeln.xml";

            if (m_dlgOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                m_strCurrentPath = m_dlgOpenFileDialog.FileName;
                LoadLanguageFile(null);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Load the file that has been set in m_strCurrentPath
        /// </summary>
        //===================================================================================================
        private void LoadLanguageFile(int? nNeededStep)
        {
            int nTotalCountCorrectAnswers = 0;
            List<KeyValuePair<string, string>> oCurrentStep = new List<KeyValuePair<string, string>>();

            try
            {
                m_oCurrentVocabularyDoc = new System.Xml.XmlDocument();
                m_oCurrentVocabularyDoc.Load(m_strCurrentPath);

                m_oTrainingResultsFirstLanguage = new SortedDictionary<string, string>();
                m_oTrainingResultsSecondLanguage = new SortedDictionary<string, string>();
                m_oFirstToSecond = new SortedDictionary<string, SortedDictionary<string, bool>>();
                m_oSecondToFirst = new SortedDictionary<string, SortedDictionary<string, bool>>();
                m_oCorrectAnswersFirstLanguage = new SortedDictionary<string, int>();
                m_oCorrectSecondLanguage = new SortedDictionary<string, int>();
                m_nTotalNumberOfErrorsFirstLanguage = 0;
                m_nTotalNumberOfErrorsSecondLanguage = 0;
                m_nStep = 0;
                bool bContinuous = false;

                m_strMode = m_oCurrentVocabularyDoc.SelectSingleNode("/vokabeln/modus")?.InnerText??null;
                if (string.IsNullOrWhiteSpace(m_strMode))
                {
                    m_strMode = "normal";
                }
                if ("kontinuierlich".Equals(m_strMode))
                {
                    bContinuous = true;

                    if (nNeededStep.HasValue)
                    {
                        m_nStep = nNeededStep.Value;
                    }
                    else
                    {
                        // in continuous mode load the training progress information, just to get the information what is
                        // the current step
                        try
                        {
                            System.Xml.XmlDocument oTrainingDoc = new System.Xml.XmlDocument();
                            oTrainingDoc.Load(m_strCurrentPath
                                .Replace("Vokabeln.xml", "Training.xml")
                                .Replace("Vocabulary.xml", "Training.xml"));
                            m_nStep = int.Parse(oTrainingDoc.SelectSingleNode("/training/schritt")?.InnerText ?? "0");
                        }
                        catch (Exception oEx)
                        {
                            // ignore
                        }
                    }
                }

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

                foreach (System.Xml.XmlElement e in m_oCurrentVocabularyDoc.SelectNodes("/vokabeln/vokabel-paar"))
                {
                    bool bContinueLoading = true;
                    int nStep = -1;

                    if (bContinuous)
                    {
                        try
                        {
                            string strStep = e.HasAttribute("schritt") ? e.GetAttribute("schritt") : "0";
                            nStep = int.Parse(strStep);

                            if (nStep > m_nStep)
                                bContinueLoading = false;

                        } catch (Exception oEx)
                        {
                            // ignore
                        }
                    }

                    if (bContinueLoading)
                    {
                        string strFirstLanguage = e.SelectSingleNode("erste-sprache").InnerText;
                        string strSecondLanguage = e.SelectSingleNode("zweite-sprache").InnerText;

                        if (nNeededStep.HasValue && nNeededStep.Value == nStep)
                        {
                            oCurrentStep.Add(new KeyValuePair<string, string>(strFirstLanguage, strSecondLanguage));
                        }

                        if (!m_oTrainingResultsFirstLanguage.ContainsKey(strFirstLanguage))
                        {
                            m_oTrainingResultsFirstLanguage[strFirstLanguage] = "111110";
                            m_nTotalNumberOfErrorsFirstLanguage += 1;
                            m_oFirstToSecond[strFirstLanguage] = new SortedDictionary<string, bool>();
                        }

                        if (!m_oTrainingResultsSecondLanguage.ContainsKey(strSecondLanguage))
                        {
                            m_oTrainingResultsSecondLanguage[strSecondLanguage] = "111110";
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

            }
            catch (Exception oEx)
            {
                NewMessageBox.Show(this, oEx.Message, Properties.Resources.ErrorLoadingXmlFileHeader, 
                    string.Format(Properties.Resources.ErrorLoadingXmlFileMessage, oEx.Message));

                m_strCurrentPath = null;
                m_oCurrentVocabularyDoc = null;

                m_oTrainingResultsFirstLanguage = new SortedDictionary<string, string>();
                m_oTrainingResultsSecondLanguage = new SortedDictionary<string, string>();
                m_oFirstToSecond = new SortedDictionary<string, SortedDictionary<string, bool>>();
                m_oSecondToFirst = new SortedDictionary<string, SortedDictionary<string, bool>>();
                m_oCorrectAnswersFirstLanguage = new SortedDictionary<string, int>();
                m_oCorrectSecondLanguage = new SortedDictionary<string, int>();
                m_nTotalNumberOfErrorsFirstLanguage = 0;
                m_nTotalNumberOfErrorsSecondLanguage = 0;
                m_nStep = 0;
                m_strMode = "";
            }

            // continue with loading of training file, after vocabulary has been loaded
            if (m_strCurrentPath != null)
            {
                try
                {

                    string strCurrentPath = m_strCurrentPath.Replace(".Vokabeln.xml", ".Training.xml")
                        .Replace("Vocabulary.xml", ".Training.xml");

                    System.IO.FileInfo fi = new System.IO.FileInfo(strCurrentPath);
                    if (fi.Exists)
                    {
                        System.Xml.XmlDocument oCurrentDoc = new System.Xml.XmlDocument();
                        oCurrentDoc.Load(strCurrentPath);

                        foreach (System.Xml.XmlElement e in oCurrentDoc.SelectNodes("/training/erste-sprache"))
                        {
                            string strTrainingProgress = e.SelectSingleNode("training-vorgeschichte").InnerText;

                            if (strTrainingProgress.Length > 6)
                            {
                                strTrainingProgress = strTrainingProgress.Substring(0, 6);
                            }
                            else
                            {
                                while (strTrainingProgress.Length < 6)
                                    strTrainingProgress = strTrainingProgress + "1";
                            }

                            if (m_oTrainingResultsFirstLanguage.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                            {
                                m_nTotalNumberOfErrorsFirstLanguage += strTrainingProgress.Length - strTrainingProgress.Replace("0", "").Length
                                        - (m_oTrainingResultsFirstLanguage[e.SelectSingleNode("vokabel").InnerText].Length -
                                            m_oTrainingResultsFirstLanguage[e.SelectSingleNode("vokabel").InnerText].Replace("0", "").Length);

                                m_oTrainingResultsFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = strTrainingProgress;

                                if (!m_oFirstToSecond.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                                    m_oFirstToSecond[e.SelectSingleNode("vokabel").InnerText] = new SortedDictionary<string, bool>();

                                System.Xml.XmlNode oCorrectAnswersNode = e.SelectSingleNode("richtige-antworten");
                                if (oCorrectAnswersNode != null)
                                {
                                    string strCorrectAnswers = oCorrectAnswersNode.InnerText;
                                    int nCorrectAnswers = 0;

                                    if (!int.TryParse(strCorrectAnswers, out nCorrectAnswers))
                                    {
                                        nCorrectAnswers = 0;
                                    }
                                    else
                                    {
                                        if (nCorrectAnswers < 0)
                                            nCorrectAnswers = 0;
                                    }

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

                            if (m_oTrainingResultsSecondLanguage.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                            {
                                m_nTotalNumberOfErrorsSecondLanguage += strTrainingProgress.Length - strTrainingProgress.Replace("0", "").Length
                                    - (m_oTrainingResultsSecondLanguage[e.SelectSingleNode("vokabel").InnerText].Length -
                                        m_oTrainingResultsSecondLanguage[e.SelectSingleNode("vokabel").InnerText].Replace("0", "").Length);

                                m_oTrainingResultsSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = strTrainingProgress;

                                if (!m_oSecondToFirst.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                                    m_oSecondToFirst[e.SelectSingleNode("vokabel").InnerText] = new SortedDictionary<string, bool>();

                                System.Xml.XmlNode oCorrectAnswersNode = e.SelectSingleNode("richtige-antworten");
                                if (oCorrectAnswersNode != null)
                                {
                                    string strCorrectAnswers = oCorrectAnswersNode.InnerText;
                                    int nCorrectAnswers = 0;

                                    if (!int.TryParse(strCorrectAnswers, out nCorrectAnswers))
                                    {
                                        nCorrectAnswers = 0;
                                    }
                                    else
                                    {
                                        if (nCorrectAnswers < 0)
                                            nCorrectAnswers = 0;
                                    }

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
                                DateTime dtmStatsDate = DateTime.ParseExact(e.SelectSingleNode("datum").InnerText,
                                    "yyyy-MM-dd",
                                    CultureInfo.InvariantCulture);
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
                catch (Exception oEx)
                {
                    NewMessageBox.Show(this, oEx.Message, Properties.Resources.ErrorLoadingTrainingFileHeader,
                        string.Format(Properties.Resources.ErrorLoadingTrainingFileMessage, oEx.Message));

                    m_strCurrentPath = null;
                    m_oCurrentVocabularyDoc = null;

                    m_oTrainingResultsFirstLanguage = new SortedDictionary<string, string>();
                    m_oTrainingResultsSecondLanguage = new SortedDictionary<string, string>();
                    m_oFirstToSecond = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    m_oSecondToFirst = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    m_oCorrectAnswersFirstLanguage = new SortedDictionary<string, int>();
                    m_oCorrectSecondLanguage = new SortedDictionary<string, int>();
                    m_nTotalNumberOfErrorsFirstLanguage = 0;
                    m_nTotalNumberOfErrorsSecondLanguage = 0;
                    m_nStep = 0;
                    m_strMode = "";
                }
            }

            if (nNeededStep.HasValue && oCurrentStep!=null && oCurrentStep.Count>0)
            {
                using (NewWordsInCourseForm oForm = new NewWordsInCourseForm(
                    Resources.NewVocabularyInCourseHeader, m_strFirstLanguage, 
                    m_strSecondLanguage, oCurrentStep))
                {
                    oForm.ShowDialog(this);
                }
            } else if (nNeededStep.HasValue)
            {
                MessageBox.Show(this, Resources.CourseFinishedText, 
                    Resources.CourseFinishedHeader, 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Asterisk);

                // switch back to "normal", so user can enter his own vocabulary, in case licence allows it.
                m_strMode = "normal";
                if (m_bModifiable)
                    SaveVokabulary();
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
            using (NewLanguageFile oNewForm = new NewLanguageFile())
            {
                if (oNewForm.ShowDialog(this) == DialogResult.OK)
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

                    // let's see, if there is a predefined course for this language pair
                    string strCode1 = Program.LanguageCodeFromName(oNewForm.m_tbxFirstLanguage.Text);
                    string strCode2 = Program.LanguageCodeFromName(oNewForm.m_tbxSecondLanguage.Text);
                    string strFilePath = null;
                    string strTestedPath;

                    if (!string.IsNullOrEmpty(strCode1) && !string.IsNullOrEmpty(strCode2))
                    {
                        string strBaseDir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Books");

                        strTestedPath = Path.Combine(strBaseDir, strCode1.Replace("-", "") + "-" + strCode2.Replace("-", "") + ".xml");
                        if (strFilePath == null && File.Exists(strTestedPath))
                        {
                            strFilePath = strTestedPath;
                        }

                        strTestedPath = Path.Combine(strBaseDir, strCode2.Replace("-", "") + "-" + strCode1.Replace("-", "") + ".xml");
                        if (strFilePath == null && File.Exists(strTestedPath))
                        {
                            strFilePath = strTestedPath;
                        }

                        strTestedPath = Path.Combine(strBaseDir, 
                            strCode1.Substring(0, strCode1.IndexOf('-')) + "-" + strCode2.Replace("-", "") + ".xml");
                        if (strFilePath == null && File.Exists(strTestedPath))
                        {
                            strFilePath = strTestedPath;
                        }


                        strTestedPath = Path.Combine(strBaseDir,
                            strCode2.Substring(0, strCode1.IndexOf('-')) + "-" + strCode1.Replace("-", "") + ".xml");
                        if (strFilePath == null && File.Exists(strTestedPath))
                        {
                            strFilePath = strTestedPath;
                        }


                        strTestedPath = Path.Combine(strBaseDir,
                            strCode1.Substring(0, strCode1.IndexOf('-')) + "-" +
                            strCode2.Substring(0, strCode2.IndexOf('-')) + ".xml");
                        if (strFilePath == null && File.Exists(strTestedPath))
                        {
                            strFilePath = strTestedPath;
                        }

                        strTestedPath = Path.Combine(strBaseDir,
                            strCode2.Substring(0, strCode2.IndexOf('-')) + "-" +
                            strCode1.Substring(0, strCode1.IndexOf('-')) + ".xml");
                        if (strFilePath == null && File.Exists(strTestedPath))
                        {
                            strFilePath = strTestedPath;
                        }
                    }

                    bool bPredefined = false;
                    if (strFilePath != null)
                    {
                        if (strFilePath.EndsWith("en-de.xml") ||
                            strFilePath.EndsWith("en-ru.xml") ||
                            strFilePath.EndsWith("de-ru.xml"))
                        {
                            if (DialogResult.Yes ==
                                MessageBox.Show(Resources.PredefinedVocabularyBook, 
                                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                bPredefined = true;
                            }
                        }
                        else
                        {
                            if (DialogResult.Yes ==
                                MessageBox.Show(Resources.ExperimentalVocabularyBook, 
                                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                MessageBox.Show(Resources.ExperimentalThanks, 
                                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                bPredefined = true;
                            }
                        }

                    }

                    string strNewPath;


                    strNewPath = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        oNewForm.m_tbxFirstLanguage.Text + "-" + oNewForm.m_tbxSecondLanguage.Text);

                    if (bPredefined)
                    {
                        if (System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag.StartsWith("de"))
                            strNewPath += ".Kurs.Vokabeln.xml";
                        else
                            strNewPath += ".Course.Vocabulary.xml";
                    }
                    else
                    {
                        if (System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag.StartsWith("de"))
                            strNewPath += ".Vokabeln.xml";
                        else
                            strNewPath += ".Vocabulary.xml";
                    }


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
                    if (strFilePath != null && bPredefined)
                    {
                        File.Copy(strFilePath, strNewPath, true);
                        
                        LoadLanguageFile(1);
                    }
                    else
                    {
                        m_strCurrentPath = m_dlgOpenFileDialog.FileName;
                        m_oCurrentVocabularyDoc = new System.Xml.XmlDocument();
                        m_oCurrentVocabularyDoc.PreserveWhitespace = false;
                        m_bIsModifiableFlagForXml = oNewForm.m_chkLanguageFileModifiable.Checked;
                        m_bModifiable = true;
                        m_bSavePossible = true;
                        if (oNewForm.m_chkLanguageFileUnderGPL2.Checked)
                        {
                            m_strLicense = "Copyright (C) " + DateTime.Now.Year + " " +
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
                            m_strLicense = "Copyright (C) " + DateTime.Now.Year + " " +
                                Environment.GetEnvironmentVariable("USERNAME") + ", all rights reserved";
                        }

                        m_oCurrentVocabularyDoc.LoadXml("<?xml version=\"1.0\" ?>\r\n<vokabeln>\r\n  <modus>normal</modus>\r\n  <erste-sprache-name>" +
                            oNewForm.m_tbxFirstLanguage.Text + "</erste-sprache-name>\r\n" +
                            "  <zweite-sprache-name>" + oNewForm.m_tbxSecondLanguage.Text + "</zweite-sprache-name>\r\n" +
                            "  <erste-sprache-rtl>" + (oNewForm.m_chkFirstLanguageRTL.Checked ? "ja" : "nein") + "</erste-sprache-rtl>\r\n" +
                            "  <zweite-sprache-rtl>" + (oNewForm.m_chkSecondLanguageRTL.Checked ? "ja" : "nein") + "</zweite-sprache-rtl>\r\n" +
                            "<lizenz><modifikationen>" + (m_bIsModifiableFlagForXml ? "Unter Lizenzbedingungen" :
                            "Keine neuen Wörter und keine Lizenzänderungen") + "</modifikationen><text>" + m_strLicense + "</text></lizenz></vokabeln>\r\n");
                        m_oCurrentVocabularyDoc.Save(m_strCurrentPath);

                        m_bFirstLanguageRtl = oNewForm.m_chkFirstLanguageRTL.Checked;
                        m_bSecondLanguageRtl = oNewForm.m_chkSecondLanguageRTL.Checked;

                        m_oTrainingResultsFirstLanguage = new SortedDictionary<string, string>();
                        m_oTrainingResultsSecondLanguage = new SortedDictionary<string, string>();
                        m_oFirstToSecond = new SortedDictionary<string, SortedDictionary<string, bool>>();
                        m_oSecondToFirst = new SortedDictionary<string, SortedDictionary<string, bool>>();
                        m_oCorrectAnswersFirstLanguage = new SortedDictionary<string, int>();
                        m_oCorrectSecondLanguage = new SortedDictionary<string, int>();
                        m_nTotalNumberOfErrorsFirstLanguage = 0;
                        m_nTotalNumberOfErrorsSecondLanguage = 0;
                        m_strFirstLanguage = oNewForm.m_tbxFirstLanguage.Text;
                        m_strSecondLanguage = oNewForm.m_tbxSecondLanguage.Text;
                    }
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

                                        if (!m_oTrainingResultsSecondLanguage.ContainsKey(s2))
                                        {
                                            m_nTotalNumberOfErrorsSecondLanguage += 1;
                                            m_oTrainingResultsSecondLanguage[s2] = "111110";

                                            m_oSecondToFirst[s2] = new SortedDictionary<string, bool>();
                                        }
                                        else
                                        {
                                            if ("1".Equals(m_oTrainingResultsSecondLanguage[s2].Substring(5, 1)))
                                            {
                                                m_nTotalNumberOfErrorsSecondLanguage += 1;
                                                m_oTrainingResultsSecondLanguage[s2] = 
                                                    m_oTrainingResultsSecondLanguage[s2].Substring(0, 5) + "0"; 
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

                                        if (!m_oTrainingResultsSecondLanguage.ContainsKey(s2))
                                        {
                                            m_nTotalNumberOfErrorsSecondLanguage += 1;
                                            m_oTrainingResultsSecondLanguage[s2] = "111110";

                                            m_oSecondToFirst[s2] = new SortedDictionary<string, bool>();
                                        }
                                        else
                                        {
                                            if ("1".Equals(m_oTrainingResultsSecondLanguage[s2].Substring(5, 1)))
                                            {
                                                m_nTotalNumberOfErrorsSecondLanguage += 1;
                                                m_oTrainingResultsSecondLanguage[s2] = 
                                                    m_oTrainingResultsSecondLanguage[s2].Substring(0, 5) + "0"; 
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

                int nSelectedError = nRnd2 % (m_nTotalNumberOfErrorsSecondLanguage + m_oTrainingResultsSecondLanguage.Count);

                m_bSkipLast = m_oTrainingResultsSecondLanguage.Count > 10;

                int nWordIndex = -1;
                using (SortedDictionary<string,string>.ValueCollection.Enumerator values = 
                    m_oTrainingResultsSecondLanguage.Values.GetEnumerator())
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

                    m_bSkipLast = m_oTrainingResultsSecondLanguage.Count > 20;

                    int nWordIndex = -1;
                    using (SortedDictionary<string, string>.ValueCollection.Enumerator values = 
                        m_oTrainingResultsSecondLanguage.Values.GetEnumerator())
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

                            m_bSkipLast = m_oTrainingResultsSecondLanguage.Count > 10;

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

                            m_bSkipLast = m_oTrainingResultsSecondLanguage.Count > 10;

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
                foreach(string s in m_oTrainingResultsSecondLanguage.Values)
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
                    foreach (string s in m_oTrainingResultsSecondLanguage.Values)
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
            bool bMore = false;
            foreach (KeyValuePair<string,string> oPair in m_oTrainingResultsSecondLanguage)
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


                    using (WordTest oTestDlg = new WordTest(m_nStep>0))
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
                                bMore = false;
                                break;
                            case DialogResult.OK:
                                bRepeat = false;
                                bVerify = true;
                                bMore = false;
                                break;
                            case DialogResult.Yes:
                                bRepeat = true;
                                bVerify = true;
                                bMore = true;
                                break;
                            default:
                                bRepeat = false;
                                bVerify = false;
                                bMore = false;
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

                                if (bMore)
                                {
                                    SaveTrainingProgress();
                                    LoadLanguageFile(m_nStep + 1);
                                }
                            }
                            else
                            {
                                if (m_cbxReader.SelectedIndex == 1 || m_cbxReader.SelectedIndex == 2)
                                    Speaker.Say(m_strFirstLanguage, oTestDlg.m_tbxAskedTranslation.Text.Trim(), 
                                        true, m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);

                                RememberResultSecondLanguage(oPair.Key, true);
                                if (bMore)
                                {
                                    SaveTrainingProgress();
                                    LoadLanguageFile(m_nStep + 1);
                                }
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
            string strPrevResults = m_oTrainingResultsSecondLanguage[strWord];
            string strNewResults = (bCorrect ? "1" : "0") + 
                strPrevResults.Substring(0, strPrevResults.Length < 5 ? strPrevResults.Length : 5);
            m_oTrainingResultsSecondLanguage[strWord] = strNewResults;
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
                        "                              ",
                        "                             ",
                        "                            ",
                        "                           ",
                        "                          ",
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
                    w.WriteLine("  <!-- Modus: kontinuierlich oder normal -->");
                    w.WriteLine("  <modus>{0}</modus>", m_strMode);
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
                            if (oFirst.Key.Trim().Length >= spaces.Length)
                                w.WriteLine("  <vokabel-paar><erste-sprache>{0}</erste-sprache><zweite-sprache>{1}</zweite-sprache></vokabel-paar>", oFirst.Key.Trim(), strSecond.Trim());
                            else
                                w.WriteLine("  <vokabel-paar><erste-sprache>{0}</erste-sprache>{2}<zweite-sprache>{1}</zweite-sprache></vokabel-paar>", oFirst.Key.Trim(), strSecond.Trim(), spaces[oFirst.Key.Trim().Length]);

                    w.WriteLine();
                    w.WriteLine("</vokabeln>");
                    w.Close();
                }

                // reload the saved document as XML, just to ensure that the structure is OK
                System.Xml.XmlDocument oDoc = new System.Xml.XmlDocument();
                oDoc.Load(m_strCurrentPath);
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
                    if ("kontinuierlich".Equals(m_strMode))
                    {
                        w.WriteLine("  <!-- Im kontinuierlichen Modus der aktuelle Schritt/Übung an dem das Training sich befindet -->");
                        w.WriteLine("  <schritt>"+m_nStep+"</schritt>");
                        w.WriteLine();
                    }
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
                    foreach (KeyValuePair<string, string> training in m_oTrainingResultsSecondLanguage)
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
                                dtmGraphPoint.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                                m_oWordsGraphData[dtmGraphPoint],
                                m_oTotalGraphData[dtmGraphPoint] - m_oWordsGraphData[dtmGraphPoint],
                                m_oWordsGraphData[dtmGraphPoint] - m_oLearnedWordsGraphData[dtmGraphPoint]);
                        }
                    }

                    w.WriteLine("</training>");
                    w.Close();
                }

                // reload the saved document as XML, just to ensure that the structure is OK
                System.Xml.XmlDocument oDoc = new System.Xml.XmlDocument();
                oDoc.Load(strCurrentPath);
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
        private bool TrainFirstToSecondLanguage(
            int nIndex
            )
        {
            bool bRepeat = false;
            bool bVerify = false;
            bool bMore = false;
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


                    using (WordTest test = new WordTest(m_nStep > 0))
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
                                bMore = false;
                                break;
                            case DialogResult.OK:
                                bRepeat = false;
                                bVerify = true;
                                bMore = false;
                                break;
                            case DialogResult.Yes:
                                bRepeat = true;
                                bVerify = true;
                                bMore = true;
                                break;
                            default:
                                bRepeat = false;
                                bVerify = false;
                                bMore = false;
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

                                        if (m_oTrainingResultsSecondLanguage.ContainsKey(s))
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
                                            if (m_oTrainingResultsSecondLanguage.ContainsKey(s))
                                                RememberResultSecondLanguage(s, false);
                                        }
                                    }


                                if (missing.Count > 0)
                                    NewMessageBox.Show(this, strErrorMessage, Properties.Resources.Mistake, null);
                                else
                                    MessageBox.Show(strErrorMessage, Properties.Resources.Mistake, 
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                                RememberResultFirstLanguage(pair.Key, false);

                                if (bMore)
                                {
                                    SaveTrainingProgress();
                                    LoadLanguageFile(m_nStep + 1);
                                }
                            }
                            else
                            {
                                if (m_cbxReader.SelectedIndex == 1 || m_cbxReader.SelectedIndex == 3)
                                    Speaker.Say(m_strSecondLanguage, test.m_tbxAskedTranslation.Text.Trim(), 
                                        true, m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);

                                RememberResultFirstLanguage(pair.Key, true);

                                if (bMore)
                                {
                                    SaveTrainingProgress();
                                    LoadLanguageFile(m_nStep + 1);
                                }
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

                    bRepeat = TrainFirstToSecondLanguage(nWordIndex);
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

                        bRepeat = TrainFirstToSecondLanguage(nWordIndex);
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

                            bRepeat = TrainFirstToSecondLanguage(nWordIndex);
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

                            bRepeat = TrainFirstToSecondLanguage(nWordIndex);
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

                        bRepeat = TrainFirstToSecondLanguage(nBestIndex);
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
        private void OnShowLicence_LinkClicked(object oSender, LinkLabelLinkClickedEventArgs oArgs)
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
        private void OnShowAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (About oAboutForm = new About())
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
        private void OnShowDesktopKeyboard_Click(object oSender, EventArgs oArgs)
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
        private void OnDownloadESpeak_LinkClicked(object oSender, LinkLabelLinkClickedEventArgs oArgs)
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
        private void OnUseESpeak_CheckedChanged(object oSender, EventArgs oArgs)
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
        private void OnSearchESpeak_Click(object oSender, EventArgs oArgs)
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
                    oResult.Add(@"https://www.youtube.com/@Der_Postillon");
                    oResult.Add(@"https://www.youtube.com/results?search_query=ganzer+film+deutsch");
                    oResult.Add(@"https://www.kraehseite.de/");
                    oResult.Add(@"https://www.welt.de/satire/");
                    oResult.Add(@"https://www.radio.de/country/germany");
                    oResult.Add(@"https://de.wikipedia.org/wiki/Spezial:Zuf%C3%A4llige_Seite");
                    oResult.Add(@"https://www.schlagerradio.de/");
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
                    oResult.Add(@"https://www.youtube.com/@LadyTamara");

                    // sometimes also add the critical book
                    if (m_oRnd.Next(10) == 7)
                    {
                        oResult.Add(@"https://www.amazon.de/s?k=die+kunst+der+ehezerr%C3%BCttung");
                    }

                    // for language speakers that aren't muslim add visionary christian songs
                    switch (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "he":
                        case "fr":
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
                            oResult.Add(@"https://www.youtube.com/@BibelA.I/videos");
                            break;
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
                            oResult.Add(@"https://www.youtube.com/@Bible-A.I/videos");
                            //https://www.youtube.com/results?search_query=Agence+pour+le+ciel+fran%C3%A7aise
                            //https://www.youtube.com/results?search_query=DANS+LE+CIEL+AVEC+JESUS.+Du+ciel+bient%C3%B4t.+Jesus+va+faire+son+apparition.+Es+tu+prets%3F+HOSANA+BENI+SOIT+L%27ETERNEL.+UN+GRAND+JOUR+DANS+LE+CIEL.+Ce+que+sa+bouche+a+dit%2C+Sa+main+I%E2%80%99accomplira+Alleluia%21+Alleluia%21+Car+iI+est+notre+Dieu.+JESUS+ROIS+DES+ROIS
                            break;
                    }
                    

                    // for german speakers: Witch Huckla / French
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
                    oResult.Add(@"https://www.amazon.co.uk/s?k=meet+the+fockers");
                    oResult.Add(@"https://www.amazon.co.uk/s?k=50+great+short+stories");

                    // also sometimes add the business dynamics that is probably useful only 
                    // for a small percentage of users
                    if (m_oRnd.Next(10) == 7)
                    {
                        oResult.Add(@"https://www.amazon.co.uk/s?k=business+dynamics+sterman");
                    }

                    // for german speakers: Witch Huckla / English
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
                    oResult.Add(@"https://www.youtube.com/results?search_query=%D0%BC%D0%B8%D1%82%D1%8F%D0%B9+%D0%B8%D0%B7+%D0%BA%D1%83%D1%87%D1%83%D0%B3%D1%83%D1%80");
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
                    oResult.Add(@"https://www.bbc.com/zhongwen/simp");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%E5%AD%97%E5%B9%95%E4%BB%98%E3%81%8D%E3%81%AE%E4%B8%AD%E5%9B%BD%E6%98%A0%E7%94%BB");
                    oResult.Add(@"https://www.youtube.com/results?search_query=%E4%B8%AD%E5%9B%BD%E7%94%B5%E5%BD%B1%E6%9C%89%E5%AD%97%E5%B9%95");
                    oResult.Add(@"https://www.youtube.com/@RainbowJuniorCN");

                    // for language speakers that aren't muslim add christian songs (sorry, haven't found visionary content)
                    // and only traditional chinese, not simplified
                    switch (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
                    {
                        case "he":
                        case "fr":
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
                        case "de":
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
                            oResult.Add(@"https://www.youtube.com/@W247/videos");
                            oResult.Add(@"https://www.youtube.com/results?search_query=%E7%A5%A2%E7%9A%84%E8%89%AF%E5%96%84%E4%BD%9C%E4%B8%BA");
                            break;
                    }

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

                    if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("en"))
                    {
                        oResult.Add(@"https://www.youtube.com/results?search_query=timeless+hebrew+tunes");
                    }
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
        private void OnDontLearnAquire_LinkClicked(object oSender, LinkLabelLinkClickedEventArgs oArgs)
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
        private void OnOsLanguageAndKeyboardSettings_Click(object oSender, EventArgs oArgs)
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
        /// Tooltip for celebrations
        /// </summary>
        private ToolTip m_oToolTip;
        //===================================================================================================
        /// <summary>
        /// Url to call when image is clicked
        /// </summary>
        private string m_strUrlForImage;

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
        static DateTime GetEasterSunday(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            int nYear = dtmTestedDate.Value.Year;

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
        static DateTime GetEasterStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return GetEasterSunday(dtmTestedDate).AddDays(-3);
        }


        //===================================================================================================
        /// <summary>
        /// Calculates the day of eastern for current year
        /// </summary>
        /// <returns>The day of eastern sunday</returns>
        //===================================================================================================
        static DateTime GetEasterEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return GetEasterSunday(dtmTestedDate).AddDays(+2);
        }



        //===================================================================================================
        /// <summary>
        /// Gets approximate ramadan start data
        /// </summary>
        /// <returns>The start or ramadan heading</returns>
        //===================================================================================================
        static DateTime GetRamadanStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            // Initialize the UmAlQuraCalendar (Islamic Calendar used in Saudi Arabia)
            Calendar umAlQura = new UmAlQuraCalendar();

            // Month of Ramadan is the 9th month
            const int nRamadanMonth = 9;
            // Ramadan starts on the 1st day of the month
            const int nStartDay = 1;

            // ... and find out which Islamic year current date falls into
            int nApproxIslamicYear = umAlQura.GetYear(dtmTestedDate.Value);


            // This calculates the Gregorian date that corresponds to the 1st day of the 9th month 
            // in the calculated Islamic year.
            DateTime dtmRamadanDateGregorian = umAlQura.ToDateTime(
                nApproxIslamicYear,      // Islamic Year
                nRamadanMonth,           // Islamic Month (12)
                nStartDay,               // Islamic Day (1)
                0, 0, 0, 0               // Time component
            );

            // Add 5 days, so there are less collisions with other lunisolar calendars
            return dtmRamadanDateGregorian.AddDays(5);
        }




        //===================================================================================================
        /// <summary>
        /// Gets a date for end of showing ramadan header
        /// </summary>
        /// <returns>5 days after start of ramadan header</returns>
        //===================================================================================================
        static DateTime GetRamadanEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return GetRamadanStart().AddDays(5);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of christmas header
        /// </summary>
        //===================================================================================================
        static DateTime GetChristmasStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 12, 22);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of christmas header
        /// </summary>
        //===================================================================================================
        static DateTime GetChristmasEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 12, 27);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of orthodox christmas header
        /// </summary>
        //===================================================================================================
        static DateTime GetOrthodoxChristmasStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 1, 5);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of orthodox christmas header
        /// </summary>
        //===================================================================================================
        static DateTime GetOrthodoxChristmasEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 1, 9);
        }

        //===================================================================================================
        /// <summary>
        /// The New Years eve header start date
        /// </summary>
        /// <returns>Last day of the year</returns>
        //===================================================================================================
        static DateTime GetNewYearStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 12, 31);
        }

        //===================================================================================================
        /// <summary>
        /// The New Years eve header end date
        /// </summary>
        /// <returns>Last day of the year</returns>
        //===================================================================================================
        static DateTime GetNewYearEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 1, 2);
        }


        //===================================================================================================
        /// <summary>
        /// Estimate Diwali dates after known data
        /// </summary>
        /// <returns>The diwali start date for current year</returns>
        //===================================================================================================
        public static DateTime GetDiwaliStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            int nYear = dtmTestedDate.Value.Year;

            if (s_oDiwaliDates.ContainsKey(nYear))
            {
                return s_oDiwaliDates[nYear].AddDays(-2);
            }
            else
            {
                /*
                // Estimate based on past trends (Diwali usually falls between October and November)
                int nEstimatedMonth = (nYear % 3 == 0) ? 10 : 11; // Alternating between Oct and Nov
                int nEstimatedDay = 20 + (nYear % 5); // Rough day estimation
                return new DateTime(nYear, nEstimatedMonth, nEstimatedDay).AddDays(-2);
                */
                // Estimate based on Rosh Hashanah: 41 day after Rosh Hashanah
                return GetRoshHashanahStart().AddDays(41);
            }
        }


        //===================================================================================================
        /// <summary>
        /// Estimate Diwali dates after known data
        /// </summary>
        /// <returns>The diwali end date for current year</returns>
        //===================================================================================================
        public static DateTime GetDiwaliEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return GetDiwaliStart(dtmTestedDate).AddDays(5);
        }


        //===================================================================================================
        /// <summary>
        /// Estimates the chinese new year dates
        /// </summary>
        /// <returns>The start of chinese new year header</returns>
        //===================================================================================================
        public static DateTime GetChineseNewYearStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            ChineseLunisolarCalendar oChineseCalendar = new ChineseLunisolarCalendar();

            // The Chinese New Year is always the first day of the first month.
            // We need to determine which Chinese year starts within our target Gregorian year.

            int nChineseYear = oChineseCalendar.GetYear(new DateTime(dtmTestedDate.Value.Year, 3, 1));
            DateTime dtmChineseNewYearDate = oChineseCalendar.ToDateTime(nChineseYear, 1, 1, 0, 0, 0, 0);

            return dtmChineseNewYearDate;
        }


        //===================================================================================================
        /// <summary>
        /// Estimates the chinese new year dates
        /// </summary>
        /// <returns>The start of chinese new year header</returns>
        //===================================================================================================
        public static DateTime GetChineseNewYearEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return GetChineseNewYearStart(dtmTestedDate).AddDays(5);
        }


        //===================================================================================================
        /// <summary>
        /// Returns the beginning of Hajj header start
        /// </summary>
        /// <returns>Start of Hajj header</returns>
        //===================================================================================================
        static DateTime GetHajjStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            // Initialize the UmAlQuraCalendar (Islamic Calendar used in Saudi Arabia)
            Calendar umAlQura = new UmAlQuraCalendar();

            // Hajj month is Dhu al-Hijjah (the 12th month)
            const int nDhuAlHijjah = 12;
            // Hajj peak day (Arafah day) is the 9th day of the month
            const int nDayOfHajj = 9;

            // ... and find out which Islamic year current date falls into
            int nApproxIslamicYear = umAlQura.GetYear(dtmTestedDate.Value);

            // Calculate the Gregorian DateTime for the 9th of Dhu al-Hijjah

            // This calculates the Gregorian date that corresponds to the 9th day of the 12th month 
            // in the calculated Islamic year.
            DateTime dtmHajjDateGregorian = umAlQura.ToDateTime(
                nApproxIslamicYear,      // Islamic Year
                nDhuAlHijjah,            // Islamic Month (12)
                nDayOfHajj,              // Islamic Day (9)
                0, 0, 0, 0               // Time component
            );

            // start one day before
            return dtmHajjDateGregorian.AddDays(-1);
        }


        //===================================================================================================
        /// <summary>
        /// Returns the end of Hajj Header 
        /// </summary>
        /// <returns>End of Hajj header</returns>
        //===================================================================================================
        static DateTime GetHajjEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return GetHajjStart(dtmTestedDate).AddDays(4);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of Halloween
        /// </summary>
        //===================================================================================================
        static DateTime GetHalloweenStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 10, 28);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of Halloween header
        /// </summary>
        //===================================================================================================
        static DateTime GetHalloweenEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 11, 2);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of children and Buddha header
        /// </summary>
        //===================================================================================================
        static DateTime GetChildrenAndBuddhaStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 5, 2);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of children and Buddha header
        /// </summary>
        //===================================================================================================
        static DateTime GetChildrenAndBuddhaEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 5, 6);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of science day header
        /// </summary>
        //===================================================================================================
        static DateTime GetScienceStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 11, 7);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of science header
        /// </summary>
        //===================================================================================================
        static DateTime GetScienceEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 11, 11);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the beginning of philosophy header
        /// </summary>
        //===================================================================================================
        static DateTime GetPhilosophyStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return GetPhilosophyEnd(dtmTestedDate).AddDays(-5);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of philosophy header
        /// </summary>
        //===================================================================================================
        static DateTime GetPhilosophyEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            int nThursdayCount = 0;
            for (int nDay = 1; nDay<=30; ++nDay)
            {
                DateTime dtmNovemberDay = new DateTime(dtmTestedDate.Value.Year, 11, nDay);
                if (dtmNovemberDay.DayOfWeek == DayOfWeek.Thursday)
                {
                    ++nThursdayCount;
                    if (nThursdayCount == 3)
                    {
                        return dtmNovemberDay.AddDays(1);
                    }
                }
            }

            // should never be reached, but c# compiler doesn't recognize it.
            return new DateTime(dtmTestedDate.Value.Year, 11, 21);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of psychology header
        /// </summary>
        //===================================================================================================
        static DateTime GetPsychologyStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 10, 7);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of psychology headerТ
        /// </summary>
        //===================================================================================================
        static DateTime GetPsychologyEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 10, 11);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of reading day header
        /// </summary>
        //===================================================================================================
        static DateTime GetReadingDayStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 4, 20);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of reading day header
        /// </summary>
        //===================================================================================================
        static DateTime GetReadingDayEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 4, 24);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the beginning of valentine header
        /// </summary>
        //===================================================================================================
        static DateTime GetValentineStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 2, 13);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of valentine header
        /// </summary>
        //===================================================================================================
        static DateTime GetValentineEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 2, 15);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the beginning of world savings day header
        /// </summary>
        //===================================================================================================
        static DateTime GetWorldSavingsDayStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 10, 24);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of world savings day header
        /// </summary>
        //===================================================================================================
        static DateTime GetWorldSavingsDayEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 10, 28);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of world dancing day header
        /// </summary>
        //===================================================================================================
        static DateTime GetWorldDancingDayStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 4, 25);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of world dancing day header
        /// </summary>
        //===================================================================================================
        static DateTime GetWorldDancingDayEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 4, 30);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of world dancing day header
        /// </summary>
        //===================================================================================================
        static DateTime GetWorldPeaceDayStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 9, 18);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of world dancing day header
        /// </summary>
        //===================================================================================================
        static DateTime GetWorldPeaceDayEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 9, 22);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of olympic summer gammes header
        /// </summary>
        //===================================================================================================
        static DateTime GetOlympicSummerStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year / 4 * 4, 8, 3);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of olympic summer gammes header
        /// </summary>
        //===================================================================================================
        static DateTime GetOlympicSummerEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year / 4 * 4, 8, 9);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of olympic winter gammes header
        /// </summary>
        //===================================================================================================
        static DateTime GetOlympicWinterStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year / 4 * 4 + 2, 2, 12);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of olympic winter gammes header
        /// </summary>
        //===================================================================================================
        static DateTime GetOlympicWinterEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year / 4 * 4 + 2, 2, 20);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of olympic winter gammes header
        /// </summary>
        //===================================================================================================
        static DateTime GetSoccerStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year / 4 * 4 + 2, 6, 14);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of olympic winter gammes header
        /// </summary>
        //===================================================================================================
        static DateTime GetSoccerEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year / 4 * 4 + 2, 6, 20);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the beginning of olympic winter gammes header
        /// </summary>
        //===================================================================================================
        static DateTime GetSoccerChampionsStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 3, 11);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of olympic winter gammes header
        /// </summary>
        //===================================================================================================
        static DateTime GetSoccerChampionsEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 3, 16);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the beginning of nobel prize header
        /// </summary>
        //===================================================================================================
        static DateTime GetNobelPrizeStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 10, 3);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the ending of nobel prize header
        /// </summary>
        //===================================================================================================
        static DateTime GetNobelPrizeEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 10, 07);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning Oscar header
        /// </summary>
        //===================================================================================================
        static DateTime GetOscarStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 3, 1);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the end of Oscar header
        /// </summary>
        //===================================================================================================
        static DateTime GetOscarEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 3, 3);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning Cannes header
        /// </summary>
        //===================================================================================================
        static DateTime GetCannesStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 5, 18);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the end of Cannes header
        /// </summary>
        //===================================================================================================
        static DateTime GetCannesEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 5, 21);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the beginning Berlinale header
        /// </summary>
        //===================================================================================================
        static DateTime GetBerlinaleStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 2, 18);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the end of Berlinale header
        /// </summary>
        //===================================================================================================
        static DateTime GetBerlinaleEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 2, 21);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the beginning Durban header
        /// </summary>
        //===================================================================================================
        static DateTime GetDurbanStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 7, 21);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the end of Durban header
        /// </summary>
        //===================================================================================================
        static DateTime GetDurbanEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 7, 24);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the beginning of Timkat header
        /// </summary>
        //===================================================================================================
        static DateTime GetTimkatStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 1, 17);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the end of Timkat header
        /// </summary>
        //===================================================================================================
        static DateTime GetTimkatEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 1, 22);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of FESTIMA header
        /// </summary>
        //===================================================================================================
        static DateTime GetFestimaStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 2, 26);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the end of FESTIMA header
        /// </summary>
        //===================================================================================================
        static DateTime GetFestimaEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 3, 1);
        }


        //===================================================================================================
        /// <summary>
        /// Gets the beginning of ESC header
        /// </summary>
        //===================================================================================================
        static DateTime GetEscStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 5, 11);
        }

        //===================================================================================================
        /// <summary>
        /// Gets the end of ESC header
        /// </summary>
        //===================================================================================================
        static DateTime GetEscEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            return new DateTime(dtmTestedDate.Value.Year, 5, 17);
        }

        //===================================================================================================
        /// <summary>
        /// Estimates the Rosh Hashanah Israeli new year dates
        /// </summary>
        /// <returns>The start of Rosh Hashanah - Israeli new year header</returns>
        //===================================================================================================
        public static DateTime GetRoshHashanahStart(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }


            // Rosh Hashanah is only two days, but we want the header to be viewed longer.
            return GetRoshHashanahEnd(dtmTestedDate).AddDays(-4);
        }

        //===================================================================================================
        /// <summary>
        /// Estimates the Rosh Hashanah Israeli new year dates
        /// </summary>
        /// <returns>The end of Rosh Hashanah - Israeli new year header</returns>
        //===================================================================================================
        public static DateTime GetRoshHashanahEnd(DateTime? dtmTestedDate = null)
        {
            // if caller didn't specify the date, then take current date
            if (!dtmTestedDate.HasValue)
            {
                dtmTestedDate = DateTime.Now;
            }

            // Initialize the HebrewCalendar
            Calendar hebrewCalendar = new HebrewCalendar();

            // Use a utility function to determine the correct Hebrew Year for the current Gregorian Year cycle
            int nHebrewYear = 5786 - 2026 + dtmTestedDate.Value.Year;

            // Month 1 of the Hebrew calendar is Tishrei (Rosh Hashanah is the 1st day of Tishrei)
            const int nTishrei = 1;
            const int nDayAfterRoshHashanah = 3;

            // 4. Calculate the Gregorian DateTime for the day after Rosh Hashanah
            DateTime dtmRoshHashanahEndDateGregorian = hebrewCalendar.ToDateTime(
                nHebrewYear,
                nTishrei,
                nDayAfterRoshHashanah,
                0, 0, 0, 0);

            return dtmRoshHashanahEndDateGregorian;
        }

    }
}
