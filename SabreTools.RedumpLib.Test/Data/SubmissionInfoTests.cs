using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Data.Sections;
using Xunit;

namespace SabreTools.RedumpLib.Test.Data
{
    public class SubmissionInfoTests
    {
        [Fact]
        public void DefaultSerializationTest()
        {
            var submissionInfo = new SubmissionInfo();
            string json = JsonConvert.SerializeObject(submissionInfo, Formatting.Indented);
            Assert.NotNull(json);
        }

        [Fact]
        public void FullSerializationTest()
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

            string json = JsonConvert.SerializeObject(submissionInfo, Formatting.Indented);
            Assert.NotNull(json);
        }
    }
}
