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
