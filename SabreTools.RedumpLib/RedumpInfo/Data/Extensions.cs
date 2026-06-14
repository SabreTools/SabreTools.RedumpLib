using System;
using SabreTools.RedumpLib.Attributes;
using MediaTypeCombined = SabreTools.RedumpLib.RedumpOrg.Data.MediaType;
using MediaTypeInfo = SabreTools.RedumpLib.RedumpInfo.Data.MediaType;

namespace SabreTools.RedumpLib.RedumpInfo.Data
{
    /// <summary>
    /// Information pertaining to Redump systems
    /// </summary>
    public static class Extensions
    {
        #region Cross-Enumeration

        /// <summary>
        /// Convert master list of all media types to currently known Redump disc types
        /// </summary>
        /// <param name="mediaType">MediaType value to check</param>
        /// <returns>DiscType if possible, null on error</returns>
        public static MediaTypeInfo? ToMediaTypeInfo(this MediaTypeCombined? mediaType)
        {
            return mediaType switch
            {
                MediaTypeCombined.BluRay => MediaTypeInfo.BD50,
                MediaTypeCombined.CDROM => MediaTypeInfo.CD,
                MediaTypeCombined.DVD => MediaTypeInfo.DVD9,
                MediaTypeCombined.GDROM => MediaTypeInfo.GDROM,
                MediaTypeCombined.HDDVD => MediaTypeInfo.HDDVDSL,
                // MediaTypeCombined.MILCD => MediaTypeInfo.MILCD, // TODO: Support this?
                MediaTypeCombined.NintendoGameCubeGameDisc => MediaTypeInfo.NintendoGameCubeGameDisc,
                MediaTypeCombined.NintendoWiiOpticalDisc => MediaTypeInfo.WiiOpticalDiscDL,
                MediaTypeCombined.NintendoWiiUOpticalDisc => MediaTypeInfo.WiiUOpticalDiscSL,
                MediaTypeCombined.UMD => MediaTypeInfo.UMDDL,

                // Invalid cases for conversion
                MediaTypeCombined.NONE => null,
                MediaTypeCombined.ApertureCard => null,
                MediaTypeCombined.JacquardLoomCard => null,
                MediaTypeCombined.MagneticStripeCard => null,
                MediaTypeCombined.OpticalPhonecard => null,
                MediaTypeCombined.PunchedCard => null,
                MediaTypeCombined.PunchedTape => null,
                MediaTypeCombined.Cassette => null,
                MediaTypeCombined.DataCartridge => null,
                MediaTypeCombined.OpenReel => null,
                MediaTypeCombined.FloppyDisk => null,
                MediaTypeCombined.Floptical => null,
                MediaTypeCombined.HardDisk => null,
                MediaTypeCombined.IomegaBernoulliDisk => null,
                MediaTypeCombined.IomegaJaz => null,
                MediaTypeCombined.IomegaZip => null,
                MediaTypeCombined.LaserDisc => null,
                MediaTypeCombined.Nintendo64DD => null,
                MediaTypeCombined.NintendoFamicomDiskSystem => null,
                MediaTypeCombined.Cartridge => null,
                MediaTypeCombined.CED => null,
                MediaTypeCombined.CompactFlash => null,
                MediaTypeCombined.MMC => null,
                MediaTypeCombined.SDCard => null,
                MediaTypeCombined.FlashDrive => null,
                null => null,
                _ => null,
            };
        }

