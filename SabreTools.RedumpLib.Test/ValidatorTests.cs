using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Data.Sections;
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
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = null }
            };

            Validator.NormalizeDiscType(si);

            Assert.Null(si.CommonDiscInfo.Media);
        }

        [Fact]
        public void NormalizeDiscType_InvalidSizeChecksums_Untouched()
        {
            DiscType expected = DiscType.CD;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = DiscType.CD },
                SizeAndChecksums = new(),
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Fact]
        public void NormalizeDiscType_UnformattedType_Fixed()
        {
            DiscType expected = DiscType.CD;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = DiscType.CD },
                SizeAndChecksums = new SizeAndChecksumsSection(),
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(DiscType.DVD5)]
        [InlineData(DiscType.DVD9)]
        public void NormalizeDiscType_DVD9_Fixed(DiscType type)
        {
            DiscType expected = DiscType.DVD9;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection { Layerbreak = 12345 },
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(DiscType.DVD5)]
        [InlineData(DiscType.DVD9)]
        public void NormalizeDiscType_DVD5_Fixed(DiscType type)
        {
            DiscType expected = DiscType.DVD5;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection(),
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(DiscType.BD25)]
        [InlineData(DiscType.BD33)]
        [InlineData(DiscType.BD50)]
        [InlineData(DiscType.BD66)]
        [InlineData(DiscType.BD100)]
        [InlineData(DiscType.BD128)]
        public void NormalizeDiscType_BD128_Fixed(DiscType type)
        {
            DiscType expected = DiscType.BD128;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection { Layerbreak3 = 12345 },
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(DiscType.BD25)]
        [InlineData(DiscType.BD33)]
        [InlineData(DiscType.BD50)]
        [InlineData(DiscType.BD66)]
        [InlineData(DiscType.BD100)]
        [InlineData(DiscType.BD128)]
        public void NormalizeDiscType_BD100_Fixed(DiscType type)
        {
            DiscType expected = DiscType.BD100;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection { Layerbreak2 = 12345 },
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(DiscType.BD25)]
        [InlineData(DiscType.BD33)]
        [InlineData(DiscType.BD50)]
        [InlineData(DiscType.BD66)]
        [InlineData(DiscType.BD100)]
        [InlineData(DiscType.BD128)]
        public void NormalizeDiscType_BD66PIC_Fixed(DiscType type)
        {
            DiscType expected = DiscType.BD66;
            SubmissionInfo si = new SubmissionInfo
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
        [InlineData(DiscType.BD25)]
        [InlineData(DiscType.BD33)]
        [InlineData(DiscType.BD50)]
        [InlineData(DiscType.BD66)]
        [InlineData(DiscType.BD100)]
        [InlineData(DiscType.BD128)]
        public void NormalizeDiscType_BD66Size_Fixed(DiscType type)
        {
            DiscType expected = DiscType.BD66;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection
                {
                    Layerbreak = 12345,
                    Size = 50_050_629_633,
                },
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(DiscType.BD25)]
        [InlineData(DiscType.BD33)]
        [InlineData(DiscType.BD50)]
        [InlineData(DiscType.BD66)]
        [InlineData(DiscType.BD100)]
        [InlineData(DiscType.BD128)]
        public void NormalizeDiscType_BD50_Fixed(DiscType type)
        {
            DiscType expected = DiscType.BD50;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection { Layerbreak = 12345 },
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(DiscType.BD25)]
        [InlineData(DiscType.BD33)]
        [InlineData(DiscType.BD50)]
        [InlineData(DiscType.BD66)]
        [InlineData(DiscType.BD100)]
        [InlineData(DiscType.BD128)]
        public void NormalizeDiscType_BD33PIC_Fixed(DiscType type)
        {
            DiscType expected = DiscType.BD33;
            SubmissionInfo si = new SubmissionInfo
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
        [InlineData(DiscType.BD25)]
        [InlineData(DiscType.BD33)]
        [InlineData(DiscType.BD50)]
        [InlineData(DiscType.BD66)]
        [InlineData(DiscType.BD100)]
        [InlineData(DiscType.BD128)]
        public void NormalizeDiscType_BD33Size_Fixed(DiscType type)
        {
            DiscType expected = DiscType.BD33;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection
                {
                    Size = 25_025_314_817,
                },
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(DiscType.BD25)]
        [InlineData(DiscType.BD33)]
        [InlineData(DiscType.BD50)]
        [InlineData(DiscType.BD66)]
        [InlineData(DiscType.BD100)]
        [InlineData(DiscType.BD128)]
        public void NormalizeDiscType_BD25_Fixed(DiscType type)
        {
            DiscType expected = DiscType.BD25;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection(),
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

        [Theory]
        [InlineData(DiscType.UMDSL)]
        [InlineData(DiscType.UMDDL)]
        public void NormalizeDiscType_UMDDL_Fixed(DiscType type)
        {
            DiscType expected = DiscType.UMDDL;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection { Layerbreak = 12345 },
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }

         [Theory]
        [InlineData(DiscType.UMDSL)]
        [InlineData(DiscType.UMDDL)]
        public void NormalizeDiscType_UMDSL_Fixed(DiscType type)
        {
            DiscType expected = DiscType.UMDSL;
            SubmissionInfo si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection { Media = type },
                SizeAndChecksums = new SizeAndChecksumsSection(),
            };

            Validator.NormalizeDiscType(si);

            Assert.Equal(expected, si.CommonDiscInfo.Media);
        }
    }
}
