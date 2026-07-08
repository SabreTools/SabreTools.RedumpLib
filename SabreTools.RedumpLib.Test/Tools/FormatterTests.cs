using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Data.Sections;
using SabreTools.RedumpLib.Tools;
using Xunit;

namespace SabreTools.RedumpLib.Test.Tools
{
    public class FormatterTests
    {
        #region FormatOutputData

        [Fact]
        public void FormatOutputDataTest()
        {
            var submissionInfo = new SubmissionInfo()
            {
                SchemaVersion = 1,
                FullyMatchedIDs = [3],
                PartiallyMatchedIDs = [0, 1, 2, 3],
                Added = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,

                DiscIdentity = new DiscIdentitySection()
                {
                    System = PhysicalSystem.IBMPCcompatible,
                    Media = MediaType.CD,
                    Category = DiscCategory.Games,
                    Title = "Game Title",
                    ForeignTitle = "Foreign Game Title",
                    DiscNumber = "1",
                    DiscTitle = "Install Disc",
                    FilenameSuffix = "(Alt)",
                },

                RegionsAndLanguages = new RegionsAndLanguagesSection()
                {
                    Regions = [Region.World],
                    Languages = [Language.English, Language.Spanish, Language.French],
                },

                DiscIdentifiers = new DiscIdentifiersSection()
                {
                    DiscSerials = "Disc Serial",
                    Editions = "Rerelease",
                    Barcodes = "UPC Barcode",
                    Version = "Original",
                    ErrorCount = "0",
                    EXEDate = "19xx-xx-xx",
                    EDC = YesNo.Yes,
                    Layerbreak = 0,
                    Layerbreak2 = 1,
                    Layerbreak3 = 2,
                    DiscID = "Disc ID",
                    DiscKey = "Disc key",
                    UniversalHash = "SHA-1 hash",
                },

                RingCodes = new RingCodesSection()
                {
                    Layer0MasteringCode = "L0 Mastering Ring",
                    Layer0MasteringSID = "L0 Mastering SID",
                    Layer0Toolstamps = "L0 Toolstamp",
                    Layer0MouldSIDs = "L0 Mould SID",
                    Layer0AdditionalMoulds = "L0 Additional Mould",
                    Layer1MasteringCode = "L1 Mastering Ring",
                    Layer1MasteringSID = "L1 Mastering SID",
                    Layer1Toolstamps = "L1 Toolstamp",
                    Layer1MouldSIDs = "L1 Mould SID",
                    Layer1AdditionalMoulds = "L1 Additional Mould",
                    Layer2MasteringCode = "L2 Mastering Ring",
                    Layer2MasteringSID = "L2 Mastering SID",
                    Layer2Toolstamps = "L2 Toolstamp",
                    Layer2MouldSIDs = "L2 Mould SID",
                    Layer2AdditionalMoulds = "L2 Additional Mould",
                    Layer3MasteringCode = "L3 Mastering Ring",
                    Layer3MasteringSID = "L3 Mastering SID",
                    Layer3Toolstamps = "L3 Toolstamp",
                    Layer3MouldSIDs = "L3 Mould SID",
                    Layer3AdditionalMoulds = "L3 Additional Mould",
                    LabelSideMasteringCode = "LS Mastering Ring",
                    LabelSideMasteringSID = "LS Mastering SID",
                    LabelSideToolstamps = "LS Toolstamp",
                    LabelSideMouldSIDs = "LS Mould SID",
                    LabelSideAdditionalMoulds = "LS Additional Mould",
                    WriteOffset = "+12",
                    SampleStart = "+357",
                },

                DumpMetadata = new DumpMetadataSection()
                {
                    Comments = "Comment data line 1\r\nComment data line 2",
                    CommentsSpecialFields = new Dictionary<SiteCode, string>()
                    {
                        [SiteCode.ISBN] = "ISBN",
                    },
                    Contents = "Special contents 1\r\nSpecial contents 2",
                    ContentsSpecialFields = new Dictionary<SiteCode, string>()
                    {
                        [SiteCode.PlayableDemos] = "Game Demo 1",
                    },
                    Protection = "List of protections",
                    SectorRanges = "SSv1 Ranges",
                    SBI = "SecuROM data",
                    PVD = "PVD",
                    Header = "Header",
                    BCA = "BCA",
                    PIC = "PIC",
                    PICIdentifier = "XB4",
                    Cuesheet = "Cuesheet",
                    Dat = "Datfile",
                },

                SubmissionControls = new SubmissionControlsSection()
                {
                    DumpLog = "Redumper log",
                    LogsArchiveURL = "Logs URL",
                    ReviewComment = "Fine, I guess",
                    SubmissionComment = "Ignore the 3500 errors please",
                    SubmitAs = "SelfAutomaton",
                },

                DumpingInfo = new DumpingInfoSection()
                {
                    FrontendVersion = "10.0.0",
                    DumpingProgram = "Redumper b000",
                    DumpingDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                    DumpingParameters = "disc --drive=C:\\",
                    Manufacturer = "ATAPI",
                    Model = "Optical Disc Drive",
                    Firmware = "1.23",
                    ReportedDiscType = "CD-R",
                    C2ErrorsCount = "0",
                },

                Artifacts = new Dictionary<string, string>()
                {
                    ["Sample Artifact"] = "Sample Data",
                },
            };

            string? text = Formatter.FormatOutputData(submissionInfo, out _);
            Assert.NotNull(text);
        }