        /// <summary>
        /// Convert currently known Redump disc types to master list of all media types
        /// </summary>
        /// <param name="discType">DiscType value to check</param>
        /// <returns>MediaType if possible, null on error</returns>
        public static MediaTypeCombined? ToMediaTypeCombined(this MediaTypeInfo? discType)
        {
            return discType switch
            {
                MediaTypeInfo.BD25
                    or MediaTypeInfo.BD50
                    or MediaTypeInfo.BD66
                    or MediaTypeInfo.BD100
                    or MediaTypeInfo.MaxTest4Layer => MediaTypeCombined.BluRay,
                MediaTypeInfo.CD => MediaTypeCombined.CDROM,
                MediaTypeInfo.DVD5
                    or MediaTypeInfo.DVD9 => MediaTypeCombined.DVD,
                MediaTypeInfo.GDROM => MediaTypeCombined.GDROM,
                MediaTypeInfo.HDDVDSL
                    or MediaTypeInfo.HDDVDDL => MediaTypeCombined.HDDVD,
                // MediaTypeInfo.MILCD => MediaType.MILCD, // TODO: Support this?
                MediaTypeInfo.NintendoGameCubeGameDisc => MediaTypeCombined.NintendoGameCubeGameDisc,
                MediaTypeInfo.WiiOpticalDiscSL
                    or MediaTypeInfo.WiiOpticalDiscDL => MediaTypeCombined.NintendoWiiOpticalDisc,
                MediaTypeInfo.WiiUOpticalDiscSL => MediaTypeCombined.NintendoWiiUOpticalDisc,
                MediaTypeInfo.UMDSL
                    or MediaTypeInfo.UMDDL => MediaTypeCombined.UMD,

                // Invalid cases for conversion
                MediaTypeInfo.NONE => null,
                null => null,
                _ => null,
            };
        }

        #endregion

        #region Disc Category

        /// <summary>
        /// Get the Redump longnames for each known category
        /// </summary>
        public static string? LongName(this DiscCategory category)
            => ((DiscCategory?)category).LongName();

        /// <summary>
        /// Get the Redump longnames for each known category
        /// </summary>
        public static string? LongName(this DiscCategory? category)
            => AttributeHelper<DiscCategory?>.GetHumanReadableAttribute(category)?.LongName;

