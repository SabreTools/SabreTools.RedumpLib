using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.RedumpOrg.Data
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
