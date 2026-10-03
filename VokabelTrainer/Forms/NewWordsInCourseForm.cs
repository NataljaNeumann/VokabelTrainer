// VokabelTrainer v1.7
// Copyright (C) 2026 NataljaNeumann@gmx.de
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using VokabelTrainer.Properties;

namespace VokabelTrainer.Forms
{
    //*******************************************************************************************************
    /// <summary>
    /// The form for showing new words in a course
    /// </summary>
    //*******************************************************************************************************
    public partial class NewWordsInCourseForm : Form, IDisposable
    {
        //===================================================================================================
        /// <summary>
        /// The font for words
        /// </summary>
        Font m_oFont;

        //===================================================================================================
        /// <summary>
        /// The font for language names
        /// </summary>
        Font m_oHeaderFont;

        //===================================================================================================
        /// <summary>
        /// Whether the form is in help mode (cursor = help)
        /// </summary>
        private bool m_bHelpMode = false;

        //===================================================================================================
        /// <summary>
        /// URLs for searching first word
        /// </summary>
        List<string> m_oUrlsForFirst = new List<string>();
        //===================================================================================================
        /// <summary>
        /// URLs for searching second word
        /// </summary>
        List<string> m_oUrlsForSecond = new List<string>();

        //===================================================================================================
        /// <summary>
        /// Search counts of first words
        /// </summary>
        Dictionary<string, int> m_oSearchCountsFirst = new Dictionary<string, int>();
        //===================================================================================================
        /// <summary>
        /// Search counts of first words
        /// </summary>
        Dictionary<string, int> m_oSearchCountsSecond = new Dictionary<string, int>();


