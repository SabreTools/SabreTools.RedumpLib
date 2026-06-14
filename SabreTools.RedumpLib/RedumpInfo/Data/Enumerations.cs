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
    /// List of all disc langauges
    /// </summary>
    /// <remarks>https://www.loc.gov/standards/iso639-2/php/code_list.php</remarks>
    public enum Language
    {
        #region A

        [Language(LongName = "Abkhazian", TwoLetterCode = "ab", ThreeLetterCode = "abk")]
        Abkhazian,

        [Language(LongName = "Achinese", ThreeLetterCode = "ace")]
        Achinese,

        [Language(LongName = "Acoli", ThreeLetterCode = "ach")]
        Acoli,

        [Language(LongName = "Adangme", ThreeLetterCode = "ada")]
        Adangme,

        // Adyghe; Adygei
        [Language(LongName = "Adyghe", ThreeLetterCode = "ady")]
        Adyghe,

        [Language(LongName = "Afar", TwoLetterCode = "aa", ThreeLetterCode = "aar")]
        Afar,

        [Language(LongName = "Afrihili", ThreeLetterCode = "afh")]
        Afrihili,

        [Language(LongName = "Afrikaans", TwoLetterCode = "af", ThreeLetterCode = "afr")]
        Afrikaans,

        [Language(LongName = "Ainu", ThreeLetterCode = "ain")]
        Ainu,

        [Language(LongName = "Akan", TwoLetterCode = "ak", ThreeLetterCode = "aka")]
        Akan,

        [Language(LongName = "Akkadian", ThreeLetterCode = "akk")]
        Akkadian,

        [Language(LongName = "Albanian", TwoLetterCode = "sq", ThreeLetterCode = "alb", ThreeLetterCodeAlt = "sqi")]
        Albanian,

        [Language(LongName = "Aleut", ThreeLetterCode = "ale")]
        Aleut,

        [Language(LongName = "Amharic", TwoLetterCode = "am", ThreeLetterCode = "amh")]
        Amharic,

        [Language(LongName = "Angika", ThreeLetterCode = "anp")]
        Angika,

        [Language(LongName = "Arabic", TwoLetterCode = "ar", ThreeLetterCode = "ara")]
        Arabic,

        [Language(LongName = "Aragonese", TwoLetterCode = "an", ThreeLetterCode = "arg")]
        Aragonese,

        // Official Aramaic (700-300 BCE); Imperial Aramaic (700-300 BCE)
        [Language(LongName = "Aramaic", ThreeLetterCode = "arc")]
        Aramaic,

        [Language(LongName = "Armenian", TwoLetterCode = "hy", ThreeLetterCode = "arm", ThreeLetterCodeAlt = "hye")]
        Armenian,

        [Language(LongName = "Arapaho", ThreeLetterCode = "arp")]
        Arapaho,

        // Aromanian; Arumanian; Macedo-Romanian
        [Language(LongName = "Aromanian", ThreeLetterCode = "rup")]
        Aromanian,

        [Language(LongName = "Arawak", ThreeLetterCode = "arw")]
        Arawak,

        [Language(LongName = "Assamese", TwoLetterCode = "as", ThreeLetterCode = "asm")]
        Assamese,

        // Asturian; Bable; Leonese; Asturleonese
        [Language(LongName = "Asturian", ThreeLetterCode = "ast")]
        Asturian,

        [Language(LongName = "Athapascan", ThreeLetterCode = "den")]
        Athapascan,

        [Language(LongName = "Avaric", TwoLetterCode = "av", ThreeLetterCode = "ava")]
        Avaric,

        [Language(LongName = "Avestan", TwoLetterCode = "ae", ThreeLetterCode = "ave")]
        Avestan,

        [Language(LongName = "Awadhi", ThreeLetterCode = "awa")]
        Awadhi,

        [Language(LongName = "Aymara", TwoLetterCode = "ay", ThreeLetterCode = "aym")]
        Aymara,

        [Language(LongName = "Azerbaijani", TwoLetterCode = "az", ThreeLetterCode = "aze")]
        Azerbaijani,

        #endregion

        #region B

        [Language(LongName = "Balinese", ThreeLetterCode = "ban")]
        Balinese,

        [Language(LongName = "Baluchi", ThreeLetterCode = "bal")]
        Baluchi,

        [Language(LongName = "Bambara", TwoLetterCode = "bm", ThreeLetterCode = "bam")]
        Bambara,

        [Language(LongName = "Basa", ThreeLetterCode = "bas")]
        Basa,

        [Language(LongName = "Bashkir", TwoLetterCode = "ba", ThreeLetterCode = "bak")]
        Bashkir,

        [Language(LongName = "Basque", TwoLetterCode = "eu", ThreeLetterCode = "baq", ThreeLetterCodeAlt = "eus")]
        Basque,

        // Beja; Bedawiyet
        [Language(LongName = "Beja", ThreeLetterCode = "bej")]
        Beja,

        [Language(LongName = "Belarusian", TwoLetterCode = "be", ThreeLetterCode = "bel")]
        Belarusian,

        [Language(LongName = "Bemba", ThreeLetterCode = "bem")]
        Bemba,

        [Language(LongName = "Bengali", TwoLetterCode = "bn", ThreeLetterCode = "ben")]
        Bengali,

        [Language(LongName = "Bhojpuri", ThreeLetterCode = "bho")]
        Bhojpuri,

        [Language(LongName = "Bikol", ThreeLetterCode = "bik")]
        Bikol,

        [Language(LongName = "Bini; Edo", ThreeLetterCode = "bin")]
        Bini,

        [Language(LongName = "Bislama", TwoLetterCode = "bi", ThreeLetterCode = "bis")]
        Bislama,

        // Blin; Bilin
        [Language(LongName = "Blin", ThreeLetterCode = "byn")]
        Blin,

        // Blissymbols; Blissymbolics; Bliss
        [Language(LongName = "Blissymbols", ThreeLetterCode = "zbl")]
        Blissymbols,

        [Language(LongName = "Bosnian", TwoLetterCode = "bs", ThreeLetterCode = "bos")]
        Bosnian,

        [Language(LongName = "Braj", ThreeLetterCode = "bra")]
        Braj,

        [Language(LongName = "Breton", TwoLetterCode = "br", ThreeLetterCode = "bre")]
        Breton,

        [Language(LongName = "Buginese", ThreeLetterCode = "bug")]
        Buginese,

        [Language(LongName = "Bulgarian", TwoLetterCode = "bg", ThreeLetterCode = "bul")]
        Bulgarian,

        [Language(LongName = "Buriat", ThreeLetterCode = "bua")]
        Buriat,

        [Language(LongName = "Burmese", TwoLetterCode = "my", ThreeLetterCode = "bur", ThreeLetterCodeAlt = "mya")]
        Burmese,

        #endregion

        #region C

        [Language(LongName = "Caddo", ThreeLetterCode = "cad")]
        Caddo,

        // Catalan; Valencian
        [Language(LongName = "Catalan", TwoLetterCode = "ca", ThreeLetterCode = "cat")]
        Catalan,

        [Language(LongName = "Cebuano", ThreeLetterCode = "ceb")]
        Cebuano,

        [Language(LongName = "Central Khmer", TwoLetterCode = "km", ThreeLetterCode = "khm")]
        CentralKhmer,

        [Language(LongName = "Chagatai", ThreeLetterCode = "chg")]
        Chagatai,

        [Language(LongName = "Chamorro", TwoLetterCode = "ch", ThreeLetterCode = "cha")]
        Chamorro,

        [Language(LongName = "Chechen", TwoLetterCode = "ce", ThreeLetterCode = "che")]
        Chechen,

        [Language(LongName = "Cherokee", ThreeLetterCode = "chr")]
        Cherokee,

        [Language(LongName = "Cheyenne", ThreeLetterCode = "chy")]
        Cheyenne,

        [Language(LongName = "Chibcha", ThreeLetterCode = "chb")]
        Chibcha,

        // Chichewa; Chewa; Nyanja
        [Language(LongName = "Chichewa", TwoLetterCode = "ny", ThreeLetterCode = "nya")]
        Chichewa,

        [Language(LongName = "Chinese", TwoLetterCode = "zh", ThreeLetterCode = "chi", ThreeLetterCodeAlt = "zho")]
        Chinese,

        [Language(LongName = "Chinook jargon", ThreeLetterCode = "chn")]
        ChinookJargon,

        // Chipewyan; Dene Suline
        [Language(LongName = "Chipewyan", ThreeLetterCode = "chp")]
        Chipewyan,

        [Language(LongName = "Choctaw", ThreeLetterCode = "cho")]
        Choctaw,

        // Church Slavic; Old Slavonic; Church Slavonic; Old Bulgarian; Old Church Slavonic
        [Language(LongName = "Church Slavic", TwoLetterCode = "cu", ThreeLetterCode = "chu")]
        ChurchSlavic,

        [Language(LongName = "Chuukese", ThreeLetterCode = "chk")]
        Chuukese,

        [Language(LongName = "Chuvash", TwoLetterCode = "cv", ThreeLetterCode = "chv")]
        Chuvash,

        // Classical Newari; Old Newari; Classical Nepal Bhasa
        [Language(LongName = "Classical Newari", ThreeLetterCode = "nwc")]
        ClassicalNewari,

        [Language(LongName = "Coptic", ThreeLetterCode = "cop")]
        Coptic,

        [Language(LongName = "Cornish", TwoLetterCode = "kw", ThreeLetterCode = "cor")]
        Cornish,

        [Language(LongName = "Corsican", TwoLetterCode = "co", ThreeLetterCode = "cos")]
        Corsican,

        [Language(LongName = "Cree", TwoLetterCode = "cr", ThreeLetterCode = "cre")]
        Cree,

        [Language(LongName = "Creek", ThreeLetterCode = "mus")]
        Creek,

        [Language(LongName = "Creoles and pidgins", ThreeLetterCode = "crp")]
        CreolesAndPidgins,

        [Language(LongName = "Creoles and pidgins (English-based)", ThreeLetterCode = "cpe")]
        EnglishCreole,

        [Language(LongName = "Creoles and pidgins (French-based)", ThreeLetterCode = "cpf")]
        FrenchCreole,

        [Language(LongName = "Creoles and pidgins (Portuguese-based)", ThreeLetterCode = "cpp")]
        PortugueseCreole,

        // Crimean Tatar; Crimean Turkish
        [Language(LongName = "Crimean Tatar", ThreeLetterCode = "crh")]
        CrimeanTatar,

        [Language(LongName = "Croatian", TwoLetterCode = "hr", ThreeLetterCode = "hrv")]
        Croatian,

        [Language(LongName = "Czech", TwoLetterCode = "cs", ThreeLetterCode = "cze", ThreeLetterCodeAlt = "ces")]
        Czech,

        #endregion

        #region D

        [Language(LongName = "Dakota", ThreeLetterCode = "dak")]
        Dakota,

        [Language(LongName = "Danish", TwoLetterCode = "da", ThreeLetterCode = "dan")]
        Danish,

        [Language(LongName = "Dargwa", ThreeLetterCode = "dar")]
        Dargwa,

        [Language(LongName = "Delaware", ThreeLetterCode = "del")]
        Delaware,

        [Language(LongName = "Dinka", ThreeLetterCode = "din")]
        Dinka,

        // Divehi; Dhivehi; Maldivian
        [Language(LongName = "Divehi", TwoLetterCode = "dv", ThreeLetterCode = "div")]
        Divehi,

        [Language(LongName = "Dogrib", ThreeLetterCode = "dgr")]
        Dogrib,

        [Language(LongName = "Dogri", ThreeLetterCode = "doi")]
        Dogri,

        [Language(LongName = "Duala", ThreeLetterCode = "dua")]
        Duala,

        // Dutch; Flemish
        [Language(LongName = "Dutch", TwoLetterCode = "nl", ThreeLetterCode = "dut", ThreeLetterCodeAlt = "nld")]
        Dutch,

        [Language(LongName = "Dutch, Middle (ca.1050-1350)", ThreeLetterCode = "dum")]
        MiddleDutch,

        [Language(LongName = "Dyula", ThreeLetterCode = "dyu")]
        Dyula,

        [Language(LongName = "Dzongkha", TwoLetterCode = "dz", ThreeLetterCode = "dzo")]
        Dzongkha,

        #endregion

        #region E

        [Language(LongName = "Eastern Frisian", ThreeLetterCode = "frs")]
        EasternFrisian,

        [Language(LongName = "Efik", ThreeLetterCode = "efi")]
        Efik,

        [Language(LongName = "Egyptian (Ancient)", ThreeLetterCode = "egy")]
        AncientEgyptian,

        [Language(LongName = "Ekajuk", ThreeLetterCode = "eka")]
        Ekajuk,

        [Language(LongName = "Elamite", ThreeLetterCode = "elx")]
        Elamite,

        [Language(LongName = "English", TwoLetterCode = "en", ThreeLetterCode = "eng")]
        English,

        [Language(LongName = "English, Old (ca.450-1100)", ThreeLetterCode = "ang")]
        OldEnglish,

        [Language(LongName = "English, Middle (1100-1500)", ThreeLetterCode = "enm")]
        MiddleEnglish,

        [Language(LongName = "Erzya", ThreeLetterCode = "myv")]
        Erzya,

        [Language(LongName = "Esperanto", TwoLetterCode = "eo", ThreeLetterCode = "epo")]
        Esperanto,

        [Language(LongName = "Estonian", TwoLetterCode = "et", ThreeLetterCode = "est")]
        Estonian,

        [Language(LongName = "Ewe", TwoLetterCode = "ee", ThreeLetterCode = "ewe")]
        Ewe,

        [Language(LongName = "Ewondo", ThreeLetterCode = "ewo")]
        Ewondo,

        #endregion

        #region F

        [Language(LongName = "Fang", ThreeLetterCode = "fan")]
        Fang,

        [Language(LongName = "Faroese", TwoLetterCode = "fo", ThreeLetterCode = "fao")]
        Faroese,

        [Language(LongName = "Fanti", ThreeLetterCode = "fat")]
        Fanti,

        [Language(LongName = "Fijian", TwoLetterCode = "fj", ThreeLetterCode = "fij")]
        Fijian,

        // Filipino; Pilipino
        [Language(LongName = "Filipino", ThreeLetterCode = "fil")]
        Filipino,

        [Language(LongName = "Finnish", TwoLetterCode = "fi", ThreeLetterCode = "fin")]
        Finnish,

        [Language(LongName = "Fon", ThreeLetterCode = "fon")]
        Fon,

        [Language(LongName = "French", TwoLetterCode = "fr", ThreeLetterCode = "fre", ThreeLetterCodeAlt = "fra")]
        French,

        [Language(LongName = "French, Middle (ca.1400-1600)", ThreeLetterCode = "frm")]
        MiddleFrench,

        [Language(LongName = "French, Old (842-ca.1400)", ThreeLetterCode = "fro")]
        OldFrench,

        [Language(LongName = "Friulian", ThreeLetterCode = "fur")]
        Friulian,

        [Language(LongName = "Fulah", TwoLetterCode = "ff", ThreeLetterCode = "ful")]
        Fulah,

        #endregion

        #region G

        [Language(LongName = "Ga", ThreeLetterCode = "gaa")]
        Ga,

        // Gaelic; Scottish Gaelic
        [Language(LongName = "Gaelic", TwoLetterCode = "gd", ThreeLetterCode = "gla")]
        Gaelic,

        [Language(LongName = "Galibi Carib", ThreeLetterCode = "car")]
        GalibiCarib,

        [Language(LongName = "Galician", TwoLetterCode = "gl", ThreeLetterCode = "glg")]
        Galician,

        [Language(LongName = "Ganda", TwoLetterCode = "lg", ThreeLetterCode = "lug")]
        Ganda,

        [Language(LongName = "Gayo", ThreeLetterCode = "gay")]
        Gayo,

        [Language(LongName = "Gbaya", ThreeLetterCode = "gba")]
        Gbaya,

        [Language(LongName = "Geez", ThreeLetterCode = "gez")]
        Geez,

        [Language(LongName = "Georgian", TwoLetterCode = "ka", ThreeLetterCode = "geo", ThreeLetterCodeAlt = "kat")]
        Georgian,

        [Language(LongName = "German", TwoLetterCode = "de", ThreeLetterCode = "ger", ThreeLetterCodeAlt = "deu")]
        German,

        [Language(LongName = "German, Middle High (ca.1050-1500)", ThreeLetterCode = "gmh")]
        MiddleHighGerman,

        [Language(LongName = "German, Old High (ca.750-1050)", ThreeLetterCode = "goh")]
        OldHighGerman,

        [Language(LongName = "Gilbertese", ThreeLetterCode = "gil")]
        Gilbertese,

        [Language(LongName = "Gondi", ThreeLetterCode = "gon")]
        Gondi,

        [Language(LongName = "Gorontalo", ThreeLetterCode = "gor")]
        Gorontalo,

        [Language(LongName = "Gothic", ThreeLetterCode = "got")]
        Gothic,

        [Language(LongName = "Grebo", ThreeLetterCode = "grb")]
        Grebo,

        [Language(LongName = "Greek", TwoLetterCode = "el", ThreeLetterCode = "gre", ThreeLetterCodeAlt = "eli")]
        Greek,

        [Language(LongName = "Greek, Ancient (to 1453)", ThreeLetterCode = "grc")]
        AncientGreek,

        [Language(LongName = "Guarani", TwoLetterCode = "gn", ThreeLetterCode = "grn")]
        Guarani,

        [Language(LongName = "Gujarati", TwoLetterCode = "gu", ThreeLetterCode = "guj")]
        Gujarati,

        [Language(LongName = "Gwich'in", ThreeLetterCode = "gwi")]
        Gwichin,

        #endregion

        #region H

        [Language(LongName = "Haida", ThreeLetterCode = "hai")]
        Haida,

        // Haitian; Haitian Creole
        [Language(LongName = "Haitian", TwoLetterCode = "ht", ThreeLetterCode = "hat")]
        Haitian,

        [Language(LongName = "Hausa", TwoLetterCode = "ha", ThreeLetterCode = "gau")]
        Hausa,

        [Language(LongName = "Hawaiian", ThreeLetterCode = "haw")]
        Hawaiian,

        [Language(LongName = "Hebrew", TwoLetterCode = "he", ThreeLetterCode = "heb")]
        Hebrew,

        [Language(LongName = "Herero", TwoLetterCode = "hz", ThreeLetterCode = "her")]
        Herero,

        [Language(LongName = "Hiligaynon", ThreeLetterCode = "hil")]
        Hiligaynon,

        [Language(LongName = "Hindi", TwoLetterCode = "hi", ThreeLetterCode = "hin")]
        Hindi,

        [Language(LongName = "Hiri Motu", ThreeLetterCode = "hmo")]
        HiriMotu,

        [Language(LongName = "Hittite", ThreeLetterCode = "hit")]
        Hittite,

        // Hmong; Mong
        [Language(LongName = "Hmong", ThreeLetterCode = "hmn")]
        Hmong,

        [Language(LongName = "Hungarian", TwoLetterCode = "hu", ThreeLetterCode = "hun")]
        Hungarian,

        [Language(LongName = "Hupa", ThreeLetterCode = "hup")]
        Hupa,

        #endregion

        #region I

        [Language(LongName = "Iban", ThreeLetterCode = "iba")]
        Iban,

        [Language(LongName = "Icelandic", TwoLetterCode = "is", ThreeLetterCode = "ice", ThreeLetterCodeAlt = "isl")]
        Icelandic,

        [Language(LongName = "Ido", TwoLetterCode = "io", ThreeLetterCode = "ido")]
        Ido,

        [Language(LongName = "Igbo", TwoLetterCode = "ig", ThreeLetterCode = "ibo")]
        Igbo,

        [Language(LongName = "Iloko", ThreeLetterCode = "ilo")]
        Iloko,

        [Language(LongName = "Inari Sami", ThreeLetterCode = "smn")]
        InariSami,

        [Language(LongName = "Indonesian", TwoLetterCode = "id", ThreeLetterCode = "ind")]
        Indonesian,

        [Language(LongName = "Ingush", ThreeLetterCode = "inh")]
        Ingush,

        // Interlingua (International Auxiliary Language Association)
        [Language(LongName = "Interlingua", TwoLetterCode = "ia", ThreeLetterCode = "ina")]
        Interlingua,

        // Interlingue; Occidental
        [Language(LongName = "Interlingue", TwoLetterCode = "ie", ThreeLetterCode = "ile")]
        Interlingue,

        [Language(LongName = "Inuktitut", TwoLetterCode = "iu", ThreeLetterCode = "iku")]
        Inuktitut,

        [Language(LongName = "Inupiaq", TwoLetterCode = "ik", ThreeLetterCode = "ipk")]
        Inupiaq,

        [Language(LongName = "Irish", TwoLetterCode = "ga", ThreeLetterCode = "gle")]
        Irish,

        [Language(LongName = "Irish, Middle (900-1200)", ThreeLetterCode = "mga")]
        MiddleIrish,

        [Language(LongName = "Irish, Old (to 900)", ThreeLetterCode = "sga")]
        OldIrish,

        [Language(LongName = "Italian", TwoLetterCode = "it", ThreeLetterCode = "ita")]
        Italian,

        #endregion

        #region J

        [Language(LongName = "Japanese", TwoLetterCode = "ja", ThreeLetterCode = "jap")]
        Japanese,

        [Language(LongName = "Javanese", TwoLetterCode = "jv", ThreeLetterCode = "jav")]
        Javanese,

        [Language(LongName = "Judeo-Arabic", ThreeLetterCode = "jrb")]
        JudeoArabic,

        [Language(LongName = "Judeo-Persian", ThreeLetterCode = "jpr")]
        JudeoPersian,

        #endregion

        #region K

        [Language(LongName = "Kabardian", ThreeLetterCode = "kbd")]
        Kabardian,

        [Language(LongName = "Kabyle", ThreeLetterCode = "kab")]
        Kabyle,

        // Kachin; Jingpho
        [Language(LongName = "Kachin", ThreeLetterCode = "kac")]
        Kachin,

        // Kalaallisut; Greenlandic
        [Language(LongName = "Kalaallisut", TwoLetterCode = "kl", ThreeLetterCode = "kal")]
        Kalaallisut,

        // Kalmyk; Oirat
        [Language(LongName = "Kalmyk", ThreeLetterCode = "xal")]
        Kalmyk,

        [Language(LongName = "Kamba", ThreeLetterCode = "kam")]
        Kamba,

        [Language(LongName = "Kannada", TwoLetterCode = "kn", ThreeLetterCode = "kan")]
        Kannada,

        [Language(LongName = "Kanuri", TwoLetterCode = "kr", ThreeLetterCode = "kau")]
        Kanuri,

        [Language(LongName = "Karachay-Balkar", ThreeLetterCode = "krc")]
        KarachayBalkar,

        [Language(LongName = "Kara-Kalpak", ThreeLetterCode = "kaa")]
        KaraKalpak,

        [Language(LongName = "Karelian", ThreeLetterCode = "krl")]
        Karelian,

        [Language(LongName = "Kashmiri", TwoLetterCode = "ks", ThreeLetterCode = "kas")]
        Kashmiri,

        [Language(LongName = "Kashubian", ThreeLetterCode = "csb")]
        Kashubian,

        [Language(LongName = "Kawi", ThreeLetterCode = "kaw")]
        Kawi,

        [Language(LongName = "Kazakh", TwoLetterCode = "kk", ThreeLetterCode = "kaz")]
        Kazakh,

        [Language(LongName = "Khasi", ThreeLetterCode = "kha")]
        Khasi,

        // Khotanese; Sakan
        [Language(LongName = "Khotanese", ThreeLetterCode = "kho")]
        Khotanese,

        // Kikuyu; Gikuyu
        [Language(LongName = "Kikuyu", TwoLetterCode = "ki", ThreeLetterCode = "kik")]
        Kikuyu,

        [Language(LongName = "Kimbundu", ThreeLetterCode = "kmb")]
        Kimbundu,

        [Language(LongName = "Kinyarwanda", TwoLetterCode = "rw", ThreeLetterCode = "kin")]
        Kinyarwanda,

        // Kirghiz; Kyrgyz
        [Language(LongName = "Kirghiz", TwoLetterCode = "ky", ThreeLetterCode = "kir")]
        Kirghiz,

        // Klingon; tlhIngan-Hol
        [Language(LongName = "Klingon", ThreeLetterCode = "tlh")]
        Klingon,

        [Language(LongName = "Komi", TwoLetterCode = "kv", ThreeLetterCode = "kom")]
        Komi,

        [Language(LongName = "Kongo", TwoLetterCode = "kg", ThreeLetterCode = "kon")]
        Kongo,

        [Language(LongName = "Konkani", ThreeLetterCode = "kok")]
        Konkani,

        [Language(LongName = "Korean", TwoLetterCode = "ko", ThreeLetterCode = "kor")]
        Korean,

        [Language(LongName = "Kosraean", ThreeLetterCode = "kos")]
        Kosraean,

        [Language(LongName = "Kpelle", ThreeLetterCode = "kpe")]
        Kpelle,

        // Kuanyama; Kwanyama
        [Language(LongName = "Kuanyama", TwoLetterCode = "kj", ThreeLetterCode = "kua")]
        Kuanyama,

        [Language(LongName = "Kumyk", ThreeLetterCode = "kum")]
        Kumyk,

        [Language(LongName = "Kurdish", TwoLetterCode = "ku", ThreeLetterCode = "kur")]
        Kurdish,

        [Language(LongName = "Kurukh", ThreeLetterCode = "kru")]
        Kurukh,

        [Language(LongName = "Kutenai", ThreeLetterCode = "kut")]
        Kutenai,

        #endregion

        #region L

        [Language(LongName = "Ladino", ThreeLetterCode = "lad")]
        Ladino,

        [Language(LongName = "Lahnda", ThreeLetterCode = "lah")]
        Lahnda,

        [Language(LongName = "Lamba", ThreeLetterCode = "lam")]
        Lamba,

        [Language(LongName = "Lao", TwoLetterCode = "lo", ThreeLetterCode = "lao")]
        Lao,

        [Language(LongName = "Latin", TwoLetterCode = "la", ThreeLetterCode = "lat")]
        Latin,

        [Language(LongName = "Latvian", TwoLetterCode = "lv", ThreeLetterCode = "lav")]
        Latvian,

        [Language(LongName = "Lezghian", ThreeLetterCode = "lez")]
        Lezghian,

        // Limburgan; Limburger; Limburgish
        [Language(LongName = "Limburgan", TwoLetterCode = "li", ThreeLetterCode = "lim")]
        Limburgan,

        [Language(LongName = "Lingala", TwoLetterCode = "ln", ThreeLetterCode = "lin")]
        Lingala,

        [Language(LongName = "Lithuanian", TwoLetterCode = "lt", ThreeLetterCode = "lit")]
        Lithuanian,

        [Language(LongName = "Lojban", ThreeLetterCode = "jbo")]
        Lojban,

        // Low German; Low Saxon
        [Language(LongName = "Low German", ThreeLetterCode = "nds")]
        LowGerman,

        [Language(LongName = "Lower Sorbian", ThreeLetterCode = "dsb")]
        LowerSorbian,

        [Language(LongName = "Lozi", ThreeLetterCode = "loz")]
        Lozi,

        [Language(LongName = "Luba-Katanga", TwoLetterCode = "lu", ThreeLetterCode = "lub")]
        LubaKatanga,

        [Language(LongName = "Luba-Lulua", ThreeLetterCode = "lua")]
        LubaLulua,

        [Language(LongName = "Luiseno", ThreeLetterCode = "lui")]
        Luiseno,

        [Language(LongName = "Lule Sami", ThreeLetterCode = "smj")]
        LuleSami,

        [Language(LongName = "Lunda", ThreeLetterCode = "lun")]
        Lunda,

        [Language(LongName = "Luo (Kenya and Tanzania)", ThreeLetterCode = "luo")]
        Luo,

        [Language(LongName = "Lushai", ThreeLetterCode = "lus")]
        Lushai,

        // Luxembourgish; Letzeburgesch
        [Language(LongName = "Luxembourgish", TwoLetterCode = "lb", ThreeLetterCode = "ltz")]
        Luxembourgish,

        #endregion

        #region M

        [Language(LongName = "Macedonian", TwoLetterCode = "mk", ThreeLetterCode = "mac", ThreeLetterCodeAlt = "mkd")]
        Macedonian,

        [Language(LongName = "Madurese", ThreeLetterCode = "mad")]
        Madurese,

        [Language(LongName = "Magahi", ThreeLetterCode = "mag")]
        Magahi,

        [Language(LongName = "Maithili", ThreeLetterCode = "mai")]
        Maithili,

        [Language(LongName = "Makasar", ThreeLetterCode = "mak")]
        Makasar,

        [Language(LongName = "Malagasy", TwoLetterCode = "mg", ThreeLetterCode = "mlg")]
        Malagasy,

        [Language(LongName = "Malay", TwoLetterCode = "ms", ThreeLetterCode = "may", ThreeLetterCodeAlt = "msa")]
        Malay,

        [Language(LongName = "Malayalam", TwoLetterCode = "ml", ThreeLetterCode = "mal")]
        Malayalam,

        [Language(LongName = "Maltese", TwoLetterCode = "mt", ThreeLetterCode = "mlt")]
        Maltese,

        [Language(LongName = "Manchu", ThreeLetterCode = "mnc")]
        Manchu,

        [Language(LongName = "Mandar", ThreeLetterCode = "mdr")]
        Mandar,

        [Language(LongName = "Mandingo", ThreeLetterCode = "man")]
        Mandingo,

        [Language(LongName = "Manipuri", ThreeLetterCode = "mni")]
        Manipuri,

        [Language(LongName = "Manx", TwoLetterCode = "gv", ThreeLetterCode = "glv")]
        Manx,

        [Language(LongName = "Maori", TwoLetterCode = "mi", ThreeLetterCode = "mao", ThreeLetterCodeAlt = "mri")]
        Maori,

        // Mapudungun; Mapuche
        [Language(LongName = "Mapudungun", ThreeLetterCode = "arn")]
        Mapudungun,

        [Language(LongName = "Marathi", TwoLetterCode = "mr", ThreeLetterCode = "mar")]
        Marathi,

        [Language(LongName = "Mari", ThreeLetterCode = "chm")]
        Mari,

        [Language(LongName = "Marshallese", TwoLetterCode = "mh", ThreeLetterCode = "mah")]
        Marshallese,

        [Language(LongName = "Marwari", ThreeLetterCode = "mwr")]
        Marwari,

        [Language(LongName = "Masai", ThreeLetterCode = "mas")]
        Masai,

        [Language(LongName = "Mende", ThreeLetterCode = "men")]
        Mende,

        // Mi'kmaq; Micmac
        [Language(LongName = "Mi'kmaq", ThreeLetterCode = "mic")]
        Mikmaq,

        [Language(LongName = "Minangkabau", ThreeLetterCode = "min")]
        Minangkabau,

        [Language(LongName = "Mirandese", ThreeLetterCode = "mwl")]
        Mirandese,

        [Language(LongName = "Mohawk", ThreeLetterCode = "moh")]
        Mohawk,

        [Language(LongName = "Moksha", ThreeLetterCode = "mdf")]
        Moksha,

        [Language(LongName = "Mongo", ThreeLetterCode = "lol")]
        Mongo,

        [Language(LongName = "Mongolian", TwoLetterCode = "mn", ThreeLetterCode = "mon")]
        Mongolian,

        [Language(LongName = "Montenegrin", ThreeLetterCode = "cnr")]
        Montenegrin,

        [Language(LongName = "Mossi", ThreeLetterCode = "mos")]
        Mossi,

        #endregion

        #region N

        [Language(LongName = "N'Ko", ThreeLetterCode = "nqo")]
        NKo,

        [Language(LongName = "Nauru", TwoLetterCode = "na", ThreeLetterCode = "nau")]
        Nauru,

        // Navajo; Navaho
        [Language(LongName = "Navajo", TwoLetterCode = "nv", ThreeLetterCode = "nav")]
        Navajo,

        [Language(LongName = "Ndonga", TwoLetterCode = "ng", ThreeLetterCode = "ndo")]
        Ndonga,

        [Language(LongName = "Neapolitan", ThreeLetterCode = "nap")]
        Neapolitan,

        // Nepal Bhasa; Newari
        [Language(LongName = "Nepal Bhasa", ThreeLetterCode = "new")]
        NepalBhasa,

        [Language(LongName = "Nepali", TwoLetterCode = "ne", ThreeLetterCode = "nep")]
        Nepali,

        [Language(LongName = "Nias", ThreeLetterCode = "nia")]
        Nias,

        [Language(LongName = "Niuean", ThreeLetterCode = "niu")]
        Niuean,

        // Commented out to avoid confusion
        //[Language(LongName = "No linguistic content; Not applicable", ThreeLetterCode = "zxx")]
        //NoLinguisticContent,

        [Language(LongName = "Nogai", ThreeLetterCode = "nog")]
        Nogai,

        [Language(LongName = "Norse, Old", ThreeLetterCode = "non")]
        OldNorse,

        [Language(LongName = "North Ndebele", TwoLetterCode = "nd", ThreeLetterCode = "nde")]
        NorthNdebele,

        [Language(LongName = "Northern Frisian", ThreeLetterCode = "frr")]
        NorthernFrisian,

        [Language(LongName = "Northern Sami", TwoLetterCode = "se", ThreeLetterCode = "sme")]
        NorthernSami,

        [Language(LongName = "Norwegian", TwoLetterCode = "no", ThreeLetterCode = "nor")]
        Norwegian,

        [Language(LongName = "Norwegian Bokmål", TwoLetterCode = "nb", ThreeLetterCode = "nob")]
        NorwegianBokmal,

        [Language(LongName = "Norwegian Nynorsk", TwoLetterCode = "nn", ThreeLetterCode = "nno")]
        NorwegianNynorsk,

        [Language(LongName = "Nyamwezi", ThreeLetterCode = "nym")]
        Nyamwezi,

        [Language(LongName = "Nyankole", ThreeLetterCode = "nyn")]
        Nyankole,

        [Language(LongName = "Nyoro", ThreeLetterCode = "nyo")]
        Nyoro,

        [Language(LongName = "Nzima", ThreeLetterCode = "nzi")]
        Nzima,

        #endregion

        #region O

        [Language(LongName = "Occitan (post 1500)", TwoLetterCode = "oc", ThreeLetterCode = "oci")]
        Occitan,

        // Occitan, Old (to 1500); Provençal, Old (to 1500)
        [Language(LongName = "Occitan, Old (to 1500)", ThreeLetterCode = "pro")]
        OldOccitan,

        [Language(LongName = "Ojibwa", TwoLetterCode = "oj", ThreeLetterCode = "oji")]
        Ojibwa,

        [Language(LongName = "Oriya", TwoLetterCode = "or", ThreeLetterCode = "ori")]
        Oriya,

        [Language(LongName = "Oromo", TwoLetterCode = "om", ThreeLetterCode = "orm")]
        Oromo,

        [Language(LongName = "Osage", ThreeLetterCode = "osa")]
        Osage,

        // Ossetian; Ossetic
        [Language(LongName = "Ossetian", TwoLetterCode = "os", ThreeLetterCode = "oss")]
        Ossetian,

        #endregion

        #region P

        [Language(LongName = "Pahlavi", ThreeLetterCode = "pal")]
        Pahlavi,

        [Language(LongName = "Palauan", ThreeLetterCode = "pau")]
        Palauan,

        [Language(LongName = "Pali", TwoLetterCode = "pi", ThreeLetterCode = "pli")]
        Pali,

        // Pampanga; Kapampangan
        [Language(LongName = "Pampanga", ThreeLetterCode = "pam")]
        Pampanga,

        [Language(LongName = "Pangasinan", ThreeLetterCode = "pag")]
        Pangasinan,

        // Panjabi; Punjabi
        [Language(LongName = "Panjabi", TwoLetterCode = "pa", ThreeLetterCode = "pan")]
        Panjabi,

        [Language(LongName = "Papiamento", ThreeLetterCode = "pap")]
        Papiamento,

        // Pedi; Sepedi; Northern Sotho
        [Language(LongName = "Pedi", ThreeLetterCode = "nso")]
        Pedi,

        [Language(LongName = "Persian", TwoLetterCode = "fa", ThreeLetterCode = "per", ThreeLetterCodeAlt = "fas")]
        Persian,

        [Language(LongName = "Persian, Old (ca.600-400 B.C.)", ThreeLetterCode = "peo")]
        OldPersian,

        [Language(LongName = "Phoenician", ThreeLetterCode = "phn")]
        Phoenician,

        [Language(LongName = "Polish", TwoLetterCode = "pl", ThreeLetterCode = "pol")]
        Polish,

        [Language(LongName = "Portuguese", TwoLetterCode = "pt", ThreeLetterCode = "por")]
        Portuguese,

        // Pushto; Pashto
        [Language(LongName = "Pushto", TwoLetterCode = "ps", ThreeLetterCode = "pus")]
        Pushto,

        #endregion

        #region Q

        // qaa-qtz: Reserved for local use

        [Language(LongName = "Quechua", TwoLetterCode = "qu", ThreeLetterCode = "que")]
        Quechua,

        #endregion

        #region R

        [Language(LongName = "Rajasthani", ThreeLetterCode = "raj")]
        Rajasthani,

        [Language(LongName = "Rapanui", ThreeLetterCode = "rap")]
        Rapanui,

        // Rarotongan; Cook Islands Maori
        [Language(LongName = "Rarotongan", ThreeLetterCode = "rar")]
        Rarotongan,

        // Romanian; Moldavian; Moldovan
        [Language(LongName = "Romanian", TwoLetterCode = "ro", ThreeLetterCode = "rum", ThreeLetterCodeAlt = "ron")]
        Romanian,

        [Language(LongName = "Romansh", TwoLetterCode = "rm", ThreeLetterCode = "roh")]
        Romansh,

        [Language(LongName = "Romany", ThreeLetterCode = "rom")]
        Romany,

        [Language(LongName = "Rundi", TwoLetterCode = "rn", ThreeLetterCode = "run")]
        Rundi,

        [Language(LongName = "Russian", TwoLetterCode = "ru", ThreeLetterCode = "rus")]
        Russian,

        #endregion

        #region S

        [Language(LongName = "Samaritan Aramaic", ThreeLetterCode = "sam")]
        SamaritanAramaic,

        [Language(LongName = "Samoan", TwoLetterCode = "sm", ThreeLetterCode = "smo")]
        Samoan,

        [Language(LongName = "Sandawe", ThreeLetterCode = "sad")]
        Sandawe,

        [Language(LongName = "Sango", TwoLetterCode = "sg", ThreeLetterCode = "sag")]
        Sango,

        [Language(LongName = "Sanskrit", TwoLetterCode = "sa", ThreeLetterCode = "san")]
        Sanskrit,

        [Language(LongName = "Santali", ThreeLetterCode = "sat")]
        Santali,

        [Language(LongName = "Sardinian", TwoLetterCode = "sc", ThreeLetterCode = "srd")]
        Sardinian,

        [Language(LongName = "Sasak", ThreeLetterCode = "sas")]
        Sasak,

        [Language(LongName = "Scots", ThreeLetterCode = "sco")]
        Scots,

        [Language(LongName = "Selkup", ThreeLetterCode = "sel")]
        Selkup,

        [Language(LongName = "Serbian", TwoLetterCode = "sr", ThreeLetterCode = "srp")]
        Serbian,

        [Language(LongName = "Serer", ThreeLetterCode = "srr")]
        Serer,

        [Language(LongName = "Shan", ThreeLetterCode = "shn")]
        Shan,

        [Language(LongName = "Shona", TwoLetterCode = "sn", ThreeLetterCode = "sna")]
        Shona,

        // Sichuan Yi; Nuosu
        [Language(LongName = "Sichuan Yi", TwoLetterCode = "ii", ThreeLetterCode = "iii")]
        SichuanYi,

        [Language(LongName = "Sicilian", ThreeLetterCode = "scn")]
        Sicilian,

        [Language(LongName = "Sidamo", ThreeLetterCode = "sid")]
        Sidamo,

        [Language(LongName = "Sign Languages", ThreeLetterCode = "sgn")]
        SignLanguages,

        [Language(LongName = "Siksika", ThreeLetterCode = "bla")]
        Siksika,

        [Language(LongName = "Sindhi", TwoLetterCode = "sd", ThreeLetterCode = "snd")]
        Sindhi,

        // Sinhala; Sinhalese
        [Language(LongName = "Sinhala", TwoLetterCode = "si", ThreeLetterCode = "sin")]
        Sinhala,

        [Language(LongName = "Skolt Sami", ThreeLetterCode = "sms")]
        SkoltSami,

        [Language(LongName = "Slovak", TwoLetterCode = "sk", ThreeLetterCode = "slo", ThreeLetterCodeAlt = "slk")]
        Slovak,

        [Language(LongName = "Slovenian", TwoLetterCode = "sl", ThreeLetterCode = "slv")]
        Slovenian,

        [Language(LongName = "Sogdian", ThreeLetterCode = "sog")]
        Sogdian,

        [Language(LongName = "Somali", TwoLetterCode = "so", ThreeLetterCode = "som")]
        Somali,

        [Language(LongName = "Soninke", ThreeLetterCode = "snk")]
        Soninke,

        [Language(LongName = "Sotho, Southern", TwoLetterCode = "st", ThreeLetterCode = "sot")]
        Sotho,

        [Language(LongName = "South Ndebele", TwoLetterCode = "nr", ThreeLetterCode = "nbl")]
        SouthNdebele,

        [Language(LongName = "Southern Altai", ThreeLetterCode = "alt")]
        SouthernAltai,

        [Language(LongName = "Southern Sami", ThreeLetterCode = "sma")]
        SouthernSami,

        // Spanish; Castilian
        [Language(LongName = "Spanish", TwoLetterCode = "es", ThreeLetterCode = "spa")]
        Spanish,

        [Language(LongName = "Sranan Tongo", ThreeLetterCode = "srn")]
        SrananTongo,

        [Language(LongName = "Standard Moroccan Tamazight", ThreeLetterCode = "zgh")]
        StandardMoroccanTamazight,

        [Language(LongName = "Sukuma", ThreeLetterCode = "suk")]
        Sukuma,

        [Language(LongName = "Sumerian", ThreeLetterCode = "sux")]
        Sumerian,

        [Language(LongName = "Sundanese", TwoLetterCode = "su", ThreeLetterCode = "sun")]
        Sundanese,

        [Language(LongName = "Susu", ThreeLetterCode = "sus")]
        Susu,

        [Language(LongName = "Susu", TwoLetterCode = "sw", ThreeLetterCode = "swa")]
        Swahili,

        [Language(LongName = "Swatio", TwoLetterCode = "ss", ThreeLetterCode = "ssw")]
        Swati,

        [Language(LongName = "Swedish", TwoLetterCode = "sv", ThreeLetterCode = "swe")]
        Swedish,

        // Swiss German; Alemannic; Alsatian
        [Language(LongName = "Swiss German", ThreeLetterCode = "gsw")]
        SwissGerman,

        [Language(LongName = "Syriac", ThreeLetterCode = "syr")]
        Syriac,

        [Language(LongName = "Syriac, Classical", ThreeLetterCode = "syc")]
        ClassicalSyriac,

        #endregion

        #region T

        [Language(LongName = "Tagalog", TwoLetterCode = "tl", ThreeLetterCode = "tgl")]
        Tagalog,

        [Language(LongName = "Tahitian", TwoLetterCode = "ty", ThreeLetterCode = "tah")]
        Tahitian,

        [Language(LongName = "Tajik", TwoLetterCode = "tg", ThreeLetterCode = "tgk")]
        Tajik,

        [Language(LongName = "Tamashek", ThreeLetterCode = "tmh")]
        Tamashek,

        [Language(LongName = "Tamil", TwoLetterCode = "ta", ThreeLetterCode = "tam")]
        Tamil,

        [Language(LongName = "Tatar", TwoLetterCode = "tt", ThreeLetterCode = "tat")]
        Tatar,

        [Language(LongName = "Telugu", TwoLetterCode = "te", ThreeLetterCode = "tel")]
        Telugu,

        [Language(LongName = "Tereno", ThreeLetterCode = "ter")]
        Tereno,

        [Language(LongName = "Tetum", ThreeLetterCode = "tet")]
        Tetum,

        [Language(LongName = "Thai", TwoLetterCode = "th", ThreeLetterCode = "tha")]
        Thai,

        [Language(LongName = "Tibetan", TwoLetterCode = "bo", ThreeLetterCode = "tib", ThreeLetterCodeAlt = "bod")]
        Tibetan,

        [Language(LongName = "Tigre", ThreeLetterCode = "tig")]
        Tigre,

        [Language(LongName = "Tigrinya", TwoLetterCode = "ti", ThreeLetterCode = "tir")]
        Tigrinya,

        [Language(LongName = "Timne", ThreeLetterCode = "tem")]
        Timne,

        [Language(LongName = "Tiv", ThreeLetterCode = "tiv")]
        Tiv,

        [Language(LongName = "Tlingit", ThreeLetterCode = "tli")]
        Tlingit,

        [Language(LongName = "Tok Pisin", ThreeLetterCode = "tpi")]
        TokPisin,

        [Language(LongName = "Tokelau", ThreeLetterCode = "tkl")]
        Tokelau,

        [Language(LongName = "Tonga (Nyasa)", ThreeLetterCode = "tog")]
        TongaNyasa,

        [Language(LongName = "Tonga (Tonga Islands)", TwoLetterCode = "to", ThreeLetterCode = "ton")]
        TongaIslands,

        [Language(LongName = "Tsimshian", ThreeLetterCode = "tsi")]
        Tsimshian,

        [Language(LongName = "Tsonga", TwoLetterCode = "ts", ThreeLetterCode = "tso")]
        Tsonga,

        [Language(LongName = "Tswana", TwoLetterCode = "tn", ThreeLetterCode = "tsn")]
        Tswana,

        [Language(LongName = "Tumbuka", ThreeLetterCode = "tum")]
        Tumbuka,

        [Language(LongName = "Turkish", TwoLetterCode = "tr", ThreeLetterCode = "tur")]
        Turkish,

        [Language(LongName = "Turkish, Ottoman (1500-1928)", ThreeLetterCode = "ota")]
        OttomanTurkish,

        [Language(LongName = "Turkmen", TwoLetterCode = "tk", ThreeLetterCode = "tuk")]
        Turkmen,

        [Language(LongName = "Tuvalu", ThreeLetterCode = "tvl")]
        Tuvalu,

        [Language(LongName = "Tuvinian", ThreeLetterCode = "tyv")]
        Tuvinian,

        [Language(LongName = "Twi", TwoLetterCode = "tw", ThreeLetterCode = "twi")]
        Twi,

        #endregion

        #region U

        [Language(LongName = "Udmurt", ThreeLetterCode = "udm")]
        Udmurt,

        [Language(LongName = "Ugaritic", ThreeLetterCode = "uga")]
        Ugaritic,

        // Uighur; Uyghur
        [Language(LongName = "Uighur", TwoLetterCode = "ug", ThreeLetterCode = "uig")]
        Uighur,

        [Language(LongName = "Ukrainian", TwoLetterCode = "uk", ThreeLetterCode = "ukr")]
        Ukrainian,

        [Language(LongName = "Umbundu", ThreeLetterCode = "umb")]
        Umbundu,

        // Commented out to avoid confusion
        //[Language(LongName = "Undetermined", ThreeLetterCode = "und")]
        //Undetermined,

        [Language(LongName = "Upper Sorbian", ThreeLetterCode = "hsb")]
        UpperSorbian,

        [Language(LongName = "Urdu", TwoLetterCode = "ur", ThreeLetterCode = "urd")]
        Urdu,

        [Language(LongName = "Uzbek", TwoLetterCode = "uz", ThreeLetterCode = "uzb")]
        Uzbek,

        #endregion

        #region V

        [Language(LongName = "Vai", ThreeLetterCode = "vai")]
        Vai,

        [Language(LongName = "Venda", TwoLetterCode = "ve", ThreeLetterCode = "ven")]
        Venda,

        [Language(LongName = "Vietnamese", TwoLetterCode = "vi", ThreeLetterCode = "vie")]
        Vietnamese,

        [Language(LongName = "Volapük", TwoLetterCode = "vo", ThreeLetterCode = "vol")]
        Volapuk,

        [Language(LongName = "Votic", ThreeLetterCode = "vot")]
        Votic,

        #endregion

        #region W

        [Language(LongName = "Walloon", TwoLetterCode = "wa", ThreeLetterCode = "wln")]
        Walloon,

        [Language(LongName = "Waray", ThreeLetterCode = "war")]
        Waray,

        [Language(LongName = "Washo", ThreeLetterCode = "was")]
        Washo,

        [Language(LongName = "Welsh", TwoLetterCode = "cy", ThreeLetterCode = "wel", ThreeLetterCodeAlt = "cym")]
        Welsh,

        [Language(LongName = "Western Frisian", TwoLetterCode = "fy", ThreeLetterCode = "fry")]
        WesternFrisian,

        // Wolaitta; Wolaytta
        [Language(LongName = "Wolaitta", ThreeLetterCode = "wal")]
        Wolaitta,

        [Language(LongName = "Wolof", TwoLetterCode = "wo", ThreeLetterCode = "wol")]
        Wolof,

        #endregion

        #region X

        [Language(LongName = "Xhosa", TwoLetterCode = "xh", ThreeLetterCode = "xho")]
        Xhosa,

        #endregion

        #region Y

        [Language(LongName = "Yakut", ThreeLetterCode = "sah")]
        Yakut,

        [Language(LongName = "Yao", ThreeLetterCode = "yao")]
        Yao,

        [Language(LongName = "Yapese", ThreeLetterCode = "yap")]
        Yapese,

        [Language(LongName = "Yiddish", TwoLetterCode = "yi", ThreeLetterCode = "yid")]
        Yiddish,

        [Language(LongName = "Yoruba", TwoLetterCode = "yo", ThreeLetterCode = "yor")]
        Yoruba,

        #endregion

        #region Z

        [Language(LongName = "Zapotec", ThreeLetterCode = "zap")]
        Zapotec,

        // Zaza; Dimili; Dimli; Kirdki; Kirmanjki; Zazaki
        [Language(LongName = "Zaza", ThreeLetterCode = "zza")]
        Zaza,

        [Language(LongName = "Zenaga", ThreeLetterCode = "zen")]
        Zenaga,

        // Zhuang; Chuang
        [Language(LongName = "Zhuang", TwoLetterCode = "za", ThreeLetterCode = "zha")]
        Zhuang,

        [Language(LongName = "Zulu", TwoLetterCode = "zu", ThreeLetterCode = "zul")]
        Zulu,

        [Language(LongName = "Zuni", ThreeLetterCode = "zun")]
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
    /// List of all known systems
    /// </summary>
    /// TODO: Remove marker items
    /// TODO: Double check all flags once the site is live
    /// TODO: Add MAXTEST as a debug-only system
    /// TODO: Does "Banned" now only mean that things like keys can't be downloaded?
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
    /// List of all known regions
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
    /// </remarks>
    public enum Region
    {
        #region Aggregates - Redump Only

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

        [HumanReadable(LongName = "Ukraine", ShortName = "ua")]
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
