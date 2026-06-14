using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Data.Sections;
using SabreTools.RedumpLib.RedumpOrg.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test
{
    public class ValidatorTests
    {
        // Most tests here will require installing and using the Moq library
        // to mock the RedumpClient type.

        [Fact]
        public void NormalizeDiscType_InvalidMedia_Untouched()
        {
            var si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = null }
            };

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

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

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }
    }
}
