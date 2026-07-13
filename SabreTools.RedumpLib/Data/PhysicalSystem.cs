using System.Collections.Generic;

namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// Represents a single physical system type
    /// </summary>
    public sealed class PhysicalSystem
    {
        #region Properties

        /// <summary>
        /// Human-readable name of the system
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Category for the system
        /// </summary>
        public readonly SystemCategory Category;

        /// <summary>
        /// System is included in the site
        /// </summary>
        public readonly bool Available;

        /// <summary>
        /// Site short code, if it exists
        /// </summary>
        public readonly string? Code;

        /// <summary>
        /// System has a CUE pack
        /// </summary>
        public readonly bool HasCues;

        /// <summary>
        /// System has a DAT
        /// </summary>
        public readonly bool HasDat;

        /// <summary>
        /// System has an SBI pack
        /// </summary>
        public readonly bool HasSbi;

        /// <summary>
        /// List of valid media types for the system
        /// </summary>
        /// <remarks>Order of types is used by some implementing projects</remarks>
        public readonly List<PhysicalMediaType?> MediaTypes;

        /// <summary>
        /// Indicates if this is a marker item
        /// </summary>
        /// <remarks>Internal use only</remarks>
        /// TODO: Remove this eventually
        public bool IsMarker { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private PhysicalSystem(string name,
            SystemCategory category,
            bool available = true,
            string? code = null,
            bool hasCues = false,
            bool hasDat = false,
            bool hasSbi = false,
            List<PhysicalMediaType?>? mediaTypes = null)
        {
            Name = name;
            Category = category;

            Available = available;
            Code = code;
            HasCues = hasCues;
            HasDat = hasDat;
            HasSbi = hasSbi;
            MediaTypes = mediaTypes ?? [PhysicalMediaType.NONE];
        }

        #endregion

        #region Static Instances

        #region BIOS Sets

        public static readonly PhysicalSystem MicrosoftXboxBIOS = new("Microsoft Xbox (BIOS)",
            SystemCategory.NONE,
            code: "xbox-bios",
            hasDat: true);

        public static readonly PhysicalSystem NintendoGameCubeBIOS = new("Nintendo GameCube (BIOS)",
            SystemCategory.NONE,
            code: "gc-bios",
            hasDat: true);

        public static readonly PhysicalSystem SonyPlayStationBIOS = new("Sony PlayStation (BIOS)",
            SystemCategory.NONE,
            code: "psx-bios",
            hasDat: true);

        public static readonly PhysicalSystem SonyPlayStation2BIOS = new("Sony PlayStation 2 (BIOS)",
            SystemCategory.NONE,
            code: "ps2-bios",
            hasDat: true);

        #endregion

        #region Disc-Based Consoles

        // https://en.wikipedia.org/wiki/Apple_Bandai_Pippin
        public static readonly PhysicalSystem AppleBandaiPippin = new("Apple/Bandai Pippin",
            SystemCategory.DiscBasedConsole,
            code: "PIPPIN",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Atari_Jaguar_CD
        public static readonly PhysicalSystem AtariJaguarCDInteractiveMultimediaSystem = new("Atari Jaguar CD Interactive Multimedia System",
            SystemCategory.DiscBasedConsole,
            code: "AJCD",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Playdia
        public static readonly PhysicalSystem BandaiPlaydiaQuickInteractiveSystem = new("Bandai Playdia Quick Interactive System",
            SystemCategory.DiscBasedConsole,
            code: "QIS",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Amiga_CD32
        public static readonly PhysicalSystem CommodoreAmigaCD32 = new("Commodore Amiga CD32",
            SystemCategory.DiscBasedConsole,
            code: "CD32",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Commodore_CDTV
        public static readonly PhysicalSystem CommodoreAmigaCDTV = new("Commodore Amiga CDTV",
            SystemCategory.DiscBasedConsole,
            code: "CDTV",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/EVO_Smart_Console
        public static readonly PhysicalSystem EnvizionsEVOSmartConsole = new("Envizions EVO Smart Console",
            SystemCategory.DiscBasedConsole,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/FM_Towns_Marty
        public static readonly PhysicalSystem FujitsuFMTownsMarty = new("Fujitsu FM Towns Marty",
            SystemCategory.DiscBasedConsole,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.FloppyDisk]);

        // http://videogamekraken.com/ion-educational-gaming-system-by-hasbro
        public static readonly PhysicalSystem HasbroiONEducationalGamingSystem = new("Hasbro iON Educational Gaming System",
            SystemCategory.DiscBasedConsole,
            code: "ION",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/VideoNow
        public static readonly PhysicalSystem HasbroVideoNow = new("Hasbro VideoNow",
            SystemCategory.DiscBasedConsole,
            code: "HVN",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/VideoNow
        public static readonly PhysicalSystem HasbroVideoNowColor = new("Hasbro VideoNow Color",
            SystemCategory.DiscBasedConsole,
            code: "HVNC",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/VideoNow
        public static readonly PhysicalSystem HasbroVideoNowJr = new("Hasbro VideoNow Jr.",
            SystemCategory.DiscBasedConsole,
            code: "HVNJR",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/VideoNow
        public static readonly PhysicalSystem HasbroVideoNowXP = new("Hasbro VideoNow XP",
            SystemCategory.DiscBasedConsole,
            code: "HVNXP",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://m.service.mattel.com/us/Technical/TechnicalProductDetail?prodno=R9703&siteid=27&catid=520&from=supportlanding
        public static readonly PhysicalSystem MattelFisherPriceiXL = new("Mattel Fisher-Price iXL",
            SystemCategory.DiscBasedConsole,
            code: "IXL",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/HyperScan
        public static readonly PhysicalSystem MattelHyperScan = new("Mattel HyperScan",
            SystemCategory.DiscBasedConsole,
            code: "HS",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Tandy_Video_Information_System
        public static readonly PhysicalSystem MemorexVisualInformationSystem = new("Memorex Visual Information System",
            SystemCategory.DiscBasedConsole,
            code: "VIS",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Xbox_(console)
        public static readonly PhysicalSystem MicrosoftXbox = new("Microsoft Xbox",
            SystemCategory.DiscBasedConsole,
            code: "XBOX",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.DVD, PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Xbox_360
        public static readonly PhysicalSystem MicrosoftXbox360 = new("Microsoft Xbox 360",
            SystemCategory.DiscBasedConsole,
            code: "XBOX360",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.DVD, PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Xbox_One
        public static readonly PhysicalSystem MicrosoftXboxOne = new("Microsoft Xbox One",
            SystemCategory.DiscBasedConsole,
            code: "XBOXONE",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.BluRay]);

        // https://en.wikipedia.org/wiki/Xbox_Series_X_and_Series_S
        public static readonly PhysicalSystem MicrosoftXboxSeriesXS = new("Microsoft Xbox Series X",
            SystemCategory.DiscBasedConsole,
            code: "XBOXSX",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.BluRay]);

        // https://en.wikipedia.org/wiki/TurboGrafx-16
        public static readonly PhysicalSystem NECPCEngineCDTurboGrafxCD = new("NEC PC Engine CD & TurboGrafx CD",
            SystemCategory.DiscBasedConsole,
            code: "PCE",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/PC-FX
        public static readonly PhysicalSystem NECPCFXPCFXGA = new("NEC PC-FX & PC-FXGA",
            SystemCategory.DiscBasedConsole,
            code: "PC-FX",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/GameCube
        public static readonly PhysicalSystem NintendoGameCube = new("Nintendo GameCube",
            SystemCategory.DiscBasedConsole,
            code: "GC",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.DVD, PhysicalMediaType.NintendoGameCubeGameDisc]);

        // https://en.wikipedia.org/wiki/Super_NES_CD-ROM
        public static readonly PhysicalSystem NintendoSonySuperNESCDROMSystem = new("Nintendo-Sony Super NES CD-ROM System",
            SystemCategory.DiscBasedConsole,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Wii
        public static readonly PhysicalSystem NintendoWii = new("Nintendo Wii",
            SystemCategory.DiscBasedConsole,
            code: "WII",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.DVD, PhysicalMediaType.NintendoWiiOpticalDisc]);

        // https://en.wikipedia.org/wiki/Wii_U
        public static readonly PhysicalSystem NintendoWiiU = new("Nintendo Wii U",
            SystemCategory.DiscBasedConsole,
            code: "WIIU",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.BluRay, PhysicalMediaType.NintendoWiiUOpticalDisc]);

        // https://en.wikipedia.org/wiki/3DO_Interactive_Multiplayer
        public static readonly PhysicalSystem Panasonic3DOInteractiveMultiplayer = new("3DO Interactive Multiplayer",
            SystemCategory.DiscBasedConsole,
            code: "3DO",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Philips_CD-i
        public static readonly PhysicalSystem PhilipsCDi = new("Philips CD-i",
            SystemCategory.DiscBasedConsole,
            code: "CDI",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Polymega
        public static readonly PhysicalSystem PlaymajiPolymega = new("Playmaji Polymega",
            SystemCategory.DiscBasedConsole,
            code: "POLYMEGA",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/LaserActive
        public static readonly PhysicalSystem PioneerLaserActive = new("Pioneer LaserActive",
            SystemCategory.DiscBasedConsole,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.LaserDisc]);

        // https://en.wikipedia.org/wiki/Dreamcast
        public static readonly PhysicalSystem SegaDreamcast = new("Sega Dreamcast",
            SystemCategory.DiscBasedConsole,
            code: "DC",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.GDROM]);

        // https://en.wikipedia.org/wiki/Sega_CD
        public static readonly PhysicalSystem SegaMegaCDSegaCD = new("Sega Mega CD & Sega CD",
            SystemCategory.DiscBasedConsole,
            code: "MCD",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Sega_Saturn
        public static readonly PhysicalSystem SegaSaturn = new("Sega Saturn",
            SystemCategory.DiscBasedConsole,
            code: "SS",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Neo_Geo_CD
        public static readonly PhysicalSystem SNKNeoGeoCD = new("Neo Geo CD",
            SystemCategory.DiscBasedConsole,
            code: "NGCD",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/PlayStation_(console)
        public static readonly PhysicalSystem SonyPlayStation = new("Sony PlayStation",
            SystemCategory.DiscBasedConsole,
            code: "PSX",
            hasCues: true,
            hasDat: true,
            hasSbi: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/PlayStation_2
        public static readonly PhysicalSystem SonyPlayStation2 = new("Sony PlayStation 2",
            SystemCategory.DiscBasedConsole,
            code: "PS2",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/PlayStation_3
        public static readonly PhysicalSystem SonyPlayStation3 = new("Sony PlayStation 3",
            SystemCategory.DiscBasedConsole,
            code: "PS3",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.BluRay, PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/PlayStation_4
        public static readonly PhysicalSystem SonyPlayStation4 = new("Sony PlayStation 4",
            SystemCategory.DiscBasedConsole,
            code: "PS4",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.BluRay]);

        // https://en.wikipedia.org/wiki/PlayStation_5
        public static readonly PhysicalSystem SonyPlayStation5 = new("Sony PlayStation 5",
            SystemCategory.DiscBasedConsole,
            code: "PS5",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.BluRay]);

        // https://en.wikipedia.org/wiki/PlayStation_Portable
        public static readonly PhysicalSystem SonyPlayStationPortable = new("Sony PlayStation Portable",
            SystemCategory.DiscBasedConsole,
            code: "PSP",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.UMD, PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/Nuon_(DVD_technology)
        public static readonly PhysicalSystem VMLabsNUON = new("VM Labs NUON",
            SystemCategory.DiscBasedConsole,
            code: "NUON",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/V.Flash
        public static readonly PhysicalSystem VTechVFlashVSmilePro = new("VTech V.Flash & V.Smile Pro",
            SystemCategory.DiscBasedConsole,
            code: "VFLASH",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Game_Wave_Family_Entertainment_System
        public static readonly PhysicalSystem ZAPiTGamesGameWaveFamilyEntertainmentSystem = new("ZAPiT Games Game Wave Family Entertainment System",
            SystemCategory.DiscBasedConsole,
            code: "GAMEWAVE",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // End of console section delimiter
        public static readonly PhysicalSystem MarkerDiscBasedConsoleEnd = new("MarkerDiscBasedConsoleEnd", SystemCategory.NONE, available: false) { IsMarker = true };

        #endregion

        #region Cartridge-Based and Other Consoles

        /*
        public static readonly PhysicalSystem AmstradGX4000 = new("Amstrad GX-4000",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem APFMicrocomputerSystem = new("APF Microcomputer System",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem Atari2600VCS = new("Atari 2600 & VCS",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem Atari5200 = new("Atari 5200",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem Atari7800 = new("Atari 7800",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem AtariJaguar = new("Atari Jaguar",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem AtariXEGS = new("Atari XEGS",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem Audiosonic1292AdvancedProgrammableVideoSystem = new("Audiosonic 1292 Advanced Programmable Video System",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem BallyAstrocade = new("Bally Astrocade",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem BitCorporationDina = new("Bit Corporation Dina",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem CasioLoopy = new("Casio Loopy",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem CasioPV1000 = new("Casio PV-1000",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem Commodore64GamesSystem = new("Commodore 64 Games System",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem DaewooElectronicsZemmix = new("Daewoo Electronics Zemmix",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem EmersonArcadia2001 = new("Emerson Arcadia 2001",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem EpochCassetteVision = new("Epoch Cassette Vision",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem EpochSuperCassetteVision = new("Epoch Super Cassette Vision",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem FairchildChannelF = new("Fairchild Channel F",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem FuntechSuperACan = new("Funtech Super A'Can",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem GCEVectrex = new("GCE Vectrex",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem HeberBBCBridgeCompanion = new("Heber BBC Bridge Companion",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem IntertonVC4000 = new("Interton VC-4000",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem JungleTacVii = new("JungleTac Vii",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem LeapFrogClickStart = new("LeapFrog ClickStart",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem LJNVideoArt = new("LJN VideoArt",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem MagnavoxOdyssey2 = new("Magnavox Odyssey 2",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem MattelIntellivision = new("Mattel Intellivision",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem NECPCEngineTurboGrafx16 = new("NEC PC Engine & TurboGrafx-16",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem NichibutsuMyVision = new("Nichibutsu MyVision",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem Nintendo64 = new("Nintendo 64",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem Nintendo64DD = new("Nintendo 64DD",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem NintendoFamicomNintendoEntertainmentSystem = new("Nintendo Famicom & Nintendo Entertainment System",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem NintendoFamicomDiskSystem = new("Nintendo Famicom Disk System",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem NintendoSuperFamicomSuperNintendoEntertainmentSystem = new("Nintendo Super Famicom & Super Nintendo Entertainment System",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem NintendoSwitch = new("Nintendo Switch",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem PhilipsVideopacPlusG7400 = new("Philips Videopac+ & G7400",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem RCAStudioII = new("RCA Studio-II",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem Sega32X = new("Sega 32X",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem SegaMarkIIIMasterSystem = new("Sega Mark III & Master System",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem SegaMegaDriveGenesis = new("Sega MegaDrive & Genesis",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem SegaSG1000 = new("Sega SG-1000",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem SNKNeoGeo = new("SNK NeoGeo",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem SSDCOMPANYLIMITEDXaviXPORT = new("SSD COMPANY LIMITED XaviXPORT",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem ViewMasterInteractiveVision = new("ViewMaster Interactive Vision",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem VTechCreatiVision = new("V.Tech CreatiVision",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem VTechVSmile = new("V.Tech V.Smile",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem VTechSocrates = new("V.Tech Socrates",
            SystemCategory.OtherConsole,
            available: false);

        public static readonly PhysicalSystem WorldsOfWonderActionMax = new("Worlds of Wonder ActionMax",
            SystemCategory.OtherConsole,
            available: false);

        // End of other console delimiter
        public static readonly PhysicalSystem MarkerOtherConsoleEnd = new("MarkerOtherConsoleEnd", SystemCategory.NONE, available: false) { IsMarker = true };
        */

        #endregion

        #region Computers

        // https://en.wikipedia.org/wiki/Acorn_Archimedes
        public static readonly PhysicalSystem AcornArchimedesAndRiscPC = new("Acorn Archimedes & Risc PC",
            SystemCategory.Computer,
            code: "ARCH",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/Macintosh
        public static readonly PhysicalSystem AppleMacintosh = new("Apple Macintosh",
            SystemCategory.Computer,
            code: "MAC",
            hasCues: true,
            hasDat: true,
            hasSbi: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD, PhysicalMediaType.FloppyDisk, PhysicalMediaType.HardDisk]);

        // https://en.wikipedia.org/wiki/Atari_ST
        public static readonly PhysicalSystem AtariSTSeries = new("Atari ST series",
            SystemCategory.Computer,
            code: "ATARIST",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/Commodore_64
        public static readonly PhysicalSystem Commodore64 = new("Commodore 64",
            SystemCategory.Computer,
            code: "C64",
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/Amiga
        public static readonly PhysicalSystem CommodoreAmigaCD = new("Commodore Amiga CD",
            SystemCategory.Computer,
            code: "ACD",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/FM_Towns
        public static readonly PhysicalSystem FujitsuFMTownsSeries = new("Fujitsu FM Towns series",
            SystemCategory.Computer,
            code: "FMT",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/IBM_PC_compatible
        public static readonly PhysicalSystem IBMPCcompatible = new("IBM PC compatible",
            SystemCategory.Computer,
            code: "PC",
            hasCues: true,
            hasDat: true,
            hasSbi: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD, PhysicalMediaType.BluRay, PhysicalMediaType.FloppyDisk, PhysicalMediaType.HardDisk, PhysicalMediaType.DataCartridge]);

        // https://en.wikipedia.org/wiki/MSX
        public static readonly PhysicalSystem MicrosoftMSX = new("Microsoft MSX",
            SystemCategory.Computer,
            code: "MSX",
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/PC-8800_series
        public static readonly PhysicalSystem NECPC88Series = new("NEC PC-88 series",
            SystemCategory.Computer,
            code: "PC-88",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/PC-9800_series
        public static readonly PhysicalSystem NECPC98Series = new("NEC PC-98 series",
            SystemCategory.Computer,
            code: "PC-98",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/SGI_Indigo
        public static readonly PhysicalSystem SGIIndigoSeries = new("SGI Indigo series",
            SystemCategory.Computer,
            code: "INDIGO",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/X68000
        public static readonly PhysicalSystem SharpX68000 = new("Sharp X68000",
            SystemCategory.Computer,
            code: "X68K",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/ZX_Spectrum
        public static readonly PhysicalSystem SinclairZXSpectrum = new("Sinclair ZX Spectrum",
            SystemCategory.Computer,
            code: "ZX-SPECTRUM",
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.FloppyDisk]);

        // https://en.wikipedia.org/wiki/Sun_Microsystems
        public static readonly PhysicalSystem SunMicrosystemsUltra = new("Sun Microsystems Ultra",
            SystemCategory.Computer,
            code: "ULTRA",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD, PhysicalMediaType.FloppyDisk]);

        // End of computer section delimiter
        public static readonly PhysicalSystem MarkerComputerEnd = new("MarkerComputerEnd", SystemCategory.NONE, available: false) { IsMarker = true };

        #endregion

        #region Arcade

        // https://en.wikipedia.org/wiki/Orbatak
        public static readonly PhysicalSystem AmericanLaserGames3DO = new("American Laser Games 3DO",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // http://system16.com/hardware.php?id=779
        public static readonly PhysicalSystem Atari3DO = new("Atari 3DO",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // http://newlifegames.net/nlg/index.php?topic=22003.0
        // http://newlifegames.net/nlg/index.php?topic=5486.msg119440
        public static readonly PhysicalSystem Atronic = new("Atronic",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://www.arcade-museum.com/members/member_detail.php?member_id=406530
        public static readonly PhysicalSystem AUSCOMSystem1 = new("AUSCOM System 1",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // http://newlifegames.net/nlg/index.php?topic=285.0
        public static readonly PhysicalSystem BallyGameMagic = new("Bally Game Magic",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/CP_System_III
        public static readonly PhysicalSystem CapcomPlaySystemIII = new("Capcom Play System III",
            SystemCategory.Arcade,
            code: "CPS3",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://www.bigbookofamigahardware.com/bboah/product.aspx?id=36
        // https://amiga.resource.cx/exp/cubo
        public static readonly PhysicalSystem CDExpressCuboCD32 = new("C.D. Express Cubo CD32",
            SystemCategory.Arcade,
            code: "CUBO",
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // UNKNOWN
        public static readonly PhysicalSystem FunworldPhotoPlay = new("Funworld Photo Play",
            SystemCategory.Arcade,
            code: "FPP",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/FuRyu
        public static readonly PhysicalSystem FuRyuOmronPurikura = new("FuRyu & Omron Purikura",
            SystemCategory.Arcade,
            code: "FPURI",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // UNKNOWN
        public static readonly PhysicalSystem GlobalVRVarious = new("Global VR PC-based Systems",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://service.globalvr.com/troubleshooting/vortek.html
        public static readonly PhysicalSystem GlobalVRVortek = new("Global VR Vortek",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://service.globalvr.com/downloads/v3/040-1001-01c-V3-System-Manual.pdf
        public static readonly PhysicalSystem GlobalVRVortekV3 = new("Global VR Vortek V3",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://www.icegame.com/games
        public static readonly PhysicalSystem ICEPCHardware = new("ICE PC-based Hardware",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://github.com/mamedev/mame/blob/master/src/mame/drivers/iteagle.cpp
        public static readonly PhysicalSystem IncredibleTechnologiesEagle = new("Incredible Technologies Eagle",
            SystemCategory.Arcade,
            code: "ITE",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // UNKNOWN
        public static readonly PhysicalSystem IncredibleTechnologiesVarious = new("Incredible Technologies PC-based Systems",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // UNKNOWN
        public static readonly PhysicalSystem JVLiTouch = new("JVL iTouch",
            SystemCategory.Arcade,
            code: "ITOUCH",
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/E-Amusement
        public static readonly PhysicalSystem KonamieAmusement = new("Konami e-Amusement",
            SystemCategory.Arcade,
            code: "KEA",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=828
        public static readonly PhysicalSystem KonamiFireBeat = new("Konami FireBeat",
            SystemCategory.Arcade,
            code: "KFB",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=575
        public static readonly PhysicalSystem KonamiM2 = new("Konami M2",
            SystemCategory.Arcade,
            code: "KM2",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=586
        // http://system16.com/hardware.php?id=977
        public static readonly PhysicalSystem KonamiPython = new("Konami Python",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=976
        // http://system16.com/hardware.php?id=831
        public static readonly PhysicalSystem KonamiPython2 = new("Konami Python 2",
            SystemCategory.Arcade,
            code: "KP2",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=582
        // http://system16.com/hardware.php?id=822
        // http://system16.com/hardware.php?id=823
        public static readonly PhysicalSystem KonamiSystem573 = new("Konami System 573",
            SystemCategory.Arcade,
            code: "KS573",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=577
        public static readonly PhysicalSystem KonamiSystemGV = new("Konami System GV",
            SystemCategory.Arcade,
            code: "KSGV",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=827
        public static readonly PhysicalSystem KonamiTwinkle = new("Konami Twinkle",
            SystemCategory.Arcade,
            code: "KT",
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // UNKNOWN
        public static readonly PhysicalSystem KonamiVarious = new("Konami PC-based Systems",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://www.meritgames.com/Support_Center/manuals/PM0591-01.pdf
        public static readonly PhysicalSystem MeritIndustriesBoardwalk = new("Merit Industries Boardwalk",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // TODO: Combine all MegaTouch systems?

        // http://www.meritgames.com/Support_Center/Force%20Elite/PM0380-09.pdf
        // http://www.meritgames.com/Support_Center/Force%20Upright/PM0382-07%20FORCE%20Upright%20manual.pdf
        // http://www.meritgames.com/Support_Center/Force%20Upright/PM0383-07%20FORCE%20Upright%20manual.pdf
        public static readonly PhysicalSystem MeritIndustriesMegaTouchForce = new("Merit Industries MegaTouch Force",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://www.meritgames.com/Service%20Center/Ion%20Troubleshooting.pdf
        public static readonly PhysicalSystem MeritIndustriesMegaTouchION = new("Merit Industries MegaTouch ION",
            SystemCategory.Arcade,
            code: "MION",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://www.meritgames.com/Support_Center/EZ%20Maxx/Manuals/MAXX%20Elite%20with%20coin.pdf
        // http://www.meritgames.com/Support_Center/EZ%20Maxx/Manuals/MAXX%20Elite.pdf
        // http://www.meritgames.com/Support_Center/manuals/90003010%20Maxx%20TSM_Rev%20C.pdf
        public static readonly PhysicalSystem MeritIndustriesMegaTouchMaxx = new("Merit Industries MegaTouch Maxx",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://www.meritgames.com/Support_Center/manuals/pm0076_OA_Megatouch%20XL%20Trouble%20Shooting%20Manual.pdf
        // http://www.meritgames.com/Support_Center/MEGA%20XL/manuals/Megatouch_XL_pm0109-0D.pdf
        // http://www.meritgames.com/Support_Center/MEGA%20XL/manuals/Megatouch_XL_Super_5000_manual.pdf
        public static readonly PhysicalSystem MeritIndustriesMegaTouchXL = new("Merit Industries MegaTouch XL",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // UNKNOWN
        public static readonly PhysicalSystem NamcoPurikura = new("Namco Purikura",
            SystemCategory.Arcade,
            code: "NPURI",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=545
        public static readonly PhysicalSystem NamcoSegaNintendoTriforce = new("Namco · Sega · Nintendo Triforce",
            SystemCategory.Arcade,
            code: "TRF",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.GDROM]);

        // http://system16.com/hardware.php?id=535
        public static readonly PhysicalSystem NamcoSystem12 = new("Namco System 12",
            SystemCategory.Arcade,
            code: "NS12",
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://system16.com/hardware.php?id=537
        public static readonly PhysicalSystem NamcoSystem22 = new("Namco System 22",
            SystemCategory.Arcade,
            code: "NS22",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=543
        public static readonly PhysicalSystem NamcoSystem246 = new("Namco System 246",
            SystemCategory.Arcade,
            code: "NS246",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=546
        // http://system16.com/hardware.php?id=872
        public static readonly PhysicalSystem NamcoSystem256 = new("Namco System 256",
            SystemCategory.Arcade,
            code: "NS256",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://www.arcade-history.com/?n=the-yakyuuken-part-1&page=detail&id=33049
        public static readonly PhysicalSystem NewJatreCDi = new("New Jatre CD-i",
            SystemCategory.Arcade,
            code: "NJ-CDI",
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // http://blog.system11.org/?p=2499
        public static readonly PhysicalSystem NichibutsuHighRateSystem = new("Nichibutsu High Rate System",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // http://blog.system11.org/?p=2514
        public static readonly PhysicalSystem NichibutsuSuperCD = new("Nichibutsu Super CD",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // http://collectedit.com/collectors/shou-time-213/arcade-pcbs-281/x-rate-dvd-series-17-newlywed-life-japan-by-nichibutsu-32245
        public static readonly PhysicalSystem NichibutsuXRateSystem = new("Nichibutsu X-Rate System",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Panasonic_M2
        public static readonly PhysicalSystem PanasonicM2 = new("Panasonic M2",
            SystemCategory.Arcade,
            code: "M2",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // TODO: Investigate whether the various PC-based systems listed would wrap into this instead
        // If they wrap in, maybe make a note about what they used to be

        // UNKNOWN
        public static readonly PhysicalSystem PCBasedArcade = new("PC-Based Arcade",
            SystemCategory.Arcade,
            code: "PCARCADE",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://github.com/mamedev/mame/blob/master/src/mame/drivers/photoply.cpp
        public static readonly PhysicalSystem PhotoPlayVarious = new("PhotoPlay PC-based Systems",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // UNKNOWN
        public static readonly PhysicalSystem RawThrillsVarious = new("Raw Thrills PC-based Systems",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // UNKNOWN
        public static readonly PhysicalSystem SegaALLS = new("Sega ALLS",
            SystemCategory.Arcade,
            code: "ALLS",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=729
        public static readonly PhysicalSystem SegaChihiro = new("Sega Chihiro",
            SystemCategory.Arcade,
            code: "CHIHIRO",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.GDROM]);

        // https://www.system16.com/hardware.php?id=730
        public static readonly PhysicalSystem SegaChihiroSatelliteTerminalPC = new("Sega Chihiro Satellite Terminal PC",
            SystemCategory.Arcade,
            code: "CHIHIROPC",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=907
        public static readonly PhysicalSystem SegaEuropaR = new("Sega Europa-R",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=985
        // http://system16.com/hardware.php?id=731
        // http://system16.com/hardware.php?id=984
        // http://system16.com/hardware.php?id=986
        // TODO: Split if/when Yellow, Blue, Red, Red EX, and Silver are defined
        public static readonly PhysicalSystem SegaLindbergh = new("Sega Lindbergh",
            SystemCategory.Arcade,
            code: "LINDBERGH",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // UNKNOWN
        public static readonly PhysicalSystem SegaLindberghSatelliteTerminalPC = new("Sega Lindbergh Satellite Terminal PC",
            SystemCategory.Arcade,
            code: "LINDPC",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=721
        // http://system16.com/hardware.php?id=723
        // http://system16.com/hardware.php?id=906
        // http://system16.com/hardware.php?id=722
        public static readonly PhysicalSystem SegaNaomi = new("Sega Naomi",
            SystemCategory.Arcade,
            code: "NAOMI",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.GDROM]);

        // http://system16.com/hardware.php?id=725
        // http://system16.com/hardware.php?id=726
        // http://system16.com/hardware.php?id=727
        public static readonly PhysicalSystem SegaNaomi2 = new("Sega Naomi 2",
            SystemCategory.Arcade,
            code: "NAOMI2",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.GDROM]);

        // https://segaretro.org/Sega_NAOMI#NAOMI_Satellite_Terminal
        public static readonly PhysicalSystem SegaNaomiSatelliteTerminalPC = new("Sega Naomi Satellite Terminal PC",
            SystemCategory.Arcade,
            code: "NAOMIPC",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
        public static readonly PhysicalSystem SegaNu = new("Sega Nu",
            SystemCategory.Arcade,
            code: "NU",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
        public static readonly PhysicalSystem SegaNu11 = new("Sega Nu 1.1",
            SystemCategory.Arcade,
            code: "NU11",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
        public static readonly PhysicalSystem SegaNu2 = new("Sega Nu 2",
            SystemCategory.Arcade,
            code: "NU2",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
        public static readonly PhysicalSystem SegaNuSX = new("Sega Nu SX",
            SystemCategory.Arcade,
            code: "NUSX",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=910
        // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
        public static readonly PhysicalSystem SegaRingEdge = new("Sega RingEdge",
            SystemCategory.Arcade,
            code: "SRE",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=982
        // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
        public static readonly PhysicalSystem SegaRingEdge2 = new("Sega RingEdge 2",
            SystemCategory.Arcade,
            code: "SRE2",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=911
        // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
        public static readonly PhysicalSystem SegaRingWide = new("Sega RingWide",
            SystemCategory.Arcade,
            code: "SRW",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=709
        // http://system16.com/hardware.php?id=710
        public static readonly PhysicalSystem SegaSystem32 = new("Sega System 32",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // http://system16.com/hardware.php?id=711
        public static readonly PhysicalSystem SegaTitanVideo = new("Sega Titan Video",
            SystemCategory.Arcade,
            code: "stv",
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://github.com/mamedev/mame/blob/master/src/mame/drivers/seibucats.cpp
        public static readonly PhysicalSystem SeibuCATSSystem = new("Seibu CATS System",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://www.tab.at/en/support/support/downloads
        public static readonly PhysicalSystem TABAustriaQuizard = new("TAB-Austria Quizard",
            SystemCategory.Arcade,
            code: "QUIZARD",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://primetimeamusements.com/product/tsumo-multi-game-motion-system/
        // https://www.highwaygames.com/arcade-machines/tsumo-tsunami-motion-8117/
        public static readonly PhysicalSystem TsunamiTsuMoMultiGameMotionSystem = new("Tsunami TsuMo Multi-Game Motion System",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/UltraCade_Technologies
        public static readonly PhysicalSystem UltraCade = new("UltraCade PC-based Systems",
            SystemCategory.Arcade,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // End of arcade section delimiter
        public static readonly PhysicalSystem MarkerArcadeEnd = new("MarkerArcadeEnd", SystemCategory.NONE, available: false) { IsMarker = true };

        #endregion

        #region Other

        // https://en.wikipedia.org/wiki/Audio_CD
        public static readonly PhysicalSystem AudioCD = new("Audio CD",
            SystemCategory.Other,
            code: "AUDIO-CD",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Blu-ray#Player_profiles
        public static readonly PhysicalSystem BDVideo = new("BD-Video",
            SystemCategory.Other,
            code: "BD-VIDEO",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.BluRay]);

        // UNKNOWN
        public static readonly PhysicalSystem DatelPlayStationCheatDeviceUpdates = new("Datel PlayStation Cheat Device Updates",
            SystemCategory.Other,
            code: "PSXGS",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/DVD-Audio
        public static readonly PhysicalSystem DVDAudio = new("DVD-Audio",
            SystemCategory.Other,
            available: false,
            mediaTypes: [PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/DVD-Video
        public static readonly PhysicalSystem DVDVideo = new("DVD-Video",
            SystemCategory.Other,
            code: "DVD-VIDEO",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/Blue_Book_(CD_standard)
        public static readonly PhysicalSystem EnhancedCD = new("Enhanced CD",
            SystemCategory.Other,
            code: "ENHANCED-CD",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/HD_DVD
        public static readonly PhysicalSystem HDDVDVideo = new("HD DVD-Video",
            SystemCategory.Other,
            code: "HDDVD-VIDEO",
            hasDat: true,
            mediaTypes: [PhysicalMediaType.HDDVD]);

        // UNKNOWN
        public static readonly PhysicalSystem MicrosoftPocketPC = new("Microsoft Pocket PC",
            SystemCategory.Other,
            code: "PPC",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/Compressed_audio_optical_disc
        public static readonly PhysicalSystem MP3AudioDisc = new("MP3 Audio Disc",
            SystemCategory.Other,
            code: "MP3",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // UNKNOWN
        public static readonly PhysicalSystem NavisoftNaviken = new("Navisoft Naviken",
            SystemCategory.Other,
            code: "NAVI",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // UNKNOWN
        public static readonly PhysicalSystem PalmOS = new("Palm OS",
            SystemCategory.Other,
            code: "PALM",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://en.wikipedia.org/wiki/Photo_CD
        public static readonly PhysicalSystem PhotoCD = new("Photo CD",
            SystemCategory.Other,
            code: "PHOTO-CD",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // UNKNOWN
        public static readonly PhysicalSystem Psion = new("Psion",
            SystemCategory.Other,
            code: "PSION",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Doors_and_Windows_(EP)
        public static readonly PhysicalSystem RainbowDisc = new("Rainbow Disc",
            SystemCategory.Other,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://segaretro.org/Prologue_21
        public static readonly PhysicalSystem SegaPrologue21MultimediaKaraokeSystem = new("Sega Prologue 21 Multimedia Karaoke System",
            SystemCategory.Other,
            code: "SP21",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // UNKNOWN
        public static readonly PhysicalSystem SharpZaurus = new("Sharp Zaurus",
            SystemCategory.Other,
            code: "ZAURUS",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // UNKNOWN
        public static readonly PhysicalSystem SonyElectronicBook = new("Sony Electronic Book",
            SystemCategory.Other,
            code: "SEB",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Super_Audio_CD
        public static readonly PhysicalSystem SuperAudioCD = new("Super Audio CD",
            SystemCategory.Other,
            available: false,
            mediaTypes: [PhysicalMediaType.CDROM, PhysicalMediaType.DVD]);

        // https://www.cnet.com/products/tao-music-iktv-karaoke-station-karaoke-system-series/
        public static readonly PhysicalSystem TaoiKTV = new("Tao iKTV",
            SystemCategory.Other,
            code: "IKTV",
            mediaTypes: [PhysicalMediaType.CDROM]);

        // http://ultimateconsoledatabase.com/golden/kiss_site.htm
        public static readonly PhysicalSystem TomyKissSite = new("Tomy Kiss-Site",
            SystemCategory.Other,
            code: "KSITE",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // https://en.wikipedia.org/wiki/Video_CD
        public static readonly PhysicalSystem VideoCD = new("Video CD",
            SystemCategory.Other,
            code: "VCD",
            hasCues: true,
            hasDat: true,
            mediaTypes: [PhysicalMediaType.CDROM]);

        // End of other section delimiter
        public static readonly PhysicalSystem MarkerOtherEnd = new("MarkerOtherEnd", SystemCategory.NONE, available: false) { IsMarker = true };

        #endregion

        #endregion

        #region Static Collections

        /// <summary>
        /// All languages
        /// </summary>
        /// TODO: Figure out how to remove delimiters
        public static readonly PhysicalSystem[] AllSystems =
        [
            #region BIOS Sets

            MicrosoftXboxBIOS,
            NintendoGameCubeBIOS,
            SonyPlayStationBIOS,
            SonyPlayStation2BIOS,

            #endregion

            #region Disc-Based Consoles

            AppleBandaiPippin,
            AtariJaguarCDInteractiveMultimediaSystem,
            BandaiPlaydiaQuickInteractiveSystem,
            CommodoreAmigaCD32,
            CommodoreAmigaCDTV,
            EnvizionsEVOSmartConsole,
            FujitsuFMTownsMarty,
            HasbroiONEducationalGamingSystem,
            HasbroVideoNow,
            HasbroVideoNowColor,
            HasbroVideoNowJr,
            HasbroVideoNowXP,
            MattelFisherPriceiXL,
            MattelHyperScan,
            MemorexVisualInformationSystem,
            MicrosoftXbox,
            MicrosoftXbox360,
            MicrosoftXboxOne,
            MicrosoftXboxSeriesXS,
            NECPCEngineCDTurboGrafxCD,
            NECPCFXPCFXGA,
            NintendoGameCube,
            NintendoSonySuperNESCDROMSystem,
            NintendoWii,
            NintendoWiiU,
            Panasonic3DOInteractiveMultiplayer,
            PhilipsCDi,
            PlaymajiPolymega,
            PioneerLaserActive,
            SegaDreamcast,
            SegaMegaCDSegaCD,
            SegaSaturn,
            SNKNeoGeoCD,
            SonyPlayStation,
            SonyPlayStation2,
            SonyPlayStation3,
            SonyPlayStation4,
            SonyPlayStation5,
            SonyPlayStationPortable,
            VMLabsNUON,
            VTechVFlashVSmilePro,
            ZAPiTGamesGameWaveFamilyEntertainmentSystem,

            // End of console section delimiter
            MarkerDiscBasedConsoleEnd,

            #endregion

            #region Cartridge-Based and Other Consoles

            /*
            AmstradGX4000,
            APFMicrocomputerSystem,
            Atari2600VCS,
            Atari5200,
            Atari7800,
            AtariJaguar,
            AtariXEGS,
            Audiosonic1292AdvancedProgrammableVideoSystem,
            BallyAstrocade,
            BitCorporationDina,
            CasioLoopy,
            CasioPV1000,
            Commodore64GamesSystem,
            DaewooElectronicsZemmix,
            EmersonArcadia2001,
            EpochCassetteVision,
            EpochSuperCassetteVision,
            FairchildChannelF,
            FuntechSuperACan,
            GCEVectrex,
            HeberBBCBridgeCompanion,
            IntertonVC4000,
            JungleTacVii,
            LeapFrogClickStart,
            LJNVideoArt,
            MagnavoxOdyssey2,
            MattelIntellivision,
            NECPCEngineTurboGrafx16,
            NichibutsuMyVision,
            Nintendo64,
            Nintendo64DD,
            NintendoFamicomNintendoEntertainmentSystem,
            NintendoFamicomDiskSystem,
            NintendoSuperFamicomSuperNintendoEntertainmentSystem,
            NintendoSwitch,
            PhilipsVideopacPlusG7400,
            RCAStudioII,
            Sega32X,
            SegaMarkIIIMasterSystem,
            SegaMegaDriveGenesis,
            SegaSG1000,
            SNKNeoGeo,
            SSDCOMPANYLIMITEDXaviXPORT,
            ViewMasterInteractiveVision,
            VTechCreatiVision,
            VTechVSmile,
            VTechSocrates,
            WorldsOfWonderActionMax,

            // End of other console delimiter
            MarkerOtherConsoleEnd,
            */

            #endregion

            #region Computers

            AcornArchimedesAndRiscPC,
            AppleMacintosh,
            AtariSTSeries,
            Commodore64,
            CommodoreAmigaCD,
            FujitsuFMTownsSeries,
            IBMPCcompatible,
            MicrosoftMSX,
            NECPC88Series,
            NECPC98Series,
            SGIIndigoSeries,
            SharpX68000,
            SinclairZXSpectrum,
            SunMicrosystemsUltra,

            // End of computer section delimiter
            MarkerComputerEnd,

            #endregion

            #region Arcade

            AmericanLaserGames3DO,
            Atari3DO,
            Atronic,
            AUSCOMSystem1,
            BallyGameMagic,
            CapcomPlaySystemIII,
            CDExpressCuboCD32,
            FunworldPhotoPlay,
            FuRyuOmronPurikura,
            GlobalVRVarious,
            GlobalVRVortek,
            GlobalVRVortekV3,
            ICEPCHardware,
            IncredibleTechnologiesEagle,
            IncredibleTechnologiesVarious,
            JVLiTouch,
            KonamieAmusement,
            KonamiFireBeat,
            KonamiM2,
            KonamiPython,
            KonamiPython2,
            KonamiSystem573,
            KonamiSystemGV,
            KonamiTwinkle,
            KonamiVarious,
            MeritIndustriesBoardwalk,
            MeritIndustriesMegaTouchForce,
            MeritIndustriesMegaTouchION,
            MeritIndustriesMegaTouchMaxx,
            MeritIndustriesMegaTouchXL,
            NamcoPurikura,
            NamcoSegaNintendoTriforce,
            NamcoSystem12,
            NamcoSystem22,
            NamcoSystem246,
            NamcoSystem256,
            NewJatreCDi,
            NichibutsuHighRateSystem,
            NichibutsuSuperCD,
            NichibutsuXRateSystem,
            PanasonicM2,
            PCBasedArcade,
            PhotoPlayVarious,
            RawThrillsVarious,
            SegaALLS,
            SegaChihiro,
            SegaChihiroSatelliteTerminalPC,
            SegaEuropaR,
            SegaLindbergh,
            SegaLindberghSatelliteTerminalPC,
            SegaNaomi,
            SegaNaomi2,
            SegaNaomiSatelliteTerminalPC,
            SegaNu,
            SegaNu11,
            SegaNu2,
            SegaNuSX,
            SegaRingEdge,
            SegaRingEdge2,
            SegaRingWide,
            SegaSystem32,
            SegaTitanVideo,
            SeibuCATSSystem,
            TABAustriaQuizard,
            TsunamiTsuMoMultiGameMotionSystem,
            UltraCade,

            // End of arcade section delimiter
            MarkerArcadeEnd,

            #endregion

            #region Other

            AudioCD,
            BDVideo,
            DatelPlayStationCheatDeviceUpdates,
            DVDAudio,
            DVDVideo,
            EnhancedCD,
            HDDVDVideo,
            MicrosoftPocketPC,
            MP3AudioDisc,
            NavisoftNaviken,
            PalmOS,
            PhotoCD,
            Psion,
            RainbowDisc,
            SegaPrologue21MultimediaKaraokeSystem,
            SharpZaurus,
            SonyElectronicBook,
            SuperAudioCD,
            TaoiKTV,
            TomyKissSite,
            VideoCD,

            // End of other section delimiter
            MarkerOtherEnd,

            #endregion
        ];

        #endregion
    }
}
