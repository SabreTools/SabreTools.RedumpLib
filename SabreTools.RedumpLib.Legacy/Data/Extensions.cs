using System;
using System.Collections.Generic;
using SabreTools.RedumpLib.Attributes;
using MediaType = SabreTools.RedumpLib.Data.MediaType;
using SystemAttribute = SabreTools.RedumpLib.Legacy.Attributes.SystemAttribute;
using SystemCategory = SabreTools.RedumpLib.Data.SystemCategory;

namespace SabreTools.RedumpLib.Legacy.Data
{
    /// <summary>
    /// Information pertaining to Redump systems
    /// </summary>
    public static class Extensions
    {
        #region Non-Enumerable

        /// <summary>
        /// Adjust the disc type based on size and layerbreak information
        /// </summary>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <returns>Corrected disc type, if possible</returns>
        public static void NormalizeDiscType(this SubmissionInfo info)
        {
            // If we have nothing valid, do nothing
            if (info.CommonDiscInfo.Media is null || info.SizeAndChecksums == default)
                return;

#pragma warning disable IDE0010
            switch (info.CommonDiscInfo.Media)
            {
                case MediaType.DVD5:
                case MediaType.DVD9:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.DVD9;
                    else
                        info.CommonDiscInfo.Media = MediaType.DVD5;
                    break;

                case MediaType.BD25:
                case MediaType.BD33:
                case MediaType.BD50:
                case MediaType.BD66:
                case MediaType.BD100:
                case MediaType.BD128:
                    // Extract the size from the hashes
                    long size = RedumpLib.Data.Extensions.ExtractSizeFromHashData(info.TracksAndWriteOffsets.ClrMameProData);

                    if (info.SizeAndChecksums.Layerbreak3 != default)
                        info.CommonDiscInfo.Media = MediaType.BD128;
                    else if (info.SizeAndChecksums.Layerbreak2 != default)
                        info.CommonDiscInfo.Media = MediaType.BD100;
                    else if (info.SizeAndChecksums.Layerbreak != default && info.SizeAndChecksums.PICIdentifier == "BDU")
                        info.CommonDiscInfo.Media = MediaType.BD66;
                    else if (info.SizeAndChecksums.Layerbreak != default && size > 50_050_629_632)
                        info.CommonDiscInfo.Media = MediaType.BD66;
                    else if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.BD50;
                    else if (info.SizeAndChecksums.PICIdentifier == "BDU")
                        info.CommonDiscInfo.Media = MediaType.BD33;
                    else if (size > 25_025_314_816)
                        info.CommonDiscInfo.Media = MediaType.BD33;
                    else
                        info.CommonDiscInfo.Media = MediaType.BD25;
                    break;

                case MediaType.HDDVDSL:
                case MediaType.HDDVDDL:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.HDDVDDL;
                    else
                        info.CommonDiscInfo.Media = MediaType.HDDVDSL;
                    break;

                case MediaType.UMDSL:
                case MediaType.UMDDL:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.UMDDL;
                    else
                        info.CommonDiscInfo.Media = MediaType.UMDSL;
                    break;

                // All other disc types are not processed
                default:
                    break;
            }
#pragma warning restore IDE0010
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
            => (AttributeHelper<Language>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.ThreeLetterCode;

        /// <summary>
        /// Get the ISO 639-2 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCode(this Language? language)
            => (AttributeHelper<Language?>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.ThreeLetterCode;

        /// <summary>
        /// Get the ISO 639-2 alternate code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCodeAlt(this Language language)
            => (AttributeHelper<Language>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.ThreeLetterCodeAlt;

        /// <summary>
        /// Get the ISO 639-2 alternate code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCodeAlt(this Language? language)
            => (AttributeHelper<Language?>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.ThreeLetterCodeAlt;

        /// <summary>
        /// Get the ISO 639-1 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? TwoLetterCode(this Language language)
            => (AttributeHelper<Language>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.TwoLetterCode;

        /// <summary>
        /// Get the ISO 639-1 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? TwoLetterCode(this Language? language)
            => (AttributeHelper<Language?>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.TwoLetterCode;

        #endregion

        #region Language Selection

        /// <summary>
        /// Get the string representation of the LanguageSelection enum values
        /// </summary>
        /// <param name="langSelect">LanguageSelection value to convert</param>
        /// <returns>String representing the value, if possible</returns>
        public static string? LongName(this LanguageSelection langSelect)
            => ((LanguageSelection?)langSelect).LongName();

        /// <summary>
        /// Get the string representation of the LanguageSelection enum values
        /// </summary>
        /// <param name="langSelect">LanguageSelection value to convert</param>
        /// <returns>String representing the value, if possible</returns>
        public static string? LongName(this LanguageSelection? langSelect)
            => AttributeHelper<LanguageSelection?>.GetHumanReadableAttribute(langSelect)?.LongName;

        /// <summary>
        /// Get the LanguageSelection enum value for a given string
        /// </summary>
        /// <param name="langSelect">String value to convert</param>
        /// <returns>LanguageSelection represented by the string, if possible</returns>
        public static LanguageSelection? ToLanguageSelection(this string? langSelect)
        {
            // No value means no match
            if (langSelect is null || langSelect.Length == 0)
                return null;

            langSelect = langSelect?.ToLowerInvariant();
            var selects = (LanguageSelection[])Enum.GetValues(typeof(LanguageSelection));

            // Check long names
            int index = Array.FindIndex(selects, l => langSelect == l.LongName()?.ToLowerInvariant()
                || langSelect == l.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return selects[index];

            return null;
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
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                // BIOS Sets
                PhysicalSystem.MicrosoftXboxBIOS
                    or PhysicalSystem.NintendoGameCubeBIOS
                    or PhysicalSystem.SonyPlayStationBIOS
                    or PhysicalSystem.SonyPlayStation2BIOS => false,

                // Disc-Based Consoles
                PhysicalSystem.AppleBandaiPippin
                    or PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem
                    or PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem
                    or PhysicalSystem.HasbroVideoNow
                    or PhysicalSystem.HasbroVideoNowColor
                    or PhysicalSystem.HasbroVideoNowJr
                    or PhysicalSystem.HasbroVideoNowXP
                    or PhysicalSystem.NintendoGameCube
                    or PhysicalSystem.NintendoWii
                    or PhysicalSystem.NintendoWiiU
                    or PhysicalSystem.Panasonic3DOInteractiveMultiplayer
                    or PhysicalSystem.PhilipsCDi
                    or PhysicalSystem.MarkerDiscBasedConsoleEnd => false,

                // Computers
                PhysicalSystem.AppleMacintosh
                    or PhysicalSystem.MarkerComputerEnd => false,

                // Arcade
                PhysicalSystem.PanasonicM2
                    or PhysicalSystem.MarkerArcadeEnd => false,

                // Other
                PhysicalSystem.PlayStationGameSharkUpdates
                    or PhysicalSystem.MarkerOtherEnd => false,

                null => false,
                _ => true,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system has reversed ringcodes
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system has reversed ringcodes, false otherwise</returns>
        public static bool HasReversedRingcodes(this PhysicalSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                PhysicalSystem.SonyPlayStation2
                    or PhysicalSystem.SonyPlayStation3
                    or PhysicalSystem.SonyPlayStation4
                    or PhysicalSystem.SonyPlayStation5
                    or PhysicalSystem.SonyPlayStationPortable => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system is considered audio-only
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system is audio-only, false otherwise</returns>
        /// <remarks>
        /// Philips CD-i should NOT be in this list. It's being included until there's a
        /// reasonable distinction between CD-i and CD-i ready on the database side.
        /// </remarks>
        public static bool IsAudio(this PhysicalSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem
                    or PhysicalSystem.AudioCD
                    or PhysicalSystem.PlayStationGameSharkUpdates
                    or PhysicalSystem.HasbroVideoNow
                    or PhysicalSystem.HasbroVideoNowColor
                    or PhysicalSystem.HasbroVideoNowJr
                    or PhysicalSystem.HasbroVideoNowXP
                    or PhysicalSystem.PhilipsCDi => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system is a marker value
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system is a marker value, false otherwise</returns>
        public static bool IsMarker(this PhysicalSystem system)
            => ((PhysicalSystem?)system).IsMarker();

        /// <summary>
        /// Determine if a system is a marker value
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system is a marker value, false otherwise</returns>
        public static bool IsMarker(this PhysicalSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                PhysicalSystem.MarkerArcadeEnd
                    or PhysicalSystem.MarkerComputerEnd
                    or PhysicalSystem.MarkerDiscBasedConsoleEnd
                    or PhysicalSystem.MarkerOtherEnd => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system is considered XGD
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system is XGD, false otherwise</returns>
        public static bool IsXGD(this PhysicalSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                PhysicalSystem.MicrosoftXbox
                    or PhysicalSystem.MicrosoftXbox360
                    or PhysicalSystem.MicrosoftXboxOne
                    or PhysicalSystem.MicrosoftXboxSeriesXS => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// List all systems with their short usable names
        /// </summary>
        public static List<string> ListSystems()
        {
            var systems = (PhysicalSystem[])Enum.GetValues(typeof(PhysicalSystem));
            var knownSystems = Array.FindAll(systems, s => !s.IsMarker() && s.GetCategory() != SystemCategory.NONE);
            Array.Sort(knownSystems, (x, y) => (x.LongName() ?? string.Empty).CompareTo(y.LongName() ?? string.Empty));
            return [.. Array.ConvertAll(knownSystems, val => $"{val.ShortName()} - {val.LongName()}")];
        }

        /// <summary>
        /// Get the Redump longnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? LongName(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.LongName;

        /// <summary>
        /// Get the Redump longnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? LongName(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortName(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortName(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.ShortName;

        /// <summary>
        /// Determine the category of a system
        /// </summary>
        public static SystemCategory GetCategory(this PhysicalSystem system)
            => ((PhysicalSystem?)system).GetCategory();

        /// <summary>
        /// Determine the category of a system
        /// </summary>
        public static SystemCategory GetCategory(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.Category ?? SystemCategory.NONE;

        /// <summary>
        /// Determine if a system is available in Redump yet
        /// </summary>
        public static bool IsAvailable(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.Available ?? false;

        /// <summary>
        /// Determine if a system is available in Redump yet
        /// </summary>
        public static bool IsAvailable(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.Available ?? false;

        /// <summary>
        /// Determine if a system is restricted to dumpers
        /// </summary>
        public static bool IsBanned(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.IsBanned ?? false;

        /// <summary>
        /// Determine if a system is restricted to dumpers
        /// </summary>
        public static bool IsBanned(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.IsBanned ?? false;

        /// <summary>
        /// Determine if a system has a CUE pack
        /// </summary>
        public static bool HasCues(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasCues ?? false;

        /// <summary>
        /// Determine if a system has a CUE pack
        /// </summary>
        public static bool HasCues(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasCues ?? false;

        /// <summary>
        /// Determine if a system has a DAT
        /// </summary>
        public static bool HasDat(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDat ?? false;

        /// <summary>
        /// Determine if a system has a DAT
        /// </summary>
        public static bool HasDat(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDat ?? false;

        /// <summary>
        /// Determine if a system has a decrypted keys pack
        /// </summary>
        public static bool HasDkeys(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDkeys ?? false;

        /// <summary>
        /// Determine if a system has a decrypted keys pack
        /// </summary>
        public static bool HasDkeys(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDkeys ?? false;

        /// <summary>
        /// Determine if a system has a GDI pack
        /// </summary>
        public static bool HasGdi(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasGdi ?? false;

        /// <summary>
        /// Determine if a system has a GDI pack
        /// </summary>
        public static bool HasGdi(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasGdi ?? false;

        /// <summary>
        /// Determine if a system has a keys pack
        /// </summary>
        public static bool HasKeys(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasKeys ?? false;

        /// <summary>
        /// Determine if a system has a keys pack
        /// </summary>
        public static bool HasKeys(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasKeys ?? false;

        /// <summary>
        /// Determine if a system has an LSD pack
        /// </summary>
        public static bool HasLsd(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasLsd ?? false;

        /// <summary>
        /// Determine if a system has an LSD pack
        /// </summary>
        public static bool HasLsd(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasLsd ?? false;

        /// <summary>
        /// Determine if a system has an SBI pack
        /// </summary>
        public static bool HasSbi(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasSbi ?? false;

        /// <summary>
        /// Determine if a system has an SBI pack
        /// </summary>
        public static bool HasSbi(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasSbi ?? false;

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
            var redumpSystems = (PhysicalSystem[])Enum.GetValues(typeof(PhysicalSystem));

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

        #region Region

        /// <summary>
        /// Get the Redump longnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? LongName(this Region region)
            => AttributeHelper<Region>.GetHumanReadableAttribute(region)?.LongName;

        /// <summary>
        /// Get the Redump longnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? LongName(this Region? region)
            => AttributeHelper<Region?>.GetHumanReadableAttribute(region)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? ShortName(this Region region)
            => AttributeHelper<Region>.GetHumanReadableAttribute(region)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? ShortName(this Region? region)
            => AttributeHelper<Region?>.GetHumanReadableAttribute(region)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="region">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static Region? ToRegion(this string? region)
        {
            // No value means no match
            if (region is null || region.Length == 0)
                return null;

            region = region.ToLowerInvariant();
            var redumpSystems = (Region[])Enum.GetValues(typeof(Region));

            // Check short names
            int index = Array.FindIndex(redumpSystems, s => region == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return redumpSystems[index];

            // Check long names
            index = Array.FindIndex(redumpSystems, s => region == s.LongName()?.ToLowerInvariant()
                || region == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return redumpSystems[index];

            return null;
        }

        #endregion
    }
}
