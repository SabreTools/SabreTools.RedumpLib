using SabreTools.RedumpLib.Attributes;

// TODO: For any enums where order could change, turn into a class with helper methods like Hashing
// TODO: For any enums where order cannot change, ensure they have numeric values attached
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
        Cuesheet,

        [HumanReadable(LongName = "Edit", ShortName = "edit")]
        Edit,

        // Placeholder for the linked queue history page, not an actual subpath
        [HumanReadable(LongName = "History", ShortName = "history")]
        History,

        [HumanReadable(LongName = "SBI", ShortName = "sbi")]
        SBI,
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
        BD25,

        /// <remarks>Not official</remarks>
        [HumanReadable(LongName = "BD-33", ShortName = "bd33")]
        BD33,

        [HumanReadable(LongName = "BD-50", ShortName = "bd50")]
        BD50,

        [HumanReadable(LongName = "BD-66", ShortName = "bd66")]
        BD66,

        [HumanReadable(LongName = "BD-100", ShortName = "bd100")]
        BD100,

        /// <remarks>Not official</remarks>
        [HumanReadable(LongName = "BD-128", ShortName = "bd128")]
        BD128,

        [HumanReadable(LongName = "CD", ShortName = "cd")]
        CD,

        [HumanReadable(LongName = "DVD-5", ShortName = "dvd5")]
        DVD5,

        [HumanReadable(LongName = "DVD-9", ShortName = "dvd9")]
        DVD9,

        [HumanReadable(LongName = "GD-ROM", ShortName = "gdrom")]
        GDROM,

        [HumanReadable(LongName = "HD-DVD (SL)", ShortName = "hdvd15")]
        HDDVDSL,

        [HumanReadable(LongName = "HD-DVD (DL)", ShortName = "hdvd30")]
        HDDVDDL,

        /// <remarks>Not official</remarks>
        [HumanReadable(LongName = "MIL-CD", ShortName = "milcd")]
        MILCD,

        [HumanReadable(LongName = "Nintendo GameCube Game Disc", ShortName = "dvd5gc")]
        NintendoGameCubeGameDisc,

        [HumanReadable(LongName = "Nintendo Wii Optical Disc (SL)", ShortName = "dvd5wii")]
        NintendoWiiOpticalDiscSL,

        [HumanReadable(LongName = "Nintendo Wii Optical Disc (DL)", ShortName = "dvd9wii")]
        NintendoWiiOpticalDiscDL,

        [HumanReadable(LongName = "Nintendo Wii U Optical Disc (SL)", ShortName = "bd25wiiu")]
        NintendoWiiUOpticalDiscSL,

        [HumanReadable(LongName = "UMD (SL)", ShortName = "umd1")]
        UMDSL,

        [HumanReadable(LongName = "UMD (DL)", ShortName = "umd2")]
        UMDDL,
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

        [HumanReadable(LongName = "SBI", ShortName = "sbi")]
        Sbis,
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

    /// <summary>
    /// List of all known regions
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
    /// </remarks>
    public enum Region
    {
        #region Aggregate Regions

        [HumanReadable(LongName = "Asia", ShortName = "xa")]
        Asia,

        [HumanReadable(LongName = "Europe", ShortName = "eu")]
        Europe,

        [HumanReadable(LongName = "Export", ShortName = "xp")]
        Export,

        [HumanReadable(LongName = "Latin America", ShortName = "xl")]
        LatinAmerica,

        [HumanReadable(LongName = "Scandinavia", ShortName = "xs")]
        Scandinavia,

        [HumanReadable(LongName = "World", ShortName = "un")]
        World,

        #endregion

        #region A

        [HumanReadable(LongName = "Afghanistan", ShortName = "af")]
        Afghanistan,

        [HumanReadable(LongName = "Åland Islands", ShortName = "ax")]
        AlandIslands,

        [HumanReadable(LongName = "Albania", ShortName = "al")]
        Albania,

        [HumanReadable(LongName = "Algeria", ShortName = "dz")]
        Algeria,

        [HumanReadable(LongName = "American Samoa", ShortName = "as")]
        AmericanSamoa,

        [HumanReadable(LongName = "Andorra", ShortName = "ad")]
        Andorra,

        [HumanReadable(LongName = "Angola", ShortName = "ao")]
        Angola,

        [HumanReadable(LongName = "Anguilla", ShortName = "ai")]
        Anguilla,

        [HumanReadable(LongName = "Antarctica", ShortName = "aq")]
        Antarctica,

        [HumanReadable(LongName = "Antigua and Barbuda", ShortName = "ag")]
        AntiguaAndBarbuda,

        [HumanReadable(LongName = "Argentina", ShortName = "ar")]
        Argentina,

        [HumanReadable(LongName = "Armenia", ShortName = "am")]
        Armenia,

        [HumanReadable(LongName = "Aruba", ShortName = "aw")]
        Aruba,

        [HumanReadable(LongName = "Ascension Island", ShortName = "ac")]
        AscensionIsland,

        [HumanReadable(LongName = "Australia", ShortName = "au")]
        Australia,

        [HumanReadable(LongName = "Austria", ShortName = "at")]
        Austria,

        [HumanReadable(LongName = "Azerbaijan", ShortName = "az")]
        Azerbaijan,

        #endregion

        #region B

        [HumanReadable(LongName = "Bahamas", ShortName = "bs")]
        Bahamas,

        [HumanReadable(LongName = "Bahrain", ShortName = "bh")]
        Bahrain,

        [HumanReadable(LongName = "Bangladesh", ShortName = "bd")]
        Bangladesh,

        [HumanReadable(LongName = "Barbados", ShortName = "bb")]
        Barbados,

        [HumanReadable(LongName = "Belarus", ShortName = "by")]
        Belarus,

        [HumanReadable(LongName = "Belgium", ShortName = "be")]
        Belgium,

        [HumanReadable(LongName = "Belize", ShortName = "bz")]
        Belize,

        [HumanReadable(LongName = "Benin", ShortName = "bj")]
        Benin,

        [HumanReadable(LongName = "Bermuda", ShortName = "bm")]
        Bermuda,

        [HumanReadable(LongName = "Bhutan", ShortName = "bt")]
        Bhutan,

        [HumanReadable(LongName = "Bolivia", ShortName = "bo")]
        Bolivia,

        [HumanReadable(LongName = "Bonaire, Sint Eustatius and Saba", ShortName = "bq")]
        Bonaire,

        [HumanReadable(LongName = "Bosnia and Herzegovina", ShortName = "ba")]
        BosniaAndHerzegovina,

        [HumanReadable(LongName = "Botswana", ShortName = "bw")]
        Botswana,

        [HumanReadable(LongName = "Bouvet Island", ShortName = "bv")]
        BouvetIsland,

        [HumanReadable(LongName = "Brazil", ShortName = "br")]
        Brazil,

        [HumanReadable(LongName = "British Indian Ocean Territory", ShortName = "io")]
        BritishIndianOceanTerritory,

        [HumanReadable(LongName = "Brunei Darussalam", ShortName = "bn")]
        BruneiDarussalam,

        [HumanReadable(LongName = "Bulgaria", ShortName = "bg")]
        Bulgaria,

        [HumanReadable(LongName = "Burkina Faso", ShortName = "bf")]
        BurkinaFaso,

        [HumanReadable(LongName = "Burundi", ShortName = "bi")]
        Burundi,

        #endregion

        #region C

        [HumanReadable(LongName = "Cabo Verde", ShortName = "cv")]
        CaboVerde,

        [HumanReadable(LongName = "Cambodia", ShortName = "kh")]
        Cambodia,

        [HumanReadable(LongName = "Cameroon", ShortName = "cm")]
        Cameroon,

        [HumanReadable(LongName = "Canada", ShortName = "ca")]
        Canada,

        [HumanReadable(LongName = "Canary Islands", ShortName = "ic")]
        CanaryIslands,

        [HumanReadable(LongName = "Cayman Islands", ShortName = "ky")]
        CaymanIslands,

        [HumanReadable(LongName = "Central African Republic", ShortName = "cf")]
        CentralAfricanRepublic,

        [HumanReadable(LongName = "Ceuta, Melilla", ShortName = "ea")]
        CeutaMelilla,

        [HumanReadable(LongName = "Chad", ShortName = "td")]
        Chad,

        [HumanReadable(LongName = "Chile", ShortName = "cl")]
        Chile,

        [HumanReadable(LongName = "China", ShortName = "cn")]
        China,

        [HumanReadable(LongName = "Christmas Island", ShortName = "cx")]
        ChristmasIsland,

        [HumanReadable(LongName = "Clipperton Island", ShortName = "cp")]
        ClippertonIsland,

        [HumanReadable(LongName = "Cocos (Keeling) Islands", ShortName = "cc")]
        CocosIslands,

        [HumanReadable(LongName = "Colombia", ShortName = "co")]
        Colombia,

        [HumanReadable(LongName = "Comoros", ShortName = "km")]
        Comoros,

        [HumanReadable(LongName = "Congo", ShortName = "cg")]
        Congo,

        [HumanReadable(LongName = "Cook Islands", ShortName = "ck")]
        CookIslands,

        [HumanReadable(LongName = "Costa Rica", ShortName = "cr")]
        CostaRica,

        [HumanReadable(LongName = "Côte d'Ivoire", ShortName = "ci")]
        CoteDIvoire,

        [HumanReadable(LongName = "Croatia", ShortName = "hr")]
        Croatia,

        [HumanReadable(LongName = "Cuba", ShortName = "cu")]
        Cuba,

        [HumanReadable(LongName = "Curaçao", ShortName = "cw")]
        Curacao,

        [HumanReadable(LongName = "Cyprus", ShortName = "cy")]
        Cyprus,

        [HumanReadable(LongName = "Czechia", ShortName = "cz")]
        Czechia,

        [HumanReadable(LongName = "Czechoslovakia", ShortName = "cs")]
        Czechoslovakia,

        #endregion

        #region D

        // Zaire was "Zr"
        [HumanReadable(LongName = "Democratic Republic of the Congo (Zaire)", ShortName = "cd")]
        DemocraticRepublicOfTheCongo,

        [HumanReadable(LongName = "Denmark", ShortName = "dk")]
        Denmark,

        [HumanReadable(LongName = "Diego Garcia", ShortName = "dg")]
        DiegoGarcia,

        [HumanReadable(LongName = "Djibouti", ShortName = "dj")]
        Djibouti,

        [HumanReadable(LongName = "Dominica", ShortName = "dm")]
        Dominica,

        [HumanReadable(LongName = "Dominican Republic", ShortName = "do")]
        DominicanRepublic,

        #endregion

        #region E

        [HumanReadable(LongName = "Ecuador", ShortName = "ec")]
        Ecuador,

        [HumanReadable(LongName = "Egypt", ShortName = "eg")]
        Egypt,

        [HumanReadable(LongName = "El Salvador", ShortName = "sv")]
        ElSalvador,

        [HumanReadable(LongName = "Equatorial Guinea", ShortName = "gq")]
        EquatorialGuinea,

        [HumanReadable(LongName = "Eritrea", ShortName = "er")]
        Eritrea,

        [HumanReadable(LongName = "Estonia", ShortName = "ee")]
        Estonia,

        [HumanReadable(LongName = "Eswatini", ShortName = "sz")]
        Eswatini,

        [HumanReadable(LongName = "Ethiopia", ShortName = "et")]
        Ethiopia,

        // Commented out to avoid confusion
        //[HumanReadable(LongName = "European Union", ShortName="eu")]
        //EuropeanUnion,

        // Commented out to avoid confusion
        //[HumanReadable(LongName = "Eurozone", ShortName="ez")]
        //Eurozone,

        #endregion

        #region F

        [HumanReadable(LongName = "Falkland Islands (Malvinas)", ShortName = "fk")]
        FalklandIslands,

        [HumanReadable(LongName = "Faroe Islands", ShortName = "fo")]
        FaroeIslands,

        [HumanReadable(LongName = "Federated States of Micronesia", ShortName = "fm")]
        FederatedStatesOfMicronesia,

        [HumanReadable(LongName = "Fiji", ShortName = "fj")]
        Fiji,

        // Formerly "Sf"
        [HumanReadable(LongName = "Finland", ShortName = "fi")]
        Finland,

        [HumanReadable(LongName = "France", ShortName = "fr")]
        France,

        // Commented out to avoid confusion
        //[HumanReadable(LongName = "France, Metropolitan", ShortName="fx")]
        //FranceMetropolitan,

        [HumanReadable(LongName = "French Guiana", ShortName = "gf")]
        FrenchGuiana,

        [HumanReadable(LongName = "French Polynesia", ShortName = "pf")]
        FrenchPolynesia,

        [HumanReadable(LongName = "French Southern Territories", ShortName = "tf")]
        FrenchSouthernTerritories,

        #endregion

        #region G

        [HumanReadable(LongName = "Gabon", ShortName = "ga")]
        Gabon,

        [HumanReadable(LongName = "Gambia", ShortName = "gm")]
        Gambia,

        [HumanReadable(LongName = "Georgia", ShortName = "ge")]
        Georgia,

        [HumanReadable(LongName = "Germany", ShortName = "de")]
        Germany,

        [HumanReadable(LongName = "Ghana", ShortName = "gh")]
        Ghana,

        [HumanReadable(LongName = "Gibraltar", ShortName = "gi")]
        Gibraltar,

        [HumanReadable(LongName = "Greece", ShortName = "gr")]
        Greece,

        [HumanReadable(LongName = "Greenland", ShortName = "gl")]
        Greenland,

        [HumanReadable(LongName = "Grenada", ShortName = "gd")]
        Grenada,

        [HumanReadable(LongName = "Guadeloupe", ShortName = "gp")]
        Guadeloupe,

        [HumanReadable(LongName = "Guam", ShortName = "gu")]
        Guam,

        [HumanReadable(LongName = "Guatemala", ShortName = "gt")]
        Guatemala,

        [HumanReadable(LongName = "Guernsey", ShortName = "gg")]
        Guernsey,

        [HumanReadable(LongName = "Guinea", ShortName = "gn")]
        Guinea,

        [HumanReadable(LongName = "Guinea-Bissau", ShortName = "gw")]
        GuineaBissau,

        [HumanReadable(LongName = "Guyana", ShortName = "gy")]
        Guyana,

        #endregion

        #region H

        [HumanReadable(LongName = "Haiti", ShortName = "ht")]
        Haiti,

        [HumanReadable(LongName = "Heard Island and McDonald Islands", ShortName = "hm")]
        HeardIslandAndMcDonaldIslands,

        [HumanReadable(LongName = "Holy See (Vatican City)", ShortName = "va")]
        HolySee,

        [HumanReadable(LongName = "Honduras", ShortName = "hn")]
        Honduras,

        [HumanReadable(LongName = "Hong Kong", ShortName = "hk")]
        HongKong,

        [HumanReadable(LongName = "Hungary", ShortName = "hu")]
        Hungary,

        #endregion

        #region I

        [HumanReadable(LongName = "Iceland", ShortName = "is")]
        Iceland,

        [HumanReadable(LongName = "India", ShortName = "in")]
        India,

        [HumanReadable(LongName = "Indonesia", ShortName = "id")]
        Indonesia,

        [HumanReadable(LongName = "Iran", ShortName = "ir")]
        Iran,

        [HumanReadable(LongName = "Iraq", ShortName = "iq")]
        Iraq,

        [HumanReadable(LongName = "Ireland", ShortName = "ie")]
        Ireland,

        [HumanReadable(LongName = "Island of Sark", ShortName = "cq")]
        IslandOfSark,

        [HumanReadable(LongName = "Isle of Man", ShortName = "im")]
        IsleOfMan,

        [HumanReadable(LongName = "Israel", ShortName = "il")]
        Israel,

        [HumanReadable(LongName = "Italy", ShortName = "it")]
        Italy,

        #endregion

        #region J

        [HumanReadable(LongName = "Jamaica", ShortName = "jm")]
        Jamaica,

        [HumanReadable(LongName = "Japan", ShortName = "jp")]
        Japan,

        [HumanReadable(LongName = "Jersey", ShortName = "je")]
        Jersey,

        [HumanReadable(LongName = "Jordan", ShortName = "jo")]
        Jordan,

        #endregion

        #region K

        [HumanReadable(LongName = "Kazakhstan", ShortName = "kz")]
        Kazakhstan,

        [HumanReadable(LongName = "Kenya", ShortName = "ke")]
        Kenya,

        [HumanReadable(LongName = "Kiribati", ShortName = "ki")]
        Kiribati,

        [HumanReadable(LongName = "Korea (Democratic People's Republic of Korea)", ShortName = "kp")]
        NorthKorea,

        [HumanReadable(LongName = "Korea (Republic of Korea)", ShortName = "kr")]
        SouthKorea,

        [HumanReadable(LongName = "Kuwait", ShortName = "kw")]
        Kuwait,

        [HumanReadable(LongName = "Kyrgyzstan", ShortName = "kg")]
        Kyrgyzstan,

        #endregion

        #region L

        [HumanReadable(LongName = "(Laos) Lao People's Democratic Republic", ShortName = "la")]
        Laos,

        [HumanReadable(LongName = "Latvia", ShortName = "lv")]
        Latvia,

        [HumanReadable(LongName = "Lebanon", ShortName = "lb")]
        Lebanon,

        [HumanReadable(LongName = "Lesotho", ShortName = "ls")]
        Lesotho,

        [HumanReadable(LongName = "Liberia", ShortName = "lr")]
        Liberia,

        [HumanReadable(LongName = "Libya", ShortName = "ly")]
        Libya,

        [HumanReadable(LongName = "Liechtenstein", ShortName = "li")]
        Liechtenstein,

        [HumanReadable(LongName = "Lithuania", ShortName = "lt")]
        Lithuania,

        [HumanReadable(LongName = "Luxembourg", ShortName = "lu")]
        Luxembourg,

        #endregion

        #region M

        [HumanReadable(LongName = "Macao", ShortName = "mo")]
        Macao,

        [HumanReadable(LongName = "Madagascar", ShortName = "mg")]
        Madagascar,

        [HumanReadable(LongName = "Malawi", ShortName = "mw")]
        Malawi,

        [HumanReadable(LongName = "Malaysia", ShortName = "my")]
        Malaysia,

        [HumanReadable(LongName = "Maldives", ShortName = "mv")]
        Maldives,

        [HumanReadable(LongName = "Mali", ShortName = "ml")]
        Mali,

        [HumanReadable(LongName = "Malta", ShortName = "mt")]
        Malta,

        [HumanReadable(LongName = "Marshall Islands", ShortName = "mh")]
        MarshallIslands,

        [HumanReadable(LongName = "Martinique", ShortName = "mq")]
        Martinique,

        [HumanReadable(LongName = "Mauritania", ShortName = "mr")]
        Mauritania,

        [HumanReadable(LongName = "Mauritius", ShortName = "mu")]
        Mauritius,

        [HumanReadable(LongName = "Mayotte", ShortName = "yt")]
        Mayotte,

        [HumanReadable(LongName = "Mexico", ShortName = "mx")]
        Mexico,

        [HumanReadable(LongName = "Monaco", ShortName = "mc")]
        Monaco,

        [HumanReadable(LongName = "Mongolia", ShortName = "mn")]
        Mongolia,

        [HumanReadable(LongName = "Montenegro", ShortName = "me")]
        Montenegro,

        [HumanReadable(LongName = "Montserrat", ShortName = "ms")]
        Montserrat,

        [HumanReadable(LongName = "Morocco", ShortName = "ma")]
        Morocco,

        [HumanReadable(LongName = "Mozambique", ShortName = "mz")]
        Mozambique,

        // Burma was "Bu"
        [HumanReadable(LongName = "Myanmar (Burma)", ShortName = "mm")]
        Myanmar,

        #endregion

        #region N

        [HumanReadable(LongName = "Namibia", ShortName = "na")]
        Namibia,

        [HumanReadable(LongName = "Nauru", ShortName = "nr")]
        Nauru,

        [HumanReadable(LongName = "Nepal", ShortName = "np")]
        Nepal,

        [HumanReadable(LongName = "Netherlands", ShortName = "nl")]
        Netherlands,

        [HumanReadable(LongName = "Netherlands Antilles", ShortName = "an")]
        NetherlandsAntilles,

        // Commented out to avoid confusion
        //[HumanReadable(LongName = "Neutral Zone", ShortName="nt")]
        //NeutralZone,

        [HumanReadable(LongName = "New Caledonia", ShortName = "nc")]
        NewCaledonia,

        [HumanReadable(LongName = "New Zealand", ShortName = "nz")]
        NewZealand,

        [HumanReadable(LongName = "Nicaragua", ShortName = "ni")]
        Nicaragua,

        [HumanReadable(LongName = "Niger", ShortName = "ne")]
        Niger,

        [HumanReadable(LongName = "Nigeria", ShortName = "ng")]
        Nigeria,

        [HumanReadable(LongName = "Niue", ShortName = "nu")]
        Niue,

        [HumanReadable(LongName = "Norfolk Island", ShortName = "nf")]
        NorfolkIsland,

        [HumanReadable(LongName = "North Macedonia", ShortName = "mk")]
        NorthMacedonia,

        [HumanReadable(LongName = "Northern Mariana Islands", ShortName = "mp")]
        NorthernMarianaIslands,

        [HumanReadable(LongName = "Norway", ShortName = "no")]
        Norway,

        #endregion

        #region O

        [HumanReadable(LongName = "Oman", ShortName = "om")]
        Oman,

        #endregion

        #region P

        [HumanReadable(LongName = "Pakistan", ShortName = "pk")]
        Pakistan,

        [HumanReadable(LongName = "Palau", ShortName = "pw")]
        Palau,

        [HumanReadable(LongName = "Panama", ShortName = "pa")]
        Panama,

        [HumanReadable(LongName = "Papua New Guinea", ShortName = "pg")]
        PapuaNewGuinea,

        [HumanReadable(LongName = "Paraguay", ShortName = "py")]
        Paraguay,

        [HumanReadable(LongName = "Peru", ShortName = "pe")]
        Peru,

        [HumanReadable(LongName = "Philippines", ShortName = "ph")]
        Philippines,

        [HumanReadable(LongName = "Pitcairn", ShortName = "pn")]
        Pitcairn,

        [HumanReadable(LongName = "Poland", ShortName = "pl")]
        Poland,

        [HumanReadable(LongName = "Portugal", ShortName = "pt")]
        Portugal,

        [HumanReadable(LongName = "Puerto Rico", ShortName = "pr")]
        PuertoRico,

        #endregion

        #region Q

        [HumanReadable(LongName = "Qatar", ShortName = "qa")]
        Qatar,

        #endregion

        #region R

        [HumanReadable(LongName = "Republic of Moldova", ShortName = "md")]
        RepublicOfMoldova,

        [HumanReadable(LongName = "Réunion", ShortName = "re")]
        Reunion,

        [HumanReadable(LongName = "Romania", ShortName = "ro")]
        Romania,

        [HumanReadable(LongName = "Russian Federation", ShortName = "ru")]
        RussianFederation,

        [HumanReadable(LongName = "Rwanda", ShortName = "rw")]
        Rwanda,

        #endregion

        #region S

        [HumanReadable(LongName = "Saint Barthélemy", ShortName = "bl")]
        SaintBarthelemy,

        [HumanReadable(LongName = "Saint Helena, Ascension and Tristan da Cunha", ShortName = "sh")]
        SaintHelena,

        [HumanReadable(LongName = "Saint Kitts and Nevis", ShortName = "kn")]
        SaintKittsAndNevis,

        [HumanReadable(LongName = "Saint Lucia", ShortName = "lc")]
        SaintLucia,

        [HumanReadable(LongName = "Saint Martin", ShortName = "mf")]
        SaintMartin,

        [HumanReadable(LongName = "Saint Pierre and Miquelon", ShortName = "pm")]
        SaintPierreAndMiquelon,

        [HumanReadable(LongName = "Saint Vincent and the Grenadines", ShortName = "vc")]
        SaintVincentAndTheGrenadines,

        [HumanReadable(LongName = "Samoa", ShortName = "ws")]
        Samoa,

        [HumanReadable(LongName = "San Marino", ShortName = "sm")]
        SanMarino,

        [HumanReadable(LongName = "Sao Tome and Principe", ShortName = "st")]
        SaoTomeAndPrincipe,

        [HumanReadable(LongName = "Saudi Arabia", ShortName = "sa")]
        SaudiArabia,

        [HumanReadable(LongName = "Senegal", ShortName = "sn")]
        Senegal,

        [HumanReadable(LongName = "Serbia", ShortName = "rs")]
        Serbia,

        [HumanReadable(LongName = "Seychelles", ShortName = "sc")]
        Seychelles,

        [HumanReadable(LongName = "Sierra Leone", ShortName = "sl")]
        SierraLeone,

        [HumanReadable(LongName = "Singapore", ShortName = "sg")]
        Singapore,

        [HumanReadable(LongName = "Sint Maarten", ShortName = "sx")]
        SintMaarten,

        [HumanReadable(LongName = "Slovakia", ShortName = "sk")]
        Slovakia,

        [HumanReadable(LongName = "Slovenia", ShortName = "si")]
        Slovenia,

        [HumanReadable(LongName = "Solomon Islands", ShortName = "sb")]
        SolomonIslands,

        [HumanReadable(LongName = "Somalia", ShortName = "so")]
        Somalia,

        [HumanReadable(LongName = "South Africa", ShortName = "za")]
        SouthAfrica,

        [HumanReadable(LongName = "South Georgia and the South Sandwich Islands", ShortName = "gs")]
        SouthGeorgia,

        [HumanReadable(LongName = "South Sudan", ShortName = "ss")]
        SouthSudan,

        [HumanReadable(LongName = "Spain", ShortName = "es")]
        Spain,

        [HumanReadable(LongName = "Sri Lanka", ShortName = "lk")]
        SriLanka,

        [HumanReadable(LongName = "State of Palestine", ShortName = "ps")]
        StateOfPalestine,

        [HumanReadable(LongName = "Sudan", ShortName = "sd")]
        Sudan,

        [HumanReadable(LongName = "Suriname", ShortName = "sr")]
        Suriname,

        [HumanReadable(LongName = "Svalbard and Jan Mayen", ShortName = "sj")]
        SvalbardAndJanMayen,

        [HumanReadable(LongName = "Sweden", ShortName = "se")]
        Sweden,

        [HumanReadable(LongName = "Switzerland", ShortName = "ch")]
        Switzerland,

        [HumanReadable(LongName = "Syrian Arab Republic", ShortName = "sy")]
        SyrianArabRepublic,

        #endregion

        #region T

        [HumanReadable(LongName = "Taiwan", ShortName = "tw")]
        Taiwan,

        [HumanReadable(LongName = "Tajikistan", ShortName = "tj")]
        Tajikistan,

        [HumanReadable(LongName = "Thailand", ShortName = "th")]
        Thailand,

        // East Timor was "Tp"
        [HumanReadable(LongName = "Timor-Leste (East Timor)", ShortName = "tl")]
        TimorLeste,

        [HumanReadable(LongName = "Togo", ShortName = "tg")]
        Togo,

        [HumanReadable(LongName = "Tokelau", ShortName = "tk")]
        Tokelau,

        [HumanReadable(LongName = "Tonga", ShortName = "to")]
        Tonga,

        [HumanReadable(LongName = "Trinidad and Tobago", ShortName = "tt")]
        TrinidadAndTobago,

        [HumanReadable(LongName = "Tristan da Cunha", ShortName = "ta")]
        TristanDaCunha,

        [HumanReadable(LongName = "Tunisia", ShortName = "tn")]
        Tunisia,

        [HumanReadable(LongName = "Turkey", ShortName = "tr")]
        Turkey,

        [HumanReadable(LongName = "Turkmenistan", ShortName = "tm")]
        Turkmenistan,

        [HumanReadable(LongName = "Turks and Caicos Islands", ShortName = "tc")]
        TurksAndCaicosIslands,

        [HumanReadable(LongName = "Tuvalu", ShortName = "tv")]
        Tuvalu,

        #endregion

        #region U

        [HumanReadable(LongName = "Uganda", ShortName = "ug")]
        Uganda,

        // Should be both "Gb" and "Uk"
        // United Kingdom of Great Britain and Northern Ireland
        [HumanReadable(LongName = "UK", ShortName = "gb")]
        UnitedKingdom,

        [HumanReadable(LongName = "Ukraine", ShortName = "ue")]
        Ukraine,

        [HumanReadable(LongName = "United Arab Emirates", ShortName = "ae")]
        UnitedArabEmirates,

        // Commented out to avoid confusion
        //[HumanReadable(LongName = "United Nations", ShortName="un")]
        //UnitedNations,

        [HumanReadable(LongName = "United Republic of Tanzania", ShortName = "tz")]
        UnitedRepublicOfTanzania,

        [HumanReadable(LongName = "United States Minor Outlying Islands", ShortName = "um")]
        UnitedStatesMinorOutlyingIslands,

        [HumanReadable(LongName = "Uruguay", ShortName = "uy")]
        Uruguay,

        // United States of America
        [HumanReadable(LongName = "USA", ShortName = "us")]
        UnitedStatesOfAmerica,

        [HumanReadable(LongName = "USSR", ShortName = "su")]
        USSR,

        [HumanReadable(LongName = "Uzbekistan", ShortName = "uz")]
        Uzbekistan,

        #endregion

        #region V

        [HumanReadable(LongName = "Vanuatu", ShortName = "vu")]
        Vanuatu,

        [HumanReadable(LongName = "Venezuela", ShortName = "ve")]
        Venezuela,

        [HumanReadable(LongName = "Viet Nam", ShortName = "vn")]
        VietNam,

        [HumanReadable(LongName = "Virgin Islands (British)", ShortName = "vg")]
        BritishVirginIslands,

        [HumanReadable(LongName = "Virgin Islands (US)", ShortName = "vi")]
        USVirginIslands,

        #endregion

        #region W

        [HumanReadable(LongName = "Wallis and Futuna", ShortName = "wf")]
        WallisAndFutuna,

        [HumanReadable(LongName = "Western Sahara", ShortName = "eh")]
        WesternSahara,

        #endregion

        #region Y

        [HumanReadable(LongName = "Yemen", ShortName = "ye")]
        Yemen,

        [HumanReadable(LongName = "Yugoslavia", ShortName = "yu")]
        Yugoslavia,

        #endregion

        #region Z

        [HumanReadable(LongName = "Zambia", ShortName = "zm")]
        Zambia,

        [HumanReadable(LongName = "Zimbabwe", ShortName = "zw")]
        Zimbabwe,

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
        [HumanReadable(ShortName = "<b>Dice Multimedia</b>:", LongName = "<b>Dice Multimedia</b>:")]
        DiceMultimedia,

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

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Focus Multimedia</b>:", LongName = "<b>Focus Multimedia</b>:")]
        FocusMultimedia,

        [HumanReadable(ShortName = "[T:FIID]", LongName = "<b>Fox Interactive ID</b>:")]
        FoxInteractiveID,

        [HumanReadable(ShortName = "[T:GF]", LongName = "<b>Game Footage</b>:")]
        GameFootage,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>Games</b>:", LongName = "<b>Games</b>:")]
        Games,

        [HumanReadable(ShortName = "[T:G]", LongName = "<b>Genre</b>:")]
        Genre,

        // This doesn't have a site tag yet
        [HumanReadable(ShortName = "<b>GSP Software</b>:", LongName = "<b>GSP Software</b>:")]
        GSPSoftware,

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

        // This doesn't have a site tag yet, promoted to field in redump.info
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
        [HumanReadable(LongName = "Title", ShortName = "title")]
        Title,

        /// <remarks>Not exposed in the redump.info UI</remarks>
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

        [HumanReadable(LongName = "Language", ShortName = "language")]
        Language,

        [HumanReadable(LongName = "Serial", ShortName = "serial")]
        Serial,

        [HumanReadable(LongName = "Status", ShortName = "status")]
        Status,

        /// <remarks>Not exposed in the redump.info UI</remarks>
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
    /// List of all recognized submission display kinds
    /// </summary>
    public enum SubmissionType
    {
        [HumanReadable(LongName = "Edit", ShortName = "Edit")]
        Edit,

        [HumanReadable(LongName = "New Disc", ShortName = "New+Disc")]
        NewDisc,

        [HumanReadable(LongName = "Verification", ShortName = "Verification")]
        Verification
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
