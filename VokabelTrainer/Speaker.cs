// VokabelTrainer v1.3
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
        public static void Say(string strLanguage, string text, bool bAsync, bool bUseESpeak, string strESpeakPath)
        {
            string strLanguageFirstTwo = strLanguage.Substring(0,2);
            string strLanguageFirstThree = strLanguageFirstTwo.Substring(0, 3);
            System.Speech.Synthesis.SpeechSynthesizer reader = new System.Speech.Synthesis.SpeechSynthesizer();

            if ((!bUseESpeak || string.IsNullOrEmpty(strESpeakPath)) &&
                ("Rus".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ||
                "Рус".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase)))
            {
                text = (text + " ").Replace(",", " ").Replace("; ", " ").Replace(". ", " ").Replace("?", " ")
                    .Replace("،", ",").Replace("、", ",").Replace("，", ",").Replace("!", " ").Replace("  ", " ").Replace("  ", " ")
                    .Replace("ться", "ца").Replace("тся", "ца").Replace("шого ", "шова ").Replace("того ", "това ").Replace("кого ", "кова ")
                    .Replace("шего ", "шева ").Replace("чего ", "чева ").Replace("рого ", "рова ").Replace("чого ", "чово ").Replace("его ", "ево ")
                    .Replace("чу ", "чю ").Replace("щу ", "щю ").Replace("бъё", "бё").Replace("ое ", "оэ ").Replace("ного ", "нова ").Trim();
                StringBuilder b = new StringBuilder();
                StringBuilder b2 = new StringBuilder();
                b.Append("<phoneme alphabet=\"ups\" ph=\"");
                foreach(char c in text)
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
                string strCurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;
                // decide, if we support speaking this language
                string strSSMLLanguageToSpeak =
                    // english
                    "En".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                        (strCurrentCulture.StartsWith("en") ? "xml:lang='" + strCurrentCulture + "'" : "xml:lang='en-US'") :
                    "Ing".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='en-US'" :
                    "Ан".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='en-US'" :
                    // german
                    "De".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                        (strCurrentCulture.StartsWith("de") ? "xml:lang='" + strCurrentCulture + "'" : "xml:lang='de-DE'") :
                    "Ale".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='de-DE'" :
                    "All".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='de-DE'" :
                    "Ger".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='de-DE'" :
                    "Не".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='de-DE'" :
                    // spanish
                    "Sp".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='es-ES'" :
                    "Es".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                        (strCurrentCulture.StartsWith("es") ? "xml:lang='" + strCurrentCulture + "'" : "xml:lang='es-ES'") :
                    "Ис".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='es-ES'" :
                    // russian
                    "Rus".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='ru-RU'" :
                    "Рус".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ?
                        (strCurrentCulture.StartsWith("ru") ? "xml:lang='" + strCurrentCulture + "'" : "xml:lang='ru-RU'") :
                    "руc".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ?
                        (strCurrentCulture.StartsWith("ru") ? "xml:lang='" + strCurrentCulture + "'" : "xml:lang='ru-RU'") :
                    // french
                    "Fr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                        (strCurrentCulture.StartsWith("fr") ? "xml:lang='" + strCurrentCulture + "'" : "xml:lang='fr-FR'") :
                    "Фр".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='fr-FR'" :
                    // hindi
                    "Hi".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='hi-IN'" :
                    "Ин".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='hi-IN'" :
                    "द्".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='hi-IN'" :
                    "हि".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='hi-IN'" :
                    "भा".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='hi-IN'" :
                    // japanese
                    "Ja".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='ja-JP'" :
                    "Яп".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='ja-JP'" :
                    "日本".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='ja-JP'" :
                    // portugese
                    "Por".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ?
                        (strCurrentCulture.StartsWith("pt") ? "xml:lang='" + strCurrentCulture + "'" : "xml:lang='pt-PT'") :
                    "Пор".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='pt-PT'" :
                    // Italian
                    "It".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ?
                        (strCurrentCulture.StartsWith("it") ? "xml:lang='" + strCurrentCulture + "'" : "xml:lang='it-IT'") :
                    "Ит".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='it-IT'" :
                    // korean
                    "Ko".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='ko-KR'" :
                    "Ко".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='ko-KR'" :
                    "한국".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='ko-KR'" :
                    // chinese
                    "Chi".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='zh-CN'" :
                    "Ки".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='zh-CN'" :
                    "中文".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ?
                        (strCurrentCulture.StartsWith("zh") ? "xml:lang='" + strCurrentCulture + "'" : "xml:lang='zh-CN'") :
                    // arab
                    "Ar".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xml:lang='ar-SA'" :
                    "Ара".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "xml:lang='ar-SA'" :
                    "عر".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ?
                    (strCurrentCulture.StartsWith("ar") ? "xml:lang='" + strCurrentCulture + "'" : "xml:lang='ar-SA'") :
                    // hebrew
                    "He".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='he-IL'" :
                    "עב".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='he-IL'" :
                    "עִ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='he-IL'" :
                    "Ив".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='he-IL'" :
                    // greek
                    "Gr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='el-GR'" :
                    "Гре".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='el-GR'" :
                    "ελ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='el-GR'" :
                    "Ελ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='el-GR'" :
                    // Afrikaans
                    "Af".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='af-ZA'" :
                    "Афр".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='af-ZA'" :
                    // Bosnian bosanski
                    "Bo".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='bs-BA'" :
                    "Бo".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='bs-BA'" :
                    // Català
                    "Ca".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='ca'" :
                    // Czech čeština
                    "Cz".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='cs-CZ'" :
                    "Če".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='cs-CZ'" :
                    "če".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='cs-CZ'" :
                    "Чеш".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='cs-CZ'" :
                    // Danish Dansk
                    "Da".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='da-DK'" :
                    // Finnish Suomi
                    "Fi".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='fi-FI'" :
                    "Su".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='fi-FI'" :
                    "Фи".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='fi-FI'" :
                    // Croatian hrvatski
                    "Cr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='hr-HR'" :
                    "Hr".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='hr-HR'" :
                    // Hungarian magyar
                    "Hu".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='hu-HU'" :
                    "Un".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='hu-HU'" :
                    "Ве".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='hu-HU'" :
                    "Mag".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='hu-HU'" :
                    // Kannada ಕನ್ನಡ
                    "Ka".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='kn-IN'" :
                    "ಕನ".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='kn-IN'" :
                    // Kurdish Kurdî
                    "Ku".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='ku-IQ'" :
                    "Ку".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='ku-IQ'" :
                    // Latvian Lettisch latviski
                    "La".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='lv-LV'" :
                    "Le".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='lv-LV'" :
                    // Dutch Nederlands Niederländisch
                    "Ne".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='nl-NL'" :
                    "Ni".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='nl-NL'" :
                    "Du".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='nl-NL'" :
                    "Го".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='nl-NL'" :
                    // Polish, Polski
                    "Pol".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='pl-PL'" :
                    "Пол".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='pl-PL'" :
                    // Romanian, Rumänisch, română
                    "Ro".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='ro-RO'" :
                    "Rum".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='ro-RO'" :
                    "Рум".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='ro-RO'" :
                    // Slowak
                    "Slovenský".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='sk-SK'" :
                    // Slovenian
                    "Slovenski".Equals(strLanguage, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='sl-SL'" :
                    // Serbian српски
                    "Se".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='sr-RS'" :
                    "Ср".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='sr-RS'" :
                    "Се".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='sr-RS'" :
                    // Swedish Svenska
                    "Sw".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='sv-SE'" :
                    "Sv".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='sv-SE'" :
                    "Шв".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='sv-SE'" :
                    // Tamil தமிழ்
                    "Ta".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='ta-LK'" :
                    "தம".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='ta-LK'" :
                    // Armenian հայկ
                    "Arm".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='hy-AM'" :
                    "Арм".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='hy-AM'" :
                    "հա".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='hy-AM'" :
                    "ՀԱ".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='hy-AM'" :
                    "Հա".Equals(strLanguageFirstTwo, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='hy-AM'" :
                    // Inonesian
                    "Ind".Equals(strLanguageFirstThree, StringComparison.CurrentCultureIgnoreCase) ? "xmk:lang='id-ID'" :
                    // Icelandic íslenskur ÍSLENSKT
                    "Isl".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='id-ID'" :
                    "ÍS".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='id-ID'" :
                    "ís".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='id-ID'" :
                    // Georgian ქართული  ქართველი
                    "Geo".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='ka-GE'" :
                    "ქა".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='ka-GE'" :
                    // Macedonian македонски
                    "Ma".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='mk-MK'" :
                    "Ма".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='mk-MK'" :
                    // Norwegian  norsk
                    "No".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='no-NO'" :
                    // Albanian shqiptare shqipe sq
                    "Alb".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='sq-AL'" :
                    "Shq".Equals(strLanguageFirstThree, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='sq-AL'" :
                    // Vietnamese việt
                    "Vi".Equals(strLanguageFirstTwo, StringComparison.InvariantCultureIgnoreCase) ? "xmk:lang='vi-VI'" :
                    "";


                if (!string.IsNullOrEmpty(strSSMLLanguageToSpeak))
                {
                    string strSsml = string.Format(@"<speak version='1.0' " +
                                                            "xmlns='http://www.w3.org/2001/10/synthesis' {0}>{1}</speak>",
                                                            strSSMLLanguageToSpeak,
                                                            text);
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
