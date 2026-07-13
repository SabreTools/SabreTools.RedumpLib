using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.Data
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
        [HumanReadable(LongName = "Cuesheet", ShortName = "cue")]
        Cuesheet = 1,

        [HumanReadable(LongName = "Edit", ShortName = "edit")]
        Edit = 2,

        // Placeholder for the linked queue history page, not an actual subpath
        [HumanReadable(LongName = "History", ShortName = "history")]
        History = 3,

        [HumanReadable(LongName = "SBI", ShortName = "sbi")]
        SBI = 4,
    }

    /// <summary>
    /// Dump status
    /// </summary>
    public enum DumpStatus
    {
        [HumanReadable(LongName = "Unknown", ShortName = "grey")]
        UnknownGrey = 1,

        [HumanReadable(LongName = "Disabled", ShortName = "red")]
        DisabledRed = 2,

        [HumanReadable(LongName = "Questionable", ShortName = "yellow")]
        QuestionableYellow = 3,

        [HumanReadable(LongName = "Unverified", ShortName = "blue")]
        UnverifiedBlue = 4,

        [HumanReadable(LongName = "Verified", ShortName = "green")]
        VerifiedGreen = 5,
    }

    /// <summary>
    /// List of all site-supported media types
    /// </summary>
    public enum MediaType
    {
        NONE = 0,

        [HumanReadable(LongName = "BD-25", ShortName = "bd25")]
        BD25 = 1,

        /// <remarks>Not official</remarks>
        [HumanReadable(LongName = "BD-33", ShortName = "bd33")]
        BD33 = 2,

        [HumanReadable(LongName = "BD-50", ShortName = "bd50")]
        BD50 = 3,

        [HumanReadable(LongName = "BD-66", ShortName = "bd66")]
        BD66 = 4,

        [HumanReadable(LongName = "BD-100", ShortName = "bd100")]
        BD100 = 5,

        /// <remarks>Not official</remarks>
        [HumanReadable(LongName = "BD-128", ShortName = "bd128")]
        BD128 = 6,

        [HumanReadable(LongName = "CD", ShortName = "cd")]
        CD = 7,

        [HumanReadable(LongName = "DVD-5", ShortName = "dvd5")]
        DVD5 = 8,

        [HumanReadable(LongName = "DVD-9", ShortName = "dvd9")]
        DVD9 = 9,

        [HumanReadable(LongName = "GD-ROM", ShortName = "gdrom")]
        GDROM = 10,

        [HumanReadable(LongName = "HD-DVD (SL)", ShortName = "hdvd15")]
        HDDVDSL = 11,

        [HumanReadable(LongName = "HD-DVD (DL)", ShortName = "hdvd30")]
        HDDVDDL = 12,

        /// <remarks>Not official</remarks>
        [HumanReadable(LongName = "MIL-CD", ShortName = "milcd")]
        MILCD = 13,

        [HumanReadable(LongName = "Nintendo GameCube Game Disc", ShortName = "dvd5gc")]
        NintendoGameCubeGameDisc = 14,

        [HumanReadable(LongName = "Nintendo Wii Optical Disc (SL)", ShortName = "dvd5wii")]
        NintendoWiiOpticalDiscSL = 15,

        [HumanReadable(LongName = "Nintendo Wii Optical Disc (DL)", ShortName = "dvd9wii")]
        NintendoWiiOpticalDiscDL = 16,

        [HumanReadable(LongName = "Nintendo Wii U Optical Disc (SL)", ShortName = "bd25wiiu")]
        NintendoWiiUOpticalDiscSL = 17,

        [HumanReadable(LongName = "UMD (SL)", ShortName = "umd1")]
        UMDSL = 18,

        [HumanReadable(LongName = "UMD (DL)", ShortName = "umd2")]
        UMDDL = 19,
    }

    /// <summary>
    /// All possible packs
    /// </summary>
    public enum PackType
    {
        [HumanReadable(LongName = "CUES", ShortName = "cues")]
        Cuesheets = 1,

        [HumanReadable(LongName = "DAT", ShortName = "datfile")]
        Datfile = 2,

        [HumanReadable(LongName = "SBI", ShortName = "sbi")]
        Sbis = 3,
    }

    /// <summary>
    /// All possible media types not bound to specific site limitations
    /// </summary>
    public enum PhysicalMediaType
    {
        [HumanReadable(Available = false, LongName = "Unknown", ShortName = "unknown")]
        NONE = 0,

        #region Punched Media

        [HumanReadable(Available = false, LongName = "Aperture card", ShortName = "aperture")]
        ApertureCard = 1,

        [HumanReadable(Available = false, LongName = "Jacquard Loom card", ShortName = "jacquard loom card")]
        JacquardLoomCard = 2,

        [HumanReadable(Available = false, LongName = "Magnetic stripe card", ShortName = "magnetic stripe")]
        MagneticStripeCard = 3,

        [HumanReadable(Available = false, LongName = "Optical phonecard", ShortName = "optical phonecard")]
        OpticalPhonecard = 4,

        [HumanReadable(Available = false, LongName = "Punched card", ShortName = "punchcard")]
        PunchedCard = 5,

        [HumanReadable(Available = false, LongName = "Punched tape", ShortName = "punchtape")]
        PunchedTape = 6,

        #endregion

        #region Tape

        [HumanReadable(Available = false, LongName = "Cassette Tape", ShortName = "cassette")]
        Cassette = 7,

        [HumanReadable(Available = false, LongName = "Data Tape Cartridge", ShortName = "data cartridge")]
        DataCartridge = 8,

        [HumanReadable(Available = false, LongName = "Open Reel Tape", ShortName = "open reel")]
        OpenReel = 9,

        #endregion

        #region Disc / Disc

        [HumanReadable(LongName = "BD-ROM", ShortName = "bdrom")]
        BluRay = 10,

        [HumanReadable(LongName = "CD-ROM", ShortName = "cdrom")]
        CDROM = 11,

        [HumanReadable(LongName = "DVD-ROM", ShortName = "dvd")]
        DVD = 12,

        [HumanReadable(LongName = "Floppy Disk", ShortName = "fd")]
        FloppyDisk = 13,

        [HumanReadable(Available = false, LongName = "Floptical", ShortName = "floptical")]
        Floptical = 14,

        [HumanReadable(LongName = "GD-ROM", ShortName = "gdrom")]
        GDROM = 15,

        [HumanReadable(LongName = "HD-DVD-ROM", ShortName = "hddvd")]
        HDDVD = 16,

        [HumanReadable(LongName = "Hard Disk", ShortName = "hdd")]
        HardDisk = 17,

        [HumanReadable(Available = false, LongName = "Iomega Bernoulli Disk", ShortName = "bernoulli")]
        IomegaBernoulliDisk = 18,

        [HumanReadable(Available = false, LongName = "Iomega Jaz", ShortName = "jaz")]
        IomegaJaz = 19,

        [HumanReadable(Available = false, LongName = "Iomega Zip", ShortName = "zip")]
        IomegaZip = 20,

        [HumanReadable(LongName = "LD-ROM / LV-ROM", ShortName = "ldrom")]
        LaserDisc = 21, // LD-ROM and LV-ROM variants

        [HumanReadable(Available = false, LongName = "64DD Disk", ShortName = "64dd")]
        Nintendo64DD = 22,

        [HumanReadable(Available = false, LongName = "Famicom Disk System Disk", ShortName = "fds")]
        NintendoFamicomDiskSystem = 23,

        [HumanReadable(LongName = "GameCube Game Disc", ShortName = "gc")]
        NintendoGameCubeGameDisc = 24,

        [HumanReadable(LongName = "Wii Optical Disc", ShortName = "wii")]
        NintendoWiiOpticalDisc = 25,

        [HumanReadable(LongName = "Wii U Optical Disc", ShortName = "wiiu")]
        NintendoWiiUOpticalDisc = 26,

        [HumanReadable(LongName = "UMD", ShortName = "umd")]
        UMD = 27,

        #endregion

        #region Unsorted Formats

        [HumanReadable(Available = false, LongName = "Cartridge", ShortName = "cart")]
        Cartridge = 28,

        [HumanReadable(Available = false, LongName = "CED", ShortName = "ced")]
        CED = 29,

        [HumanReadable(Available = false, LongName = "Compact Flash", ShortName = "cf")]
        CompactFlash = 30,

        [HumanReadable(Available = false, LongName = "MMC", ShortName = "mmc")]
        MMC = 31,

        [HumanReadable(Available = false, LongName = "SD Card", ShortName = "sd")]
        SDCard = 32,

        [HumanReadable(Available = false, LongName = "Flash Drive", ShortName = "fkd")]
        FlashDrive = 33,

        #endregion
    }

    /// <summary>
    /// List of all recognized sort parameters
    /// </summary>
    public enum SortCategory
    {
        [HumanReadable(LongName = "Title", ShortName = "title")]
        Title = 0,

        /// <remarks>Not exposed in the redump.info UI</remarks>
        [HumanReadable(LongName = "Added", ShortName = "added")]
        Added = 1,

        [HumanReadable(LongName = "Region", ShortName = "region")]
        Region = 2,

        [HumanReadable(LongName = "System", ShortName = "system")]
        System = 3,

        [HumanReadable(LongName = "Version", ShortName = "version")]
        Version = 4,

        [HumanReadable(LongName = "Edition", ShortName = "edition")]
        Edition = 5,

        [HumanReadable(LongName = "Language", ShortName = "language")]
        Language = 6,

        [HumanReadable(LongName = "Serial", ShortName = "serial")]
        Serial = 7,

        [HumanReadable(LongName = "Status", ShortName = "status")]
        Status = 8,

        /// <remarks>Not exposed in the redump.info UI</remarks>
        [HumanReadable(LongName = "Modified", ShortName = "modified")]
        Modified = 9,
    }

    /// <summary>
    /// List of all recognized sort directions
    /// </summary>
    public enum SortDirection
    {
        [HumanReadable(LongName = "Ascending", ShortName = "asc")]
        Ascending = 0,

        [HumanReadable(LongName = "Descending", ShortName = "desc")]
        Descending = 1,
    }

    /// <summary>
    /// List of all recognized submission display kinds
    /// </summary>
    public enum SubmissionType
    {
        [HumanReadable(LongName = "Edit", ShortName = "Edit")]
        Edit = 0,

        [HumanReadable(LongName = "New Disc", ShortName = "New+Disc")]
        NewDisc = 1,

        [HumanReadable(LongName = "Verification", ShortName = "Verification")]
        Verification = 2,
    }

    /// <summary>
    /// List of system categories
    /// </summary>
    public enum SystemCategory
    {
        NONE = 0,

        [HumanReadable(LongName = "Disc-Based Consoles")]
        DiscBasedConsole = 1,

        [HumanReadable(LongName = "Other Consoles")]
        OtherConsole = 2,

        [HumanReadable(LongName = "Computers")]
        Computer = 3,

        [HumanReadable(LongName = "Arcade")]
        Arcade = 4,

        [HumanReadable(LongName = "Other")]
        Other = 5,
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
