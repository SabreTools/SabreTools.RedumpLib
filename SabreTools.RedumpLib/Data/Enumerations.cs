using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// All possible media types not bound to specific site limitations
    /// </summary>
    public enum PhysicalMediaType
    {
        [HumanReadable(Available = false, LongName = "Unknown", ShortName = "unknown")]
        NONE = 0,

        #region Punched Media

        [HumanReadable(Available = false, LongName = "Aperture card", ShortName = "aperture")]
        ApertureCard,

        [HumanReadable(Available = false, LongName = "Jacquard Loom card", ShortName = "jacquard loom card")]
        JacquardLoomCard,

        [HumanReadable(Available = false, LongName = "Magnetic stripe card", ShortName = "magnetic stripe")]
        MagneticStripeCard,

        [HumanReadable(Available = false, LongName = "Optical phonecard", ShortName = "optical phonecard")]
        OpticalPhonecard,

        [HumanReadable(Available = false, LongName = "Punched card", ShortName = "punchcard")]
        PunchedCard,

        [HumanReadable(Available = false, LongName = "Punched tape", ShortName = "punchtape")]
        PunchedTape,

        #endregion

        #region Tape

        [HumanReadable(Available = false, LongName = "Cassette Tape", ShortName = "cassette")]
        Cassette,

        [HumanReadable(Available = false, LongName = "Data Tape Cartridge", ShortName = "data cartridge")]
        DataCartridge,

        [HumanReadable(Available = false, LongName = "Open Reel Tape", ShortName = "open reel")]
        OpenReel,

        #endregion

        #region Disc / Disc

        [HumanReadable(LongName = "BD-ROM", ShortName = "bdrom")]
        BluRay,

        [HumanReadable(LongName = "CD-ROM", ShortName = "cdrom")]
        CDROM,

        [HumanReadable(LongName = "DVD-ROM", ShortName = "dvd")]
        DVD,

        [HumanReadable(LongName = "Floppy Disk", ShortName = "fd")]
        FloppyDisk,

        [HumanReadable(Available = false, LongName = "Floptical", ShortName = "floptical")]
        Floptical,

        [HumanReadable(LongName = "GD-ROM", ShortName = "gdrom")]
        GDROM,

        [HumanReadable(LongName = "HD-DVD-ROM", ShortName = "hddvd")]
        HDDVD,

        [HumanReadable(LongName = "Hard Disk", ShortName = "hdd")]
        HardDisk,

        [HumanReadable(Available = false, LongName = "Iomega Bernoulli Disk", ShortName = "bernoulli")]
        IomegaBernoulliDisk,

        [HumanReadable(Available = false, LongName = "Iomega Jaz", ShortName = "jaz")]
        IomegaJaz,

        [HumanReadable(Available = false, LongName = "Iomega Zip", ShortName = "zip")]
        IomegaZip,

        [HumanReadable(LongName = "LD-ROM / LV-ROM", ShortName = "ldrom")]
        LaserDisc, // LD-ROM and LV-ROM variants

        [HumanReadable(Available = false, LongName = "64DD Disk", ShortName = "64dd")]
        Nintendo64DD,

        [HumanReadable(Available = false, LongName = "Famicom Disk System Disk", ShortName = "fds")]
        NintendoFamicomDiskSystem,

        [HumanReadable(LongName = "GameCube Game Disc", ShortName = "gc")]
        NintendoGameCubeGameDisc,

        [HumanReadable(LongName = "Wii Optical Disc", ShortName = "wii")]
        NintendoWiiOpticalDisc,

        [HumanReadable(LongName = "Wii U Optical Disc", ShortName = "wiiu")]
        NintendoWiiUOpticalDisc,

        [HumanReadable(LongName = "UMD", ShortName = "umd")]
        UMD,

        #endregion

        #region Unsorted Formats

        [HumanReadable(Available = false, LongName = "Cartridge", ShortName = "cart")]
        Cartridge,

        [HumanReadable(Available = false, LongName = "CED", ShortName = "ced")]
        CED,

        [HumanReadable(Available = false, LongName = "Compact Flash", ShortName = "cf")]
        CompactFlash,

        [HumanReadable(Available = false, LongName = "MMC", ShortName = "mmc")]
        MMC,

        [HumanReadable(Available = false, LongName = "SD Card", ShortName = "sd")]
        SDCard,

        [HumanReadable(Available = false, LongName = "Flash Drive", ShortName = "fkd")]
        FlashDrive,

        #endregion
    }
}