        //===================================================================================================
        /// <summary>
        /// Constructs a new form object
        /// </summary>
        /// <param name="strHeading">The heading of the object</param>
        /// <param name="strFirstLanguage">The name of the first language</param>
        /// <param name="strSecondLanguage">The name of the second language</param>
        /// <param name="oWordPairs">New word pairs</param>
        /// <param name="bSay">Whether to speak words</param>
        /// <param name="bUseESpeak">Whether to use eSpeak</param>
        /// <param name="strESpeakPath">Path to eSpeak</param>
        /// <param name="oHelpCallback">Optional callback invoked on help request</param>
        //===================================================================================================
        public NewWordsInCourseForm(
            string strHeading,
            string strFirstLanguage,
            string strSecondLanguage,
            IEnumerable<KeyValuePair<string, string>> oWordPairs,
            bool bSay,
            bool bUseESpeak,
            string strESpeakPath
            )
        {

            InitializeComponent();

            // Wiktionary
            Dictionary<string,string> oWiktionaryLinks = new Dictionary<string,string>();
            oWiktionaryLinks["af"] = "https://af.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["am"] = "https://am.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ar"] = "https://ar.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["as"] = "https://as.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["az"] = "https://az.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["be"] = "https://be.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["bg"] = "https://bg.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["bn"] = "https://bn.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["bo"] = "https://bo.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["bs"] = "https://bs.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ca"] = "https://ca.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["cs"] = "https://cs.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["da"] = "https://da.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["de"] = "https://de.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["dz"] = "https://dz.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["dz"] = "https://dz.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["el"] = "https://el.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["en"] = "https://en.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["eo"] = "https://eo.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["es"] = "https://es.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["fa"] = "https://fa.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["dz"] = "https://dz.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["fi"] = "https://fi.wiktionary.org/w/index.php?search={0}";
            //oWikiLinks["fil"] = "https://fil.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["fr"] = "https://fr.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ga"] = "https://ga.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["he"] = "https://he.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["hi"] = "https://hi.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["hr"] = "https://hr.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["hu"] = "https://hu.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["hy"] = "https://hy.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["id"] = "https://id.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ig"] = "https://ig.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["is"] = "https://is.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["it"] = "https://it.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ja"] = "https://ja.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ka"] = "https://ka.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["kk"] = "https://kk.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["km"] = "https://km.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["kn"] = "https://kn.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ko"] = "https://ko.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ku"] = "https://ckb.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ky"] = "https://ky.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["la"] = "https://la.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["lo"] = "https://lo.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["lt"] = "https://lt.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["lv"] = "https://lv.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["mi"] = "https://mi.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["mk"] = "https://mk.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ml"] = "https://ml.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["mn"] = "https://mn.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["mr"] = "https://mr.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ms"] = "https://ms.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["my"] = "https://my.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ne"] = "https://ne.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["nl"] = "https://nl.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["no"] = "https://no.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["or"] = "https://or.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["pa"] = "https://pa.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["pl"] = "https://pl.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["prs"] = "https://fa.wiktionary.org/w/index.php?search={0}";
            //oWikiLinks["ps"] = "https://ps.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["pt"] = "https://pt.wiktionary.org/w/index.php?search={0}";
            //oWikiLinks["quz"] = "https://quz.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ro"] = "https://ro.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ru"] = "https://ru.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["rw"] = "https://rw.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["sa"] = "https://sa.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["es"] = "https://es.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["sd"] = "https://sd.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["sk"] = "https://sk.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["sl"] = "https://sl.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["so"] = "https://so.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["sq"] = "https://sq.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["sr"] = "https://sr.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["sv"] = "https://sv.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["sw"] = "https://sw.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ta"] = "https://ta.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["tg"] = "https://tg.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["th"] = "https://th.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ti"] = "https://ti.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["tk"] = "https://tk.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["tr"] = "https://tr.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["uk"] = "https://uk.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["ur"] = "https://ur.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["uz"] = "https://uz.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["vi"] = "https://vi.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["wo"] = "https://wo.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["yo"] = "https://yo.wiktionary.org/w/index.php?search={0}";
            oWiktionaryLinks["zh"] = "https://zh.wiktionary.org/w/index.php?search={0}";


            string strFirstLanguageCode = Program.LanguageCodeFromName(strFirstLanguage).Length >= 2 ?
                    Program.LanguageCodeFromName(strFirstLanguage).Substring(0, 2) : "";
            string strSecondLanguageCode = Program.LanguageCodeFromName(strSecondLanguage).Length >= 2 ?
                    Program.LanguageCodeFromName(strSecondLanguage).Substring(0, 2) : "";

            if (oWiktionaryLinks.ContainsKey(strFirstLanguageCode))
            {
                m_oUrlsForFirst.Add(oWiktionaryLinks[strFirstLanguageCode]);
            }

            if (oWiktionaryLinks.ContainsKey(strSecondLanguageCode))
            {
                m_oUrlsForSecond.Add(oWiktionaryLinks[strSecondLanguageCode]);
            }

            if (oWiktionaryLinks.ContainsKey(strFirstLanguageCode))
            {
                m_oUrlsForSecond.Add(oWiktionaryLinks[strFirstLanguageCode]);
            }

            if (oWiktionaryLinks.ContainsKey(strSecondLanguageCode))
            {
                m_oUrlsForFirst.Add(oWiktionaryLinks[strSecondLanguageCode]);
            }

            // Leo
            bool bGermanIsOneOfLanguages = 
                "de".Equals(strFirstLanguageCode) || 
                "de".Equals(strSecondLanguageCode);
            bool bEnglishIsOneOfLanguages = 
                "en".Equals(strFirstLanguageCode) || 
                "en".Equals(strSecondLanguageCode);
            bool bFrenchIsOneOfLanguages = 
                "fr".Equals(strFirstLanguageCode) || 
                "fr".Equals(strSecondLanguageCode);
            bool bItalianIsOneOfLanguages = 
                "it".Equals(strFirstLanguageCode) || 
                "it".Equals(strSecondLanguageCode);
            bool bSpanishIsOneOfLanguages = 
                "es".Equals(strFirstLanguageCode) || 
                "es".Equals(strSecondLanguageCode);
            bool bRussianIsOneOfLanguages = 
                "ru".Equals(strFirstLanguageCode) || 
                "ru".Equals(strSecondLanguageCode);
            bool bPortugeseIsOneOfLanguages = 
                "pt".Equals(strFirstLanguageCode) || 
                "pt".Equals(strSecondLanguageCode);
            bool bChineseIsOneOfLanguages = 
                "zh".Equals(strFirstLanguageCode) || 
                "zh".Equals(strSecondLanguageCode);
            bool bPolishIsOneOfLanguages = 
                "pl".Equals(strFirstLanguageCode) || 
                "pl".Equals(strSecondLanguageCode);

            if (bGermanIsOneOfLanguages)
            {
                if (bEnglishIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/englisch-deutsch/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/englisch-deutsch/{0}");
                }
                if (bFrenchIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/franz%C3%B6sisch-deutsch/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/franz%C3%B6sisch-deutsch/{0}");
                }
                if (bSpanishIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/spanisch-deutsch/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/spanisch-deutsch/{0}");
                }
                if (bPortugeseIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/alem%C3%A3o-portugu%C3%AAs/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/alem%C3%A3o-portugu%C3%AAs/{0}");
                }
                if (bItalianIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/italienisch-deutsch/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/italienisch-deutsch/{0}");
                }
                if (bChineseIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/chinesisch-deutsch/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/chinesisch-deutsch/{0}");
                }
                if (bRussianIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/russisch-deutsch/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/russisch-deutsch/{0}");
                }
                if (bPolishIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/polnisch-deutsch/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/polnisch-deutsch/{0}");
                }
            }
            else if (bEnglishIsOneOfLanguages)
            {
                if (bSpanishIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/spanish-english/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/spanish-english/{0}");
                }

                if (bFrenchIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/french-english/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/french-english/{0}");
                }

                if (bRussianIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/russian-english/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/russian-english/{0}");
                }
            }
            else if (bSpanishIsOneOfLanguages)
            {
                if (bPortugeseIsOneOfLanguages)
                {
                    m_oUrlsForFirst.Add("https://dict.leo.org/portugu%C3%A9s-espa%C3%B1ol/{0}");
                    m_oUrlsForSecond.Add("https://dict.leo.org/portugu%C3%A9s-espa%C3%B1ol/{0}");
                }
            }

            // Search queries in crawlers
            m_oUrlsForFirst.Add(SearchEngineRouter.GetSearchUrl(
                string.Format(Resources.WhatIsTheMeaningOf, "{0}", strFirstLanguage))
                .Replace("%7B0%7D","{0}"));
            m_oUrlsForSecond.Add(SearchEngineRouter.GetSearchUrl(
                string.Format(Resources.WhatIsTheMeaningOf, "{0}", strSecondLanguage))
                .Replace("%7B0%7D", "{0}"));

            {
                HashSet<string> oList = new HashSet<string>();
                foreach (KeyValuePair<string, string> oPair in oWordPairs)
                {
                    if (!oList.Contains(oPair.Key))
                    {
                        oList.Add(oPair.Key);
                    }
                }

                if (oList.Count > 1)
                {
                    string[] astrWords = oList.ToArray();

                    if (astrWords.Length > 1)
                        m_oUrlsForFirst.Add(SearchEngineRouter.GetSearchUrl(
                            string.Format(Resources.WhatAreMeaningsOf, string.Join(", ", astrWords), strFirstLanguage)));
                }
            }

            {
                HashSet<string> oList = new HashSet<string>();
                foreach (KeyValuePair<string, string> oPair in oWordPairs)
                {
                    if (!oList.Contains(oPair.Value))
                    {
                        oList.Add(oPair.Value);
                    }
                }

                if (oList.Count > 1)
                {
                    string[] astrWords = oList.ToArray();

                    if (astrWords.Length > 1)
                        m_oUrlsForSecond.Add(SearchEngineRouter.GetSearchUrl(
                            string.Format(Resources.WhatIsTheMeaningOf, string.Join(", ", astrWords), strSecondLanguage)));
                }
            }


            // Set up fonts
            m_oFont = new Font(family: Font.FontFamily, emSize: 22, style: FontStyle.Regular);
            m_oHeaderFont = new Font(family: Font.FontFamily, emSize: 22, style: FontStyle.Underline | FontStyle.Bold);

            if (bSay)
            {
                if (Program.LanguageCodeFromName(strFirstLanguage).Length >= 2 &&
                    Program.LanguageCodeFromName(strFirstLanguage).Substring(0, 2).Equals(
                    System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName))
                {
                    StringBuilder oAllWords = new StringBuilder();
                    foreach (KeyValuePair<string, string> oKVP in oWordPairs)
                    {
                        if (oAllWords.ToString().IndexOf(oKVP.Value) < 0)
                        {
                            if (oAllWords.Length > 0)
                                oAllWords.Append(", ");
                            oAllWords.Append(oKVP.Value);
                        }
                    }

                    Speaker.Say(Program.LanguageCodeFromName(strSecondLanguage),
                        oAllWords.ToString(),
                        true,
                        bUseESpeak,
                        strESpeakPath);

                }
                else
                if (Program.LanguageCodeFromName(strSecondLanguage).Length >= 2 &&
                    Program.LanguageCodeFromName(strSecondLanguage).Substring(0, 2).Equals(
                    System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName))
                {
                    StringBuilder oAllWords = new StringBuilder();
                    foreach (KeyValuePair<string, string> oKVP in oWordPairs)
                    {
                        if (oAllWords.ToString().IndexOf(oKVP.Key) < 0)
                        {
                            if (oAllWords.Length > 0)
                                oAllWords.Append(", ");
                            oAllWords.Append(oKVP.Key);
                        }
                    }

                    Speaker.Say(Program.LanguageCodeFromName(strFirstLanguage),
                        oAllWords.ToString(),
                        true,
                        bUseESpeak,
                        strESpeakPath);
                }
            }

            // Create a TableLayoutPanel for two columns
            var ctlTable = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(20),
            };

            ctlTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            ctlTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            // Header labels
            var lblFirstLang = new Label
            {
                Text = strFirstLanguage,
                Font = m_oHeaderFont,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Right,
                Margin = new Padding(0, 0, 30, 10)
            };
            var lblSecondLang = new Label
            {
                Text = strSecondLanguage,
                Font = m_oHeaderFont,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 0, 0, 10)
            };



            ctlTable.Controls.Add(lblFirstLang, 0, 0);
            ctlTable.Controls.Add(lblSecondLang, 1, 0);

            int nRow = 1;
            foreach (var oPair in oWordPairs)
            {
                ctlTable.RowCount++;

                var lblFirst = new Label
                {
                    Text = oPair.Key,
                    Font = m_oFont,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleRight,
                    Anchor = AnchorStyles.Right,
                    Margin = new Padding(0, 0, 30, 5),
                    Tag = "first"
                };
                var lblSecond = new Label
                {
                    Text = oPair.Value,
                    Font = m_oFont,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0, 0, 0, 5),
                    Tag = "second"
                };

                if (m_oUrlsForFirst.Count == 0)
                {
                    lblFirstLang.Cursor = Cursors.No;
                }
                if (m_oUrlsForSecond.Count == 1)
                {
                    lblSecondLang.Cursor = Cursors.No;
                }

