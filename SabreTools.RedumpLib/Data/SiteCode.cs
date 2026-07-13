namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// Represents a single site code
    /// </summary>
    public sealed class SiteCode
    {
        #region Properties

        /// <summary>
        /// HTML tag representing the code
        /// </summary>
        public readonly string HTML;

        /// <summary>
        /// Short code, if it exists
        /// </summary>
        public readonly string? Code;

        /// <summary>
        /// Indicates if a site code is boolean
        /// </summary>
        public readonly bool IsBoolean;

        /// <summary>
        /// Indicates if the site code can go in the comments section
        /// </summary>
        public readonly bool IsCommentCode;

        /// <summary>
        /// Indicates if the site code can go in the contents section
        /// </summary>
        public readonly bool IsContentCode;

        /// <summary>
        /// Indicates if the site code can span multiple lines
        /// </summary>
        public readonly bool IsMultiLine;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private SiteCode(string html,
            string? code = null,
            bool isBoolean = false,
            bool isCommentCode = false,
            bool isContentCode = false,
            bool isMultiLine = false)
        {
            HTML = html;

            Code = code;
            IsBoolean = isBoolean;
            IsCommentCode = isCommentCode;
            IsContentCode = isContentCode;
            IsMultiLine = isMultiLine;
        }

        #endregion

        #region Static Instances

        public static readonly SiteCode AcclaimID = new("<b>Acclaim ID</b>:",
            code: "[T:ACC]",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode AccoladeID = new("<b>Accolade ID</b>:",
            code: "<b>Accolade ID</b>:",
            isCommentCode: true);

        public static readonly SiteCode ActivisionID = new("<b>Activision ID</b>:",
            code: "[T:ACT]",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode AdditionalBCAData = new("<b>Additional BCA Data</b>:",
            code: "<b>Additional BCA Data</b>:",
            isCommentCode: true);

        public static readonly SiteCode AlternativeTitle = new("<b>Alternative Title</b>:",
            code: "[T:ALT]",
            isCommentCode: true);

        public static readonly SiteCode AlternativeForeignTitle = new("<b>Alternative Foreign Title</b>:",
            code: "[T:ALTF]",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode Applications = new("<b>Applications</b>:",
            code: "<b>Applications</b>:",
            isContentCode: true);

        public static readonly SiteCode BandaiID = new("<b>Bandai ID</b>:",
            code: "[T:BID]",
            isCommentCode: true);

        public static readonly SiteCode BBFCRegistrationNumber = new("<b>BBFC Reg. No.</b>:",
            code: "[T:BBFC]",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode BethesdaID = new("<b>Bethesda ID</b>:",
            code: "<b>Bethesda ID</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode CDProjektID = new("<b>CD Projekt ID</b>:",
            code: "<b>CD Projekt ID</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode CompatibleOS = new("<b>Compatible OS</b>:",
            code: "<b>Compatible OS</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode CoverID = new("<b>Cover ID</b>:",
            code: "<b>Cover ID</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode DiceMultimedia = new("<b>Dice Multimedia</b>:",
            code: "<b>Dice Multimedia</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode DiscHologramID = new("<b>Disc Hologram ID</b>:",
            code: "<b>Disc Hologram ID</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode DiscID = new("<b>Disc ID</b>:",
            code: "<b>Disc ID</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode DiscTitleNonLatin = new("<b>Disc Title (non-Latin)</b>:",
            code: "<b>Disc Title (non-Latin)</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode DisneyInteractiveID = new("<b>Disney Interactive ID</b>:",
            code: "<b>Disney Interactive ID</b>",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode DMIHash = new("<b>DMI</b>:",
            code: "<b>DMI</b>:",
            isCommentCode: true);

        public static readonly SiteCode DNASDiscID = new("<b>DNAS Disc ID</b>:",
            code: "[T:DNAS]",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode EditionNonLatin = new("<b>Edition (non-Latin)</b>:",
            code: "<b>Edition (non-Latin)</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode EidosID = new("<b>Eidos ID</b>:",
            code: "<b>Eidos ID</b>:",
            isCommentCode: true);

        public static readonly SiteCode ElectronicArtsID = new("<b>Electronic Arts ID</b>:",
            code: "[T:EAID]",
            isCommentCode: true);

        public static readonly SiteCode Extras = new("<b>Extras</b>:",
            code: "[T:X]",
            isContentCode: true,
            isMultiLine: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode Filename = new("<b>Filename</b>:",
            code: "<b>Filename</b>:",
            isCommentCode: true,
            isMultiLine: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode FocusMultimedia = new("<b>Focus Multimedia</b>:",
            code: "<b>Focus Multimedia</b>:",
            isCommentCode: true);

        public static readonly SiteCode FoxInteractiveID = new("<b>Fox Interactive ID</b>:",
            code: "[T:FIID]",
            isCommentCode: true);

        public static readonly SiteCode GameFootage = new("<b>Game Footage</b>:",
            code: "[T:GF]",
            isContentCode: true,
            isMultiLine: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode Games = new("<b>Games</b>:",
            code: "<b>Games</b>:",
            isContentCode: true,
            isMultiLine: true);

        public static readonly SiteCode Genre = new("<b>Genre</b>:",
            code: "[T:G]",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode GSPSoftware = new("<b>GSP Software</b>:",
            code: "<b>GSP Software</b>:",
            isCommentCode: true);

        public static readonly SiteCode GTInteractiveID = new("<b>GT Interactive ID</b>:",
            code: "[T:GTID]",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode HighSierraVolumeDescriptor = new("<b>High Sierra Volume Descriptor</b>:",
            code: "<b>High Sierra Volume Descriptor</b>:",
            isCommentCode: true,
            isMultiLine: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode InternalName = new("<b>Internal Name</b>:",
            code: "<b>Internal Name</b>:",
            isCommentCode: true);

        public static readonly SiteCode InternalSerialName = new("<b>Internal Serial</b>:",
            code: "[T:ISN]",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode InterplayID = new("<b>Interplay ID</b>:",
            code: "<b>Interplay ID</b>:",
            isCommentCode: true);

        public static readonly SiteCode ISBN = new("<b>ISBN</b>:",
            code: "[T:ISBN]",
            isCommentCode: true);

        public static readonly SiteCode ISSN = new("<b>ISSN</b>:",
            code: "[T:ISSN]",
            isCommentCode: true);

        public static readonly SiteCode JASRACID = new("<b>JASRAC ID</b>:",
            code: "[T:JID]",
            isCommentCode: true);

        public static readonly SiteCode KingRecordsID = new("<b>King Records ID</b>:",
            code: "[T:KIRZ]",
            isCommentCode: true);

        public static readonly SiteCode KoeiID = new("<b>Koei ID</b>:",
            code: "[T:KOEI]",
            isCommentCode: true);

        public static readonly SiteCode KonamiID = new("<b>Konami ID</b>:",
            code: "[T:KID]",
            isCommentCode: true);

        public static readonly SiteCode LucasArtsID = new("<b>Lucas Arts ID</b>:",
            code: "[T:LAID]",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode MicrosoftID = new("<b>Microsoft ID</b>:",
            code: "<b>Microsoft ID</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode Multisession = new("<b>Multisession</b>:",
            code: "<b>Multisession</b>:",
            isCommentCode: true,
            isMultiLine: true);

        public static readonly SiteCode NaganoID = new("<b>Nagano ID</b>:",
            code: "[T:NGID]",
            isCommentCode: true);

        public static readonly SiteCode NamcoID = new("<b>Namco ID</b>:",
            code: "[T:NID]",
            isCommentCode: true);

        public static readonly SiteCode NetYarozeGames = new("<b>Net Yaroze Games</b>:",
            code: "[T:NYG]",
            isContentCode: true,
            isMultiLine: true);

        public static readonly SiteCode NipponIchiSoftwareID = new("<b>Nippon Ichi Software ID</b>:",
            code: "[T:NPS]",
            isCommentCode: true);

        public static readonly SiteCode OriginID = new("<b>Origin ID</b>:",
            code: "[T:OID]",
            isCommentCode: true);

        public static readonly SiteCode Patches = new("<b>Patches</b>:",
            code: "[T:P]",
            isContentCode: true,
            isMultiLine: true);

        public static readonly SiteCode PCMacHybrid = new("PC/Mac Hybrid",
            code: "PC/Mac Hybrid",
            isBoolean: true,
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode PFIHash = new("<b>PFI</b>:",
            code: "<b>PFI</b>:",
            isCommentCode: true);

        public static readonly SiteCode PlayableDemos = new("<b>Playable Demos</b>:",
            code: "[T:PD]",
            isContentCode: true,
            isMultiLine: true);

        public static readonly SiteCode PonyCanyonID = new("<b>Pony Canyon ID</b>:",
            code: "[T:PCID]",
            isCommentCode: true);

        public static readonly SiteCode PostgapType = new("<b>Postgap type</b>: Form 2",
            code: "[T:PT2]",
            isBoolean: true,
            isCommentCode: true);

        public static readonly SiteCode PPN = new("<b>PPN</b>:",
            code: "[T:PPN]",
            isCommentCode: true);

        // This doesn't have a site tag for some systems yet
        public static readonly SiteCode Protection = new("<b>Protection</b>:",
            code: "<b>Protection</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode RingPerfectAudioOffset = new("<b>Ring Perfect Audio Offset</b>:",
            code: "<b>Ring Perfect Audio Offset</b>:",
            isCommentCode: true);

        public static readonly SiteCode RollingDemos = new("<b>Rolling Demos</b>:",
            code: "[T:RD]",
            isContentCode: true,
            isMultiLine: true);

        public static readonly SiteCode Savegames = new("<b>Savegames</b>:",
            code: "[T:SG]",
            isContentCode: true,
            isMultiLine: true);

        public static readonly SiteCode SegaID = new("<b>Sega ID</b>:",
            code: "[T:SID]",
            isCommentCode: true);

        public static readonly SiteCode SelenID = new("<b>Selen ID</b>:",
            code: "[T:SNID]",
            isCommentCode: true);

        public static readonly SiteCode Series = new("<b>Series</b>:",
            code: "[T:S]",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode SierraID = new("<b>Sierra ID</b>:",
            code: "<b>Sierra ID</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode SSHash = new("<b>SS</b>:",
            code: "<b>SS</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode SSVersion = new("<b>SS version</b>:",
            code: "<b>SS version</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode SteamAppID = new("<b>Steam AppID</b>:",
            code: "<b>Steam App ID</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode SteamCsmCsdDepotID = new("<b>Steam Depot ID (.sis/.csm/.csd)</b>:",
            code: "<b>Steam Depot ID (.sis/.csm/.csd)</b>:",
            isContentCode: true,
            isMultiLine: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode SteamSimSidDepotID = new("<b>Steam Depot ID (.sis/.sim/.sid)</b>:",
            code: "<b>Steam Depot ID (.sis/.sim/.sid)</b>:",
            isContentCode: true,
            isMultiLine: true);

        public static readonly SiteCode TaitoID = new("<b>Taito ID</b>:",
            code: "[T:TID]",
            isCommentCode: true);

        public static readonly SiteCode TechDemos = new("<b>Tech Demos</b>:",
            code: "[T:TD]",
            isContentCode: true,
            isMultiLine: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode TitleID = new("<b>Title ID</b>:",
            code: "<b>Title ID</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode TwoKGamesID = new("<b>2K Games ID</b>:",
            code: "<b>2K Games ID</b>:",
            isCommentCode: true);

        public static readonly SiteCode UbisoftID = new("<b>Ubisoft ID</b>:",
            code: "[T:UID]",
            isCommentCode: true);

        public static readonly SiteCode ValveID = new("<b>Valve ID</b>:",
            code: "[T:VID]",
            isCommentCode: true);

        public static readonly SiteCode VFCCode = new("<b>VFC code</b>:",
            code: "[T:VFC]",
            isCommentCode: true);

        public static readonly SiteCode Videos = new("<b>Videos</b>:",
            code: "[T:V]",
            isContentCode: true,
            isMultiLine: true);

        public static readonly SiteCode VolumeLabel = new("<b>Volume Label</b>:",
            code: "[T:VOL]",
            isCommentCode: true);

        public static readonly SiteCode VCD = new("<b>V-CD</b>",
            code: "[T:VCD]",
            isBoolean: true,
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode XeMID = new("<b>XeMID</b>:",
            code: "<b>XeMID</b>:",
            isCommentCode: true);

        // This doesn't have a site tag yet
        public static readonly SiteCode XMID = new("<b>XMID</b>:",
            code: "<b>XMID</b>:",
            isCommentCode: true);

        #endregion

        #region Static Collections

        /// <summary>
        /// All site codes
        /// </summary>
        /// TODO: Figure out how to remove delimiters
        public static readonly SiteCode[] AllSiteCodes =
        [
            AcclaimID,
            AccoladeID,
            ActivisionID,
            AdditionalBCAData,
            AlternativeTitle,
            AlternativeForeignTitle,
            Applications,

            BandaiID,
            BBFCRegistrationNumber,
            BethesdaID,

            CDProjektID,
            CompatibleOS,
            CoverID,

            DiceMultimedia,
            DiscHologramID,
            DiscID,
            DiscTitleNonLatin,
            DisneyInteractiveID,
            DMIHash,
            DNASDiscID,

            EditionNonLatin,
            EidosID,
            ElectronicArtsID,
            Extras,

            Filename,
            FocusMultimedia,
            FoxInteractiveID,

            GameFootage,
            Games,
            Genre,
            GSPSoftware,
            GTInteractiveID,

            HighSierraVolumeDescriptor,

            InternalName,
            InternalSerialName,
            InterplayID,
            ISBN,
            ISSN,

            JASRACID,

            KingRecordsID,
            KoeiID,
            KonamiID,

            LucasArtsID,

            MicrosoftID,
            Multisession,

            NaganoID,
            NamcoID,
            NetYarozeGames,

            NipponIchiSoftwareID,

            OriginID,

            Patches,
            PCMacHybrid,
            PFIHash,
            PlayableDemos,
            PonyCanyonID,
            PostgapType,
            PPN,
            Protection,

            RingPerfectAudioOffset,
            RollingDemos,

            Savegames,
            SegaID,
            SelenID,
            Series,
            SierraID,
            SSHash,
            SSVersion,
            SteamAppID,
            SteamCsmCsdDepotID,
            SteamSimSidDepotID,

            TaitoID,
            TechDemos,
            TitleID,
            TwoKGamesID,

            UbisoftID,

            ValveID,
            VFCCode,
            Videos,
            VolumeLabel,
            VCD,

            XeMID,
            XMID,
        ];

        #endregion
    }
}
