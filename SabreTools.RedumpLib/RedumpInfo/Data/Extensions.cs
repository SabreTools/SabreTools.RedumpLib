using System;
using SabreTools.RedumpLib.Attributes;
using SabreTools.RedumpLib.Data;

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
        public static MediaType? ToMediaType(this PhysicalMediaType? mediaType)
        {
            return mediaType switch
            {
                PhysicalMediaType.BluRay => MediaType.BD50,
                PhysicalMediaType.CDROM => MediaType.CD,
                PhysicalMediaType.DVD => MediaType.DVD9,
                PhysicalMediaType.GDROM => MediaType.GDROM,
                PhysicalMediaType.HDDVD => MediaType.HDDVDSL,
                // PhysicalMediaType.MILCD => MediaType.MILCD, // TODO: Support this?
                PhysicalMediaType.NintendoGameCubeGameDisc => MediaType.NintendoGameCubeGameDisc,
                PhysicalMediaType.NintendoWiiOpticalDisc => MediaType.WiiOpticalDiscDL,
                PhysicalMediaType.NintendoWiiUOpticalDisc => MediaType.WiiUOpticalDiscSL,
                PhysicalMediaType.UMD => MediaType.UMDDL,

                // Invalid cases for conversion
                PhysicalMediaType.NONE => null,
                PhysicalMediaType.ApertureCard => null,
                PhysicalMediaType.JacquardLoomCard => null,
                PhysicalMediaType.MagneticStripeCard => null,
                PhysicalMediaType.OpticalPhonecard => null,
                PhysicalMediaType.PunchedCard => null,
                PhysicalMediaType.PunchedTape => null,
                PhysicalMediaType.Cassette => null,
                PhysicalMediaType.DataCartridge => null,
                PhysicalMediaType.OpenReel => null,
                PhysicalMediaType.FloppyDisk => null,
                PhysicalMediaType.Floptical => null,
                PhysicalMediaType.HardDisk => null,
                PhysicalMediaType.IomegaBernoulliDisk => null,
                PhysicalMediaType.IomegaJaz => null,
                PhysicalMediaType.IomegaZip => null,
                PhysicalMediaType.LaserDisc => null,
                PhysicalMediaType.Nintendo64DD => null,
                PhysicalMediaType.NintendoFamicomDiskSystem => null,
                PhysicalMediaType.Cartridge => null,
                PhysicalMediaType.CED => null,
                PhysicalMediaType.CompactFlash => null,
                PhysicalMediaType.MMC => null,
                PhysicalMediaType.SDCard => null,
                PhysicalMediaType.FlashDrive => null,
                null => null,
                _ => null,
            };
        }

        /// <summary>
        /// Convert currently known Redump disc types to master list of all media types
        /// </summary>
        /// <param name="discType">DiscType value to check</param>
        /// <returns>MediaType if possible, null on error</returns>
        public static PhysicalMediaType? ToPhysicalMediaType(this MediaType? discType)
        {
            return discType switch
            {
                MediaType.BD25
                    or MediaType.BD50
                    or MediaType.BD66
                    or MediaType.BD100
                    or MediaType.MaxTest4Layer => PhysicalMediaType.BluRay,
                MediaType.CD => PhysicalMediaType.CDROM,
                MediaType.DVD5
                    or MediaType.DVD9 => PhysicalMediaType.DVD,
                MediaType.GDROM => PhysicalMediaType.GDROM,
                MediaType.HDDVDSL
                    or MediaType.HDDVDDL => PhysicalMediaType.HDDVD,
                // MediaType.MILCD => MediaType.MILCD, // TODO: Support this?
                MediaType.NintendoGameCubeGameDisc => PhysicalMediaType.NintendoGameCubeGameDisc,
                MediaType.WiiOpticalDiscSL
                    or MediaType.WiiOpticalDiscDL => PhysicalMediaType.NintendoWiiOpticalDisc,
                MediaType.WiiUOpticalDiscSL => PhysicalMediaType.NintendoWiiUOpticalDisc,
                MediaType.UMDSL
                    or MediaType.UMDDL => PhysicalMediaType.UMD,

                // Invalid cases for conversion
                MediaType.NONE => null,
                null => null,
                _ => null,
            };
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
        public static string? LongName(this MediaType mediaType)
            => ((MediaType?)mediaType).LongName();

        /// <summary>
        /// Get the Redump longnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? LongName(this MediaType? mediaType)
            => AttributeHelper<MediaType?>.GetHumanReadableAttribute(mediaType)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? ShortName(this MediaType mediaType)
            => AttributeHelper<MediaType>.GetHumanReadableAttribute(mediaType)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? ShortName(this MediaType? mediaType)
            => AttributeHelper<MediaType?>.GetHumanReadableAttribute(mediaType)?.ShortName;

        /// <summary>
        /// Get the MediaType enum value for a given string
        /// </summary>
        /// <param name="mediaType">String value to convert</param>
        /// <returns>MediaType represented by the string, if possible</returns>
        public static MediaType? ToMediaType(this string? mediaType)
        {
            // No value means no match
            if (mediaType is null || mediaType.Length == 0)
                return null;

            mediaType = mediaType.ToLowerInvariant();
            var mediaTypes = (MediaType[])Enum.GetValues(typeof(MediaType));

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
            if (int.TryParse(mediaType, out int mediaTypeInt) && Enum.IsDefined(typeof(MediaType), mediaTypeInt))
                return (MediaType)mediaTypeInt;

            // Special cases
            return (mediaType?.ToLowerInvariant()) switch
            {
                "bd"
                    or "bdrom"
                    or "bd-rom"
                    or "bluray"
                    or "blu-ray" => MediaType.BD25,
                "cdrom"
                    or "cd-rom" => MediaType.CD,
                "dvd"
                    or "dvd-rom" => MediaType.DVD5,
                "gc" => MediaType.NintendoGameCubeGameDisc,
                "gd" => MediaType.GDROM,
                "hddvd"
                    or "hd-dvd" => MediaType.HDDVDSL,
                "umd" => MediaType.UMDSL,
                "wii" => MediaType.WiiOpticalDiscSL,

                _ => null,
            };
        }

        #endregion

        #region Pack Type

        /// <summary>
        /// Get the human readable name for a PackType
        /// </summary>
        /// <param name="packType"></param>
        /// <returns></returns>
        public static string? LongName(this PackType packType)
            => AttributeHelper<PackType>.GetHumanReadableAttribute(packType)?.LongName;

        /// <summary>
        /// Get the human readable name for a PackType
        /// </summary>
        /// <param name="packType"></param>
        /// <returns></returns>
        public static string? LongName(this PackType? packType)
            => AttributeHelper<PackType?>.GetHumanReadableAttribute(packType)?.LongName;

        /// <summary>
        /// Get the URL path part for a PackType
        /// </summary>
        /// <param name="packType"></param>
        /// <returns></returns>
        public static string? ShortName(this PackType packType)
            => AttributeHelper<PackType>.GetHumanReadableAttribute(packType)?.ShortName;

        /// <summary>
        /// Get the URL path part for a PackType
        /// </summary>
        /// <param name="packType"></param>
        /// <returns></returns>
        public static string? ShortName(this PackType? packType)
            => AttributeHelper<PackType?>.GetHumanReadableAttribute(packType)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="packType">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static PackType? ToPackType(this string? packType)
        {
            // No value means no match
            if (packType is null || packType.Length == 0)
                return null;

            packType = packType.ToLowerInvariant();
            var packTypes = (PackType[])Enum.GetValues(typeof(PackType));

            // Check short names
            int index = Array.FindIndex(packTypes, s => packType == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return packTypes[index];

            // Check long names
            index = Array.FindIndex(packTypes, s => packType == s.LongName()?.ToLowerInvariant()
                || packType == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return packTypes[index];

            return null;
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
