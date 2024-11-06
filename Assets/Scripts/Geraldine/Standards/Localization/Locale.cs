using Geraldine.Standards.Localization.Databases;
using Geraldine.Standards.Localization.Infos;
using System;
using UnityEngine;
using Geraldine.Standards.Localization;
using System.Linq;

namespace Geraldine.HexEngine.Localization
{
    public static class Locale
    {
        const string STR_LOCALIZATION_KEY = "locale";
        const string STR_LOCALIZATION_PREFIX = "localization/";

        static SystemLanguage currentLanguage = SystemLanguage.Unknown;
        static TextAsset currentLocalizationText;

        /// <summary>
        /// This sets the current language. It expects a standard .Net CultureInfo.Name format
        /// </summary>
        public static SystemLanguage CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                if (value != currentLanguage)
                {
                    currentLanguage = value;
                }
            }
        }

        /// <summary>
        /// The player language. If not set in PlayerPrefs then returns Application.systemLanguage
        /// </summary>
        public static SystemLanguage PlayerLanguage
        {
            get
            {
                return PlayerPrefs.HasKey(STR_LOCALIZATION_KEY) ? (SystemLanguage)PlayerPrefs.GetInt(STR_LOCALIZATION_KEY, (int)Application.systemLanguage) : SystemLanguage.English;
            }
            set
            {
                PlayerPrefs.SetInt(STR_LOCALIZATION_KEY, (int)value);
                PlayerPrefs.Save();
            }
        }

        public static string GetFormattedLocalizedTextBody(string LocalizationId, int index = 0)
        {
            if (!(LocalizableTextInfoDatabase.Instance.LoadedDatabase.Values.SelectMany(_ => _.ToList()).Distinct().FirstOrDefault(_ => _.UniqueID == LocalizationId) != null))
                return "LOCALIZATION_KEY_NOT_FOUND";

            LocalizableTextInfo localizableText = LocalizableTextInfoDatabase.Instance.LoadedDatabase.Values.SelectMany(_ => _.ToList()).Distinct().FirstOrDefault(_ => _.UniqueID == LocalizationId);
            string rawString = GetLanguageText(CurrentLanguage, localizableText);

            if (rawString.Contains(GeraldineStandardsSettings.TextBodyIndexSeperator))
            {
                rawString = rawString.Split(GeraldineStandardsSettings.TextBodyIndexSeperator)[index];
            }

            if (String.IsNullOrEmpty(rawString))
                return "LOCALIZATION_STRING_NOT_FOUND";

            return rawString;
        }

        private static string GetLanguageText (SystemLanguage systemLanguage, LocalizableTextInfo localizableTextInfo)
        {
            switch (systemLanguage)
            {
                case SystemLanguage.Afrikaans:
                    return localizableTextInfo.Afrikaans;
                case SystemLanguage.Arabic:
                    return localizableTextInfo.Arabic;
                case SystemLanguage.Basque:
                    return localizableTextInfo.Basque;
                case SystemLanguage.Belarusian:
                    return localizableTextInfo.Belarusian;
                case SystemLanguage.Bulgarian:
                    return localizableTextInfo.Bulgarian;
                case SystemLanguage.Catalan:
                    return localizableTextInfo.Catalan;
                case SystemLanguage.Chinese:
                    return localizableTextInfo.Chinese;
                case SystemLanguage.Czech:
                    return localizableTextInfo.Czech;
                case SystemLanguage.Danish:
                    return localizableTextInfo.Danish;
                case SystemLanguage.Dutch:
                    return localizableTextInfo.Dutch;
                case SystemLanguage.English:
                    return localizableTextInfo.English;
                case SystemLanguage.Estonian:
                    return localizableTextInfo.Estonian;
                case SystemLanguage.Faroese:
                    return localizableTextInfo.Faroese;
                case SystemLanguage.Finnish:
                    return localizableTextInfo.Finnish;
                case SystemLanguage.French:
                    return localizableTextInfo.French;
                case SystemLanguage.German:
                    return localizableTextInfo.German;
                case SystemLanguage.Greek:
                    return localizableTextInfo.Greek;
                case SystemLanguage.Hebrew:
                    return localizableTextInfo.Hebrew;
                case SystemLanguage.Hungarian:
                    return localizableTextInfo.Hungarian;
                case SystemLanguage.Icelandic:
                    return localizableTextInfo.Icelandic;
                case SystemLanguage.Indonesian:
                    return localizableTextInfo.Indonesian;
                case SystemLanguage.Italian:
                    return localizableTextInfo.Italian;
                case SystemLanguage.Japanese:
                    return localizableTextInfo.Japanese;
                case SystemLanguage.Korean:
                    return localizableTextInfo.Korean;
                case SystemLanguage.Latvian:
                    return localizableTextInfo.Latvian;
                case SystemLanguage.Lithuanian:
                    return localizableTextInfo.Lithuanian;
                case SystemLanguage.Norwegian:
                    return localizableTextInfo.Norwegian;
                case SystemLanguage.Polish:
                    return localizableTextInfo.Polish;
                case SystemLanguage.Portuguese:
                    return localizableTextInfo.Portuguese;
                case SystemLanguage.Romanian:
                    return localizableTextInfo.Romanian;
                case SystemLanguage.Russian:
                    return localizableTextInfo.Russian;
                case SystemLanguage.SerboCroatian:
                    return localizableTextInfo.SerboCroatian;
                case SystemLanguage.Slovak:
                    return localizableTextInfo.Slovak;
                case SystemLanguage.Slovenian:
                    return localizableTextInfo.Slovenian;
                case SystemLanguage.Spanish:
                    return localizableTextInfo.Spanish;
                case SystemLanguage.Swedish:
                    return localizableTextInfo.Swedish;
                case SystemLanguage.Thai:
                    return localizableTextInfo.Thai;
                case SystemLanguage.Turkish:
                    return localizableTextInfo.Turkish;
                case SystemLanguage.Ukrainian:
                    return localizableTextInfo.Ukrainian;
                case SystemLanguage.Vietnamese:
                    return localizableTextInfo.Vietnamese;
                case SystemLanguage.ChineseSimplified:
                    return localizableTextInfo.ChineseSimplified;
                case SystemLanguage.ChineseTraditional:
                    return localizableTextInfo.ChineseTraditional;
                case SystemLanguage.Hindi:
                    return localizableTextInfo.Hindi;

                default:
                    return localizableTextInfo.English;
            }
        }
    }
}
