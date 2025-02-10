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
    public partial class VokabelTrainer : Form
    {
        string _firstLanguage;
        string _secondLanguage;
        string _currentPath;
        Random _rnd;
        Random _rnd2;
        System.Xml.XmlDocument _currentDoc;
        bool m_bFirstLanguageRtl;
        bool m_bSecondLanguageRtl;

        SortedDictionary<string, string> _trainingFirstLanguage;
        SortedDictionary<string, string> _trainingSecondLanguage;
        SortedDictionary<string, int> _correctFirstLanguage;
        SortedDictionary<string, int> _correctSecondLanguage;
        SortedDictionary<string, SortedDictionary<string,bool>> _firstToSecond;
        SortedDictionary<string, SortedDictionary<string,bool>> _secondToFirst;
        LinkedList<string> _trainedWords = new LinkedList<string>();
        int _totalNumberOfErrorsFirstLanguage;
        int _totalNumberOfErrorsSecondLanguage;
        bool _skipLast;
        string _license;
        bool _modifiable;
        bool _saveModifiable;
        bool _savePossible;

        public VokabelTrainer()
        {
            InitializeComponent();
            // init random with current time
            _rnd = new Random(((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);
            _rnd2 = new Random((((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond)*365+DateTime.UtcNow.DayOfYear);
            m_cbxReader.SelectedIndex = 0;
        }

        public void EnableDisableButtons()
        {
            m_btnNewLanguageFile.Enabled = true;
            m_btnEnterVocabulary.Enabled = (_currentPath != null) && _modifiable;
            m_btnExerciseSecondToFirst.Enabled = (_currentPath != null) && _firstToSecond.Count > 0;
            m_btnExerciseFirstToSecond.Enabled = (_currentPath != null) && _secondToFirst.Count > 0;
            if (string.IsNullOrEmpty(_firstLanguage) || string.IsNullOrEmpty(_secondLanguage))
            {
                m_btnExerciseSecondToFirst.Text = m_btnExerciseFirstToSecond.Text = m_btnIntensiveSecondToFirst.Text = m_btnIntensiveFirstToSecond.Text = m_btnMostIntensiveSecondToFirst.Text = m_btnMostIntensiveFirstToSecond.Text = "";
                m_btnExerciseSecondToFirst.Enabled = m_btnExerciseFirstToSecond.Enabled = m_btnIntensiveSecondToFirst.Enabled = m_btnIntensiveFirstToSecond.Enabled = m_btnMostIntensiveSecondToFirst.Enabled = m_btnMostIntensiveFirstToSecond.Enabled = false;
                m_cbxReader.Enabled = false;
                m_lblReader.Enabled = false;
            }
            else
            {
                m_btnExerciseSecondToFirst.Text = string.Format(Properties.Resources.Exercise, _secondLanguage, _firstLanguage);
                //m_btnExerciseSecondToFirst.Text =  _secondLanguage + " - " + _firstLanguage + " trainieren";
                m_btnExerciseFirstToSecond.Text = string.Format(Properties.Resources.Exercise, _firstLanguage, _secondLanguage);
                //m_btnExerciseFirstToSecond.Text =  _firstLanguage + " - " + _secondLanguage + " trainieren";
                m_btnIntensiveSecondToFirst.Enabled = m_btnExerciseSecondToFirst.Enabled = _trainingFirstLanguage.Count > 0;
                m_btnIntensiveFirstToSecond.Enabled = m_btnExerciseFirstToSecond.Enabled = _trainingSecondLanguage.Count > 0;
                m_lblReader.Enabled = m_cbxReader.Enabled = _trainingFirstLanguage.Count > 0 || _trainingSecondLanguage.Count > 0;


                m_btnIntensiveSecondToFirst.Text = string.Format(Properties.Resources.Intensive, _secondLanguage, _firstLanguage);
                m_btnIntensiveFirstToSecond.Text = string.Format(Properties.Resources.Intensive, _firstLanguage, _secondLanguage);

                //m_btnIntensiveSecondToFirst.Text = _secondLanguage + " - " + _firstLanguage + " intensiv";
                //m_btnIntensiveFirstToSecond.Text = _firstLanguage + " - " + _secondLanguage + " intensiv";


                m_btnMostIntensiveSecondToFirst.Text = string.Format(Properties.Resources.MostIntensive, _secondLanguage, _firstLanguage );
                m_btnMostIntensiveFirstToSecond.Text = string.Format(Properties.Resources.MostIntensive, _firstLanguage, _secondLanguage );

                //m_btnMostIntensiveSecondToFirst.Text = _secondLanguage + " - " + _firstLanguage + " intensivst";
                //m_btnMostIntensiveFirstToSecond.Text = _firstLanguage + " - " + _secondLanguage + " intensivst"; 

                m_btnMostIntensiveSecondToFirst.Enabled = _totalNumberOfErrorsSecondLanguage > 0;
                m_btnMostIntensiveFirstToSecond.Enabled = _totalNumberOfErrorsFirstLanguage > 0;
            }
        }

        private void button1_Click(object sender, EventArgs ev)
        {
            if (m_dlgOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _currentPath = m_dlgOpenFileDialog.FileName;
                    _currentDoc = new System.Xml.XmlDocument();
                    _currentDoc.Load(_currentPath);

                    _trainingFirstLanguage = new SortedDictionary<string, string>();
                    _trainingSecondLanguage = new SortedDictionary<string, string>();
                    _firstToSecond = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    _secondToFirst = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    _correctFirstLanguage = new SortedDictionary<string, int>();
                    _correctSecondLanguage = new SortedDictionary<string, int>();
                    _totalNumberOfErrorsFirstLanguage = 0;
                    _totalNumberOfErrorsSecondLanguage = 0;

                    _secondLanguage = _currentDoc.SelectSingleNode("/vokabeln/zweite-sprache-name").InnerText;
                    _firstLanguage = _currentDoc.SelectSingleNode("/vokabeln/erste-sprache-name").InnerText;
                    m_bFirstLanguageRtl = _currentDoc.SelectSingleNode("/vokabeln/erste-sprache-rtl") != null ?
                        _currentDoc.SelectSingleNode("/vokabeln/erste-sprache-rtl").InnerText.Equals("ja") : false;
                    m_bSecondLanguageRtl = _currentDoc.SelectSingleNode("/vokabeln/zweite-sprache-rtl") != null ?
                        _currentDoc.SelectSingleNode("/vokabeln/zweite-sprache-rtl").InnerText.Equals("ja") : false;
                    _modifiable = true;
                    _license = "";
                    foreach (System.Xml.XmlElement e in _currentDoc.SelectNodes("/vokabeln/lizenz"))
                    {
                        _saveModifiable = _modifiable = e.SelectSingleNode("modifikationen").InnerText.Equals("Unter Lizenzbedingungen", StringComparison.CurrentCultureIgnoreCase);
                        _savePossible = _saveModifiable || e.SelectSingleNode("modifikationen").InnerText.Equals("Keine neuen Wörter und keine Lizenzänderungen", StringComparison.CurrentCultureIgnoreCase);
                        _license = e.SelectSingleNode("text").InnerText;
                    }

                    foreach (System.Xml.XmlElement e in _currentDoc.SelectNodes("/vokabeln/erste-sprache"))
                    {
                        string trainingFortschritt = e.SelectSingleNode("training-vorgeschichte").InnerText;
                        if (trainingFortschritt.Length > 6)
                            trainingFortschritt = trainingFortschritt.Substring(0, 6);
                        else
                            while (trainingFortschritt.Length < 6)
                                trainingFortschritt = trainingFortschritt + "1";

                        _trainingFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = trainingFortschritt;
                        _totalNumberOfErrorsFirstLanguage += trainingFortschritt.Length - trainingFortschritt.Replace("0", "").Length; 
                        _firstToSecond[e.SelectSingleNode("vokabel").InnerText] = new SortedDictionary<string, bool>();

                        System.Xml.XmlNode n = e.SelectSingleNode("richtige-antworten");
                        if (n != null)
                        {
                            string correctAnswers = n.InnerText;
                            int nCorrectAnswers = 0;
                            if (!int.TryParse(correctAnswers, out nCorrectAnswers))
                                nCorrectAnswers = 0;
                            else
                                if (nCorrectAnswers < 0)
                                    nCorrectAnswers = 0;
                            _correctFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = nCorrectAnswers;
                        } else
                            _correctFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = 0;
                    }

                    foreach (System.Xml.XmlElement e in _currentDoc.SelectNodes("/vokabeln/zweite-sprache"))
                    {
                        string trainingFortschritt = e.SelectSingleNode("training-vorgeschichte").InnerText;
                        if (trainingFortschritt.Length > 6)
                            trainingFortschritt = trainingFortschritt.Substring(0, 6);
                        else
                            while (trainingFortschritt.Length < 6)
                                trainingFortschritt = trainingFortschritt + "1";
                        _trainingSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = trainingFortschritt;
                        _secondToFirst[e.SelectSingleNode("vokabel").InnerText] = new SortedDictionary<string, bool>();
                        _totalNumberOfErrorsSecondLanguage += trainingFortschritt.Length - trainingFortschritt.Replace("0", "").Length;

                        System.Xml.XmlNode n = e.SelectSingleNode("richtige-antworten");
                        if (n != null)
                        {
                            string correctAnswers = n.InnerText;
                            int nCorrectAnswers = 0;
                            if (!int.TryParse(correctAnswers, out nCorrectAnswers))
                                nCorrectAnswers = 0;
                            else
                                if (nCorrectAnswers < 0)
                                    nCorrectAnswers = 0;
                            _correctSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = nCorrectAnswers;
                        } else
                            _correctSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = 0;

                    }


                    foreach (System.Xml.XmlElement e in _currentDoc.SelectNodes("/vokabeln/vokabel-paar"))
                    {
                        string ersteSprache = e.SelectSingleNode("erste-sprache").InnerText;
                        string zweiteSprache = e.SelectSingleNode("zweite-sprache").InnerText;

                        if (!_trainingFirstLanguage.ContainsKey(ersteSprache))
                        {
                            _trainingFirstLanguage[ersteSprache] = "111110";
                            _totalNumberOfErrorsFirstLanguage += 1;
                            _firstToSecond[ersteSprache] = new SortedDictionary<string, bool>();
                        }

                        if (!_trainingSecondLanguage.ContainsKey(zweiteSprache))
                        {
                            _trainingSecondLanguage[zweiteSprache] = "111110";
                            _totalNumberOfErrorsSecondLanguage += 1;
                            _secondToFirst[zweiteSprache] = new SortedDictionary<string, bool>();
                        }

                        if (!_correctFirstLanguage.ContainsKey(ersteSprache))
                            _correctFirstLanguage[ersteSprache] = 0;

                        if (!_correctSecondLanguage.ContainsKey(zweiteSprache))
                            _correctSecondLanguage[zweiteSprache] = 0;


                        if (!_firstToSecond[ersteSprache].ContainsKey(zweiteSprache))
                            _firstToSecond[ersteSprache][zweiteSprache] = false;

                        if (!_secondToFirst[zweiteSprache].ContainsKey(ersteSprache))
                            _secondToFirst[zweiteSprache][ersteSprache] = false;
                    }

                }
                catch (Exception ex)
                {
                    NewMessageBox.Show(this, ex.Message, Properties.Resources.ErrorLoadingXmlFileHeader, string.Format(Properties.Resources.ErrorLoadingXmlFileMessage, ex.Message));
                    //NewMessageBox.Show(this, ex.Message, "Fehler beim Laden der XML-Vokabeldatei", "Beim Laden der XML-Vokabeldatei ist der folgende Fehler aufgetreten: " + ex.Message);
                    //System.Windows.Forms.MessageBox.Show(ex.Message, "Fehler beim Laden der XML-Vokabeldatei", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _currentPath = null;
                    _currentDoc = null;

                    _trainingFirstLanguage = new SortedDictionary<string, string>();
                    _trainingSecondLanguage = new SortedDictionary<string, string>();
                    _firstToSecond = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    _secondToFirst = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    _correctFirstLanguage = new SortedDictionary<string, int>();
                    _correctSecondLanguage = new SortedDictionary<string, int>();
                    _totalNumberOfErrorsFirstLanguage = 0;
                    _totalNumberOfErrorsSecondLanguage = 0;
                }

                // continue with loading of training file
                if (_currentPath!=null)
                try {

                    string currentPath = _currentPath.Replace(".Vokabeln.xml",".Training.xml")
                        .Replace("Vocabulary.xml",".Training.xml");
                    System.IO.FileInfo fi = new System.IO.FileInfo(currentPath);
                    if (fi.Exists)
                    {
                        System.Xml.XmlDocument currentDoc = new System.Xml.XmlDocument();
                        currentDoc.Load(currentPath);

                        foreach (System.Xml.XmlElement e in currentDoc.SelectNodes("/training/erste-sprache"))
                        {
                            string trainingFortschritt = e.SelectSingleNode("training-vorgeschichte").InnerText;
                            if (trainingFortschritt.Length > 6)
                                trainingFortschritt = trainingFortschritt.Substring(0, 6);
                            else
                                while (trainingFortschritt.Length < 6)
                                    trainingFortschritt = trainingFortschritt + "1";

                            if (_trainingFirstLanguage.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                            {
                                _totalNumberOfErrorsFirstLanguage += trainingFortschritt.Length - trainingFortschritt.Replace("0", "").Length
                                                                  -  (_trainingFirstLanguage[e.SelectSingleNode("vokabel").InnerText].Length -
                                                                      _trainingFirstLanguage[e.SelectSingleNode("vokabel").InnerText].Replace("0","").Length);

                                _trainingFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = trainingFortschritt;

                                if (!_firstToSecond.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                                    _firstToSecond[e.SelectSingleNode("vokabel").InnerText] = new SortedDictionary<string, bool>();

                                System.Xml.XmlNode n = e.SelectSingleNode("richtige-antworten");
                                if (n != null)
                                {
                                    string correctAnswers = n.InnerText;
                                    int nCorrectAnswers = 0;
                                    if (!int.TryParse(correctAnswers, out nCorrectAnswers))
                                        nCorrectAnswers = 0;
                                    else
                                        if (nCorrectAnswers < 0)
                                            nCorrectAnswers = 0;
                                    _correctFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = nCorrectAnswers;
                                }
                                else
                                    _correctFirstLanguage[e.SelectSingleNode("vokabel").InnerText] = 0;
                            }
                        }

                        foreach (System.Xml.XmlElement e in currentDoc.SelectNodes("/training/zweite-sprache"))
                        {
                            string trainingFortschritt = e.SelectSingleNode("training-vorgeschichte").InnerText;
                            if (trainingFortschritt.Length > 6)
                                trainingFortschritt = trainingFortschritt.Substring(0, 6);
                            else
                                while (trainingFortschritt.Length < 6)
                                    trainingFortschritt = trainingFortschritt + "1";

                            if (_trainingSecondLanguage.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                            {
                                _totalNumberOfErrorsSecondLanguage += trainingFortschritt.Length - trainingFortschritt.Replace("0", "").Length
                                                                   -(_trainingSecondLanguage[e.SelectSingleNode("vokabel").InnerText].Length -
                                                                     _trainingSecondLanguage[e.SelectSingleNode("vokabel").InnerText].Replace("0","").Length);

                                _trainingSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = trainingFortschritt;

                                if (!_secondToFirst.ContainsKey(e.SelectSingleNode("vokabel").InnerText))
                                    _secondToFirst[e.SelectSingleNode("vokabel").InnerText] = new SortedDictionary<string, bool>();

                                System.Xml.XmlNode n = e.SelectSingleNode("richtige-antworten");
                                if (n != null)
                                {
                                    string correctAnswers = n.InnerText;
                                    int nCorrectAnswers = 0;
                                    if (!int.TryParse(correctAnswers, out nCorrectAnswers))
                                        nCorrectAnswers = 0;
                                    else
                                        if (nCorrectAnswers < 0)
                                            nCorrectAnswers = 0;
                                    _correctSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = nCorrectAnswers;
                                }
                                else
                                    _correctSecondLanguage[e.SelectSingleNode("vokabel").InnerText] = 0;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    NewMessageBox.Show(this, ex.Message, Properties.Resources.ErrorLoadingTrainingFileHeader,
                        string.Format(Properties.Resources.ErrorLoadingTrainingFileMessage, ex.Message));
                    //NewMessageBox.Show(this, ex.Message, "Fehler beim Laden der XML-Trainingdatei", "Beim Laden der XML-Trainingdatei ist der folgende Fehler aufgetreten: " + ex.Message);
                    //System.Windows.Forms.MessageBox.Show(ex.Message, "Fehler beim Laden der XML-Vokabeldatei", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _currentPath = null;
                    _currentDoc = null;

                    _trainingFirstLanguage = new SortedDictionary<string, string>();
                    _trainingSecondLanguage = new SortedDictionary<string, string>();
                    _firstToSecond = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    _secondToFirst = new SortedDictionary<string, SortedDictionary<string, bool>>();
                    _correctFirstLanguage = new SortedDictionary<string, int>();
                    _correctSecondLanguage = new SortedDictionary<string, int>();
                    _totalNumberOfErrorsFirstLanguage = 0;
                    _totalNumberOfErrorsSecondLanguage = 0;
                }
            }

            EnableDisableButtons();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (NewLanguageFile form = new NewLanguageFile())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    string newPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), form.m_tbxFirstLanguage.Text + "-" + form.m_tbxSecondLanguage.Text + ".Vokabeln.xml");
                    System.IO.FileInfo fi = new System.IO.FileInfo(newPath);
                    if (fi.Exists)
                    {
                        if (System.Windows.Forms.MessageBox.Show(this, 
                            string.Format(Properties.Resources.LanguageFileAlreadyExistsMessage, newPath), 
                            Properties.Resources.LanguageFileAlreadyExistsHeader, MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                            != DialogResult.Yes)
                        //if (System.Windows.Forms.MessageBox.Show(this, "Die Sprachdatei existiert bereits: \"" + newPath + "\", möchten Sie diese überschreiben?", "Datei überschreiben?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            return;
                    };
                    _currentPath = newPath;
                    _currentDoc = new System.Xml.XmlDocument();
                    _currentDoc.PreserveWhitespace = false;
                    _saveModifiable = form.m_chkLanguageFileModifiable.Checked;
                    _modifiable = true;
                    _savePossible = true;
                    if (form.m_chkLanguageFileUnderGPL2.Checked)
                    {
                        _license = "Copyright (C) " + System.DateTime.Now.Year + " " + Environment.GetEnvironmentVariable("USERNAME") + "\r\n\r\n" +
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
                        _license = "Copyright (C) " + System.DateTime.Now.Year + " " + Environment.GetEnvironmentVariable("USERNAME") + ", alle Rechte vorbehalten";
                    }

                    _currentDoc.LoadXml("<?xml version=\"1.0\" ?>\r\n<vokabeln>\r\n  <erste-sprache-name>" + form.m_tbxFirstLanguage.Text + "</erste-sprache-name>\r\n"+
                        "  <zweite-sprache-name>" + form.m_tbxSecondLanguage.Text + "</zweite-sprache-name>\r\n"+
                        "  <erste-sprache-rtl>"+(form.m_chkFirstLanguageRTL.Checked?"ja":"nein")+"</erste-sprache-rtl>\r\n"+
                        "  <zweite-sprache-rtl>" + (form.m_chkSecondLanguageRTL.Checked ? "ja" : "nein") + "</zweite-sprache-rtl>\r\n" +
                        "<lizenz><modifikationen>" + (_saveModifiable ? "Unter Lizenzbedingungen" : "Keine neuen Wörter und keine Lizenzänderungen") + "</modifikationen><text>" + _license + "</text></lizenz></vokabeln>\r\n");
                    _currentDoc.Save(_currentPath);

                    m_bFirstLanguageRtl = form.m_chkFirstLanguageRTL.Checked;
                    m_bSecondLanguageRtl = form.m_chkSecondLanguageRTL.Checked;

                    _trainingFirstLanguage = new SortedDictionary<string,string>();
                    _trainingSecondLanguage = new SortedDictionary<string,string>();
                    _firstToSecond = new SortedDictionary<string,SortedDictionary<string,bool>>();
                    _secondToFirst = new SortedDictionary<string,SortedDictionary<string,bool>>();
                    _correctFirstLanguage = new SortedDictionary<string, int>();
                    _correctSecondLanguage = new SortedDictionary<string, int>();
                    _totalNumberOfErrorsFirstLanguage = 0;
                    _totalNumberOfErrorsSecondLanguage = 0;
                    _firstLanguage = form.m_tbxFirstLanguage.Text;
                    _secondLanguage = form.m_tbxSecondLanguage.Text;
                }
            }
            EnableDisableButtons();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool bRepeat = true;
            bool bSave = false;
            while (bRepeat)
            {
                string firstText = "";
                string secondText = "";

                bool bRepeat2 = true;
                while (bRepeat2)
                {
                    bRepeat2 = false;
                    using (NewDictionaryPair pair = new NewDictionaryPair())
                    {
                        char[] separators = { ',', ';' };
                        pair.m_lblFirstLanguage.Text = _firstLanguage + ":";
                        pair.m_lblSecondLanguage.Text = _secondLanguage + ":";
                        pair.m_tbxFirstLanguage.Text = firstText;
                        pair.textBoxSecondLanguage.Text = secondText;
                        pair.m_lblFirstLanguage.RightToLeft = m_bFirstLanguageRtl?RightToLeft.Yes:RightToLeft.No;
                        pair.m_lblSecondLanguage.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        pair.m_tbxFirstLanguage.RightToLeft = m_bFirstLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        pair.textBoxSecondLanguage.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        switch (pair.ShowDialog())
                        {
                            case DialogResult.Retry:
                                if (string.IsNullOrEmpty(pair.m_tbxFirstLanguage.Text.Trim()))
                                {
                                    firstText = "";
                                    secondText = pair.textBoxSecondLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }
                                if (string.IsNullOrEmpty(pair.textBoxSecondLanguage.Text.Trim()))
                                {
                                    secondText = "";
                                    firstText = pair.m_tbxFirstLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }

                                string sss1 = pair.m_tbxFirstLanguage.Text.Replace("?", "?,").Replace("!", "!,").Replace(";", ",").Replace(",,", ",").Replace(",,", ",");
                                string sss2 = pair.textBoxSecondLanguage.Text.Replace("?", "?,").Replace("!", "!,").Replace(";", ",").Replace(",,", ",").Replace(",,", ",");

                                foreach (string ss1 in sss1.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                                    foreach (string ss2 in sss2.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        string s1 = ss1.Trim();
                                        string s2 = ss2.Trim();

                                        if (!_trainingFirstLanguage.ContainsKey(s1))
                                        {
                                            _trainingFirstLanguage[s1] = "111110";
                                            _totalNumberOfErrorsFirstLanguage += 1;
                                            _firstToSecond[s1] = new SortedDictionary<string, bool>();
                                        }
                                        else
                                        {
                                            if ("1".Equals(_trainingFirstLanguage[s1].Substring(5, 1)))
                                            {
                                                _totalNumberOfErrorsFirstLanguage += 1;
                                                _trainingFirstLanguage[s1] = _trainingFirstLanguage[s1].Substring(0, 5) + "0"; // +_trainingFirstLanguage[s1].Substring(6);
                                            }
                                        }

                                        if (!_trainingSecondLanguage.ContainsKey(s2))
                                        {
                                            _totalNumberOfErrorsSecondLanguage += 1;
                                            _trainingSecondLanguage[s2] = "111110";

                                            _secondToFirst[s2] = new SortedDictionary<string, bool>();
                                        }
                                        else
                                        {
                                            if ("1".Equals(_trainingSecondLanguage[s2].Substring(5, 1)))
                                            {
                                                _totalNumberOfErrorsSecondLanguage += 1;
                                                _trainingSecondLanguage[s2] = _trainingSecondLanguage[s2].Substring(0, 5) + "0"; // +_trainingSecondLanguage[s2].Substring(6);
                                            }
                                        }

                                        if (!_correctFirstLanguage.ContainsKey(s1))
                                            _correctFirstLanguage[s1] = 0;
                                        else
                                            _correctFirstLanguage[s1] = Math.Max(0,_correctFirstLanguage[s1]-3);


                                        if (!_correctSecondLanguage.ContainsKey(s2))
                                            _correctSecondLanguage[s2] = 0;
                                        else
                                            _correctSecondLanguage[s2] = Math.Max(0, _correctSecondLanguage[s2] - 3);

                                        if (!_firstToSecond[s1].ContainsKey(s2))
                                            _firstToSecond[s1][s2] = false;
                                        if (!_secondToFirst[s2].ContainsKey(s1))
                                            _secondToFirst[s2][s1] = false;
                                    }
                                bSave = true;
                                bRepeat = true;
                                break;

                            case DialogResult.OK:
                                if (string.IsNullOrEmpty(pair.m_tbxFirstLanguage.Text.Trim()))
                                {
                                    firstText = "";
                                    secondText = pair.textBoxSecondLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }
                                if (string.IsNullOrEmpty(pair.textBoxSecondLanguage.Text.Trim()))
                                {
                                    secondText = "";
                                    firstText = pair.m_tbxFirstLanguage.Text.Trim();
                                    bRepeat2 = true;
                                    break;
                                }

                                sss1 = pair.m_tbxFirstLanguage.Text.Replace("?", "?,").Replace("!", "!,").Replace(";", ",").Replace(",,", ",").Replace(",,", ",");
                                sss2 = pair.textBoxSecondLanguage.Text.Replace("?", "?,").Replace("!", "!,").Replace(";", ",").Replace(",,", ",").Replace(",,", ",");

                                foreach (string ss1 in sss1.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                                    foreach (string ss2 in sss2.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        string s1 = ss1.Trim();
                                        string s2 = ss2.Trim();

                                        if (!_trainingFirstLanguage.ContainsKey(s1))
                                        {
                                            _trainingFirstLanguage[s1] = "111110";
                                            _totalNumberOfErrorsFirstLanguage += 1;
                                            _firstToSecond[s1] = new SortedDictionary<string, bool>();
                                        }
                                        else
                                        {
                                            if ("1".Equals(_trainingFirstLanguage[s1].Substring(5, 1)))
                                            {
                                                _totalNumberOfErrorsFirstLanguage += 1;
                                                _trainingFirstLanguage[s1] = _trainingFirstLanguage[s1].Substring(0, 5) + "0"; // +_trainingFirstLanguage[s1].Substring(6);
                                            }
                                        }

                                        if (!_trainingSecondLanguage.ContainsKey(s2))
                                        {
                                            _totalNumberOfErrorsSecondLanguage += 1;
                                            _trainingSecondLanguage[s2] = "111110";

                                            _secondToFirst[s2] = new SortedDictionary<string, bool>();
                                        }
                                        else
                                        {
                                            if ("1".Equals(_trainingSecondLanguage[s2].Substring(5, 1)))
                                            {
                                                _totalNumberOfErrorsSecondLanguage += 1;
                                                _trainingSecondLanguage[s2] = _trainingSecondLanguage[s2].Substring(0, 5) + "0"; // +_trainingSecondLanguage[s2].Substring(6);
                                            }
                                        }

                                        if (!_correctFirstLanguage.ContainsKey(s1))
                                            _correctFirstLanguage[s1] = 0;
                                        else
                                            _correctFirstLanguage[s1] = Math.Max(0, _correctFirstLanguage[s1] - 3);


                                        if (!_correctSecondLanguage.ContainsKey(s2))
                                            _correctSecondLanguage[s2] = 0;
                                        else
                                            _correctSecondLanguage[s2] = Math.Max(0, _correctSecondLanguage[s2] - 3);


                                        if (!_firstToSecond[s1].ContainsKey(s2))
                                            _firstToSecond[s1][s2] = false;
                                        if (!_secondToFirst[s2].ContainsKey(s1))
                                            _secondToFirst[s2][s1] = false;
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

        private void button3_Click(object sender, EventArgs e)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train one of the words randomly. Words with errors get higher weight
                int rnd2 = _rnd2.Next();
                _rnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                int selectedError = rnd2 % (_totalNumberOfErrorsSecondLanguage + _trainingSecondLanguage.Count);

                _skipLast = _trainingSecondLanguage.Count > 10;

                int wordIndex = -1;
                using (SortedDictionary<string,string>.ValueCollection.Enumerator values = _trainingSecondLanguage.Values.GetEnumerator())
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

                    bRepeat = TrainSecondLanguage(wordIndex);
                };
            }
            SaveTrainingProgress();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // decide, if we will train one word randomly, or one that needs additional training
                int rnd = _rnd.Next();
                _rnd = new Random(rnd + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);

                if ((_totalNumberOfErrorsSecondLanguage>0) && (rnd % 100 < 50))
                {
                    // there we train one of the words that need training
                    int rnd2 = _rnd2.Next();
                    _rnd2 = new Random(rnd + rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int selectedError = rnd2 % _totalNumberOfErrorsSecondLanguage;

                    _skipLast = _trainingSecondLanguage.Count > 20;

                    int wordIndex = -1;
                    using (SortedDictionary<string, string>.ValueCollection.Enumerator values = _trainingSecondLanguage.Values.GetEnumerator())
                    {
                        while (selectedError >= 0 && values.MoveNext())
                        {
                            wordIndex += 1;
                            if (values.Current.Contains("0"))
                            {
                                selectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
                            }
                        }

                        bRepeat = TrainSecondLanguage(wordIndex);
                    }
                }
                else
                {
                    // there we train one of the words
                    int rnd2 = _rnd2.Next();
                    _rnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);


                    // calculate mean of correct answers
                    long lTotal = 0;
                    foreach (int i in _correctSecondLanguage.Values)
                        lTotal += i;

                    int mean = (int)((lTotal+_correctSecondLanguage.Count/2) / _correctSecondLanguage.Count);

                    // now calculate the sum of weights of all words
                    int iTotalWeights = 0;
                    foreach (int i in _correctSecondLanguage.Values)
                    {
                        int weight = (i > mean + 3) ? 0 : (mean + 3 - i)*(mean + 3 - i);
                        iTotalWeights += weight;
                    };

                    int selectedWeight = rnd2 % iTotalWeights;

                    int wordIndex = -1;
                    using (SortedDictionary<string, int>.ValueCollection.Enumerator values = _correctSecondLanguage.Values.GetEnumerator())
                    {
                        while (selectedWeight >= 0 && values.MoveNext())
                        {
                            wordIndex += 1;

                            int weight = (values.Current > mean + 3) ? 0 : (mean + 3 - values.Current) * (mean + 3 - values.Current);

                            selectedWeight -= weight;
                        }

                        _skipLast = _trainingSecondLanguage.Count > 10;

                        bRepeat = TrainSecondLanguage(wordIndex);
                    }

                    /*
                    int wordIndex = rnd2 % _trainingSecondLanguage.Count;

                    bRepeat = TrainSecondLanguage(wordIndex);
                     */
                }
            }
            SaveTrainingProgress();
        }

        private void button8_Click(object sender, EventArgs e)
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
                foreach(string s in _trainingSecondLanguage.Values)
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
                    int rnd2 = _rnd2.Next();
                    _rnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int selectedBest = rnd2 % bestCount;

                    wordIndex = -1;
                    foreach (string s in _trainingSecondLanguage.Values)
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
                        _skipLast = false;
                        bRepeat = TrainSecondLanguage(bestIndex);
                    }
                }
            }
            SaveTrainingProgress();
        }



        private bool TrainSecondLanguage(int index)
        {
            bool bRepeat = false;
            bool bVerify = false;
            foreach(KeyValuePair<string,string> pair in _trainingSecondLanguage)
            {
                if (0 == index-- )
                {
                    if (_skipLast)
                    {
                        foreach (string s in _trainedWords)
                        {
                            // if we trained this word recently, then try to skip it
                            if (s.Equals(pair.Key))
                                if (_rnd2.Next(100) > 0)
                                    return true;
                        };

                        // add the word to the list of recently trained words, rotate the list
                        if (_trainedWords.Count > 10)
                            _trainedWords.RemoveLast();
                        _trainedWords.AddFirst(pair.Key);

                    }
                    else
                    {
                        foreach (string s in _trainedWords)
                        {
                            // if we trained this word recently, then try to skip it, but not with that high probability
                            if (s.Equals(pair.Key))
                                if (_rnd2.Next(3) > 0)
                                    return true;
                        }

                        // add the word to the list of recently trained words, rotate the list
                        while (_trainedWords.Count > 3)
                            _trainedWords.RemoveLast();
                        _trainedWords.AddFirst(pair.Key);

                    }


                    using (WordTest test = new WordTest())
                    {
                        test.m_lblShownText.Text = _secondLanguage + ": " + pair.Key;
                        test.m_lblAskedTranslation.Text = _firstLanguage + ":";
                        test.m_lblShownText.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        test.m_lblAskedTranslation.RightToLeft = m_bFirstLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        test.m_tbxAskedTranslation.RightToLeft = m_bFirstLanguageRtl ? RightToLeft.Yes : RightToLeft.No;

                        test.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VokabelTrainer_MouseMove);

                        if (m_cbxReader.SelectedIndex == 0 || m_cbxReader.SelectedIndex == 3)
                            Speaker.Say(_secondLanguage,pair.Key,true);

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

                            char[] separators = { ',' };

                            Dictionary<string, bool> typedIn = new Dictionary<string, bool>();
                            foreach (string s in test.m_tbxAskedTranslation.Text.Trim().Replace("?", "?,").Replace("!", "!,").Replace(";",",").Replace(",,", ",").Replace(",,", ",").Split(separators, StringSplitOptions.RemoveEmptyEntries))
                            {
                                typedIn[s.Trim()] = false;
                            }

                            Dictionary<string, bool> missing = new Dictionary<string, bool>();
                            Dictionary<string, bool> wrong = new Dictionary<string, bool>();

                            foreach (string s in _secondToFirst[pair.Key].Keys)
                            {
                                if (!typedIn.ContainsKey(s))
                                    missing[s] = false;
                            }

                            foreach (string s in typedIn.Keys)
                            {
                                if (!_secondToFirst[pair.Key].ContainsKey(s))
                                {
                                    wrong[s] = false;
                                }
                            }

                            if (missing.Count > 0 || wrong.Count > 0)
                            {
                                string errorMessage = "";
                                if (missing.Count > 1)
                                {
                                    string textToSpeak = "";

                                    errorMessage = Properties.Resources.FollowingMeaningWereMissing;
                                    //errorMessage = "Folgende Bedeutungen haben gefehlt: ";
                                    bool bFirst = true;
                                    foreach (string s in missing.Keys)
                                    {
                                        if (!bFirst)
                                        {
                                            textToSpeak = textToSpeak + ", ";
                                            errorMessage = errorMessage + ", ";
                                        }
                                        else
                                            bFirst = false;

                                        errorMessage = errorMessage + s;
                                        textToSpeak = textToSpeak + s;
                                    }
                                    errorMessage = errorMessage + ". ";

                                    Speaker.Say(_firstLanguage, textToSpeak, true);

                                }
                                else
                                    if (missing.Count == 1)
                                    {
                                        errorMessage = Properties.Resources.FollowingMeaningWasMissing;
                                        //errorMessage = "Folgende Bedeutung hat gefehlt: ";
                                        foreach (string s in missing.Keys)
                                        {
                                            errorMessage = errorMessage + s + ". ";

                                            Speaker.Say(_firstLanguage, s, true);

                                        }
                                    }

                                if (wrong.Count > 0 && !string.IsNullOrEmpty(errorMessage))
                                    errorMessage = errorMessage + "\r\n";

                                if (wrong.Count > 1)
                                {

                                    errorMessage = errorMessage + Properties.Resources.FollowingMeaningsWereWrong;
                                    bool bFirst = true;
                                    foreach (string s in wrong.Keys)
                                    {
                                        if (!bFirst)
                                            errorMessage = errorMessage + ", ";
                                        else
                                            bFirst = false;

                                        errorMessage = errorMessage + s;

                                        if (_trainingFirstLanguage.ContainsKey(s))
                                            RememberResultFirstLanguage(s, false);
                                    }
                                    errorMessage = errorMessage + ". ";
                                }
                                else
                                    if (wrong.Count == 1)
                                    {
                                        errorMessage = errorMessage + Properties.Resources.FollowingMeaningWasWrong;
                                        foreach (string s in wrong.Keys)
                                        {
                                            errorMessage = errorMessage + s + ". ";
                                            if (_trainingFirstLanguage.ContainsKey(s))
                                                RememberResultFirstLanguage(s, false);
                                        }
                                    }

                                if (missing.Count > 0)
                                    NewMessageBox.Show(this, errorMessage, Properties.Resources.Mistake, null);
                                else
                                    MessageBox.Show(errorMessage, Properties.Resources.Mistake, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //MessageBox.Show(errorMessage, "Fehler!", MessageBoxButtons.OK, missing.Count>0? MessageBoxIcon.None:MessageBoxIcon.Error);

                                RememberResultSecondLanguage(pair.Key, false);
                            }
                            else
                            {
                                if (m_cbxReader.SelectedIndex == 1 || m_cbxReader.SelectedIndex == 2)
                                    Speaker.Say(_firstLanguage, test.m_tbxAskedTranslation.Text.Trim(), true);
                                RememberResultSecondLanguage(pair.Key, true);
                            }
                        }
                    }

                    return bRepeat;
                }
            }
            return bRepeat;

        }

        void RememberResultFirstLanguage(string word, bool bCorrect)
        {
            string prevResults = _trainingFirstLanguage[word];
            string newResults = (bCorrect ? "1" : "0") + prevResults.Substring(0, prevResults.Length<5?prevResults.Length:5);
            _trainingFirstLanguage[word] = newResults;
            _totalNumberOfErrorsFirstLanguage += newResults.Length - newResults.Replace("0", "").Length - (prevResults.Length - prevResults.Replace("0", "").Length);

            // if the result was correct and we didn't repeat it because of earlier mistakes, then increment the number of correct answers
            if (bCorrect && prevResults.IndexOf('0')<0)
                _correctFirstLanguage[word]++;

            EnableDisableButtons();
        }


        void RememberResultSecondLanguage(string word, bool bCorrect)
        {
            string prevResults = _trainingSecondLanguage[word];
            string newResults = (bCorrect ? "1" : "0") + prevResults.Substring(0, prevResults.Length < 5 ? prevResults.Length : 5);
            _trainingSecondLanguage[word] = newResults;
            _totalNumberOfErrorsSecondLanguage += newResults.Length - newResults.Replace("0", "").Length - (prevResults.Length - prevResults.Replace("0", "").Length);

            // if the result was correct and we didn't repeat it because of earlier mistakes, then increment the number of correct answers
            if (bCorrect && prevResults.IndexOf('0') < 0)
                _correctSecondLanguage[word]++;

            EnableDisableButtons();
        }


        void SaveVokabulary()
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(_currentPath);

            try
            {

                System.IO.FileInfo fi4 = new System.IO.FileInfo(_currentPath + ".bak");
                if (fi4.Exists)
                    fi4.Delete();


                if (fi.Exists)
                    fi.MoveTo(fi.FullName + ".bak");

                using (System.IO.StreamWriter w = new System.IO.StreamWriter(_currentPath, false, System.Text.Encoding.UTF8))
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
                    w.WriteLine("  <erste-sprache-name>{0}</erste-sprache-name>", _firstLanguage);
                    w.WriteLine("  <zweite-sprache-name>{0}</zweite-sprache-name>", _secondLanguage);
                    w.WriteLine("  <erste-sprache-rtl>{0}</erste-sprache-rtl>", m_bFirstLanguageRtl ? "ja" : "nein");
                    w.WriteLine("  <zweite-sprache-rtl>{0}</zweite-sprache-rtl>", m_bSecondLanguageRtl ? "ja" : "nein");;

                    w.WriteLine();
                    w.WriteLine("  <!-- Allgemeiner Teil: Lizenz für das Vokabelheft -->");
                    w.WriteLine("  <lizenz><modifikationen>{0}</modifikationen>", _saveModifiable ? "Unter Lizenzbedingungen" : "Keine neuen Wörter und keine Lizenzänderungen");
                    w.WriteLine("  <text>{0}</text></lizenz>", _license.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;"));
                    w.WriteLine();
                    w.WriteLine("  <!-- Die Verbindung zwischen den Vokabeln der zwei Sprachen ({0}-{1}) -->", _firstLanguage, _secondLanguage);
                    /*
                    foreach (KeyValuePair<string, SortedDictionary<string, bool>> second in _secondToFirst)
                        foreach (string first in second.Value.Keys)
                            if (first.Length >= spaces.Length)
                                w.WriteLine("  <vokabel-paar><erste-sprache>{0}</erste-sprache><zweite-sprache>{1}</zweite-sprache></vokabel-paar>", first.Trim(), second.Key.Trim());
                            else
                                w.WriteLine("  <vokabel-paar><erste-sprache>{0}</erste-sprache>{2}<zweite-sprache>{1}</zweite-sprache></vokabel-paar>", first.Trim(), second.Key.Trim(), spaces[first.Trim().Length]);
                     */
                    foreach (KeyValuePair<string, SortedDictionary<string, bool>> first in _firstToSecond)
                        foreach (string second in first.Value.Keys)
                            if (second.Length >= spaces.Length)
                                w.WriteLine("  <vokabel-paar><erste-sprache>{0}</erste-sprache><zweite-sprache>{1}</zweite-sprache></vokabel-paar>", first.Key.Trim(), second.Trim());
                            else
                                w.WriteLine("  <vokabel-paar><erste-sprache>{0}</erste-sprache>{2}<zweite-sprache>{1}</zweite-sprache></vokabel-paar>", first.Key.Trim(), second.Trim(), spaces[first.Key.Trim().Length]);

                    w.WriteLine();
                    w.WriteLine("</vokabeln>");
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                try
                {
                    System.IO.FileInfo fi3 = new System.IO.FileInfo(_currentPath);
                    if (fi3.Exists)
                        fi3.Delete();
                }
                catch (Exception)
                {
                };

                try
                {
                    System.IO.FileInfo fi2 = new System.IO.FileInfo(_currentPath + ".bak");
                    if (fi2.Exists)
                        fi.MoveTo(_currentPath);
                }
                catch (Exception)
                {
                };

                NewMessageBox.Show(this, ex.Message, Properties.Resources.Error, 
                    string.Format(Properties.Resources.ErrorWhileSavingVocabularyFile, ex.Message));
                //System.Windows.Forms.MessageBox.Show(ex.Message, "Fehler beim Speichern", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        bool SaveTrainingProgress()
        {
            string currentPath = _currentPath.Replace(".Vokabeln.xml",".Training.xml")
                .Replace(".Vocabulary.xml",".Training.xml");
            System.IO.FileInfo fi = new System.IO.FileInfo(currentPath);

            try
            {

                System.IO.FileInfo fi4 = new System.IO.FileInfo(currentPath + ".bak");
                if (fi4.Exists)
                    fi4.Delete();


                if (fi.Exists)
                    fi.MoveTo(fi.FullName + ".bak");

                using (System.IO.StreamWriter w = new System.IO.StreamWriter(currentPath, false, System.Text.Encoding.UTF8))
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
                    w.WriteLine("  <!-- Die Vokabeln der ersten Sprache ({0}), und deren Trainingsfortschritt 1=richtig 0=falsch -->", _firstLanguage);
                    w.WriteLine("  <!-- Das letzte Training ist am Anfang der Zahl, die jeweils früheren jeweils danach -->");
                    foreach (KeyValuePair<string, string> training in _trainingFirstLanguage)
                        if (training.Key.Length >= spaces.Length)
                            w.WriteLine("  <erste-sprache><vokabel>{0}</vokabel><training-vorgeschichte>{1}</training-vorgeschichte><richtige-antworten>{2}</richtige-antworten></erste-sprache>", training.Key.Trim(), training.Value, _correctFirstLanguage[training.Key]);
                        else
                            w.WriteLine("  <erste-sprache><vokabel>{0}</vokabel>{3}<training-vorgeschichte>{1}</training-vorgeschichte><richtige-antworten>{2}</richtige-antworten></erste-sprache>", training.Key.Trim(), training.Value, _correctFirstLanguage[training.Key], spaces[training.Key.Trim().Length]);
                    w.WriteLine();
                    w.WriteLine("  <!-- Die Vokabeln der zweiten Sprache ({0}), und deren Trainingsfortschritt 1=richtig 0=falsch -->", _secondLanguage);
                    w.WriteLine("  <!-- Das letzte Training ist am Anfang der Zahl, die jeweils früheren jeweils danach -->");
                    foreach (KeyValuePair<string, string> training in _trainingSecondLanguage)
                        if (training.Key.Length >= spaces.Length)
                            w.WriteLine("  <zweite-sprache><vokabel>{0}</vokabel><training-vorgeschichte>{1}</training-vorgeschichte><richtige-antworten>{2}</richtige-antworten></zweite-sprache>", training.Key.Trim(), training.Value, _correctSecondLanguage[training.Key]);
                        else
                            w.WriteLine("  <zweite-sprache><vokabel>{0}</vokabel>{3}<training-vorgeschichte>{1}</training-vorgeschichte><richtige-antworten>{2}</richtige-antworten></zweite-sprache>", training.Key.Trim(), training.Value, _correctSecondLanguage[training.Key], spaces[training.Key.Trim().Length]);
                    w.WriteLine();
                    w.WriteLine("</training>");
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                try
                {
                    System.IO.FileInfo fi3 = new System.IO.FileInfo(currentPath);
                    if (fi3.Exists)
                        fi3.Delete();
                }
                catch (Exception)
                {
                };

                try
                {
                    System.IO.FileInfo fi2 = new System.IO.FileInfo(currentPath + ".bak");
                    if (fi2.Exists)
                        fi.MoveTo(currentPath);
                }
                catch (Exception)
                {
                };

                NewMessageBox.Show(this, ex.Message, Properties.Resources.Error, 
                    string.Format(Properties.Resources.ErrorWhileSavingTrainingFile,ex.Message));
                //System.Windows.Forms.MessageBox.Show(ex.Message, "Fehler beim Speichern", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool TrainFirstLanguage(int index)
        {
            bool bRepeat = false;
            bool bVerify = false;
            foreach (KeyValuePair<string, string> pair in _trainingFirstLanguage)
            {
                if (0 == index--)
                {
                    if (_skipLast)
                    {
                        foreach (string s in _trainedWords)
                        {
                            // if we trained this word recently, then try to skip it
                            if (s.Equals(pair.Key))
                                if (_rnd2.Next(100) > 0)
                                    return true;
                        };

                        // add the word to the list of recently trained words, rotate the list
                        if (_trainedWords.Count > 10)
                            _trainedWords.RemoveLast();
                        _trainedWords.AddFirst(pair.Key);
                    }
                    else
                    {
                        foreach (string s in _trainedWords)
                        {
                            // if we trained this word recently, then try to skip it, but not with that high probability
                            if (s.Equals(pair.Key))
                                if (_rnd2.Next(3) > 0)
                                    return true;
                        }

                        // add the word to the list of recently trained words, rotate the list
                        while (_trainedWords.Count > 3)
                            _trainedWords.RemoveLast();
                        _trainedWords.AddFirst(pair.Key);

                    }


                    using (WordTest test = new WordTest())
                    {
                        test.m_lblShownText.Text = _firstLanguage + ": " + pair.Key;
                        test.m_lblAskedTranslation.Text = _secondLanguage + ":";
                        test.m_lblShownText.RightToLeft = m_bFirstLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        test.m_lblAskedTranslation.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        test.m_tbxAskedTranslation.RightToLeft = m_bSecondLanguageRtl ? RightToLeft.Yes : RightToLeft.No;
                        test.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VokabelTrainer_MouseMove);

                        if (m_cbxReader.SelectedIndex == 0 || m_cbxReader.SelectedIndex == 2)
                            Speaker.Say(_firstLanguage, pair.Key, true);


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
                            foreach (string s in test.m_tbxAskedTranslation.Text.Trim().Replace("?", "?,").Replace("!", "!,").Replace(";",",").Replace(",,", ",").Replace(",,", ",").Split(separators, StringSplitOptions.RemoveEmptyEntries))
                            {
                                typedIn[s.Trim()] = false;
                            }

                            Dictionary<string, bool> missing = new Dictionary<string, bool>();
                            Dictionary<string, bool> wrong = new Dictionary<string, bool>();

                            foreach (string s in _firstToSecond[pair.Key].Keys)
                            {
                                if (!typedIn.ContainsKey(s))
                                    missing[s] = false;
                            }

                            foreach (string s in typedIn.Keys)
                            {
                                if (!_firstToSecond[pair.Key].ContainsKey(s))
                                {
                                    wrong[s] = false;
                                }
                            }

                            if (missing.Count > 0 || wrong.Count > 0)
                            {
                                string errorMessage = "";
                                if (missing.Count > 1)
                                {
                                    string textToSpeak = "";
                                    errorMessage = Properties.Resources.FollowingMeaningsWereMissing;
                                    bool bFirst = true;
                                    foreach (string s in missing.Keys)
                                    {
                                        if (!bFirst)
                                        {
                                            textToSpeak = textToSpeak + ", ";
                                            errorMessage = errorMessage + ", ";
                                        }
                                        else
                                            bFirst = false;

                                        errorMessage = errorMessage + s;
                                        textToSpeak = textToSpeak + s;
                                    }
                                    errorMessage = errorMessage + ". ";

                                    Speaker.Say(_secondLanguage, textToSpeak, true);

                                }
                                else
                                    if (missing.Count == 1)
                                    {
                                        errorMessage = Properties.Resources.FollowingMeaningWasMissing;
                                        foreach (string s in missing.Keys)
                                        {
                                            errorMessage = errorMessage + s + ". ";


                                            Speaker.Say(_secondLanguage, s, true);


                                        }
                                    }

                                if (wrong.Count > 0 && !string.IsNullOrEmpty(errorMessage))
                                    errorMessage = errorMessage + "\r\n";

                                if (wrong.Count > 1)
                                {

                                    errorMessage = errorMessage + Properties.Resources.FollowingMeaningsWereWrong;
                                    bool bFirst = true;
                                    foreach (string s in wrong.Keys)
                                    {
                                        if (!bFirst)
                                            errorMessage = errorMessage + ", ";
                                        else
                                            bFirst = false;

                                        errorMessage = errorMessage + s;

                                        if (_trainingSecondLanguage.ContainsKey(s))
                                            RememberResultSecondLanguage(s, false);
                                    }
                                    errorMessage = errorMessage + ". ";
                                }
                                else
                                    if (wrong.Count == 1)
                                    {
                                        errorMessage = errorMessage + Properties.Resources.FollowingMeaningWasWrong;
                                        foreach (string s in wrong.Keys)
                                        {
                                            errorMessage = errorMessage + s + ". ";
                                            if (_trainingSecondLanguage.ContainsKey(s))
                                                RememberResultSecondLanguage(s, false);
                                        }
                                    }


                                if (missing.Count > 0)
                                    NewMessageBox.Show(this, errorMessage, Properties.Resources.Mistake, null);
                                else
                                    MessageBox.Show(errorMessage, Properties.Resources.Mistake, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //MessageBox.Show(errorMessage, "Fehler!", MessageBoxButtons.OK, missing.Count>0? MessageBoxIcon.None:MessageBoxIcon.Error);

                                RememberResultFirstLanguage(pair.Key, false);
                            }
                            else
                            {
                                if (m_cbxReader.SelectedIndex == 1 || m_cbxReader.SelectedIndex == 3)
                                    Speaker.Say(_secondLanguage, test.m_tbxAskedTranslation.Text.Trim(), true);
                                RememberResultFirstLanguage(pair.Key, true);
                            }
                        }
                    }

                    return bRepeat;
                }
            }
            return bRepeat;


        }


        private void button4_Click(object sender, EventArgs e)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train one of the words randomly. The words with errors get higher weight.
                int rnd2 = _rnd2.Next();
                _rnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                int selectedError = rnd2 % (_totalNumberOfErrorsFirstLanguage + _trainingFirstLanguage.Count);

                _skipLast = _trainingFirstLanguage.Count > 10;

                int wordIndex = -1;
                using (SortedDictionary<string, string>.ValueCollection.Enumerator values = _trainingFirstLanguage.Values.GetEnumerator())
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

        private void button7_Click(object sender, EventArgs e)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // decide, if we will train one word randomly, or one that needs additional training
                int rnd = _rnd.Next();
                _rnd = new Random(rnd + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);

                if ( (_totalNumberOfErrorsFirstLanguage>0) && (rnd % 100 < 50) )
                {
                    // there we train one of the words that need training
                    int rnd2 = _rnd2.Next();
                    _rnd2 = new Random(rnd + rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int selectedError = rnd2 % _totalNumberOfErrorsFirstLanguage;

                    _skipLast = _trainingFirstLanguage.Count > 20;

                    int wordIndex = -1;
                    using (SortedDictionary<string, string>.ValueCollection.Enumerator values = _trainingFirstLanguage.Values.GetEnumerator())
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
                    int rnd2 = _rnd2.Next();
                    _rnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    // calculate mean of correct answers
                    long lTotal = 0;
                    foreach (int i in _correctFirstLanguage.Values)
                        lTotal += i;

                    int mean = (int)( (lTotal + _correctFirstLanguage.Count/2) / _correctFirstLanguage.Count);

                    // now calculate the sum of weights of all words
                    int iTotalWeights = 0;
                    foreach (int i in _correctFirstLanguage.Values)
                    {
                        int weight = (i > mean + 3) ? 0 : (mean + 3 - i) * (mean + 3 - i);

                        iTotalWeights += weight;
                    };

                    int selectedWeight = rnd2 % iTotalWeights;

                    int wordIndex = -1;
                    using (SortedDictionary<string, int>.ValueCollection.Enumerator values = _correctFirstLanguage.Values.GetEnumerator())
                    {
                        while (selectedWeight >= 0 && values.MoveNext())
                        {
                            wordIndex += 1;

                            int weight = (values.Current > mean + 3) ? 0 : (mean + 3 - values.Current) * (mean + 3 - values.Current);

                            selectedWeight -= weight;
                        }

                        _skipLast = _trainingFirstLanguage.Count > 10;

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

        private void button9_Click(object sender, EventArgs e)
        {
            bool bRepeat = true;
            while (bRepeat)
            {
                bRepeat = false;
                // there we train only the words that with errors, and we start with those that had errors recently
                int bestIndex = -1;
                int bestTime = 16;
                int wordIndex = -1;
                int bestCount = 0;
                foreach (string s in _trainingFirstLanguage.Values)
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
                    int rnd2 = _rnd2.Next();
                    _rnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int selectedBest = rnd2 % bestCount;

                    wordIndex = -1;
                    foreach (string s in _trainingFirstLanguage.Values)
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
                        _skipLast = false;

                        bRepeat = TrainFirstLanguage(bestIndex);
                    }
                }
            }
            SaveTrainingProgress();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gnu.de/documents/gpl-2.0.de.html");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gnu.org/licenses/gpl-2.0.html");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (About form = new About())
            {
                form.ShowDialog(this);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void VokabelTrainer_MouseMove(object sender, MouseEventArgs e)
        {
            // make randoms less deterministic, whenever possible
            if (_rnd != null)
                _rnd = new Random(_rnd.Next() + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond + (e.X & 3) * 256);
            if (_rnd2 != null)
                _rnd2 = new Random(_rnd2.Next() + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear + (e.Y & 3) * 256);
        }

    }
}
