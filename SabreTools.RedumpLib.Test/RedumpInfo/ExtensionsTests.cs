using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SabreTools.RedumpLib.RedumpInfo.Data;
using Xunit;
using MediaTypeCombined = SabreTools.RedumpLib.RedumpOrg.Data.MediaType;
using MediaTypeInfo = SabreTools.RedumpLib.RedumpInfo.Data.MediaType;

namespace SabreTools.RedumpLib.Test.RedumpInfo
{
    public class ExtensionsTests
    {
        #region Cross-Enumeration

        /// <summary>
        /// redump.info media type values that map to the combined media type
        /// </summary>
        private static readonly MediaTypeInfo?[] _mappableMediaTypeInfos =
        [
            MediaTypeInfo.BD25,
            MediaTypeInfo.BD50,
            MediaTypeInfo.BD66,
            MediaTypeInfo.BD100,
            MediaTypeInfo.MaxTest4Layer,
            MediaTypeInfo.CD,
            MediaTypeInfo.DVD5,
            MediaTypeInfo.DVD9,
            MediaTypeInfo.GDROM,
            MediaTypeInfo.HDDVDSL,
            MediaTypeInfo.HDDVDDL,
            MediaTypeInfo.NintendoGameCubeGameDisc,
            MediaTypeInfo.WiiOpticalDiscSL,
            MediaTypeInfo.WiiOpticalDiscDL,
            MediaTypeInfo.WiiUOpticalDiscSL,
            MediaTypeInfo.UMDSL,
            MediaTypeInfo.UMDDL,
        ];

        /// <summary>
        /// Combined media type values that map to redump.info media type
        /// </summary>
        private static readonly MediaTypeCombined?[] _mappableMediaTypes =
        [
            MediaTypeCombined.BluRay,
            MediaTypeCombined.CDROM,
            MediaTypeCombined.DVD,
            MediaTypeCombined.GDROM,
            MediaTypeCombined.HDDVD,
            MediaTypeCombined.NintendoGameCubeGameDisc,
            MediaTypeCombined.NintendoWiiOpticalDisc,
            MediaTypeCombined.NintendoWiiUOpticalDisc,
            MediaTypeCombined.UMD,
        ];

        /// <summary>
        /// Check that both mappable and unmappable media types output correctly
        /// </summary>
        /// <param name="mediaType">MediaType value to check</param>
        /// <param name="expectNull">True to expect a null mapping, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateMediaTypeMappingTestData))]
        public void ToMediaTypeInfoTest(MediaTypeCombined? mediaType, bool expectNull)
        {
            MediaTypeInfo? actual = mediaType.ToMediaTypeInfo();
            Assert.Equal(expectNull, actual is null);
        }

        /// <summary>
        /// Check that MediaTypeInfo values all map to something appropriate
        /// </summary>
        /// <param name="discType">MediaTypeInfo value to check</param>
        /// <param name="expectNull">True to expect a null mapping, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateMediaTypeInfoMappingTestData))]
        public void ToMediaTypeTest(MediaTypeInfo? discType, bool expectNull)
        {
            MediaTypeCombined? actual = discType.ToMediaTypeCombined();
            Assert.Equal(expectNull, actual is null);
        }

        /// <summary>
        /// Generate a test set of MediaTypeInfo values
        /// </summary>
        /// <returns>MemberData-compatible list of MediaTypeInfo values</returns>
        public static TheoryData<MediaTypeInfo?, bool> GenerateMediaTypeInfoMappingTestData()
        {
            var testData = new TheoryData<MediaTypeInfo?, bool>() { { null, true } };
            foreach (MediaTypeInfo? discType in Enum.GetValues<MediaTypeInfo>().Cast<MediaTypeInfo?>())
            {
                if (_mappableMediaTypeInfos.Contains(discType))
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
        public static TheoryData<MediaTypeCombined?, bool> GenerateMediaTypeMappingTestData()
        {
            var testData = new TheoryData<MediaTypeCombined?, bool>() { { null, true } };

            foreach (MediaTypeCombined? mediaType in Enum.GetValues<MediaTypeCombined>().Cast<MediaTypeCombined?>())
            {
                if (_mappableMediaTypes.Contains(mediaType))
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

        #region Media Type

        /// <summary>
        /// Check that every MediaTypeInfo has a long name provided
        /// </summary>
        /// <param name="mediaType">MediaTypeInfo value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateMediaTypeInfoTestData))]
        public void DiscType_LongName(MediaTypeInfo? mediaType, bool expectNull)
        {
            var actual = mediaType.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every MediaTypeInfo has a short name provided
        /// </summary>
        /// <param name="mediaType">MediaTypeInfo value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateMediaTypeInfoTestData))]
        public void DiscType_ShortName(MediaTypeInfo? mediaType, bool expectNull)
        {
            var actual = mediaType.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every MediaTypeInfo can be mapped from a string
        /// </summary>
        /// <param name="mediaType">MediaTypeInfo value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateMediaTypeInfoTestData))]
        public void DiscType_ToMediaType(MediaTypeInfo? mediaType, bool expectNull)
        {
            string? longName = mediaType.LongName();
            string? longNameSpaceless = longName?
                .Replace(" ", string.Empty)
                .Replace("-", string.Empty);

            var actualNormal = longName.ToMediaType();
            var actualSpaceless = longNameSpaceless.ToMediaType();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(mediaType, actualNormal);
                Assert.Equal(mediaType, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of DiscType values
        /// </summary>
        /// <returns>MemberData-compatible list of DiscType values</returns>
        public static TheoryData<MediaTypeInfo?, bool> GenerateMediaTypeInfoTestData()
        {
            var testData = new TheoryData<MediaTypeInfo?, bool>() { { null, true } };
            foreach (MediaTypeInfo? discType in Enum.GetValues<MediaTypeInfo>().Cast<MediaTypeInfo?>())
            {
                if (discType == MediaTypeInfo.NONE)
                    testData.Add(discType, true);
                else
                    testData.Add(discType, false);
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
    }
}
