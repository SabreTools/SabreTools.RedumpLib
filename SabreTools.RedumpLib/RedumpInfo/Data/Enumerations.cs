using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.RedumpInfo.Data
{
    /// <summary>
    /// List of all disc categories
    /// </summary>
    public enum DiscCategory
    {
        [HumanReadable(LongName = "Add-Ons")]
        AddOns,

        [HumanReadable(LongName = "Applications")]
        Applications,

        [HumanReadable(LongName = "Audio")]
        Audio,

        [HumanReadable(LongName = "Bonus Discs")]
        BonusDiscs,

        [HumanReadable(LongName = "Coverdiscs")]
        Coverdiscs,

        [HumanReadable(LongName = "Demos")]
        Demos,

        [HumanReadable(LongName = "Educational")]
        Educational,

        [HumanReadable(LongName = "Games")]
        Games,

        [HumanReadable(LongName = "Multimedia")]
        Multimedia,

        [HumanReadable(LongName = "Preproduction")]
        Preproduction,

        [HumanReadable(LongName = "Video")]
        Video,
    }

    /// <summary>
    /// List of all disc subpaths
    /// </summary>
    public enum DiscSubpath
    {
        [HumanReadable(LongName = "Cuesheet", ShortName = "cue")]
        Cuesheet,

        [HumanReadable(LongName = "Edit", ShortName = "edit")]
        Edit,

        // Placeholder for the linked queue history page, not an actual subpath
        [HumanReadable(LongName = "History", ShortName = "history")]
        History,
    }

    /// <summary>
    /// Dump status
    /// </summary>
    public enum DumpStatus
    {
        // TODO: Verify new naming
        [HumanReadable(LongName = "Unknown", ShortName = "grey")]
        UnknownGrey = 1,

        // TODO: Verify new naming
        [HumanReadable(LongName = "Bad Dump", ShortName = "red")]
        BadDumpRed = 2,

        [HumanReadable(LongName = "Questionable", ShortName = "yellow")]
        Questionable,

        [HumanReadable(LongName = "Unverified", ShortName = "blue")]
        Unverified,

        [HumanReadable(LongName = "Verified", ShortName = "green")]
        Verified,
    }

    /// <summary>
    /// List of all media types
    /// </summary>
    public enum MediaType
    {
        NONE = 0,

        [HumanReadable(LongName = "BD-100", ShortName = "bd100")]
        BD100,

        [HumanReadable(LongName = "BD-25", ShortName = "bd25")]
        BD25,

        [HumanReadable(LongName = "BD-50", ShortName = "bd50")]
        BD50,

        [HumanReadable(LongName = "BD-66", ShortName = "bd66")]
        BD66,

        [HumanReadable(LongName = "CD", ShortName = "cd")]
        CD,

        [HumanReadable(LongName = "DVD-5", ShortName = "dvd5")]
        DVD5,

        [HumanReadable(LongName = "DVD-9", ShortName = "dvd9")]
        DVD9,

        [HumanReadable(LongName = "GD-ROM", ShortName = "gdrom")]
        GDROM,

        [HumanReadable(LongName = "HD DVD (DL)", ShortName = "hdvd30")]
        HDDVDDL,

        [HumanReadable(LongName = "HD DVD (SL)", ShortName = "hdvd15")]
        HDDVDSL,

        // TODO: Figure out how to mark this as debug-only
        [HumanReadable(LongName = "Max Test (4-layer)", ShortName = "test4l")]
        MaxTest4Layer,

        [HumanReadable(LongName = "Nintendo GameCube Game Disc", ShortName = "dvd5gc")]
        NintendoGameCubeGameDisc,

        [HumanReadable(LongName = "UMD (DL)", ShortName = "umd2")]
        UMDDL,

        [HumanReadable(LongName = "UMD (SL)", ShortName = "umd1")]
        UMDSL,

        [HumanReadable(LongName = "Wii Optical Disc (DL)", ShortName = "dvd9wii")]
        WiiOpticalDiscDL,

        [HumanReadable(LongName = "Wii Optical Disc (SL)", ShortName = "dvd5wii")]
        WiiOpticalDiscSL,

        [HumanReadable(LongName = "Wii U Optical Disc (SL)", ShortName = "bd25wiiu")]
        WiiUOpticalDiscSL,
    }

    /// <summary>
    /// List of all known systems
    /// </summary>
    /// TODO: Remove marker items
    /// TODO: Double check all flags once the site is live
    /// TODO: Add MAXTEST as a debug-only system
    public enum RedumpSystem
    {
        // TODO: Somehow indicate that these are static paths
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

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Atari Jaguar CD Interactive Multimedia System", ShortName = "AJCD", HasCues = true, HasDat = true)]
        AtariJaguarCDInteractiveMultimediaSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Bandai Pippin", ShortName = "PIPPIN", HasCues = true, HasDat = true)]
        BandaiPippin,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Bandai Playdia Quick Interactive System", ShortName = "QIS", HasCues = true, HasDat = true)]
        BandaiPlaydiaQuickInteractiveSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Commodore Amiga CD32", ShortName = "CD32", HasCues = true, HasDat = true)]
        CommodoreAmigaCD32,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Commodore Amiga CDTV", ShortName = "CDTV", HasCues = true, HasDat = true)]
        CommodoreAmigaCDTV,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Envizions EVO Smart Console")]
        EnvizionsEVOSmartConsole,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Fujitsu FM Towns Marty")]
        FujitsuFMTownsMarty,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Hasbro iON Educational Gaming System")]
        HasbroiONEducationalGamingSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow", ShortName = "HVN", IsBanned = true, HasCues = true, HasDat = true)]
        HasbroVideoNow,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow Color", ShortName = "HVNC", IsBanned = true, HasCues = true, HasDat = true)]
        HasbroVideoNowColor,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow Jr.", ShortName = "HVNJR", IsBanned = true, HasCues = true, HasDat = true)]
        HasbroVideoNowJr,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow XP", ShortName = "HVNXP", IsBanned = true, HasCues = true, HasDat = true)]
        HasbroVideoNowXP,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Mattel Fisher-Price iXL", ShortName = "IXL", HasCues = true, HasDat = true)]
        MattelFisherPriceiXL,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Mattel HyperScan", ShortName = "HS", HasCues = true, HasDat = true)]
        MattelHyperScan,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Memorex Visual Information System", ShortName = "VIS", HasCues = true, HasDat = true)]
        MemorexVisualInformationSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Microsoft Xbox", ShortName = "XBOX", HasCues = true, HasDat = true)]
        MicrosoftXbox,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Microsoft Xbox 360", ShortName = "XBOX360", HasCues = true, HasDat = true)]
        MicrosoftXbox360,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Microsoft Xbox One", ShortName = "XBOXONE", IsBanned = true, HasDat = true)]
        MicrosoftXboxOne,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Microsoft Xbox Series X", ShortName = "XBOXSX", IsBanned = true, HasDat = true)]
        MicrosoftXboxSeriesXS,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "NEC PC Engine CD & TurboGrafx CD", ShortName = "PCE", HasCues = true, HasDat = true)]
        NECPCEngineCDTurboGrafxCD,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "NEC PC-FX & PC-FXGA", ShortName = "PC-FX", HasCues = true, HasDat = true)]
        NECPCFXPCFXGA,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Nintendo GameCube", ShortName = "GC", HasDat = true)]
        NintendoGameCube,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Nintendo-Sony Super NES CD-ROM System")]
        NintendoSonySuperNESCDROMSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Nintendo Wii", ShortName = "WII", HasDat = true)]
        NintendoWii,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Nintendo Wii U", ShortName = "WIIU", IsBanned = true, HasDat = true, HasKeys = true)]
        NintendoWiiU,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Panasonic 3DO Interactive Multiplayer", ShortName = "3DO", HasCues = true, HasDat = true)]
        Panasonic3DOInteractiveMultiplayer,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Philips CD-i", ShortName = "CDI", HasCues = true, HasDat = true)]
        PhilipsCDi,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Playmaji Polymega")]
        PlaymajiPolymega,

        [System(Category = SystemCategory.DiscBasedConsole, Available = false, LongName = "Pioneer LaserActive")]
        PioneerLaserActive,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sega Dreamcast", ShortName = "DC", HasCues = true, HasDat = true)]
        SegaDreamcast,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sega Mega CD & Sega CD", ShortName = "MCD", HasCues = true, HasDat = true)]
        SegaMegaCDSegaCD,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sega Saturn", ShortName = "SS", HasCues = true, HasDat = true)]
        SegaSaturn,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Neo Geo CD", ShortName = "NGCD", HasCues = true, HasDat = true)]
        SNKNeoGeoCD,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation", ShortName = "PSX", HasCues = true, HasDat = true, HasSbi = true)]
        SonyPlayStation,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation 2", ShortName = "PS2", HasCues = true, HasDat = true)]
        SonyPlayStation2,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation 3", ShortName = "PS3", HasCues = true, HasDat = true, HasKeys = true)]
        SonyPlayStation3,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation 4", ShortName = "PS4", IsBanned = true, HasDat = true)]
        SonyPlayStation4,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation 5", ShortName = "PS5", IsBanned = true, HasDat = true)]
        SonyPlayStation5,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation Portable", ShortName = "PSP", HasDat = true)]
        SonyPlayStationPortable,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "VM Labs NUON", ShortName = "NUON", HasDat = true)]
        VMLabsNUON,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "VTech V.Flash & V.Smile Pro", ShortName = "VFLASH", HasCues = true, HasDat = true)]
        VTechVFlashVSmilePro,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "ZAPiT Games Game Wave Family Entertainment System", ShortName = "GAMEWAVE", HasDat = true)]
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

        [System(Category = SystemCategory.Computer, LongName = "Acorn Archimedes", ShortName = "ARCH", HasCues = true, HasDat = true)]
        AcornArchimedes,

        [System(Category = SystemCategory.Computer, LongName = "Apple Macintosh", ShortName = "MAC", HasCues = true, HasDat = true, HasSbi = true)]
        AppleMacintosh,

        [System(Category = SystemCategory.Computer, LongName = "Commodore Amiga CD", ShortName = "ACD", HasCues = true, HasDat = true)]
        CommodoreAmigaCD,

        [System(Category = SystemCategory.Computer, LongName = "Fujitsu FM Towns series", ShortName = "FMT", HasCues = true, HasDat = true)]
        FujitsuFMTownsseries,

        [System(Category = SystemCategory.Computer, LongName = "IBM PC compatible", ShortName = "PC", HasCues = true, HasDat = true, HasSbi = true)]
        IBMPCcompatible,

        [System(Category = SystemCategory.Computer, LongName = "NEC PC-88 series", ShortName = "PC-88", HasCues = true, HasDat = true)]
        NECPC88series,

        [System(Category = SystemCategory.Computer, LongName = "NEC PC-98 series", ShortName = "PC-98", HasCues = true, HasDat = true)]
        NECPC98series,

        [System(Category = SystemCategory.Computer, LongName = "Sharp X68000", ShortName = "X68K", HasCues = true, HasDat = true)]
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

        [System(Category = SystemCategory.Arcade, LongName = "funworld Photo Play", ShortName = "FPP", HasCues = true, HasDat = true)]
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

        [System(Category = SystemCategory.Arcade, LongName = "Incredible Technologies Eagle", ShortName = "ITE", HasCues = true, HasDat = true)]
        IncredibleTechnologiesEagle,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Incredible Technologies PC-based Systems")]
        IncredibleTechnologiesVarious,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "JVL iTouch")]
        JVLiTouch,

        [System(Category = SystemCategory.Arcade, LongName = "Konami e-Amusement", ShortName = "KEA", HasCues = true, HasDat = true)]
        KonamieAmusement,

        [System(Category = SystemCategory.Arcade, LongName = "Konami FireBeat", ShortName = "KFB", HasCues = true, HasDat = true)]
        KonamiFireBeat,

        [System(Category = SystemCategory.Arcade, LongName = "Konami M2", ShortName = "KM2", IsBanned = true, HasCues = true, HasDat = true)]
        KonamiM2,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Konami Python")]
        KonamiPython,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Konami Python 2")]
        KonamiPython2,

        [System(Category = SystemCategory.Arcade, LongName = "Konami System 573", ShortName = "KS573", HasCues = true, HasDat = true)]
        KonamiSystem573,

        [System(Category = SystemCategory.Arcade, LongName = "Konami System GV", ShortName = "KSGV", HasCues = true, HasDat = true)]
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

        [System(Category = SystemCategory.Arcade, LongName = "Namco · Sega · Nintendo Triforce", ShortName = "TRF", HasCues = true, HasDat = true)]
        NamcoSegaNintendoTriforce,

        [System(Category = SystemCategory.Arcade, LongName = "Namco System 12", ShortName = "ns12")]
        NamcoSystem12,

        [System(Category = SystemCategory.Arcade, LongName = "Namco System 246 / System 256", ShortName = "NS246", HasCues = true, HasDat = true)]
        NamcoSystem246256,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "New Jatre CD-i")]
        NewJatreCDi,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Nichibutsu High Rate System")]
        NichibutsuHighRateSystem,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Nichibutsu Super CD")]
        NichibutsuSuperCD,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Nichibutsu X-Rate System")]
        NichibutsuXRateSystem,

        [System(Category = SystemCategory.Arcade, LongName = "Panasonic M2", ShortName = "M2", IsBanned = true, HasCues = true, HasDat = true)]
        PanasonicM2,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "PhotoPlay PC-based Systems")]
        PhotoPlayVarious,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Raw Thrills PC-based Systems")]
        RawThrillsVarious,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega ALLS")]
        SegaALLS,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Chihiro", ShortName = "CHIHIRO", HasCues = true, HasDat = true)]
        SegaChihiro,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega Europa-R")]
        SegaEuropaR,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Lindbergh", ShortName = "LINDBERGH", HasDat = true)]
        SegaLindbergh,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Naomi", ShortName = "NAOMI", HasCues = true, HasDat = true)]
        SegaNaomi,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Naomi 2", ShortName = "NAOMI2", HasCues = true, HasDat = true)]
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

        [System(Category = SystemCategory.Arcade, LongName = "Sega RingEdge", ShortName = "SRE", HasDat = true)]
        SegaRingEdge,

        [System(Category = SystemCategory.Arcade, LongName = "Sega RingEdge 2", ShortName = "SRE2", HasDat = true)]
        SegaRingEdge2,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega RingWide")]
        SegaRingWide,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega System 32")]
        SegaSystem32,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Titan Video", ShortName = "stv")]
        SegaTitanVideo,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Seibu CATS System")]
        SeibuCATSSystem,

        [System(Category = SystemCategory.Arcade, LongName = "TAB-Austria Quizard", ShortName = "QUIZARD", HasCues = true, HasDat = true)]
        TABAustriaQuizard,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Tsunami TsuMo Multi-Game Motion System")]
        TsunamiTsuMoMultiGameMotionSystem,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "UltraCade PC-based Systems")]
        UltraCade,

        // End of arcade section delimiter
        MarkerArcadeEnd,

        #endregion

        #region Other

        [System(Category = SystemCategory.Other, LongName = "Audio CD", ShortName = "AUDIO-CD", IsBanned = true, HasCues = true, HasDat = true)]
        AudioCD,

        [System(Category = SystemCategory.Other, LongName = "BD-Video", ShortName = "BD-VIDEO", IsBanned = true, HasDat = true)]
        BDVideo,

        [System(Category = SystemCategory.Other, Available = false, LongName = "DVD-Audio")]
        DVDAudio,

        [System(Category = SystemCategory.Other, LongName = "DVD-Video", ShortName = "DVD-VIDEO", IsBanned = true, HasDat = true)]
        DVDVideo,

        [System(Category = SystemCategory.Other, LongName = "Enhanced CD", ShortName = "ENHANCED-CD", IsBanned = true)]
        EnhancedCD,

        [System(Category = SystemCategory.Other, LongName = "HD DVD-Video", ShortName = "HDDVD-VIDEO", IsBanned = true, HasDat = true)]
        HDDVDVideo,

        [System(Category = SystemCategory.Other, LongName = "Navisoft Naviken 2.1", ShortName = "NAVI21", IsBanned = true, HasCues = true, HasDat = true)]
        NavisoftNaviken21,

        [System(Category = SystemCategory.Other, LongName = "Palm OS", ShortName = "PALM", HasCues = true, HasDat = true)]
        PalmOS,

        [System(Category = SystemCategory.Other, LongName = "Photo CD", ShortName = "PHOTO-CD", HasCues = true, HasDat = true)]
        PhotoCD,

        [System(Category = SystemCategory.Other, LongName = "PlayStation GameShark Updates", ShortName = "PSXGS", HasCues = true, HasDat = true)]
        PlayStationGameSharkUpdates,

        [System(Category = SystemCategory.Other, LongName = "Pocket PC", ShortName = "PPC", HasCues = true, HasDat = true)]
        PocketPC,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Psion")]
        Psion,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Rainbow Disc")]
        RainbowDisc,

        [System(Category = SystemCategory.Other, LongName = "Sega Prologue 21 Multimedia Karaoke System", ShortName = "SP21", HasCues = true, HasDat = true)]
        SegaPrologue21MultimediaKaraokeSystem,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Sharp Zaurus")]
        SharpZaurus,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Sony Electronic Book")]
        SonyElectronicBook,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Super Audio CD")]
        SuperAudioCD,

        [System(Category = SystemCategory.Other, LongName = "Tao iKTV", ShortName = "IKTV")]
        TaoiKTV,

        [System(Category = SystemCategory.Other, LongName = "Tomy Kiss-Site", ShortName = "KSITE", HasCues = true, HasDat = true)]
        TomyKissSite,

        [System(Category = SystemCategory.Other, LongName = "Video CD", ShortName = "VCD", IsBanned = true, HasCues = true, HasDat = true)]
        VideoCD,

        // End of other section delimiter
        MarkerOtherEnd,

        #endregion
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
}
