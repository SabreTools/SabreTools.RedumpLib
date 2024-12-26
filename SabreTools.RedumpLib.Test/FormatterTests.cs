using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test
{
    public class FormatterTests
    {
        #region ProcessSpecialFields

        // TODO: Write tests for ProcessSpecialFields

        #endregion

        #region CommonDiscInfoSection

        // TODO: Write tests for FormatOutputData(CommonDiscInfoSection)

        [Fact]
        public void FormatOutputData_CDINullSACNullTAWONull_Minimal()
        {
            string expected = "Common Disc Info:\n\tRegion: SPACE! (CHANGE THIS)\n\tLanguages: ADD LANGUAGES HERE (ONLY IF YOU TESTED)\n\n\tRingcode Information:\n\n\n";

            var builder = new StringBuilder();
            CommonDiscInfoSection? section = null;
            SizeAndChecksumsSection? sac = null;
            TracksAndWriteOffsetsSection? tawo = null;
            int? fullyMatchedID = null;
            List<int>? partiallyMatchedIDs = null;

            Formatter.FormatOutputData(builder,
                section,
                sac,
                tawo,
                fullyMatchedID,
                partiallyMatchedIDs);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region VersionAndEditionsSection

        [Fact]
        public void FormatOutputData_VAENull_Minimal()
        {
            string expected = "Version and Editions:\n";

            var builder = new StringBuilder();
            VersionAndEditionsSection? section = null;

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_VAE_Formatted()
        {
            string expected = "Version and Editions:\n\tVersion: XXXXXX\n\tEdition/Release: XXXXXX\n";

            var builder = new StringBuilder();
            VersionAndEditionsSection? section = new VersionAndEditionsSection
            {
                Version = "XXXXXX",
                OtherEditions = "XXXXXX",
            };

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region EDCSection

        [Fact]
        public void FormatOutputData_EDCNull_Minimal()
        {
            string expected = "EDC:\n";

            var builder = new StringBuilder();
            EDCSection? section = null;
            RedumpSystem? system = RedumpSystem.SonyPlayStation;

            Formatter.FormatOutputData(builder, section, system);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_EDCInvalidSystem_Empty()
        {
            string expected = string.Empty;

            var builder = new StringBuilder();
            EDCSection? section = null;
            RedumpSystem? system = RedumpSystem.IBMPCcompatible;

            Formatter.FormatOutputData(builder, section, system);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_EDC_Formatted()
        {
            string expected = "EDC:\n\tEDC: Yes\n";

            var builder = new StringBuilder();
            EDCSection? section = new EDCSection { EDC = YesNo.Yes };
            RedumpSystem? system = RedumpSystem.SonyPlayStation;

            Formatter.FormatOutputData(builder, section, system);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ExtrasSection

        [Fact]
        public void FormatOutputData_ExtrasNull_Empty()
        {
            string expected = string.Empty;

            var builder = new StringBuilder();
            ExtrasSection? section = null;

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_ExtrasInvalid_Empty()
        {
            string expected = string.Empty;

            var builder = new StringBuilder();
            ExtrasSection? section = new ExtrasSection
            {
                PVD = null,
                PIC = null,
                BCA = null,
                SecuritySectorRanges = null,
            };

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_Extras_Formatted()
        {
            string expected = "Extras:\n\tPrimary Volume Descriptor (PVD): XXXXXX\n\tDisc Key: XXXXXX\n\tDisc ID: XXXXXX\n\tPermanent Information & Control (PIC): XXXXXX\n\tHeader: XXXXXX\n\tBCA: XXXXXX\n\tSecurity Sector Ranges: XXXXXX\n";

            var builder = new StringBuilder();
            ExtrasSection? section = new ExtrasSection
            {
                PVD = "XXXXXX",
                DiscKey = "XXXXXX",
                DiscID = "XXXXXX",
                PIC = "XXXXXX",
                Header = "XXXXXX",
                BCA = "XXXXXX",
                SecuritySectorRanges = "XXXXXX",
            };

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region CopyProtectionSection

        [Fact]
        public void FormatOutputData_COPNull_Empty()
        {
            string expected = string.Empty;

            var builder = new StringBuilder();
            CopyProtectionSection? section = null;
            RedumpSystem? system = RedumpSystem.IBMPCcompatible;

            Formatter.FormatOutputData(builder, section, system);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_COPInvalid_Empty()
        {
            string expected = string.Empty;

            var builder = new StringBuilder();
            CopyProtectionSection? section = new CopyProtectionSection
            {
                Protection = null,
                AntiModchip = null,
                LibCrypt = null,
                LibCryptData = null,
                SecuROMData = null,
            };
            RedumpSystem? system = RedumpSystem.IBMPCcompatible;

            Formatter.FormatOutputData(builder, section, system);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_COP_Formatted()
        {
            string expected = "Copy Protection:\n\tCopy Protection: XXXXXX\n\tSubIntention Data (SecuROM/LibCrypt): XXXXXX\n";

            var builder = new StringBuilder();
            CopyProtectionSection? section = new CopyProtectionSection
            {
                AntiModchip = YesNo.Yes,
                LibCrypt = YesNo.Yes,
                LibCryptData = "XXXXXX",
                Protection = "XXXXXX",
                SecuROMData = "XXXXXX",
            };
            RedumpSystem? system = RedumpSystem.IBMPCcompatible;

            Formatter.FormatOutputData(builder, section, system);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_COPPSX_Formatted()
        {
            string expected = "Copy Protection:\n\tAnti-modchip: Yes\n\tLibCrypt: Yes\n\tSubIntention Data (SecuROM/LibCrypt): XXXXXX\n\tCopy Protection: XXXXXX\n\tSubIntention Data (SecuROM/LibCrypt): XXXXXX\n";

            var builder = new StringBuilder();
            CopyProtectionSection? section = new CopyProtectionSection
            {
                AntiModchip = YesNo.Yes,
                LibCrypt = YesNo.Yes,
                LibCryptData = "XXXXXX",
                Protection = "XXXXXX",
                SecuROMData = "XXXXXX",
            };
            RedumpSystem? system = RedumpSystem.SonyPlayStation;

            Formatter.FormatOutputData(builder, section, system);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region TracksAndWriteOffsetsSection

        [Fact]
        public void FormatOutputData_TAWOInvalid_Minimal()
        {
            string expected = "Tracks and Write Offsets:\n\tDAT:\n\n\n\n\n";

            var builder = new StringBuilder();
            TracksAndWriteOffsetsSection? section = new TracksAndWriteOffsetsSection();

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_TAWO_Formatted()
        {
            string expected = "Tracks and Write Offsets:\n\tDAT:\n\nXXXXXX\n\n\n\tCuesheet: XXXXXX\n\tWrite Offset: XXXXXX\n";

            var builder = new StringBuilder();
            TracksAndWriteOffsetsSection? section = new TracksAndWriteOffsetsSection
            {
                ClrMameProData = "XXXXXX",
                Cuesheet = "XXXXXX",
                OtherWriteOffsets = "XXXXXX",
            };

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region SizeAndChecksumsSection

        // TODO: Write tests for FormatOutputData(SizeAndChecksumsSection)

        #endregion

        #region DumpingInfoSection

        [Fact]
        public void FormatOutputData_DINull_Minimal()
        {
            string expected = "Dumping Info:\n";

            var builder = new StringBuilder();
            DumpingInfoSection? section = null;

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_DI_Formatted()
        {
            string expected = "Dumping Info:\n\tFrontend Version: XXXXXX\n\tDumping Program: XXXXXX\n\tDate: XXXXXX\n\tParameters: XXXXXX\n\tManufacturer: XXXXXX\n\tModel: XXXXXX\n\tFirmware: XXXXXX\n\tReported Disc Type: XXXXXX\n\tC2 Error Count: XXXXXX\n";

            var builder = new StringBuilder();
            DumpingInfoSection? section = new DumpingInfoSection
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
            MediaType? mediaType = null;
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

            MediaType? mediaType = MediaType.CDROM;
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

            MediaType? mediaType = MediaType.DVD;
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

            MediaType? mediaType = MediaType.DVD;
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

            MediaType? mediaType = MediaType.BluRay;
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

            MediaType? mediaType = MediaType.BluRay;
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

            MediaType? mediaType = MediaType.BluRay;
            string? picIdentifier = Models.PIC.Constants.DiscTypeIdentifierROMUltra;
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

            MediaType? mediaType = MediaType.BluRay;
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

            MediaType? mediaType = MediaType.BluRay;
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

            MediaType? mediaType = MediaType.BluRay;
            string? picIdentifier = Models.PIC.Constants.DiscTypeIdentifierROMUltra;
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

            MediaType? mediaType = MediaType.BluRay;
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

            MediaType? mediaType = MediaType.BluRay;
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

            MediaType? mediaType = MediaType.HDDVD;
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

            MediaType? mediaType = MediaType.HDDVD;
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

            MediaType? mediaType = MediaType.UMD;
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

            MediaType? mediaType = MediaType.UMD;
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