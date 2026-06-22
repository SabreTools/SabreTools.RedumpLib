using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Data.Sections;
using SabreTools.RedumpLib.Tools;
using Xunit;

// TODO: Fix all tests here to point to new formatter
// TODO: Add tests for all FormatOutputData methods
namespace SabreTools.RedumpLib.Test.Tools
{
    public class FormatterTests
    {
        #region ProcessSpecialFields

        // TODO: Write tests for ProcessSpecialFields

        #endregion

        #region DumpingInfoSection

        [Fact]
        public void FormatOutputData_DI_Formatted()
        {
            string expected = "Dumping Info:\n\tFrontend Version: XXXXXX\n\tDumping Program: XXXXXX\n\tDate: XXXXXX\n\tParameters: XXXXXX\n\tManufacturer: XXXXXX\n\tModel: XXXXXX\n\tFirmware: XXXXXX\n\tReported Disc Type: XXXXXX\n\tC2 Error Count: XXXXXX\n";

            var builder = new StringBuilder();
            DumpingInfoSection? section = new()
            {
                FrontendVersion = "XXXXXX",
                DumpingProgram = "XXXXXX",
                DumpingDate = "XXXXXX",
                DumpingParameters = "XXXXXX",
                Manufacturer = "XXXXXX",
                Model = "XXXXXX",
                Firmware = "XXXXXX",
                ReportedDiscType = "XXXXXX",
                C2ErrorsCount = "XXXXXX",
            };

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region FormatSiteTag

        [Fact]
        public void FormatSiteTag_NoValue_Empty()
        {
            SiteCode code = SiteCode.AlternativeTitle;
            string value = string.Empty;

            string actual = Formatter.FormatSiteTag(code, value);
            Assert.Empty(actual);
        }

        [Fact]
        public void FormatSiteTag_Standard_Formatted()
        {
            string expected = "[T:ALT] XXXXXX";
            SiteCode code = SiteCode.AlternativeTitle;
            string value = "XXXXXX";

            string actual = Formatter.FormatSiteTag(code, value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatSiteTag_BooleanTrue_Formatted()
        {
            string expected = "[T:VCD]";
            SiteCode code = SiteCode.VCD;
            string value = "True";

            string actual = Formatter.FormatSiteTag(code, value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatSiteTag_BooleanFalse_Empty()
        {
            SiteCode code = SiteCode.VCD;
            string value = "XXXXXX";

            string actual = Formatter.FormatSiteTag(code, value);
            Assert.Empty(actual);
        }

        [Fact]
        public void FormatSiteTag_Multiline_Formatted()
        {
            string expected = "[T:X]\nXXXXXX\n";
            SiteCode code = SiteCode.Extras;
            string value = "XXXXXX";

            string actual = Formatter.FormatSiteTag(code, value);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region GetFixedMediaType

        [Fact]
        public void GetFixedMediaType_NullType_Null()
        {
            PhysicalMediaType? mediaType = null;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = null;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Null(actual);
        }

        [Fact]
        public void GetFixedMediaType_UnformattedType_Formatted()
        {
            string? expected = "CD-ROM";

            PhysicalMediaType? mediaType = PhysicalMediaType.CDROM;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = null;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_DVD9_Formatted()
        {
            string? expected = "DVD-ROM-9";

            PhysicalMediaType? mediaType = PhysicalMediaType.DVD;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = 12345;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_DVD5_Formatted()
        {
            string? expected = "DVD-ROM-5";

            PhysicalMediaType? mediaType = PhysicalMediaType.DVD;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = null;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_BD128_Formatted()
        {
            string? expected = "BD-ROM-128";

            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = null;
            long? layerbreak2 = null;
            long? layerbreak3 = 12345;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_BD100_Formatted()
        {
            string? expected = "BD-ROM-100";

            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = null;
            long? layerbreak2 = 12345;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_BD66PIC_Formatted()
        {
            string? expected = "BD-ROM-66";

            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;
            string? picIdentifier = "BDU";
            long? size = null;
            long? layerbreak = 12345;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_BD66Size_Formatted()
        {
            string? expected = "BD-ROM-66";

            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;
            string? picIdentifier = null;
            long? size = 53_687_063_713;
            long? layerbreak = 12345;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_BD50_Formatted()
        {
            string? expected = "BD-ROM-50";

            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = 12345;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_BD33PIC_Formatted()
        {
            string? expected = "BD-ROM-33";

            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;
            string? picIdentifier = "BDU";
            long? size = null;
            long? layerbreak = null;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_BD33Size_Formatted()
        {
            string? expected = "BD-ROM-33";

            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;
            string? picIdentifier = null;
            long? size = 26_843_531_857;
            long? layerbreak = null;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_BD25_Formatted()
        {
            string? expected = "BD-ROM-25";

            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = null;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_HDDVDDL_Formatted()
        {
            string? expected = "HD-DVD-ROM-DL";

            PhysicalMediaType? mediaType = PhysicalMediaType.HDDVD;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = 12345;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_HDDVDSL_Formatted()
        {
            string? expected = "HD-DVD-ROM-SL";

            PhysicalMediaType? mediaType = PhysicalMediaType.HDDVD;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = null;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_UMDDL_Formatted()
        {
            string? expected = "UMD-DL";

            PhysicalMediaType? mediaType = PhysicalMediaType.UMD;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = 12345;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFixedMediaType_UMDSL_Formatted()
        {
            string? expected = "UMD-SL";

            PhysicalMediaType? mediaType = PhysicalMediaType.UMD;
            string? picIdentifier = null;
            long? size = null;
            long? layerbreak = null;
            long? layerbreak2 = null;
            long? layerbreak3 = null;

            string? actual = Formatter.GetFixedMediaType(mediaType,
                picIdentifier,
                size,
                layerbreak,
                layerbreak2,
                layerbreak3);

            Assert.Equal(expected, actual);
        }

        #endregion

        #region OrderCommentTags

        [Fact]
        public void OrderCommentTags_Empty_Empty()
        {
            Dictionary<SiteCode, string> tags = [];
            var actual = Formatter.OrderCommentTags(tags);
            Assert.Empty(actual);
        }

        [Fact]
        public void OrderCommentTags_NoMatch_Empty()
        {
            var tags = new Dictionary<SiteCode, string>
            {
                { SiteCode.Applications, "XXXXXX" },
            };
            var actual = Formatter.OrderCommentTags(tags);
            Assert.Empty(actual);
        }

        [Fact]
        public void OrderCommentTags_All_Ordered()
        {
            Dictionary<SiteCode, string> tags = [];
            foreach (SiteCode code in Enum.GetValues<SiteCode>())
            {
                tags[code] = "XXXXXX";
            }

            var actual = Formatter.OrderCommentTags(tags);

            Assert.NotEmpty(actual);
            var actualCodes = actual.Select(kvp => kvp.Key);
            Assert.True(Formatter.OrderedCommentCodes.SequenceEqual(actualCodes));
        }

        #endregion

        #region OrderContentTags

        [Fact]
        public void OrderContentTags_Empty_Empty()
        {
            Dictionary<SiteCode, string> tags = [];
            var actual = Formatter.OrderContentTags(tags);
            Assert.Empty(actual);
        }

        [Fact]
        public void OrderContentTags_NoMatch_Empty()
        {
            var tags = new Dictionary<SiteCode, string>
            {
                { SiteCode.AlternativeTitle, "XXXXXX" },
            };
            var actual = Formatter.OrderContentTags(tags);
            Assert.Empty(actual);
        }

        [Fact]
        public void OrderContentTags_All_Ordered()
        {
            Dictionary<SiteCode, string> tags = [];
            foreach (SiteCode code in Enum.GetValues<SiteCode>())
            {
                tags[code] = "XXXXXX";
            }

            var actual = Formatter.OrderContentTags(tags);

            Assert.NotEmpty(actual);
            var actualCodes = actual.Select(kvp => kvp.Key);
            Assert.True(Formatter.OrderedContentCodes.SequenceEqual(actualCodes));
        }

        #endregion

        #region RemoveConsecutiveEmptyLines

        [Fact]
        public void RemoveConsecutiveEmptyLines_Linux_Removed()
        {
            string expected = "data\n\nbase";
            string newlines = "data\n\n\n\n\n\n\n\n\n\nbase";

            string actual = Formatter.RemoveConsecutiveEmptyLines(newlines);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveConsecutiveEmptyLines_Windows_Removed()
        {
            string expected = "data\r\n\r\nbase";
            string newlines = "data\r\n\r\n\r\n\r\n\r\nbase";

            string actual = Formatter.RemoveConsecutiveEmptyLines(newlines);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
