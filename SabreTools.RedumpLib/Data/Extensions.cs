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

        /// <summary>
        /// Adjust the disc type based on size and layerbreak information
        /// </summary>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <returns>Corrected disc type, if possible</returns>
        public static void NormalizeDiscType(this SubmissionInfo info)
        {
            // If we have nothing valid, do nothing
            if (info.DiscIdentity.Media is null)
                return;

            // Some systems have limitations on media type when there's ambiguity
            var system = info.DiscIdentity.System;

#pragma warning disable IDE0010
            switch (info.DiscIdentity.Media)
            {
                case MediaType.DVD5:
                case MediaType.DVD9:
                    // 2-layer
                    if (info.DiscIdentifiers.Layerbreak != default)
                        info.DiscIdentity.Media = MediaType.DVD9;

                    // 1-layer
                    else
                        info.DiscIdentity.Media = MediaType.DVD5;

                    break;

                case MediaType.BD25:
                case MediaType.BD33:
                case MediaType.BD50:
                case MediaType.BD66:
                case MediaType.BD100:
                case MediaType.BD128:
                    // Extract the size from the hashes
                    long size = ExtractSizeFromHashData(info.DumpMetadata.Dat);

                    // 4-layer
                    if (info.DiscIdentifiers.Layerbreak3 != default)
                        info.DiscIdentity.Media = MediaType.BD128;

                    // 3-layer
                    else if (info.DiscIdentifiers.Layerbreak2 != default)
                        info.DiscIdentity.Media = MediaType.BD100;

                    // 2-layer
                    else if (info.DiscIdentifiers.Layerbreak != default && info.DumpMetadata.PICIdentifier == "BDU")
                        info.DiscIdentity.Media = MediaType.BD66;
                    else if (info.DiscIdentifiers.Layerbreak != default && size > 50_050_629_632)
                        info.DiscIdentity.Media = MediaType.BD66;
                    else if (info.DiscIdentifiers.Layerbreak != default && system == PhysicalSystem.SonyPlayStation5)
                        info.DiscIdentity.Media = MediaType.BD66;
                    else if (info.DiscIdentifiers.Layerbreak != default)
                        info.DiscIdentity.Media = MediaType.BD50;

                    // 1-layer
                    else if (info.DumpMetadata.PICIdentifier == "BDU")
                        info.DiscIdentity.Media = MediaType.BD33;
                    else if (size > 25_025_314_816)
                        info.DiscIdentity.Media = MediaType.BD33;
                    else if (system == PhysicalSystem.SonyPlayStation5)
                        info.DiscIdentity.Media = MediaType.BD33;
                    else
                        info.DiscIdentity.Media = MediaType.BD25;
                    break;

                case MediaType.HDDVDSL:
                case MediaType.HDDVDDL:
                    // 2-layer
                    if (info.DiscIdentifiers.Layerbreak != default)
                        info.DiscIdentity.Media = MediaType.HDDVDDL;

                    // 1-layer
                    else
                        info.DiscIdentity.Media = MediaType.HDDVDSL;
                    break;

                case MediaType.UMDSL:
                case MediaType.UMDDL:
                    // 2-layer
                    if (info.DiscIdentifiers.Layerbreak != default)
                        info.DiscIdentity.Media = MediaType.UMDDL;

                    // 1-layer
                    else
                        info.DiscIdentity.Media = MediaType.UMDSL;

                    break;

                // All other disc types are not processed
                default:
                    break;
            }
#pragma warning restore IDE0010
        }

        #endregion

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
                PhysicalMediaType.NintendoWiiOpticalDisc => MediaType.NintendoWiiOpticalDiscDL,
                PhysicalMediaType.NintendoWiiUOpticalDisc => MediaType.NintendoWiiUOpticalDiscSL,
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
                    or MediaType.BD33
                    or MediaType.BD50
                    or MediaType.BD66
                    or MediaType.BD100
                    or MediaType.BD128 => PhysicalMediaType.BluRay,
                MediaType.CD => PhysicalMediaType.CDROM,
                MediaType.DVD5
                    or MediaType.DVD9 => PhysicalMediaType.DVD,
                MediaType.GDROM => PhysicalMediaType.GDROM,
                MediaType.HDDVDSL
                    or MediaType.HDDVDDL => PhysicalMediaType.HDDVD,
                // MediaType.MILCD => PhysicalMediaType.MILCD, // TODO: Support this?
                MediaType.NintendoGameCubeGameDisc => PhysicalMediaType.NintendoGameCubeGameDisc,
                MediaType.NintendoWiiOpticalDiscSL
                    or MediaType.NintendoWiiOpticalDiscDL => PhysicalMediaType.NintendoWiiOpticalDisc,
                MediaType.NintendoWiiUOpticalDiscSL => PhysicalMediaType.NintendoWiiUOpticalDisc,
                MediaType.UMDSL
                    or MediaType.UMDDL => PhysicalMediaType.UMD,

                // Invalid cases for conversion
                MediaType.NONE => null,
                MediaType.MILCD => null,
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

        #region Language Code

        /// <summary>
        /// Get the LanguageCode value for a given string
        /// </summary>
        /// <param name="lang">String value to convert</param>
        /// <returns>Language represented by the string, if possible</returns>
        public static LanguageCode? ToLanguageCode(this string? lang)
        {
            // No value means no match
            if (lang is null || lang.Length == 0)
                return null;

            lang = lang.ToLowerInvariant();
            var languages = LanguageCode.AllLanguages;

            // Check ISO 639-1 codes
            int index = Array.FindIndex(languages, l => lang == l.TwoLetterCode);
            if (index > -1)
                return languages[index];

            // Check standard ISO 639-2 codes
            index = Array.FindIndex(languages, l => lang == l.ThreeLetterCode);
            if (index > -1)
                return languages[index];

            // Check alternate ISO 639-2 codes
            index = Array.FindIndex(languages, l => lang == l.ThreeLetterCodeAlt);
            if (index > -1)
                return languages[index];

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
                "wii" => MediaType.NintendoWiiOpticalDiscSL,

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
        /// Get the PackType enum value for a given string
        /// </summary>
        /// <param name="packType">String value to convert</param>
        /// <returns>PackType represented by the string, if possible</returns>
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
        public static string? LongName(this PhysicalMediaType mediaType)
            => ((PhysicalMediaType?)mediaType).LongName();

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
        public static string? ShortName(this PhysicalMediaType mediaType)
            => ((PhysicalMediaType?)mediaType).ShortName();

        /// <summary>
        /// Get the shortnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? ShortName(this PhysicalMediaType? mediaType)
            => AttributeHelper<PhysicalMediaType?>.GetHumanReadableAttribute(mediaType)?.ShortName;

        /// <summary>
        /// Get the PhysicalMediaType enum value for a given string
        /// </summary>
        /// <param name="mediaType">String value to convert</param>
        /// <returns>PhysicalMediaType represented by the string, if possible</returns>
        public static PhysicalMediaType ToPhysicalMediaType(this string? mediaType)
        {
            // No value means no match
            if (mediaType is null || mediaType.Length == 0)
                return PhysicalMediaType.NONE;

            mediaType = mediaType.ToLowerInvariant();
            var mediaTypes = (PhysicalMediaType[])Enum.GetValues(typeof(PhysicalMediaType));

            // Check short names
            int index = Array.FindIndex(mediaTypes, s => mediaType == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return mediaTypes[index];

            // Check long names
            index = Array.FindIndex(mediaTypes, s => mediaType == s.LongName()?.ToLowerInvariant()
                || mediaType == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return mediaTypes[index];

            return PhysicalMediaType.NONE;
        }

        #endregion

        #region Physical System

        /// <summary>
        /// Determine if a system is okay if it's not detected by Windows
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if Windows show see a disc when dumping, false otherwise</returns>
        public static bool DetectedByWindows(this PhysicalSystem? system)
        {
            // BIOS Sets
            if (system == PhysicalSystem.MicrosoftXboxBIOS)
                return false;
            else if (system == PhysicalSystem.NintendoGameCubeBIOS)
                return false;
            else if (system == PhysicalSystem.SonyPlayStationBIOS)
                return false;
            else if (system == PhysicalSystem.SonyPlayStation2BIOS)
                return false;

            // Disc-Based Consoles
            if (system == PhysicalSystem.AppleBandaiPippin)
                return false;
            else if (system == PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem)
                return false;
            else if (system == PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem)
                return false;
            else if (system == PhysicalSystem.HasbroVideoNow)
                return false;
            else if (system == PhysicalSystem.HasbroVideoNowColor)
                return false;
            else if (system == PhysicalSystem.HasbroVideoNowJr)
                return false;
            else if (system == PhysicalSystem.HasbroVideoNowXP)
                return false;
            else if (system == PhysicalSystem.NintendoGameCube)
                return false;
            else if (system == PhysicalSystem.NintendoWii)
                return false;
            else if (system == PhysicalSystem.NintendoWiiU)
                return false;
            else if (system == PhysicalSystem.Panasonic3DOInteractiveMultiplayer)
                return false;
            else if (system == PhysicalSystem.PhilipsCDi)
                return false;
            else if (system == PhysicalSystem.PioneerLaserActive)
                return false;
            else if (system == PhysicalSystem.MarkerDiscBasedConsoleEnd)
                return false;

            // Computers
            if (system == PhysicalSystem.AppleMacintosh)
                return false;
            else if (system == PhysicalSystem.MarkerComputerEnd)
                return false;

            // Arcade
            if (system == PhysicalSystem.AmericanLaserGames3DO)
                return false;
            else if (system == PhysicalSystem.Atari3DO)
                return false;
            else if (system == PhysicalSystem.NewJatreCDi)
                return false;
            else if (system == PhysicalSystem.PanasonicM2)
                return false;
            else if (system == PhysicalSystem.MarkerArcadeEnd)
                return false;

            // Other
            if (system == PhysicalSystem.DatelPlayStationCheatDeviceUpdates)
                return false;
            else if (system == PhysicalSystem.SuperAudioCD)
                return false;
            else if (system == PhysicalSystem.MarkerOtherEnd)
                return false;
            else if (system == null)
                return false;

            return true;
        }

        /// <summary>
        /// Determine if a system is considered XGD
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system is XGD, false otherwise</returns>
        public static bool IsXGD(this PhysicalSystem? system)
        {
            if (system == PhysicalSystem.MicrosoftXbox)
                return true;
            else if (system == PhysicalSystem.MicrosoftXbox360)
                return true;
            else if (system == PhysicalSystem.MicrosoftXboxOne)
                return true;
            else if (system == PhysicalSystem.MicrosoftXboxSeriesXS)
                return true;

            return false;
        }

        /// <summary>
        /// List all systems with their short usable names
        /// </summary>
        public static List<string> ListSystems()
        {
            var knownSystems = Array.FindAll(PhysicalSystem.AllSystems, s => !s.IsMarker && s.Category != SystemCategory.NONE);
            Array.Sort(knownSystems, (x, y) => x.Name.CompareTo(y.Name));
            return [.. Array.ConvertAll(knownSystems, val => $"{val.Code} - {val.Name}")];
        }

        /// <summary>
        /// Get the PhysicalSystem enum value for a given string
        /// </summary>
        /// <param name="system">String value to convert</param>
        /// <returns>PhysicalSystem represented by the string, if possible</returns>
        public static PhysicalSystem? ToPhysicalSystem(this string? system)
        {
            // No value means no match
            if (system is null || system.Length == 0)
                return null;

            system = system.ToLowerInvariant();

            // Check codes
            int index = Array.FindIndex(PhysicalSystem.AllSystems, s => system == s.Code?.ToLowerInvariant());
            if (index > -1)
                return PhysicalSystem.AllSystems[index];

            // Check names
            index = Array.FindIndex(PhysicalSystem.AllSystems, s => system == s.Name.ToLowerInvariant()
                || system == s.Name.Replace(" ", string.Empty).ToLowerInvariant());
            if (index > -1)
                return PhysicalSystem.AllSystems[index];

            return null;
        }

        #endregion

        #region Region Code

        /// <summary>
        /// Get the RegionCode value for a given string
        /// </summary>
        /// <param name="region">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static RegionCode? ToRegionCode(this string? region)
        {
            // No value means no match
            if (region is null || region.Length == 0)
                return null;

            region = region.ToLowerInvariant();

            // Check short names
            int index = Array.FindIndex(RegionCode.AllRegions, s => region == s.Code.ToLowerInvariant());
            if (index > -1)
                return RegionCode.AllRegions[index];

            // Check long names
            index = Array.FindIndex(RegionCode.AllRegions, s => region == s.Name.ToLowerInvariant()
                || region == s.Name.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return RegionCode.AllRegions[index];

            return null;
        }

        #endregion

        #region Site Code

        /// <summary>
        /// List all site codes with their short usable names
        /// </summary>
        public static List<string> ListSiteCodes()
        {
            var siteCodes = new List<string>();

            foreach (var val in SiteCode.AllSiteCodes)
            {
                string? shortName = val.Code?.TrimEnd(':');
                string longName = val.HTML.TrimEnd(':');

                // Invalid codes should be skipped
                if (shortName is null || longName is null)
                    continue;

                // Handle site tags
                string siteCode;
                if (shortName == longName)
                    siteCode = "***".PadRight(16, ' ');
                else
                    siteCode = shortName.PadRight(16, ' ');

                // Handle expanded tags
                siteCode += longName.PadRight(32, ' ');

                // Include special indicators, if necessary
                var additionalInfo = new List<string>();
                if (val.IsCommentCode)
                    additionalInfo.Add("Comment Field");
                if (val.IsContentCode)
                    additionalInfo.Add("Content Field");
                if (val.IsMultiLine)
                    additionalInfo.Add("Multiline");
                if (additionalInfo.Count > 0)
                    siteCode += $"[{string.Join(", ", [.. additionalInfo])}]";

                // Add the formatted site code
                siteCodes.Add(siteCode);
            }

            return siteCodes;
        }

        #endregion

        #region Sort Category

        /// <summary>
        /// Get the human readable name for a SortCategory
        /// </summary>
        /// <param name="sortCategory"></param>
        /// <returns></returns>
        public static string? LongName(this SortCategory sortCategory)
            => AttributeHelper<SortCategory>.GetHumanReadableAttribute(sortCategory)?.LongName;

        /// <summary>
        /// Get the human readable name for a SortCategory
        /// </summary>
        /// <param name="sortCategory"></param>
        /// <returns></returns>
        public static string? LongName(this SortCategory? sortCategory)
            => AttributeHelper<SortCategory?>.GetHumanReadableAttribute(sortCategory)?.LongName;

        /// <summary>
        /// Get the URL path part for a SortCategory
        /// </summary>
        /// <param name="sortCategory"></param>
        /// <returns></returns>
        public static string? ShortName(this SortCategory sortCategory)
            => AttributeHelper<SortCategory>.GetHumanReadableAttribute(sortCategory)?.ShortName;

        /// <summary>
        /// Get the URL path part for a SortCategory
        /// </summary>
        /// <param name="sortCategory"></param>
        /// <returns></returns>
        public static string? ShortName(this SortCategory? sortCategory)
            => AttributeHelper<SortCategory?>.GetHumanReadableAttribute(sortCategory)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="sortCategory">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static SortCategory? ToSortCategory(this string? sortCategory)
        {
            // No value means no match
            if (sortCategory is null || sortCategory.Length == 0)
                return null;

            sortCategory = sortCategory.ToLowerInvariant();
            var sortCategories = (SortCategory[])Enum.GetValues(typeof(SortCategory));

            // Check short names
            int index = Array.FindIndex(sortCategories, s => sortCategory == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return sortCategories[index];

            // Check long names
            index = Array.FindIndex(sortCategories, s => sortCategory == s.LongName()?.ToLowerInvariant()
                || sortCategory == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return sortCategories[index];

            return null;
        }

        #endregion

        #region Sort Direction

        /// <summary>
        /// Get the human readable name for a SortDirection
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? LongName(this SortDirection sortDirection)
            => AttributeHelper<SortDirection>.GetHumanReadableAttribute(sortDirection)?.LongName;

        /// <summary>
        /// Get the human readable name for a SortDirection
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? LongName(this SortDirection? sortDirection)
            => AttributeHelper<SortDirection?>.GetHumanReadableAttribute(sortDirection)?.LongName;

        /// <summary>
        /// Get the URL path part for a SortDirection
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? ShortName(this SortDirection sortDirection)
            => AttributeHelper<SortDirection>.GetHumanReadableAttribute(sortDirection)?.ShortName;

        /// <summary>
        /// Get the URL path part for a SortDirection
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? ShortName(this SortDirection? sortDirection)
            => AttributeHelper<SortDirection?>.GetHumanReadableAttribute(sortDirection)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="sortDirection">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static SortDirection? ToSortDirection(this string? sortDirection)
        {
            // No value means no match
            if (sortDirection is null || sortDirection.Length == 0)
                return null;

            sortDirection = sortDirection.ToLowerInvariant();
            var sortCategories = (SortDirection[])Enum.GetValues(typeof(SortDirection));

            // Check short names
            int index = Array.FindIndex(sortCategories, s => sortDirection == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return sortCategories[index];

            // Check long names
            index = Array.FindIndex(sortCategories, s => sortDirection == s.LongName()?.ToLowerInvariant()
                || sortDirection == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return sortCategories[index];

            return null;
        }

        #endregion

        #region Submission Type

        /// <summary>
        /// Get the human readable name for a SubmissionType
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? LongName(this SubmissionType sortDirection)
            => AttributeHelper<SubmissionType>.GetHumanReadableAttribute(sortDirection)?.LongName;

        /// <summary>
        /// Get the human readable name for a SubmissionType
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? LongName(this SubmissionType? sortDirection)
            => AttributeHelper<SubmissionType?>.GetHumanReadableAttribute(sortDirection)?.LongName;

        /// <summary>
        /// Get the URL path part for a SubmissionType
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? ShortName(this SubmissionType sortDirection)
            => AttributeHelper<SubmissionType>.GetHumanReadableAttribute(sortDirection)?.ShortName;

        /// <summary>
        /// Get the URL path part for a SubmissionType
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? ShortName(this SubmissionType? sortDirection)
            => AttributeHelper<SubmissionType?>.GetHumanReadableAttribute(sortDirection)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="sortDirection">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static SubmissionType? ToSubmissionType(this string? sortDirection)
        {
            // No value means no match
            if (sortDirection is null || sortDirection.Length == 0)
                return null;

            sortDirection = sortDirection.ToLowerInvariant();
            var sortCategories = (SubmissionType[])Enum.GetValues(typeof(SubmissionType));

            // Check short names
            int index = Array.FindIndex(sortCategories, s => sortDirection == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return sortCategories[index];

            // Check long names
            index = Array.FindIndex(sortCategories, s => sortDirection == s.LongName()?.ToLowerInvariant()
                || sortDirection == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return sortCategories[index];

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

        #region Yes/No

        /// <summary>
        /// Get the string representation of the YesNo value
        /// </summary>
        /// <param name="yesno"></param>
        /// <returns></returns>
        public static string LongName(this YesNo? yesno)
            => AttributeHelper<YesNo?>.GetHumanReadableAttribute(yesno)?.LongName ?? "Yes/No";

        /// <summary>
        /// Get the YesNo enum value for a given nullable boolean
        /// </summary>
        /// <param name="yesno">Nullable boolean value to convert</param>
        /// <returns>YesNo represented by the nullable boolean, if possible</returns>
        public static YesNo ToYesNo(this bool yesno)
        {
            return yesno switch
            {
                false => YesNo.No,
                true => YesNo.Yes,
            };
        }

        /// <summary>
        /// Get the YesNo enum value for a given nullable boolean
        /// </summary>
        /// <param name="yesno">Nullable boolean value to convert</param>
        /// <returns>YesNo represented by the nullable boolean, if possible</returns>
        public static YesNo? ToYesNo(this bool? yesno)
        {
            return yesno switch
            {
                false => YesNo.No,
                true => YesNo.Yes,
                _ => YesNo.NULL,
            };
        }

        /// <summary>
        /// Get the YesNo enum value for a given string
        /// </summary>
        /// <param name="yesno">String value to convert</param>
        /// <returns>YesNo represented by the string, if possible</returns>
        public static YesNo? ToYesNo(this string? yesno)
        {
            return (yesno?.ToLowerInvariant()) switch
            {
                "no" or "false" => YesNo.No,
                "yes" or "true" => YesNo.Yes,
                _ => YesNo.NULL,
            };
        }

        #endregion
    }
}
