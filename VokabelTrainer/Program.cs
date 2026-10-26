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
using System.Windows.Forms;
using System.Globalization;
using System.Text;

namespace VokabelTrainer
{
    //*******************************************************************************************************
    /// <summary>
    /// Main class 
    /// </summary>
    //*******************************************************************************************************
    static class Program
    {
        //===================================================================================================
        /// <summary>
        /// Indicates that the application runs in a predominantly islamic country;
        /// </summary>
        public static bool IslamicCountry;



        //===================================================================================================
        /// <summary>
        /// Main entry point of the application
        /// </summary>
        //===================================================================================================
        [STAThread]
        static void Main()
        {
#if DEBUG
            // string strSetCulture =
            // "af-ZA";
            // "ar-SA";
            // "as";
            // "az-Latn-AZ";
            // "be-BY";
            // "bg-BG";
            // "bn";
            // "bs-Latn-BA";
            // "cs-CZ";
            // "da-DK";
            // "de-DE";
            // "dz";
            // "el-GR";
            // "en-US";
            // "eo";
            // "es-ES";
            // "et-EE";
            // "fa-IR";
            // "fi-FI";
            // "fil";
            // "fr-FR";
            // "he-IL";
            // "hi-IN";
            // "hr";
            // "hu-HU";
            // "hy-AM";
            // "id-ID";
            // "is-IS";
            // "it-IT";
            // "ja-JP";
            // "ka-GE";
            // "kk-KZ";
            // "kn";
            // "km-KH";
            // "ko-KR";
            // "ky-KG";
            // "la";
            // "lo";
            // "lt-LT";
            // "lv-LV";
            // "mi";
            // "mk-MK";
            // "ml";
            // "mn-MN";
            // "mr";
            // "ms-MY";
            // "my";
            // "ne-NP";
            // "nl-NL";
            // "no-NO";
            // "or";
            // "pa-Arab-PK";
            // "pa-IN";
            // "pl-PL";
            // "prs";
            // "ps-AF";
            // "pt-PT";
            // "quz";
            // "ro-RO";
            // "ru-RU";
            // "sa-IN";
            // "sd-Arab";
            // "sk-SK";
            // "sl-SL";
            // "sq";
            // "sr"; 
            // "sv-SE";
            // "sw";
            // "ta";
            // "tg-Cyrl-TJ";
            // "th-TH";
            // "tk";
            // "tr-TR";
            // "uk-UA";
            // "ur-PK";
            // "uz-Latn-UZ";
            // "vi-VN";
            // "zh-TW";
            // "zh-CN";

            // "ga-IE";
            // "ku-Arab-IQ";
            // "ti-ER";
            // "ti-ET";
            // "am-ET";
            // "ig-NG";
            // "yo-NG";
            // "wo-SN";
            // "so-SO";
            // "rw-RW";
            // System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(strSetCulture);
            // System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(strSetCulture);

            // we need gregorian calender, so the dates are parsed correctly across all cultures
            // System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.Calendar = new GregorianCalendar();
            // System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.Calendar = new GregorianCalendar();
#endif

            var astrIslamicCountries = new[]
            {
                "AF", "DZ", "BH", "BD", "BJ", "BN", "BF", "CM", "TD", "KM",
                "DJ", "EG", "GM", "GN", "ID", "IR", "IQ", "JO", "KZ", "KW",
                "KG", "LB", "LY", "MY", "MV", "ML", "MR", "MA", "MZ", "NE",
                "NG", "OM", "PK", "PS", "QA", "SA", "SN", "SO", "SS", "SD",
                "SY", "TJ", "TG", "TN", "TR", "TM", "AE", "UZ", "EH", "YE"
            };


            string cultureName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            IslamicCountry = Array.Exists(astrIslamicCountries, code => cultureName.Contains(code));

            string strSetCulture = Properties.Settings.Default.ChosenCulture;
            if (!string.IsNullOrEmpty(strSetCulture) && 
                !System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag.StartsWith(strSetCulture+"-"))
            {
                try
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(strSetCulture);
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(strSetCulture);
                }
                catch (Exception oEx)
                {
                    strSetCulture = string.Empty;
                }
            }

