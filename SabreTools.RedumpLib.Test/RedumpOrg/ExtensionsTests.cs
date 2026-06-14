using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.RedumpOrg.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.RedumpOrg
{
    public class ExtensionsTests
    {
        #region Cross-Enumeration

        /// <summary>
        /// DiscType values that map to PhysicalMediaType
        /// </summary>
        private static readonly DiscType?[] _mappableDiscTypes =
        [
            DiscType.BD25,
            DiscType.BD33,
            DiscType.BD50,
            DiscType.BD66,
            DiscType.BD100,
            DiscType.BD128,
            DiscType.CD,
            DiscType.DVD5,
            DiscType.DVD9,
            DiscType.GDROM,
            DiscType.HDDVDSL,
            DiscType.HDDVDDL,
            DiscType.NintendoGameCubeGameDisc,
            DiscType.NintendoWiiOpticalDiscSL,
            DiscType.NintendoWiiOpticalDiscDL,
            DiscType.NintendoWiiUOpticalDiscSL,
            DiscType.UMDSL,
            DiscType.UMDDL,
        ];

        /// <summary>
        /// PhysicalMediaType values that map to DiscType
        /// </summary>
        private static readonly PhysicalMediaType?[] _mappablePhysicalMediaTypes =
        [
            PhysicalMediaType.BluRay,
            PhysicalMediaType.CDROM,
            PhysicalMediaType.DVD,
            PhysicalMediaType.GDROM,
            PhysicalMediaType.HDDVD,
            PhysicalMediaType.NintendoGameCubeGameDisc,
            PhysicalMediaType.NintendoWiiOpticalDisc,
            PhysicalMediaType.NintendoWiiUOpticalDisc,
            PhysicalMediaType.UMD,
        ];

        /// <summary>
        /// Check that both mappable and unmappable media types output correctly
        /// </summary>
        /// <param name="mediaType">MediaType value to check</param>
        /// <param name="expectNull">True to expect a null mapping, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateMediaTypeMappingTestData))]
        public void ToDiscTypeTest(PhysicalMediaType? mediaType, bool expectNull)
        {
            DiscType? actual = mediaType.ToDiscType();
            Assert.Equal(expectNull, actual is null);
        }

        /// <summary>
        /// Check that DiscType values all map to something appropriate
        /// </summary>
        /// <param name="discType">DiscType value to check</param>
        /// <param name="expectNull">True to expect a null mapping, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscTypeMappingTestData))]
        public void ToMediaTypeTest(DiscType? discType, bool expectNull)
        {
            PhysicalMediaType? actual = discType.ToMediaType();
            Assert.Equal(expectNull, actual is null);
        }

        /// <summary>
        /// Generate a test set of DiscType values
        /// </summary>
        /// <returns>MemberData-compatible list of DiscType values</returns>
        public static TheoryData<DiscType?, bool> GenerateDiscTypeMappingTestData()
        {
            var testData = new TheoryData<DiscType?, bool>() { { null, true } };
            foreach (DiscType? discType in Enum.GetValues<DiscType>().Cast<DiscType?>())
            {
                if (_mappableDiscTypes.Contains(discType))
                    testData.Add(discType, false);
                else
                    testData.Add(discType, true);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of mappable media types
        /// </summary>
        /// <returns>MemberData-compatible list of MediaTypes</returns>
        public static TheoryData<PhysicalMediaType?, bool> GenerateMediaTypeMappingTestData()
        {
            var testData = new TheoryData<PhysicalMediaType?, bool>() { { null, true } };

            foreach (PhysicalMediaType? mediaType in Enum.GetValues<PhysicalMediaType>().Cast<PhysicalMediaType?>())
            {
                if (_mappablePhysicalMediaTypes.Contains(mediaType))
                    testData.Add(mediaType, false);
                else
                    testData.Add(mediaType, true);
            }

            return testData;
        }

        #endregion

        #region Disc Category

        /// <summary>
        /// Check that every DiscCategory has a long name provided
        /// </summary>
        /// <param name="discCategory">DiscCategory value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscCategoryTestData))]
        public void DiscCategory_LongName(DiscCategory? discCategory, bool expectNull)
        {
            var actual = discCategory.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every DiscCategory can be mapped from a string
        /// </summary>
        /// <param name="discCategory">DiscCategory value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscCategoryTestData))]
        public void DiscCategory_ToDiscCategory(DiscCategory? discCategory, bool expectNull)
        {
            string? longName = discCategory.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);

            var actualNormal = longName.ToDiscCategory();
            var actualSpaceless = longNameSpaceless.ToDiscCategory();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(discCategory, actualNormal);
                Assert.Equal(discCategory, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of DiscCategory values
        /// </summary>
        /// <returns>MemberData-compatible list of DiscCategory values</returns>
        public static TheoryData<DiscCategory?, bool> GenerateDiscCategoryTestData()
        {
            var testData = new TheoryData<DiscCategory?, bool>() { { null, true } };
            foreach (DiscCategory? discCategory in Enum.GetValues<DiscCategory>().Cast<DiscCategory?>())
            {
                testData.Add(discCategory, false);
            }

            return testData;
        }

        #endregion

        #region Disc Subpath

        /// <summary>
        /// Check that every DiscSubpath has a long name provided
        /// </summary>
        /// <param name="discSubpath">DiscSubpath value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscSubpathTestData))]
        public void DiscSubpath_LongName(DiscSubpath? discSubpath, bool expectNull)
        {
            var actual = discSubpath.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every DiscSubpath has a short name provided
        /// </summary>
        /// <param name="discSubpath">DiscSubpath value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscSubpathTestData))]
        public void DiscSubpath_ShortName(DiscSubpath? discSubpath, bool expectNull)
        {
            var actual = discSubpath.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Ensure that every DiscSubpath that has a short name that is unique
        /// </summary>
        [Fact]
        public void DiscSubpath_ShortName_NoDuplicates()
        {
            var fullDiscSubpaths = Enum.GetValues<DiscSubpath>().Cast<DiscSubpath>().ToList();
            var filteredDiscSubpaths = new Dictionary<string, DiscSubpath?>();

            int totalCount = 0;
            foreach (DiscSubpath? discSubpath in fullDiscSubpaths)
            {
                var code = discSubpath.ShortName();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredDiscSubpaths.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredDiscSubpaths[code] = discSubpath;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredDiscSubpaths.Count);
        }

        /// <summary>
        /// Check that every DiscSubpath can be mapped from a string
        /// </summary>
        /// <param name="discSubpath">DiscSubpath value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscSubpathTestData))]
        public void DiscSubpath_ToDiscSubpath(DiscSubpath? discSubpath, bool expectNull)
        {
            string? longName = discSubpath.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);

            var actualNormal = longName.ToDiscSubpath();
            var actualSpaceless = longNameSpaceless.ToDiscSubpath();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(discSubpath, actualNormal);
                Assert.Equal(discSubpath, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of DiscSubpath values
        /// </summary>
        /// <returns>MemberData-compatible list of DiscSubpath values</returns>
        public static TheoryData<DiscSubpath?, bool> GenerateDiscSubpathTestData()
        {
            var testData = new TheoryData<DiscSubpath?, bool>() { { null, true } };
            foreach (DiscSubpath? discSubpath in Enum.GetValues<DiscSubpath>().Cast<DiscSubpath?>())
            {
                testData.Add(discSubpath, false);
            }

            return testData;
        }

        #endregion

        #region Disc Type

        /// <summary>
        /// Check that every DiscType has a long name provided
        /// </summary>
        /// <param name="discType">DiscType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscTypeTestData))]
        public void DiscType_LongName(DiscType? discType, bool expectNull)
        {
            var actual = discType.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every DiscType can be mapped from a string
        /// </summary>
        /// <param name="discType">DiscType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscTypeTestData))]
        public void DiscType_ToDiscType(DiscType? discType, bool expectNull)
        {
            string? longName = discType.LongName();
            string? longNameSpaceless = longName?
                .Replace(" ", string.Empty)
                .Replace("-", string.Empty);

            var actualNormal = longName.ToDiscType();
            var actualSpaceless = longNameSpaceless.ToDiscType();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(discType, actualNormal);
                Assert.Equal(discType, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of DiscType values
        /// </summary>
        /// <returns>MemberData-compatible list of DiscType values</returns>
        public static TheoryData<DiscType?, bool> GenerateDiscTypeTestData()
        {
            var testData = new TheoryData<DiscType?, bool>() { { null, true } };
            foreach (DiscType? discType in Enum.GetValues<DiscType>().Cast<DiscType?>())
            {
                if (discType == DiscType.NONE)
                    testData.Add(discType, true);
                else
                    testData.Add(discType, false);
            }

            return testData;
        }

        #endregion

        #region Dump Status

        /// <summary>
        /// Check that every DumpStatus has a long name provided
        /// </summary>
        /// <param name="dumpStatus">DumpStatus value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDumpStatusTestData))]
        public void DumpStatus_LongName(DumpStatus? dumpStatus, bool expectNull)
        {
            var actual = dumpStatus.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every DumpStatus has a short name provided
        /// </summary>
        /// <param name="dumpStatus">DumpStatus value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDumpStatusTestData))]
        public void DumpStatus_ShortName(DumpStatus? dumpStatus, bool expectNull)
        {
            var actual = dumpStatus.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Ensure that every DumpStatus that has a short name that is unique
        /// </summary>
        [Fact]
        public void DumpStatus_ShortName_NoDuplicates()
        {
            var fullDumpStatuses = Enum.GetValues<DumpStatus>().Cast<DumpStatus>().ToList();
            var filteredDumpStatuses = new Dictionary<string, DumpStatus?>();

            int totalCount = 0;
            foreach (DumpStatus? dumpStatus in fullDumpStatuses)
            {
                var code = dumpStatus.ShortName();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredDumpStatuses.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredDumpStatuses[code] = dumpStatus;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredDumpStatuses.Count);
        }

        /// <summary>
        /// Check that every DumpStatus can be mapped from a string
        /// </summary>
        /// <param name="dumpStatus">DumpStatus value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDumpStatusTestData))]
        public void DumpStatus_ToDumpStatus(DumpStatus? dumpStatus, bool expectNull)
        {
            string? longName = dumpStatus.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);
            string? possibleInteger = $"{(int?)dumpStatus}";

            var actualNormal = longName.ToDumpStatus();
            var actualSpaceless = longNameSpaceless.ToDumpStatus();
            var actualInteger = possibleInteger.ToDumpStatus();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
                Assert.Null(actualInteger);
            }
            else
            {
                Assert.Equal(dumpStatus, actualNormal);
                Assert.Equal(dumpStatus, actualSpaceless);
                Assert.Equal(dumpStatus, actualInteger);
            }
        }

        /// <summary>
        /// Generate a test set of DumpStatus values
        /// </summary>
        /// <returns>MemberData-compatible list of DumpStatus values</returns>
        public static TheoryData<DumpStatus?, bool> GenerateDumpStatusTestData()
        {
            var testData = new TheoryData<DumpStatus?, bool>() { { null, true } };
            foreach (DumpStatus? dumpStatus in Enum.GetValues<DumpStatus>().Cast<DumpStatus?>())
            {
                testData.Add(dumpStatus, false);
            }

            return testData;
        }

        #endregion

        #region Language Selection

        /// <summary>
        /// Check that every LanguageSelection has a long name provided
        /// </summary>
        /// <param name="languageSelection">LanguageSelection value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateLanguageSelectionTestData))]
        public void LanguageSelection_LongName(LanguageSelection? languageSelection, bool expectNull)
        {
            var actual = languageSelection.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every LanguageSelection can be mapped from a string
        /// </summary>
        /// <param name="languageSelection">LanguageSelection value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateLanguageSelectionTestData))]
        public void Region_ToLanguageSelection(LanguageSelection? languageSelection, bool expectNull)
        {
            string? longName = languageSelection.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);

            var actualNormal = longName.ToLanguageSelection();
            var actualSpaceless = longNameSpaceless.ToLanguageSelection();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(languageSelection, actualNormal);
                Assert.Equal(languageSelection, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of LanguageSelection values
        /// </summary>
        /// <returns>MemberData-compatible list of LanguageSelection values</returns>
        public static TheoryData<LanguageSelection?, bool> GenerateLanguageSelectionTestData()
        {
            var testData = new TheoryData<LanguageSelection?, bool>() { { null, true } };
            foreach (LanguageSelection? languageSelection in Enum.GetValues<LanguageSelection>().Cast<LanguageSelection?>())
            {
                testData.Add(languageSelection, false);
            }

            return testData;
        }

        #endregion

        #region Pack Type

        /// <summary>
        /// Check that every PackType has a long name provided
        /// </summary>
        /// <param name="packType">PackType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GeneratePackTypeTestData))]
        public void PackType_LongName(PackType? packType, bool expectNull)
        {
            var actual = packType.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every PackType has a short name provided
        /// </summary>
        /// <param name="packType">PackType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GeneratePackTypeTestData))]
        public void PackType_ShortName(PackType? packType, bool expectNull)
        {
            var actual = packType.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Ensure that every PackType that has a short name that is unique
        /// </summary>
        [Fact]
        public void PackType_ShortName_NoDuplicates()
        {
            var fullPackTypes = Enum.GetValues<PackType>().Cast<PackType>().ToList();
            var filteredPackTypes = new Dictionary<string, PackType?>();

            int totalCount = 0;
            foreach (PackType? packType in fullPackTypes)
            {
                var code = packType.ShortName();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredPackTypes.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredPackTypes[code] = packType;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredPackTypes.Count);
        }

        /// <summary>
        /// Check that every PackType can be mapped from a string
        /// </summary>
        /// <param name="packType">PackType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GeneratePackTypeTestData))]
        public void PackType_ToPackType(PackType? packType, bool expectNull)
        {
            string? longName = packType.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);

            var actualNormal = longName.ToPackType();
            var actualSpaceless = longNameSpaceless.ToPackType();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(packType, actualNormal);
                Assert.Equal(packType, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of PackType values
        /// </summary>
        /// <returns>MemberData-compatible list of PackType values</returns>
        public static TheoryData<PackType?, bool> GeneratePackTypeTestData()
        {
            var testData = new TheoryData<PackType?, bool>() { { null, true } };
            foreach (PackType? packType in Enum.GetValues<PackType>().Cast<PackType?>())
            {
                testData.Add(packType, false);
            }

            return testData;
        }

        #endregion

        #region Site Code

        /// <summary>
        /// SiteCode values that are considered Boolean
        /// </summary>
        private static readonly SiteCode?[] _booleanSiteCodes =
        [
            SiteCode.PCMacHybrid,
            SiteCode.PostgapType,
            SiteCode.VCD,
        ];

        /// <summary>
        /// SiteCode values that are considered Comment
        /// </summary>
        private static readonly SiteCode?[] _commentSiteCodes =
        [
            // Identifying Info
            SiteCode.AdditionalBCAData,
            SiteCode.AlternativeTitle,
            SiteCode.AlternativeForeignTitle,
            SiteCode.BBFCRegistrationNumber,
            SiteCode.CompatibleOS,
            SiteCode.CoverID,
            SiteCode.DiscHologramID,
            SiteCode.DiscID,
            SiteCode.DiscTitleNonLatin,
            SiteCode.DMIHash,
            SiteCode.DNASDiscID,
            SiteCode.EditionNonLatin,
            SiteCode.Filename,
            SiteCode.Genre,
            SiteCode.HighSierraVolumeDescriptor,
            SiteCode.InternalName,
            SiteCode.InternalSerialName,
            SiteCode.ISBN,
            SiteCode.ISSN,
            SiteCode.LogsLink,
            SiteCode.Multisession,
            SiteCode.PCMacHybrid,
            SiteCode.PFIHash,
            SiteCode.PostgapType,
            SiteCode.PPN,
            SiteCode.Protection,
            SiteCode.RingNonZeroDataStart,
            SiteCode.RingPerfectAudioOffset,
            SiteCode.Series,
            SiteCode.SSHash,
            SiteCode.SSVersion,
            SiteCode.SteamAppID,
            SiteCode.TitleID,
            SiteCode.UniversalHash,
            SiteCode.VCD,
            SiteCode.VFCCode,
            SiteCode.VolumeLabel,
            SiteCode.XeMID,
            SiteCode.XMID,

            // Publisher / Company IDs
            SiteCode.TwoKGamesID,
            SiteCode.AcclaimID,
            SiteCode.AccoladeID,
            SiteCode.ActivisionID,
            SiteCode.BandaiID,
            SiteCode.BethesdaID,
            SiteCode.CDProjektID,
            SiteCode.DisneyInteractiveID,
            SiteCode.EidosID,
            SiteCode.ElectronicArtsID,
            SiteCode.FoxInteractiveID,
            SiteCode.GTInteractiveID,
            SiteCode.InterplayID,
            SiteCode.JASRACID,
            SiteCode.KingRecordsID,
            SiteCode.KoeiID,
            SiteCode.KonamiID,
            SiteCode.LucasArtsID,
            SiteCode.MicrosoftID,
            SiteCode.NaganoID,
            SiteCode.NamcoID,
            SiteCode.NipponIchiSoftwareID,
            SiteCode.OriginID,
            SiteCode.PonyCanyonID,
            SiteCode.SegaID,
            SiteCode.SelenID,
            SiteCode.SierraID,
            SiteCode.TaitoID,
            SiteCode.UbisoftID,
            SiteCode.ValveID,
        ];

        /// <summary>
        /// SiteCode values that are considered Content
        /// </summary>
        private static readonly SiteCode?[] _contentSiteCodes =
        [
            SiteCode.Applications,
            SiteCode.Extras,
            SiteCode.GameFootage,
            SiteCode.Games,
            SiteCode.NetYarozeGames,
            SiteCode.Patches,
            SiteCode.PlayableDemos,
            SiteCode.RollingDemos,
            SiteCode.Savegames,
            SiteCode.SteamSimSidDepotID,
            SiteCode.SteamCsmCsdDepotID,
            SiteCode.TechDemos,
            SiteCode.Videos,
        ];

        /// <summary>
        /// SiteCode values that are considered Multiline
        /// </summary>
        private static readonly SiteCode?[] _multilineSiteCodes =
        [
            SiteCode.Extras,
            SiteCode.Filename,
            SiteCode.Games,
            SiteCode.GameFootage,
            SiteCode.HighSierraVolumeDescriptor,
            SiteCode.Multisession,
            SiteCode.NetYarozeGames,
            SiteCode.Patches,
            SiteCode.PlayableDemos,
            SiteCode.RollingDemos,
            SiteCode.Savegames,
            SiteCode.SteamSimSidDepotID,
            SiteCode.SteamCsmCsdDepotID,
            SiteCode.TechDemos,
            SiteCode.Videos,
        ];

        [Fact]
        public void SiteCode_ListSiteCodes()
        {
            var actual = RedumpLib.RedumpOrg.Data.Extensions.ListSiteCodes();
            Assert.NotEmpty(actual);
        }

        /// <summary>
        /// Check that all boolean site codes are marked properly
        /// </summary>
        /// <param name="siteCode">SiteCode value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateBooleanSiteCodesTestData))]
        public void SiteCode_IsBoolean(SiteCode? siteCode, bool expected)
        {
            bool actual = siteCode.IsBoolean();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all comment site codes are marked properly
        /// </summary>
        /// <param name="siteCode">SiteCode value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateCommentSiteCodesTestData))]
        public void SiteCode_IsCommentCode(SiteCode? siteCode, bool expected)
        {
            bool actual = siteCode.IsCommentCode();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all content site codes are marked properly
        /// </summary>
        /// <param name="siteCode">SiteCode value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateContentSiteCodesTestData))]
        public void SiteCode_IsContentCode(SiteCode? siteCode, bool expected)
        {
            bool actual = siteCode.IsContentCode();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all multiline site codes are marked properly
        /// </summary>
        /// <param name="siteCode">SiteCode value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateMultilineSiteCodesTestData))]
        public void SiteCode_IsMultiLine(SiteCode? siteCode, bool expected)
        {
            bool actual = siteCode.IsMultiLine();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that every SiteCode has a long name provided
        /// </summary>
        /// <param name="siteCode">SiteCode value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateSiteCodeTestData))]
        public void SiteCode_LongName(SiteCode? siteCode, bool expectNull)
        {
            var actual = siteCode.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every SiteCode has a short name provided
        /// </summary>
        /// <param name="siteCode">SiteCode value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateSiteCodeTestData))]
        public void SiteCode_ShortName(SiteCode? siteCode, bool expectNull)
        {
            var actual = siteCode.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Generate a test set of SiteCode values
        /// </summary>
        /// <returns>MemberData-compatible list of SiteCode values</returns>
        public static TheoryData<SiteCode?, bool> GenerateSiteCodeTestData()
        {
            var testData = new TheoryData<SiteCode?, bool>() { { null, true } };
            foreach (SiteCode? siteCode in Enum.GetValues<SiteCode>().Cast<SiteCode?>())
            {
                testData.Add(siteCode, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of SiteCode values that are considered Boolean
        /// </summary>
        /// <returns>MemberData-compatible list of SiteCode values</returns>
        public static TheoryData<SiteCode?, bool> GenerateBooleanSiteCodesTestData()
        {
            var testData = new TheoryData<SiteCode?, bool>() { { null, false } };
            foreach (SiteCode siteCode in Enum.GetValues<SiteCode>())
            {
                if (_booleanSiteCodes.Contains(siteCode))
                    testData.Add(siteCode, true);
                else
                    testData.Add(siteCode, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of SiteCode values that are considered Comment
        /// </summary>
        /// <returns>MemberData-compatible list of SiteCode values</returns>
        public static TheoryData<SiteCode?, bool> GenerateCommentSiteCodesTestData()
        {
            var testData = new TheoryData<SiteCode?, bool>() { { null, false } };
            foreach (SiteCode siteCode in Enum.GetValues<SiteCode>())
            {
                if (_commentSiteCodes.Contains(siteCode))
                    testData.Add(siteCode, true);
                else
                    testData.Add(siteCode, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of SiteCode values that are considered Content
        /// </summary>
        /// <returns>MemberData-compatible list of SiteCode values</returns>
        public static TheoryData<SiteCode?, bool> GenerateContentSiteCodesTestData()
        {
            var testData = new TheoryData<SiteCode?, bool>() { { null, false } };
            foreach (SiteCode siteCode in Enum.GetValues<SiteCode>())
            {
                if (_contentSiteCodes.Contains(siteCode))
                    testData.Add(siteCode, true);
                else
                    testData.Add(siteCode, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of SiteCode values that are considered Multiline
        /// </summary>
        /// <returns>MemberData-compatible list of SiteCode values</returns>
        public static TheoryData<SiteCode?, bool> GenerateMultilineSiteCodesTestData()
        {
            var testData = new TheoryData<SiteCode?, bool>() { { null, false } };
            foreach (SiteCode siteCode in Enum.GetValues<SiteCode>())
            {
                if (_multilineSiteCodes.Contains(siteCode))
                    testData.Add(siteCode, true);
                else
                    testData.Add(siteCode, false);
            }

            return testData;
        }

        #endregion

        #region Sort Category

        /// <summary>
        /// Check that every SortCategory has a long name provided
        /// </summary>
        /// <param name="sortCategory">SortCategory value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateSortCategoryTestData))]
        public void SortCategory_LongName(SortCategory? sortCategory, bool expectNull)
        {
            var actual = sortCategory.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every SortCategory has a short name provided
        /// </summary>
        /// <param name="sortCategory">SortCategory value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateSortCategoryTestData))]
        public void SortCategory_ShortName(SortCategory? sortCategory, bool expectNull)
        {
            var actual = sortCategory.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Ensure that every SortCategory that has a short name that is unique
        /// </summary>
        [Fact]
        public void SortCategory_ShortName_NoDuplicates()
        {
            var fullSortCategorys = Enum.GetValues<SortCategory>().Cast<SortCategory>().ToList();
            var filteredSortCategorys = new Dictionary<string, SortCategory?>();

            int totalCount = 0;
            foreach (SortCategory? sortCategory in fullSortCategorys)
            {
                var code = sortCategory.ShortName();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredSortCategorys.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredSortCategorys[code] = sortCategory;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredSortCategorys.Count);
        }

        /// <summary>
        /// Check that every SortCategory can be mapped from a string
        /// </summary>
        /// <param name="sortCategory">SortCategory value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateSortCategoryTestData))]
        public void SortCategory_ToSortCategory(SortCategory? sortCategory, bool expectNull)
        {
            string? longName = sortCategory.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);

            var actualNormal = longName.ToSortCategory();
            var actualSpaceless = longNameSpaceless.ToSortCategory();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(sortCategory, actualNormal);
                Assert.Equal(sortCategory, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of SortCategory values
        /// </summary>
        /// <returns>MemberData-compatible list of SortCategory values</returns>
        public static TheoryData<SortCategory?, bool> GenerateSortCategoryTestData()
        {
            var testData = new TheoryData<SortCategory?, bool>() { { null, true } };
            foreach (SortCategory? sortCategory in Enum.GetValues<SortCategory>().Cast<SortCategory?>())
            {
                testData.Add(sortCategory, false);
            }

            return testData;
        }

        #endregion

        #region Sort Direction

        /// <summary>
        /// Check that every SortDirection has a long name provided
        /// </summary>
        /// <param name="sortDirection">SortDirection value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateSortDirectionTestData))]
        public void SortDirection_LongName(SortDirection? sortDirection, bool expectNull)
        {
            var actual = sortDirection.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every SortDirection has a short name provided
        /// </summary>
        /// <param name="sortDirection">SortDirection value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateSortDirectionTestData))]
        public void SortDirection_ShortName(SortDirection? sortDirection, bool expectNull)
        {
            var actual = sortDirection.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Ensure that every SortDirection that has a short name that is unique
        /// </summary>
        [Fact]
        public void SortDirection_ShortName_NoDuplicates()
        {
            var fullSortDirections = Enum.GetValues<SortDirection>().Cast<SortDirection>().ToList();
            var filteredSortDirections = new Dictionary<string, SortDirection?>();

            int totalCount = 0;
            foreach (SortDirection? sortDirection in fullSortDirections)
            {
                var code = sortDirection.ShortName();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredSortDirections.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredSortDirections[code] = sortDirection;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredSortDirections.Count);
        }

        /// <summary>
        /// Check that every SortDirection can be mapped from a string
        /// </summary>
        /// <param name="sortDirection">SortDirection value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateSortDirectionTestData))]
        public void SortDirection_ToSortDirection(SortDirection? sortDirection, bool expectNull)
        {
            string? longName = sortDirection.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);

            var actualNormal = longName.ToSortDirection();
            var actualSpaceless = longNameSpaceless.ToSortDirection();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(sortDirection, actualNormal);
                Assert.Equal(sortDirection, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of SortDirection values
        /// </summary>
        /// <returns>MemberData-compatible list of SortDirection values</returns>
        public static TheoryData<SortDirection?, bool> GenerateSortDirectionTestData()
        {
            var testData = new TheoryData<SortDirection?, bool>() { { null, true } };
            foreach (SortDirection? sortDirection in Enum.GetValues<SortDirection>().Cast<SortDirection?>())
            {
                testData.Add(sortDirection, false);
            }

            return testData;
        }

        #endregion

        #region System Category

        /// <summary>
        /// Check that every SystemCategory has a long name provided
        /// </summary>
        /// <param name="systemCategory">SystemCategory value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateSystemCategoryTestData))]
        public void SystemCategory_LongName(SystemCategory? systemCategory, bool expectNull)
        {
            var actual = systemCategory.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Generate a test set of SystemCategory values
        /// </summary>
        /// <returns>MemberData-compatible list of SystemCategory values</returns>
        public static TheoryData<SystemCategory?, bool> GenerateSystemCategoryTestData()
        {
            var testData = new TheoryData<SystemCategory?, bool>() { { null, true } };
            foreach (SystemCategory? systemCategory in Enum.GetValues<SystemCategory>().Cast<SystemCategory?>())
            {
                if (systemCategory == SystemCategory.NONE)
                    testData.Add(systemCategory, true);
                else
                    testData.Add(systemCategory, false);
            }

            return testData;
        }

        #endregion

        #region Yes/No

        /// <summary>
        /// Check that every YesNo has a long name provided
        /// </summary>
        /// <param name="yesNo">YesNo value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateYesNoTestData))]
        public void YesNo_LongName(YesNo? yesNo, bool expectNull)
        {
            string actual = yesNo.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        [Theory]
        [InlineData(true, YesNo.Yes)]
        [InlineData(false, YesNo.No)]
        [InlineData(null, YesNo.NULL)]
        public void YesNo_ToYesNo_Boolean(bool? value, YesNo? expected)
        {
            YesNo? actual = value.ToYesNo();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("True", YesNo.Yes)]
        [InlineData("true", YesNo.Yes)]
        [InlineData("Yes", YesNo.Yes)]
        [InlineData("yes", YesNo.Yes)]
        [InlineData("False", YesNo.No)]
        [InlineData("false", YesNo.No)]
        [InlineData("No", YesNo.No)]
        [InlineData("no", YesNo.No)]
        [InlineData("INVALID", YesNo.NULL)]
        [InlineData(null, YesNo.NULL)]
        public void YesNo_ToYesNo_String(string? value, YesNo? expected)
        {
            YesNo? actual = value.ToYesNo();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Generate a test set of YesNo values
        /// </summary>
        /// <returns>MemberData-compatible list of YesNo values</returns>
        public static TheoryData<YesNo?, bool> GenerateYesNoTestData()
        {
            var testData = new TheoryData<YesNo?, bool>() { { null, false } };
            foreach (YesNo? yesNo in Enum.GetValues<YesNo>().Cast<YesNo?>())
            {
                testData.Add(yesNo, false);
            }

            return testData;
        }

        #endregion
    }
}
