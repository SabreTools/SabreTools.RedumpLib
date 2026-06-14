using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// Information pertaining to Redump systems
    /// </summary>
    public static class Extensions
    {
        #region Non-Enumerable

        /// <summary>
        /// Extract the size from XML hash data
        /// </summary>
        /// <param name="hashData">String representing the combined hash data</param>
        /// <returns>Extracted size on success, -1 on error</returns>
        public static long ExtractSizeFromHashData(string? hashData)
        {
            if (string.IsNullOrEmpty(hashData))
                return -1;

            var hashreg = new Regex(@"<rom name="".*?"" size=""(.*?)"" crc=""(.*?)"" md5=""(.*?)"" sha1=""(.*?)""", RegexOptions.Compiled);
            Match m = hashreg.Match(hashData);
            if (m.Success)
            {
                if (long.TryParse(m.Groups[1].Value, out long size))
                    return size;
            }

            // Everything else is a failure case
            return -1;
        }

        #endregion

        #region Language

        /// <summary>
        /// Get the Redump longnames for each known language
        /// </summary>
        public static string? LongName(this Language language)
            => ((Language?)language).LongName();

        /// <summary>
        /// Get the Redump longnames for each known language
        /// </summary>
        public static string? LongName(this Language? language)
            => AttributeHelper<Language?>.GetHumanReadableAttribute(language)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known language
        /// </summary>
        public static string? ShortName(this Language language)
            => ((Language?)language).ShortName();

        /// <summary>
        /// Get the Redump shortnames for each known language
        /// </summary>
        public static string? ShortName(this Language? language)
        {
            // Some languages need to use the alternate code instead
#pragma warning disable IDE0072 // Add missing cases
            return language switch
            {
                Language.Albanian
                    or Language.Armenian
                    or Language.Icelandic
                    or Language.Macedonian
                    or Language.Romanian
                    or Language.Slovak => language.ThreeLetterCodeAlt(),
                _ => language.ThreeLetterCode(),
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Get the Language enum value for a given string
        /// </summary>
        /// <param name="lang">String value to convert</param>
        /// <returns>Language represented by the string, if possible</returns>
        public static Language? ToLanguage(this string? lang)
        {
            // No value means no match
            if (lang is null || lang.Length == 0)
                return null;

            lang = lang.ToLowerInvariant();
            var languages = (Language[])Enum.GetValues(typeof(Language));

            // Check ISO 639-1 codes
            int index = Array.FindIndex(languages, l => lang == l.TwoLetterCode());
            if (index > -1)
                return languages[index];

            // Check standard ISO 639-2 codes
            index = Array.FindIndex(languages, l => lang == l.ThreeLetterCode());
            if (index > -1)
                return languages[index];

            // Check alternate ISO 639-2 codes
            index = Array.FindIndex(languages, l => lang == l.ThreeLetterCodeAlt());
            if (index > -1)
                return languages[index];

            return null;
        }

        /// <summary>
        /// Get the ISO 639-2 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCode(this Language language)
            => (AttributeHelper<Language>.GetHumanReadableAttribute(language) as LanguageAttribute)?.ThreeLetterCode;

        /// <summary>
        /// Get the ISO 639-2 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCode(this Language? language)
            => (AttributeHelper<Language?>.GetHumanReadableAttribute(language) as LanguageAttribute)?.ThreeLetterCode;

        /// <summary>
        /// Get the ISO 639-2 alternate code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCodeAlt(this Language language)
            => (AttributeHelper<Language>.GetHumanReadableAttribute(language) as LanguageAttribute)?.ThreeLetterCodeAlt;

        /// <summary>
        /// Get the ISO 639-2 alternate code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCodeAlt(this Language? language)
            => (AttributeHelper<Language?>.GetHumanReadableAttribute(language) as LanguageAttribute)?.ThreeLetterCodeAlt;

        /// <summary>
        /// Get the ISO 639-1 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? TwoLetterCode(this Language language)
            => (AttributeHelper<Language>.GetHumanReadableAttribute(language) as LanguageAttribute)?.TwoLetterCode;

        /// <summary>
        /// Get the ISO 639-1 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? TwoLetterCode(this Language? language)
            => (AttributeHelper<Language?>.GetHumanReadableAttribute(language) as LanguageAttribute)?.TwoLetterCode;

        #endregion

        #region Physical Media Type

        /// <summary>
        /// List all media types with their short usable names
        /// </summary>
        public static List<string> ListMediaTypes()
        {
            var mediaTypes = new List<string>();

            foreach (var val in Enum.GetValues(typeof(PhysicalMediaType)))
            {
                if (val is null || ((PhysicalMediaType)val) == PhysicalMediaType.NONE)
                    continue;

                mediaTypes.Add($"{((PhysicalMediaType?)val).ShortName()} - {((PhysicalMediaType?)val).LongName()}");
            }

            return mediaTypes;
        }

        /// <summary>
        /// Get the longnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? LongName(this PhysicalMediaType? mediaType)
            => AttributeHelper<PhysicalMediaType?>.GetHumanReadableAttribute(mediaType)?.LongName;

        /// <summary>
        /// Get the shortnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? ShortName(this PhysicalMediaType? mediaType)
            => AttributeHelper<PhysicalMediaType?>.GetHumanReadableAttribute(mediaType)?.ShortName;

        #endregion
    }
}