                // Attach click handlers for help mode; these will only act when help mode is active
                lblFirst.Click += PairLabel_Click;
                lblSecond.Click += PairLabel_Click;

                ctlTable.Controls.Add(lblFirst, 0, nRow);
                ctlTable.Controls.Add(lblSecond, 1, nRow);
                nRow++;
            }

            Controls.Add(ctlTable);
            Text = strHeading;

            if (!string.IsNullOrEmpty(Resources.OkButtonText))
                m_btnOk.Text = Resources.OkButtonText;


            // Center ctlTable in the form and resize form to fit ctlTable
            ctlTable.Location = ClientRectangle.Location;
            this.MinimumSize = this.Size = new Size(
                ctlTable.PreferredSize.Width + Width - ClientRectangle.Width,
                ctlTable.PreferredSize.Height + Height - ClientRectangle.Height +
                m_btnOk.Height + (ClientRectangle.Height - m_btnOk.Bottom) * 2);
            this.AutoSize = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Decide, vertical or horizontal image
            if (this.MinimumSize.Width * 6 < this.MinimumSize.Height * 5)
            {
                // For islamic countries: try the islamic learning header
                if (Program.IslamicCountry &&
                    ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderMuslim.jpg"))
                {
                    return;
                }

                // the IETF language tag for decisions about the headers
                string strIetf = System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;

                // If language indicates it is eastern Asia, then try east Asia header
                if ((strIetf.StartsWith("ja") || strIetf.StartsWith("zh") || strIetf.StartsWith("ms") ||
                    strIetf.StartsWith("id") || strIetf.StartsWith("fil") || strIetf.StartsWith("ko") ||
                    strIetf.StartsWith("lo")) &&
                    ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderEastAsia.jpg"))
                {
                    return;
                }

                // If language indicates it is Africa, then try African header
                if ((strIetf.StartsWith("yo") || strIetf.StartsWith("ig") || strIetf.StartsWith("so") ||
                    strIetf.StartsWith("af") || strIetf.StartsWith("am") || strIetf.StartsWith("ig") ||
                    strIetf.StartsWith("wo") || strIetf.StartsWith("rw") || strIetf.StartsWith("ti") ||
                    strIetf.StartsWith("sw")) &&
                    ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderAfrica.jpg"))
                {
                    return;
                }

                // If language indicates it is latin America or India environment, then try latin American header
                if ((strIetf.StartsWith("es") || strIetf.StartsWith("pt") ||
                    strIetf.StartsWith("hi") || strIetf.StartsWith("ne") ||
                    strIetf.StartsWith("hi") || strIetf.StartsWith("sa") ||
                    strIetf.StartsWith("bo") || strIetf.StartsWith("dz") ||
                    strIetf.StartsWith("hi") || strIetf.StartsWith("ml") ||
                    strIetf.StartsWith("mr") || strIetf.StartsWith("or"))
                    && !strIetf.Equals("es-ES") && !strIetf.Equals("pt-PT") &&
                    ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderLatinAmerica.jpg"))
                {
                    return;
                }


                // Try the standard learning header
                if (ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeader.jpg"))
                {
                    return;
                }

                // In case of failure, then try all headers, maybe still one exists
                if (ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderEastAsia.jpg"))
                {
                    return;
                }

                if (ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderLatinAmerica.jpg"))
                {
                    return;
                }

                if (ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderMuslim.jpg"))
                {
                    return;
                }

                if (ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderAfrica.jpg"))
                {
                    return;
                }
            }
            else
            {

                // For islamic countries: try the islamic learning header
                if (Program.IslamicCountry &&
                    ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderMuslim.jpg"))
                {
                    return;
                }

                // the IETF language tag for decisions about the headers
                string strIetf = System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;

                // If language indicates it is eastern Asia, then try east Asia header
                if ((strIetf.StartsWith("ja") || strIetf.StartsWith("zh") || strIetf.StartsWith("ms") ||
                    strIetf.StartsWith("id") || strIetf.StartsWith("fil") || strIetf.StartsWith("ko") ||
                    strIetf.StartsWith("lo")) &&
                    ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderEastAsia.jpg"))
                {
                    return;
                }

                // If language indicates it is Africa, then try African header
                if ((strIetf.StartsWith("yo") || strIetf.StartsWith("ig") || strIetf.StartsWith("so") ||
                    strIetf.StartsWith("af") || strIetf.StartsWith("am") || strIetf.StartsWith("ig") ||
                    strIetf.StartsWith("wo") || strIetf.StartsWith("rw") || strIetf.StartsWith("ti") ||
                    strIetf.StartsWith("sw")) &&
                    ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderAfrica.jpg"))
                {
                    return;
                }

                // If language indicates it is latin America or india environment, then try latin American header
                if ((strIetf.StartsWith("es") || strIetf.StartsWith("pt") ||
                    strIetf.StartsWith("hi") || strIetf.StartsWith("ne") ||
                    strIetf.StartsWith("hi") || strIetf.StartsWith("sa") ||
                    strIetf.StartsWith("bo") || strIetf.StartsWith("dz") ||
                    strIetf.StartsWith("hi") || strIetf.StartsWith("ml") ||
                    strIetf.StartsWith("mr") || strIetf.StartsWith("or"))
                    && !strIetf.Equals("es-ES") && !strIetf.Equals("pt-PT") &&
                    ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderLatinAmerica.jpg"))
                {
                    return;
                }


                // Try the standard learning header
                if (ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeader.jpg"))
                {
                    return;
                }

                // In case of failure, then try all headers, maybe still one exists
                if (ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderEastAsia.jpg"))
                {
                    return;
                }

                if (ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderLatinAmerica.jpg"))
                {
                    return;
                }

                if (ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderMuslim.jpg"))
                {
                    return;
                }

                if (ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderAfrica.jpg"))
                {
                    return;
                }
            }

        }

        //===================================================================================================
        /// <summary>
        /// Disposes the object and its disposablee contents
        /// </summary>
        //===================================================================================================
        public void Dispose()
        {
            base.Dispose();
            m_oFont.Dispose();

            if (m_oLoadedImage != null)
            {
                m_oLoadedImage.Dispose();
            }
        }


        #region side image injection part
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
        private bool ReadyToUseSideImageInjection(
            string strImageName
            )
        {
            bool bResult = false;
            string strImagePath = System.IO.Path.Combine(Application.StartupPath, strImageName);
            if (System.IO.File.Exists(strImagePath))
            {
                m_oOriginalPositions = new Dictionary<Control, int>();
                foreach (Control ctl in Controls)
                {
                    m_oOriginalPositions[ctl] = ctl.Left;
                }

                m_ctlPictureBox = new PictureBox();
                m_ctlPictureBox.Location = this.ClientRectangle.Location;
                m_ctlPictureBox.Size = new Size(0, 0);
                Controls.Add(m_ctlPictureBox);

                LoadAndResizeSideImage(strImagePath);

                bResult = true;

                this.Resize += new EventHandler(ResizeSideImageAlongWithForm);
            }

            return bResult;
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image along with the form
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void ResizeSideImageAlongWithForm(
            object oSender,
            EventArgs oEventArgs
            )
        {
            ResizeSideImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Loads an image and resizes it to the width of client area
        /// </summary>
        /// <param name="strImagePath"></param>
        //===================================================================================================
        private void LoadAndResizeSideImage(
            string strImagePath
            )
        {
            m_oLoadedImage = Image.FromFile(strImagePath);
            ResizeSideImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image and shifts other elements
        /// </summary>
        //===================================================================================================
        private void ResizeSideImageAndShiftElements()
        {
            if (m_oLoadedImage != null)
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    float fAspectRatio = (float)m_oLoadedImage.Width / (float)m_oLoadedImage.Height;

                    int nNewHeight = this.ClientSize.Height;
                    if (nNewHeight != 0)
                    {
                        int nNewWidth = (int)(nNewHeight * fAspectRatio);

                        int nWidthChange = nNewWidth - m_ctlPictureBox.Width;

                        this.m_ctlPictureBox.Image = new Bitmap(m_oLoadedImage, nNewWidth, nNewHeight);
                        this.m_ctlPictureBox.Size = new Size(nNewWidth, nNewHeight);

                        if (nWidthChange != 0)
                        {
                            this.Width += nWidthChange;
                            ShiftOtherElementsLeftOrRight(nWidthChange);
                        }
                    }
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Shifts elements, apart from the image box up or down
        /// </summary>
        /// <param name="nNewPictureWidth">New width of the picture</param>
        //===================================================================================================
        private void ShiftOtherElementsLeftOrRight(
            int nWidthChange
            )
        {
            foreach (Control ctl in m_oOriginalPositions.Keys)
            {
                if ((ctl.Anchor & AnchorStyles.Right) == AnchorStyles.None)
                {
                    ctl.Left += nWidthChange;
                }
                else
                    if ((ctl.Anchor & AnchorStyles.Left) != AnchorStyles.None)
                {
                    ctl.Left += nWidthChange;
                    //ctl.Width -= nWidthChange;
                }
            }
        }
        #endregion

        #region image injection part
        //===================================================================================================
        /// <summary>
        /// Loads an image from application startup path and shows it at the top of the window
        /// </summary>
        /// <param name="strName">Name of the image, without directory specifications</param>
        //===================================================================================================
        private bool ReadyToUseImageInjection(
            string strImageName
            )
        {
            bool bResult = false;
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

                bResult = true;

                this.Resize += new EventHandler(ResizeImageAlongWithForm);
            }

            return bResult;
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image along with the form
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void ResizeImageAlongWithForm(
            object oSender,
            EventArgs oEventArgs
            )
        {
            ResizeImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Loads an image and resizes it to the width of client area
        /// </summary>
        /// <param name="strImagePath"></param>
        //===================================================================================================
        private void LoadAndResizeImage(
            string strImagePath
            )
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

                        this.Height += nHeightChange;
                        ShiftOtherElementsUpOrDown(nNewHeight);
                    }
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Shifts elements, apart from the image box up or down
        /// </summary>
        /// <param name="nNewPictureHeight">New height of the picture</param>
        //===================================================================================================
        private void ShiftOtherElementsUpOrDown(
            int nNewPictureHeight
            )
        {
            foreach (Control ctl in m_oOriginalPositions.Keys)
            {
                if ((ctl.Anchor & AnchorStyles.Bottom) == AnchorStyles.None)
                    ctl.Top = m_oOriginalPositions[ctl] + nNewPictureHeight;
            }
        }
        #endregion


        //===================================================================================================
        /// <summary>
        /// Handles key down events to close the form on Space, Enter, or Escape
        /// </summary>
        /// <param name="roMsg">The reference to processed message</param>
        /// <param name="oKeyData">Key event arguments</param>
        //===================================================================================================
        protected override bool ProcessCmdKey(
            ref Message roMsg,
            Keys oKeyData
            )
        {
            if (oKeyData == Keys.Space || oKeyData == Keys.Enter || oKeyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref roMsg, oKeyData);
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks on the OK button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_btnOk_Click(
            object oSender, 
            EventArgs oEventArgs
            )
        {
            this.Close();
        }

        /*
         PSEUDOCODE / PLAN (step-by-step):
         1. Toggle help mode state (m_bHelpMode) as before and set the cursor accordingly.
         2. If we just entered help mode, try to obtain screen coordinates from oEventArgs.MousePos.
            - If MousePos is not empty/invalid, convert it to find the deepest child control under that screen point.
         3. To find the deepest control:
            - Convert the screen point to the root control's client coordinates with PointToClient.
            - Call GetChildAtPoint to find the immediate child at that point.
            - If there is a child, recurse into that child with the same screen point.
            - If no child is found, verify whether the root itself is a Label and contains the point.
            - Return the deepest found control (or null if none).
         4. If the deepest control is a Label and has Tag "first" or "second", directly invoke PairLabel_Click
            with that control as sender and EventArgs.Empty to simulate the help click.
         5. Surround conversions and lookups with try/catch so help handling never throws.
         6. Leave existing cursor / m_bHelpMode behavior intact; PairLabel_Click will exit help mode after handling.
        */

        //===================================================================================================
        /// <summary>
        /// This is executed when user presses F1 key
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Even args</param>
        //===================================================================================================
        private void OnHelpRequested(object oSender, HelpEventArgs oEventArgs)
        {
            m_bHelpMode = !m_bHelpMode;
            try
            {
                Cursor = m_bHelpMode ? Cursors.Help : Cursors.Default;

                if (m_bHelpMode)
                {
                    try
                    {
                        // Validate mouse position from event args (screen coordinates)
                        Point mouseScreen = oEventArgs != null ? oEventArgs.MousePos : Point.Empty;
                        if (mouseScreen != Point.Empty && mouseScreen != new Point(-1, -1))
                        {
                            // Find the deepest control under the screen point, starting from the form
                            Control HitControl(Control root, Point screenPoint)
                            {
                                Point ptClient = root.PointToClient(screenPoint);
                                Control child = root.GetChildAtPoint(ptClient);
                                if (child == null)
                                {
                                    // If the root itself is a label and contains the point, return it
                                    if (root is Label && root.ClientRectangle.Contains(ptClient))
                                        return root;
                                    return null;
                                }

                                // Recurse into the child; if recursion finds deeper control, prefer it
                                Control deeper = HitControl(child, screenPoint);
                                return deeper ?? child;
                            }

                            Control ctlHit = HitControl(this, mouseScreen);

                            if (ctlHit is Label lbl)
                            {
                                // Only react for translation labels (they are tagged "first" or "second")
                                var tag = (lbl.Tag as string) ?? string.Empty;
                                if (string.Equals(tag, "first", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(tag, "second", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Call PairLabel_Click directly to handle the help request for this label
                                    PairLabel_Click(lbl, EventArgs.Empty);
                                    // PairLabel_Click will exit help mode and restore cursor
                                    return;
                                }
                            }
                        }
                    }
                    catch
                    {
                        // Swallow any exceptions - help request must not break UI
                    }
                }
            }
            catch { }
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when one of the labels with translations is clicked
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Even args</param>
        //===================================================================================================
        private void PairLabel_Click(object oSender, EventArgs oEventArgs)
        {
            // Validate sender
            var ctlSender = oSender as Control;
            if (ctlSender == null)
                return;

            // Only handle clicks when help mode is active
            if (!m_bHelpMode)
                return;

            // Determine which label was clicked
            string strWhich = (ctlSender.Tag as string) ?? "first";
            string strText = ctlSender.Text;

            // Increment appropriate counter
            string strUrlToOpen = null;
            int nHelpCount;
            if (string.Equals(strWhich, "first", StringComparison.OrdinalIgnoreCase))
            {
                if (!m_oSearchCountsFirst.ContainsKey(strText))
                    nHelpCount = m_oSearchCountsFirst[strText] = 0;
                else
                    nHelpCount = ++m_oSearchCountsFirst[strText];

                if (nHelpCount > 0 && nHelpCount >= m_oUrlsForFirst.Count)
                    nHelpCount = m_oSearchCountsFirst[strText] = 0;

                if (nHelpCount < m_oUrlsForFirst.Count)
                {
                    strUrlToOpen = string.Format(m_oUrlsForFirst[nHelpCount], 
                        Uri.EscapeDataString(strText));
                }
            }
            else
            {


                if (!m_oSearchCountsSecond.ContainsKey(strText))
                    nHelpCount = m_oSearchCountsSecond[strText] = 0;
                else
                    nHelpCount = ++m_oSearchCountsSecond[strText];

                if (nHelpCount > 0 && nHelpCount >= m_oUrlsForSecond.Count)
                    nHelpCount = m_oSearchCountsSecond[strText] = 0;

                if (nHelpCount < m_oUrlsForSecond.Count)
                {
                    strUrlToOpen = string.Format(m_oUrlsForSecond[nHelpCount], 
                        Uri.EscapeDataString(strText));
                }
            }

            try
            {
                this.Cursor = Cursors.AppStarting;
            } catch
            {

            }

            if (!string.IsNullOrEmpty(strUrlToOpen))
            {
                System.Diagnostics.Process.Start(strUrlToOpen);
            }


            // Fallback: show a temporary tooltip if no external callback handled the request

            /*

                var tt = new ToolTip();
                // Show tooltip slightly above the control
                tt.Show($"Help ({strWhich}): {ctl.Text}", ctl, 0, -20, 3000);

                // Use a timer to dispose the tooltip after it is no longer needed
                var timer = new Timer { Interval = 3200 };
                timer.Tick += (s, args) =>
                {
                    timer.Stop();
                    timer.Dispose();
                    try
                    {
                        tt.Dispose();
                    }
                    catch { }
                };
                timer.Start();
            */

            // Exit help mode after a help click and restore cursor
            m_bHelpMode = false;
            try
            {
                this.Cursor = Cursors.Default;
            }
            catch { }
        }
    }
}