            if (string.IsNullOrEmpty(strSetCulture) 
                || Properties.Settings.Default.StartsSinceCultureChange == 1
                || Properties.Settings.Default.StartsSinceCultureChange == 20)
            {
                using (Forms.CultureSelectionForm oCultureDialog =
                    new Forms.CultureSelectionForm(GetAvailableLocalizations()))
                {
                    if (oCultureDialog.ShowDialog() == DialogResult.OK &&
                        !string.IsNullOrEmpty(oCultureDialog.SelectedLanguage))
                    {
                        if (string.IsNullOrEmpty(strSetCulture) || !strSetCulture.Equals(oCultureDialog.SelectedLanguage))
                        {
                            Properties.Settings.Default.ChosenCulture = oCultureDialog.SelectedLanguage;
                            Properties.Settings.Default.StartsSinceCultureChange = 0;
                            Properties.Settings.Default.Save();

                            System.Diagnostics.Process.Start(
                                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

                            return;
                        }
                    }

                    // continue counting for asking another time
                    if (Properties.Settings.Default.StartsSinceCultureChange <= 20)
                    {
                        Properties.Settings.Default.StartsSinceCultureChange++;
                        Properties.Settings.Default.Save();
                    }

                    System.Diagnostics.Process.Start(
                        System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                    return;
                }
            }

            // continue counting for asking another time
            if (Properties.Settings.Default.StartsSinceCultureChange <= 20)
            {
                Properties.Settings.Default.StartsSinceCultureChange++;
                Properties.Settings.Default.Save();
            }


            Application.EnableVisualStyles();
            try
            {
                Application.SetCompatibleTextRenderingDefault(false);
            }
            catch (Exception oEx)
            {
                // ignore
            }
            Application.Run(new VokabelTrainer());
        }


        //===================================================================================================
        /// <summary>
        /// Chooses culture settings from language name
        /// </summary>
        /// <param name="strLanguage">Language name, if possible in its native naming</param>
        /// <returns>Culture code string</returns>
        //===================================================================================================
        public static string LanguageCodeFromName(
            string strLanguage
            )
        {
            string strLanguageFirstTwo = strLanguage.Length >= 2 ? strLanguage.Substring(0, 2) : strLanguage;
            string strLanguageFirstThree = strLanguage.Length >= 3 ? strLanguage.Substring(0, 3) : strLanguage;

            string strCurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;
            // decide, if we support speaking this language
            string strSSMLLanguageToSpeak =

                // english
                "En".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("en") && !strCurrentCulture.Equals("en") ? strCurrentCulture : "en-US") :
                "Ing".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "en-US" :
                "Ан".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "en-US" :

                // german
                "De".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("de") && !strCurrentCulture.Equals("de") ? strCurrentCulture : "de-DE") :
                "Ale".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "de-DE" :
                "All".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "de-DE" :
                "Ger".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "de-DE" :
                "Jer".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "de-DE" :
                "Tys".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "de-DE" :
                "Tus".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "de-DE" :
                "Dui".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "de-DE" :
                "Nem".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "de-DE" :
                "Не".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "de-DE" :
                "Гер".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "de-DE" :
                "Нім".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "de-DE" :

                // Esperanto
                "Esperanto".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "eo" :
                "Есп".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "eo" :
                "Эсп".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "eo" :

                // Spanish
                "Sp".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "es-ES" :
                "Es".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("es") && !strCurrentCulture.Equals("es") ? strCurrentCulture : "es-ES") :
                "Ис".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "es-ES" :

