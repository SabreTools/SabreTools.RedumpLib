using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.RedumpInfo.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.RedumpInfo
{
    public class ExtensionsTests
    {
        #region Cross-Enumeration

        /// <summary>
        /// redump.info media type values that map to the combined media type
        /// </summary>
        private static readonly MediaType?[] _mappableMediaTypes =
        [
            MediaType.BD25,
            MediaType.BD50,
            MediaType.BD66,
            MediaType.BD100,
            MediaType.MaxTest4Layer,
            MediaType.CD,
            MediaType.DVD5,
            MediaType.DVD9,
            MediaType.GDROM,
            MediaType.HDDVDSL,
            MediaType.HDDVDDL,
            MediaType.NintendoGameCubeGameDisc,
            MediaType.WiiOpticalDiscSL,
            MediaType.WiiOpticalDiscDL,
            MediaType.WiiUOpticalDiscSL,
            MediaType.UMDSL,
            MediaType.UMDDL,
        ];

        /// <summary>
        /// Combined media type values that map to redump.info media type
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
        [MemberData(nameof(GeneratePhysicalMediaTypeMappingTestData))]
        public void ToPhysicalMediaTypeTest(PhysicalMediaType? mediaType, bool expectNull)
        {
            MediaType? actual = mediaType.ToMediaType();
            Assert.Equal(expectNull, actual is null);
        }

        /// <summary>
        /// Check that MediaType values all map to something appropriate
        /// </summary>
        /// <param name="discType">MediaType value to check</param>
        /// <param name="expectNull">True to expect a null mapping, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateMediaTypeMappingTestData))]
        public void ToMediaTypeTest(MediaType? discType, bool expectNull)
        {
            PhysicalMediaType? actual = discType.ToPhysicalMediaType();
            Assert.Equal(expectNull, actual is null);
        }

        /// <summary>
        /// Generate a test set of MediaType values
        /// </summary>
        /// <returns>MemberData-compatible list of MediaType values</returns>
        public static TheoryData<MediaType?, bool> GenerateMediaTypeMappingTestData()
        {
            var testData = new TheoryData<MediaType?, bool>() { { null, true } };
            foreach (MediaType? discType in Enum.GetValues<MediaType>().Cast<MediaType?>())
            {
                if (_mappableMediaTypes.Contains(discType))
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
        public static TheoryData<PhysicalMediaType?, bool> GeneratePhysicalMediaTypeMappingTestData()
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
        /// Check that every MediaType has a long name provided
        /// </summary>
        /// <param name="mediaType">MediaType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateMediaTypeTestData))]
        public void DiscType_LongName(MediaType? mediaType, bool expectNull)
        {
            var actual = mediaType.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every MediaType has a short name provided
        /// </summary>
        /// <param name="mediaType">MediaType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateMediaTypeTestData))]
        public void DiscType_ShortName(MediaType? mediaType, bool expectNull)
        {
            var actual = mediaType.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every MediaType can be mapped from a string
        /// </summary>
        /// <param name="mediaType">MediaType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateMediaTypeTestData))]
        public void DiscType_ToMediaType(MediaType? mediaType, bool expectNull)
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
        public static TheoryData<MediaType?, bool> GenerateMediaTypeTestData()
        {
            var testData = new TheoryData<MediaType?, bool>() { { null, true } };
            foreach (MediaType? discType in Enum.GetValues<MediaType>().Cast<MediaType?>())
            {
                if (discType == MediaType.NONE)
                    testData.Add(discType, true);
                else
                    testData.Add(discType, false);
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