        #endregion

        #region ProcessSpecialFields

        // TODO: Write tests for ProcessSpecialFields

        #endregion

        #region DiscIdentitySection

        [Fact]
        public void FormatOutputData_DiscIdentitySection_Formatted()
        {
            string expected = "Disc Identity:\n\tSystem: Sony PlayStation 5\n\tMedia Type: BD-ROM-128\n\tCategory: Games\n\tTitle: XXXXXX\n\tForeign Title: XXXXXX\n\tDisc Number: XXXXXX\n\tDisc Title: XXXXXX\n\tFilename Suffix: XXXXXX\n\tFully Matching IDs: 1, 2, 3\n\tPartially Matching IDs: 4, 5, 6\n";

            var builder = new StringBuilder();
            DiscIdentitySection? section = new()
            {
                System = PhysicalSystem.SonyPlayStation5,
                Media = MediaType.BD128,
                Category = DiscCategory.Games,
                Title = "XXXXXX",
                ForeignTitle = "XXXXXX",
                DiscNumber = "XXXXXX",
                DiscTitle = "XXXXXX",
                FilenameSuffix = "XXXXXX",
            };
            DiscIdentifiersSection discIdentifiers = new()
            {
                Layerbreak = 1,
                Layerbreak2 = 2,
                Layerbreak3 = 3,
            };
            DumpMetadataSection dumpMetadata = new()
            {
                PICIdentifier = "BDU",
                Dat = @"<rom name=""name"" size=""12345"" crc=""crc"" md5=""md5"" sha1=""sha1"" />",
            };

            List<int>? fullyMatchedIDs = [1, 2, 3];
            List<int>? partiallyMatchedIDs = [4, 5, 6];

            Formatter.FormatOutputData(builder,
                section,
                discIdentifiers,
                dumpMetadata,
                fullyMatchedIDs,
                partiallyMatchedIDs);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region RegionsAndLanguagesSection

        // TODO: Add tests for null region and language
        [Fact]
        public void FormatOutputData_RegionsAndLanguagesSection_Formatted()
        {
            string expected = "Regions and Languages:\n\tRegions: USA, Europe, Japan\n\tLanguages: English, French, Spanish\n";

            var builder = new StringBuilder();
            RegionsAndLanguagesSection? section = new()
            {
                Regions = [Region.UnitedStatesOfAmerica, Region.Europe, Region.Japan],
                Languages = [Language.English, Language.French, Language.Spanish]
            };

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region DiscIdentifiersSection

        [Fact]
        public void FormatOutputData_DiscIdentifiersSection_Formatted()
        {
            string expected = "Disc Identifiers:\n\tDisc Serials: XXXXXX\n\tEditions: XXXXXX\n\tBarcodes: XXXXXX\n\tVersion: XXXXXX\n\tError Count: XXXXXX\n\tEXE Date: XXXXXX\n\tEDC: Yes\n\tLayerbreak: 1\n\tLayerbreak 2: 2\n\tLayerbreak 3: 3\n\tDisc ID: XXXXXX\n\tDisc Key: XXXXXX\n\tUniversal Hash: XXXXXX\n";

            var builder = new StringBuilder();
            DiscIdentifiersSection? section = new()
            {
                DiscSerials = "XXXXXX",
                Editions = "XXXXXX",
                Barcodes = "XXXXXX",
                Version = "XXXXXX",
                ErrorCount = "XXXXXX",
                EXEDate = "XXXXXX",
                EDC = YesNo.Yes,
                Layerbreak = 1,
                Layerbreak2 = 2,
                Layerbreak3 = 3,
                DiscID = "XXXXXX",
                DiscKey = "XXXXXX",
                UniversalHash = "XXXXXX",
            };

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, "Disc Identifiers:\n")]
        [InlineData(YesNo.NULL, "Disc Identifiers:\n")]
        [InlineData(YesNo.No, "Disc Identifiers:\n\tEDC: No\n")]
        [InlineData(YesNo.Yes, "Disc Identifiers:\n\tEDC: Yes\n")]
        public void FormatOutputData_DiscIdentifiersSection_EDCStatus_Formatted(YesNo? yesNo, string expected)
        {
            var builder = new StringBuilder();
            DiscIdentifiersSection? section = new()
            {
                EDC = yesNo,
            };

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region RingCodesSection

        [Fact]
        public void FormatOutputData_RingCodesSection_1Layer_Formatted()
        {
            string expected = "Ring Codes:\n\tLayer 0 Mastering Code: XXXXXX\n\tLayer 0 Mastering SID: XXXXXX\n\tLayer 0 Toolstamps: XXXXXX\n\tLayer 0 Mould SIDs: XXXXXX\n\tLayer 0 Additional Moulds: XXXXXX\n\tLabel Side Mastering Code: XXXXXX\n\tLabel Side Mastering SID: XXXXXX\n\tLabel Side Toolstamps: XXXXXX\n\tLabel Side Mould SIDs: XXXXXX\n\tLabel Side Additional Moulds: XXXXXX\n\tWrite Offset: XXXXXX\n\tSample Start: XXXXXX\n";

            var builder = new StringBuilder();
            RingCodesSection? section = new()
            {
                Layer0MasteringCode = "XXXXXX",
                Layer0MasteringSID = "XXXXXX",
                Layer0Toolstamps = "XXXXXX",
                Layer0MouldSIDs = "XXXXXX",
                Layer0AdditionalMoulds = "XXXXXX",

                Layer1MasteringCode = "XXXXXX",
                Layer1MasteringSID = "XXXXXX",
                Layer1Toolstamps = "XXXXXX",
                Layer1MouldSIDs = "XXXXXX",
                Layer1AdditionalMoulds = "XXXXXX",

                Layer2MasteringCode = "XXXXXX",
                Layer2MasteringSID = "XXXXXX",
                Layer2Toolstamps = "XXXXXX",
                Layer2MouldSIDs = "XXXXXX",
                Layer2AdditionalMoulds = "XXXXXX",

                Layer3MasteringCode = "XXXXXX",
                Layer3MasteringSID = "XXXXXX",
                Layer3Toolstamps = "XXXXXX",
                Layer3MouldSIDs = "XXXXXX",
                Layer3AdditionalMoulds = "XXXXXX",

                LabelSideMasteringCode = "XXXXXX",
                LabelSideMasteringSID = "XXXXXX",
                LabelSideToolstamps = "XXXXXX",
                LabelSideMouldSIDs = "XXXXXX",
                LabelSideAdditionalMoulds = "XXXXXX",

                WriteOffset = "XXXXXX",
                SampleStart = "XXXXXX",
            };
            DiscIdentifiersSection discIdentifiers = new();
            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;

            Formatter.FormatOutputData(builder,
                section,
                discIdentifiers,
                mediaType);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_RingCodesSection_2Layer_Formatted()
        {
            string expected = "Ring Codes:\n\tLayer 0 Mastering Code: XXXXXX\n\tLayer 0 Mastering SID: XXXXXX\n\tLayer 0 Toolstamps: XXXXXX\n\tLayer 0 Mould SIDs: XXXXXX\n\tLayer 0 Additional Moulds: XXXXXX\n\tLayer 1 Mastering Code: XXXXXX\n\tLayer 1 Mastering SID: XXXXXX\n\tLayer 1 Toolstamps: XXXXXX\n\tLayer 1 Mould SIDs: XXXXXX\n\tLayer 1 Additional Moulds: XXXXXX\n\tLabel Side Mastering Code: XXXXXX\n\tLabel Side Mastering SID: XXXXXX\n\tLabel Side Toolstamps: XXXXXX\n\tLabel Side Mould SIDs: XXXXXX\n\tLabel Side Additional Moulds: XXXXXX\n\tWrite Offset: XXXXXX\n\tSample Start: XXXXXX\n";

            var builder = new StringBuilder();
            RingCodesSection? section = new()
            {
                Layer0MasteringCode = "XXXXXX",
                Layer0MasteringSID = "XXXXXX",
                Layer0Toolstamps = "XXXXXX",
                Layer0MouldSIDs = "XXXXXX",
                Layer0AdditionalMoulds = "XXXXXX",

                Layer1MasteringCode = "XXXXXX",
                Layer1MasteringSID = "XXXXXX",
                Layer1Toolstamps = "XXXXXX",
                Layer1MouldSIDs = "XXXXXX",
                Layer1AdditionalMoulds = "XXXXXX",

                Layer2MasteringCode = "XXXXXX",
                Layer2MasteringSID = "XXXXXX",
                Layer2Toolstamps = "XXXXXX",
                Layer2MouldSIDs = "XXXXXX",
                Layer2AdditionalMoulds = "XXXXXX",

                Layer3MasteringCode = "XXXXXX",
                Layer3MasteringSID = "XXXXXX",
                Layer3Toolstamps = "XXXXXX",
                Layer3MouldSIDs = "XXXXXX",
                Layer3AdditionalMoulds = "XXXXXX",

                LabelSideMasteringCode = "XXXXXX",
                LabelSideMasteringSID = "XXXXXX",
                LabelSideToolstamps = "XXXXXX",
                LabelSideMouldSIDs = "XXXXXX",
                LabelSideAdditionalMoulds = "XXXXXX",

                WriteOffset = "XXXXXX",
                SampleStart = "XXXXXX",
            };
            DiscIdentifiersSection discIdentifiers = new()
            {
                Layerbreak = 1,
            };
            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;

            Formatter.FormatOutputData(builder,
                section,
                discIdentifiers,
                mediaType);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_RingCodesSection_3Layer_Formatted()
        {
            string expected = "Ring Codes:\n\tLayer 0 Mastering Code: XXXXXX\n\tLayer 0 Mastering SID: XXXXXX\n\tLayer 0 Toolstamps: XXXXXX\n\tLayer 0 Mould SIDs: XXXXXX\n\tLayer 0 Additional Moulds: XXXXXX\n\tLayer 1 Mastering Code: XXXXXX\n\tLayer 1 Mastering SID: XXXXXX\n\tLayer 1 Toolstamps: XXXXXX\n\tLayer 1 Mould SIDs: XXXXXX\n\tLayer 1 Additional Moulds: XXXXXX\n\tLayer 2 Mastering Code: XXXXXX\n\tLayer 2 Mastering SID: XXXXXX\n\tLayer 2 Toolstamps: XXXXXX\n\tLayer 2 Mould SIDs: XXXXXX\n\tLayer 2 Additional Moulds: XXXXXX\n\tLabel Side Mastering Code: XXXXXX\n\tLabel Side Mastering SID: XXXXXX\n\tLabel Side Toolstamps: XXXXXX\n\tLabel Side Mould SIDs: XXXXXX\n\tLabel Side Additional Moulds: XXXXXX\n\tWrite Offset: XXXXXX\n\tSample Start: XXXXXX\n";

            var builder = new StringBuilder();
            RingCodesSection? section = new()
            {
                Layer0MasteringCode = "XXXXXX",
                Layer0MasteringSID = "XXXXXX",
                Layer0Toolstamps = "XXXXXX",
                Layer0MouldSIDs = "XXXXXX",
                Layer0AdditionalMoulds = "XXXXXX",

                Layer1MasteringCode = "XXXXXX",
                Layer1MasteringSID = "XXXXXX",
                Layer1Toolstamps = "XXXXXX",
                Layer1MouldSIDs = "XXXXXX",
                Layer1AdditionalMoulds = "XXXXXX",

                Layer2MasteringCode = "XXXXXX",
                Layer2MasteringSID = "XXXXXX",
                Layer2Toolstamps = "XXXXXX",
                Layer2MouldSIDs = "XXXXXX",
                Layer2AdditionalMoulds = "XXXXXX",

                Layer3MasteringCode = "XXXXXX",
                Layer3MasteringSID = "XXXXXX",
                Layer3Toolstamps = "XXXXXX",
                Layer3MouldSIDs = "XXXXXX",
                Layer3AdditionalMoulds = "XXXXXX",

                LabelSideMasteringCode = "XXXXXX",
                LabelSideMasteringSID = "XXXXXX",
                LabelSideToolstamps = "XXXXXX",
                LabelSideMouldSIDs = "XXXXXX",
                LabelSideAdditionalMoulds = "XXXXXX",

                WriteOffset = "XXXXXX",
                SampleStart = "XXXXXX",
            };
            DiscIdentifiersSection discIdentifiers = new()
            {
                Layerbreak = 1,
                Layerbreak2 = 2,
            };
            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;

            Formatter.FormatOutputData(builder,
                section,
                discIdentifiers,
                mediaType);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_RingCodesSection_4Layer_Formatted()
        {
            string expected = "Ring Codes:\n\tLayer 0 Mastering Code: XXXXXX\n\tLayer 0 Mastering SID: XXXXXX\n\tLayer 0 Toolstamps: XXXXXX\n\tLayer 0 Mould SIDs: XXXXXX\n\tLayer 0 Additional Moulds: XXXXXX\n\tLayer 1 Mastering Code: XXXXXX\n\tLayer 1 Mastering SID: XXXXXX\n\tLayer 1 Toolstamps: XXXXXX\n\tLayer 1 Mould SIDs: XXXXXX\n\tLayer 1 Additional Moulds: XXXXXX\n\tLayer 2 Mastering Code: XXXXXX\n\tLayer 2 Mastering SID: XXXXXX\n\tLayer 2 Toolstamps: XXXXXX\n\tLayer 2 Mould SIDs: XXXXXX\n\tLayer 2 Additional Moulds: XXXXXX\n\tLayer 3 Mastering Code: XXXXXX\n\tLayer 3 Mastering SID: XXXXXX\n\tLayer 3 Toolstamps: XXXXXX\n\tLayer 3 Mould SIDs: XXXXXX\n\tLayer 3 Additional Moulds: XXXXXX\n\tLabel Side Mastering Code: XXXXXX\n\tLabel Side Mastering SID: XXXXXX\n\tLabel Side Toolstamps: XXXXXX\n\tLabel Side Mould SIDs: XXXXXX\n\tLabel Side Additional Moulds: XXXXXX\n\tWrite Offset: XXXXXX\n\tSample Start: XXXXXX\n";

            var builder = new StringBuilder();
            RingCodesSection? section = new()
            {
                Layer0MasteringCode = "XXXXXX",
                Layer0MasteringSID = "XXXXXX",
                Layer0Toolstamps = "XXXXXX",
                Layer0MouldSIDs = "XXXXXX",
                Layer0AdditionalMoulds = "XXXXXX",

                Layer1MasteringCode = "XXXXXX",
                Layer1MasteringSID = "XXXXXX",
                Layer1Toolstamps = "XXXXXX",
                Layer1MouldSIDs = "XXXXXX",
                Layer1AdditionalMoulds = "XXXXXX",

                Layer2MasteringCode = "XXXXXX",
                Layer2MasteringSID = "XXXXXX",
                Layer2Toolstamps = "XXXXXX",
                Layer2MouldSIDs = "XXXXXX",
                Layer2AdditionalMoulds = "XXXXXX",

                Layer3MasteringCode = "XXXXXX",
                Layer3MasteringSID = "XXXXXX",
                Layer3Toolstamps = "XXXXXX",
                Layer3MouldSIDs = "XXXXXX",
                Layer3AdditionalMoulds = "XXXXXX",

                LabelSideMasteringCode = "XXXXXX",
                LabelSideMasteringSID = "XXXXXX",
                LabelSideToolstamps = "XXXXXX",
                LabelSideMouldSIDs = "XXXXXX",
                LabelSideAdditionalMoulds = "XXXXXX",

                WriteOffset = "XXXXXX",
                SampleStart = "XXXXXX",
            };
            DiscIdentifiersSection discIdentifiers = new()
            {
                Layerbreak = 1,
                Layerbreak2 = 2,
                Layerbreak3 = 3,
            };
            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;

            Formatter.FormatOutputData(builder,
                section,
                discIdentifiers,
                mediaType);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatOutputData_RingCodesSection_UMDAlways2Layer_Formatted()
        {
            string expected = "Ring Codes:\n\tLayer 0 Mastering Code: XXXXXX\n\tLayer 0 Mastering SID: XXXXXX\n\tLayer 0 Toolstamps: XXXXXX\n\tLayer 0 Mould SIDs: XXXXXX\n\tLayer 0 Additional Moulds: XXXXXX\n\tLayer 1 Mastering Code: XXXXXX\n\tLayer 1 Mastering SID: XXXXXX\n\tLayer 1 Toolstamps: XXXXXX\n\tLayer 1 Mould SIDs: XXXXXX\n\tLayer 1 Additional Moulds: XXXXXX\n\tLabel Side Mastering Code: XXXXXX\n\tLabel Side Mastering SID: XXXXXX\n\tLabel Side Toolstamps: XXXXXX\n\tLabel Side Mould SIDs: XXXXXX\n\tLabel Side Additional Moulds: XXXXXX\n\tWrite Offset: XXXXXX\n\tSample Start: XXXXXX\n";

            var builder = new StringBuilder();
            RingCodesSection? section = new()
            {
                Layer0MasteringCode = "XXXXXX",
                Layer0MasteringSID = "XXXXXX",
                Layer0Toolstamps = "XXXXXX",
                Layer0MouldSIDs = "XXXXXX",
                Layer0AdditionalMoulds = "XXXXXX",

                Layer1MasteringCode = "XXXXXX",
                Layer1MasteringSID = "XXXXXX",
                Layer1Toolstamps = "XXXXXX",
                Layer1MouldSIDs = "XXXXXX",
                Layer1AdditionalMoulds = "XXXXXX",

                Layer2MasteringCode = "XXXXXX",
                Layer2MasteringSID = "XXXXXX",
                Layer2Toolstamps = "XXXXXX",
                Layer2MouldSIDs = "XXXXXX",
                Layer2AdditionalMoulds = "XXXXXX",

                Layer3MasteringCode = "XXXXXX",
                Layer3MasteringSID = "XXXXXX",
                Layer3Toolstamps = "XXXXXX",
                Layer3MouldSIDs = "XXXXXX",
                Layer3AdditionalMoulds = "XXXXXX",

                LabelSideMasteringCode = "XXXXXX",
                LabelSideMasteringSID = "XXXXXX",
                LabelSideToolstamps = "XXXXXX",
                LabelSideMouldSIDs = "XXXXXX",
                LabelSideAdditionalMoulds = "XXXXXX",

                WriteOffset = "XXXXXX",
                SampleStart = "XXXXXX",
            };
            DiscIdentifiersSection discIdentifiers = new();
            PhysicalMediaType? mediaType = PhysicalMediaType.UMD;

            Formatter.FormatOutputData(builder,
                section,
                discIdentifiers,
                mediaType);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, "Ring Codes:\n")]
        [InlineData("INVALID", "Ring Codes:\n\tWrite Offset: INVALID\n")]
        [InlineData("-1", "Ring Codes:\n\tWrite Offset: -1\n")]
        [InlineData("0", "Ring Codes:\n\tWrite Offset: 0\n")]
        [InlineData("1", "Ring Codes:\n\tWrite Offset: +1\n")]
        [InlineData("+1", "Ring Codes:\n\tWrite Offset: +1\n")]
        public void FormatOutputData_RingCodesSection_Offsets_Formatted(string? offset, string expected)
        {
            var builder = new StringBuilder();
            RingCodesSection? section = new()
            {
                WriteOffset = offset,
            };
            DiscIdentifiersSection discIdentifiers = new();
            PhysicalMediaType? mediaType = PhysicalMediaType.BluRay;

            Formatter.FormatOutputData(builder,
                section,
                discIdentifiers,
                mediaType);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region DumpMetadataSection

        [Fact]
        public void FormatOutputData_DumpMetadataSection_Formatted()
        {
            string expected = "Dump Metadata:\n\tComments: XXXXXX\n\tContents: XXXXXX\n\tProtection: XXXXXX\n\tSector Ranges: XXXXXX\n\tSBI: XXXXXX\n\tPrimary Volume Descriptor (PVD): XXXXXX\n\tHeader: XXXXXX\n\tBurst Cutting Area (BCA): XXXXXX\n\tPermanent Information & Control (PIC): XXXXXX\n\tCuesheet: XXXXXX\n\tDat:\n\nXXXXXX\n\n\n";

            var builder = new StringBuilder();
            DumpMetadataSection? section = new()
            {
                Comments = "XXXXXX",
                Contents = "XXXXXX",
                Protection = "XXXXXX",
                SectorRanges = "XXXXXX",
                SBI = "XXXXXX",
                PVD = "XXXXXX",
                Header = "XXXXXX",
                BCA = "XXXXXX",
                PIC = "XXXXXX",
                Cuesheet = "XXXXXX",
                Dat = "XXXXXX",
            };

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region SubmissionControlsSection

        [Fact]
        public void FormatOutputData_SubmissionControlsSection_Formatted()
        {
            string expected = "Submission Controls:\n\tDump Log: XXXXXX\n\tLogs Archive URL: XXXXXX\n\tReview Comment: XXXXXX\n\tSubmission Comment: XXXXXX\n\tSubmit As: XXXXXX\n";

            var builder = new StringBuilder();
            SubmissionControlsSection? section = new()
            {
                DumpLog = "XXXXXX",
                LogsArchiveURL = "XXXXXX",
                ReviewComment = "XXXXXX",
                SubmissionComment = "XXXXXX",
                SubmitAs = "XXXXXX",
            };

            Formatter.FormatOutputData(builder, section);

            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region DumpingInfoSection

        [Fact]
        public void FormatOutputData_DumpingInfoSection_Formatted()
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
