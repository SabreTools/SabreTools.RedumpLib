namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// Represents a single physical media type
    /// </summary>
    public sealed class PhysicalMediaType
    {
        #region Properties

        /// <summary>
        /// Human-readable name of the media type
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Media type is included in the site
        /// </summary>
        public readonly bool Available;

        /// <summary>
        /// Site short code, if it exists
        /// </summary>
        public readonly string? Code;

        /// <summary>
        /// Default media type mapping
        /// </summary>
        /// <remarks>This is used when converting from <see cref="PhysicalMediaType"/> to <see cref="MediaType"/></remarks>
        public readonly MediaType? DefaultMediaType;

        /// <summary>
        /// Media types that map to this <see cref="PhysicalMediaType"/>
        /// </summary>
        /// <remarks>This is used when converting from <see cref="MediaType"/> to <see cref="PhysicalMediaType"/></remarks>
        public readonly MediaType[] MappedMediaTypes;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private PhysicalMediaType(string name,
            bool available = true,
            string? code = null,
            MediaType? defaultMediaType = null,
            MediaType[]? mappedMediaTypes = null)
        {
            Name = name;

            Available = available;
            Code = code;
            DefaultMediaType = defaultMediaType;
            MappedMediaTypes = mappedMediaTypes ?? [];
        }

        #endregion

        #region Static Instances

        #region Punched Media

        public static readonly PhysicalMediaType ApertureCard = new("Aperture card",
            available: false,
            code: "aperture");

        public static readonly PhysicalMediaType JacquardLoomCard = new("Jacquard Loom card",
            available: false,
            code: "jacquard loom card");

        public static readonly PhysicalMediaType MagneticStripeCard = new("Magnetic stripe card",
            available: false,
            code: "magnetic stripe");

        public static readonly PhysicalMediaType OpticalPhonecard = new("Optical phonecard",
            available: false,
            code: "optical phonecard");

        public static readonly PhysicalMediaType PunchedCard = new("Punched card",
            available: false,
            code: "punchcard");

        public static readonly PhysicalMediaType PunchedTape = new("Punched tape",
            available: false,
            code: "punchtape");

        #endregion

        #region Tape

        public static readonly PhysicalMediaType Cassette = new("Cassette Tape",
            available: false,
            code: "cassette");

        public static readonly PhysicalMediaType DataCartridge = new("Data Tape Cartridge",
            available: false,
            code: "data cartridge");

        public static readonly PhysicalMediaType OpenReel = new("Open Reel Tape",
            available: false,
            code: "open reel");

        #endregion

        #region Magnetic and Optical

        public static readonly PhysicalMediaType BluRay = new("BD-ROM",
            code: "bdrom",
            defaultMediaType: MediaType.BD50,
            mappedMediaTypes: [MediaType.BD25, MediaType.BD33, MediaType.BD50, MediaType.BD66, MediaType.BD100, MediaType.BD128]);

        public static readonly PhysicalMediaType CDROM = new("CD-ROM",
            code: "cdrom",
            defaultMediaType: MediaType.CD,
            mappedMediaTypes: [MediaType.CD]);

        public static readonly PhysicalMediaType DVD = new("DVD-ROM",
            code: "dvd",
            defaultMediaType: MediaType.DVD9,
            mappedMediaTypes: [MediaType.DVD5, MediaType.DVD9]);

        public static readonly PhysicalMediaType FloppyDisk = new("Floppy Disk",
            code: "fd");

        public static readonly PhysicalMediaType Floptical = new("Floptical",
            available: false,
            code: "floptical");

        public static readonly PhysicalMediaType GDROM = new("GD-ROM",
            code: "gdrom",
            defaultMediaType: MediaType.GDROM,
            mappedMediaTypes: [MediaType.GDROM]);

        public static readonly PhysicalMediaType HDDVD = new("HD-DVD-ROM",
            code: "hddvd",
            defaultMediaType: MediaType.HDDVDSL,
            mappedMediaTypes: [MediaType.HDDVDSL, MediaType.HDDVDDL]);

        public static readonly PhysicalMediaType HardDisk = new("Hard Disk",
            code: "hdd");

        public static readonly PhysicalMediaType IomegaBernoulliDisk = new("Iomega Bernoulli Disk",
            available: false,
            code: "bernoulli");

        public static readonly PhysicalMediaType IomegaJaz = new("Iomega Jaz",
            available: false,
            code: "jaz");

        public static readonly PhysicalMediaType IomegaZip = new("Iomega Zip",
            available: false,
            code: "zip");

        // LD-ROM and LV-ROM variants
        public static readonly PhysicalMediaType LaserDisc = new("LD-ROM / LV-ROM",
            code: "ldrom");

        public static readonly PhysicalMediaType Nintendo64DD = new("64DD Disk",
            available: false,
            code: "64dd");

        public static readonly PhysicalMediaType NintendoFamicomDiskSystem = new("Famicom Disk System Disk",
            available: false,
            code: "fds");

        public static readonly PhysicalMediaType NintendoGameCubeGameDisc = new("GameCube Game Disc",
            code: "gc",
            defaultMediaType: MediaType.NintendoGameCubeGameDisc,
            mappedMediaTypes: [MediaType.NintendoGameCubeGameDisc]);

        public static readonly PhysicalMediaType NintendoWiiOpticalDisc = new("Wii Optical Disc",
            code: "wii",
            defaultMediaType: MediaType.NintendoWiiOpticalDiscDL,
            mappedMediaTypes: [MediaType.NintendoWiiOpticalDiscSL, MediaType.NintendoWiiOpticalDiscDL]);

        public static readonly PhysicalMediaType NintendoWiiUOpticalDisc = new("Wii U Optical Disc",
            code: "wiiu",
            defaultMediaType: MediaType.NintendoWiiUOpticalDiscSL,
            mappedMediaTypes: [MediaType.NintendoWiiUOpticalDiscSL]);

        public static readonly PhysicalMediaType UMD = new("UMD",
            code: "umd",
            defaultMediaType: MediaType.UMDDL,
            mappedMediaTypes: [MediaType.UMDSL, MediaType.UMDDL]);

        public static readonly PhysicalMediaType XGD = new("Xbox Game Disc",
            code: "xgd",
            defaultMediaType: MediaType.XGD,
            mappedMediaTypes: [MediaType.XGD]);

        #endregion

        #region Unsorted Formats

        public static readonly PhysicalMediaType Cartridge = new("Cartridge",
            available: false,
            code: "cart");

        public static readonly PhysicalMediaType CED = new("CED",
            available: false,
            code: "ced");

        public static readonly PhysicalMediaType CompactFlash = new("Compact Flash",
            available: false,
            code: "cf");

        public static readonly PhysicalMediaType MMC = new("MMC",
            available: false,
            code: "mmc");

        public static readonly PhysicalMediaType SDCard = new("SD Card",
            available: false,
            code: "sd");

        public static readonly PhysicalMediaType FlashDrive = new("Flash Drive",
            available: false,
            code: "fkd");

        #endregion

        #endregion

        #region Static Collections

        /// <summary>
        /// All media types
        /// </summary>
        public static readonly PhysicalMediaType[] AllMediaTypes =
        [
            #region Punched Media

            ApertureCard,
            JacquardLoomCard,
            MagneticStripeCard,
            OpticalPhonecard,
            PunchedCard,
            PunchedTape,

            #endregion

            #region Tape

            Cassette,
            DataCartridge,
            OpenReel,

            #endregion

            #region Magnetic and Optical

            BluRay,
            CDROM,
            DVD,
            FloppyDisk,
            Floptical,
            GDROM,
            HDDVD,
            HardDisk,
            IomegaBernoulliDisk,
            IomegaJaz,
            IomegaZip,
            LaserDisc,
            Nintendo64DD,
            NintendoFamicomDiskSystem,
            NintendoGameCubeGameDisc,
            NintendoWiiOpticalDisc,
            NintendoWiiUOpticalDisc,
            UMD,
            XGD,

            #endregion

            #region Unsorted Formats

            Cartridge,
            CED,
            CompactFlash,
            MMC,
            SDCard,
            FlashDrive,

            #endregion
        ];

        #endregion
    }
}
