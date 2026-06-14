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
