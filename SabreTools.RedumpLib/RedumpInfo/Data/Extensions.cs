using System;
using System.Collections.Generic;
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

        #region System

        /// <summary>
        /// Determine if a system is okay if it's not detected by Windows
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if Windows show see a disc when dumping, false otherwise</returns>
        public static bool DetectedByWindows(this RedumpSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                // BIOS Sets
                RedumpSystem.MicrosoftXboxBIOS
                    or RedumpSystem.NintendoGameCubeBIOS
                    or RedumpSystem.SonyPlayStationBIOS
                    or RedumpSystem.SonyPlayStation2BIOS => false,

                // Disc-Based Consoles
                RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem
                    or RedumpSystem.BandaiPlaydiaQuickInteractiveSystem
                    or RedumpSystem.BandaiPippin
                    or RedumpSystem.HasbroVideoNow
                    or RedumpSystem.HasbroVideoNowColor
                    or RedumpSystem.HasbroVideoNowJr
                    or RedumpSystem.HasbroVideoNowXP
                    or RedumpSystem.NintendoGameCube
                    or RedumpSystem.NintendoWii
                    or RedumpSystem.NintendoWiiU
                    or RedumpSystem.Panasonic3DOInteractiveMultiplayer
                    or RedumpSystem.PhilipsCDi
                    or RedumpSystem.PioneerLaserActive
                    or RedumpSystem.MarkerDiscBasedConsoleEnd => false,

                // Computers
                RedumpSystem.AppleMacintosh
                    or RedumpSystem.MarkerComputerEnd => false,

                // Arcade
                RedumpSystem.AmericanLaserGames3DO
                    or RedumpSystem.Atari3DO
                    or RedumpSystem.NewJatreCDi
                    or RedumpSystem.PanasonicM2
                    or RedumpSystem.MarkerArcadeEnd => false,

                // Other
                RedumpSystem.PlayStationGameSharkUpdates
                    or RedumpSystem.SuperAudioCD
                    or RedumpSystem.MarkerOtherEnd => false,

                null => false,
                _ => true,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system has reversed ringcodes
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if the system has reversed ringcodes, false otherwise</returns>
        public static bool HasReversedRingcodes(this RedumpSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                RedumpSystem.SonyPlayStation2
                    or RedumpSystem.SonyPlayStation3
                    or RedumpSystem.SonyPlayStation4
                    or RedumpSystem.SonyPlayStation5
                    or RedumpSystem.SonyPlayStationPortable => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system is considered audio-only
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if the system is audio-only, false otherwise</returns>
        /// <remarks>
        /// Philips CD-i should NOT be in this list. It's being included until there's a
        /// reasonable distinction between CD-i and CD-i ready on the database side.
        /// </remarks>
        public static bool IsAudio(this RedumpSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem
                    or RedumpSystem.AudioCD
                    or RedumpSystem.DVDAudio
                    or RedumpSystem.HasbroiONEducationalGamingSystem
                    or RedumpSystem.HasbroVideoNow
                    or RedumpSystem.HasbroVideoNowColor
                    or RedumpSystem.HasbroVideoNowJr
                    or RedumpSystem.HasbroVideoNowXP
                    or RedumpSystem.PhilipsCDi
                    or RedumpSystem.PlayStationGameSharkUpdates
                    or RedumpSystem.SuperAudioCD => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system is a marker value
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if the system is a marker value, false otherwise</returns>
        public static bool IsMarker(this RedumpSystem system)
            => ((RedumpSystem?)system).IsMarker();

        /// <summary>
        /// Determine if a system is a marker value
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if the system is a marker value, false otherwise</returns>
        public static bool IsMarker(this RedumpSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                RedumpSystem.MarkerArcadeEnd
                    or RedumpSystem.MarkerComputerEnd
                    or RedumpSystem.MarkerDiscBasedConsoleEnd
                    or RedumpSystem.MarkerOtherEnd => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system is considered XGD
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if the system is XGD, false otherwise</returns>
        public static bool IsXGD(this RedumpSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                RedumpSystem.MicrosoftXbox
                    or RedumpSystem.MicrosoftXbox360
                    or RedumpSystem.MicrosoftXboxOne
                    or RedumpSystem.MicrosoftXboxSeriesXS => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// List all systems with their short usable names
        /// </summary>
        public static List<string> ListSystems()
        {
            var systems = (RedumpSystem[])Enum.GetValues(typeof(RedumpSystem));
            var knownSystems = Array.FindAll(systems, s => !s.IsMarker() && s.GetCategory() != SystemCategory.NONE);
            Array.Sort(knownSystems, (x, y) => (x.LongName() ?? string.Empty).CompareTo(y.LongName() ?? string.Empty));
            return [.. Array.ConvertAll(knownSystems, val => $"{val.ShortName()} - {val.LongName()}")];
        }

        /// <summary>
        /// Get the Redump longnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? LongName(this RedumpSystem system)
            => AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system)?.LongName;

        /// <summary>
        /// Get the Redump longnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? LongName(this RedumpSystem? system)
            => AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortName(this RedumpSystem system)
            => AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortName(this RedumpSystem? system)
            => AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system)?.ShortName;

        /// <summary>
        /// Determine the category of a system
        /// </summary>
        public static SystemCategory GetCategory(this RedumpSystem system)
            => ((RedumpSystem?)system).GetCategory();

        /// <summary>
        /// Determine the category of a system
        /// </summary>
        public static SystemCategory GetCategory(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.Category ?? SystemCategory.NONE;

        /// <summary>
        /// Determine if a system is available in Redump yet
        /// </summary>
        public static bool IsAvailable(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.Available ?? false;

        /// <summary>
        /// Determine if a system is available in Redump yet
        /// </summary>
        public static bool IsAvailable(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.Available ?? false;

        /// <summary>
        /// Determine if a system is restricted to dumpers
        /// </summary>
        public static bool IsBanned(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.IsBanned ?? false;

        /// <summary>
        /// Determine if a system is restricted to dumpers
        /// </summary>
        public static bool IsBanned(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.IsBanned ?? false;

        /// <summary>
        /// Determine if a system has a CUE pack
        /// </summary>
        public static bool HasCues(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasCues ?? false;

        /// <summary>
        /// Determine if a system has a CUE pack
        /// </summary>
        public static bool HasCues(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasCues ?? false;

        /// <summary>
        /// Determine if a system has a DAT
        /// </summary>
        public static bool HasDat(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDat ?? false;

        /// <summary>
        /// Determine if a system has a DAT
        /// </summary>
        public static bool HasDat(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDat ?? false;

        /// <summary>
        /// Determine if a system has a keys pack
        /// </summary>
        public static bool HasKeys(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasKeys ?? false;

        /// <summary>
        /// Determine if a system has a keys pack
        /// </summary>
        public static bool HasKeys(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasKeys ?? false;

        /// <summary>
        /// Determine if a system has an SBI pack
        /// </summary>
        public static bool HasSbi(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasSbi ?? false;

        /// <summary>
        /// Determine if a system has an SBI pack
        /// </summary>
        public static bool HasSbi(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasSbi ?? false;

        /// <summary>
        /// Get the RedumpSystem enum value for a given string
        /// </summary>
        /// <param name="system">String value to convert</param>
        /// <returns>RedumpSystem represented by the string, if possible</returns>
        public static RedumpSystem? ToRedumpSystem(this string? system)
        {
            // No value means no match
            if (system is null || system.Length == 0)
                return null;

            system = system.ToLowerInvariant();
            var redumpSystems = (RedumpSystem[])Enum.GetValues(typeof(RedumpSystem));

            // Check short names
            int index = Array.FindIndex(redumpSystems, s => system == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return redumpSystems[index];

            // Check long names
            index = Array.FindIndex(redumpSystems, s => system == s.LongName()?.ToLowerInvariant()
                || system == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return redumpSystems[index];

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
