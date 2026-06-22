using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SabreTools.RedumpLib.Data;
using Xunit;
using CommonDiscInfoSection = SabreTools.RedumpLib.RedumpOrg.Sections.CommonDiscInfoSection;
using SizeAndChecksumsSection = SabreTools.RedumpLib.RedumpOrg.Sections.SizeAndChecksumsSection;
using SubmissionInfo = SabreTools.RedumpLib.RedumpOrg.SubmissionInfo;
using TracksAndWriteOffsetsSection = SabreTools.RedumpLib.RedumpOrg.Sections.TracksAndWriteOffsetsSection;

namespace SabreTools.RedumpLib.Test.Data
{
    public class ExtensionsTests
    {
        #region Non-Enumerable

        #region NormalizeDiscType

        [Fact]
        public void NormalizeDiscType_InvalidMedia_Untouched()
        {
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = null }
            };

            si.NormalizeDiscType();

            Assert.Null(si.CommonDiscInfo.Media);
        }

        [Fact]
        public void NormalizeDiscType_InvalidSizeChecksums_Untouched()
        {
            MediaType expected = MediaType.CD;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = MediaType.CD },
                SizeAndChecksums = new(),
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Fact]
        public void NormalizeDiscType_UnformattedType_Fixed()
        {
            MediaType expected = MediaType.CD;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = MediaType.CD },
                SizeAndChecksums = new SizeAndChecksumsSection(),
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.DVD5)]
        [InlineData(MediaType.DVD9)]
        public void NormalizeDiscType_DVD9_Fixed(MediaType type)
        {
            MediaType expected = MediaType.DVD9;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection { Layerbreak = 12345 },
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.DVD5)]
        [InlineData(MediaType.DVD9)]
        public void NormalizeDiscType_DVD5_Fixed(MediaType type)
        {
            MediaType expected = MediaType.DVD5;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection(),
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.BD25)]
        [InlineData(MediaType.BD33)]
        [InlineData(MediaType.BD50)]
        [InlineData(MediaType.BD66)]
        [InlineData(MediaType.BD100)]
        [InlineData(MediaType.BD128)]
        public void NormalizeDiscType_BD128_Fixed(MediaType type)
        {
            MediaType expected = MediaType.BD128;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection { Layerbreak3 = 12345 },
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.BD25)]
        [InlineData(MediaType.BD33)]
        [InlineData(MediaType.BD50)]
        [InlineData(MediaType.BD66)]
        [InlineData(MediaType.BD100)]
        [InlineData(MediaType.BD128)]
        public void NormalizeDiscType_BD100_Fixed(MediaType type)
        {
            MediaType expected = MediaType.BD100;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection { Layerbreak2 = 12345 },
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.BD25)]
        [InlineData(MediaType.BD33)]
        [InlineData(MediaType.BD50)]
        [InlineData(MediaType.BD66)]
        [InlineData(MediaType.BD100)]
        [InlineData(MediaType.BD128)]
        public void NormalizeDiscType_BD66PIC_Fixed(MediaType type)
        {
            MediaType expected = MediaType.BD66;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection
                {
                    Layerbreak = 12345,
                    PICIdentifier = "BDU",
                },
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.BD25)]
        [InlineData(MediaType.BD33)]
        [InlineData(MediaType.BD50)]
        [InlineData(MediaType.BD66)]
        [InlineData(MediaType.BD100)]
        [InlineData(MediaType.BD128)]
        public void NormalizeDiscType_BD66Size_Fixed(MediaType type)
        {
            MediaType expected = MediaType.BD66;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                TracksAndWriteOffsets = new TracksAndWriteOffsetsSection
                {
                    ClrMameProData = "<rom name=\"X\" size=\"50050629633\" crc=\"X\" md5=\"X\" sha1=\"X\" />",
                },
                SizeAndChecksums = new SizeAndChecksumsSection
                {
                    Layerbreak = 12345,
                },
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.BD25)]
        [InlineData(MediaType.BD33)]
        [InlineData(MediaType.BD50)]
        [InlineData(MediaType.BD66)]
        [InlineData(MediaType.BD100)]
        [InlineData(MediaType.BD128)]
        public void NormalizeDiscType_BD50_Fixed(MediaType type)
        {
            MediaType expected = MediaType.BD50;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection { Layerbreak = 12345 },
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.BD25)]
        [InlineData(MediaType.BD33)]
        [InlineData(MediaType.BD50)]
        [InlineData(MediaType.BD66)]
        [InlineData(MediaType.BD100)]
        [InlineData(MediaType.BD128)]
        public void NormalizeDiscType_BD33PIC_Fixed(MediaType type)
        {
            MediaType expected = MediaType.BD33;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection
                {
                    PICIdentifier = "BDU",
                },
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.BD25)]
        [InlineData(MediaType.BD33)]
        [InlineData(MediaType.BD50)]
        [InlineData(MediaType.BD66)]
        [InlineData(MediaType.BD100)]
        [InlineData(MediaType.BD128)]
        public void NormalizeDiscType_BD33Size_Fixed(MediaType type)
        {
            MediaType expected = MediaType.BD33;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                TracksAndWriteOffsets = new TracksAndWriteOffsetsSection
                {
                    ClrMameProData = "<rom name=\"X\" size=\"25025314817\" crc=\"X\" md5=\"X\" sha1=\"X\" />",
                },
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.BD25)]
        [InlineData(MediaType.BD33)]
        [InlineData(MediaType.BD50)]
        [InlineData(MediaType.BD66)]
        [InlineData(MediaType.BD100)]
        [InlineData(MediaType.BD128)]
        public void NormalizeDiscType_BD25_Fixed(MediaType type)
        {
            MediaType expected = MediaType.BD25;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection(),
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.UMDSL)]
        [InlineData(MediaType.UMDDL)]
        public void NormalizeDiscType_UMDDL_Fixed(MediaType type)
        {
            MediaType expected = MediaType.UMDDL;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection { Layerbreak = 12345 },
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(MediaType.UMDSL)]
        [InlineData(MediaType.UMDDL)]
        public void NormalizeDiscType_UMDSL_Fixed(MediaType type)
        {
            MediaType expected = MediaType.UMDSL;
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection(),
            };

            si.NormalizeDiscType();

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        #endregion

        #endregion

        #region Cross-Enumeration

        /// <summary>
        /// redump.info media type values that map to the combined media type
        /// </summary>
        private static readonly MediaType?[] _mappableMediaTypes =
        [
            MediaType.BD25,
            MediaType.BD33,
            MediaType.BD50,
            MediaType.BD66,
            MediaType.BD100,
            MediaType.BD128,
            MediaType.MaxTest4Layer,
            MediaType.CD,
            MediaType.DVD5,
            MediaType.DVD9,
            MediaType.GDROM,
            MediaType.HDDVDSL,
            MediaType.HDDVDDL,
            MediaType.NintendoGameCubeGameDisc,
            MediaType.NintendoWiiOpticalDiscSL,
            MediaType.NintendoWiiOpticalDiscDL,
            MediaType.NintendoWiiUOpticalDiscSL,
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
        /// Check that every supported system has some set of MediaTypes supported
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        [Theory]
        [MemberData(nameof(GeneratePhysicalSystemMappingTestData))]
        public void MediaTypesTest(PhysicalSystem? physicalSystem)
        {
            var actual = physicalSystem.MediaTypes();
            Assert.NotEmpty(actual);
        }

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
        /// Generate a test set of PhysicalSystem values
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?> GeneratePhysicalSystemMappingTestData()
        {
            var testData = new TheoryData<PhysicalSystem?>() { null };
            foreach (PhysicalSystem? physicalSystem in Enum.GetValues<PhysicalSystem>().Cast<PhysicalSystem?>())
            {
                testData.Add(physicalSystem);
            }

            return testData;
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

        #region Language

        /// <summary>
        /// Check that every Language has a long name provided
        /// </summary>
        /// <param name="language">Language value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateLanguageTestData))]
        public void Language_LongName(Language? language, bool expectNull)
        {
            var actual = language.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every Language has a short name provided
        /// </summary>
        /// <param name="language">Language value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateLanguageTestData))]
        public void Language_ShortName(Language? language, bool expectNull)
        {
            var actual = language.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every Language can be mapped from a string
        /// </summary>
        /// <param name="lang">Language value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateLanguageTestData))]
        public void Language_ToLanguage(Language? lang, bool expectNull)
        {
            string? twoLetterCode = lang.TwoLetterCode();
            string? threeLetterCode = lang.ThreeLetterCode();
            string? threeLetterCodeAlt = lang.ThreeLetterCodeAlt();

            var actualTwoLetterCode = twoLetterCode.ToLanguage();
            var actualThreeLetterCode = threeLetterCode.ToLanguage();
            var actualThreeLetterCodeAlt = threeLetterCodeAlt.ToLanguage();

            if (expectNull)
            {
                Assert.Null(actualTwoLetterCode);
                Assert.Null(actualThreeLetterCode);
                Assert.Null(actualThreeLetterCodeAlt);
            }
            else
            {
                Assert.Equal(lang, actualThreeLetterCode);

                // Not guaranteed to exist
                if (twoLetterCode is not null)
                    Assert.Equal(lang, actualTwoLetterCode);
                if (threeLetterCodeAlt is not null)
                    Assert.Equal(lang, actualThreeLetterCodeAlt);
            }
        }

        /// <summary>
        /// Ensure that every Language that has a standard/bibliographic ISO 639-2 code is unique
        /// </summary>
        [Fact]
        public void Language_ThreeLetterCode_NoDuplicates()
        {
            var fullLanguages = Enum.GetValues<Language>().Cast<Language?>().ToList();
            var filteredLanguages = new Dictionary<string, Language?>();

            int totalCount = 0;
            foreach (Language? language in fullLanguages)
            {
                var code = language.ThreeLetterCode();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredLanguages.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredLanguages[code] = language;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredLanguages.Count);
        }

        /// <summary>
        /// Ensure that every Language that has a terminology ISO 639-2 code is unique
        /// </summary>
        [Fact]
        public void Language_ThreeLetterCodeAlt_NoDuplicates()
        {
            var fullLanguages = Enum.GetValues<Language>().Cast<Language?>().ToList();
            var filteredLanguages = new Dictionary<string, Language?>();

            int totalCount = 0;
            foreach (Language? language in fullLanguages)
            {
                var code = language.ThreeLetterCodeAlt();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredLanguages.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredLanguages[code] = language;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredLanguages.Count);
        }

        /// <summary>
        /// Ensure that every Language that has an ISO 639-1 code is unique
        /// </summary>
        [Fact]
        public void Language_TwoLetterCode_NoDuplicates()
        {
            var fullLanguages = Enum.GetValues<Language>().Cast<Language?>().ToList();
            var filteredLanguages = new Dictionary<string, Language?>();

            int totalCount = 0;
            foreach (Language? language in fullLanguages)
            {
                var code = language.TwoLetterCode();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredLanguages.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredLanguages[code] = language;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredLanguages.Count);
        }

        /// <summary>
        /// Generate a test set of Language values
        /// </summary>
        /// <returns>MemberData-compatible list of Language values</returns>
        public static TheoryData<Language?, bool> GenerateLanguageTestData()
        {
            var testData = new TheoryData<Language?, bool>() { { null, true } };
            foreach (Language? language in Enum.GetValues<Language>().Cast<Language?>())
            {
                testData.Add(language, false);
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

        #region Physical Media Type

        [Fact]
        public void PhysicalMediaType_ListMediaTypes()
        {
            var actual = Extensions.ListMediaTypes();
            Assert.NotEmpty(actual);
        }

        /// <summary>
        /// Check that every PhysicalMediaType has a long name provided
        /// </summary>
        /// <param name="mediaType">PhysicalMediaType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GeneratePhysicalMediaTypeTestData))]
        public void PhysicalMediaType_LongName(PhysicalMediaType? mediaType, bool expectNull)
        {
            var actual = mediaType.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every PhysicalMediaType has a short name provided
        /// </summary>
        /// <param name="mediaType">PhysicalMediaType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GeneratePhysicalMediaTypeTestData))]
        public void PhysicalMediaType_ShortName(PhysicalMediaType? mediaType, bool expectNull)
        {
            var actual = mediaType.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Generate a test set of PhysicalMediaType values
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalMediaType values</returns>
        public static TheoryData<PhysicalMediaType?, bool> GeneratePhysicalMediaTypeTestData()
        {
            var testData = new TheoryData<PhysicalMediaType?, bool>() { { null, true } };
            foreach (PhysicalMediaType? mediaType in Enum.GetValues<PhysicalMediaType>().Cast<PhysicalMediaType?>())
            {
                testData.Add(mediaType, false);
            }

            return testData;
        }

        #endregion

        #region Physical System

        /// <summary>
        /// PhysicalSystem values that are considered Audio
        /// </summary>
        private static readonly PhysicalSystem?[] _audioSystems =
        [
            PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem,
            PhysicalSystem.AudioCD,
            PhysicalSystem.DatelPlayStationCheatDeviceUpdates,
            PhysicalSystem.DVDAudio,
            PhysicalSystem.HasbroiONEducationalGamingSystem,
            PhysicalSystem.HasbroVideoNow,
            PhysicalSystem.HasbroVideoNowColor,
            PhysicalSystem.HasbroVideoNowJr,
            PhysicalSystem.HasbroVideoNowXP,
            PhysicalSystem.PhilipsCDi,
            PhysicalSystem.SuperAudioCD,
        ];

        /// <summary>
        /// PhysicalSystem values that are available
        /// </summary>
        private static readonly PhysicalSystem?[] _availableSystems =
        [
            // BIOS Sets
            PhysicalSystem.MicrosoftXboxBIOS,
            PhysicalSystem.NintendoGameCubeBIOS,
            PhysicalSystem.SonyPlayStationBIOS,
            PhysicalSystem.SonyPlayStation2BIOS,

            // Disc-Based Consoles
            PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem,
            PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem,
            PhysicalSystem.AppleBandaiPippin,
            PhysicalSystem.CommodoreAmigaCD32,
            PhysicalSystem.CommodoreAmigaCDTV,
            PhysicalSystem.HasbroVideoNow,
            PhysicalSystem.HasbroVideoNowColor,
            PhysicalSystem.HasbroVideoNowJr,
            PhysicalSystem.HasbroVideoNowXP,
            PhysicalSystem.MattelFisherPriceiXL,
            PhysicalSystem.MattelHyperScan,
            PhysicalSystem.MemorexVisualInformationSystem,
            PhysicalSystem.MicrosoftXbox,
            PhysicalSystem.MicrosoftXbox360,
            PhysicalSystem.MicrosoftXboxOne,
            PhysicalSystem.MicrosoftXboxSeriesXS,
            PhysicalSystem.NECPCEngineCDTurboGrafxCD,
            PhysicalSystem.NECPCFXPCFXGA,
            PhysicalSystem.NintendoGameCube,
            PhysicalSystem.NintendoWii,
            PhysicalSystem.NintendoWiiU,
            PhysicalSystem.Panasonic3DOInteractiveMultiplayer,
            PhysicalSystem.PhilipsCDi,
            PhysicalSystem.SegaDreamcast,
            PhysicalSystem.SegaMegaCDSegaCD,
            PhysicalSystem.SegaSaturn,
            PhysicalSystem.SNKNeoGeoCD,
            PhysicalSystem.SonyPlayStation,
            PhysicalSystem.SonyPlayStation2,
            PhysicalSystem.SonyPlayStation3,
            PhysicalSystem.SonyPlayStation4,
            PhysicalSystem.SonyPlayStation5,
            PhysicalSystem.SonyPlayStationPortable,
            PhysicalSystem.VMLabsNUON,
            PhysicalSystem.VTechVFlashVSmilePro,
            PhysicalSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem,

            // Computers
            PhysicalSystem.AcornArchimedesAndRiscPC,
            PhysicalSystem.AppleMacintosh,
            PhysicalSystem.CommodoreAmigaCD,
            PhysicalSystem.FujitsuFMTownsseries,
            PhysicalSystem.IBMPCcompatible,
            PhysicalSystem.NECPC88series,
            PhysicalSystem.NECPC98series,
            PhysicalSystem.SharpX68000,

            // Arcade
            PhysicalSystem.FunworldPhotoPlay,
            PhysicalSystem.IncredibleTechnologiesEagle,
            PhysicalSystem.KonamieAmusement,
            PhysicalSystem.KonamiFireBeat,
            PhysicalSystem.KonamiM2,
            PhysicalSystem.KonamiSystem573,
            PhysicalSystem.KonamiSystemGV,
            PhysicalSystem.KonamiTwinkle,
            PhysicalSystem.NamcoSegaNintendoTriforce,
            PhysicalSystem.NamcoSystem12,
            PhysicalSystem.NamcoSystem246256,
            PhysicalSystem.PanasonicM2,
            PhysicalSystem.SegaChihiro,
            PhysicalSystem.SegaLindbergh,
            PhysicalSystem.SegaNaomi,
            PhysicalSystem.SegaNaomi2,
            PhysicalSystem.SegaRingEdge,
            PhysicalSystem.SegaRingEdge2,
            PhysicalSystem.SegaTitanVideo,
            PhysicalSystem.TABAustriaQuizard,

            // Other
            PhysicalSystem.AudioCD,
            PhysicalSystem.BDVideo,
            PhysicalSystem.DatelPlayStationCheatDeviceUpdates,
            PhysicalSystem.DVDVideo,
            PhysicalSystem.EnhancedCD,
            PhysicalSystem.HDDVDVideo,
            PhysicalSystem.MicrosoftPocketPC,
            PhysicalSystem.NavisoftNaviken,
            PhysicalSystem.PalmOS,
            PhysicalSystem.PhotoCD,
            PhysicalSystem.SegaPrologue21MultimediaKaraokeSystem,
            PhysicalSystem.TaoiKTV,
            PhysicalSystem.TomyKissSite,
            PhysicalSystem.VideoCD,
        ];

        /// <summary>
        /// PhysicalSystem values that are dumper-only
        /// </summary>
        private static readonly PhysicalSystem?[] _bannedSystems =
        [
            // Disc-Based Consoles
            PhysicalSystem.HasbroVideoNow,
            PhysicalSystem.HasbroVideoNowColor,
            PhysicalSystem.HasbroVideoNowJr,
            PhysicalSystem.HasbroVideoNowXP,
            PhysicalSystem.MicrosoftXboxOne,
            PhysicalSystem.MicrosoftXboxSeriesXS,
            PhysicalSystem.NintendoWiiU,
            PhysicalSystem.SonyPlayStation4,
            PhysicalSystem.SonyPlayStation5,

            // Arcade
            PhysicalSystem.KonamiM2,
            PhysicalSystem.PanasonicM2,

            // Other
            PhysicalSystem.AudioCD,
            PhysicalSystem.BDVideo,
            PhysicalSystem.DVDVideo,
            PhysicalSystem.EnhancedCD,
            PhysicalSystem.HDDVDVideo,
            PhysicalSystem.NavisoftNaviken,
            PhysicalSystem.VideoCD,
        ];

        /// <summary>
        /// PhysicalSystem values that are considered markers
        /// </summary>
        private static readonly PhysicalSystem?[] _markerSystems =
        [
            PhysicalSystem.MarkerArcadeEnd,
            PhysicalSystem.MarkerComputerEnd,
            PhysicalSystem.MarkerDiscBasedConsoleEnd,
            PhysicalSystem.MarkerOtherEnd,
        ];

        /// <summary>
        /// PhysicalSystem values that are have reversed ringcodes
        /// </summary>
        private static readonly PhysicalSystem?[] _reverseRingcodeSystems =
        [
            PhysicalSystem.SonyPlayStation2,
            PhysicalSystem.SonyPlayStation3,
            PhysicalSystem.SonyPlayStation4,
            PhysicalSystem.SonyPlayStation5,
            PhysicalSystem.SonyPlayStationPortable,
        ];

        /// <summary>
        /// Map of PhysicalSystem values to their corresponding categories
        /// </summary>
        private static readonly Dictionary<PhysicalSystem, SystemCategory> _systemCategoryMap = new()
        {
            // BIOS
            [PhysicalSystem.MicrosoftXboxBIOS] = SystemCategory.NONE,
            [PhysicalSystem.NintendoGameCubeBIOS] = SystemCategory.NONE,
            [PhysicalSystem.SonyPlayStationBIOS] = SystemCategory.NONE,
            [PhysicalSystem.SonyPlayStation2BIOS] = SystemCategory.NONE,

            // Disc-Based Consoles
            [PhysicalSystem.AppleBandaiPippin] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.CommodoreAmigaCD32] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.CommodoreAmigaCDTV] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.EnvizionsEVOSmartConsole] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.FujitsuFMTownsMarty] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.HasbroiONEducationalGamingSystem] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.HasbroVideoNow] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.HasbroVideoNowColor] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.HasbroVideoNowJr] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.HasbroVideoNowXP] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.MattelFisherPriceiXL] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.MattelHyperScan] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.MemorexVisualInformationSystem] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.MicrosoftXbox] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.MicrosoftXbox360] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.MicrosoftXboxOne] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.MicrosoftXboxSeriesXS] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.NECPCEngineCDTurboGrafxCD] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.NECPCFXPCFXGA] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.NintendoGameCube] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.NintendoSonySuperNESCDROMSystem] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.NintendoWii] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.NintendoWiiU] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.Panasonic3DOInteractiveMultiplayer] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.PhilipsCDi] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.PlaymajiPolymega] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.PioneerLaserActive] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.SegaDreamcast] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.SegaMegaCDSegaCD] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.SegaSaturn] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.SNKNeoGeoCD] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.SonyPlayStation] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.SonyPlayStation2] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.SonyPlayStation3] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.SonyPlayStation4] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.SonyPlayStation5] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.SonyPlayStationPortable] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.VMLabsNUON] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.VTechVFlashVSmilePro] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.MarkerDiscBasedConsoleEnd] = SystemCategory.NONE,

            // Computers
            [PhysicalSystem.AcornArchimedesAndRiscPC] = SystemCategory.Computer,
            [PhysicalSystem.AppleMacintosh] = SystemCategory.Computer,
            [PhysicalSystem.CommodoreAmigaCD] = SystemCategory.Computer,
            [PhysicalSystem.FujitsuFMTownsseries] = SystemCategory.Computer,
            [PhysicalSystem.IBMPCcompatible] = SystemCategory.Computer,
            [PhysicalSystem.NECPC88series] = SystemCategory.Computer,
            [PhysicalSystem.NECPC98series] = SystemCategory.Computer,
            [PhysicalSystem.SharpX68000] = SystemCategory.Computer,
            [PhysicalSystem.MarkerComputerEnd] = SystemCategory.NONE,

            // Arcade
            [PhysicalSystem.AmigaCUBOCD32] = SystemCategory.Arcade,
            [PhysicalSystem.AmericanLaserGames3DO] = SystemCategory.Arcade,
            [PhysicalSystem.Atari3DO] = SystemCategory.Arcade,
            [PhysicalSystem.Atronic] = SystemCategory.Arcade,
            [PhysicalSystem.AUSCOMSystem1] = SystemCategory.Arcade,
            [PhysicalSystem.BallyGameMagic] = SystemCategory.Arcade,
            [PhysicalSystem.CapcomCPSystemIII] = SystemCategory.Arcade,
            [PhysicalSystem.FunworldPhotoPlay] = SystemCategory.Arcade,
            [PhysicalSystem.FuRyuOmronPurikura] = SystemCategory.Arcade,
            [PhysicalSystem.GlobalVRVarious] = SystemCategory.Arcade,
            [PhysicalSystem.GlobalVRVortek] = SystemCategory.Arcade,
            [PhysicalSystem.GlobalVRVortekV3] = SystemCategory.Arcade,
            [PhysicalSystem.ICEPCHardware] = SystemCategory.Arcade,
            [PhysicalSystem.IncredibleTechnologiesEagle] = SystemCategory.Arcade,
            [PhysicalSystem.IncredibleTechnologiesVarious] = SystemCategory.Arcade,
            [PhysicalSystem.JVLiTouch] = SystemCategory.Arcade,
            [PhysicalSystem.KonamieAmusement] = SystemCategory.Arcade,
            [PhysicalSystem.KonamiFireBeat] = SystemCategory.Arcade,
            [PhysicalSystem.KonamiM2] = SystemCategory.Arcade,
            [PhysicalSystem.KonamiPython] = SystemCategory.Arcade,
            [PhysicalSystem.KonamiPython2] = SystemCategory.Arcade,
            [PhysicalSystem.KonamiSystem573] = SystemCategory.Arcade,
            [PhysicalSystem.KonamiSystemGV] = SystemCategory.Arcade,
            [PhysicalSystem.KonamiTwinkle] = SystemCategory.Arcade,
            [PhysicalSystem.KonamiVarious] = SystemCategory.Arcade,
            [PhysicalSystem.MeritIndustriesBoardwalk] = SystemCategory.Arcade,
            [PhysicalSystem.MeritIndustriesMegaTouchForce] = SystemCategory.Arcade,
            [PhysicalSystem.MeritIndustriesMegaTouchION] = SystemCategory.Arcade,
            [PhysicalSystem.MeritIndustriesMegaTouchMaxx] = SystemCategory.Arcade,
            [PhysicalSystem.MeritIndustriesMegaTouchXL] = SystemCategory.Arcade,
            [PhysicalSystem.NamcoPurikura] = SystemCategory.Arcade,
            [PhysicalSystem.NamcoSegaNintendoTriforce] = SystemCategory.Arcade,
            [PhysicalSystem.NamcoSystem12] = SystemCategory.Arcade,
            [PhysicalSystem.NamcoSystem246256] = SystemCategory.Arcade,
            [PhysicalSystem.NewJatreCDi] = SystemCategory.Arcade,
            [PhysicalSystem.NichibutsuHighRateSystem] = SystemCategory.Arcade,
            [PhysicalSystem.NichibutsuSuperCD] = SystemCategory.Arcade,
            [PhysicalSystem.NichibutsuXRateSystem] = SystemCategory.Arcade,
            [PhysicalSystem.PanasonicM2] = SystemCategory.Arcade,
            [PhysicalSystem.PhotoPlayVarious] = SystemCategory.Arcade,
            [PhysicalSystem.RawThrillsVarious] = SystemCategory.Arcade,
            [PhysicalSystem.SegaALLS] = SystemCategory.Arcade,
            [PhysicalSystem.SegaChihiro] = SystemCategory.Arcade,
            [PhysicalSystem.SegaEuropaR] = SystemCategory.Arcade,
            [PhysicalSystem.SegaLindbergh] = SystemCategory.Arcade,
            [PhysicalSystem.SegaNaomi] = SystemCategory.Arcade,
            [PhysicalSystem.SegaNaomi2] = SystemCategory.Arcade,
            [PhysicalSystem.SegaNaomiSatelliteTerminalPC] = SystemCategory.Arcade,
            [PhysicalSystem.SegaNu] = SystemCategory.Arcade,
            [PhysicalSystem.SegaNu11] = SystemCategory.Arcade,
            [PhysicalSystem.SegaNu2] = SystemCategory.Arcade,
            [PhysicalSystem.SegaNuSX] = SystemCategory.Arcade,
            [PhysicalSystem.SegaRingEdge] = SystemCategory.Arcade,
            [PhysicalSystem.SegaRingEdge2] = SystemCategory.Arcade,
            [PhysicalSystem.SegaRingWide] = SystemCategory.Arcade,
            [PhysicalSystem.SegaSystem32] = SystemCategory.Arcade,
            [PhysicalSystem.SegaTitanVideo] = SystemCategory.Arcade,
            [PhysicalSystem.SeibuCATSSystem] = SystemCategory.Arcade,
            [PhysicalSystem.TABAustriaQuizard] = SystemCategory.Arcade,
            [PhysicalSystem.TsunamiTsuMoMultiGameMotionSystem] = SystemCategory.Arcade,
            [PhysicalSystem.UltraCade] = SystemCategory.Arcade,
            [PhysicalSystem.MarkerArcadeEnd] = SystemCategory.NONE,

            // Other
            [PhysicalSystem.AudioCD] = SystemCategory.Other,
            [PhysicalSystem.BDVideo] = SystemCategory.Other,
            [PhysicalSystem.DatelPlayStationCheatDeviceUpdates] = SystemCategory.Other,
            [PhysicalSystem.DVDAudio] = SystemCategory.Other,
            [PhysicalSystem.DVDVideo] = SystemCategory.Other,
            [PhysicalSystem.EnhancedCD] = SystemCategory.Other,
            [PhysicalSystem.HDDVDVideo] = SystemCategory.Other,
            [PhysicalSystem.MicrosoftPocketPC] = SystemCategory.Other,
            [PhysicalSystem.NavisoftNaviken] = SystemCategory.Other,
            [PhysicalSystem.PalmOS] = SystemCategory.Other,
            [PhysicalSystem.PhotoCD] = SystemCategory.Other,
            [PhysicalSystem.Psion] = SystemCategory.Other,
            [PhysicalSystem.RainbowDisc] = SystemCategory.Other,
            [PhysicalSystem.SegaPrologue21MultimediaKaraokeSystem] = SystemCategory.Other,
            [PhysicalSystem.SharpZaurus] = SystemCategory.Other,
            [PhysicalSystem.SonyElectronicBook] = SystemCategory.Other,
            [PhysicalSystem.SuperAudioCD] = SystemCategory.Other,
            [PhysicalSystem.TaoiKTV] = SystemCategory.Other,
            [PhysicalSystem.TomyKissSite] = SystemCategory.Other,
            [PhysicalSystem.VideoCD] = SystemCategory.Other,
            [PhysicalSystem.MarkerOtherEnd] = SystemCategory.NONE,
        };

        /// <summary>
        /// PhysicalSystem values that have cuesheet packs
        /// </summary>
        private static readonly PhysicalSystem?[] _systemsWithCues =
        [
            // Disc-Based Consoles
            PhysicalSystem.AppleBandaiPippin,
            PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem,
            PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem,
            PhysicalSystem.CommodoreAmigaCD32,
            PhysicalSystem.CommodoreAmigaCDTV,
            PhysicalSystem.HasbroVideoNow,
            PhysicalSystem.HasbroVideoNowColor,
            PhysicalSystem.HasbroVideoNowJr,
            PhysicalSystem.HasbroVideoNowXP,
            PhysicalSystem.MattelFisherPriceiXL,
            PhysicalSystem.MattelHyperScan,
            PhysicalSystem.MemorexVisualInformationSystem,
            PhysicalSystem.MicrosoftXbox,
            PhysicalSystem.MicrosoftXbox360,
            PhysicalSystem.NECPCEngineCDTurboGrafxCD,
            PhysicalSystem.NECPCFXPCFXGA,
            PhysicalSystem.Panasonic3DOInteractiveMultiplayer,
            PhysicalSystem.PhilipsCDi,
            PhysicalSystem.SegaDreamcast,
            PhysicalSystem.SegaMegaCDSegaCD,
            PhysicalSystem.SegaSaturn,
            PhysicalSystem.SNKNeoGeoCD,
            PhysicalSystem.SonyPlayStation,
            PhysicalSystem.SonyPlayStation2,
            PhysicalSystem.SonyPlayStation3,
            PhysicalSystem.VTechVFlashVSmilePro,

            // Computers
            PhysicalSystem.AcornArchimedesAndRiscPC,
            PhysicalSystem.AppleMacintosh,
            PhysicalSystem.CommodoreAmigaCD,
            PhysicalSystem.FujitsuFMTownsseries,
            PhysicalSystem.IBMPCcompatible,
            PhysicalSystem.NECPC88series,
            PhysicalSystem.NECPC98series,
            PhysicalSystem.SharpX68000,

            // Arcade
            PhysicalSystem.FunworldPhotoPlay,
            PhysicalSystem.IncredibleTechnologiesEagle,
            PhysicalSystem.KonamieAmusement,
            PhysicalSystem.KonamiFireBeat,
            PhysicalSystem.KonamiM2,
            PhysicalSystem.KonamiSystem573,
            PhysicalSystem.KonamiSystemGV,
            PhysicalSystem.NamcoSegaNintendoTriforce,
            PhysicalSystem.NamcoSystem246256,
            PhysicalSystem.PanasonicM2,
            PhysicalSystem.SegaChihiro,
            PhysicalSystem.SegaNaomi,
            PhysicalSystem.SegaNaomi2,
            PhysicalSystem.TABAustriaQuizard,

            // Other
            PhysicalSystem.AudioCD,
            PhysicalSystem.DatelPlayStationCheatDeviceUpdates,
            PhysicalSystem.MicrosoftPocketPC,
            PhysicalSystem.NavisoftNaviken,
            PhysicalSystem.PalmOS,
            PhysicalSystem.PhotoCD,
            PhysicalSystem.SegaPrologue21MultimediaKaraokeSystem,
            PhysicalSystem.TomyKissSite,
            PhysicalSystem.VideoCD,
        ];

        /// <summary>
        /// PhysicalSystem values that have dats
        /// </summary>
        private static readonly PhysicalSystem?[] _systemsWithDats =
        [
            // BIOS Sets
            PhysicalSystem.MicrosoftXboxBIOS,
            PhysicalSystem.NintendoGameCubeBIOS,
            PhysicalSystem.SonyPlayStationBIOS,
            PhysicalSystem.SonyPlayStation2BIOS,

            // Disc-Based Consoles
            PhysicalSystem.AppleBandaiPippin,
            PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem,
            PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem,
            PhysicalSystem.CommodoreAmigaCD32,
            PhysicalSystem.CommodoreAmigaCDTV,
            PhysicalSystem.HasbroVideoNow,
            PhysicalSystem.HasbroVideoNowColor,
            PhysicalSystem.HasbroVideoNowJr,
            PhysicalSystem.HasbroVideoNowXP,
            PhysicalSystem.MattelFisherPriceiXL,
            PhysicalSystem.MattelHyperScan,
            PhysicalSystem.MemorexVisualInformationSystem,
            PhysicalSystem.MicrosoftXbox,
            PhysicalSystem.MicrosoftXbox360,
            PhysicalSystem.MicrosoftXboxOne,
            PhysicalSystem.MicrosoftXboxSeriesXS,
            PhysicalSystem.NECPCEngineCDTurboGrafxCD,
            PhysicalSystem.NECPCFXPCFXGA,
            PhysicalSystem.NintendoGameCube,
            PhysicalSystem.NintendoWii,
            PhysicalSystem.NintendoWiiU,
            PhysicalSystem.Panasonic3DOInteractiveMultiplayer,
            PhysicalSystem.PhilipsCDi,
            PhysicalSystem.SegaDreamcast,
            PhysicalSystem.SegaMegaCDSegaCD,
            PhysicalSystem.SegaSaturn,
            PhysicalSystem.SNKNeoGeoCD,
            PhysicalSystem.SonyPlayStation,
            PhysicalSystem.SonyPlayStation2,
            PhysicalSystem.SonyPlayStation3,
            PhysicalSystem.SonyPlayStation4,
            PhysicalSystem.SonyPlayStation5,
            PhysicalSystem.SonyPlayStationPortable,
            PhysicalSystem.VMLabsNUON,
            PhysicalSystem.VTechVFlashVSmilePro,
            PhysicalSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem,

            // Computers
            PhysicalSystem.AcornArchimedesAndRiscPC,
            PhysicalSystem.AppleMacintosh,
            PhysicalSystem.CommodoreAmigaCD,
            PhysicalSystem.FujitsuFMTownsseries,
            PhysicalSystem.IBMPCcompatible,
            PhysicalSystem.NECPC88series,
            PhysicalSystem.NECPC98series,
            PhysicalSystem.SharpX68000,

            // Arcade
            PhysicalSystem.FunworldPhotoPlay,
            PhysicalSystem.IncredibleTechnologiesEagle,
            PhysicalSystem.KonamieAmusement,
            PhysicalSystem.KonamiFireBeat,
            PhysicalSystem.KonamiM2,
            PhysicalSystem.KonamiSystem573,
            PhysicalSystem.KonamiSystemGV,
            PhysicalSystem.NamcoSegaNintendoTriforce,
            PhysicalSystem.NamcoSystem246256,
            PhysicalSystem.PanasonicM2,
            PhysicalSystem.SegaChihiro,
            PhysicalSystem.SegaLindbergh,
            PhysicalSystem.SegaNaomi,
            PhysicalSystem.SegaNaomi2,
            PhysicalSystem.SegaRingEdge,
            PhysicalSystem.SegaRingEdge2,
            PhysicalSystem.TABAustriaQuizard,

            // Other
            PhysicalSystem.AudioCD,
            PhysicalSystem.BDVideo,
            PhysicalSystem.DatelPlayStationCheatDeviceUpdates,
            PhysicalSystem.DVDVideo,
            PhysicalSystem.HDDVDVideo,
            PhysicalSystem.MicrosoftPocketPC,
            PhysicalSystem.NavisoftNaviken,
            PhysicalSystem.PalmOS,
            PhysicalSystem.PhotoCD,
            PhysicalSystem.SegaPrologue21MultimediaKaraokeSystem,
            PhysicalSystem.TomyKissSite,
            PhysicalSystem.VideoCD,
        ];

        /// <summary>
        /// PhysicalSystem values that have decrypted keys
        /// </summary>
        private static readonly PhysicalSystem?[] _systemsWithDKeys =
        [
            PhysicalSystem.SonyPlayStation3,
        ];

        /// <summary>
        /// PhysicalSystem values that have GDI packs
        /// </summary>
        private static readonly PhysicalSystem?[] _systemsWithGdis =
        [
            // Disc-Based Consoles
            PhysicalSystem.SegaDreamcast,

            // Arcade
            PhysicalSystem.NamcoSegaNintendoTriforce,
            PhysicalSystem.SegaChihiro,
            PhysicalSystem.SegaNaomi,
            PhysicalSystem.SegaNaomi2,
        ];

        /// <summary>
        /// PhysicalSystem values that have keys
        /// </summary>
        private static readonly PhysicalSystem?[] _systemsWithKeys =
        [
            PhysicalSystem.NintendoWiiU,
            PhysicalSystem.SonyPlayStation3,
        ];

        /// <summary>
        /// PhysicalSystem values that have LSD packs
        /// </summary>
        private static readonly PhysicalSystem?[] _systemsWithLsds =
        [
            // Disc-Based Consoles
            PhysicalSystem.SonyPlayStation,

            // Computers
            PhysicalSystem.AppleMacintosh,
            PhysicalSystem.IBMPCcompatible,
        ];

        /// <summary>
        /// PhysicalSystem values that have SBI packs
        /// </summary>
        private static readonly PhysicalSystem?[] _systemsWithSbis =
        [
            // Disc-Based Consoles
            PhysicalSystem.SonyPlayStation,

            // Computers
            PhysicalSystem.AppleMacintosh,
            PhysicalSystem.IBMPCcompatible,
        ];

        /// <summary>
        /// PhysicalSystem values that are considered detected by Windows
        /// </summary>
        private static readonly PhysicalSystem?[] _windowsDetectedSystems =
        [
            // Disc-Based Consoles
            PhysicalSystem.CommodoreAmigaCD32,
            PhysicalSystem.CommodoreAmigaCDTV,
            PhysicalSystem.EnvizionsEVOSmartConsole,
            PhysicalSystem.FujitsuFMTownsMarty,
            PhysicalSystem.HasbroiONEducationalGamingSystem,
            PhysicalSystem.MattelFisherPriceiXL,
            PhysicalSystem.MattelHyperScan,
            PhysicalSystem.MemorexVisualInformationSystem,
            PhysicalSystem.MicrosoftXbox,
            PhysicalSystem.MicrosoftXbox360,
            PhysicalSystem.MicrosoftXboxOne,
            PhysicalSystem.MicrosoftXboxSeriesXS,
            PhysicalSystem.NECPCEngineCDTurboGrafxCD,
            PhysicalSystem.NECPCFXPCFXGA,
            PhysicalSystem.NintendoSonySuperNESCDROMSystem,
            PhysicalSystem.PlaymajiPolymega,
            PhysicalSystem.SegaDreamcast,
            PhysicalSystem.SegaMegaCDSegaCD,
            PhysicalSystem.SegaSaturn,
            PhysicalSystem.SNKNeoGeoCD,
            PhysicalSystem.SonyPlayStation,
            PhysicalSystem.SonyPlayStation2,
            PhysicalSystem.SonyPlayStation3,
            PhysicalSystem.SonyPlayStation4,
            PhysicalSystem.SonyPlayStation5,
            PhysicalSystem.SonyPlayStationPortable,
            PhysicalSystem.VMLabsNUON,
            PhysicalSystem.VTechVFlashVSmilePro,
            PhysicalSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem,

            // Computers
            PhysicalSystem.AcornArchimedesAndRiscPC,
            PhysicalSystem.CommodoreAmigaCD,
            PhysicalSystem.FujitsuFMTownsseries,
            PhysicalSystem.IBMPCcompatible,
            PhysicalSystem.NECPC88series,
            PhysicalSystem.NECPC98series,
            PhysicalSystem.SharpX68000,

            // Arcade
            PhysicalSystem.AmigaCUBOCD32,
            PhysicalSystem.Atronic,
            PhysicalSystem.AUSCOMSystem1,
            PhysicalSystem.BallyGameMagic,
            PhysicalSystem.CapcomCPSystemIII,
            PhysicalSystem.FunworldPhotoPlay,
            PhysicalSystem.FuRyuOmronPurikura,
            PhysicalSystem.GlobalVRVarious,
            PhysicalSystem.GlobalVRVortek,
            PhysicalSystem.GlobalVRVortekV3,
            PhysicalSystem.ICEPCHardware,
            PhysicalSystem.IncredibleTechnologiesEagle,
            PhysicalSystem.IncredibleTechnologiesVarious,
            PhysicalSystem.JVLiTouch,
            PhysicalSystem.KonamieAmusement,
            PhysicalSystem.KonamiFireBeat,
            PhysicalSystem.KonamiM2,
            PhysicalSystem.KonamiPython,
            PhysicalSystem.KonamiPython2,
            PhysicalSystem.KonamiSystem573,
            PhysicalSystem.KonamiSystemGV,
            PhysicalSystem.KonamiTwinkle,
            PhysicalSystem.KonamiVarious,
            PhysicalSystem.MeritIndustriesBoardwalk,
            PhysicalSystem.MeritIndustriesMegaTouchForce,
            PhysicalSystem.MeritIndustriesMegaTouchION,
            PhysicalSystem.MeritIndustriesMegaTouchMaxx,
            PhysicalSystem.MeritIndustriesMegaTouchXL,
            PhysicalSystem.NamcoPurikura,
            PhysicalSystem.NamcoSegaNintendoTriforce,
            PhysicalSystem.NamcoSystem12,
            PhysicalSystem.NamcoSystem246256,
            PhysicalSystem.NichibutsuHighRateSystem,
            PhysicalSystem.NichibutsuSuperCD,
            PhysicalSystem.NichibutsuXRateSystem,
            PhysicalSystem.PhotoPlayVarious,
            PhysicalSystem.RawThrillsVarious,
            PhysicalSystem.SegaALLS,
            PhysicalSystem.SegaChihiro,
            PhysicalSystem.SegaEuropaR,
            PhysicalSystem.SegaLindbergh,
            PhysicalSystem.SegaNaomi,
            PhysicalSystem.SegaNaomi2,
            PhysicalSystem.SegaNaomiSatelliteTerminalPC,
            PhysicalSystem.SegaNu,
            PhysicalSystem.SegaNu11,
            PhysicalSystem.SegaNu2,
            PhysicalSystem.SegaNuSX,
            PhysicalSystem.SegaRingEdge,
            PhysicalSystem.SegaRingEdge2,
            PhysicalSystem.SegaRingWide,
            PhysicalSystem.SegaSystem32,
            PhysicalSystem.SegaTitanVideo,
            PhysicalSystem.SeibuCATSSystem,
            PhysicalSystem.TABAustriaQuizard,
            PhysicalSystem.TsunamiTsuMoMultiGameMotionSystem,
            PhysicalSystem.UltraCade,

            // Other
            PhysicalSystem.AudioCD,
            PhysicalSystem.BDVideo,
            PhysicalSystem.DVDAudio,
            PhysicalSystem.DVDVideo,
            PhysicalSystem.EnhancedCD,
            PhysicalSystem.HDDVDVideo,
            PhysicalSystem.MicrosoftPocketPC,
            PhysicalSystem.NavisoftNaviken,
            PhysicalSystem.PalmOS,
            PhysicalSystem.PhotoCD,
            PhysicalSystem.Psion,
            PhysicalSystem.RainbowDisc,
            PhysicalSystem.SharpZaurus,
            PhysicalSystem.SegaPrologue21MultimediaKaraokeSystem,
            PhysicalSystem.SonyElectronicBook,
            PhysicalSystem.TaoiKTV,
            PhysicalSystem.TomyKissSite,
            PhysicalSystem.VideoCD,
        ];

        /// <summary>
        /// PhysicalSystem values that are considered XGD
        /// </summary>
        private static readonly PhysicalSystem?[] _xgdSystems =
        [
            PhysicalSystem.MicrosoftXbox,
            PhysicalSystem.MicrosoftXbox360,
            PhysicalSystem.MicrosoftXboxOne,
            PhysicalSystem.MicrosoftXboxSeriesXS,
        ];

        /// <summary>
        /// Check that all systems detected by Windows are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateWindowsDetectedSystemsTestData))]
        public void PhysicalSystem_DetectedByWindows(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.DetectedByWindows();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with reversed ringcodes are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateReversedRingcodeSystemsTestData))]
        public void PhysicalSystem_HasReversedRingcodes(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.HasReversedRingcodes();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all audio systems are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateAudioSystemsTestData))]
        public void PhysicalSystem_IsAudio(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.IsAudio();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all marker systems are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateMarkerSystemsTestData))]
        public void PhysicalSystem_IsMarker(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.IsMarker();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all XGD systems are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateXGDSystemsTestData))]
        public void PhysicalSystem_IsXGD(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.IsXGD();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PhysicalSystem_ListPhysicalSystem()
        {
            var actual = Extensions.ListSystems();
            Assert.NotEmpty(actual);
        }

        /// <summary>
        /// Check that every PhysicalSystem has a long name provided
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GeneratePhysicalSystemTestData))]
        public void PhysicalSystem_LongName(PhysicalSystem? physicalSystem, bool expectNull)
        {
            var actual = physicalSystem.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        // TODO: Re-enable the following test once non-Redump systems are accounted for

        /// <summary>
        /// Check that every PhysicalSystem has a short name provided
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        // [Theory]
        // [MemberData(nameof(GeneratePhysicalSystemTestData))]
        // public void PhysicalSystem_ShortName(PhysicalSystem? physicalSystem, bool expectNull)
        // {
        //    string? actual = physicalSystem.ShortName();

        //    if (expectNull)
        //        Assert.Null(actual);
        //    else
        //        Assert.NotNull(actual);
        // }

        /// <summary>
        /// Check that every PhysicalSystem has a short name alt provided
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        // [Theory]
        // [MemberData(nameof(GeneratePhysicalSystemTestData))]
        // public void PhysicalSystem_ShortNameAlt(PhysicalSystem? physicalSystem, bool expectNull)
        // {
        //    string? actual = physicalSystem.ShortNameAlt();

        //    if (expectNull)
        //        Assert.Null(actual);
        //    else
        //        Assert.NotNull(actual);
        // }

        /// <summary>
        /// Check that all systems are mapped to a category
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateCategoriesSystemTestData))]
        public void PhysicalSystem_GetCategory(PhysicalSystem? physicalSystem, SystemCategory expected)
        {
            SystemCategory actual = physicalSystem.GetCategory();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all available systems are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateAvailableSystemsTestData))]
        public void PhysicalSystem_IsAvailable(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.IsAvailable();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all banned systems are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateBannedSystemsTestData))]
        public void PhysicalSystem_IsBanned(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.IsBanned();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with cuesheets are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateCuesheetSystemsTestData))]
        public void PhysicalSystem_HasCues(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.HasCues();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with DATs are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateDatSystemsTestData))]
        public void PhysicalSystem_HasDat(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.HasDat();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with decrypted keys are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateDKeySystemsTestData))]
        public void PhysicalSystem_HasDkeys(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.HasDkeys();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with GDIs are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateGdiSystemsTestData))]
        public void PhysicalSystem_HasGdi(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.HasGdi();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with keys are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateKeySystemsTestData))]
        public void PhysicalSystem_HasKeys(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.HasKeys();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with LSDs are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateLsdSystemsTestData))]
        public void PhysicalSystem_HasLsd(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.HasLsd();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with SBIs are marked properly
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateSbiSystemsTestData))]
        public void PhysicalSystem_HasSbi(PhysicalSystem? physicalSystem, bool expected)
        {
            bool actual = physicalSystem.HasSbi();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that every PhysicalSystem can be mapped from a string
        /// </summary>
        /// <param name="physicalSystem">PhysicalSystem value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GeneratePhysicalSystemTestData))]
        public void PhysicalSystem_ToPhysicalSystem(PhysicalSystem? physicalSystem, bool expectNull)
        {
            string? longName = physicalSystem.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);

            var actualNormal = longName.ToPhysicalSystem();
            var actualSpaceless = longNameSpaceless.ToPhysicalSystem();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(physicalSystem, actualNormal);
                Assert.Equal(physicalSystem, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GeneratePhysicalSystemTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, true } };
            foreach (PhysicalSystem? physicalSystem in Enum.GetValues<PhysicalSystem>().Cast<PhysicalSystem?>())
            {
                // We want to skip all markers for this
                if (physicalSystem.IsMarker())
                    continue;

                testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that are considered Audio
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateAudioSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_audioSystems.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that are available
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateAvailableSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_availableSystems.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that are banned
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateBannedSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_bannedSystems.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values mapped to their categories
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, SystemCategory> GenerateCategoriesSystemTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, SystemCategory>() { { null, SystemCategory.NONE } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                testData.Add(physicalSystem, _systemCategoryMap[physicalSystem]);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that have cuesheets
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateCuesheetSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_systemsWithCues.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that have DATs
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateDatSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_systemsWithDats.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that have decrypted keys
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateDKeySystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_systemsWithDKeys.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that have GDIs
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateGdiSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_systemsWithGdis.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that have keys
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateKeySystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_systemsWithKeys.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that have LSDs
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateLsdSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_systemsWithLsds.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that are considered markers
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateMarkerSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_markerSystems.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that are considered markers
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateReversedRingcodeSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_reverseRingcodeSystems.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that have SBIs
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateSbiSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_systemsWithSbis.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that are detected by Windows
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateWindowsDetectedSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_windowsDetectedSystems.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of PhysicalSystem values that are considered XGD
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalSystem values</returns>
        public static TheoryData<PhysicalSystem?, bool> GenerateXGDSystemsTestData()
        {
            var testData = new TheoryData<PhysicalSystem?, bool>() { { null, false } };
            foreach (PhysicalSystem physicalSystem in Enum.GetValues<PhysicalSystem>())
            {
                if (_xgdSystems.Contains(physicalSystem))
                    testData.Add(physicalSystem, true);
                else
                    testData.Add(physicalSystem, false);
            }

            return testData;
        }

        #endregion

        #region Region

        /// <summary>
        /// Region values that don't have redump.info shortnames
        /// </summary>
        private static readonly Region?[] _regionsWithoutInfoShortname =
        [
            Region.AsiaEurope,
            Region.AsiaUSA,
            Region.AustraliaGermany,
            Region.AustraliaNewZealand,
            Region.AustriaSwitzerland,
            Region.BelgiumNetherlands,
            Region.EuropeAsia,
            Region.EuropeAustralia,
            Region.EuropeCanada,
            Region.EuropeGermany,
            Region.FranceSpain,
            Region.GreaterChina,
            Region.JapanAsia,
            Region.JapanEurope,
            Region.JapanKorea,
            Region.JapanUSA,
            Region.SpainPortugal,
            Region.UKAustralia,
            Region.USAAsia,
            Region.USAAustralia,
            Region.USABrazil,
            Region.USACanada,
            Region.USAEurope,
            Region.USAGermany,
            Region.USAJapan,
            Region.USAKorea,
        ];

        /// <summary>
        /// Check that every Region has a long name provided
        /// </summary>
        /// <param name="region">Region value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateRegionTestData))]
        public void Region_LongName(Region? region, bool expectNull)
        {
            var actual = region.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every Region has a short name provided
        /// </summary>
        /// <param name="region">Region value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateRegionTestData))]
        public void Region_ShortName(Region? region, bool expectNull)
        {
            // HACK: Hardcoded list of aggregate regions to ignore
            if (_regionsWithoutInfoShortname.Contains(region))
                expectNull = true;

            var actual = region.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Ensure that every Region that has a short name that is unique
        /// </summary>
        [Fact]
        public void Region_ShortName_NoDuplicates()
        {
            var fullRegions = Enum.GetValues<Region>().Cast<Region?>().ToList();
            var filteredRegions = new Dictionary<string, Region?>();

            int totalCount = 0;
            foreach (Region? region in fullRegions)
            {
                var code = region.ShortName();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredRegions.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredRegions[code] = region;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredRegions.Count);
        }

        /// <summary>
        /// Check that every Region has a short name provided
        /// </summary>
        /// <param name="region">Region value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateRegionTestData))]
        public void Region_ShortNameAlt(Region? region, bool expectNull)
        {
            var actual = region.ShortNameAlt();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Ensure that every Region that has a short name that is unique
        /// </summary>
        [Fact]
        public void Region_ShortNameAlt_NoDuplicates()
        {
            var fullRegions = Enum.GetValues<Region>().Cast<Region?>().ToList();
            var filteredRegions = new Dictionary<string, Region?>();

            int totalCount = 0;
            foreach (Region? region in fullRegions)
            {
                var code = region.ShortNameAlt();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredRegions.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredRegions[code] = region;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredRegions.Count);
        }

        /// <summary>
        /// Check that every Region can be mapped from a string
        /// </summary>
        /// <param name="region">Region value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateRegionTestData))]
        public void Region_ToRegion(Region? region, bool expectNull)
        {
            string? longName = region.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);

            var actualNormal = longName.ToRegion();
            var actualSpaceless = longNameSpaceless.ToRegion();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(region, actualNormal);
                Assert.Equal(region, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of Region values
        /// </summary>
        /// <returns>MemberData-compatible list of Region values</returns>
        public static TheoryData<Region?, bool> GenerateRegionTestData()
        {
            var testData = new TheoryData<Region?, bool>() { { null, true } };
            foreach (Region? region in Enum.GetValues<Region>().Cast<Region?>())
            {
                testData.Add(region, false);
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
            var actual = Extensions.ListSiteCodes();
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
