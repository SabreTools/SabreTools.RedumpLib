using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.RedumpOrg.Data
{
    /// <summary>
    /// List of all disc categories
    /// </summary>
    public enum DiscCategory
    {
        [HumanReadable(LongName = "Games")]
        Games = 1,

        [HumanReadable(LongName = "Demos")]
        Demos = 2,

        [HumanReadable(LongName = "Video")]
        Video = 3,

        [HumanReadable(LongName = "Audio")]
        Audio = 4,

        [HumanReadable(LongName = "Multimedia")]
        Multimedia = 5,

        [HumanReadable(LongName = "Applications")]
        Applications = 6,

        [HumanReadable(LongName = "Coverdiscs")]
        Coverdiscs = 7,

        [HumanReadable(LongName = "Educational")]
        Educational = 8,

        [HumanReadable(LongName = "Bonus Discs")]
        BonusDiscs = 9,

        [HumanReadable(LongName = "Preproduction")]
        Preproduction = 10,

        [HumanReadable(LongName = "Add-Ons")]
        AddOns = 11,
    }

    /// <summary>
    /// List of all disc subpaths
    /// </summary>
    public enum DiscSubpath
    {
        [HumanReadable(LongName = "Changes", ShortName = "changes")]
        Changes,

        [HumanReadable(LongName = "Cuesheet", ShortName = "cue")]
        Cuesheet,

        [HumanReadable(LongName = "Edit", ShortName = "edit")]
        Edit,

        [HumanReadable(LongName = "GDI", ShortName = "gdi")]
        GDI,

        [HumanReadable(LongName = "Key", ShortName = "key")]
        Key,

        [HumanReadable(LongName = "LSD", ShortName = "lsd")]
        LSD,

        [HumanReadable(LongName = "MD5", ShortName = "md5")]
        MD5,

        [HumanReadable(LongName = "SBI", ShortName = "sbi")]
        SBI,

        [HumanReadable(LongName = "SFV", ShortName = "sfv")]
        SFV,

        [HumanReadable(LongName = "SHA-1", ShortName = "sha1")]
        SHA1,

        // Placeholder for the linked new disc page, not an actual subpath
        [HumanReadable(Available = false, LongName = "WIP", ShortName = "wip")]
        WIP,
    }

    /// <summary>
    /// List of all disc types
    /// </summary>
    /// <remarks>
    /// All names here match Redump names for the types, not official
    /// naming. Some names had to be extrapolated due to no current support
    /// in the Redump site.
    /// </remarks>
    public enum DiscType
    {
        NONE = 0,

        [HumanReadable(LongName = "BD-25")]
        BD25,

        [HumanReadable(LongName = "BD-33")]
        BD33,

        [HumanReadable(LongName = "BD-50")]
        BD50,

        [HumanReadable(LongName = "BD-66")]
        BD66,

        [HumanReadable(LongName = "BD-100")]
        BD100,

        [HumanReadable(LongName = "BD-128")]
        BD128,

        [HumanReadable(LongName = "CD")]
        CD,

        [HumanReadable(LongName = "DVD-5")]
        DVD5,

        [HumanReadable(LongName = "DVD-9")]
        DVD9,

        [HumanReadable(LongName = "GD-ROM")]
        GDROM,

        [HumanReadable(LongName = "HD-DVD SL")]
        HDDVDSL,

        [HumanReadable(LongName = "HD-DVD DL")]
        HDDVDDL,

        [HumanReadable(LongName = "MIL-CD")]
        MILCD,

        [HumanReadable(LongName = "Nintendo GameCube Game Disc")]
        NintendoGameCubeGameDisc,

        [HumanReadable(LongName = "Nintendo Wii Optical Disc SL")]
        NintendoWiiOpticalDiscSL,

        [HumanReadable(LongName = "Nintendo Wii Optical Disc DL")]
        NintendoWiiOpticalDiscDL,

        [HumanReadable(LongName = "Nintendo Wii U Optical Disc SL")]
        NintendoWiiUOpticalDiscSL,

        [HumanReadable(LongName = "UMD SL")]
        UMDSL,

        [HumanReadable(LongName = "UMD DL")]
        UMDDL,
    }

    /// <summary>
    /// Dump status
    /// </summary>
    public enum DumpStatus
    {
        [HumanReadable(LongName = "Unknown", ShortName = "grey")]
        UnknownGrey = 1,

        [HumanReadable(LongName = "Bad Dump", ShortName = "red")]
        BadDumpRed = 2,

        [HumanReadable(LongName = "Possible Bad Dump", ShortName = "yellow")]
        PossibleBadDumpYellow = 3,

        [HumanReadable(LongName = "Original Media", ShortName = "blue")]
        OriginalMediaBlue = 4,

        [HumanReadable(LongName = "Two or More", ShortName = "green")]
        TwoOrMoreGreen = 5,
    }

    /// <summary>
    /// All possible language selections
    /// </summary>
    public enum LanguageSelection
    {
        [HumanReadable(LongName = "Bios settings")]
        BiosSettings,

        [HumanReadable(LongName = "Language selector")]
        LanguageSelector,

        [HumanReadable(LongName = "Options menu")]
        OptionsMenu,
    }

    /// <summary>
    /// All possible packs
    /// </summary>
    public enum PackType
    {
        [HumanReadable(LongName = "CUES", ShortName = "cues")]
        Cuesheets,

        [HumanReadable(LongName = "DAT", ShortName = "datfile")]
        Datfile,

        [HumanReadable(LongName = "Decrypted KEYS", ShortName = "dkeys")]
        DecryptedKeys,

        [HumanReadable(LongName = "GDIs", ShortName = "gdi")]
        Gdis,

        [HumanReadable(LongName = "KEYS", ShortName = "keys")]
        Keys,

        [HumanReadable(LongName = "LSD", ShortName = "lsd")]
        Lsds,

        [HumanReadable(LongName = "SBI", ShortName = "sbi")]
        Sbis,
    }

    /// <summary>
    /// List of all known systems
    /// </summary>
    /// TODO: Remove marker items
    public enum RedumpSystem
    {
        #region BIOS Sets

        [System(LongName = "Microsoft Xbox (BIOS)", ShortName = "xbox-bios", HasDat = true)]
        MicrosoftXboxBIOS,

        [System(LongName = "Nintendo GameCube (BIOS)", ShortName = "gc-bios", HasDat = true)]
        NintendoGameCubeBIOS,

        [System(LongName = "Sony PlayStation (BIOS)", ShortName = "psx-bios", HasDat = true)]
        SonyPlayStationBIOS,

        [System(LongName = "Sony PlayStation 2 (BIOS)", ShortName = "ps2-bios", HasDat = true)]
        SonyPlayStation2BIOS,

        #endregion

        #region Disc-Based Consoles

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Atari Jaguar CD Interactive Multimedia System", ShortName = "ajcd", HasCues = true, HasDat = true)]
        AtariJaguarCDInteractiveMultimediaSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Bandai Playdia Quick Interactive System", ShortName = "qis", HasCues = true, HasDat = true)]
        BandaiPlaydiaQuickInteractiveSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Bandai Pippin", ShortName = "pippin", HasCues = true, HasDat = true)]
        BandaiPippin,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Commodore Amiga CD32", ShortName = "cd32", HasCues = true, HasDat = true)]
        CommodoreAmigaCD32,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Commodore Amiga CDTV", ShortName = "cdtv", HasCues = true, HasDat = true)]
        CommodoreAmigaCDTV,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Envizions EVO Smart Console")]
        EnvizionsEVOSmartConsole,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Fujitsu FM Towns Marty")]
        FujitsuFMTownsMarty,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Hasbro iON Educational Gaming System")]
        HasbroiONEducationalGamingSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow", ShortName = "hvn", IsBanned = true, HasCues = true, HasDat = true)]
        HasbroVideoNow,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow Color", ShortName = "hvnc", IsBanned = true, HasCues = true, HasDat = true)]
        HasbroVideoNowColor,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow Jr.", ShortName = "hvnjr", IsBanned = true, HasCues = true, HasDat = true)]
        HasbroVideoNowJr,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow XP", ShortName = "hvnxp", IsBanned = true, HasCues = true, HasDat = true)]
        HasbroVideoNowXP,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Mattel Fisher-Price iXL", ShortName = "ixl", HasCues = true, HasDat = true)]
        MattelFisherPriceiXL,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Mattel HyperScan", ShortName = "hs", HasCues = true, HasDat = true)]
        MattelHyperScan,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Memorex Visual Information System", ShortName = "vis", HasCues = true, HasDat = true)]
        MemorexVisualInformationSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Microsoft Xbox", ShortName = "xbox", HasCues = true, HasDat = true)]
        MicrosoftXbox,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Microsoft Xbox 360", ShortName = "xbox360", HasCues = true, HasDat = true)]
        MicrosoftXbox360,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Microsoft Xbox One", ShortName = "xboxone", IsBanned = true, HasDat = true)]
        MicrosoftXboxOne,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Microsoft Xbox Series X", ShortName = "xboxsx", IsBanned = true, HasDat = true)]
        MicrosoftXboxSeriesXS,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "NEC PC Engine CD & TurboGrafx CD", ShortName = "pce", HasCues = true, HasDat = true)]
        NECPCEngineCDTurboGrafxCD,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "NEC PC-FX & PC-FXGA", ShortName = "pc-fx", HasCues = true, HasDat = true)]
        NECPCFXPCFXGA,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Nintendo GameCube", ShortName = "gc", HasDat = true)]
        NintendoGameCube,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Nintendo-Sony Super NES CD-ROM System")]
        NintendoSonySuperNESCDROMSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Nintendo Wii", ShortName = "wii", HasDat = true)]
        NintendoWii,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Nintendo Wii U", ShortName = "wiiu", IsBanned = true, HasDat = true, HasKeys = true)]
        NintendoWiiU,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Panasonic 3DO Interactive Multiplayer", ShortName = "3do", HasCues = true, HasDat = true)]
        Panasonic3DOInteractiveMultiplayer,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Philips CD-i", ShortName = "cdi", HasCues = true, HasDat = true)]
        PhilipsCDi,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Playmaji Polymega")]
        PlaymajiPolymega,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Pioneer LaserActive")]
        PioneerLaserActive,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sega Dreamcast", ShortName = "dc", HasCues = true, HasDat = true, HasGdi = true)]
        SegaDreamcast,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sega Mega CD & Sega CD", ShortName = "mcd", HasCues = true, HasDat = true)]
        SegaMegaCDSegaCD,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sega Saturn", ShortName = "ss", HasCues = true, HasDat = true)]
        SegaSaturn,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Neo Geo CD", ShortName = "ngcd", HasCues = true, HasDat = true)]
        SNKNeoGeoCD,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation", ShortName = "psx", HasCues = true, HasDat = true, HasLsd = true, HasSbi = true)]
        SonyPlayStation,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation 2", ShortName = "ps2", HasCues = true, HasDat = true)]
        SonyPlayStation2,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation 3", ShortName = "ps3", HasCues = true, HasDat = true, HasDkeys = true, HasKeys = true)]
        SonyPlayStation3,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation 4", ShortName = "ps4", IsBanned = true, HasDat = true)]
        SonyPlayStation4,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation 5", ShortName = "ps5", IsBanned = true, HasDat = true)]
        SonyPlayStation5,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation Portable", ShortName = "psp", HasDat = true)]
        SonyPlayStationPortable,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "VM Labs NUON", ShortName = "nuon", HasDat = true)]
        VMLabsNUON,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "VTech V.Flash & V.Smile Pro", ShortName = "vflash", HasCues = true, HasDat = true)]
        VTechVFlashVSmilePro,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "ZAPiT Games Game Wave Family Entertainment System", ShortName = "gamewave", HasDat = true)]
        ZAPiTGamesGameWaveFamilyEntertainmentSystem,

        // End of console section delimiter
        MarkerDiscBasedConsoleEnd,

        #endregion

        #region Cartridge-Based and Other Consoles

        /*
        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Amstrad GX-4000")]
        AmstradGX4000,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "APF Microcomputer System")]
        APFMicrocomputerSystem,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Atari 2600 & VCS")]
        Atari2600VCS,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Atari 5200")]
        Atari5200,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Atari 7800")]
        Atari7800,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Atari Jaguar")]
        AtariJaguar,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Atari XEGS")]
        AtariXEGS,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Audiosonic 1292 Advanced Programmable Video System")]
        Audiosonic1292AdvancedProgrammableVideoSystem,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Bally Astrocade")]
        BallyAstrocade,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Bit Corporation Dina")]
        BitCorporationDina,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Casio Loopy")]
        CasioLoopy,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Casio PV-1000")]
        CasioPV1000,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Commodore 64 Games System")]
        Commodore64GamesSystem,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Daewoo Electronics Zemmix")]
        DaewooElectronicsZemmix,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Emerson Arcadia 2001")]
        EmersonArcadia2001,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Epoch Cassette Vision")]
        EpochCassetteVision,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Epoch Super Cassette Vision")]
        EpochSuperCassetteVision,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Fairchild Channel F")]
        FairchildChannelF,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Funtech Super A'Can")]
        FuntechSuperACan,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "GCE Vectrex")]
        GCEVectrex,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Heber BBC Bridge Companion")]
        HeberBBCBridgeCompanion,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Interton VC-4000")]
        IntertonVC4000,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "JungleTac Vii")]
        JungleTacVii,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "LeapFrog ClickStart")]
        LeapFrogClickStart,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "LJN VideoArt")]
        LJNVideoArt,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Magnavox Odyssey 2")]
        MagnavoxOdyssey2,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Mattel Intellivision")]
        MattelIntellivision,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "NEC PC Engine & TurboGrafx-16")]
        NECPCEngineTurboGrafx16,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Nichibutsu MyVision")]
        NichibutsuMyVision,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Nintendo 64")]
        Nintendo64,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Nintendo 64DD")]
        Nintendo64DD,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Nintendo Famicom & Nintendo Entertainment System")]
        NintendoFamicomNintendoEntertainmentSystem,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Nintendo Famicom Disk System")]
        NintendoFamicomDiskSystem,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Nintendo Super Famicom & Super Nintendo Entertainment System")]
        NintendoSuperFamicomSuperNintendoEntertainmentSystem,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Nintendo Switch")]
        NintendoSwitch,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Philips Videopac+ & G7400")]
        PhilipsVideopacPlusG7400,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "RCA Studio-II")]
        RCAStudioII,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Sega 32X")]
        Sega32X,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Sega Mark III & Master System")]
        SegaMarkIIIMasterSystem,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Sega MegaDrive & Genesis")]
        SegaMegaDriveGenesis,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Sega SG-1000")]
        SegaSG1000,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "SNK NeoGeo")]
        SNKNeoGeo,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "SSD COMPANY LIMITED XaviXPORT")]
        SSDCOMPANYLIMITEDXaviXPORT,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "ViewMaster Interactive Vision")]
        ViewMasterInteractiveVision,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "V.Tech CreatiVision")]
        VTechCreatiVision,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "V.Tech V.Smile")]
        VTechVSmile,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "V.Tech Socrates")]
        VTechSocrates,

        [System(Category = SystemCategory.OtherConsole, Available = false, LongName = "Worlds of Wonder ActionMax")]
        WorldsOfWonderActionMax,

        // End of other console delimiter
        MarkerOtherConsoleEnd,
        */

        #endregion

        #region Computers

        [System(Category = SystemCategory.Computer, LongName = "Acorn Archimedes", ShortName = "arch", HasCues = true, HasDat = true)]
        AcornArchimedes,

        [System(Category = SystemCategory.Computer, LongName = "Apple Macintosh", ShortName = "mac", HasCues = true, HasDat = true, HasLsd = true, HasSbi = true)]
        AppleMacintosh,

        [System(Category = SystemCategory.Computer, LongName = "Commodore Amiga CD", ShortName = "acd", HasCues = true, HasDat = true)]
        CommodoreAmigaCD,

        [System(Category = SystemCategory.Computer, LongName = "Fujitsu FM Towns series", ShortName = "fmt", HasCues = true, HasDat = true)]
        FujitsuFMTownsseries,

        [System(Category = SystemCategory.Computer, LongName = "IBM PC compatible", ShortName = "pc", HasCues = true, HasDat = true, HasLsd = true, HasSbi = true)]
        IBMPCcompatible,

        [System(Category = SystemCategory.Computer, LongName = "NEC PC-88 series", ShortName = "pc-88", HasCues = true, HasDat = true)]
        NECPC88series,

        [System(Category = SystemCategory.Computer, LongName = "NEC PC-98 series", ShortName = "pc-98", HasCues = true, HasDat = true)]
        NECPC98series,

        [System(Category = SystemCategory.Computer, LongName = "Sharp X68000", ShortName = "x68k", HasCues = true, HasDat = true)]
        SharpX68000,

        // End of computer section delimiter
        MarkerComputerEnd,

        #endregion

        #region Arcade

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Amiga CUBO CD32")]
        AmigaCUBOCD32,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "American Laser Games 3DO")]
        AmericanLaserGames3DO,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Atari 3DO")]
        Atari3DO,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Atronic")]
        Atronic,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "AUSCOM System 1")]
        AUSCOMSystem1,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Bally Game Magic")]
        BallyGameMagic,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Capcom CP System III")]
        CapcomCPSystemIII,

        [System(Category = SystemCategory.Arcade, LongName = "funworld Photo Play", ShortName = "fpp", HasCues = true, HasDat = true)]
        funworldPhotoPlay,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "FuRyu & Omron Purikura")]
        FuRyuOmronPurikura,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Global VR PC-based Systems")]
        GlobalVRVarious,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Global VR Vortek")]
        GlobalVRVortek,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Global VR Vortek V3")]
        GlobalVRVortekV3,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "ICE PC-based Hardware")]
        ICEPCHardware,

        [System(Category = SystemCategory.Arcade, LongName = "Incredible Technologies Eagle", ShortName = "ite", HasCues = true, HasDat = true)]
        IncredibleTechnologiesEagle,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Incredible Technologies PC-based Systems")]
        IncredibleTechnologiesVarious,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "JVL iTouch")]
        JVLiTouch,

        [System(Category = SystemCategory.Arcade, LongName = "Konami e-Amusement", ShortName = "kea", HasCues = true, HasDat = true)]
        KonamieAmusement,

        [System(Category = SystemCategory.Arcade, LongName = "Konami FireBeat", ShortName = "kfb", HasCues = true, HasDat = true)]
        KonamiFireBeat,

        [System(Category = SystemCategory.Arcade, LongName = "Konami M2", ShortName = "km2", IsBanned = true, HasCues = true, HasDat = true)]
        KonamiM2,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Konami Python")]
        KonamiPython,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Konami Python 2")]
        KonamiPython2,

        [System(Category = SystemCategory.Arcade, LongName = "Konami System 573", ShortName = "ks573", HasCues = true, HasDat = true)]
        KonamiSystem573,

        [System(Category = SystemCategory.Arcade, LongName = "Konami System GV", ShortName = "ksgv", HasCues = true, HasDat = true)]
        KonamiSystemGV,

        [System(Category = SystemCategory.Arcade, LongName = "Konami Twinkle", ShortName = "kt")]
        KonamiTwinkle,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Konami PC-based Systems")]
        KonamiVarious,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Merit Industries Boardwalk")]
        MeritIndustriesBoardwalk,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Merit Industries MegaTouch Force")]
        MeritIndustriesMegaTouchForce,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Merit Industries MegaTouch ION")]
        MeritIndustriesMegaTouchION,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Merit Industries MegaTouch Maxx")]
        MeritIndustriesMegaTouchMaxx,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Merit Industries MegaTouch XL")]
        MeritIndustriesMegaTouchXL,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Namco Purikura")]
        NamcoPurikura,

        [System(Category = SystemCategory.Arcade, LongName = "Namco · Sega · Nintendo Triforce", ShortName = "trf", HasCues = true, HasDat = true, HasGdi = true)]
        NamcoSegaNintendoTriforce,

        [System(Category = SystemCategory.Arcade, LongName = "Namco System 12", ShortName = "ns12")]
        NamcoSystem12,

        [System(Category = SystemCategory.Arcade, LongName = "Namco System 246 / System 256", ShortName = "ns246", HasCues = true, HasDat = true)]
        NamcoSystem246256,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "New Jatre CD-i")]
        NewJatreCDi,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Nichibutsu High Rate System")]
        NichibutsuHighRateSystem,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Nichibutsu Super CD")]
        NichibutsuSuperCD,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Nichibutsu X-Rate System")]
        NichibutsuXRateSystem,

        [System(Category = SystemCategory.Arcade, LongName = "Panasonic M2", ShortName = "m2", IsBanned = true, HasCues = true, HasDat = true)]
        PanasonicM2,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "PhotoPlay PC-based Systems")]
        PhotoPlayVarious,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Raw Thrills PC-based Systems")]
        RawThrillsVarious,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega ALLS")]
        SegaALLS,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Chihiro", ShortName = "chihiro", HasCues = true, HasDat = true, HasGdi = true)]
        SegaChihiro,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega Europa-R")]
        SegaEuropaR,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Lindbergh", ShortName = "lindbergh", HasDat = true)]
        SegaLindbergh,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Naomi", ShortName = "naomi", HasCues = true, HasDat = true, HasGdi = true)]
        SegaNaomi,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Naomi 2", ShortName = "naomi2", HasCues = true, HasDat = true, HasGdi = true)]
        SegaNaomi2,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega NAOMI Satellite Terminal PC")]
        SegaNaomiSatelliteTerminalPC,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega Nu")]
        SegaNu,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega Nu 1.1")]
        SegaNu11,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega Nu 2")]
        SegaNu2,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega Nu SX")]
        SegaNuSX,

        [System(Category = SystemCategory.Arcade, LongName = "Sega RingEdge", ShortName = "sre", HasDat = true)]
        SegaRingEdge,

        [System(Category = SystemCategory.Arcade, LongName = "Sega RingEdge 2", ShortName = "sre2", HasDat = true)]
        SegaRingEdge2,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega RingWide")]
        SegaRingWide,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega System 32")]
        SegaSystem32,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Titan Video", ShortName = "stv")]
        SegaTitanVideo,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Seibu CATS System")]
        SeibuCATSSystem,

        [System(Category = SystemCategory.Arcade, LongName = "TAB-Austria Quizard", ShortName = "quizard", HasCues = true, HasDat = true)]
        TABAustriaQuizard,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Tsunami TsuMo Multi-Game Motion System")]
        TsunamiTsuMoMultiGameMotionSystem,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "UltraCade PC-based Systems")]
        UltraCade,

        // End of arcade section delimiter
        MarkerArcadeEnd,

        #endregion

        #region Other

        [System(Category = SystemCategory.Other, LongName = "Audio CD", ShortName = "audio-cd", IsBanned = true, HasCues = true, HasDat = true)]
        AudioCD,

        [System(Category = SystemCategory.Other, LongName = "BD-Video", ShortName = "bd-video", IsBanned = true, HasDat = true)]
        BDVideo,

        [System(Category = SystemCategory.Other, Available = false, LongName = "DVD-Audio")]
        DVDAudio,

        [System(Category = SystemCategory.Other, LongName = "DVD-Video", ShortName = "dvd-video", IsBanned = true, HasDat = true)]
        DVDVideo,

        [System(Category = SystemCategory.Other, LongName = "Enhanced CD", ShortName = "enhanced-cd", IsBanned = true)]
        EnhancedCD,

        [System(Category = SystemCategory.Other, LongName = "HD DVD-Video", ShortName = "hddvd-video", IsBanned = true, HasDat = true)]
        HDDVDVideo,

        [System(Category = SystemCategory.Other, LongName = "Navisoft Naviken 2.1", ShortName = "navi21", IsBanned = true, HasCues = true, HasDat = true)]
        NavisoftNaviken21,

        [System(Category = SystemCategory.Other, LongName = "Palm OS", ShortName = "palm", HasCues = true, HasDat = true)]
        PalmOS,

        [System(Category = SystemCategory.Other, LongName = "Photo CD", ShortName = "photo-cd", HasCues = true, HasDat = true)]
        PhotoCD,

        [System(Category = SystemCategory.Other, LongName = "PlayStation GameShark Updates", ShortName = "psxgs", HasCues = true, HasDat = true)]
        PlayStationGameSharkUpdates,

        [System(Category = SystemCategory.Other, LongName = "Pocket PC", ShortName = "ppc", HasCues = true, HasDat = true)]
        PocketPC,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Psion")]
        Psion,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Rainbow Disc")]
        RainbowDisc,

        [System(Category = SystemCategory.Other, LongName = "Sega Prologue 21 Multimedia Karaoke System", ShortName = "sp21", HasCues = true, HasDat = true)]
        SegaPrologue21MultimediaKaraokeSystem,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Sharp Zaurus")]
        SharpZaurus,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Sony Electronic Book")]
        SonyElectronicBook,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Super Audio CD")]
        SuperAudioCD,

        [System(Category = SystemCategory.Other, LongName = "Tao iKTV", ShortName = "iktv")]
        TaoiKTV,

        [System(Category = SystemCategory.Other, LongName = "Tomy Kiss-Site", ShortName = "ksite", HasCues = true, HasDat = true)]
        TomyKissSite,

        [System(Category = SystemCategory.Other, LongName = "Video CD", ShortName = "vcd", IsBanned = true, HasCues = true, HasDat = true)]
        VideoCD,

        // End of other section delimiter
        MarkerOtherEnd,

        #endregion
    }

    /// <summary>
    /// List of all Redump site codes
    /// </summary>
    public enum SiteCode
    {
        [HumanReadable(ShortName = "[T:ACC]", LongName = "<b>Acclaim ID</b>:")]
        AcclaimID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Accolade ID</b>:", LongName = "<b>Accolade ID</b>:")]
        AccoladeID,

        [HumanReadable(ShortName = "[T:ACT]", LongName = "<b>Activision ID</b>:")]
        ActivisionID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Additional BCA Data</b>:", LongName = "<b>Additional BCA Data</b>:")]
        AdditionalBCAData,

        [HumanReadable(ShortName = "[T:ALT]", LongName = "<b>Alternative Title</b>:")]
        AlternativeTitle,

        [HumanReadable(ShortName = "[T:ALTF]", LongName = "<b>Alternative Foreign Title</b>:")]
        AlternativeForeignTitle,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Applications</b>:", LongName = "<b>Applications</b>:")]
        Applications,

        [HumanReadable(ShortName = "[T:BID]", LongName = "<b>Bandai ID</b>:")]
        BandaiID,

        [HumanReadable(ShortName = "[T:BBFC]", LongName = "<b>BBFC Reg. No.</b>:")]
        BBFCRegistrationNumber,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Bethesda ID</b>:", LongName = "<b>Bethesda ID</b>:")]
        BethesdaID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>CD Projekt ID</b>:", LongName = "<b>CD Projekt ID</b>:")]
        CDProjektID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Compatible OS</b>:", LongName = "<b>Compatible OS</b>:")]
        CompatibleOS,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Cover ID</b>:", LongName = "<b>Cover ID</b>:")]
        CoverID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Disc Hologram ID</b>:", LongName = "<b>Disc Hologram ID</b>:")]
        DiscHologramID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Disc ID</b>:", LongName = "<b>Disc ID</b>:")]
        DiscID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Disc Title (non-Latin)</b>:", LongName = "<b>Disc Title (non-Latin)</b>:")]
        DiscTitleNonLatin,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Disney Interactive ID</b>", LongName = "<b>Disney Interactive ID</b>:")]
        DisneyInteractiveID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>DMI</b>:", LongName = "<b>DMI</b>:")]
        DMIHash,

        [HumanReadable(ShortName = "[T:DNAS]", LongName = "<b>DNAS Disc ID</b>:")]
        DNASDiscID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Edition (non-Latin)</b>:", LongName = "<b>Edition (non-Latin)</b>:")]
        EditionNonLatin,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Eidos ID</b>:", LongName = "<b>Eidos ID</b>:")]
        EidosID,

        [HumanReadable(ShortName = "[T:EAID]", LongName = "<b>Electronic Arts ID</b>:")]
        ElectronicArtsID,

        [HumanReadable(ShortName = "[T:X]", LongName = "<b>Extras</b>:")]
        Extras,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Filename</b>:", LongName = "<b>Filename</b>:")]
        Filename,

        [HumanReadable(ShortName = "[T:FIID]", LongName = "<b>Fox Interactive ID</b>:")]
        FoxInteractiveID,

        [HumanReadable(ShortName = "[T:GF]", LongName = "<b>Game Footage</b>:")]
        GameFootage,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Games</b>:", LongName = "<b>Games</b>:")]
        Games,

        [HumanReadable(ShortName = "[T:G]", LongName = "<b>Genre</b>:")]
        Genre,

        [HumanReadable(ShortName = "[T:GTID]", LongName = "<b>GT Interactive ID</b>:")]
        GTInteractiveID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>High Sierra Volume Descriptor</b>:", LongName = "<b>High Sierra Volume Descriptor</b>:")]
        HighSierraVolumeDescriptor,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Internal Name</b>:", LongName = "<b>Internal Name</b>:")]
        InternalName,

        [HumanReadable(ShortName = "[T:ISN]", LongName = "<b>Internal Serial</b>:")]
        InternalSerialName,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Interplay ID</b>:", LongName = "<b>Interplay ID</b>:")]
        InterplayID,

        [HumanReadable(ShortName = "[T:ISBN]", LongName = "<b>ISBN</b>:")]
        ISBN,

        [HumanReadable(ShortName = "[T:ISSN]", LongName = "<b>ISSN</b>:")]
        ISSN,

        [HumanReadable(ShortName = "[T:JID]", LongName = "<b>JASRAC ID</b>:")]
        JASRACID,

        [HumanReadable(ShortName = "[T:KIRZ]", LongName = "<b>King Records ID</b>:")]
        KingRecordsID,

        [HumanReadable(ShortName = "[T:KOEI]", LongName = "<b>Koei ID</b>:")]
        KoeiID,

        [HumanReadable(ShortName = "[T:KID]", LongName = "<b>Konami ID</b>:")]
        KonamiID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Logs Link</b>:", LongName = "<b>Logs Link</b>:")]
        LogsLink,

        [HumanReadable(ShortName = "[T:LAID]", LongName = "<b>Lucas Arts ID</b>:")]
        LucasArtsID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Microsoft ID</b>:", LongName = "<b>Microsoft ID</b>:")]
        MicrosoftID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Multisession</b>:", LongName = "<b>Multisession</b>:")]
        Multisession,

        [HumanReadable(ShortName = "[T:NGID]", LongName = "<b>Nagano ID</b>:")]
        NaganoID,

        [HumanReadable(ShortName = "[T:NID]", LongName = "<b>Namco ID</b>:")]
        NamcoID,

        [HumanReadable(ShortName = "[T:NYG]", LongName = "<b>Net Yaroze Games</b>:")]
        NetYarozeGames,

        [HumanReadable(ShortName = "[T:NPS]", LongName = "<b>Nippon Ichi Software ID</b>:")]
        NipponIchiSoftwareID,

        [HumanReadable(ShortName = "[T:OID]", LongName = "<b>Origin ID</b>:")]
        OriginID,

        [HumanReadable(ShortName = "[T:P]", LongName = "<b>Patches</b>:")]
        Patches,

        // This doesn't have a site tag yet
        /// <remarks>No text value after</remarks>
        [HumanReadable(ShortName = "PC/Mac Hybrid", LongName = "PC/Mac Hybrid")]
        PCMacHybrid,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>PFI</b>:", LongName = "<b>PFI</b>:")]
        PFIHash,

        [HumanReadable(ShortName = "[T:PD]", LongName = "<b>Playable Demos</b>:")]
        PlayableDemos,

        [HumanReadable(ShortName = "[T:PCID]", LongName = "<b>Pony Canyon ID</b>:")]
        PonyCanyonID,

        /// <remarks>No text value after</remarks>
        [HumanReadable(ShortName = "[T:PT2]", LongName = "<b>Postgap type</b>: Form 2")]
        PostgapType,

        [HumanReadable(ShortName = "[T:PPN]", LongName = "<b>PPN</b>:")]
        PPN,

        // This doesn't have a site tag for some systems yet
        [HumanReadable(ShortName = "<b>Protection</b>:", LongName = "<b>Protection</b>:")]
        Protection,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Ring non-zero data start</b>:", LongName = "<b>Ring non-zero data start</b>:")]
        RingNonZeroDataStart,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Ring Perfect Audio Offset</b>:", LongName = "<b>Ring Perfect Audio Offset</b>:")]
        RingPerfectAudioOffset,

        [HumanReadable(ShortName = "[T:RD]", LongName = "<b>Rolling Demos</b>:")]
        RollingDemos,

        [HumanReadable(ShortName = "[T:SG]", LongName = "<b>Savegames</b>:")]
        Savegames,

        [HumanReadable(ShortName = "[T:SID]", LongName = "<b>Sega ID</b>:")]
        SegaID,

        [HumanReadable(ShortName = "[T:SNID]", LongName = "<b>Selen ID</b>:")]
        SelenID,

        [HumanReadable(ShortName = "[T:S]", LongName = "<b>Series</b>:")]
        Series,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Sierra ID</b>:", LongName = "<b>Sierra ID</b>:")]
        SierraID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>SS</b>:", LongName = "<b>SS</b>:")]
        SSHash,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>SS version</b>:", LongName = "<b>SS version</b>:")]
        SSVersion,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Steam App ID</b>:", LongName = "<b>Steam AppID</b>:")]
        SteamAppID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Steam Depot ID (.sis/.csm/.csd)</b>:", LongName = "<b>Steam Depot ID (.sis/.csm/.csd)</b>:")]
        SteamCsmCsdDepotID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Steam Depot ID (.sis/.sim/.sid)</b>:", LongName = "<b>Steam Depot ID (.sis/.sim/.sid)</b>:")]
        SteamSimSidDepotID,

        [HumanReadable(ShortName = "[T:TID]", LongName = "<b>Taito ID</b>:")]
        TaitoID,

        [HumanReadable(ShortName = "[T:TD]", LongName = "<b>Tech Demos</b>:")]
        TechDemos,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Title ID</b>:", LongName = "<b>Title ID</b>:")]
        TitleID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>2K Games ID</b>:", LongName = "<b>2K Games ID</b>:")]
        TwoKGamesID,

        [HumanReadable(ShortName = "[T:UID]", LongName = "<b>Ubisoft ID</b>:")]
        UbisoftID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Universal Hash (SHA-1)</b>:", LongName = "<b>Universal Hash (SHA-1)</b>:")]
        UniversalHash,

        [HumanReadable(ShortName = "[T:VID]", LongName = "<b>Valve ID</b>:")]
        ValveID,

        [HumanReadable(ShortName = "[T:VFC]", LongName = "<b>VFC code</b>:")]
        VFCCode,

        [HumanReadable(ShortName = "[T:V]", LongName = "<b>Videos</b>:")]
        Videos,

        [HumanReadable(ShortName = "[T:VOL]", LongName = "<b>Volume Label</b>:")]
        VolumeLabel,

        /// <remarks>No text value after</remarks>
        [HumanReadable(ShortName = "[T:VCD]", LongName = "<b>V-CD</b>")]
        VCD,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>XeMID</b>:", LongName = "<b>XeMID</b>:")]
        XeMID,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>XMID</b>:", LongName = "<b>XMID</b>:")]
        XMID,
    }

    /// <summary>
    /// List of all recognized sort parameters
    /// </summary>
    public enum SortCategory
    {
        [HumanReadable(LongName = "Title", ShortName = "")]
        Title,

        [HumanReadable(LongName = "Added", ShortName = "added")]
        Added,

        [HumanReadable(LongName = "Region", ShortName = "region")]
        Region,

        [HumanReadable(LongName = "System", ShortName = "system")]
        System,

        [HumanReadable(LongName = "Version", ShortName = "version")]
        Version,

        [HumanReadable(LongName = "Edition", ShortName = "edition")]
        Edition,

        [HumanReadable(LongName = "Languages", ShortName = "languages")]
        Languages,

        [HumanReadable(LongName = "Serial", ShortName = "serial")]
        Serial,

        [HumanReadable(LongName = "Status", ShortName = "status")]
        Status,

        [HumanReadable(LongName = "Modified", ShortName = "modified")]
        Modified,
    }

    /// <summary>
    /// List of all recognized sort directions
    /// </summary>
    public enum SortDirection
    {
        [HumanReadable(LongName = "Ascending", ShortName = "asc")]
        Ascending,

        [HumanReadable(LongName = "Descending", ShortName = "desc")]
        Descending,
    }

    /// <summary>
    /// List of system categories
    /// </summary>
    public enum SystemCategory
    {
        NONE = 0,

        [HumanReadable(LongName = "Disc-Based Consoles")]
        DiscBasedConsole,

        [HumanReadable(LongName = "Other Consoles")]
        OtherConsole,

        [HumanReadable(LongName = "Computers")]
        Computer,

        [HumanReadable(LongName = "Arcade")]
        Arcade,

        [HumanReadable(LongName = "Other")]
        Other,
    };

    /// <summary>
    /// Generic yes/no values
    /// </summary>
    public enum YesNo
    {
        [HumanReadable(LongName = "Yes/No")]
        NULL = 0,

        [HumanReadable(LongName = "No")]
        No = 1,

        [HumanReadable(LongName = "Yes")]
        Yes = 2,
    }
}
