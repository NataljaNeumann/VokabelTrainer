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
using System.Windows.Forms;
using System.Globalization;

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
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        //===================================================================================================
        [STAThread]
        static void Main()
        {
#if DEBUG
             //string strSetCulture =
                // "af-ZA";
                // "ar-SA";
                // "az-Latn-AZ";
                // "be-BY";
                // "bg-BG";
                // "bs-Latn-BA";
                // "cs-CZ";
                // "da-DK";
                // "de-DE";
                // "el-GR";
                // "es-ES";
                // "et-EE";
                // "fa-IR";
                // "fi-FI";
                // "fr-FR";
                // "he-IL";
                // "hi-IN";
                // "hu-HU";
                // "hy-AM";
                // "id-ID";
                // "is-IS";
                // "it-IT";
                // "ja-JP";
                // "ka-GE";
                // "kk-KZ";
                // "km-KH";
                // "ko-KR";
                // "ky-KG";
                // "lt-LT";
                // "lv-LV";
                // "mk-MK";
                // "mn-MN";
                // "ms-MY";
                // "nl-NL";
                // "no-NO";
                // "pa-Arab-PK";
                // "pa-IN";
                // "pl-PL";
                // "ps-AF";
                // "pt-PT";
                // "en-US";
                // "ro-RO";
                // "ru-RU";
                // "sa-IN";
                // "sk-SK";
                // "sl-SL";
                // "sr-Cyrl-RS"; // TODO: need a fix
                // "sv-SE";
                // "tg-Cyrl-TJ";
                // "th-TH";
                // "tr-TR";
                // "uk-UA";
                // "uz-Latn-UZ";
                // "vi-VN";
                // "zh-TW";
                // "zh-CN";

            // System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(strSetCulture);
            // System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(strSetCulture);
#endif


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VokabelTrainer());
        }


        //===================================================================================================
        /// <summary>
        /// Chooses culture settings from language name
        /// </summary>
        /// <param name="strLanguage">Language name, if possible in its native naming</param>
        /// <returns>Culture code string</returns>
        //===================================================================================================
        public static string LanguageCodeFromName(string strLanguage)
        {
            string strLanguageFirstTwo = strLanguage.Length >= 2 ? strLanguage.Substring(0, 2) : strLanguage;
            string strLanguageFirstThree = strLanguage.Length >= 3 ? strLanguage.Substring(0, 3) : strLanguage;

            string strCurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;
            // decide, if we support speaking this language
            string strSSMLLanguageToSpeak =
                // english
                "En".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("en") ? strCurrentCulture : "en-US") :
                "Ing".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "en-US" :
                "Ан".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "en-US" :
                // german
                "De".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("de") ? strCurrentCulture : "de-DE") :
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
                // spanish
                "Sp".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "es-ES" :
                "Es".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("es") ? strCurrentCulture : "es-ES") :
                "Ис".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "es-ES" :
                // russian
                "Rus".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "ru-RU" :
                "Рус".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("ru") ? strCurrentCulture : "ru-RU") :
                "руc".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("ru") ? strCurrentCulture : "ru-RU") :
                // french
                "Fr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("fr") ? strCurrentCulture : "fr-FR") :
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
                // portugese
                "Por".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("pt") ? strCurrentCulture : "pt-PT") :
                "Пор".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "pt-PT" :
                // Italian
                "It".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("it") ? strCurrentCulture : "it-IT") :
                "Ит".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "it-IT" :
                // korean
                "Ko".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ko-KR" :
                "Ко".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "ko-KR" :
                "한국".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "ko-KR" :
                // chinese
                "Chi".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "zh-CN" :
                "Ки".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "zh-CN" :
                "中文".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("zh") ? strCurrentCulture : "zh-CN") :
                // arab
                "Ar".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ar-SA" :
                "Ара".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "ar-SA" :
                "عر".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ?
                (strCurrentCulture.StartsWith("ar") ? strCurrentCulture : "ar-SA") :
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
                "Bo".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "bs-BA" :
                "Бo".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "bs-BA" :
                // Català
                "Ca".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ca" :
                // Czech čeština
                "Cz".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "cs-CZ" :
                "Če".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "cs-CZ" :
                "če".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "cs-CZ" :
                "Чеш".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "cs-CZ" :
                // Danish Dansk
                "Da".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "da-DK" :
                // Finnish Suomi
                "Fi".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "fi-FI" :
                "Su".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "fi-FI" :
                "Фи".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "fi-FI" :
                // Croatian hrvatski
                "Cr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "hr-HR" :
                "Hr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "hr-HR" :
                // Hungarian magyar
                "Hu".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "hu-HU" :
                "Un".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "hu-HU" :
                "Ве".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hu-HU" :
                "Mag".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "hu-HU" :
                // Kannada ಕನ್ನಡ
                "Ka".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "kn-IN" :
                "ಕನ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "kn-IN" :
                // Kurdish Kurdî
                "Ku".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ku-IQ" :
                "Ку".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "ku-IQ" :
                // Latvian Lettisch latviski
                "La".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "lv-LV" :
                "Le".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "lv-LV" :
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
                // Slowak
                "Slovenský".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sk-SK" :
                // Slovenian
                "Slovenski".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "sl-SL" :
                // Serbian српски
                "Se".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "sr-RS" :
                "Ср".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "sr-RS" :
                "Се".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "sr-RS" :
                // Swedish Svenska
                "Sw".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "sv-SE" :
                "Sv".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "sv-SE" :
                "Шв".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "sv-SE" :
                // Tamil தமிழ்
                "Ta".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ta-LK" :
                "தம".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ta-LK" :
                // Armenian հայկ
                "Arm".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "hy-AM" :
                "Арм".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "hy-AM" :
                "հա".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hy-AM" :
                "ՀԱ".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hy-AM" :
                "Հա".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "hy-AM" :
                // Inonesian
                "Ind".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "id-ID" :
                // Icelandic íslenskur ÍSLENSKT
                "Isl".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "id-ID" :
                "ÍS".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "id-ID" :
                "ís".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "id-ID" :
                // Georgian ქართული  ქართველი
                "Geo".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ka-GE" :
                "ქა".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "ka-GE" :
                // Macedonian македонски
                "Ma".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "mk-MK" :
                "Ма".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "mk-MK" :
                // Norwegian  norsk
                "No".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "no-NO" :
                // Albanian shqiptare shqipe sq
                "Alb".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sq-AL" :
                "Shq".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "sq-AL" :
                // Vietnamese việt
                "Vi".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "vi-VI" :
                "";

            return strSSMLLanguageToSpeak;
        }
    }
}