                // russian
                "Rus".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ru-RU" :
                "Рус".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("ru") && !strCurrentCulture.Equals("ru") ? strCurrentCulture : "ru-RU") :
                "руc".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("ru") && !strCurrentCulture.Equals("ru") ? strCurrentCulture : "ru-RU") :

                // french
                "Fr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("fr") && !strCurrentCulture.Equals("fr") ? strCurrentCulture : "fr-FR") :
                "Фр".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "fr-FR" :

                // hindi
                "Hi".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "hi-IN" :
                "Ин".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hi-IN" :
                "द्".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hi-IN" :
                "हि".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hi-IN" :
                "भा".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hi-IN" :

                // japanese
                "Ja".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ja-JP" :
                "Яп".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "ja-JP" :
                "日本".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "ja-JP" :
                "ひら".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "ja-JP" :
                "Rōm".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "ja-JP" :
                "Rôm".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "ja-JP" :
                "Roo".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "ja-JP" :

                // portugese
                "Por".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("pt") && !strCurrentCulture.Equals("pt") ? strCurrentCulture : "pt-PT") :
                "Пор".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "pt-PT" :

                // Italian
                "It".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("it") && !strCurrentCulture.Equals("it") ? strCurrentCulture : "it-IT") :
                "Ит".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "it-IT" :

                // korean 한국어
                "Ko".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ko-KR" :
                "Ко".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "ko-KR" :
                "한국".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "ko-KR" :

                // chinese
                "Chi".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "zh-CN" :
                "Ки".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "zh-CN" :
                "Zhō".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "zh-CN" :
                "简体".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "zh-CN" :
                "中文(简体)".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "zh-CN" :
                "中文(繁體)".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "zh-TW" :
                "中文".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) && strCurrentCulture.Equals("zh-CHT") ? "zh-TW" :
                "中文".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("zh") && !strCurrentCulture.Equals("zh-CHS") && !strCurrentCulture.Equals("zh-CHT") ? strCurrentCulture : "zh-CN") :
                "繁體中".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "zh-TW" :
                "Tái".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "zh-TW" :
                "ㄓㄨˋ".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "zh-TW" :

                // arab العربية
                "Ar".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ar-SA" :
                "Ара".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "ar-SA" :
                "ال".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ?
                (strCurrentCulture.StartsWith("ar") && !strCurrentCulture.Equals("ar") ? strCurrentCulture : "ar-SA") :
                "عر".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ?
                (strCurrentCulture.StartsWith("ar") && !strCurrentCulture.Equals("ar") ? strCurrentCulture : "ar-SA") :

                // hebrew
                "He".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "he-IL" :
                "עב".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "he-IL" :
                "עִ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "he-IL" :
                "Ив".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "he-IL" :

                // greek
                "Gr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "el-GR" :
                "Гре".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "el-GR" :
                "ελ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "el-GR" :
                "Ελ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "el-GR" :
                "Έλ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "el-GR" :

                // Afrikaans
                "Af".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "af-ZA" :
                "Афр".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "af-ZA" :

                // Bosnian bosanski
                "Bos".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "bs-BA" :
                "Бoс".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "bs-BA" :

                // Català
                "Ca".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ca" :
                "Kat".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ca" :
                "Кат".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ca" :

                // Czech čeština
                "Cz".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "cs-CZ" :
                "Če".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "cs-CZ" :
                "če".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "cs-CZ" :
                "Чеш".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "cs-CZ" :

                // Danish Dansk
                "Dan".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "da-DK" :
                "Dän".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "da-DK" :

                // Finnish Suomi
                "Fin".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "fi-FI" :
                "Suo".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "fi-FI" :
                "Фин".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "fi-FI" :

                // Croatian hrvatski
                "Cr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "hr-HR" :
                "Hr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "hr-HR" :

                // Hungarian magyar
                "Hu".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "hu-HU" :
                "Un".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "hu-HU" :
                "Ве".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hu-HU" :
                "Mag".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "hu-HU" :

                // Kannada ಕನ್ನಡ ಕನ್ನಡ
                "Ka".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "kn-IN" :
                "Ка".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "kn-IN" :
                "ಕನ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "kn-IN" :
                "ಕನ್ನ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "kn-IN" :

                // Kurdish/Sorani Kurdî  کوردیی ناوەڕاست
                "Ku".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ku-IQ" :
                "Ку".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "ku-IQ" :
                "Сор".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "ku-IQ" :
                "Sor".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "ku-IQ" :
                "کوردیی ناوەندی".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "ku-IQ" :
                "کوردیی ناوەڕاست".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "ku-IQ" :
                "سۆرانی".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "ku-IQ" :

                // Lietuvių, Lithuanian, Litauisch
                "Lie".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "lt-LT" :
                "Lit".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "lt-LT" :
                "Ли".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "lt-LT" :

                // lingua latīna
                "Lingua latīna".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "la" :
                "Lingua latina".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "la" :
                "Latīna".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "la" :
                "Latina".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "la" :
                "Latin".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "la" :
                "Latein".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "la" :
                "Латинский".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "la" :

                // Latvian Lettisch latviski
                "La".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "lv-LV" :
                "Le".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "lv-LV" :
                "Ла".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "lv-LV" :

                // Eesti, Estonian, Estnisch
                "Ees".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "et-EE" :
                "Est".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "et-EE" :
                "Ист".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "et-EE" :

                // Dutch Nederlands Niederländisch
                "Ne".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "nl-NL" :
                "Ni".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "nl-NL" :
                "Dut".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "nl-NL" :
                "Го".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "nl-NL" :

                // Polish, Polski
                "Pol".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "pl-PL" :
                "Пол".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "pl-PL" :

                // Romanian, Rumänisch, română
                "Ro".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ro-RO" :
                "Rum".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ro-RO" :
                "Рум".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "ro-RO" :

                // Slowak slovenčina
                "Slovenčina".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sk-SK" :
                "Slovenský".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sk-SK" :
                "Slovakisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sk-SK" :
                "Slovak".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sk-SK" :

                // Slovenian Slovenščina 
                "Slovenski".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sl-SL" :
                "Slovenščina".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sl-SL" :
                "Slovenian".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sl-SL" :
                "Slovenisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sl-SL" :

                // Serbian српски srpski
                "Se".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "sr-RS" :
                "Ср".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "sr-RS" :
                "Се".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "sr-RS" :
                "srpski".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "sr-RS" :

                // Swedish Svenska
                "Swe".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sv-SE" :
                "Sv".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "sv-SE" :
                "Schwedisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sv-SE" :
                "Шв".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "sv-SE" :

                // Tamil தமிழ்
                "Ta".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ta-LK" :
                "Та".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ta-LK" :
                "தம".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ta-LK" :

                // Armenian հայկ Հայերեն
                "Arm".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "hy-AM" :
                "Арм".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "hy-AM" :
                "հա".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hy-AM" :
                "ՀԱ".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hy-AM" :
                "Հա".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hy-AM" :


                // Inonesian Bahasa Indonesia
                "Ind".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "id-ID" :
                "Bahasa Indonesia".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "id-ID" :

                // Malaisisch Malay Bahasa Melayu
                "Bahasa Melayu".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "ms-MY" :
                "Mal".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "ms-MY" :
                "Melayu".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "ms-MY" :

                // Filipino Wikang Filipino
                "Wik".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "fil-PH" :
                "Fil".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "fil-PH" :
                "Фил".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "fil-PH" :

                // Lao ລາວ
                "Lao".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "lo-LA" :
                "ພາສາລາວ".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "lo-LA" :
                "ລາວ".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "lo-LA" :

                // Papua
                "Tok Pisin".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "tpi-PG" :

                // New Zealand "Te Reo Māori"
                "Te Reo Māori".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "mi-NZ" :
                "Te Reo".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "mi-NZ" :
                "Maori".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "mi-NZ" :
                "Māori".Equals(strLanguage, StringComparison.CurrentCultureIgnoreCase) ? "mi-NZ" :

                // Icelandic íslenskur ÍSLENSKT
                "Isl".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "is-IS" :
                "ÍS".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "is-IS" :
                "ís".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "is-IS" :
                "Исл".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "is-IS" :

                // Georgian ქართული  ქართველი 
                "Geo".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ka-GE" :
                "Гру".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ka-GE" :
                "ქა".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ka-GE" :

                // Macedonian македонски
                "Ma".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "mk-MK" :
                "Ма".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "mk-MK" :

                // Norwegian  norsk 
                "No".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "no-NO" :
                "Нор".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "no-NO" :

                // Irish Gaeilge Irisch
                "Iri".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ga-IE" :
                "Gae".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ga-IE" :
                "Ирл".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ga-IE" :

                // Albanian shqiptare shqipe sq shqip
                "Alb".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sq-AL" :
                "Shq".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sq-AL" :
                "Алб".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sq-AL" :

                // Vietnamese việt Tiếng Việt
                "Vi".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "vi-VI" :
                "Tiế".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "vi-VI" :
                "Вье".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "vi-VI" :

                // Urkrainian Українська
                "Uk".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "uk-UA" :
                "Ук".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "uk-UA" :

                // Thai ไทย
                "ไทย".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "th-TH" :
                "Tha".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "th-TH" :
                "Tai".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "th-TH" :
                "Тай".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "th-TH" :

                // Khmer ខ្មែរ (កម្ពុជា)
                "ភា".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "km-KH" :
                "ខ្".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "km-KH" :
                "Khm".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "km-KH" :

                // Persian/Farsi فارسی
                "فا".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "fa-IR" :
                "Per".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "fa-IR" :
                "Far".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "fa-IR" :
                "Фар".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "fa-IR" :
                "Пер".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "fa-IR" :

                // Dari دری
                "در".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "prs-AF" :
                "Dar".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "prs-AF" :
                "Дар".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "prs-AF" :
                "Афг".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "prs-AF" :

                // Pashtu پښتو
                "پښ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ps-AF" :
                "Pas".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ps-AF" :
                "Паш".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ps-AF" :

                // Punjabi-Pakistan پنجابی  پنجابی
                "پنجابی".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "pa-PK" :
                "Pun".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "pa-PK" :
                "Пан".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "pa-PK" :

                // Punjabi - India ਪੰਜਾਬੀ
                "ਪੰਜਾਬੀ".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "pa-IN" :

                // Urdu اُردُو  اُردو
                "اُرد".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ur-PK" :
                "Urd".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ur-PK" :

                // Tigrinya ትግርኛ 
                "ትግ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ti-ER" :
                "Tig".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ti-ER" :
                "Тиг".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ti-ER" :

                // Wolof làkk  
                "Вол".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "wo-SN" :
                "Wol".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "wo-SN" :

                // Amharish አማርኛ
                "አማርኛ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "am-ET" :
                "Amh".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "am-ET" :
                "Амх".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "am-ET" :

                // Swahili Kiswahili
                "Kis".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sw-TZ" :
                "Swa".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sw-TZ" :
                "Сва".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sw-TZ" :

                // Mongolian "Монгол хэл"
                "Mon".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "mn-MN" :
                "Мон".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "mn-MN" :

                // Chechen "Нохчийн мотт" - let there be peace...
                "Нох".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ce-RU" :

                // Kazakh "Qazaqşa"  қазaқ
                "Каз".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "kk-KZ" :
                "қаз".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "kk-KZ" :
                "Qaz".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "kk-KZ" :

                // Kyrgyz Кыргызча 
                "Кыр".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ky-KG" :
                "Кир".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ky-KG" :
                "Kir".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ky-KG" :
                "Kyr".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ky-KG" :

                // Tajik Тоҷикӣ 
                "Тоҷ".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "tg-TJ" :
                "Тад".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "tg-TJ" :
                "Taj".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "tg-TJ" :
                "Tad".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "tg-TJ" :

                // Turkmen Türkmençe Türkmen
                "Turkmen".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tk-TM" :
                "Türkmenisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tk-TM" :
                "Türkmençe".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tk-TM" :
                "Türkmen".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tk-TM" :
                "Tуркменский".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tk-TM" :

                // Turkish Türkçe
                "Turkish".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tr-TR" :
                "Türkisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tr-TR" :
                "Turc".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tr-TR" :
                "Turco".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tr-TR" :
                "Türkçe".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tr-TR" :
                "Турецкий".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "tr-TR" :

                // Uzbek Oʻzbekcha
                "Uzbek".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "uz-UZ" :
                "Usbekisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "uz-UZ" :
                "Узбекский".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "uz-UZ" :
                "Oʻzbekcha".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "uz-UZ" :

                // Burmese "မြန်မာဘာသာ" မြန်မာစာ မြန်မာစကား  (Birma/Myanma) ဗမာ
                "မြန်မာဘာသာ".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "my-MM" :
                "မြန်မာစာ".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "my-MM" :
                "မြန်မာစကား".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "my-MM" :
                "ဗမာ".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "my-MM" :
                "Burmese".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "my-MM" :
                "Birmanisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "my-MM" :

                // Bengali Bangla বাংলা
                "বাংলা".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "bn-BD" :
                "Bengali".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "bn-BD" :
                "Bangla".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "bn-BD" :
                "Бенгальский".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "bn-BD" :

                // Nepali "नेपाली" 
                "नेपाली".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "ne-NP" :
                "Nepali".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "ne-NP" :
                "Nepalesisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "ne-NP" :
                "Непальский".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "ne-NP" :

                // Assamese অসমীয়া
                "অসমীয়া".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "as-IN" :
                "Assamese".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "as-IN" :
                "Assamesisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "as-IN" :

                // Azerbaijani
                "Azərbaycan".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "az-AZ" :
                "Azerbaijani".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "az-AZ" :
                "Aserbaijanisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "az-AZ" :
                "Азербайджанский".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "az-AZ" :

                // Belarussian
                "Беларуская".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "be-BY" :
                "Belarusian".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "be-BY" :
                "Weißrussisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "be-BY" :
                "Belarussisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "be-BY" :
                "Беларусский".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "be-BY" :

                // Bulgarian
                "Български".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "bg-BG" :
                "Bulgarian".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "bg-BG" :
                "Болгарский".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "bg-BG" :
                "Bulgarisch".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "bg-BG" :

                // Tibetan
                "བོ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "bo-CN" :
                "Tib".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "bo-CN" :
                "Тиб".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "bo-CN" :

                // Dzongkha
                "རྫོ".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "dz" :
                "Джо".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "dz" :
                "Dzo".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "dz" :

                // Asụsụ Igbo
                "Asụsụ Igbo".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "ig" :
                "Igb".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ig" :

                // Malailam
                "മലയാളം".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "ml-IN" :
                "Malaylam".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "ml-IN" :

                // Marathi मराठी
                "मराठी".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "mr-IN":
                "Marathi".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "mr-IN" :

                // Odia Oriya ଓଡ଼ିଆ
                "ଓଡ଼ିଆ".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "or-IN" :
                "Odiya".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "or-IN" :
                "Oriya".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "or-IN" :

                // Quechua Runasimi
                "Runasimi".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "quz" :
                "Quechua".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "quz" :

                // Kinyarwanda
                "Kinyarwanda".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "rw-RW" :

                // Sanskrit संस्कृत
                "संस्कृत".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sa-IN" :
                "San".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sa-IN" :
                "Сан".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sa-IN" :

                // Sindhi سنڌي
                "سنڌي".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sd" :
                "Sindhi".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sd" :

                // Soomaali Somali
                "Soomaali".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "so" :
                "Somali".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "so" :

                // Èdè Yorùbá
                "Èdè Yorùbá".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "yo" :
                "Yorùbá".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "yo" :
                "Yoruba".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "yo" :

                // unknown
                "";

            return strSSMLLanguageToSpeak;
        }

        /*
        af	Afrikaans	Afrikaans
        am	አማርኛ	Amharic
        ar	العربية	Arabic
        as	অসমীয়া	Assamese
        az	azərbaycan	Azerbaijani
        be	Беларуская	Belarusian
        bg	български	Bulgarian
        bn	বাংলা		Bangla
        bo-CN	བོད་ཡིག (ཀྲུང་ཧྭ་མི་དམངས་སྤྱི་མཐུན་རྒྱལ་ཁབ།)		Tibetan (PRC)
        bs	bosanski	Bosnian
        ca	català	Catalan
        cs	čeština	Czech
        da	dansk	Danish
        de	Deutsch	German
        dz	རྫོང་ཁ	Dzongkha
        el	Ελληνικά	Greek
        eo	esperanto	Esperanto
        es	español	Spanish
        et	eesti	Estonian
        fa	فارسی	Persian
        fi	suomi	Finnish
        fil	Filipino	Filipino
        fr	français	French
        ga-IE	Gaeilge (Éire)	Irish (Ireland)
        he	עברית	Hebrew
        hi	हिन्दी	Hindi
        hr	hrvatski	Croatian
        hu	magyar	Hungarian
        hy	Հայերեն	Armenian
        id	Indonesia	Indonesian
        ig	Asụsụ Igbo	Igbo
        is	íslenska	Icelandic
        it	italiano	Italian
        ja	日本語	Japanese
        ka	ქართული		Georgian
        kk	қазақ тілі	Kazakh
        km-KH	ខ្មែរ (កម្ពុជា)	Khmer (Cambodia)
        kn	ಕನ್ನಡ	Kannada
        ko	한국어	Korean
        ku-Arab-IQ	کوردیی ناوەڕاست (عێراق)	Central Kurdish (Iraq)
        ky	кыргызча	Kyrgyz
        la	lingua latīna	Latin
        lo	ລາວ	Lao
        lt	lietuvių	Lithuanian
        lv	latviešu	Latvian
        mi	te reo Māori	Maori
        mk	македонски	Macedonian
        ml	മലയാളം	Malayalam
        mn-MN	монгол (Монгол)	Mongolian (Cyrillic, Mongolia)
        mr	मराठी	Marathi
        ms	Melayu	Malay
        my	ဗမာ	Burmese
        ne	नेपाली	Nepali
        nl	Nederlands	Dutch
        no	norsk	Norwegian
        or	ଓଡ଼ିଆ		Odia
        pa-Arab-PK	پنجابی (پاکستان)	Punjabi (Islamic Republic of Pakistan)
        pa-IN	ਪੰਜਾਬੀ (ਭਾਰਤ)	Punjabi (India)
        pl	polski	Polish
        prs	درى	Dari
        ps	پښتو	Pashto
        pt	português	Portuguese
        quz	Runasimi	Quechua
        ro	română	Romanian
        ru	русский	Russian
        rw	Kinyarwanda	Kinyarwanda
        sa	संस्कृत	Sanskrit
        sd-Arab	سنڌي	Sindhi (Arabic)
        sk	slovenčina	Slovak
        sl	slovenščina	Slovenian
        so	Soomaali	Somali
        sq	shqip	Albanian
        sr	srpski	Serbian
        sv	svenska	Swedish
        sw	Kiswahili	Kiswahili
        ta	தமிழ்	Tamil
        tg	Тоҷикӣ	Tajik
        th	ไทย	Thai
        ti	ትግርኛ	Tigrinya
        tk	Türkmen dili	Turkmen
        tr	Türkçe	Turkish
        uk	українська	Ukrainian
        ur-PK	اُردو (پاکستان)	Urdu (Islamic Republic of Pakistan)
        uz	o‘zbek	Uzbek
        vi	Tiếng Việt	Vietnamese
        wo	Wolof	Wolof
        yo	Èdè Yorùbá	Yoruba
        zh-CHS	中文(简体) 旧版	Chinese (Simplified) Legacy
        zh-CHT	中文(繁體) 舊版	Chinese (Traditional) Legacy

         */
        //===================================================================================================
        /// <summary>
        /// Scans the application's base directory for satellite localization assemblies and
        /// returns a dictionary of available localization cultures.
        /// Key: culture name string suitable for `new CultureInfo(key)` (lowercase).
        /// Value: culture native name (self-name), e.g. "Deutsch (Deutschland)".
        /// </summary>
        /// <returns>
        /// Dictionary of available localizations where the key is the culture code
        /// and the value is the culture native name.
        /// </returns>
        //===================================================================================================
        public static System.Collections.Generic.SortedDictionary<string, string> GetAvailableLocalizations()
        {
            // dictionary to return: keys are culture codes (lowercase), values are native names
            System.Collections.Generic.SortedDictionary<string, string> oCultureCodes =
                new System.Collections.Generic.SortedDictionary<string, string>(
                    System.StringComparer.InvariantCultureIgnoreCase);

            // base directory of the running application
            string strBaseDir = AppDomain.CurrentDomain.BaseDirectory;

            // assembly name used to compute satellite resource file name (e.g. "VokabelTrainer.resources.dll")
            string strAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string strResourceFileName = strAssemblyName + ".resources.dll";

            // get all cultures known to .NET
            System.Globalization.CultureInfo[] aCultures =
                System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures);

            StringBuilder oAllCultures = new StringBuilder();
            // iterate through cultures and check for corresponding satellite assembly
            foreach (System.Globalization.CultureInfo oCulture in aCultures)
            {
                // safety: skip null cultures (shouldn't happen) and invariant culture (empty Name)
                if (oCulture == null)
                {
                    continue;
                }

                string strCultureName = oCulture.Name; // e.g. "de-DE"
                if (string.IsNullOrEmpty(strCultureName))
                {
                    // invariant culture has empty Name -> no subdirectory expected
                    if (!oCultureCodes.ContainsKey("en"))
                    {
                        oCultureCodes.Add("en", "English");
                    }
                    continue;
                }

                // expected directory for satellite assemblies
                string strCultureDir = System.IO.Path.Combine(strBaseDir, strCultureName);

                // if culture directory does not exist, skip
                if (!System.IO.Directory.Exists(strCultureDir))
                {
                    continue;
                }

                // expected path to satellite resource assembly
                string strResourcePath = System.IO.Path.Combine(strCultureDir, strResourceFileName);

                // if resources assembly not present, skip
                if (!System.IO.File.Exists(strResourcePath))
                {
                    continue;
                }

                // use the culture's native name as the display value (self-name)
                string strNativeName = oCulture.NativeName; // e.g. "Deutsch (Deutschland)"

                // add to dictionary if not already present
                if (!oCultureCodes.ContainsKey(strCultureName))
                {
                    oCultureCodes.Add(strCultureName, 
                        char.ToUpper(strNativeName[0], oCulture) + strNativeName.Substring(1));

                    oAllCultures.Append(oCulture.Name + "\t" + oCulture.NativeName + "\t" + oCulture.DisplayName + Environment.NewLine);
                }
            }

            string strAllCultures = oAllCultures.ToString();
            return oCultureCodes;
        }
    }
}
