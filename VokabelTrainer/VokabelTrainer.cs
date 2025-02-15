// Vokabel-Trainer v1.2
// Copyright (C) 2019 NataljaNeumann@gmx.de
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
            }
            else
            {
                m_btnExerciseSecondToFirst.Text = string.Format(Properties.Resources.Exercise, m_strSecondLanguage, m_strFirstLanguage);
                //m_btnExerciseSecondToFirst.Text =  _secondLanguage + " - " + _firstLanguage + " trainieren";
                m_btnExerciseFirstToSecond.Text = string.Format(Properties.Resources.Exercise, m_strFirstLanguage, m_strSecondLanguage);
                //m_btnExerciseFirstToSecond.Text =  _firstLanguage + " - " + _secondLanguage + " trainieren";
                m_btnIntensiveSecondToFirst.Enabled = m_btnExerciseSecondToFirst.Enabled = m_oTrainingResultsFirstLanguage.Count > 0;
                m_btnIntensiveFirstToSecond.Enabled = m_btnExerciseFirstToSecond.Enabled = m_oTtrainingResultsSecondLanguage.Count > 0;
                m_lblReader.Enabled = m_cbxReader.Enabled = m_oTrainingResultsFirstLanguage.Count > 0 || m_oTtrainingResultsSecondLanguage.Count > 0;


                m_btnIntensiveSecondToFirst.Text = string.Format(Properties.Resources.Intensive, m_strSecondLanguage, m_strFirstLanguage);
                m_btnIntensiveFirstToSecond.Text = string.Format(Properties.Resources.Intensive, m_strFirstLanguage, m_strSecondLanguage);

                //m_btnIntensiveSecondToFirst.Text = _secondLanguage + " - " + _firstLanguage + " intensiv";
                //m_btnIntensiveFirstToSecond.Text = _firstLanguage + " - " + _secondLanguage + " intensiv";


                m_btnMostIntensiveSecondToFirst.Text = string.Format(Properties.Resources.MostIntensive, m_strSecondLanguage, m_strFirstLanguage );
                m_btnMostIntensiveFirstToSecond.Text = string.Format(Properties.Resources.MostIntensive, m_strFirstLanguage, m_strSecondLanguage );

                //m_btnMostIntensiveSecondToFirst.Text = _secondLanguage + " - " + _firstLanguage + " intensivst";
                //m_btnMostIntensiveFirstToSecond.Text = _firstLanguage + " - " + _secondLanguage + " intensivst"; 

                m_btnMostIntensiveSecondToFirst.Enabled = m_nTotalNumberOfErrorsSecondLanguage > 0;
                m_btnMostIntensiveFirstToSecond.Enabled = m_nTotalNumberOfErrorsFirstLanguage > 0;
            }
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user wants to load a language file
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_btnLoadLanguageFile_Click(object oSender, EventArgs oArgs)
        {
            m_dlgOpenFileDialog.InitialDirectory = 
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
                                                                  -  (m_oTrainingResultsFirstLanguage[e.SelectSingleNode("vokabel").InnerText].Length -
                                                                      m_oTrainingResultsFirstLanguage[e.SelectSingleNode("vokabel").InnerText].Replace("0","").Length);

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
                                                                     m_oTtrainingResultsSecondLanguage[e.SelectSingleNode("vokabel").InnerText].Replace("0","").Length);

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
                                    m_oCorrectSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = nCorrectAnswers;
                                }
                                else
                                    m_oCorrectSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = 0;
                            }

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
        private void m_btnNewLanguage_Click(object oSender, EventArgs oArgs)
        {
            using (NewLanguageFile form = new NewLanguageFile())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    string strNewPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                        form.m_tbxFirstLanguage.Text + "-" + form.m_tbxSecondLanguage.Text + ".Vokabeln.xml");

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

                    m_oCurrentVocabularyDoc.LoadXml("<?xml version=\"1.0\" ?>\r\n<vokabeln>\r\n  <erste-sprache-name>" + form.m_tbxFirstLanguage.Text + "</erste-sprache-name>\r\n"+
                        "  <zweite-sprache-name>" + form.m_tbxSecondLanguage.Text + "</zweite-sprache-name>\r\n"+
                        "  <erste-sprache-rtl>"+(form.m_chkFirstLanguageRTL.Checked?"ja":"nein")+"</erste-sprache-rtl>\r\n"+
                        "  <zweite-sprache-rtl>" + (form.m_chkSecondLanguageRTL.Checked ? "ja" : "nein") + "</zweite-sprache-rtl>\r\n" +
                        "<lizenz><modifikationen>" + (m_bIsModifiableFlagForXml ? "Unter Lizenzbedingungen" : "Keine neuen Wörter und keine Lizenzänderungen") + "</modifikationen><text>" + m_strLicense + "</text></lizenz></vokabeln>\r\n");
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
        private void m_btnEnterVocabulary_Click(object oSender, EventArgs oArgs)
        {
            bool bRepeat = true;
            bool bSave = false;
            while (bRepeat)
            {
                string strFirstText = "";
                string strSecondText = "";

                bool bRepeat2 = true;
                while (bRepeat2)
                {
                    bRepeat2 = false;
                    using (NewDictionaryPair pair = new NewDictionaryPair(m_chkUseESpeak.Checked, m_tbxESpeakPath.Text))
                    {
                        char[] separators = { ',', ';' };
                        pair.m_lblFirstLanguage.Text = m_strFirstLanguage + ":";
                        pair.m_lblSecondLanguage.Text = m_strSecondLanguage + ":";
                        pair.m_tbxFirstLanguage.Text = strFirstText;
                        pair.textBoxSecondLanguage.Text = strSecondText;
                        pair.m_lblFirstLanguage.RightToLeft = m_bFirstLanguageRtl?RightToLeft.Yes:RightToLeft.No;
                        pair.m_lblSecondLanguage.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        pair.m_tbxFirstLanguage.RightToLeft = m_bFirstLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        pair.textBoxSecondLanguage.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        switch (pair.ShowDialog())
                        {
                            case DialogResult.Retry:
                                if (string.IsNullOrEmpty(pair.m_tbxFirstLanguage.Text.Trim()))
                                {
                                    strFirstText = "";
                                    strSecondText = pair.textBoxSecondLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }
                                if (string.IsNullOrEmpty(pair.textBoxSecondLanguage.Text.Trim()))
                                {
                                    strSecondText = "";
                                    strFirstText = pair.m_tbxFirstLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }

                                string sss1 = pair.m_tbxFirstLanguage.Text
                                    .Replace("?", "?,").Replace("!", "!,")
                                    .Replace(";", ",").Replace(",,", ",").Replace(",,", ",");
                                string sss2 = pair.textBoxSecondLanguage.Text
                                    .Replace("?", "?,").Replace("!", "!,")
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
                                    strSecondText = pair.textBoxSecondLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }
                                if (string.IsNullOrEmpty(pair.textBoxSecondLanguage.Text.Trim()))
                                {
                                    strSecondText = "";
                                    strFirstText = pair.m_tbxFirstLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }

                                sss1 = pair.m_tbxFirstLanguage.Text
                                    .Replace("?", "?,").Replace("!", "!,")
                                    .Replace(";", ",").Replace(",,", ",").Replace(",,", ",");
                                sss2 = pair.textBoxSecondLanguage.Text
                                    .Replace("?", "?,").Replace("!", "!,")
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
        private void button3_Click(object oSender, EventArgs oArgs)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train one of the words randomly. Words with errors get higher weight
                int rnd2 = m_oRnd2.Next();
                m_oRnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                int selectedError = rnd2 % (m_nTotalNumberOfErrorsSecondLanguage + m_oTtrainingResultsSecondLanguage.Count);

                m_bSkipLast = m_oTtrainingResultsSecondLanguage.Count > 10;

                int wordIndex = -1;
                using (SortedDictionary<string,string>.ValueCollection.Enumerator values = 
                    m_oTtrainingResultsSecondLanguage.Values.GetEnumerator())
                {
                    while (selectedError >= 0 && values.MoveNext())
                    {
                        wordIndex += 1;
                        if (values.Current.Contains("0"))
                        {
                            selectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
                        }
                        else
                            selectedError -= 1;
                    }

                    bRepeat = TrainSecondToFirstLanguage(wordIndex);
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
        private void m_btnIntensiveSecondToFirst_Click(object oSender, EventArgs oArgs)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // decide, if we will train one word randomly, or one that needs additional training
                int rnd = m_oRnd.Next();
                m_oRnd = new Random(rnd + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);

                if ((m_nTotalNumberOfErrorsSecondLanguage>0) && (rnd % 100 < 50))
                {
                    // there we train one of the words that need training
                    int rnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(rnd + rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int selectedError = rnd2 % m_nTotalNumberOfErrorsSecondLanguage;

                    m_bSkipLast = m_oTtrainingResultsSecondLanguage.Count > 20;

                    int wordIndex = -1;
                    using (SortedDictionary<string, string>.ValueCollection.Enumerator values = 
                        m_oTtrainingResultsSecondLanguage.Values.GetEnumerator())
                    {
                        while (selectedError >= 0 && values.MoveNext())
                        {
                            wordIndex += 1;
                            if (values.Current.Contains("0"))
                            {
                                selectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
                            }
                        }

                        bRepeat = TrainSecondToFirstLanguage(wordIndex);
                    }
                }
                else
                {
                    // there we train one of the words
                    int rnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);


                    // calculate mean of correct answers
                    long lTotal = 0;
                    foreach (int i in m_oCorrectSecondLanguage.Values)
                        lTotal += i;

                    int mean = (int)((lTotal+m_oCorrectSecondLanguage.Count/2) / m_oCorrectSecondLanguage.Count);

                    // now calculate the sum of weights of all words
                    int iTotalWeights = 0;
                    foreach (int i in m_oCorrectSecondLanguage.Values)
                    {
                        int weight = (i > mean + 3) ? 0 : (mean + 3 - i)*(mean + 3 - i);
                        iTotalWeights += weight;
                    };

                    int selectedWeight = rnd2 % iTotalWeights;

                    int wordIndex = -1;
                    using (SortedDictionary<string, int>.ValueCollection.Enumerator values = 
                        m_oCorrectSecondLanguage.Values.GetEnumerator())
                    {
                        while (selectedWeight >= 0 && values.MoveNext())
                        {
                            wordIndex += 1;

                            int weight = (values.Current > mean + 3) ? 0 : 
                                (mean + 3 - values.Current) * (mean + 3 - values.Current);

                            selectedWeight -= weight;
                        }

                        m_bSkipLast = m_oTtrainingResultsSecondLanguage.Count > 10;

                        bRepeat = TrainSecondToFirstLanguage(wordIndex);
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
        private void m_btnMostIntensiveSecondToFirst_Click(object oSender, EventArgs oArgs)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train only the words with errors, and we start with those that had errors recently
                int bestIndex = -1;
                int bestCount = 0;
                int bestTime = 16;
                int wordIndex = -1;
                foreach(string s in m_oTtrainingResultsSecondLanguage.Values)
                {
                    ++wordIndex;
                    int time = s.IndexOf('0');
                    if (time >= 0)
                    {
                        if (bestTime > time)
                        {
                            bestTime = time;
                            bestCount = 1;
                        }
                        else
                            if (bestTime == time)
                                ++bestCount;
                    }
                }


                if (bestCount > 0)
                {
                    int rnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int selectedBest = rnd2 % bestCount;

                    wordIndex = -1;
                    foreach (string s in m_oTtrainingResultsSecondLanguage.Values)
                    {
                        ++wordIndex;
                        int time = s.IndexOf('0');
                        if (time >= 0)
                        {
                            if (bestTime == time)
                            {
                                if (--selectedBest < 0)
                                {
                                    bestIndex = wordIndex;
                                    break;
                                }
                            }
                        }
                    }

                    if (bestIndex >= 0)
                    {
                        m_bSkipLast = false;
                        bRepeat = TrainSecondToFirstLanguage(bestIndex);
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
        private bool TrainSecondToFirstLanguage(int nIndex)
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
                            // if we trained this word recently, then try to skip it, but not with that high probability
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
                                .Replace("?", "?,").Replace("!", "!,").Replace(";",",")
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
        void RememberResultFirstLanguage(string strWord, bool bCorrect)
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
        void RememberResultSecondLanguage(string strWord, bool bCorrect)
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
                    w.WriteLine("  <lizenz><modifikationen>{0}</modifikationen>", m_bIsModifiableFlagForXml ? "Unter Lizenzbedingungen" : "Keine neuen Wörter und keine Lizenzänderungen");
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
                            w.WriteLine("  <erste-sprache><vokabel>{0}</vokabel><training-vorgeschichte>{1}</training-vorgeschichte><richtige-antworten>{2}</richtige-antworten></erste-sprache>", training.Key.Trim(), training.Value, m_oCorrectAnswersFirstLanguage[training.Key]);
                        else
                            w.WriteLine("  <erste-sprache><vokabel>{0}</vokabel>{3}<training-vorgeschichte>{1}</training-vorgeschichte><richtige-antworten>{2}</richtige-antworten></erste-sprache>", training.Key.Trim(), training.Value, m_oCorrectAnswersFirstLanguage[training.Key], spaces[training.Key.Trim().Length]);
                    w.WriteLine();
                    w.WriteLine("  <!-- Die Vokabeln der zweiten Sprache ({0}), und deren Trainingsfortschritt 1=richtig 0=falsch -->", m_strSecondLanguage);
                    w.WriteLine("  <!-- Das letzte Training ist am Anfang der Zahl, die jeweils früheren jeweils danach -->");
                    foreach (KeyValuePair<string, string> training in m_oTtrainingResultsSecondLanguage)
                        if (training.Key.Length >= spaces.Length)
                            w.WriteLine("  <zweite-sprache><vokabel>{0}</vokabel><training-vorgeschichte>{1}</training-vorgeschichte><richtige-antworten>{2}</richtige-antworten></zweite-sprache>", training.Key.Trim(), training.Value, m_oCorrectSecondLanguage[training.Key]);
                        else
                            w.WriteLine("  <zweite-sprache><vokabel>{0}</vokabel>{3}<training-vorgeschichte>{1}</training-vorgeschichte><richtige-antworten>{2}</richtige-antworten></zweite-sprache>", training.Key.Trim(), training.Value, m_oCorrectSecondLanguage[training.Key], spaces[training.Key.Trim().Length]);
                    w.WriteLine();
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
        private bool TrainFirstLanguage(int nIndex)
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
                                .Replace("?", "?,").Replace("!", "!,").Replace(";",",")
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

                                    Speaker.Say(m_strSecondLanguage, strTextToSpeak, true, m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);

                                }
                                else
                                    if (missing.Count == 1)
                                    {
                                        strErrorMessage = Properties.Resources.FollowingMeaningWasMissing;
                                        foreach (string s in missing.Keys)
                                        {
                                            strErrorMessage = strErrorMessage + s + ". ";


                                            Speaker.Say(m_strSecondLanguage, s, true, m_chkUseESpeak.Checked, m_tbxESpeakPath.Text);


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
        private void m_btnExerciseFirstToSecond_Click(object oSender, EventArgs oArgs)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train one of the words randomly. The words with errors get higher weight.
                int rnd2 = m_oRnd2.Next();
                m_oRnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                int selectedError = rnd2 % (m_nTotalNumberOfErrorsFirstLanguage + m_oTrainingResultsFirstLanguage.Count);

                m_bSkipLast = m_oTrainingResultsFirstLanguage.Count > 10;

                int wordIndex = -1;
                using (SortedDictionary<string, string>.ValueCollection.Enumerator values = 
                    m_oTrainingResultsFirstLanguage.Values.GetEnumerator())
                {
                    while (selectedError >= 0 && values.MoveNext())
                    {
                        wordIndex += 1;
                        if (values.Current.Contains("0"))
                        {
                            selectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
                        }
                        else
                            selectedError -= 1;
                    }

                    bRepeat = TrainFirstLanguage(wordIndex);
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
        private void m_btnIntensiveFirstToSecond_Click(object oSender, EventArgs oArgs)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // decide, if we will train one word randomly, or one that needs additional training
                int rnd = m_oRnd.Next();
                m_oRnd = new Random(rnd + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);

                if ( (m_nTotalNumberOfErrorsFirstLanguage>0) && (rnd % 100 < 50) )
                {
                    // there we train one of the words that need training
                    int rnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(rnd + rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int selectedError = rnd2 % m_nTotalNumberOfErrorsFirstLanguage;

                    m_bSkipLast = m_oTrainingResultsFirstLanguage.Count > 20;

                    int wordIndex = -1;
                    using (SortedDictionary<string, string>.ValueCollection.Enumerator values = 
                        m_oTrainingResultsFirstLanguage.Values.GetEnumerator())
                    {
                        while (selectedError >= 0 && values.MoveNext())
                        {
                            wordIndex += 1;
                            if (values.Current.Contains("0"))
                            {
                                selectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
                            }
                        }

                        bRepeat = TrainFirstLanguage(wordIndex);
                    }
                }
                else
                {
                    // there we train one of the words
                    int rnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    // calculate mean of correct answers
                    long lTotal = 0;
                    foreach (int i in m_oCorrectAnswersFirstLanguage.Values)
                        lTotal += i;

                    int mean = (int)( (lTotal + m_oCorrectAnswersFirstLanguage.Count/2) / m_oCorrectAnswersFirstLanguage.Count);

                    // now calculate the sum of weights of all words
                    int iTotalWeights = 0;
                    foreach (int i in m_oCorrectAnswersFirstLanguage.Values)
                    {
                        int weight = (i > mean + 3) ? 0 : (mean + 3 - i) * (mean + 3 - i);

                        iTotalWeights += weight;
                    };

                    int selectedWeight = rnd2 % iTotalWeights;

                    int wordIndex = -1;
                    using (SortedDictionary<string, int>.ValueCollection.Enumerator values = 
                        m_oCorrectAnswersFirstLanguage.Values.GetEnumerator())
                    {
                        while (selectedWeight >= 0 && values.MoveNext())
                        {
                            wordIndex += 1;

                            int weight = (values.Current > mean + 3) ? 0 : 
                                (mean + 3 - values.Current) * (mean + 3 - values.Current);

                            selectedWeight -= weight;
                        }

                        m_bSkipLast = m_oTrainingResultsFirstLanguage.Count > 10;

                        bRepeat = TrainFirstLanguage(wordIndex);
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
        private void m_btnMostIntensiveFirstToSecond_Click(object oSender, EventArgs oArgs)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train only the words that with errors, 
                // and we start with those that had errors recently
                int bestIndex = -1;
                int bestTime = 16;
                int wordIndex = -1;
                int bestCount = 0;
                foreach (string s in m_oTrainingResultsFirstLanguage.Values)
                {
                    ++wordIndex;
                    int time = s.IndexOf('0');
                    if (time >= 0)
                    {
                        if (bestTime > time)
                        {
                            bestTime = time;
                            bestCount = 1;
                        }
                        else
                            if (bestTime == time)
                                ++bestCount;
                    }
                }

                if (bestCount > 0)
                {
                    int rnd2 = m_oRnd2.Next();
                    m_oRnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int selectedBest = rnd2 % bestCount;

                    wordIndex = -1;
                    foreach (string s in m_oTrainingResultsFirstLanguage.Values)
                    {
                        ++wordIndex;
                        int time = s.IndexOf('0');
                        if (time >= 0)
                        {
                            if (bestTime == time)
                            {
                                if (--selectedBest < 0)
                                {
                                    bestIndex = wordIndex;
                                    break;
                                }
                            }
                        }
                    }

                    if (bestIndex >= 0)
                    {
                        m_bSkipLast = false;

                        bRepeat = TrainFirstLanguage(bestIndex);
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
            using (About form = new About())
            {
                form.ShowDialog(this);
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
            m_tbxESpeakPath.Text = "C:\\Program Files (x86)\\eSpeak\\command_line\\espeak.exe";
            m_btnSearchESpeak.Enabled = 
                m_tbxESpeakPath.Enabled = 
                    m_chkUseESpeak.Checked = System.IO.File.Exists(m_tbxESpeakPath.Text);
        }

    }
}
