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
using System.Globalization;

namespace VokabelTrainer
{
    //=======================================================================================================
    /// <summary>
    /// Finds appropriate search engine for current culture
    /// </summary>
    //=======================================================================================================
    public static class SearchEngineRouter
    {
        //===================================================================================================
        /// <summary>
        /// Search engines, based on availability, popularity
        /// </summary>
        private static readonly Dictionary<string, string> s_oSearchEngines = new Dictionary<string, string>()
        {
            // Region-specific search engines
            { "zh-CN", "https://www.baidu.com/s?wd=" },
            { "zh-CHS", "https://www.baidu.com/s?wd=" },
            { "zh-CHT", "https://www.baidu.com/s?wd=" },
            { "ru", "https://yandex.ru/search/?text=" },
            { "ko", "https://search.naver.com/search.naver?query=" },
            { "ja", "https://www.google.co.jp/search?q=" },
            { "de", "https://www.google.de/search?q=" },
            { "fr", "https://www.google.fr/search?q=" },
            { "es", "https://www.google.es/search?q=" },
            { "pt", "https://www.google.pt/search?q=" },
            { "tr", "https://www.google.com.tr/search?q=" },
            { "uk", "https://www.google.com.ua/search?q=" },
            { "ar", "https://www.google.com.sa/search?q=" },
            { "vi", "https://www.google.com.vn/search?q=" },
            { "id", "https://www.google.co.id/search?q=" },
            { "th", "https://www.google.co.th/search?q=" },
            { "ms", "https://www.google.com.my/search?q=" },
            { "hi", "https://www.google.co.in/search?q=" },
            { "it", "https://www.google.it/search?q=" },
            { "nl", "https://www.google.nl/search?q=" },
            { "pl", "https://www.google.pl/search?q=" },
            { "sv", "https://www.bing.com/search?q=" },
            { "no", "https://www.bing.com/search?q=" },
            { "fi", "https://www.bing.com/search?q=" },
            { "en-US", "https://www.bing.com/search?q=" },
            { "en-GB", "https://www.bing.com/search?q=" },
            { "en", "https://www.bing.com/search?q=" },

            // Fallbacks for less common locales
            { "af", "https://www.bing.com/search?q=" },
            { "am-ET", "https://www.bing.com/search?q=" },
            { "az", "https://www.bing.com/search?q=" },
            { "be", "https://www.bing.com/search?q=" },
            { "bg", "https://www.bing.com/search?q=" },
            { "bo-CN", "https://www.bing.com/search?q=" },
            { "bs-Latn-BA", "https://www.bing.com/search?q=" },
            { "ca", "https://www.bing.com/search?q=" },
            { "cs-CZ", "https://www.google.cz/search?q=" },
            { "da", "https://www.google.dk/search?q=" },
            { "el", "https://www.google.gr/search?q=" },
            { "et", "https://www.bing.com/search?q=" },
            { "fa", "https://www.bing.com/search?q=" },
            { "he", "https://www.google.co.il/search?q=" },
            { "hr", "https://www.bing.com/search?q=" },
            { "hu", "https://www.google.hu/search?q=" },
            { "hy", "https://www.bing.com/search?q=" },
            { "ig-NG", "https://www.bing.com/search?q=" },
            { "is", "https://www.bing.com/search?q=" },
            { "ka", "https://www.bing.com/search?q=" },
            { "kk", "https://www.bing.com/search?q=" },
            { "km-KH", "https://www.bing.com/search?q=" },
            { "ku-Arab-IQ", "https://www.bing.com/search?q=" },
            { "ky-KG", "https://www.bing.com/search?q=" },
            { "lt", "https://www.google.lt/search?q=" },
            { "lv", "https://www.google.lv/search?q=" },
            { "mk", "https://www.bing.com/search?q=" },
            { "mn-MN", "https://www.bing.com/search?q=" },
            { "pa-Arab-PK", "https://www.bing.com/search?q=" },
            { "pa-IN", "https://www.google.co.in/search?q=" },
            { "ps-AF", "https://www.bing.com/search?q=" },
            { "ro", "https://www.google.ro/search?q=" },
            { "rw-RW", "https://www.bing.com/search?q=" },
            { "sa", "https://www.bing.com/search?q=" },
            { "sk", "https://www.google.sk/search?q=" },
            { "sl", "https://www.google.si/search?q=" },
            { "so-SO", "https://www.bing.com/search?q=" },
            { "sr", "https://www.bing.com/search?q=" },
            { "tg-Cyrl-TJ", "https://www.bing.com/search?q=" },
            { "ti-ER", "https://www.bing.com/search?q=" },
            { "tk-TM", "https://www.bing.com/search?q=" },
            { "uz", "https://www.bing.com/search?q=" },
            { "wo-SN", "https://www.bing.com/search?q=" },
            { "yo-NG", "https://www.bing.com/search?q=" }
        };

        //===================================================================================================
        /// <summary>
        /// Returns search Urls for particular cultures
        /// </summary>
        /// <param name="strSearchQuery">String to search for</param>
        /// <returns>The Url to open</returns>
        //===================================================================================================
        public static string GetSearchUrl(string strSearchQuery)
        {
            var strCulture = CultureInfo.CurrentUICulture.Name;
            var strLang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            string strBaseUrl = s_oSearchEngines.ContainsKey(strCulture)
                ? s_oSearchEngines[strCulture]
                : s_oSearchEngines.ContainsKey(strLang)
                    ? s_oSearchEngines[strLang]
                    : "https://www.bing.com/search?q=";

            return strBaseUrl + Uri.EscapeDataString(strSearchQuery);
        }
    }
}
