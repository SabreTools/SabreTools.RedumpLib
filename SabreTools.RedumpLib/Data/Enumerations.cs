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
    /// List of all disc langauges
    /// </summary>
    /// <remarks>https://www.loc.gov/standards/iso639-2/php/code_list.php</remarks>
    public enum Language
    {
        #region A

        [LanguageCode(LongName = "Abkhazian", TwoLetterCode = "ab", ThreeLetterCode = "abk")]
        Abkhazian,

        [LanguageCode(LongName = "Achinese", ThreeLetterCode = "ace")]
        Achinese,

        [LanguageCode(LongName = "Acoli", ThreeLetterCode = "ach")]
        Acoli,

        [LanguageCode(LongName = "Adangme", ThreeLetterCode = "ada")]
        Adangme,

        // Adyghe; Adygei
        [LanguageCode(LongName = "Adyghe", ThreeLetterCode = "ady")]
        Adyghe,

        [LanguageCode(LongName = "Afar", TwoLetterCode = "aa", ThreeLetterCode = "aar")]
        Afar,

        [LanguageCode(LongName = "Afrihili", ThreeLetterCode = "afh")]
        Afrihili,

        [LanguageCode(LongName = "Afrikaans", TwoLetterCode = "af", ThreeLetterCode = "afr")]
        Afrikaans,

        [LanguageCode(LongName = "Ainu", ThreeLetterCode = "ain")]
        Ainu,

        [LanguageCode(LongName = "Akan", TwoLetterCode = "ak", ThreeLetterCode = "aka")]
        Akan,

        [LanguageCode(LongName = "Akkadian", ThreeLetterCode = "akk")]
        Akkadian,

        [LanguageCode(LongName = "Albanian", TwoLetterCode = "sq", ThreeLetterCode = "alb", ThreeLetterCodeAlt = "sqi")]
        Albanian,

        [LanguageCode(LongName = "Aleut", ThreeLetterCode = "ale")]
        Aleut,

        [LanguageCode(LongName = "Amharic", TwoLetterCode = "am", ThreeLetterCode = "amh")]
        Amharic,

        [LanguageCode(LongName = "Angika", ThreeLetterCode = "anp")]
        Angika,

        [LanguageCode(LongName = "Arabic", TwoLetterCode = "ar", ThreeLetterCode = "ara")]
        Arabic,

        [LanguageCode(LongName = "Aragonese", TwoLetterCode = "an", ThreeLetterCode = "arg")]
        Aragonese,

        // Official Aramaic (700-300 BCE); Imperial Aramaic (700-300 BCE)
        [LanguageCode(LongName = "Aramaic", ThreeLetterCode = "arc")]
        Aramaic,

        [LanguageCode(LongName = "Armenian", TwoLetterCode = "hy", ThreeLetterCode = "arm", ThreeLetterCodeAlt = "hye")]
        Armenian,

        [LanguageCode(LongName = "Arapaho", ThreeLetterCode = "arp")]
        Arapaho,

        // Aromanian; Arumanian; Macedo-Romanian
        [LanguageCode(LongName = "Aromanian", ThreeLetterCode = "rup")]
        Aromanian,

        [LanguageCode(LongName = "Arawak", ThreeLetterCode = "arw")]
        Arawak,

        [LanguageCode(LongName = "Assamese", TwoLetterCode = "as", ThreeLetterCode = "asm")]
        Assamese,

        // Asturian; Bable; Leonese; Asturleonese
        [LanguageCode(LongName = "Asturian", ThreeLetterCode = "ast")]
        Asturian,

        [LanguageCode(LongName = "Athapascan", ThreeLetterCode = "den")]
        Athapascan,

        [LanguageCode(LongName = "Avaric", TwoLetterCode = "av", ThreeLetterCode = "ava")]
        Avaric,

        [LanguageCode(LongName = "Avestan", TwoLetterCode = "ae", ThreeLetterCode = "ave")]
        Avestan,

        [LanguageCode(LongName = "Awadhi", ThreeLetterCode = "awa")]
        Awadhi,

        [LanguageCode(LongName = "Aymara", TwoLetterCode = "ay", ThreeLetterCode = "aym")]
        Aymara,

        [LanguageCode(LongName = "Azerbaijani", TwoLetterCode = "az", ThreeLetterCode = "aze")]
        Azerbaijani,

        #endregion

        #region B

        [LanguageCode(LongName = "Balinese", ThreeLetterCode = "ban")]
        Balinese,

        [LanguageCode(LongName = "Baluchi", ThreeLetterCode = "bal")]
        Baluchi,

        [LanguageCode(LongName = "Bambara", TwoLetterCode = "bm", ThreeLetterCode = "bam")]
        Bambara,

        [LanguageCode(LongName = "Basa", ThreeLetterCode = "bas")]
        Basa,

        [LanguageCode(LongName = "Bashkir", TwoLetterCode = "ba", ThreeLetterCode = "bak")]
        Bashkir,

        [LanguageCode(LongName = "Basque", TwoLetterCode = "eu", ThreeLetterCode = "baq", ThreeLetterCodeAlt = "eus")]
        Basque,

        // Beja; Bedawiyet
        [LanguageCode(LongName = "Beja", ThreeLetterCode = "bej")]
        Beja,

        [LanguageCode(LongName = "Belarusian", TwoLetterCode = "be", ThreeLetterCode = "bel")]
        Belarusian,

        [LanguageCode(LongName = "Bemba", ThreeLetterCode = "bem")]
        Bemba,

        [LanguageCode(LongName = "Bengali", TwoLetterCode = "bn", ThreeLetterCode = "ben")]
        Bengali,

        [LanguageCode(LongName = "Bhojpuri", ThreeLetterCode = "bho")]
        Bhojpuri,

        [LanguageCode(LongName = "Bikol", ThreeLetterCode = "bik")]
        Bikol,

        [LanguageCode(LongName = "Bini; Edo", ThreeLetterCode = "bin")]
        Bini,

        [LanguageCode(LongName = "Bislama", TwoLetterCode = "bi", ThreeLetterCode = "bis")]
        Bislama,

        // Blin; Bilin
        [LanguageCode(LongName = "Blin", ThreeLetterCode = "byn")]
        Blin,

        // Blissymbols; Blissymbolics; Bliss
        [LanguageCode(LongName = "Blissymbols", ThreeLetterCode = "zbl")]
        Blissymbols,

        [LanguageCode(LongName = "Bosnian", TwoLetterCode = "bs", ThreeLetterCode = "bos")]
        Bosnian,

        [LanguageCode(LongName = "Braj", ThreeLetterCode = "bra")]
        Braj,

        [LanguageCode(LongName = "Breton", TwoLetterCode = "br", ThreeLetterCode = "bre")]
        Breton,

        [LanguageCode(LongName = "Buginese", ThreeLetterCode = "bug")]
        Buginese,

        [LanguageCode(LongName = "Bulgarian", TwoLetterCode = "bg", ThreeLetterCode = "bul")]
        Bulgarian,

        [LanguageCode(LongName = "Buriat", ThreeLetterCode = "bua")]
        Buriat,

        [LanguageCode(LongName = "Burmese", TwoLetterCode = "my", ThreeLetterCode = "bur", ThreeLetterCodeAlt = "mya")]
        Burmese,

        #endregion

        #region C

        [LanguageCode(LongName = "Caddo", ThreeLetterCode = "cad")]
        Caddo,

        // Catalan; Valencian
        [LanguageCode(LongName = "Catalan", TwoLetterCode = "ca", ThreeLetterCode = "cat")]
        Catalan,

        [LanguageCode(LongName = "Cebuano", ThreeLetterCode = "ceb")]
        Cebuano,

        [LanguageCode(LongName = "Central Khmer", TwoLetterCode = "km", ThreeLetterCode = "khm")]
        CentralKhmer,

        [LanguageCode(LongName = "Chagatai", ThreeLetterCode = "chg")]
        Chagatai,

        [LanguageCode(LongName = "Chamorro", TwoLetterCode = "ch", ThreeLetterCode = "cha")]
        Chamorro,

        [LanguageCode(LongName = "Chechen", TwoLetterCode = "ce", ThreeLetterCode = "che")]
        Chechen,

        [LanguageCode(LongName = "Cherokee", ThreeLetterCode = "chr")]
        Cherokee,

        [LanguageCode(LongName = "Cheyenne", ThreeLetterCode = "chy")]
        Cheyenne,

        [LanguageCode(LongName = "Chibcha", ThreeLetterCode = "chb")]
        Chibcha,

        // Chichewa; Chewa; Nyanja
        [LanguageCode(LongName = "Chichewa", TwoLetterCode = "ny", ThreeLetterCode = "nya")]
        Chichewa,

        [LanguageCode(LongName = "Chinese", TwoLetterCode = "zh", ThreeLetterCode = "chi", ThreeLetterCodeAlt = "zho")]
        Chinese,

        [LanguageCode(LongName = "Chinook jargon", ThreeLetterCode = "chn")]
        ChinookJargon,

        // Chipewyan; Dene Suline
        [LanguageCode(LongName = "Chipewyan", ThreeLetterCode = "chp")]
        Chipewyan,

        [LanguageCode(LongName = "Choctaw", ThreeLetterCode = "cho")]
        Choctaw,

        // Church Slavic; Old Slavonic; Church Slavonic; Old Bulgarian; Old Church Slavonic
        [LanguageCode(LongName = "Church Slavic", TwoLetterCode = "cu", ThreeLetterCode = "chu")]
        ChurchSlavic,

        [LanguageCode(LongName = "Chuukese", ThreeLetterCode = "chk")]
        Chuukese,

        [LanguageCode(LongName = "Chuvash", TwoLetterCode = "cv", ThreeLetterCode = "chv")]
        Chuvash,

        // Classical Newari; Old Newari; Classical Nepal Bhasa
        [LanguageCode(LongName = "Classical Newari", ThreeLetterCode = "nwc")]
        ClassicalNewari,

        [LanguageCode(LongName = "Coptic", ThreeLetterCode = "cop")]
        Coptic,

        [LanguageCode(LongName = "Cornish", TwoLetterCode = "kw", ThreeLetterCode = "cor")]
        Cornish,

        [LanguageCode(LongName = "Corsican", TwoLetterCode = "co", ThreeLetterCode = "cos")]
        Corsican,

        [LanguageCode(LongName = "Cree", TwoLetterCode = "cr", ThreeLetterCode = "cre")]
        Cree,

        [LanguageCode(LongName = "Creek", ThreeLetterCode = "mus")]
        Creek,

        [LanguageCode(LongName = "Creoles and pidgins", ThreeLetterCode = "crp")]
        CreolesAndPidgins,

        [LanguageCode(LongName = "Creoles and pidgins (English-based)", ThreeLetterCode = "cpe")]
        EnglishCreole,

        [LanguageCode(LongName = "Creoles and pidgins (French-based)", ThreeLetterCode = "cpf")]
        FrenchCreole,

        [LanguageCode(LongName = "Creoles and pidgins (Portuguese-based)", ThreeLetterCode = "cpp")]
        PortugueseCreole,

        // Crimean Tatar; Crimean Turkish
        [LanguageCode(LongName = "Crimean Tatar", ThreeLetterCode = "crh")]
        CrimeanTatar,

        [LanguageCode(LongName = "Croatian", TwoLetterCode = "hr", ThreeLetterCode = "hrv")]
        Croatian,

        [LanguageCode(LongName = "Czech", TwoLetterCode = "cs", ThreeLetterCode = "cze", ThreeLetterCodeAlt = "ces")]
        Czech,

        #endregion

        #region D

        [LanguageCode(LongName = "Dakota", ThreeLetterCode = "dak")]
        Dakota,

        [LanguageCode(LongName = "Danish", TwoLetterCode = "da", ThreeLetterCode = "dan")]
        Danish,

        [LanguageCode(LongName = "Dargwa", ThreeLetterCode = "dar")]
        Dargwa,

        [LanguageCode(LongName = "Delaware", ThreeLetterCode = "del")]
        Delaware,

        [LanguageCode(LongName = "Dinka", ThreeLetterCode = "din")]
        Dinka,

        // Divehi; Dhivehi; Maldivian
        [LanguageCode(LongName = "Divehi", TwoLetterCode = "dv", ThreeLetterCode = "div")]
        Divehi,

        [LanguageCode(LongName = "Dogrib", ThreeLetterCode = "dgr")]
        Dogrib,

        [LanguageCode(LongName = "Dogri", ThreeLetterCode = "doi")]
        Dogri,

        [LanguageCode(LongName = "Duala", ThreeLetterCode = "dua")]
        Duala,

        // Dutch; Flemish
        [LanguageCode(LongName = "Dutch", TwoLetterCode = "nl", ThreeLetterCode = "dut", ThreeLetterCodeAlt = "nld")]
        Dutch,

        [LanguageCode(LongName = "Dutch, Middle (ca.1050-1350)", ThreeLetterCode = "dum")]
        MiddleDutch,

        [LanguageCode(LongName = "Dyula", ThreeLetterCode = "dyu")]
        Dyula,

        [LanguageCode(LongName = "Dzongkha", TwoLetterCode = "dz", ThreeLetterCode = "dzo")]
        Dzongkha,

        #endregion

        #region E

        [LanguageCode(LongName = "Eastern Frisian", ThreeLetterCode = "frs")]
        EasternFrisian,

        [LanguageCode(LongName = "Efik", ThreeLetterCode = "efi")]
        Efik,

        [LanguageCode(LongName = "Egyptian (Ancient)", ThreeLetterCode = "egy")]
        AncientEgyptian,

        [LanguageCode(LongName = "Ekajuk", ThreeLetterCode = "eka")]
        Ekajuk,

        [LanguageCode(LongName = "Elamite", ThreeLetterCode = "elx")]
        Elamite,

        [LanguageCode(LongName = "English", TwoLetterCode = "en", ThreeLetterCode = "eng")]
        English,

        [LanguageCode(LongName = "English, Old (ca.450-1100)", ThreeLetterCode = "ang")]
        OldEnglish,

        [LanguageCode(LongName = "English, Middle (1100-1500)", ThreeLetterCode = "enm")]
        MiddleEnglish,

        [LanguageCode(LongName = "Erzya", ThreeLetterCode = "myv")]
        Erzya,

        [LanguageCode(LongName = "Esperanto", TwoLetterCode = "eo", ThreeLetterCode = "epo")]
        Esperanto,

        [LanguageCode(LongName = "Estonian", TwoLetterCode = "et", ThreeLetterCode = "est")]
        Estonian,

        [LanguageCode(LongName = "Ewe", TwoLetterCode = "ee", ThreeLetterCode = "ewe")]
        Ewe,

        [LanguageCode(LongName = "Ewondo", ThreeLetterCode = "ewo")]
        Ewondo,

        #endregion

        #region F

        [LanguageCode(LongName = "Fang", ThreeLetterCode = "fan")]
        Fang,

        [LanguageCode(LongName = "Faroese", TwoLetterCode = "fo", ThreeLetterCode = "fao")]
        Faroese,

        [LanguageCode(LongName = "Fanti", ThreeLetterCode = "fat")]
        Fanti,

        [LanguageCode(LongName = "Fijian", TwoLetterCode = "fj", ThreeLetterCode = "fij")]
        Fijian,

        // Filipino; Pilipino
        [LanguageCode(LongName = "Filipino", ThreeLetterCode = "fil")]
        Filipino,

        [LanguageCode(LongName = "Finnish", TwoLetterCode = "fi", ThreeLetterCode = "fin")]
        Finnish,

        [LanguageCode(LongName = "Fon", ThreeLetterCode = "fon")]
        Fon,

        [LanguageCode(LongName = "French", TwoLetterCode = "fr", ThreeLetterCode = "fre", ThreeLetterCodeAlt = "fra")]
        French,

        [LanguageCode(LongName = "French, Middle (ca.1400-1600)", ThreeLetterCode = "frm")]
        MiddleFrench,

        [LanguageCode(LongName = "French, Old (842-ca.1400)", ThreeLetterCode = "fro")]
        OldFrench,

        [LanguageCode(LongName = "Friulian", ThreeLetterCode = "fur")]
        Friulian,

        [LanguageCode(LongName = "Fulah", TwoLetterCode = "ff", ThreeLetterCode = "ful")]
        Fulah,

        #endregion

        #region G

        [LanguageCode(LongName = "Ga", ThreeLetterCode = "gaa")]
        Ga,

        // Gaelic; Scottish Gaelic
        [LanguageCode(LongName = "Gaelic", TwoLetterCode = "gd", ThreeLetterCode = "gla")]
        Gaelic,

        [LanguageCode(LongName = "Galibi Carib", ThreeLetterCode = "car")]
        GalibiCarib,

        [LanguageCode(LongName = "Galician", TwoLetterCode = "gl", ThreeLetterCode = "glg")]
        Galician,

        [LanguageCode(LongName = "Ganda", TwoLetterCode = "lg", ThreeLetterCode = "lug")]
        Ganda,

        [LanguageCode(LongName = "Gayo", ThreeLetterCode = "gay")]
        Gayo,

        [LanguageCode(LongName = "Gbaya", ThreeLetterCode = "gba")]
        Gbaya,

        [LanguageCode(LongName = "Geez", ThreeLetterCode = "gez")]
        Geez,

        [LanguageCode(LongName = "Georgian", TwoLetterCode = "ka", ThreeLetterCode = "geo", ThreeLetterCodeAlt = "kat")]
        Georgian,

        [LanguageCode(LongName = "German", TwoLetterCode = "de", ThreeLetterCode = "ger", ThreeLetterCodeAlt = "deu")]
        German,

        [LanguageCode(LongName = "German, Middle High (ca.1050-1500)", ThreeLetterCode = "gmh")]
        MiddleHighGerman,

        [LanguageCode(LongName = "German, Old High (ca.750-1050)", ThreeLetterCode = "goh")]
        OldHighGerman,

        [LanguageCode(LongName = "Gilbertese", ThreeLetterCode = "gil")]
        Gilbertese,

        [LanguageCode(LongName = "Gondi", ThreeLetterCode = "gon")]
        Gondi,

        [LanguageCode(LongName = "Gorontalo", ThreeLetterCode = "gor")]
        Gorontalo,

        [LanguageCode(LongName = "Gothic", ThreeLetterCode = "got")]
        Gothic,

        [LanguageCode(LongName = "Grebo", ThreeLetterCode = "grb")]
        Grebo,

        [LanguageCode(LongName = "Greek", TwoLetterCode = "el", ThreeLetterCode = "gre", ThreeLetterCodeAlt = "eli")]
        Greek,

        [LanguageCode(LongName = "Greek, Ancient (to 1453)", ThreeLetterCode = "grc")]
        AncientGreek,

        [LanguageCode(LongName = "Guarani", TwoLetterCode = "gn", ThreeLetterCode = "grn")]
        Guarani,

        [LanguageCode(LongName = "Gujarati", TwoLetterCode = "gu", ThreeLetterCode = "guj")]
        Gujarati,

        [LanguageCode(LongName = "Gwich'in", ThreeLetterCode = "gwi")]
        Gwichin,

        #endregion

        #region H

        [LanguageCode(LongName = "Haida", ThreeLetterCode = "hai")]
        Haida,

        // Haitian; Haitian Creole
        [LanguageCode(LongName = "Haitian", TwoLetterCode = "ht", ThreeLetterCode = "hat")]
        Haitian,

        [LanguageCode(LongName = "Hausa", TwoLetterCode = "ha", ThreeLetterCode = "gau")]
        Hausa,

        [LanguageCode(LongName = "Hawaiian", ThreeLetterCode = "haw")]
        Hawaiian,

        [LanguageCode(LongName = "Hebrew", TwoLetterCode = "he", ThreeLetterCode = "heb")]
        Hebrew,

        [LanguageCode(LongName = "Herero", TwoLetterCode = "hz", ThreeLetterCode = "her")]
        Herero,

        [LanguageCode(LongName = "Hiligaynon", ThreeLetterCode = "hil")]
        Hiligaynon,

        [LanguageCode(LongName = "Hindi", TwoLetterCode = "hi", ThreeLetterCode = "hin")]
        Hindi,

        [LanguageCode(LongName = "Hiri Motu", ThreeLetterCode = "hmo")]
        HiriMotu,

        [LanguageCode(LongName = "Hittite", ThreeLetterCode = "hit")]
        Hittite,

        // Hmong; Mong
        [LanguageCode(LongName = "Hmong", ThreeLetterCode = "hmn")]
        Hmong,

        [LanguageCode(LongName = "Hungarian", TwoLetterCode = "hu", ThreeLetterCode = "hun")]
        Hungarian,

        [LanguageCode(LongName = "Hupa", ThreeLetterCode = "hup")]
        Hupa,

        #endregion

        #region I

        [LanguageCode(LongName = "Iban", ThreeLetterCode = "iba")]
        Iban,

        [LanguageCode(LongName = "Icelandic", TwoLetterCode = "is", ThreeLetterCode = "ice", ThreeLetterCodeAlt = "isl")]
        Icelandic,

        [LanguageCode(LongName = "Ido", TwoLetterCode = "io", ThreeLetterCode = "ido")]
        Ido,

        [LanguageCode(LongName = "Igbo", TwoLetterCode = "ig", ThreeLetterCode = "ibo")]
        Igbo,

        [LanguageCode(LongName = "Iloko", ThreeLetterCode = "ilo")]
        Iloko,

        [LanguageCode(LongName = "Inari Sami", ThreeLetterCode = "smn")]
        InariSami,

        [LanguageCode(LongName = "Indonesian", TwoLetterCode = "id", ThreeLetterCode = "ind")]
        Indonesian,

        [LanguageCode(LongName = "Ingush", ThreeLetterCode = "inh")]
        Ingush,

        // Interlingua (International Auxiliary Language Association)
        [LanguageCode(LongName = "Interlingua", TwoLetterCode = "ia", ThreeLetterCode = "ina")]
        Interlingua,

        // Interlingue; Occidental
        [LanguageCode(LongName = "Interlingue", TwoLetterCode = "ie", ThreeLetterCode = "ile")]
        Interlingue,

        [LanguageCode(LongName = "Inuktitut", TwoLetterCode = "iu", ThreeLetterCode = "iku")]
        Inuktitut,

        [LanguageCode(LongName = "Inupiaq", TwoLetterCode = "ik", ThreeLetterCode = "ipk")]
        Inupiaq,

        [LanguageCode(LongName = "Irish", TwoLetterCode = "ga", ThreeLetterCode = "gle")]
        Irish,

        [LanguageCode(LongName = "Irish, Middle (900-1200)", ThreeLetterCode = "mga")]
        MiddleIrish,

        [LanguageCode(LongName = "Irish, Old (to 900)", ThreeLetterCode = "sga")]
        OldIrish,

        [LanguageCode(LongName = "Italian", TwoLetterCode = "it", ThreeLetterCode = "ita")]
        Italian,

        #endregion

        #region J

        [LanguageCode(LongName = "Japanese", TwoLetterCode = "ja", ThreeLetterCode = "jap")]
        Japanese,

        [LanguageCode(LongName = "Javanese", TwoLetterCode = "jv", ThreeLetterCode = "jav")]
        Javanese,

        [LanguageCode(LongName = "Judeo-Arabic", ThreeLetterCode = "jrb")]
        JudeoArabic,

        [LanguageCode(LongName = "Judeo-Persian", ThreeLetterCode = "jpr")]
        JudeoPersian,

        #endregion

        #region K

        [LanguageCode(LongName = "Kabardian", ThreeLetterCode = "kbd")]
        Kabardian,

        [LanguageCode(LongName = "Kabyle", ThreeLetterCode = "kab")]
        Kabyle,

        // Kachin; Jingpho
        [LanguageCode(LongName = "Kachin", ThreeLetterCode = "kac")]
        Kachin,

        // Kalaallisut; Greenlandic
        [LanguageCode(LongName = "Kalaallisut", TwoLetterCode = "kl", ThreeLetterCode = "kal")]
        Kalaallisut,

        // Kalmyk; Oirat
        [LanguageCode(LongName = "Kalmyk", ThreeLetterCode = "xal")]
        Kalmyk,

        [LanguageCode(LongName = "Kamba", ThreeLetterCode = "kam")]
        Kamba,

        [LanguageCode(LongName = "Kannada", TwoLetterCode = "kn", ThreeLetterCode = "kan")]
        Kannada,

        [LanguageCode(LongName = "Kanuri", TwoLetterCode = "kr", ThreeLetterCode = "kau")]
        Kanuri,

        [LanguageCode(LongName = "Karachay-Balkar", ThreeLetterCode = "krc")]
        KarachayBalkar,

        [LanguageCode(LongName = "Kara-Kalpak", ThreeLetterCode = "kaa")]
        KaraKalpak,

        [LanguageCode(LongName = "Karelian", ThreeLetterCode = "krl")]
        Karelian,

        [LanguageCode(LongName = "Kashmiri", TwoLetterCode = "ks", ThreeLetterCode = "kas")]
        Kashmiri,

        [LanguageCode(LongName = "Kashubian", ThreeLetterCode = "csb")]
        Kashubian,

        [LanguageCode(LongName = "Kawi", ThreeLetterCode = "kaw")]
        Kawi,

        [LanguageCode(LongName = "Kazakh", TwoLetterCode = "kk", ThreeLetterCode = "kaz")]
        Kazakh,

        [LanguageCode(LongName = "Khasi", ThreeLetterCode = "kha")]
        Khasi,

        // Khotanese; Sakan
        [LanguageCode(LongName = "Khotanese", ThreeLetterCode = "kho")]
        Khotanese,

        // Kikuyu; Gikuyu
        [LanguageCode(LongName = "Kikuyu", TwoLetterCode = "ki", ThreeLetterCode = "kik")]
        Kikuyu,

        [LanguageCode(LongName = "Kimbundu", ThreeLetterCode = "kmb")]
        Kimbundu,

        [LanguageCode(LongName = "Kinyarwanda", TwoLetterCode = "rw", ThreeLetterCode = "kin")]
        Kinyarwanda,

        // Kirghiz; Kyrgyz
        [LanguageCode(LongName = "Kirghiz", TwoLetterCode = "ky", ThreeLetterCode = "kir")]
        Kirghiz,

        // Klingon; tlhIngan-Hol
        [LanguageCode(LongName = "Klingon", ThreeLetterCode = "tlh")]
        Klingon,

        [LanguageCode(LongName = "Komi", TwoLetterCode = "kv", ThreeLetterCode = "kom")]
        Komi,

        [LanguageCode(LongName = "Kongo", TwoLetterCode = "kg", ThreeLetterCode = "kon")]
        Kongo,

        [LanguageCode(LongName = "Konkani", ThreeLetterCode = "kok")]
        Konkani,

        [LanguageCode(LongName = "Korean", TwoLetterCode = "ko", ThreeLetterCode = "kor")]
        Korean,

        [LanguageCode(LongName = "Kosraean", ThreeLetterCode = "kos")]
        Kosraean,

        [LanguageCode(LongName = "Kpelle", ThreeLetterCode = "kpe")]
        Kpelle,

        // Kuanyama; Kwanyama
        [LanguageCode(LongName = "Kuanyama", TwoLetterCode = "kj", ThreeLetterCode = "kua")]
        Kuanyama,

        [LanguageCode(LongName = "Kumyk", ThreeLetterCode = "kum")]
        Kumyk,

        [LanguageCode(LongName = "Kurdish", TwoLetterCode = "ku", ThreeLetterCode = "kur")]
        Kurdish,

        [LanguageCode(LongName = "Kurukh", ThreeLetterCode = "kru")]
        Kurukh,

        [LanguageCode(LongName = "Kutenai", ThreeLetterCode = "kut")]
        Kutenai,

        #endregion

        #region L

        [LanguageCode(LongName = "Ladino", ThreeLetterCode = "lad")]
        Ladino,

        [LanguageCode(LongName = "Lahnda", ThreeLetterCode = "lah")]
        Lahnda,

        [LanguageCode(LongName = "Lamba", ThreeLetterCode = "lam")]
        Lamba,

        [LanguageCode(LongName = "Lao", TwoLetterCode = "lo", ThreeLetterCode = "lao")]
        Lao,

        [LanguageCode(LongName = "Latin", TwoLetterCode = "la", ThreeLetterCode = "lat")]
        Latin,

        [LanguageCode(LongName = "Latvian", TwoLetterCode = "lv", ThreeLetterCode = "lav")]
        Latvian,

        [LanguageCode(LongName = "Lezghian", ThreeLetterCode = "lez")]
        Lezghian,

        // Limburgan; Limburger; Limburgish
        [LanguageCode(LongName = "Limburgan", TwoLetterCode = "li", ThreeLetterCode = "lim")]
        Limburgan,

        [LanguageCode(LongName = "Lingala", TwoLetterCode = "ln", ThreeLetterCode = "lin")]
        Lingala,

        [LanguageCode(LongName = "Lithuanian", TwoLetterCode = "lt", ThreeLetterCode = "lit")]
        Lithuanian,

        [LanguageCode(LongName = "Lojban", ThreeLetterCode = "jbo")]
        Lojban,

        // Low German; Low Saxon
        [LanguageCode(LongName = "Low German", ThreeLetterCode = "nds")]
        LowGerman,

        [LanguageCode(LongName = "Lower Sorbian", ThreeLetterCode = "dsb")]
        LowerSorbian,

        [LanguageCode(LongName = "Lozi", ThreeLetterCode = "loz")]
        Lozi,

        [LanguageCode(LongName = "Luba-Katanga", TwoLetterCode = "lu", ThreeLetterCode = "lub")]
        LubaKatanga,

        [LanguageCode(LongName = "Luba-Lulua", ThreeLetterCode = "lua")]
        LubaLulua,

        [LanguageCode(LongName = "Luiseno", ThreeLetterCode = "lui")]
        Luiseno,

        [LanguageCode(LongName = "Lule Sami", ThreeLetterCode = "smj")]
        LuleSami,

        [LanguageCode(LongName = "Lunda", ThreeLetterCode = "lun")]
        Lunda,

        [LanguageCode(LongName = "Luo (Kenya and Tanzania)", ThreeLetterCode = "luo")]
        Luo,

        [LanguageCode(LongName = "Lushai", ThreeLetterCode = "lus")]
        Lushai,

        // Luxembourgish; Letzeburgesch
        [LanguageCode(LongName = "Luxembourgish", TwoLetterCode = "lb", ThreeLetterCode = "ltz")]
        Luxembourgish,

        #endregion

        #region M

        [LanguageCode(LongName = "Macedonian", TwoLetterCode = "mk", ThreeLetterCode = "mac", ThreeLetterCodeAlt = "mkd")]
        Macedonian,

        [LanguageCode(LongName = "Madurese", ThreeLetterCode = "mad")]
        Madurese,

        [LanguageCode(LongName = "Magahi", ThreeLetterCode = "mag")]
        Magahi,

        [LanguageCode(LongName = "Maithili", ThreeLetterCode = "mai")]
        Maithili,

        [LanguageCode(LongName = "Makasar", ThreeLetterCode = "mak")]
        Makasar,

        [LanguageCode(LongName = "Malagasy", TwoLetterCode = "mg", ThreeLetterCode = "mlg")]
        Malagasy,

        [LanguageCode(LongName = "Malay", TwoLetterCode = "ms", ThreeLetterCode = "may", ThreeLetterCodeAlt = "msa")]
        Malay,

        [LanguageCode(LongName = "Malayalam", TwoLetterCode = "ml", ThreeLetterCode = "mal")]
        Malayalam,

        [LanguageCode(LongName = "Maltese", TwoLetterCode = "mt", ThreeLetterCode = "mlt")]
        Maltese,

        [LanguageCode(LongName = "Manchu", ThreeLetterCode = "mnc")]
        Manchu,

        [LanguageCode(LongName = "Mandar", ThreeLetterCode = "mdr")]
        Mandar,

        [LanguageCode(LongName = "Mandingo", ThreeLetterCode = "man")]
        Mandingo,

        [LanguageCode(LongName = "Manipuri", ThreeLetterCode = "mni")]
        Manipuri,

        [LanguageCode(LongName = "Manx", TwoLetterCode = "gv", ThreeLetterCode = "glv")]
        Manx,

        [LanguageCode(LongName = "Maori", TwoLetterCode = "mi", ThreeLetterCode = "mao", ThreeLetterCodeAlt = "mri")]
        Maori,

        // Mapudungun; Mapuche
        [LanguageCode(LongName = "Mapudungun", ThreeLetterCode = "arn")]
        Mapudungun,

        [LanguageCode(LongName = "Marathi", TwoLetterCode = "mr", ThreeLetterCode = "mar")]
        Marathi,

        [LanguageCode(LongName = "Mari", ThreeLetterCode = "chm")]
        Mari,

        [LanguageCode(LongName = "Marshallese", TwoLetterCode = "mh", ThreeLetterCode = "mah")]
        Marshallese,

        [LanguageCode(LongName = "Marwari", ThreeLetterCode = "mwr")]
        Marwari,

        [LanguageCode(LongName = "Masai", ThreeLetterCode = "mas")]
        Masai,

        [LanguageCode(LongName = "Mende", ThreeLetterCode = "men")]
        Mende,

        // Mi'kmaq; Micmac
        [LanguageCode(LongName = "Mi'kmaq", ThreeLetterCode = "mic")]
        Mikmaq,

        [LanguageCode(LongName = "Minangkabau", ThreeLetterCode = "min")]
        Minangkabau,

        [LanguageCode(LongName = "Mirandese", ThreeLetterCode = "mwl")]
        Mirandese,

        [LanguageCode(LongName = "Mohawk", ThreeLetterCode = "moh")]
        Mohawk,

        [LanguageCode(LongName = "Moksha", ThreeLetterCode = "mdf")]
        Moksha,

        [LanguageCode(LongName = "Mongo", ThreeLetterCode = "lol")]
        Mongo,

        [LanguageCode(LongName = "Mongolian", TwoLetterCode = "mn", ThreeLetterCode = "mon")]
        Mongolian,

        [LanguageCode(LongName = "Montenegrin", ThreeLetterCode = "cnr")]
        Montenegrin,

        [LanguageCode(LongName = "Mossi", ThreeLetterCode = "mos")]
        Mossi,

        #endregion

        #region N

        [LanguageCode(LongName = "N'Ko", ThreeLetterCode = "nqo")]
        NKo,

        [LanguageCode(LongName = "Nauru", TwoLetterCode = "na", ThreeLetterCode = "nau")]
        Nauru,

        // Navajo; Navaho
        [LanguageCode(LongName = "Navajo", TwoLetterCode = "nv", ThreeLetterCode = "nav")]
        Navajo,

        [LanguageCode(LongName = "Ndonga", TwoLetterCode = "ng", ThreeLetterCode = "ndo")]
        Ndonga,

        [LanguageCode(LongName = "Neapolitan", ThreeLetterCode = "nap")]
        Neapolitan,

        // Nepal Bhasa; Newari
        [LanguageCode(LongName = "Nepal Bhasa", ThreeLetterCode = "new")]
        NepalBhasa,

        [LanguageCode(LongName = "Nepali", TwoLetterCode = "ne", ThreeLetterCode = "nep")]
        Nepali,

        [LanguageCode(LongName = "Nias", ThreeLetterCode = "nia")]
        Nias,

        [LanguageCode(LongName = "Niuean", ThreeLetterCode = "niu")]
        Niuean,

        // Commented out to avoid confusion
        //[Language(LongName = "No linguistic content; Not applicable", ThreeLetterCode = "zxx")]
        //NoLinguisticContent,

        [LanguageCode(LongName = "Nogai", ThreeLetterCode = "nog")]
        Nogai,

        [LanguageCode(LongName = "Norse, Old", ThreeLetterCode = "non")]
        OldNorse,

        [LanguageCode(LongName = "North Ndebele", TwoLetterCode = "nd", ThreeLetterCode = "nde")]
        NorthNdebele,

        [LanguageCode(LongName = "Northern Frisian", ThreeLetterCode = "frr")]
        NorthernFrisian,

        [LanguageCode(LongName = "Northern Sami", TwoLetterCode = "se", ThreeLetterCode = "sme")]
        NorthernSami,

        [LanguageCode(LongName = "Norwegian", TwoLetterCode = "no", ThreeLetterCode = "nor")]
        Norwegian,

        [LanguageCode(LongName = "Norwegian Bokmål", TwoLetterCode = "nb", ThreeLetterCode = "nob")]
        NorwegianBokmal,

        [LanguageCode(LongName = "Norwegian Nynorsk", TwoLetterCode = "nn", ThreeLetterCode = "nno")]
        NorwegianNynorsk,

        [LanguageCode(LongName = "Nyamwezi", ThreeLetterCode = "nym")]
        Nyamwezi,

        [LanguageCode(LongName = "Nyankole", ThreeLetterCode = "nyn")]
        Nyankole,

        [LanguageCode(LongName = "Nyoro", ThreeLetterCode = "nyo")]
        Nyoro,

        [LanguageCode(LongName = "Nzima", ThreeLetterCode = "nzi")]
        Nzima,

        #endregion

        #region O

        [LanguageCode(LongName = "Occitan (post 1500)", TwoLetterCode = "oc", ThreeLetterCode = "oci")]
        Occitan,

        // Occitan, Old (to 1500); Provençal, Old (to 1500)
        [LanguageCode(LongName = "Occitan, Old (to 1500)", ThreeLetterCode = "pro")]
        OldOccitan,

        [LanguageCode(LongName = "Ojibwa", TwoLetterCode = "oj", ThreeLetterCode = "oji")]
        Ojibwa,

        [LanguageCode(LongName = "Oriya", TwoLetterCode = "or", ThreeLetterCode = "ori")]
        Oriya,

        [LanguageCode(LongName = "Oromo", TwoLetterCode = "om", ThreeLetterCode = "orm")]
        Oromo,

        [LanguageCode(LongName = "Osage", ThreeLetterCode = "osa")]
        Osage,

        // Ossetian; Ossetic
        [LanguageCode(LongName = "Ossetian", TwoLetterCode = "os", ThreeLetterCode = "oss")]
        Ossetian,

        #endregion

        #region P

        [LanguageCode(LongName = "Pahlavi", ThreeLetterCode = "pal")]
        Pahlavi,

        [LanguageCode(LongName = "Palauan", ThreeLetterCode = "pau")]
        Palauan,

        [LanguageCode(LongName = "Pali", TwoLetterCode = "pi", ThreeLetterCode = "pli")]
        Pali,

        // Pampanga; Kapampangan
        [LanguageCode(LongName = "Pampanga", ThreeLetterCode = "pam")]
        Pampanga,

        [LanguageCode(LongName = "Pangasinan", ThreeLetterCode = "pag")]
        Pangasinan,

        // Panjabi; Punjabi
        [LanguageCode(LongName = "Panjabi", TwoLetterCode = "pa", ThreeLetterCode = "pan")]
        Panjabi,

        [LanguageCode(LongName = "Papiamento", ThreeLetterCode = "pap")]
        Papiamento,

        // Pedi; Sepedi; Northern Sotho
        [LanguageCode(LongName = "Pedi", ThreeLetterCode = "nso")]
        Pedi,

        [LanguageCode(LongName = "Persian", TwoLetterCode = "fa", ThreeLetterCode = "per", ThreeLetterCodeAlt = "fas")]
        Persian,

        [LanguageCode(LongName = "Persian, Old (ca.600-400 B.C.)", ThreeLetterCode = "peo")]
        OldPersian,

        [LanguageCode(LongName = "Phoenician", ThreeLetterCode = "phn")]
        Phoenician,

        [LanguageCode(LongName = "Polish", TwoLetterCode = "pl", ThreeLetterCode = "pol")]
        Polish,

        [LanguageCode(LongName = "Portuguese", TwoLetterCode = "pt", ThreeLetterCode = "por")]
        Portuguese,

        // Pushto; Pashto
        [LanguageCode(LongName = "Pushto", TwoLetterCode = "ps", ThreeLetterCode = "pus")]
        Pushto,

        #endregion

        #region Q

        // qaa-qtz: Reserved for local use

        [LanguageCode(LongName = "Quechua", TwoLetterCode = "qu", ThreeLetterCode = "que")]
        Quechua,

        #endregion

        #region R

        [LanguageCode(LongName = "Rajasthani", ThreeLetterCode = "raj")]
        Rajasthani,

        [LanguageCode(LongName = "Rapanui", ThreeLetterCode = "rap")]
        Rapanui,

        // Rarotongan; Cook Islands Maori
        [LanguageCode(LongName = "Rarotongan", ThreeLetterCode = "rar")]
        Rarotongan,

        // Romanian; Moldavian; Moldovan
        [LanguageCode(LongName = "Romanian", TwoLetterCode = "ro", ThreeLetterCode = "rum", ThreeLetterCodeAlt = "ron")]
        Romanian,

        [LanguageCode(LongName = "Romansh", TwoLetterCode = "rm", ThreeLetterCode = "roh")]
        Romansh,

        [LanguageCode(LongName = "Romany", ThreeLetterCode = "rom")]
        Romany,

        [LanguageCode(LongName = "Rundi", TwoLetterCode = "rn", ThreeLetterCode = "run")]
        Rundi,

        [LanguageCode(LongName = "Russian", TwoLetterCode = "ru", ThreeLetterCode = "rus")]
        Russian,

        #endregion

        #region S

        [LanguageCode(LongName = "Samaritan Aramaic", ThreeLetterCode = "sam")]
        SamaritanAramaic,

        [LanguageCode(LongName = "Samoan", TwoLetterCode = "sm", ThreeLetterCode = "smo")]
        Samoan,

        [LanguageCode(LongName = "Sandawe", ThreeLetterCode = "sad")]
        Sandawe,

        [LanguageCode(LongName = "Sango", TwoLetterCode = "sg", ThreeLetterCode = "sag")]
        Sango,

        [LanguageCode(LongName = "Sanskrit", TwoLetterCode = "sa", ThreeLetterCode = "san")]
        Sanskrit,

        [LanguageCode(LongName = "Santali", ThreeLetterCode = "sat")]
        Santali,

        [LanguageCode(LongName = "Sardinian", TwoLetterCode = "sc", ThreeLetterCode = "srd")]
        Sardinian,

        [LanguageCode(LongName = "Sasak", ThreeLetterCode = "sas")]
        Sasak,

        [LanguageCode(LongName = "Scots", ThreeLetterCode = "sco")]
        Scots,

        [LanguageCode(LongName = "Selkup", ThreeLetterCode = "sel")]
        Selkup,

        [LanguageCode(LongName = "Serbian", TwoLetterCode = "sr", ThreeLetterCode = "srp")]
        Serbian,

        [LanguageCode(LongName = "Serer", ThreeLetterCode = "srr")]
        Serer,

        [LanguageCode(LongName = "Shan", ThreeLetterCode = "shn")]
        Shan,

        [LanguageCode(LongName = "Shona", TwoLetterCode = "sn", ThreeLetterCode = "sna")]
        Shona,

        // Sichuan Yi; Nuosu
        [LanguageCode(LongName = "Sichuan Yi", TwoLetterCode = "ii", ThreeLetterCode = "iii")]
        SichuanYi,

        [LanguageCode(LongName = "Sicilian", ThreeLetterCode = "scn")]
        Sicilian,

        [LanguageCode(LongName = "Sidamo", ThreeLetterCode = "sid")]
        Sidamo,

        [LanguageCode(LongName = "Sign Languages", ThreeLetterCode = "sgn")]
        SignLanguages,

        [LanguageCode(LongName = "Siksika", ThreeLetterCode = "bla")]
        Siksika,

        [LanguageCode(LongName = "Sindhi", TwoLetterCode = "sd", ThreeLetterCode = "snd")]
        Sindhi,

        // Sinhala; Sinhalese
        [LanguageCode(LongName = "Sinhala", TwoLetterCode = "si", ThreeLetterCode = "sin")]
        Sinhala,

        [LanguageCode(LongName = "Skolt Sami", ThreeLetterCode = "sms")]
        SkoltSami,

        [LanguageCode(LongName = "Slovak", TwoLetterCode = "sk", ThreeLetterCode = "slo", ThreeLetterCodeAlt = "slk")]
        Slovak,

        [LanguageCode(LongName = "Slovenian", TwoLetterCode = "sl", ThreeLetterCode = "slv")]
        Slovenian,

        [LanguageCode(LongName = "Sogdian", ThreeLetterCode = "sog")]
        Sogdian,

        [LanguageCode(LongName = "Somali", TwoLetterCode = "so", ThreeLetterCode = "som")]
        Somali,

        [LanguageCode(LongName = "Soninke", ThreeLetterCode = "snk")]
        Soninke,

        [LanguageCode(LongName = "Sotho, Southern", TwoLetterCode = "st", ThreeLetterCode = "sot")]
        Sotho,

        [LanguageCode(LongName = "South Ndebele", TwoLetterCode = "nr", ThreeLetterCode = "nbl")]
        SouthNdebele,

        [LanguageCode(LongName = "Southern Altai", ThreeLetterCode = "alt")]
        SouthernAltai,

        [LanguageCode(LongName = "Southern Sami", ThreeLetterCode = "sma")]
        SouthernSami,

        // Spanish; Castilian
        [LanguageCode(LongName = "Spanish", TwoLetterCode = "es", ThreeLetterCode = "spa")]
        Spanish,

        [LanguageCode(LongName = "Sranan Tongo", ThreeLetterCode = "srn")]
        SrananTongo,

        [LanguageCode(LongName = "Standard Moroccan Tamazight", ThreeLetterCode = "zgh")]
        StandardMoroccanTamazight,

        [LanguageCode(LongName = "Sukuma", ThreeLetterCode = "suk")]
        Sukuma,

        [LanguageCode(LongName = "Sumerian", ThreeLetterCode = "sux")]
        Sumerian,

        [LanguageCode(LongName = "Sundanese", TwoLetterCode = "su", ThreeLetterCode = "sun")]
        Sundanese,

        [LanguageCode(LongName = "Susu", ThreeLetterCode = "sus")]
        Susu,

        [LanguageCode(LongName = "Susu", TwoLetterCode = "sw", ThreeLetterCode = "swa")]
        Swahili,

        [LanguageCode(LongName = "Swatio", TwoLetterCode = "ss", ThreeLetterCode = "ssw")]
        Swati,

        [LanguageCode(LongName = "Swedish", TwoLetterCode = "sv", ThreeLetterCode = "swe")]
        Swedish,

        // Swiss German; Alemannic; Alsatian
        [LanguageCode(LongName = "Swiss German", ThreeLetterCode = "gsw")]
        SwissGerman,

        [LanguageCode(LongName = "Syriac", ThreeLetterCode = "syr")]
        Syriac,

        [LanguageCode(LongName = "Syriac, Classical", ThreeLetterCode = "syc")]
        ClassicalSyriac,

        #endregion

        #region T

        [LanguageCode(LongName = "Tagalog", TwoLetterCode = "tl", ThreeLetterCode = "tgl")]
        Tagalog,

        [LanguageCode(LongName = "Tahitian", TwoLetterCode = "ty", ThreeLetterCode = "tah")]
        Tahitian,

        [LanguageCode(LongName = "Tajik", TwoLetterCode = "tg", ThreeLetterCode = "tgk")]
        Tajik,

        [LanguageCode(LongName = "Tamashek", ThreeLetterCode = "tmh")]
        Tamashek,

        [LanguageCode(LongName = "Tamil", TwoLetterCode = "ta", ThreeLetterCode = "tam")]
        Tamil,

        [LanguageCode(LongName = "Tatar", TwoLetterCode = "tt", ThreeLetterCode = "tat")]
        Tatar,

        [LanguageCode(LongName = "Telugu", TwoLetterCode = "te", ThreeLetterCode = "tel")]
        Telugu,

        [LanguageCode(LongName = "Tereno", ThreeLetterCode = "ter")]
        Tereno,

        [LanguageCode(LongName = "Tetum", ThreeLetterCode = "tet")]
        Tetum,

        [LanguageCode(LongName = "Thai", TwoLetterCode = "th", ThreeLetterCode = "tha")]
        Thai,

        [LanguageCode(LongName = "Tibetan", TwoLetterCode = "bo", ThreeLetterCode = "tib", ThreeLetterCodeAlt = "bod")]
        Tibetan,

        [LanguageCode(LongName = "Tigre", ThreeLetterCode = "tig")]
        Tigre,

        [LanguageCode(LongName = "Tigrinya", TwoLetterCode = "ti", ThreeLetterCode = "tir")]
        Tigrinya,

        [LanguageCode(LongName = "Timne", ThreeLetterCode = "tem")]
        Timne,

        [LanguageCode(LongName = "Tiv", ThreeLetterCode = "tiv")]
        Tiv,

        [LanguageCode(LongName = "Tlingit", ThreeLetterCode = "tli")]
        Tlingit,

        [LanguageCode(LongName = "Tok Pisin", ThreeLetterCode = "tpi")]
        TokPisin,

        [LanguageCode(LongName = "Tokelau", ThreeLetterCode = "tkl")]
        Tokelau,

        [LanguageCode(LongName = "Tonga (Nyasa)", ThreeLetterCode = "tog")]
        TongaNyasa,

        [LanguageCode(LongName = "Tonga (Tonga Islands)", TwoLetterCode = "to", ThreeLetterCode = "ton")]
        TongaIslands,

        [LanguageCode(LongName = "Tsimshian", ThreeLetterCode = "tsi")]
        Tsimshian,

        [LanguageCode(LongName = "Tsonga", TwoLetterCode = "ts", ThreeLetterCode = "tso")]
        Tsonga,

        [LanguageCode(LongName = "Tswana", TwoLetterCode = "tn", ThreeLetterCode = "tsn")]
        Tswana,

        [LanguageCode(LongName = "Tumbuka", ThreeLetterCode = "tum")]
        Tumbuka,

        [LanguageCode(LongName = "Turkish", TwoLetterCode = "tr", ThreeLetterCode = "tur")]
        Turkish,

        [LanguageCode(LongName = "Turkish, Ottoman (1500-1928)", ThreeLetterCode = "ota")]
        OttomanTurkish,

        [LanguageCode(LongName = "Turkmen", TwoLetterCode = "tk", ThreeLetterCode = "tuk")]
        Turkmen,

        [LanguageCode(LongName = "Tuvalu", ThreeLetterCode = "tvl")]
        Tuvalu,

        [LanguageCode(LongName = "Tuvinian", ThreeLetterCode = "tyv")]
        Tuvinian,

        [LanguageCode(LongName = "Twi", TwoLetterCode = "tw", ThreeLetterCode = "twi")]
        Twi,

        #endregion

        #region U

        [LanguageCode(LongName = "Udmurt", ThreeLetterCode = "udm")]
        Udmurt,

        [LanguageCode(LongName = "Ugaritic", ThreeLetterCode = "uga")]
        Ugaritic,

        // Uighur; Uyghur
        [LanguageCode(LongName = "Uighur", TwoLetterCode = "ug", ThreeLetterCode = "uig")]
        Uighur,

        [LanguageCode(LongName = "Ukrainian", TwoLetterCode = "uk", ThreeLetterCode = "ukr")]
        Ukrainian,

        [LanguageCode(LongName = "Umbundu", ThreeLetterCode = "umb")]
        Umbundu,

        // Commented out to avoid confusion
        //[Language(LongName = "Undetermined", ThreeLetterCode = "und")]
        //Undetermined,

        [LanguageCode(LongName = "Upper Sorbian", ThreeLetterCode = "hsb")]
        UpperSorbian,

        [LanguageCode(LongName = "Urdu", TwoLetterCode = "ur", ThreeLetterCode = "urd")]
        Urdu,

        [LanguageCode(LongName = "Uzbek", TwoLetterCode = "uz", ThreeLetterCode = "uzb")]
        Uzbek,

        #endregion

        #region V

        [LanguageCode(LongName = "Vai", ThreeLetterCode = "vai")]
        Vai,

        [LanguageCode(LongName = "Venda", TwoLetterCode = "ve", ThreeLetterCode = "ven")]
        Venda,

        [LanguageCode(LongName = "Vietnamese", TwoLetterCode = "vi", ThreeLetterCode = "vie")]
        Vietnamese,

        [LanguageCode(LongName = "Volapük", TwoLetterCode = "vo", ThreeLetterCode = "vol")]
        Volapuk,

        [LanguageCode(LongName = "Votic", ThreeLetterCode = "vot")]
        Votic,

        #endregion

        #region W

        [LanguageCode(LongName = "Walloon", TwoLetterCode = "wa", ThreeLetterCode = "wln")]
        Walloon,

        [LanguageCode(LongName = "Waray", ThreeLetterCode = "war")]
        Waray,

        [LanguageCode(LongName = "Washo", ThreeLetterCode = "was")]
        Washo,

        [LanguageCode(LongName = "Welsh", TwoLetterCode = "cy", ThreeLetterCode = "wel", ThreeLetterCodeAlt = "cym")]
        Welsh,

        [LanguageCode(LongName = "Western Frisian", TwoLetterCode = "fy", ThreeLetterCode = "fry")]
        WesternFrisian,

        // Wolaitta; Wolaytta
        [LanguageCode(LongName = "Wolaitta", ThreeLetterCode = "wal")]
        Wolaitta,

        [LanguageCode(LongName = "Wolof", TwoLetterCode = "wo", ThreeLetterCode = "wol")]
        Wolof,

        #endregion

        #region X

        [LanguageCode(LongName = "Xhosa", TwoLetterCode = "xh", ThreeLetterCode = "xho")]
        Xhosa,

        #endregion

        #region Y

        [LanguageCode(LongName = "Yakut", ThreeLetterCode = "sah")]
        Yakut,

        [LanguageCode(LongName = "Yao", ThreeLetterCode = "yao")]
        Yao,

        [LanguageCode(LongName = "Yapese", ThreeLetterCode = "yap")]
        Yapese,

        [LanguageCode(LongName = "Yiddish", TwoLetterCode = "yi", ThreeLetterCode = "yid")]
        Yiddish,

        [LanguageCode(LongName = "Yoruba", TwoLetterCode = "yo", ThreeLetterCode = "yor")]
        Yoruba,

        #endregion

        #region Z

        [LanguageCode(LongName = "Zapotec", ThreeLetterCode = "zap")]
        Zapotec,

        // Zaza; Dimili; Dimli; Kirdki; Kirmanjki; Zazaki
        [LanguageCode(LongName = "Zaza", ThreeLetterCode = "zza")]
        Zaza,

        [LanguageCode(LongName = "Zenaga", ThreeLetterCode = "zen")]
        Zenaga,

        // Zhuang; Chuang
        [LanguageCode(LongName = "Zhuang", TwoLetterCode = "za", ThreeLetterCode = "zha")]
        Zhuang,

        [LanguageCode(LongName = "Zulu", TwoLetterCode = "zu", ThreeLetterCode = "zul")]
        Zulu,

        [LanguageCode(LongName = "Zuni", ThreeLetterCode = "zun")]
        Zuni,

        #endregion

        #region Language Families

        /*
        [Language(LongName = "Afro-Asiatic languages", ThreeLetterCode = "afa")]
        AfroAsiaticLanguages,

        [Language(LongName = "Algonquian languages", ThreeLetterCode = "alg")]
        AlgonquianLanguages,

        [Language(LongName = "Altaic languages", ThreeLetterCode = "tut")]
        AltaicLanguages,

        [Language(LongName = "Apache languages", ThreeLetterCode = "apa")]
        ApacheLanguages,

        [Language(LongName = "Artificial languages", ThreeLetterCode = "art")]
        ArtificialLanguages,

        [Language(LongName = "Athapascan languages", ThreeLetterCode = "ath")]
        AthapascanLanguages,

        [Language(LongName = "Australian languages", ThreeLetterCode = "aus")]
        AustralianLanguages,

        [Language(LongName = "Austronesian languages", ThreeLetterCode = "map")]
        AustronesianLanguages,

        [Language(LongName = "Baltic languages", ThreeLetterCode = "bat")]
        BalticLanguages,

        [Language(LongName = "Bamileke languages", ThreeLetterCode = "bai")]
        BamilekeLanguages,

        [Language(LongName = "Banda languages", ThreeLetterCode = "bad")]
        BandaLanguages,

        [Language(LongName = "Bantu languages", ThreeLetterCode = "bnt")]
        BantuLanguages,

        [Language(LongName = "Batak languages", ThreeLetterCode = "btk")]
        BatakLanguages,

        [Language(LongName = "Berber languages", ThreeLetterCode = "ber")]
        BerberLanguages,

        [Language(LongName = "Bihari languages", TwoLetterCode = "bh", ThreeLetterCode = "bih")]
        BihariLanguages,

        [Language(LongName = "Caucasian languages", ThreeLetterCode = "cau")]
        CaucasianLanguages,

        [Language(LongName = "Celtic languages", ThreeLetterCode = "cel")]
        CelticLanguages,

        [Language(LongName = "Central American Indian languages", ThreeLetterCode = "cai")]
        CentralAmericanIndianLanguages,

        [Language(LongName = "Chamic languages", ThreeLetterCode = "cmc")]
        ChamicLanguages,

        [Language(LongName = "Cushitic languages", ThreeLetterCode = "cus")]
        CushiticLanguages,

        [Language(LongName = "Dravidian languages", ThreeLetterCode = "dra")]
        DravidianLanguages,

        [Language(LongName = "Finno-Ugrian languages", ThreeLetterCode = "fiu")]
        FinnoUgrianLanguages,

        [Language(LongName = "Germanic languages", ThreeLetterCode = "gem")]
        GermanicLanguages,

        [Language(LongName = "Himachali languages; Western Pahari languages", ThreeLetterCode = "him")]
        HimachaliLanguages,

        [Language(LongName = "Ijo languages", ThreeLetterCode = "ijo")]
        IjoLanguages,

        [Language(LongName = "Indic languages", ThreeLetterCode = "inc")]
        IndicLanguages,

        [Language(LongName = "Indo-European languages", ThreeLetterCode = "ine")]
        IndoEuropeanLanguages,

        [Language(LongName = "Iranian languages", ThreeLetterCode = "ira")]
        IranianLanguages,

        [Language(LongName = "Iroquoian languages", ThreeLetterCode = "iro")]
        IroquoianLanguages,

        [Language(LongName = "Karen languages", ThreeLetterCode = "kar")]
        KarenLanguages,

        [Language(LongName = "Khoisan languages", ThreeLetterCode = "khi")]
        KhoisanLanguages,

        [Language(LongName = "Kru languages", ThreeLetterCode = "kro")]
        KruLanguages,

        [Language(LongName = "Land Dayak languages", ThreeLetterCode = "day")]
        LandDayakLanguages,

        [Language(LongName = "Manobo languages", ThreeLetterCode = "mno")]
        ManoboLanguages,

        [Language(LongName = "Mayan languages", ThreeLetterCode = "myn")]
        MayanLanguages,

        [Language(LongName = "Mon-Khmer languages", ThreeLetterCode = "mkh")]
        MonKhmerLanguages,

        // Commented out to avoid confusion
        //[Language(LongName = "Multiple languages", ThreeLetterCode = "mul")]
        //MultipleLanguages,

        [Language(LongName = "Munda languages", ThreeLetterCode = "mun")]
        MundaLanguages,

        [Language(LongName = "Nahuatl languages", ThreeLetterCode = "nah")]
        NahuatlLanguages,

        [Language(LongName = "Niger-Kordofanian languages", ThreeLetterCode = "nic")]
        NigerKordofanianLanguages,

        [Language(LongName = "Nilo-Saharan languages", ThreeLetterCode = "ssa")]
        NiloSaharanLanguages,

        [Language(LongName = "North American Indian languages", ThreeLetterCode = "nai")]
        NorthAmericanIndianLanguages,

        [Language(LongName = "Nubian languages", ThreeLetterCode = "nub")]
        NubianLanguages,

        [Language(LongName = "Otomian languages", ThreeLetterCode = "oto")]
        OtomianLanguages,

        [Language(LongName = "Papuan languages", ThreeLetterCode = "paa")]
        PapuanLanguages,

        [Language(LongName = "Philippine languages", ThreeLetterCode = "phi")]
        PhilippineLanguages,

        [Language(LongName = "Prakrit languages", ThreeLetterCode = "pra")]
        PrakritLanguages,

        [Language(LongName = "Romance languages", ThreeLetterCode = "roa")]
        RomanceLanguages,

        [Language(LongName = "Salishan languages", ThreeLetterCode = "sal")]
        SalishanLanguages,

        [Language(LongName = "Sami languages", ThreeLetterCode = "smi")]
        SamiLanguages,

        [Language(LongName = "Semitic languages", ThreeLetterCode = "sem")]
        SemiticLanguages,

        [Language(LongName = "Sino-Tibetan languages", ThreeLetterCode = "sit")]
        SinoTibetanLanguages,

        [Language(LongName = "Siouan languages", ThreeLetterCode = "sio")]
        SiouanLanguages,

        [Language(LongName = "Slavic languages", ThreeLetterCode = "sla")]
        SlavicLanguages,

        [Language(LongName = "Songhai languages", ThreeLetterCode = "son")]
        SonghaiLanguages,

        [Language(LongName = "Sorbian languages", ThreeLetterCode = "wen")]
        SorbianLanguages,

        [Language(LongName = "South American Indian languages", ThreeLetterCode = "sai")]
        SouthAmericanIndianLanguages,

        [Language(LongName = "Tai languages", ThreeLetterCode = "tai")]
        TaiLanguages,

        [Language(LongName = "Tupi languages", ThreeLetterCode = "tup")]
        TupiLanguages,

        [Language(LongName = "Uncoded languages", ThreeLetterCode = "mis")]
        UncodedLanguages,

        [Language(LongName = "Wakashan languages", ThreeLetterCode = "wak")]
        WakashanLanguages,

        [Language(LongName = "Yupik languages", ThreeLetterCode = "ypk")]
        YupikLanguages,

        [Language(LongName = "Zande languages", ThreeLetterCode = "znd")]
        ZandeLanguages,
        */

        #endregion
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

        [HumanReadable(LongName = "KEYS", ShortName = "keys")]
        Keys,

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
    /// List of all known systems not bound to specific site limitations
    /// </summary>
    /// TODO: Remove marker items
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

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Apple/Bandai Pippin", ShortName = "PIPPIN", HasCues = true, HasDat = true)]
        AppleBandaiPippin,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Atari Jaguar CD Interactive Multimedia System", ShortName = "AJCD", HasCues = true, HasDat = true)]
        AtariJaguarCDInteractiveMultimediaSystem,

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

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro iON Educational Gaming System", ShortName = "ION", HasCues = true, HasDat = true)]
        HasbroiONEducationalGamingSystem,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow", ShortName = "HVN", HasCues = true, HasDat = true)]
        HasbroVideoNow,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow Color", ShortName = "HVNC", HasCues = true, HasDat = true)]
        HasbroVideoNowColor,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow Jr.", ShortName = "HVNJR", HasCues = true, HasDat = true)]
        HasbroVideoNowJr,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Hasbro VideoNow XP", ShortName = "HVNXP", HasCues = true, HasDat = true)]
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

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Microsoft Xbox One", ShortName = "XBOXONE", HasDat = true)]
        MicrosoftXboxOne,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Microsoft Xbox Series X", ShortName = "XBOXSX", HasDat = true)]
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

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Nintendo Wii U", ShortName = "WIIU", HasDat = true, HasKeys = true)]
        NintendoWiiU,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "3DO Interactive Multiplayer", ShortName = "3DO", HasCues = true, HasDat = true)]
        Panasonic3DOInteractiveMultiplayer,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Philips CD-i", ShortName = "CDI", HasCues = true, HasDat = true)]
        PhilipsCDi,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Playmaji Polymega", ShortName = "POLYMEGA", HasDat = true)]
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

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation 4", ShortName = "PS4", HasDat = true)]
        SonyPlayStation4,

        [System(Category = SystemCategory.DiscBasedConsole, LongName = "Sony PlayStation 5", ShortName = "PS5", HasDat = true)]
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

        [System(Category = SystemCategory.Computer, LongName = "Acorn Archimedes & Risc PC", ShortName = "ARCH", HasCues = true, HasDat = true)]
        AcornArchimedesAndRiscPC,

        [System(Category = SystemCategory.Computer, LongName = "Apple Macintosh", ShortName = "MAC", HasCues = true, HasDat = true, HasSbi = true)]
        AppleMacintosh,

        [System(Category = SystemCategory.Computer, LongName = "Atari ST series", ShortName = "ATARIST", HasCues = true, HasDat = true)]
        AtariSTSeries,

        // Short name currently undefined
        [System(Category = SystemCategory.Computer, Available = false, LongName = "Commodore 64", ShortName = "C64")]
        Commodore64,

        [System(Category = SystemCategory.Computer, LongName = "Commodore Amiga CD", ShortName = "ACD", HasCues = true, HasDat = true)]
        CommodoreAmigaCD,

        [System(Category = SystemCategory.Computer, LongName = "Fujitsu FM Towns series", ShortName = "FMT", HasCues = true, HasDat = true)]
        FujitsuFMTownsSeries,

        [System(Category = SystemCategory.Computer, LongName = "IBM PC compatible", ShortName = "PC", HasCues = true, HasDat = true, HasSbi = true)]
        IBMPCcompatible,

        // Short name currently undefined
        [System(Category = SystemCategory.Computer, Available = false, LongName = "Microsoft MSX", ShortName = "MSX")]
        MicrosoftMSX,

        [System(Category = SystemCategory.Computer, LongName = "NEC PC-88 series", ShortName = "PC-88", HasCues = true, HasDat = true)]
        NECPC88Series,

        [System(Category = SystemCategory.Computer, LongName = "NEC PC-98 series", ShortName = "PC-98", HasCues = true, HasDat = true)]
        NECPC98Series,

        [System(Category = SystemCategory.Computer, LongName = "SGI Indigo series", ShortName = "INDIGO", HasCues = true, HasDat = true)]
        SGIIndigoSeries,

        [System(Category = SystemCategory.Computer, LongName = "Sharp X68000", ShortName = "X68K", HasCues = true, HasDat = true)]
        SharpX68000,

        // Short name currently undefined
        [System(Category = SystemCategory.Computer, Available = false, LongName = "Sinclair ZX Spectrum", ShortName = "ZX-SPECTRUM")]
        SinclairZXSpectrum,

        [System(Category = SystemCategory.Computer, LongName = "Sun Microsystems Ultra", ShortName = "ULTRA", HasCues = true, HasDat = true)]
        SunMicrosystemsUltra,

        // End of computer section delimiter
        MarkerComputerEnd,

        #endregion

        #region Arcade

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

        [System(Category = SystemCategory.Arcade, LongName = "Capcom Play System III", ShortName = "CPS3", HasCues = true, HasDat = true)]
        CapcomPlaySystemIII,

        // Short name currently undefined
        [System(Category = SystemCategory.Arcade, Available = false, LongName = "C.D. Express Cubo CD32", ShortName = "CUBO")]
        CDExpressCuboCD32,

        [System(Category = SystemCategory.Arcade, LongName = "Funworld Photo Play", ShortName = "FPP", HasCues = true, HasDat = true)]
        FunworldPhotoPlay,

        [System(Category = SystemCategory.Arcade, LongName = "FuRyu & Omron Purikura", ShortName = "FPURI", HasDat = true)]
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

        // Short name currently undefined
        [System(Category = SystemCategory.Arcade, Available = false, LongName = "JVL iTouch", ShortName = "ITOUCH")]
        JVLiTouch,

        [System(Category = SystemCategory.Arcade, LongName = "Konami e-Amusement", ShortName = "KEA", HasCues = true, HasDat = true)]
        KonamieAmusement,

        [System(Category = SystemCategory.Arcade, LongName = "Konami FireBeat", ShortName = "KFB", HasCues = true, HasDat = true)]
        KonamiFireBeat,

        [System(Category = SystemCategory.Arcade, LongName = "Konami M2", ShortName = "KM2", HasCues = true, HasDat = true)]
        KonamiM2,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Konami Python")]
        KonamiPython,

        [System(Category = SystemCategory.Arcade, LongName = "Konami Python 2", ShortName = "KP2", HasDat = true)]
        KonamiPython2,

        [System(Category = SystemCategory.Arcade, LongName = "Konami System 573", ShortName = "KS573", HasCues = true, HasDat = true)]
        KonamiSystem573,

        [System(Category = SystemCategory.Arcade, LongName = "Konami System GV", ShortName = "KSGV", HasCues = true, HasDat = true)]
        KonamiSystemGV,

        [System(Category = SystemCategory.Arcade, LongName = "Konami Twinkle", ShortName = "KT")]
        KonamiTwinkle,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Konami PC-based Systems")]
        KonamiVarious,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Merit Industries Boardwalk")]
        MeritIndustriesBoardwalk,

        // TODO: Combine all MegaTouch systems?
        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Merit Industries MegaTouch Force")]
        MeritIndustriesMegaTouchForce,

        [System(Category = SystemCategory.Arcade, LongName = "Merit Industries MegaTouch ION", ShortName = "MION", HasDat = true)]
        MeritIndustriesMegaTouchION,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Merit Industries MegaTouch Maxx")]
        MeritIndustriesMegaTouchMaxx,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Merit Industries MegaTouch XL")]
        MeritIndustriesMegaTouchXL,

        [System(Category = SystemCategory.Arcade, LongName = "Namco Purikura", ShortName = "NPURI", HasDat = true)]
        NamcoPurikura,

        [System(Category = SystemCategory.Arcade, LongName = "Namco · Sega · Nintendo Triforce", ShortName = "TRF", HasCues = true, HasDat = true)]
        NamcoSegaNintendoTriforce,

        [System(Category = SystemCategory.Arcade, LongName = "Namco System 12", ShortName = "NS12")]
        NamcoSystem12,

        [System(Category = SystemCategory.Arcade, LongName = "Namco System 22", ShortName = "NS22", HasCues = true, HasDat = true)]
        NamcoSystem22,

        [System(Category = SystemCategory.Arcade, LongName = "Namco System 246", ShortName = "NS246", HasCues = true, HasDat = true)]
        NamcoSystem246,

        [System(Category = SystemCategory.Arcade, LongName = "Namco System 256", ShortName = "NS256", HasCues = true, HasDat = true)]
        NamcoSystem256,

        // Short name currently undefined
        [System(Category = SystemCategory.Arcade, Available = false, LongName = "New Jatre CD-i", ShortName = "NJ-CDI")]
        NewJatreCDi,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Nichibutsu High Rate System")]
        NichibutsuHighRateSystem,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Nichibutsu Super CD")]
        NichibutsuSuperCD,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Nichibutsu X-Rate System")]
        NichibutsuXRateSystem,

        [System(Category = SystemCategory.Arcade, LongName = "Panasonic M2", ShortName = "M2", HasCues = true, HasDat = true)]
        PanasonicM2,

        // TODO: Investigate whether the various PC-based systems listed would wrap into this instead
        // If they wrap in, maybe make a note about what they used to be
        [System(Category = SystemCategory.Arcade, LongName = "PC-Based Arcade", ShortName = "PCARCADE", HasCues = true, HasDat = true)]
        PCBasedArcade,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "PhotoPlay PC-based Systems")]
        PhotoPlayVarious,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Raw Thrills PC-based Systems")]
        RawThrillsVarious,

        [System(Category = SystemCategory.Arcade, LongName = "Sega ALLS", ShortName = "ALLS", HasDat = true)]
        SegaALLS,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Chihiro", ShortName = "CHIHIRO", HasCues = true, HasDat = true)]
        SegaChihiro,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Chihiro Satellite Terminal PC", ShortName = "CHIHIROPC", HasCues = true, HasDat = true)]
        SegaChihiroSatelliteTerminalPC,

        [System(Category = SystemCategory.Arcade, Available = false, LongName = "Sega Europa-R")]
        SegaEuropaR,

        // TODO: Split if/when Yellow, Blue, Red, Red EX, and Silver are defined
        [System(Category = SystemCategory.Arcade, LongName = "Sega Lindbergh", ShortName = "LINDBERGH", HasDat = true)]
        SegaLindbergh,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Lindbergh Satellite Terminal PC", ShortName = "LINDPC", HasDat = true)]
        SegaLindberghSatelliteTerminalPC,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Naomi", ShortName = "NAOMI", HasCues = true, HasDat = true)]
        SegaNaomi,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Naomi 2", ShortName = "NAOMI2", HasCues = true, HasDat = true)]
        SegaNaomi2,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Naomi Satellite Terminal PC", ShortName = "NAOMIPC", HasCues = true, HasDat = true)]
        SegaNaomiSatelliteTerminalPC,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Nu", ShortName = "NU", HasDat = true)]
        SegaNu,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Nu 1.1", ShortName = "NU11", HasDat = true)]
        SegaNu11,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Nu 2", ShortName = "NU2", HasDat = true)]
        SegaNu2,

        [System(Category = SystemCategory.Arcade, LongName = "Sega Nu SX", ShortName = "NUSX", HasDat = true)]
        SegaNuSX,

        [System(Category = SystemCategory.Arcade, LongName = "Sega RingEdge", ShortName = "SRE", HasDat = true)]
        SegaRingEdge,

        [System(Category = SystemCategory.Arcade, LongName = "Sega RingEdge 2", ShortName = "SRE2", HasDat = true)]
        SegaRingEdge2,

        [System(Category = SystemCategory.Arcade, LongName = "Sega RingWide", ShortName = "SRW", HasDat = true)]
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

        [System(Category = SystemCategory.Other, LongName = "Audio CD", ShortName = "AUDIO-CD", HasCues = true, HasDat = true)]
        AudioCD,

        [System(Category = SystemCategory.Other, LongName = "BD-Video", ShortName = "BD-VIDEO", HasDat = true)]
        BDVideo,

        [System(Category = SystemCategory.Other, LongName = "Datel PlayStation Cheat Device Updates", ShortName = "PSXGS", HasCues = true, HasDat = true)]
        DatelPlayStationCheatDeviceUpdates,

        [System(Category = SystemCategory.Other, Available = false, LongName = "DVD-Audio")]
        DVDAudio,

        [System(Category = SystemCategory.Other, LongName = "DVD-Video", ShortName = "DVD-VIDEO", HasDat = true)]
        DVDVideo,

        [System(Category = SystemCategory.Other, LongName = "Enhanced CD", ShortName = "ENHANCED-CD", HasCues = true, HasDat = true)]
        EnhancedCD,

        [System(Category = SystemCategory.Other, LongName = "HD DVD-Video", ShortName = "HDDVD-VIDEO", HasDat = true)]
        HDDVDVideo,

        [System(Category = SystemCategory.Other, LongName = "Navisoft Naviken", ShortName = "NAVI", HasCues = true, HasDat = true)]
        NavisoftNaviken,

        [System(Category = SystemCategory.Other, LongName = "Palm OS", ShortName = "PALM", HasCues = true, HasDat = true)]
        PalmOS,

        [System(Category = SystemCategory.Other, LongName = "Photo CD", ShortName = "PHOTO-CD", HasCues = true, HasDat = true)]
        PhotoCD,

        [System(Category = SystemCategory.Other, LongName = "Microsoft Pocket PC", ShortName = "PPC", HasCues = true, HasDat = true)]
        MicrosoftPocketPC,

        [System(Category = SystemCategory.Other, LongName = "MP3 Audio Disc", ShortName = "MP3", HasCues = true, HasDat = true)]
        MP3AudioDisc,

        [System(Category = SystemCategory.Other, LongName = "Psion", ShortName = "PSION", HasCues = true, HasDat = true)]
        Psion,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Rainbow Disc")]
        RainbowDisc,

        [System(Category = SystemCategory.Other, LongName = "Sega Prologue 21 Multimedia Karaoke System", ShortName = "SP21", HasCues = true, HasDat = true)]
        SegaPrologue21MultimediaKaraokeSystem,

        [System(Category = SystemCategory.Other, LongName = "Sharp Zaurus", ShortName = "ZAURUS", HasCues = true, HasDat = true)]
        SharpZaurus,

        [System(Category = SystemCategory.Other, LongName = "Sony Electronic Book", ShortName = "SEB", HasCues = true, HasDat = true)]
        SonyElectronicBook,

        [System(Category = SystemCategory.Other, Available = false, LongName = "Super Audio CD")]
        SuperAudioCD,

        [System(Category = SystemCategory.Other, LongName = "Tao iKTV", ShortName = "IKTV")]
        TaoiKTV,

        [System(Category = SystemCategory.Other, LongName = "Tomy Kiss-Site", ShortName = "KSITE", HasCues = true, HasDat = true)]
        TomyKissSite,

        [System(Category = SystemCategory.Other, LongName = "Video CD", ShortName = "VCD", HasCues = true, HasDat = true)]
        VideoCD,

        // End of other section delimiter
        MarkerOtherEnd,

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
