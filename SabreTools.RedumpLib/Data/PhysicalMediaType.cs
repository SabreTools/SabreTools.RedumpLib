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

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private PhysicalMediaType(string name,
            bool available = true,
            string? code = null)
        {
            Name = name;

            Available = available;
            Code = code;
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
            code: "bdrom");

        public static readonly PhysicalMediaType CDROM = new("CD-ROM",
            code: "cdrom");

        public static readonly PhysicalMediaType DVD = new("DVD-ROM",
            code: "dvd");

        public static readonly PhysicalMediaType FloppyDisk = new("Floppy Disk",
            code: "fd");

        public static readonly PhysicalMediaType Floptical = new("Floptical",
            available: false,
            code: "floptical");

        public static readonly PhysicalMediaType GDROM = new("GD-ROM",
            code: "gdrom");

        public static readonly PhysicalMediaType HDDVD = new("HD-DVD-ROM",
            code: "hddvd");

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
            code: "gc");

        public static readonly PhysicalMediaType NintendoWiiOpticalDisc = new("Wii Optical Disc",
            code: "wii");

        public static readonly PhysicalMediaType NintendoWiiUOpticalDisc = new("Wii U Optical Disc",
            code: "wiiu");

        public static readonly PhysicalMediaType UMD = new("UMD",
            code: "umd");

        public static readonly PhysicalMediaType XGD = new("Xbox Game Disc",
            code: "xgd");

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
