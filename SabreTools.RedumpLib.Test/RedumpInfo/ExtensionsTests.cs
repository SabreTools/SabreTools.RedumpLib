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

        #region System

        /// <summary>
        /// RedumpSystem values that are considered Audio
        /// </summary>
        private static readonly RedumpSystem?[] _audioSystems =
        [
            RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem,
            RedumpSystem.AudioCD,
            RedumpSystem.DVDAudio,
            RedumpSystem.HasbroiONEducationalGamingSystem,
            RedumpSystem.HasbroVideoNow,
            RedumpSystem.HasbroVideoNowColor,
            RedumpSystem.HasbroVideoNowJr,
            RedumpSystem.HasbroVideoNowXP,
            RedumpSystem.PlayStationGameSharkUpdates,
            RedumpSystem.PhilipsCDi,
            RedumpSystem.SuperAudioCD,
        ];

        /// <summary>
        /// RedumpSystem values that are available
        /// </summary>
        private static readonly RedumpSystem?[] _availableSystems =
        [
            // BIOS Sets
            RedumpSystem.MicrosoftXboxBIOS,
            RedumpSystem.NintendoGameCubeBIOS,
            RedumpSystem.SonyPlayStationBIOS,
            RedumpSystem.SonyPlayStation2BIOS,

            // Disc-Based Consoles
            RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem,
            RedumpSystem.BandaiPlaydiaQuickInteractiveSystem,
            RedumpSystem.BandaiPippin,
            RedumpSystem.CommodoreAmigaCD32,
            RedumpSystem.CommodoreAmigaCDTV,
            RedumpSystem.HasbroVideoNow,
            RedumpSystem.HasbroVideoNowColor,
            RedumpSystem.HasbroVideoNowJr,
            RedumpSystem.HasbroVideoNowXP,
            RedumpSystem.MattelFisherPriceiXL,
            RedumpSystem.MattelHyperScan,
            RedumpSystem.MemorexVisualInformationSystem,
            RedumpSystem.MicrosoftXbox,
            RedumpSystem.MicrosoftXbox360,
            RedumpSystem.MicrosoftXboxOne,
            RedumpSystem.MicrosoftXboxSeriesXS,
            RedumpSystem.NECPCEngineCDTurboGrafxCD,
            RedumpSystem.NECPCFXPCFXGA,
            RedumpSystem.NintendoGameCube,
            RedumpSystem.NintendoWii,
            RedumpSystem.NintendoWiiU,
            RedumpSystem.Panasonic3DOInteractiveMultiplayer,
            RedumpSystem.PhilipsCDi,
            RedumpSystem.SegaDreamcast,
            RedumpSystem.SegaMegaCDSegaCD,
            RedumpSystem.SegaSaturn,
            RedumpSystem.SNKNeoGeoCD,
            RedumpSystem.SonyPlayStation,
            RedumpSystem.SonyPlayStation2,
            RedumpSystem.SonyPlayStation3,
            RedumpSystem.SonyPlayStation4,
            RedumpSystem.SonyPlayStation5,
            RedumpSystem.SonyPlayStationPortable,
            RedumpSystem.VMLabsNUON,
            RedumpSystem.VTechVFlashVSmilePro,
            RedumpSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem,

            // Computers
            RedumpSystem.AcornArchimedes,
            RedumpSystem.AppleMacintosh,
            RedumpSystem.CommodoreAmigaCD,
            RedumpSystem.FujitsuFMTownsseries,
            RedumpSystem.IBMPCcompatible,
            RedumpSystem.NECPC88series,
            RedumpSystem.NECPC98series,
            RedumpSystem.SharpX68000,

            // Arcade
            RedumpSystem.funworldPhotoPlay,
            RedumpSystem.IncredibleTechnologiesEagle,
            RedumpSystem.KonamieAmusement,
            RedumpSystem.KonamiFireBeat,
            RedumpSystem.KonamiM2,
            RedumpSystem.KonamiSystem573,
            RedumpSystem.KonamiSystemGV,
            RedumpSystem.KonamiTwinkle,
            RedumpSystem.NamcoSegaNintendoTriforce,
            RedumpSystem.NamcoSystem12,
            RedumpSystem.NamcoSystem246256,
            RedumpSystem.PanasonicM2,
            RedumpSystem.SegaChihiro,
            RedumpSystem.SegaLindbergh,
            RedumpSystem.SegaNaomi,
            RedumpSystem.SegaNaomi2,
            RedumpSystem.SegaRingEdge,
            RedumpSystem.SegaRingEdge2,
            RedumpSystem.SegaTitanVideo,
            RedumpSystem.TABAustriaQuizard,

            // Other
            RedumpSystem.AudioCD,
            RedumpSystem.BDVideo,
            RedumpSystem.DVDVideo,
            RedumpSystem.EnhancedCD,
            RedumpSystem.HDDVDVideo,
            RedumpSystem.NavisoftNaviken21,
            RedumpSystem.PalmOS,
            RedumpSystem.PhotoCD,
            RedumpSystem.PlayStationGameSharkUpdates,
            RedumpSystem.PocketPC,
            RedumpSystem.SegaPrologue21MultimediaKaraokeSystem,
            RedumpSystem.TaoiKTV,
            RedumpSystem.TomyKissSite,
            RedumpSystem.VideoCD,
        ];

        /// <summary>
        /// RedumpSystem values that are dumper-only
        /// </summary>
        private static readonly RedumpSystem?[] _bannedSystems =
        [
            // Disc-Based Consoles
            RedumpSystem.HasbroVideoNow,
            RedumpSystem.HasbroVideoNowColor,
            RedumpSystem.HasbroVideoNowJr,
            RedumpSystem.HasbroVideoNowXP,
            RedumpSystem.MicrosoftXboxOne,
            RedumpSystem.MicrosoftXboxSeriesXS,
            RedumpSystem.NintendoWiiU,
            RedumpSystem.SonyPlayStation4,
            RedumpSystem.SonyPlayStation5,

            // Arcade
            RedumpSystem.KonamiM2,
            RedumpSystem.PanasonicM2,

            // Other
            RedumpSystem.AudioCD,
            RedumpSystem.BDVideo,
            RedumpSystem.DVDVideo,
            RedumpSystem.EnhancedCD,
            RedumpSystem.HDDVDVideo,
            RedumpSystem.NavisoftNaviken21,
            RedumpSystem.VideoCD,
        ];

        /// <summary>
        /// RedumpSystem values that are considered markers
        /// </summary>
        private static readonly RedumpSystem?[] _markerSystems =
        [
            RedumpSystem.MarkerArcadeEnd,
            RedumpSystem.MarkerComputerEnd,
            RedumpSystem.MarkerDiscBasedConsoleEnd,
            RedumpSystem.MarkerOtherEnd,
        ];

        /// <summary>
        /// RedumpSystem values that are have reversed ringcodes
        /// </summary>
        private static readonly RedumpSystem?[] _reverseRingcodeSystems =
        [
            RedumpSystem.SonyPlayStation2,
            RedumpSystem.SonyPlayStation3,
            RedumpSystem.SonyPlayStation4,
            RedumpSystem.SonyPlayStation5,
            RedumpSystem.SonyPlayStationPortable,
        ];

        /// <summary>
        /// Map of RedumpSystem values to their corresponding categories
        /// </summary>
        private static readonly Dictionary<RedumpSystem, SystemCategory> _systemCategoryMap = new()
        {
            // BIOS
            [RedumpSystem.MicrosoftXboxBIOS] = SystemCategory.NONE,
            [RedumpSystem.NintendoGameCubeBIOS] = SystemCategory.NONE,
            [RedumpSystem.SonyPlayStationBIOS] = SystemCategory.NONE,
            [RedumpSystem.SonyPlayStation2BIOS] = SystemCategory.NONE,

            // Disc-Based Consoles
            [RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.BandaiPlaydiaQuickInteractiveSystem] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.BandaiPippin] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.CommodoreAmigaCD32] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.CommodoreAmigaCDTV] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.EnvizionsEVOSmartConsole] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.FujitsuFMTownsMarty] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.HasbroiONEducationalGamingSystem] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.HasbroVideoNow] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.HasbroVideoNowColor] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.HasbroVideoNowJr] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.HasbroVideoNowXP] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.MattelFisherPriceiXL] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.MattelHyperScan] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.MemorexVisualInformationSystem] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.MicrosoftXbox] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.MicrosoftXbox360] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.MicrosoftXboxOne] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.MicrosoftXboxSeriesXS] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.NECPCEngineCDTurboGrafxCD] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.NECPCFXPCFXGA] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.NintendoGameCube] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.NintendoSonySuperNESCDROMSystem] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.NintendoWii] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.NintendoWiiU] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.Panasonic3DOInteractiveMultiplayer] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.PhilipsCDi] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.PlaymajiPolymega] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.PioneerLaserActive] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.SegaDreamcast] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.SegaMegaCDSegaCD] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.SegaSaturn] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.SNKNeoGeoCD] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.SonyPlayStation] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.SonyPlayStation2] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.SonyPlayStation3] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.SonyPlayStation4] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.SonyPlayStation5] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.SonyPlayStationPortable] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.VMLabsNUON] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.VTechVFlashVSmilePro] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem] = SystemCategory.DiscBasedConsole,
            [RedumpSystem.MarkerDiscBasedConsoleEnd] = SystemCategory.NONE,

            // Computers
            [RedumpSystem.AcornArchimedes] = SystemCategory.Computer,
            [RedumpSystem.AppleMacintosh] = SystemCategory.Computer,
            [RedumpSystem.CommodoreAmigaCD] = SystemCategory.Computer,
            [RedumpSystem.FujitsuFMTownsseries] = SystemCategory.Computer,
            [RedumpSystem.IBMPCcompatible] = SystemCategory.Computer,
            [RedumpSystem.NECPC88series] = SystemCategory.Computer,
            [RedumpSystem.NECPC98series] = SystemCategory.Computer,
            [RedumpSystem.SharpX68000] = SystemCategory.Computer,
            [RedumpSystem.MarkerComputerEnd] = SystemCategory.NONE,

            // Arcade
            [RedumpSystem.AmigaCUBOCD32] = SystemCategory.Arcade,
            [RedumpSystem.AmericanLaserGames3DO] = SystemCategory.Arcade,
            [RedumpSystem.Atari3DO] = SystemCategory.Arcade,
            [RedumpSystem.Atronic] = SystemCategory.Arcade,
            [RedumpSystem.AUSCOMSystem1] = SystemCategory.Arcade,
            [RedumpSystem.BallyGameMagic] = SystemCategory.Arcade,
            [RedumpSystem.CapcomCPSystemIII] = SystemCategory.Arcade,
            [RedumpSystem.funworldPhotoPlay] = SystemCategory.Arcade,
            [RedumpSystem.FuRyuOmronPurikura] = SystemCategory.Arcade,
            [RedumpSystem.GlobalVRVarious] = SystemCategory.Arcade,
            [RedumpSystem.GlobalVRVortek] = SystemCategory.Arcade,
            [RedumpSystem.GlobalVRVortekV3] = SystemCategory.Arcade,
            [RedumpSystem.ICEPCHardware] = SystemCategory.Arcade,
            [RedumpSystem.IncredibleTechnologiesEagle] = SystemCategory.Arcade,
            [RedumpSystem.IncredibleTechnologiesVarious] = SystemCategory.Arcade,
            [RedumpSystem.JVLiTouch] = SystemCategory.Arcade,
            [RedumpSystem.KonamieAmusement] = SystemCategory.Arcade,
            [RedumpSystem.KonamiFireBeat] = SystemCategory.Arcade,
            [RedumpSystem.KonamiM2] = SystemCategory.Arcade,
            [RedumpSystem.KonamiPython] = SystemCategory.Arcade,
            [RedumpSystem.KonamiPython2] = SystemCategory.Arcade,
            [RedumpSystem.KonamiSystem573] = SystemCategory.Arcade,
            [RedumpSystem.KonamiSystemGV] = SystemCategory.Arcade,
            [RedumpSystem.KonamiTwinkle] = SystemCategory.Arcade,
            [RedumpSystem.KonamiVarious] = SystemCategory.Arcade,
            [RedumpSystem.MeritIndustriesBoardwalk] = SystemCategory.Arcade,
            [RedumpSystem.MeritIndustriesMegaTouchForce] = SystemCategory.Arcade,
            [RedumpSystem.MeritIndustriesMegaTouchION] = SystemCategory.Arcade,
            [RedumpSystem.MeritIndustriesMegaTouchMaxx] = SystemCategory.Arcade,
            [RedumpSystem.MeritIndustriesMegaTouchXL] = SystemCategory.Arcade,
            [RedumpSystem.NamcoPurikura] = SystemCategory.Arcade,
            [RedumpSystem.NamcoSegaNintendoTriforce] = SystemCategory.Arcade,
            [RedumpSystem.NamcoSystem12] = SystemCategory.Arcade,
            [RedumpSystem.NamcoSystem246256] = SystemCategory.Arcade,
            [RedumpSystem.NewJatreCDi] = SystemCategory.Arcade,
            [RedumpSystem.NichibutsuHighRateSystem] = SystemCategory.Arcade,
            [RedumpSystem.NichibutsuSuperCD] = SystemCategory.Arcade,
            [RedumpSystem.NichibutsuXRateSystem] = SystemCategory.Arcade,
            [RedumpSystem.PanasonicM2] = SystemCategory.Arcade,
            [RedumpSystem.PhotoPlayVarious] = SystemCategory.Arcade,
            [RedumpSystem.RawThrillsVarious] = SystemCategory.Arcade,
            [RedumpSystem.SegaALLS] = SystemCategory.Arcade,
            [RedumpSystem.SegaChihiro] = SystemCategory.Arcade,
            [RedumpSystem.SegaEuropaR] = SystemCategory.Arcade,
            [RedumpSystem.SegaLindbergh] = SystemCategory.Arcade,
            [RedumpSystem.SegaNaomi] = SystemCategory.Arcade,
            [RedumpSystem.SegaNaomi2] = SystemCategory.Arcade,
            [RedumpSystem.SegaNaomiSatelliteTerminalPC] = SystemCategory.Arcade,
            [RedumpSystem.SegaNu] = SystemCategory.Arcade,
            [RedumpSystem.SegaNu11] = SystemCategory.Arcade,
            [RedumpSystem.SegaNu2] = SystemCategory.Arcade,
            [RedumpSystem.SegaNuSX] = SystemCategory.Arcade,
            [RedumpSystem.SegaRingEdge] = SystemCategory.Arcade,
            [RedumpSystem.SegaRingEdge2] = SystemCategory.Arcade,
            [RedumpSystem.SegaRingWide] = SystemCategory.Arcade,
            [RedumpSystem.SegaSystem32] = SystemCategory.Arcade,
            [RedumpSystem.SegaTitanVideo] = SystemCategory.Arcade,
            [RedumpSystem.SeibuCATSSystem] = SystemCategory.Arcade,
            [RedumpSystem.TABAustriaQuizard] = SystemCategory.Arcade,
            [RedumpSystem.TsunamiTsuMoMultiGameMotionSystem] = SystemCategory.Arcade,
            [RedumpSystem.UltraCade] = SystemCategory.Arcade,
            [RedumpSystem.MarkerArcadeEnd] = SystemCategory.NONE,

            // Other
            [RedumpSystem.AudioCD] = SystemCategory.Other,
            [RedumpSystem.BDVideo] = SystemCategory.Other,
            [RedumpSystem.DVDAudio] = SystemCategory.Other,
            [RedumpSystem.DVDVideo] = SystemCategory.Other,
            [RedumpSystem.EnhancedCD] = SystemCategory.Other,
            [RedumpSystem.HDDVDVideo] = SystemCategory.Other,
            [RedumpSystem.NavisoftNaviken21] = SystemCategory.Other,
            [RedumpSystem.PalmOS] = SystemCategory.Other,
            [RedumpSystem.PhotoCD] = SystemCategory.Other,
            [RedumpSystem.PlayStationGameSharkUpdates] = SystemCategory.Other,
            [RedumpSystem.PocketPC] = SystemCategory.Other,
            [RedumpSystem.Psion] = SystemCategory.Other,
            [RedumpSystem.RainbowDisc] = SystemCategory.Other,
            [RedumpSystem.SegaPrologue21MultimediaKaraokeSystem] = SystemCategory.Other,
            [RedumpSystem.SharpZaurus] = SystemCategory.Other,
            [RedumpSystem.SonyElectronicBook] = SystemCategory.Other,
            [RedumpSystem.SuperAudioCD] = SystemCategory.Other,
            [RedumpSystem.TaoiKTV] = SystemCategory.Other,
            [RedumpSystem.TomyKissSite] = SystemCategory.Other,
            [RedumpSystem.VideoCD] = SystemCategory.Other,
            [RedumpSystem.MarkerOtherEnd] = SystemCategory.NONE,
        };

        /// <summary>
        /// RedumpSystem values that have cuesheet packs
        /// </summary>
        private static readonly RedumpSystem?[] _systemsWithCues =
        [
            // Disc-Based Consoles
            RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem,
            RedumpSystem.BandaiPlaydiaQuickInteractiveSystem,
            RedumpSystem.BandaiPippin,
            RedumpSystem.CommodoreAmigaCD32,
            RedumpSystem.CommodoreAmigaCDTV,
            RedumpSystem.HasbroVideoNow,
            RedumpSystem.HasbroVideoNowColor,
            RedumpSystem.HasbroVideoNowJr,
            RedumpSystem.HasbroVideoNowXP,
            RedumpSystem.MattelFisherPriceiXL,
            RedumpSystem.MattelHyperScan,
            RedumpSystem.MemorexVisualInformationSystem,
            RedumpSystem.MicrosoftXbox,
            RedumpSystem.MicrosoftXbox360,
            RedumpSystem.NECPCEngineCDTurboGrafxCD,
            RedumpSystem.NECPCFXPCFXGA,
            RedumpSystem.Panasonic3DOInteractiveMultiplayer,
            RedumpSystem.PhilipsCDi,
            RedumpSystem.SegaDreamcast,
            RedumpSystem.SegaMegaCDSegaCD,
            RedumpSystem.SegaSaturn,
            RedumpSystem.SNKNeoGeoCD,
            RedumpSystem.SonyPlayStation,
            RedumpSystem.SonyPlayStation2,
            RedumpSystem.SonyPlayStation3,
            RedumpSystem.VTechVFlashVSmilePro,

            // Computers
            RedumpSystem.AcornArchimedes,
            RedumpSystem.AppleMacintosh,
            RedumpSystem.CommodoreAmigaCD,
            RedumpSystem.FujitsuFMTownsseries,
            RedumpSystem.IBMPCcompatible,
            RedumpSystem.NECPC88series,
            RedumpSystem.NECPC98series,
            RedumpSystem.SharpX68000,

            // Arcade
            RedumpSystem.funworldPhotoPlay,
            RedumpSystem.IncredibleTechnologiesEagle,
            RedumpSystem.KonamieAmusement,
            RedumpSystem.KonamiFireBeat,
            RedumpSystem.KonamiM2,
            RedumpSystem.KonamiSystem573,
            RedumpSystem.KonamiSystemGV,
            RedumpSystem.NamcoSegaNintendoTriforce,
            RedumpSystem.NamcoSystem246256,
            RedumpSystem.PanasonicM2,
            RedumpSystem.SegaChihiro,
            RedumpSystem.SegaNaomi,
            RedumpSystem.SegaNaomi2,
            RedumpSystem.TABAustriaQuizard,

            // Other
            RedumpSystem.AudioCD,
            RedumpSystem.NavisoftNaviken21,
            RedumpSystem.PalmOS,
            RedumpSystem.PhotoCD,
            RedumpSystem.PlayStationGameSharkUpdates,
            RedumpSystem.PocketPC,
            RedumpSystem.SegaPrologue21MultimediaKaraokeSystem,
            RedumpSystem.TomyKissSite,
            RedumpSystem.VideoCD,
        ];

        /// <summary>
        /// RedumpSystem values that have dats
        /// </summary>
        private static readonly RedumpSystem?[] _systemsWithDats =
        [
            // BIOS Sets
            RedumpSystem.MicrosoftXboxBIOS,
            RedumpSystem.NintendoGameCubeBIOS,
            RedumpSystem.SonyPlayStationBIOS,
            RedumpSystem.SonyPlayStation2BIOS,

            // Disc-Based Consoles
            RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem,
            RedumpSystem.BandaiPlaydiaQuickInteractiveSystem,
            RedumpSystem.BandaiPippin,
            RedumpSystem.CommodoreAmigaCD32,
            RedumpSystem.CommodoreAmigaCDTV,
            RedumpSystem.HasbroVideoNow,
            RedumpSystem.HasbroVideoNowColor,
            RedumpSystem.HasbroVideoNowJr,
            RedumpSystem.HasbroVideoNowXP,
            RedumpSystem.MattelFisherPriceiXL,
            RedumpSystem.MattelHyperScan,
            RedumpSystem.MemorexVisualInformationSystem,
            RedumpSystem.MicrosoftXbox,
            RedumpSystem.MicrosoftXbox360,
            RedumpSystem.MicrosoftXboxOne,
            RedumpSystem.MicrosoftXboxSeriesXS,
            RedumpSystem.NECPCEngineCDTurboGrafxCD,
            RedumpSystem.NECPCFXPCFXGA,
            RedumpSystem.NintendoGameCube,
            RedumpSystem.NintendoWii,
            RedumpSystem.NintendoWiiU,
            RedumpSystem.Panasonic3DOInteractiveMultiplayer,
            RedumpSystem.PhilipsCDi,
            RedumpSystem.SegaDreamcast,
            RedumpSystem.SegaMegaCDSegaCD,
            RedumpSystem.SegaSaturn,
            RedumpSystem.SNKNeoGeoCD,
            RedumpSystem.SonyPlayStation,
            RedumpSystem.SonyPlayStation2,
            RedumpSystem.SonyPlayStation3,
            RedumpSystem.SonyPlayStation4,
            RedumpSystem.SonyPlayStation5,
            RedumpSystem.SonyPlayStationPortable,
            RedumpSystem.VMLabsNUON,
            RedumpSystem.VTechVFlashVSmilePro,
            RedumpSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem,

            // Computers
            RedumpSystem.AcornArchimedes,
            RedumpSystem.AppleMacintosh,
            RedumpSystem.CommodoreAmigaCD,
            RedumpSystem.FujitsuFMTownsseries,
            RedumpSystem.IBMPCcompatible,
            RedumpSystem.NECPC88series,
            RedumpSystem.NECPC98series,
            RedumpSystem.SharpX68000,

            // Arcade
            RedumpSystem.funworldPhotoPlay,
            RedumpSystem.IncredibleTechnologiesEagle,
            RedumpSystem.KonamieAmusement,
            RedumpSystem.KonamiFireBeat,
            RedumpSystem.KonamiM2,
            RedumpSystem.KonamiSystem573,
            RedumpSystem.KonamiSystemGV,
            RedumpSystem.NamcoSegaNintendoTriforce,
            RedumpSystem.NamcoSystem246256,
            RedumpSystem.PanasonicM2,
            RedumpSystem.SegaChihiro,
            RedumpSystem.SegaLindbergh,
            RedumpSystem.SegaNaomi,
            RedumpSystem.SegaNaomi2,
            RedumpSystem.SegaRingEdge,
            RedumpSystem.SegaRingEdge2,
            RedumpSystem.TABAustriaQuizard,

            // Other
            RedumpSystem.AudioCD,
            RedumpSystem.BDVideo,
            RedumpSystem.DVDVideo,
            RedumpSystem.HDDVDVideo,
            RedumpSystem.NavisoftNaviken21,
            RedumpSystem.PalmOS,
            RedumpSystem.PhotoCD,
            RedumpSystem.PlayStationGameSharkUpdates,
            RedumpSystem.PocketPC,
            RedumpSystem.SegaPrologue21MultimediaKaraokeSystem,
            RedumpSystem.TomyKissSite,
            RedumpSystem.VideoCD,
        ];

        /// <summary>
        /// RedumpSystem values that have decrypted keys
        /// </summary>
        private static readonly RedumpSystem?[] _systemsWithDKeys =
        [
            RedumpSystem.SonyPlayStation3,
        ];

        /// <summary>
        /// RedumpSystem values that have GDI packs
        /// </summary>
        private static readonly RedumpSystem?[] _systemsWithGdis =
        [
            // Disc-Based Consoles
            RedumpSystem.SegaDreamcast,

            // Arcade
            RedumpSystem.NamcoSegaNintendoTriforce,
            RedumpSystem.SegaChihiro,
            RedumpSystem.SegaNaomi,
            RedumpSystem.SegaNaomi2,
        ];

        /// <summary>
        /// RedumpSystem values that have keys
        /// </summary>
        private static readonly RedumpSystem?[] _systemsWithKeys =
        [
            RedumpSystem.NintendoWiiU,
            RedumpSystem.SonyPlayStation3,
        ];

        /// <summary>
        /// RedumpSystem values that have LSD packs
        /// </summary>
        private static readonly RedumpSystem?[] _systemsWithLsds =
        [
            // Disc-Based Consoles
            RedumpSystem.SonyPlayStation,

            // Computers
            RedumpSystem.AppleMacintosh,
            RedumpSystem.IBMPCcompatible,
        ];

        /// <summary>
        /// RedumpSystem values that have SBI packs
        /// </summary>
        private static readonly RedumpSystem?[] _systemsWithSbis =
        [
            // Disc-Based Consoles
            RedumpSystem.SonyPlayStation,

            // Computers
            RedumpSystem.AppleMacintosh,
            RedumpSystem.IBMPCcompatible,
        ];

        /// <summary>
        /// RedumpSystem values that are considered detected by Windows
        /// </summary>
        private static readonly RedumpSystem?[] _windowsDetectedSystems =
        [
            // Disc-Based Consoles
            RedumpSystem.CommodoreAmigaCD32,
            RedumpSystem.CommodoreAmigaCDTV,
            RedumpSystem.EnvizionsEVOSmartConsole,
            RedumpSystem.FujitsuFMTownsMarty,
            RedumpSystem.HasbroiONEducationalGamingSystem,
            RedumpSystem.MattelFisherPriceiXL,
            RedumpSystem.MattelHyperScan,
            RedumpSystem.MemorexVisualInformationSystem,
            RedumpSystem.MicrosoftXbox,
            RedumpSystem.MicrosoftXbox360,
            RedumpSystem.MicrosoftXboxOne,
            RedumpSystem.MicrosoftXboxSeriesXS,
            RedumpSystem.NECPCEngineCDTurboGrafxCD,
            RedumpSystem.NECPCFXPCFXGA,
            RedumpSystem.NintendoSonySuperNESCDROMSystem,
            RedumpSystem.PlaymajiPolymega,
            RedumpSystem.SegaDreamcast,
            RedumpSystem.SegaMegaCDSegaCD,
            RedumpSystem.SegaSaturn,
            RedumpSystem.SNKNeoGeoCD,
            RedumpSystem.SonyPlayStation,
            RedumpSystem.SonyPlayStation2,
            RedumpSystem.SonyPlayStation3,
            RedumpSystem.SonyPlayStation4,
            RedumpSystem.SonyPlayStation5,
            RedumpSystem.SonyPlayStationPortable,
            RedumpSystem.VMLabsNUON,
            RedumpSystem.VTechVFlashVSmilePro,
            RedumpSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem,

            // Computers
            RedumpSystem.AcornArchimedes,
            RedumpSystem.CommodoreAmigaCD,
            RedumpSystem.FujitsuFMTownsseries,
            RedumpSystem.IBMPCcompatible,
            RedumpSystem.NECPC88series,
            RedumpSystem.NECPC98series,
            RedumpSystem.SharpX68000,

            // Arcade
            RedumpSystem.AmigaCUBOCD32,
            RedumpSystem.Atronic,
            RedumpSystem.AUSCOMSystem1,
            RedumpSystem.BallyGameMagic,
            RedumpSystem.CapcomCPSystemIII,
            RedumpSystem.funworldPhotoPlay,
            RedumpSystem.FuRyuOmronPurikura,
            RedumpSystem.GlobalVRVarious,
            RedumpSystem.GlobalVRVortek,
            RedumpSystem.GlobalVRVortekV3,
            RedumpSystem.ICEPCHardware,
            RedumpSystem.IncredibleTechnologiesEagle,
            RedumpSystem.IncredibleTechnologiesVarious,
            RedumpSystem.JVLiTouch,
            RedumpSystem.KonamieAmusement,
            RedumpSystem.KonamiFireBeat,
            RedumpSystem.KonamiM2,
            RedumpSystem.KonamiPython,
            RedumpSystem.KonamiPython2,
            RedumpSystem.KonamiSystem573,
            RedumpSystem.KonamiSystemGV,
            RedumpSystem.KonamiTwinkle,
            RedumpSystem.KonamiVarious,
            RedumpSystem.MeritIndustriesBoardwalk,
            RedumpSystem.MeritIndustriesMegaTouchForce,
            RedumpSystem.MeritIndustriesMegaTouchION,
            RedumpSystem.MeritIndustriesMegaTouchMaxx,
            RedumpSystem.MeritIndustriesMegaTouchXL,
            RedumpSystem.NamcoPurikura,
            RedumpSystem.NamcoSegaNintendoTriforce,
            RedumpSystem.NamcoSystem12,
            RedumpSystem.NamcoSystem246256,
            RedumpSystem.NichibutsuHighRateSystem,
            RedumpSystem.NichibutsuSuperCD,
            RedumpSystem.NichibutsuXRateSystem,
            RedumpSystem.PhotoPlayVarious,
            RedumpSystem.RawThrillsVarious,
            RedumpSystem.SegaALLS,
            RedumpSystem.SegaChihiro,
            RedumpSystem.SegaEuropaR,
            RedumpSystem.SegaLindbergh,
            RedumpSystem.SegaNaomi,
            RedumpSystem.SegaNaomi2,
            RedumpSystem.SegaNaomiSatelliteTerminalPC,
            RedumpSystem.SegaNu,
            RedumpSystem.SegaNu11,
            RedumpSystem.SegaNu2,
            RedumpSystem.SegaNuSX,
            RedumpSystem.SegaRingEdge,
            RedumpSystem.SegaRingEdge2,
            RedumpSystem.SegaRingWide,
            RedumpSystem.SegaSystem32,
            RedumpSystem.SegaTitanVideo,
            RedumpSystem.SeibuCATSSystem,
            RedumpSystem.TABAustriaQuizard,
            RedumpSystem.TsunamiTsuMoMultiGameMotionSystem,
            RedumpSystem.UltraCade,

            // Other
            RedumpSystem.AudioCD,
            RedumpSystem.BDVideo,
            RedumpSystem.DVDAudio,
            RedumpSystem.DVDVideo,
            RedumpSystem.EnhancedCD,
            RedumpSystem.HDDVDVideo,
            RedumpSystem.NavisoftNaviken21,
            RedumpSystem.PalmOS,
            RedumpSystem.PhotoCD,
            RedumpSystem.PocketPC,
            RedumpSystem.Psion,
            RedumpSystem.RainbowDisc,
            RedumpSystem.SharpZaurus,
            RedumpSystem.SegaPrologue21MultimediaKaraokeSystem,
            RedumpSystem.SonyElectronicBook,
            RedumpSystem.TaoiKTV,
            RedumpSystem.TomyKissSite,
            RedumpSystem.VideoCD,
        ];

        /// <summary>
        /// RedumpSystem values that are considered XGD
        /// </summary>
        private static readonly RedumpSystem?[] _xgdSystems =
        [
            RedumpSystem.MicrosoftXbox,
            RedumpSystem.MicrosoftXbox360,
            RedumpSystem.MicrosoftXboxOne,
            RedumpSystem.MicrosoftXboxSeriesXS,
        ];

        /// <summary>
        /// Check that all systems detected by Windows are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateWindowsDetectedSystemsTestData))]
        public void RedumpSystem_DetectedByWindows(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.DetectedByWindows();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with reversed ringcodes are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateReversedRingcodeSystemsTestData))]
        public void RedumpSystem_HasReversedRingcodes(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.HasReversedRingcodes();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all audio systems are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateAudioSystemsTestData))]
        public void RedumpSystem_IsAudio(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.IsAudio();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all marker systems are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateMarkerSystemsTestData))]
        public void RedumpSystem_IsMarker(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.IsMarker();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all XGD systems are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateXGDSystemsTestData))]
        public void RedumpSystem_IsXGD(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.IsXGD();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RedumpSystem_ListRedumpSystem()
        {
            var actual = Extensions.ListSystems();
            Assert.NotEmpty(actual);
        }

        /// <summary>
        /// Check that every RedumpSystem has a long name provided
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateRedumpSystemTestData))]
        public void RedumpSystem_LongName(RedumpSystem? redumpSystem, bool expectNull)
        {
            var actual = redumpSystem.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        // TODO: Re-enable the following test once non-Redump systems are accounted for

        /// <summary>
        /// Check that every RedumpSystem has a short name provided
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        // [Theory]
        // [MemberData(nameof(GenerateRedumpSystemTestData))]
        // public void RedumpSystem_ShortName(RedumpSystem? redumpSystem, bool expectNull)
        // {
        //    string? actual = redumpSystem.ShortName();

        //    if (expectNull)
        //        Assert.Null(actual);
        //    else
        //        Assert.NotNull(actual);
        // }

        /// <summary>
        /// Check that all systems are mapped to a category
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateCategoriesSystemTestData))]
        public void RedumpSystem_GetCategory(RedumpSystem? redumpSystem, SystemCategory expected)
        {
            SystemCategory actual = redumpSystem.GetCategory();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all available systems are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateAvailableSystemsTestData))]
        public void RedumpSystem_IsAvailable(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.IsAvailable();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all banned systems are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateBannedSystemsTestData))]
        public void RedumpSystem_IsBanned(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.IsBanned();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with cuesheets are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateCuesheetSystemsTestData))]
        public void RedumpSystem_HasCues(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.HasCues();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with DATs are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateDatSystemsTestData))]
        public void RedumpSystem_HasDat(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.HasDat();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with keys are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateKeySystemsTestData))]
        public void RedumpSystem_HasKeys(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.HasKeys();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all systems with SBIs are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateSbiSystemsTestData))]
        public void RedumpSystem_HasSbi(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.HasSbi();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that every RedumpSystem can be mapped from a string
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateRedumpSystemTestData))]
        public void RedumpSystem_ToRedumpSystem(RedumpSystem? redumpSystem, bool expectNull)
        {
            string? longName = redumpSystem.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);

            var actualNormal = longName.ToRedumpSystem();
            var actualSpaceless = longNameSpaceless.ToRedumpSystem();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(redumpSystem, actualNormal);
                Assert.Equal(redumpSystem, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateRedumpSystemTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, true } };
            foreach (RedumpSystem? redumpSystem in Enum.GetValues<RedumpSystem>().Cast<RedumpSystem?>())
            {
                // We want to skip all markers for this
                if (redumpSystem.IsMarker())
                    continue;

                testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are considered Audio
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateAudioSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_audioSystems.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are available
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateAvailableSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_availableSystems.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are banned
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateBannedSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_bannedSystems.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values mapped to their categories
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, SystemCategory> GenerateCategoriesSystemTestData()
        {
            var testData = new TheoryData<RedumpSystem?, SystemCategory>() { { null, SystemCategory.NONE } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                testData.Add(redumpSystem, _systemCategoryMap[redumpSystem]);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that have cuesheets
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateCuesheetSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_systemsWithCues.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that have DATs
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateDatSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_systemsWithDats.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that have decrypted keys
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateDKeySystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_systemsWithDKeys.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that have GDIs
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateGdiSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_systemsWithGdis.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that have keys
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateKeySystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_systemsWithKeys.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that have LSDs
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateLsdSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_systemsWithLsds.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are considered markers
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateMarkerSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_markerSystems.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are considered markers
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateReversedRingcodeSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_reverseRingcodeSystems.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that have SBIs
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateSbiSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_systemsWithSbis.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are detected by Windows
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateWindowsDetectedSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_windowsDetectedSystems.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are considered XGD
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static TheoryData<RedumpSystem?, bool> GenerateXGDSystemsTestData()
        {
            var testData = new TheoryData<RedumpSystem?, bool>() { { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues<RedumpSystem>())
            {
                if (_xgdSystems.Contains(redumpSystem))
                    testData.Add(redumpSystem, true);
                else
                    testData.Add(redumpSystem, false);
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
