using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.RedumpOrg.Attributes;
using HumanReadableAttribute = SabreTools.RedumpLib.Attributes.HumanReadableAttribute;

namespace SabreTools.RedumpLib.RedumpOrg
{
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
    /// List of all systems defined in Redump.org
    /// </summary>
    public enum PhysicalSystem
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

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Apple/Bandai Pippin", ShortName = "pippin", HasCues = true, HasDat = true)]
        AppleBandaiPippin,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Atari Jaguar CD Interactive Multimedia System", ShortName = "ajcd", HasCues = true, HasDat = true)]
        AtariJaguarCDInteractiveMultimediaSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Bandai Playdia Quick Interactive System", ShortName = "qis", HasCues = true, HasDat = true)]
        BandaiPlaydiaQuickInteractiveSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Commodore Amiga CD32", ShortName = "cd32", HasCues = true, HasDat = true)]
        CommodoreAmigaCD32,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Commodore Amiga CDTV", ShortName = "cdtv", HasCues = true, HasDat = true)]
        CommodoreAmigaCDTV,

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

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Nintendo Wii", ShortName = "wii", HasDat = true)]
        NintendoWii,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Nintendo Wii U", ShortName = "wiiu", IsBanned = true, HasDat = true, HasKeys = true)]
        NintendoWiiU,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "3DO Interactive Multiplayer", ShortName = "3do", HasCues = true, HasDat = true)]
        Panasonic3DOInteractiveMultiplayer,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Philips CD-i", ShortName = "cdi", HasCues = true, HasDat = true)]
        PhilipsCDi,

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

        #region Computers

        [System(Category = SystemCategory.Computer, LongName = "Acorn Archimedes & Risc PC", ShortName = "arch", HasCues = true, HasDat = true)]
        AcornArchimedesAndRiscPC,

        [System(Category = SystemCategory.Computer, LongName = "Apple Macintosh", ShortName = "mac", HasCues = true, HasDat = true, HasLsd = true, HasSbi = true)]
        AppleMacintosh,

        [System(Category = SystemCategory.Computer, LongName = "Commodore Amiga CD", ShortName = "acd", HasCues = true, HasDat = true)]
        CommodoreAmigaCD,

        [System(Category = SystemCategory.Computer, LongName = "Fujitsu FM Towns series", ShortName = "fmt", HasCues = true, HasDat = true)]
        FujitsuFMTownsSeries,

        [System(Category = SystemCategory.Computer, LongName = "IBM PC compatible", ShortName = "pc", HasCues = true, HasDat = true, HasLsd = true, HasSbi = true)]
        IBMPCcompatible,

        [System(Category = SystemCategory.Computer, LongName = "NEC PC-88 series", ShortName = "pc-88", HasCues = true, HasDat = true)]
        NECPC88Series,

        [System(Category = SystemCategory.Computer, LongName = "NEC PC-98 series", ShortName = "pc-98", HasCues = true, HasDat = true)]
        NECPC98Series,

        [System(Category = SystemCategory.Computer, LongName = "Sharp X68000", ShortName = "x68k", HasCues = true, HasDat = true)]
        SharpX68000,

        // End of computer section delimiter
        MarkerComputerEnd,

        #endregion

        #region Arcade

        [System(Category = SystemCategory.Arcade, LongName = "Funworld Photo Play", ShortName = "fpp", HasCues = true, HasDat = true)]
        FunworldPhotoPlay,

        [System(Category = SystemCategory.Arcade, LongName = "Incredible Technologies Eagle", ShortName = "ite", HasCues = true, HasDat = true)]
        IncredibleTechnologiesEagle,

        [System(Category = SystemCategory.Arcade, LongName = "Konami e-Amusement", ShortName = "kea", HasCues = true, HasDat = true)]
        KonamieAmusement,

        [System(Category = SystemCategory.Arcade, LongName = "Konami FireBeat", ShortName = "kfb", HasCues = true, HasDat = true)]
        KonamiFireBeat,

        [System(Category = SystemCategory.Arcade, LongName = "Konami M2", ShortName = "km2", IsBanned = true, HasCues = true, HasDat = true)]
        KonamiM2,

        [System(Category = SystemCategory.Arcade, LongName = "Konami System 573", ShortName = "ks573", HasCues = true, HasDat = true)]
        KonamiSystem573,

        [System(Category = SystemCategory.Arcade, LongName = "Konami System GV", ShortName = "ksgv", HasCues = true, HasDat = true)]
        KonamiSystemGV,

        [System(Category = SystemCategory.Arcade, LongName = "Konami Twinkle", ShortName = "kt")]
        KonamiTwinkle,

        [System(Category = SystemCategory.Arcade, LongName = "Namco · Sega · Nintendo Triforce", ShortName = "trf", HasCues = true, HasDat = true, HasGdi = true)]
        NamcoSegaNintendoTriforce,

        [System(Category = SystemCategory.Arcade, LongName = "Namco System 12", ShortName = "ns12")]
        NamcoSystem12,

        [System(Category = SystemCategory.Arcade, LongName = "Namco System 246 / System 256", ShortName = "ns246", HasCues = true, HasDat = true)]
        NamcoSystem246,

        [System(Category = SystemCategory.Arcade, LongName = "Panasonic M2", ShortName = "m2", IsBanned = true, HasCues = true, HasDat = true)]
        PanasonicM2,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Chihiro", ShortName = "chihiro", HasCues = true, HasDat = true, HasGdi = true)]
        SegaChihiro,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Lindbergh", ShortName = "lindbergh", HasDat = true)]
        SegaLindbergh,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Naomi", ShortName = "naomi", HasCues = true, HasDat = true, HasGdi = true)]
        SegaNaomi,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Naomi 2", ShortName = "naomi2", HasCues = true, HasDat = true, HasGdi = true)]
        SegaNaomi2,

        [System(Category = SystemCategory.Arcade, LongName = "Sega RingEdge", ShortName = "sre", HasDat = true)]
        SegaRingEdge,

        [System(Category = SystemCategory.Arcade, LongName = "Sega RingEdge 2", ShortName = "sre2", HasDat = true)]
        SegaRingEdge2,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Titan Video", ShortName = "stv")]
        SegaTitanVideo,

        [System(Category = SystemCategory.Arcade, LongName = "TAB-Austria Quizard", ShortName = "quizard", HasCues = true, HasDat = true)]
        TABAustriaQuizard,

        // End of arcade section delimiter
        MarkerArcadeEnd,

        #endregion

        #region Other

        [System(Category = SystemCategory.Other, LongName = "Audio CD", ShortName = "audio-cd", IsBanned = true, HasCues = true, HasDat = true)]
        AudioCD,

        [System(Category = SystemCategory.Other, LongName = "BD-Video", ShortName = "bd-video", IsBanned = true, HasDat = true)]
        BDVideo,

        [System(Category = SystemCategory.Other, LongName = "DVD-Video", ShortName = "dvd-video", IsBanned = true, HasDat = true)]
        DVDVideo,

        [System(Category = SystemCategory.Other, LongName = "Enhanced CD", ShortName = "enhanced-cd", IsBanned = true, HasCues = true, HasDat = true)]
        EnhancedCD,

        [System(Category = SystemCategory.Other, LongName = "HD DVD-Video", ShortName = "hddvd-video", IsBanned = true, HasDat = true)]
        HDDVDVideo,

        [System(Category = SystemCategory.Other, LongName = "Navisoft Naviken 2.1", ShortName = "navi21", IsBanned = true, HasCues = true, HasDat = true)]
        NavisoftNaviken,

        [System(Category = SystemCategory.Other, LongName = "Palm OS", ShortName = "palm", HasCues = true, HasDat = true)]
        PalmOS,

        [System(Category = SystemCategory.Other, LongName = "Photo CD", ShortName = "photo-cd", HasCues = true, HasDat = true)]
        PhotoCD,

        [System(Category = SystemCategory.Other, LongName = "PlayStation GameShark Updates", ShortName = "psxgs", HasCues = true, HasDat = true)]
        PlayStationGameSharkUpdates,

        [System(Category = SystemCategory.Other, LongName = "Pocket PC", ShortName = "ppc", HasCues = true, HasDat = true)]
        PocketPC,

        [System(Category = SystemCategory.Other, LongName = "Sega Prologue 21 Multimedia Karaoke System", ShortName = "sp21", HasCues = true, HasDat = true)]
        SegaPrologue21MultimediaKaraokeSystem,

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
    /// List of all regions defined in Redump.org
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
    /// </remarks>
    public enum Region
    {
        [HumanReadable(LongName = "Argentina", ShortName = "Ar")]
        Argentina,

        [HumanReadable(LongName = "Asia", ShortName = "A")]
        Asia,

        [HumanReadable(LongName = "Asia, Europe", ShortName = "A,E")]
        AsiaEurope,

        [HumanReadable(LongName = "Asia, USA", ShortName = "A,U")]
        AsiaUSA,

        [HumanReadable(LongName = "Australia", ShortName = "Au")]
        Australia,

        [HumanReadable(LongName = "Australia, Germany", ShortName = "Au,G")]
        AustraliaGermany,

        [HumanReadable(LongName = "Australia, New Zealand", ShortName = "Au,Nz")]
        AustraliaNewZealand,

        [HumanReadable(LongName = "Austria", ShortName = "At")]
        Austria,

        [HumanReadable(LongName = "Austria, Switzerland", ShortName = "At,Ch")]
        AustriaSwitzerland,

        [HumanReadable(LongName = "Belarus", ShortName = "By")]
        Belarus,

        [HumanReadable(LongName = "Belgium", ShortName = "Be")]
        Belgium,

        [HumanReadable(LongName = "Belgium, Netherlands", ShortName = "Be,N")]
        BelgiumNetherlands,

        [HumanReadable(LongName = "Brazil", ShortName = "B")]
        Brazil,

        [HumanReadable(LongName = "Bulgaria", ShortName = "Bg")]
        Bulgaria,

        [HumanReadable(LongName = "Canada", ShortName = "Ca")]
        Canada,

        [HumanReadable(LongName = "China", ShortName = "C")]
        China,

        [HumanReadable(LongName = "Croatia", ShortName = "Hr")]
        Croatia,

        [HumanReadable(LongName = "Czech", ShortName = "Cz")]
        Czech,

        [HumanReadable(LongName = "Denmark", ShortName = "Dk")]
        Denmark,

        [HumanReadable(LongName = "Estonia", ShortName = "Ee")]
        Estonia,

        [HumanReadable(LongName = "Europe", ShortName = "E")]
        Europe,

        [HumanReadable(LongName = "Europe, Asia", ShortName = "E,A")]
        EuropeAsia,

        [HumanReadable(LongName = "Europe, Australia", ShortName = "E,Au")]
        EuropeAustralia,

        [HumanReadable(LongName = "Europe, Canada", ShortName = "E,Ca")]
        EuropeCanada,

        [HumanReadable(LongName = "Europe, Germany", ShortName = "E,G")]
        EuropeGermany,

        [HumanReadable(LongName = "Export", ShortName = "Ex")]
        Export,

        [HumanReadable(LongName = "Finland", ShortName = "Fi")]
        Finland,

        [HumanReadable(LongName = "France", ShortName = "F")]
        France,

        [HumanReadable(LongName = "France, Spain", ShortName = "F,S")]
        FranceSpain,

        [HumanReadable(LongName = "Germany", ShortName = "G")]
        Germany,

        [HumanReadable(LongName = "Greater China", ShortName = "GC")]
        GreaterChina,

        [HumanReadable(LongName = "Greece", ShortName = "Gr")]
        Greece,

        [HumanReadable(LongName = "Hungary", ShortName = "H")]
        Hungary,

        [HumanReadable(LongName = "Iceland", ShortName = "Is")]
        Iceland,

        [HumanReadable(LongName = "India", ShortName = "In")]
        India,

        [HumanReadable(LongName = "Ireland", ShortName = "Ie")]
        Ireland,

        [HumanReadable(LongName = "Israel", ShortName = "Il")]
        Israel,

        [HumanReadable(LongName = "Italy", ShortName = "I")]
        Italy,

        [HumanReadable(LongName = "Japan", ShortName = "J")]
        Japan,

        [HumanReadable(LongName = "Japan, Asia", ShortName = "J,A")]
        JapanAsia,

        [HumanReadable(LongName = "Japan, Europe", ShortName = "J,E")]
        JapanEurope,

        [HumanReadable(LongName = "Japan, Korea", ShortName = "J,K")]
        JapanKorea,

        [HumanReadable(LongName = "Japan, USA", ShortName = "J,U")]
        JapanUSA,

        [HumanReadable(LongName = "Korea", ShortName = "K")]
        Korea,

        [HumanReadable(LongName = "Latin America", ShortName = "LAm")]
        LatinAmerica,

        [HumanReadable(LongName = "Lithuania", ShortName = "Lt")]
        Lithuania,

        [HumanReadable(LongName = "Netherlands", ShortName = "N")]
        Netherlands,

        [HumanReadable(LongName = "New Zealand", ShortName = "Nz")]
        NewZealand,

        [HumanReadable(LongName = "Norway", ShortName = "No")]
        Norway,

        [HumanReadable(LongName = "Poland", ShortName = "P")]
        Poland,

        [HumanReadable(LongName = "Portugal", ShortName = "Pt")]
        Portugal,

        [HumanReadable(LongName = "Romania", ShortName = "Ro")]
        Romania,

        [HumanReadable(LongName = "Russia", ShortName = "R")]
        Russia,

        [HumanReadable(LongName = "Scandinavia", ShortName = "Sca")]
        Scandinavia,

        [HumanReadable(LongName = "Serbia", ShortName = "Rs")]
        Serbia,

        [HumanReadable(LongName = "Singapore", ShortName = "Sg")]
        Singapore,

        [HumanReadable(LongName = "Slovakia", ShortName = "Sk")]
        Slovakia,

        [HumanReadable(LongName = "South Africa", ShortName = "Za")]
        SouthAfrica,

        [HumanReadable(LongName = "Spain", ShortName = "S")]
        Spain,

        [HumanReadable(LongName = "Spain, Portugal", ShortName = "S,Pt")]
        SpainPortugal,

        [HumanReadable(LongName = "Sweden", ShortName = "Sw")]
        Sweden,

        [HumanReadable(LongName = "Switzerland", ShortName = "Ch")]
        Switzerland,

        [HumanReadable(LongName = "Taiwan", ShortName = "Tw")]
        Taiwan,

        [HumanReadable(LongName = "Thailand", ShortName = "Th")]
        Thailand,

        [HumanReadable(LongName = "Turkey", ShortName = "Tr")]
        Turkey,

        [HumanReadable(LongName = "United Arab Emirates", ShortName = "Ae")]
        UnitedArabEmirates,

        [HumanReadable(LongName = "UK", ShortName = "Uk")]
        UK,

        [HumanReadable(LongName = "UK, Australia", ShortName = "Uk,Au")]
        UKAustralia,

        [HumanReadable(LongName = "Ukraine", ShortName = "Ua")]
        Ukraine,

        [HumanReadable(LongName = "USA", ShortName = "U")]
        USA,

        [HumanReadable(LongName = "USA, Asia", ShortName = "U,A")]
        USAAsia,

        [HumanReadable(LongName = "USA, Australia", ShortName = "U,Au")]
        USAAustralia,

        [HumanReadable(LongName = "USA, Brazil", ShortName = "U,B")]
        USABrazil,

        [HumanReadable(LongName = "USA, Canada", ShortName = "U,Ca")]
        USACanada,

        [HumanReadable(LongName = "USA, Europe", ShortName = "U,E")]
        USAEurope,

        [HumanReadable(LongName = "USA, Germany", ShortName = "U,G")]
        USAGermany,

        [HumanReadable(LongName = "USA, Japan", ShortName = "U,J")]
        USAJapan,

        [HumanReadable(LongName = "USA, Korea", ShortName = "U,K")]
        USAKorea,

        [HumanReadable(LongName = "World", ShortName = "W")]
        World,
    }
}
