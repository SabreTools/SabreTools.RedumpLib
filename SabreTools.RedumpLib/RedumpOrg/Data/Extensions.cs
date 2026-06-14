using System;
using System.Collections.Generic;
using SabreTools.RedumpLib.Attributes;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.RedumpOrg.Data
{
    /// <summary>
    /// Information pertaining to Redump systems
    /// </summary>
#pragma warning disable IDE0010
#pragma warning disable IDE0072
    public static class Extensions
    {
        #region Cross-Enumeration

        /// <summary>
        /// Convert master list of all media types to currently known Redump disc types
        /// </summary>
        /// <param name="mediaType">MediaType value to check</param>
        /// <returns>DiscType if possible, null on error</returns>
        public static DiscType? ToDiscType(this PhysicalMediaType? mediaType)
        {
            return mediaType switch
            {
                PhysicalMediaType.BluRay => DiscType.BD50,
                PhysicalMediaType.CDROM => DiscType.CD,
                PhysicalMediaType.DVD => DiscType.DVD9,
                PhysicalMediaType.GDROM => DiscType.GDROM,
                PhysicalMediaType.HDDVD => DiscType.HDDVDSL,
                // PhysicalMediaType.MILCD => DiscType.MILCD, // TODO: Support this?
                PhysicalMediaType.NintendoGameCubeGameDisc => DiscType.NintendoGameCubeGameDisc,
                PhysicalMediaType.NintendoWiiOpticalDisc => DiscType.NintendoWiiOpticalDiscDL,
                PhysicalMediaType.NintendoWiiUOpticalDisc => DiscType.NintendoWiiUOpticalDiscSL,
                PhysicalMediaType.UMD => DiscType.UMDDL,
                _ => null,
            };
        }

        /// <summary>
        /// Convert currently known Redump disc types to master list of all media types
        /// </summary>
        /// <param name="discType">DiscType value to check</param>
        /// <returns>MediaType if possible, null on error</returns>
        public static PhysicalMediaType? ToMediaType(this DiscType? discType)
        {
            return discType switch
            {
                DiscType.BD25
                    or DiscType.BD33
                    or DiscType.BD50
                    or DiscType.BD66
                    or DiscType.BD100
                    or DiscType.BD128 => PhysicalMediaType.BluRay,
                DiscType.CD => PhysicalMediaType.CDROM,
                DiscType.DVD5
                    or DiscType.DVD9 => PhysicalMediaType.DVD,
                DiscType.GDROM => PhysicalMediaType.GDROM,
                DiscType.HDDVDSL
                    or DiscType.HDDVDDL => PhysicalMediaType.HDDVD,
                // DiscType.MILCD => PhysicalMediaType.MILCD, // TODO: Support this?
                DiscType.NintendoGameCubeGameDisc => PhysicalMediaType.NintendoGameCubeGameDisc,
                DiscType.NintendoWiiOpticalDiscSL
                    or DiscType.NintendoWiiOpticalDiscDL => PhysicalMediaType.NintendoWiiOpticalDisc,
                DiscType.NintendoWiiUOpticalDiscSL => PhysicalMediaType.NintendoWiiUOpticalDisc,
                DiscType.UMDSL
                    or DiscType.UMDDL => PhysicalMediaType.UMD,
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

        #region Disc Type

        /// <summary>
        /// Get the Redump longnames for each known disc type
        /// </summary>
        /// <param name="discType"></param>
        /// <returns></returns>
        public static string? LongName(this DiscType discType)
            => ((DiscType?)discType).LongName();

        /// <summary>
        /// Get the Redump longnames for each known disc type
        /// </summary>
        /// <param name="discType"></param>
        /// <returns></returns>
        public static string? LongName(this DiscType? discType)
            => AttributeHelper<DiscType?>.GetHumanReadableAttribute(discType)?.LongName;

        /// <summary>
        /// Get the DiscType enum value for a given string
        /// </summary>
        /// <param name="discType">String value to convert</param>
        /// <returns>DiscType represented by the string, if possible</returns>
        public static DiscType? ToDiscType(this string? discType)
        {
            // No value means no match
            if (discType is null || discType.Length == 0)
                return null;

            discType = discType.ToLowerInvariant();
            var discTypes = (DiscType[])Enum.GetValues(typeof(DiscType));

            // Check long names
            int index = Array.FindIndex(discTypes, s => discType == s.LongName()?.ToLowerInvariant()
                || discType == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant()
                || discType == s.LongName()?.Replace("-", string.Empty)?.ToLowerInvariant()
                || discType == s.LongName()?
                    .Replace(" ", string.Empty)?
                    .Replace("-", string.Empty)?
                    .ToLowerInvariant());
            if (index > -1)
                return discTypes[index];

            // Special cases
            return (discType?.ToLowerInvariant()) switch
            {
                "bd"
                    or "bdrom"
                    or "bd-rom"
                    or "bluray"
                    or "blu-ray" => DiscType.BD25,
                "cdrom"
                    or "cd-rom" => DiscType.CD,
                "dvd"
                    or "dvd-rom" => DiscType.DVD5,
                "gd" => DiscType.GDROM,
                "hddvd"
                    or "hd-dvd" => DiscType.HDDVDSL,
                "umd" => DiscType.UMDSL,

                _ => null,
            };
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

        #region Site Code

        /// <summary>
        /// List all site codes with their short usable names
        /// </summary>
        public static List<string> ListSiteCodes()
        {
            var siteCodes = new List<string>();

            foreach (var val in Enum.GetValues(typeof(SiteCode)))
            {
                string? shortName = ((SiteCode?)val).ShortName()?.TrimEnd(':');
                string? longName = ((SiteCode?)val).LongName()?.TrimEnd(':');

                bool isCommentCode = ((SiteCode?)val).IsCommentCode();
                bool isContentCode = ((SiteCode?)val).IsContentCode();
                bool isMultiline = ((SiteCode?)val).IsMultiLine();

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
                if (isCommentCode)
                    additionalInfo.Add("Comment Field");
                if (isContentCode)
                    additionalInfo.Add("Content Field");
                if (isMultiline)
                    additionalInfo.Add("Multiline");
                if (additionalInfo.Count > 0)
                    siteCode += $"[{string.Join(", ", [.. additionalInfo])}]";

                // Add the formatted site code
                siteCodes.Add(siteCode);
            }

            return siteCodes;
        }

        /// <summary>
        /// Check if a site code is boolean or not
        /// </summary>
        /// <param name="siteCode">SiteCode to check</param>
        /// <returns>True if the code field is a flag with no value, false otherwise</returns>
        public static bool IsBoolean(this SiteCode siteCode)
            => ((SiteCode?)siteCode).IsBoolean();

        /// <summary>
        /// Check if a site code is boolean or not
        /// </summary>
        /// <param name="siteCode">SiteCode to check</param>
        /// <returns>True if the code field is a flag with no value, false otherwise</returns>
        public static bool IsBoolean(this SiteCode? siteCode)
        {
            return siteCode switch
            {
                SiteCode.PCMacHybrid => true,
                SiteCode.PostgapType => true,
                SiteCode.VCD => true,
                _ => false,
            };
        }

        /// <summary>
        /// Check if a site code should live in the comments section
        /// </summary>
        /// <returns>True if the code field is in comments by default, false otherwise</returns>
        public static bool IsCommentCode(this SiteCode siteCode)
            => ((SiteCode?)siteCode).IsCommentCode();

        /// <summary>
        /// Check if a site code should live in the comments section
        /// </summary>
        /// <returns>True if the code field is in comments by default, false otherwise</returns>
        public static bool IsCommentCode(this SiteCode? siteCode)
        {
            return siteCode switch
            {
                // Identifying Info
                SiteCode.AdditionalBCAData => true,
                SiteCode.AlternativeTitle => true,
                SiteCode.AlternativeForeignTitle => true,
                SiteCode.BBFCRegistrationNumber => true,
                SiteCode.CompatibleOS => true,
                SiteCode.CoverID => true,
                SiteCode.DiscHologramID => true,
                SiteCode.DiscID => true,
                SiteCode.DiscTitleNonLatin => true,
                SiteCode.DMIHash => true,
                SiteCode.DNASDiscID => true,
                SiteCode.EditionNonLatin => true,
                SiteCode.Filename => true,
                SiteCode.Genre => true,
                SiteCode.HighSierraVolumeDescriptor => true,
                SiteCode.InternalName => true,
                SiteCode.InternalSerialName => true,
                SiteCode.ISBN => true,
                SiteCode.ISSN => true,
                SiteCode.LogsLink => true,
                SiteCode.Multisession => true,
                SiteCode.PCMacHybrid => true,
                SiteCode.PFIHash => true,
                SiteCode.PostgapType => true,
                SiteCode.PPN => true,
                SiteCode.Protection => true,
                SiteCode.RingNonZeroDataStart => true,
                SiteCode.RingPerfectAudioOffset => true,
                SiteCode.Series => true,
                SiteCode.SSHash => true,
                SiteCode.SSVersion => true,
                SiteCode.SteamAppID => true,
                SiteCode.TitleID => true,
                SiteCode.UniversalHash => true,
                SiteCode.VCD => true,
                SiteCode.VFCCode => true,
                SiteCode.VolumeLabel => true,
                SiteCode.XeMID => true,
                SiteCode.XMID => true,

                // Publisher / Company IDs
                SiteCode.TwoKGamesID => true,
                SiteCode.AcclaimID => true,
                SiteCode.AccoladeID => true,
                SiteCode.ActivisionID => true,
                SiteCode.BandaiID => true,
                SiteCode.BethesdaID => true,
                SiteCode.CDProjektID => true,
                SiteCode.DisneyInteractiveID => true,
                SiteCode.EidosID => true,
                SiteCode.ElectronicArtsID => true,
                SiteCode.FoxInteractiveID => true,
                SiteCode.GTInteractiveID => true,
                SiteCode.InterplayID => true,
                SiteCode.JASRACID => true,
                SiteCode.KingRecordsID => true,
                SiteCode.KoeiID => true,
                SiteCode.KonamiID => true,
                SiteCode.LucasArtsID => true,
                SiteCode.MicrosoftID => true,
                SiteCode.NaganoID => true,
                SiteCode.NamcoID => true,
                SiteCode.NipponIchiSoftwareID => true,
                SiteCode.OriginID => true,
                SiteCode.PonyCanyonID => true,
                SiteCode.SegaID => true,
                SiteCode.SelenID => true,
                SiteCode.SierraID => true,
                SiteCode.TaitoID => true,
                SiteCode.UbisoftID => true,
                SiteCode.ValveID => true,

                _ => false,
            };
        }

        /// <summary>
        /// Check if a site code should live in the contents section
        /// </summary>
        /// <returns>True if the code field is in contents by default, false otherwise</returns>
        public static bool IsContentCode(this SiteCode siteCode)
            => ((SiteCode?)siteCode).IsContentCode();

        /// <summary>
        /// Check if a site code should live in the contents section
        /// </summary>
        /// <returns>True if the code field is in contents by default, false otherwise</returns>
        public static bool IsContentCode(this SiteCode? siteCode)
        {
            return siteCode switch
            {
                SiteCode.Applications => true,
                SiteCode.Extras => true,
                SiteCode.GameFootage => true,
                SiteCode.Games => true,
                SiteCode.NetYarozeGames => true,
                SiteCode.Patches => true,
                SiteCode.PlayableDemos => true,
                SiteCode.RollingDemos => true,
                SiteCode.Savegames => true,
                SiteCode.SteamSimSidDepotID => true,
                SiteCode.SteamCsmCsdDepotID => true,
                SiteCode.TechDemos => true,
                SiteCode.Videos => true,
                _ => false,
            };
        }

        /// <summary>
        /// Check if a site code is multi-line or not
        /// </summary>
        /// <returns>True if the code field is multiline by default, false otherwise</returns>
        public static bool IsMultiLine(this SiteCode siteCode)
            => ((SiteCode?)siteCode).IsMultiLine();

        /// <summary>
        /// Check if a site code is multi-line or not
        /// </summary>
        /// <returns>True if the code field is multiline by default, false otherwise</returns>
        public static bool IsMultiLine(this SiteCode? siteCode)
        {
            return siteCode switch
            {
                SiteCode.Extras => true,
                SiteCode.Filename => true,
                SiteCode.Games => true,
                SiteCode.GameFootage => true,
                SiteCode.HighSierraVolumeDescriptor => true,
                SiteCode.Multisession => true,
                SiteCode.NetYarozeGames => true,
                SiteCode.Patches => true,
                SiteCode.PlayableDemos => true,
                SiteCode.RollingDemos => true,
                SiteCode.Savegames => true,
                SiteCode.SteamSimSidDepotID => true,
                SiteCode.SteamCsmCsdDepotID => true,
                SiteCode.TechDemos => true,
                SiteCode.Videos => true,
                _ => false,
            };
        }

        /// <summary>
        /// Get the HTML version for each known site code
        /// </summary>
        public static string? LongName(this SiteCode siteCode)
            => AttributeHelper<SiteCode>.GetHumanReadableAttribute(siteCode)?.LongName;

        /// <summary>
        /// Get the HTML version for each known site code
        /// </summary>
        public static string? LongName(this SiteCode? siteCode)
            => AttributeHelper<SiteCode?>.GetHumanReadableAttribute(siteCode)?.LongName;

        /// <summary>
        /// Get the short tag for each known site code
        /// </summary>
        public static string? ShortName(this SiteCode siteCode)
            => AttributeHelper<SiteCode>.GetHumanReadableAttribute(siteCode)?.ShortName;

        /// <summary>
        /// Get the short tag for each known site code
        /// </summary>
        public static string? ShortName(this SiteCode? siteCode)
            => AttributeHelper<SiteCode?>.GetHumanReadableAttribute(siteCode)?.ShortName;

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
#pragma warning restore IDE0010
#pragma warning restore IDE0072
}