        /// <summary>
        /// Get the Category enum value for a given string
        /// </summary>
        /// <param name="category">String value to convert</param>
        /// <returns>Category represented by the string, if possible</returns>
        public static DiscCategory? ToDiscCategory(this string? category)
        {
            // No value means no match
            if (category is null || category.Length == 0)
                return null;

            category = category?.ToLowerInvariant();
            var categories = (DiscCategory[])Enum.GetValues(typeof(DiscCategory));

            // Check long names
            int index = Array.FindIndex(categories, c => category == c.LongName()?.ToLowerInvariant()
                || category == c.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return categories[index];

            return null;
        }

        #endregion

        #region Disc Subpath

        /// <summary>
        /// Get the human readable name for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? LongName(this DiscSubpath discSubpath)
            => AttributeHelper<DiscSubpath>.GetHumanReadableAttribute(discSubpath)?.LongName;

        /// <summary>
        /// Get the human readable name for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? LongName(this DiscSubpath? discSubpath)
            => AttributeHelper<DiscSubpath?>.GetHumanReadableAttribute(discSubpath)?.LongName;

        /// <summary>
        /// Get the URL path part for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? ShortName(this DiscSubpath discSubpath)
            => AttributeHelper<DiscSubpath>.GetHumanReadableAttribute(discSubpath)?.ShortName;

        /// <summary>
        /// Get the URL path part for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? ShortName(this DiscSubpath? discSubpath)
            => AttributeHelper<DiscSubpath?>.GetHumanReadableAttribute(discSubpath)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="discSubpath">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static DiscSubpath? ToDiscSubpath(this string? discSubpath)
        {
            // No value means no match
            if (discSubpath is null || discSubpath.Length == 0)
                return null;

            discSubpath = discSubpath.ToLowerInvariant();
            var discSubpaths = (DiscSubpath[])Enum.GetValues(typeof(DiscSubpath));

            // Check short names
            int index = Array.FindIndex(discSubpaths, s => discSubpath == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return discSubpaths[index];

            // Check long names
            index = Array.FindIndex(discSubpaths, s => discSubpath == s.LongName()?.ToLowerInvariant()
                || discSubpath == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return discSubpaths[index];

            return null;
        }

        #endregion

        #region Dump Status

        /// <summary>
        /// Get the human readable name for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? LongName(this DumpStatus dumpStatus)
            => AttributeHelper<DumpStatus>.GetHumanReadableAttribute(dumpStatus)?.LongName;

        /// <summary>
        /// Get the human readable name for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? LongName(this DumpStatus? dumpStatus)
            => AttributeHelper<DumpStatus?>.GetHumanReadableAttribute(dumpStatus)?.LongName;

        /// <summary>
        /// Get the URL path part for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? ShortName(this DumpStatus dumpStatus)
            => AttributeHelper<DumpStatus>.GetHumanReadableAttribute(dumpStatus)?.ShortName;

        /// <summary>
        /// Get the URL path part for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? ShortName(this DumpStatus? dumpStatus)
            => AttributeHelper<DumpStatus?>.GetHumanReadableAttribute(dumpStatus)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="dumpStatus">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static DumpStatus? ToDumpStatus(this string? dumpStatus)
        {
            // No value means no match
            if (dumpStatus is null || dumpStatus.Length == 0)
                return null;

            dumpStatus = dumpStatus.ToLowerInvariant();
            var dumpStatuses = (DumpStatus[])Enum.GetValues(typeof(DumpStatus));

            // Check short names
            int index = Array.FindIndex(dumpStatuses, s => dumpStatus == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return dumpStatuses[index];

            // Check long names
            index = Array.FindIndex(dumpStatuses, s => dumpStatus == s.LongName()?.ToLowerInvariant()
                || dumpStatus == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return dumpStatuses[index];

            // Check numeric values
            if (int.TryParse(dumpStatus, out int dumpStatusInt) && Enum.IsDefined(typeof(DumpStatus), dumpStatusInt))
                return (DumpStatus)dumpStatusInt;

            return null;
        }

        #endregion

        #region Media Type

        /// <summary>
        /// Get the Redump longnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? LongName(this MediaTypeInfo mediaType)
            => ((MediaTypeInfo?)mediaType).LongName();

        /// <summary>
        /// Get the Redump longnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? LongName(this MediaTypeInfo? mediaType)
            => AttributeHelper<MediaTypeInfo?>.GetHumanReadableAttribute(mediaType)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? ShortName(this MediaTypeInfo mediaType)
            => AttributeHelper<MediaTypeInfo>.GetHumanReadableAttribute(mediaType)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? ShortName(this MediaTypeInfo? mediaType)
            => AttributeHelper<MediaTypeInfo?>.GetHumanReadableAttribute(mediaType)?.ShortName;

        /// <summary>
        /// Get the MediaType enum value for a given string
        /// </summary>
        /// <param name="mediaType">String value to convert</param>
        /// <returns>MediaType represented by the string, if possible</returns>
        public static MediaTypeInfo? ToMediaType(this string? mediaType)
        {
            // No value means no match
            if (mediaType is null || mediaType.Length == 0)
                return null;

            mediaType = mediaType.ToLowerInvariant();
            var mediaTypes = (MediaTypeInfo[])Enum.GetValues(typeof(MediaTypeInfo));

            // Check short names
            int index = Array.FindIndex(mediaTypes, s => mediaType == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return mediaTypes[index];

            // Check long names
            index = Array.FindIndex(mediaTypes, s => mediaType == s.LongName()?.ToLowerInvariant()
                || mediaType == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant()
                || mediaType == s.LongName()?.Replace("-", string.Empty)?.ToLowerInvariant()
                || mediaType == s.LongName()?
                    .Replace(" ", string.Empty)?
                    .Replace("-", string.Empty)?
                    .ToLowerInvariant());
            if (index > -1)
                return mediaTypes[index];

            // Check numeric values
            if (int.TryParse(mediaType, out int mediaTypeInt) && Enum.IsDefined(typeof(MediaTypeInfo), mediaTypeInt))
                return (MediaTypeInfo)mediaTypeInt;

            // Special cases
            return (mediaType?.ToLowerInvariant()) switch
            {
                "bd"
                    or "bdrom"
                    or "bd-rom"
                    or "bluray"
                    or "blu-ray" => MediaTypeInfo.BD25,
                "cdrom"
                    or "cd-rom" => MediaTypeInfo.CD,
                "dvd"
                    or "dvd-rom" => MediaTypeInfo.DVD5,
                "gc" => MediaTypeInfo.NintendoGameCubeGameDisc,
                "gd" => MediaTypeInfo.GDROM,
                "hddvd"
                    or "hd-dvd" => MediaTypeInfo.HDDVDSL,
                "umd" => MediaTypeInfo.UMDSL,
                "wii" => MediaTypeInfo.WiiOpticalDiscSL,

                _ => null,
            };
        }

        #endregion

        #region System Category

        /// <summary>
        /// Get the string representation of the system category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string? LongName(this SystemCategory? category)
            => AttributeHelper<SystemCategory?>.GetHumanReadableAttribute(category)?.LongName;

        #endregion
    }
}
