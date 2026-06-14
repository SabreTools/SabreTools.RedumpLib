using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.Data
{
    public class ExtensionsTests
    {
        #region Cross-Enumeration

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
            PhysicalSystem.DVDAudio,
            PhysicalSystem.HasbroiONEducationalGamingSystem,
            PhysicalSystem.HasbroVideoNow,
            PhysicalSystem.HasbroVideoNowColor,
            PhysicalSystem.HasbroVideoNowJr,
            PhysicalSystem.HasbroVideoNowXP,
            PhysicalSystem.PlayStationGameSharkUpdates,
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
            PhysicalSystem.BandaiPippin,
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
            PhysicalSystem.AcornArchimedes,
            PhysicalSystem.AppleMacintosh,
            PhysicalSystem.CommodoreAmigaCD,
            PhysicalSystem.FujitsuFMTownsseries,
            PhysicalSystem.IBMPCcompatible,
            PhysicalSystem.NECPC88series,
            PhysicalSystem.NECPC98series,
            PhysicalSystem.SharpX68000,

            // Arcade
            PhysicalSystem.funworldPhotoPlay,
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
            PhysicalSystem.DVDVideo,
            PhysicalSystem.EnhancedCD,
            PhysicalSystem.HDDVDVideo,
            PhysicalSystem.NavisoftNaviken21,
            PhysicalSystem.PalmOS,
            PhysicalSystem.PhotoCD,
            PhysicalSystem.PlayStationGameSharkUpdates,
            PhysicalSystem.PocketPC,
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
            PhysicalSystem.NavisoftNaviken21,
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
            [PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem] = SystemCategory.DiscBasedConsole,
            [PhysicalSystem.BandaiPippin] = SystemCategory.DiscBasedConsole,
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
            [PhysicalSystem.AcornArchimedes] = SystemCategory.Computer,
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
            [PhysicalSystem.funworldPhotoPlay] = SystemCategory.Arcade,
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
            [PhysicalSystem.DVDAudio] = SystemCategory.Other,
            [PhysicalSystem.DVDVideo] = SystemCategory.Other,
            [PhysicalSystem.EnhancedCD] = SystemCategory.Other,
            [PhysicalSystem.HDDVDVideo] = SystemCategory.Other,
            [PhysicalSystem.NavisoftNaviken21] = SystemCategory.Other,
            [PhysicalSystem.PalmOS] = SystemCategory.Other,
            [PhysicalSystem.PhotoCD] = SystemCategory.Other,
            [PhysicalSystem.PlayStationGameSharkUpdates] = SystemCategory.Other,
            [PhysicalSystem.PocketPC] = SystemCategory.Other,
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
            PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem,
            PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem,
            PhysicalSystem.BandaiPippin,
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
            PhysicalSystem.AcornArchimedes,
            PhysicalSystem.AppleMacintosh,
            PhysicalSystem.CommodoreAmigaCD,
            PhysicalSystem.FujitsuFMTownsseries,
            PhysicalSystem.IBMPCcompatible,
            PhysicalSystem.NECPC88series,
            PhysicalSystem.NECPC98series,
            PhysicalSystem.SharpX68000,

            // Arcade
            PhysicalSystem.funworldPhotoPlay,
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
            PhysicalSystem.NavisoftNaviken21,
            PhysicalSystem.PalmOS,
            PhysicalSystem.PhotoCD,
            PhysicalSystem.PlayStationGameSharkUpdates,
            PhysicalSystem.PocketPC,
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
            PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem,
            PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem,
            PhysicalSystem.BandaiPippin,
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
            PhysicalSystem.AcornArchimedes,
            PhysicalSystem.AppleMacintosh,
            PhysicalSystem.CommodoreAmigaCD,
            PhysicalSystem.FujitsuFMTownsseries,
            PhysicalSystem.IBMPCcompatible,
            PhysicalSystem.NECPC88series,
            PhysicalSystem.NECPC98series,
            PhysicalSystem.SharpX68000,

            // Arcade
            PhysicalSystem.funworldPhotoPlay,
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
            PhysicalSystem.DVDVideo,
            PhysicalSystem.HDDVDVideo,
            PhysicalSystem.NavisoftNaviken21,
            PhysicalSystem.PalmOS,
            PhysicalSystem.PhotoCD,
            PhysicalSystem.PlayStationGameSharkUpdates,
            PhysicalSystem.PocketPC,
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
            PhysicalSystem.AcornArchimedes,
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
            PhysicalSystem.funworldPhotoPlay,
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
            PhysicalSystem.NavisoftNaviken21,
            PhysicalSystem.PalmOS,
            PhysicalSystem.PhotoCD,
            PhysicalSystem.PocketPC,
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
    }
}
