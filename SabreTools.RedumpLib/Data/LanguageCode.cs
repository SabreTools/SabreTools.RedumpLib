namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// Represents a single language code
    /// </summary>
    /// <remarks>https://www.loc.gov/standards/iso639-2/php/code_list.php</remarks>
    public sealed class LanguageCode
    {
        #region Properties

        /// <summary>
        /// Human-readable name of the language
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// ISO 639-1 Code
        /// </summary>
        public readonly string? TwoLetterCode;

        /// <summary>
        /// ISO 639-2 Code (Standard or Bibliographic)
        /// </summary>
        public readonly string? ThreeLetterCode;

        /// <summary>
        /// ISO 639-2 Code (Terminology)
        /// </summary>
        public readonly string? ThreeLetterCodeAlt;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private LanguageCode(string name, string? twoLetterCode, string? threeLetterCode, string? threeLetterCodeAlt)
        {
            Name = name;
            TwoLetterCode = twoLetterCode;
            ThreeLetterCode = threeLetterCode;
            ThreeLetterCodeAlt = threeLetterCodeAlt;
        }

        #endregion

        #region Static Instances

        #region A

        public static readonly LanguageCode Abkhazian = new("Abkhazian",
            "ab",
            "abk",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Achinese = new("Achinese",
            twoLetterCode: null,
            "ace",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Acoli = new("Acoli",
            twoLetterCode: null,
            "ach",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Adangme = new("Adangme",
            twoLetterCode: null,
            "ada",
            threeLetterCodeAlt: null);

        // Adyghe; Adygei
        public static readonly LanguageCode Adyghe = new("Adyghe",
            twoLetterCode: null,
            "ady",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Afar = new("Afar",
            "aa",
            "aar",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Afrihili = new("Afrihili",
            twoLetterCode: null,
            "afh",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Afrikaans = new("Afrikaans",
            "af",
            "afr",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Ainu = new("Ainu",
            twoLetterCode: null,
            "ain",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Akan = new("Akan",
            "ak",
            "aka",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Akkadian = new("Akkadian",
            twoLetterCode: null,
            "akk",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Albanian = new("Albanian",
            "sq",
            "alb",
            "sqi");

        public static readonly LanguageCode Aleut = new("Aleut",
            twoLetterCode: null,
            "ale",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Amharic = new("Amharic",
            "am",
            "amh",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Angika = new("Angika",
            twoLetterCode: null,
            "anp",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Arabic = new("Arabic",
            "ar",
            "ara",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Aragonese = new("Aragonese",
            "an",
            "arg",
            threeLetterCodeAlt: null);

        // Official Aramaic (700-300 BCE); Imperial Aramaic (700-300 BCE)
        public static readonly LanguageCode Aramaic = new("Aramaic",
            twoLetterCode: null,
            "arc",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Armenian = new("Armenian",
            "hy",
            "arm",
            "hye");

        public static readonly LanguageCode Arapaho = new("Arapaho",
            twoLetterCode: null,
            "arp",
            threeLetterCodeAlt: null);

        // Aromanian; Arumanian; Macedo-Romanian
        public static readonly LanguageCode Aromanian = new("Aromanian",
            twoLetterCode: null,
            "rup",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Arawak = new("Arawak",
            twoLetterCode: null,
            "arw",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Assamese = new("Assamese",
            "as",
            "asm",
            threeLetterCodeAlt: null);

        // Asturian; Bable; Leonese; Asturleonese
        public static readonly LanguageCode Asturian = new("Asturian",
            twoLetterCode: null,
            "ast",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Athapascan = new("Athapascan",
            twoLetterCode: null,
            "den",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Avaric = new("Avaric",
            "av",
            "ava",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Avestan = new("Avestan",
            "ae",
            "ave",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Awadhi = new("Awadhi",
            twoLetterCode: null,
            "awa",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Aymara = new("Aymara",
            "ay",
            "aym",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Azerbaijani = new("Azerbaijani",
            "az",
            "aze",
            threeLetterCodeAlt: null);

        #endregion

        #region B

        public static readonly LanguageCode Balinese = new("Balinese",
            twoLetterCode: null,
            "ban",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Baluchi = new("Baluchi",
            twoLetterCode: null,
            "bal",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Bambara = new("Bambara",
            "bm",
            "bam",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Basa = new("Basa",
            twoLetterCode: null,
            "bas",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Bashkir = new("Bashkir",
            "ba",
            "bak",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Basque = new("Basque",
            "eu",
            "baq",
            "eus");

        // Beja; Bedawiyet
        public static readonly LanguageCode Beja = new("Beja",
            twoLetterCode: null,
            "bej",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Belarusian = new("Belarusian",
            "be",
            "bel",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Bemba = new("Bemba",
            twoLetterCode: null,
            "bem",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Bengali = new("Bengali",
            "bn",
            "ben",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Bhojpuri = new("Bhojpuri",
            twoLetterCode: null,
            "bho",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Bikol = new("Bikol",
            twoLetterCode: null,
            "bik",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Bini = new("Bini; Edo",
            twoLetterCode: null,
            "bin",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Bislama = new("Bislama",
            "bi",
            "bis",
            threeLetterCodeAlt: null);

        // Blin; Bilin
        public static readonly LanguageCode Blin = new("Blin",
            twoLetterCode: null,
            "byn",
            threeLetterCodeAlt: null);

        // Blissymbols; Blissymbolics; Bliss
        public static readonly LanguageCode Blissymbols = new("Blissymbols",
            twoLetterCode: null,
            "zbl",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Bosnian = new("Bosnian",
            "bs",
            "bos",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Braj = new("Braj",
            twoLetterCode: null,
            "bra",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Breton = new("Breton",
            "br",
            "bre",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Buginese = new("Buginese",
            twoLetterCode: null,
            "bug",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Bulgarian = new("Bulgarian",
            "bg",
            "bul",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Buriat = new("Buriat",
            twoLetterCode: null,
            "bua",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Burmese = new("Burmese",
            "my",
            "bur",
            "mya");

        #endregion

        #region C

        public static readonly LanguageCode Caddo = new("Caddo",
            twoLetterCode: null,
            "cad",
            threeLetterCodeAlt: null);

        // Catalan; Valencian
        public static readonly LanguageCode Catalan = new("Catalan",
            "ca",
            "cat",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Cebuano = new("Cebuano",
            twoLetterCode: null,
            "ceb",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode CentralKhmer = new("Central Khmer",
            "km",
            "khm",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Chagatai = new("Chagatai",
            twoLetterCode: null,
            "chg",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Chamorro = new("Chamorro",
            "ch",
            "cha",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Chechen = new("Chechen",
            "ce",
            "che",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Cherokee = new("Cherokee",
            twoLetterCode: null,
            "chr",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Cheyenne = new("Cheyenne",
            twoLetterCode: null,
            "chy",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Chibcha = new("Chibcha",
            twoLetterCode: null,
            "chb",
            threeLetterCodeAlt: null);

        // Chichewa; Chewa; Nyanja
        public static readonly LanguageCode Chichewa = new("Chichewa",
            "ny",
            "nya",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Chinese = new("Chinese",
            "zh",
            "chi",
            "zho");

        public static readonly LanguageCode ChinookJargon = new("Chinook jargon",
            twoLetterCode: null,
            "chn",
            threeLetterCodeAlt: null);

        // Chipewyan; Dene Suline
        public static readonly LanguageCode Chipewyan = new("Chipewyan",
            twoLetterCode: null,
            "chp",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Choctaw = new("Choctaw",
            twoLetterCode: null,
            "cho",
            threeLetterCodeAlt: null);

        // Church Slavic; Old Slavonic; Church Slavonic; Old Bulgarian; Old Church Slavonic
        public static readonly LanguageCode ChurchSlavic = new("Church Slavic",
            "cu",
            "chu",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Chuukese = new("Chuukese",
            twoLetterCode: null,
            "chk",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Chuvash = new("Chuvash",
            "cv",
            "chv",
            threeLetterCodeAlt: null);

        // Classical Newari; Old Newari; Classical Nepal Bhasa
        public static readonly LanguageCode ClassicalNewari = new("Classical Newari",
            twoLetterCode: null,
            "nwc",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Coptic = new("Coptic",
            twoLetterCode: null,
            "cop",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Cornish = new("Cornish",
            "kw",
            "cor",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Corsican = new("Corsican",
            "co",
            "cos",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Cree = new("Cree",
            "cr",
            "cre",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Creek = new("Creek",
            twoLetterCode: null,
            "mus",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode CreolesAndPidgins = new("Creoles and pidgins",
            twoLetterCode: null,
            "crp",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode EnglishCreole = new("Creoles and pidgins (English-based)",
            twoLetterCode: null,
            "cpe",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode FrenchCreole = new("Creoles and pidgins (French-based)",
            twoLetterCode: null,
            "cpf",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode PortugueseCreole = new("Creoles and pidgins (Portuguese-based)",
            twoLetterCode: null,
            "cpp",
            threeLetterCodeAlt: null);

        // Crimean Tatar; Crimean Turkish
        public static readonly LanguageCode CrimeanTatar = new("Crimean Tatar",
            twoLetterCode: null,
            "crh",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Croatian = new("Croatian",
            "hr",
            "hrv",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Czech = new("Czech",
            "cs",
            "cze",
            "ces");

        #endregion

        #region D

        public static readonly LanguageCode Dakota = new("Dakota",
            twoLetterCode: null,
            "dak",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Danish = new("Danish",
            "da",
            "dan",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Dargwa = new("Dargwa",
            twoLetterCode: null,
            "dar",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Delaware = new("Delaware",
            twoLetterCode: null,
            "del",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Dinka = new("Dinka",
            twoLetterCode: null,
            "din",
            threeLetterCodeAlt: null);

        // Divehi; Dhivehi; Maldivian
        public static readonly LanguageCode Divehi = new("Divehi",
            "dv",
            "div",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Dogrib = new("Dogrib",
            twoLetterCode: null,
            "dgr",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Dogri = new("Dogri",
            twoLetterCode: null,
            "doi",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Duala = new("Duala",
            twoLetterCode: null,
            "dua",
            threeLetterCodeAlt: null);

        // Dutch; Flemish
        public static readonly LanguageCode Dutch = new("Dutch",
            "nl",
            "dut",
            "nld");

        public static readonly LanguageCode MiddleDutch = new("Dutch, Middle (ca.1050-1350)",
            twoLetterCode: null,
            "dum",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Dyula = new("Dyula",
            twoLetterCode: null,
            "dyu",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Dzongkha = new("Dzongkha",
            "dz",
            "dzo",
            threeLetterCodeAlt: null);

        #endregion

        #region E

        public static readonly LanguageCode EasternFrisian = new("Eastern Frisian",
            twoLetterCode: null,
            "frs",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Efik = new("Efik",
            twoLetterCode: null,
            "efi",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode AncientEgyptian = new("Egyptian (Ancient)",
            twoLetterCode: null,
            "egy",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Ekajuk = new("Ekajuk",
            twoLetterCode: null,
            "eka",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Elamite = new("Elamite",
            twoLetterCode: null,
            "elx",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode English = new("English",
            "en",
            "eng",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode OldEnglish = new("English, Old (ca.450-1100)",
            twoLetterCode: null,
            "ang",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode MiddleEnglish = new("English, Middle (1100-1500)",
            twoLetterCode: null,
            "enm",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Erzya = new("Erzya",
            twoLetterCode: null,
            "myv",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Esperanto = new("Esperanto",
            "eo",
            "epo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Estonian = new("Estonian",
            "et",
            "est",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Ewe = new("Ewe",
            "ee",
            "ewe",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Ewondo = new("Ewondo",
            twoLetterCode: null,
            "ewo",
            threeLetterCodeAlt: null);

        #endregion

        #region F

        public static readonly LanguageCode Fang = new("Fang",
            twoLetterCode: null,
            "fan",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Faroese = new("Faroese",
            "fo",
            "fao",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Fanti = new("Fanti",
            twoLetterCode: null,
            "fat",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Fijian = new("Fijian",
            "fj",
            "fij",
            threeLetterCodeAlt: null);

        // Filipino; Pilipino
        public static readonly LanguageCode Filipino = new("Filipino",
            twoLetterCode: null,
            "fil",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Finnish = new("Finnish",
            "fi",
            "fin",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Fon = new("Fon",
            twoLetterCode: null,
            "fon",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode French = new("French",
            "fr",
            "fre",
            "fra");

        public static readonly LanguageCode MiddleFrench = new("French, Middle (ca.1400-1600)",
            twoLetterCode: null,
            "frm",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode OldFrench = new("French, Old (842-ca.1400)",
            twoLetterCode: null,
            "fro",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Friulian = new("Friulian",
            twoLetterCode: null,
            "fur",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Fulah = new("Fulah",
            "ff",
            "ful",
            threeLetterCodeAlt: null);

        #endregion

        #region G

        public static readonly LanguageCode Ga = new("Ga",
            twoLetterCode: null,
            "gaa",
            threeLetterCodeAlt: null);

        // Gaelic; Scottish Gaelic
        public static readonly LanguageCode Gaelic = new("Gaelic",
            "gd",
            "gla",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode GalibiCarib = new("Galibi Carib",
            twoLetterCode: null,
            "car",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Galician = new("Galician",
            "gl",
            "glg",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Ganda = new("Ganda",
            "lg",
            "lug",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Gayo = new("Gayo",
            twoLetterCode: null,
            "gay",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Gbaya = new("Gbaya",
            twoLetterCode: null,
            "gba",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Geez = new("Geez",
            twoLetterCode: null,
            "gez",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Georgian = new("Georgian",
            "ka",
            "geo",
            "kat");

        public static readonly LanguageCode German = new("German",
            "de",
            "ger",
            "deu");

        public static readonly LanguageCode MiddleHighGerman = new("German, Middle High (ca.1050-1500)",
            twoLetterCode: null,
            "gmh",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode OldHighGerman = new("German, Old High (ca.750-1050)",
            twoLetterCode: null,
            "goh",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Gilbertese = new("Gilbertese",
            twoLetterCode: null,
            "gil",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Gondi = new("Gondi",
            twoLetterCode: null,
            "gon",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Gorontalo = new("Gorontalo",
            twoLetterCode: null,
            "gor",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Gothic = new("Gothic",
            twoLetterCode: null,
            "got",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Grebo = new("Grebo",
            twoLetterCode: null,
            "grb",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Greek = new("Greek",
            "el",
            "gre",
            "eli");

        public static readonly LanguageCode AncientGreek = new("Greek, Ancient (to 1453)",
            twoLetterCode: null,
            "grc",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Guarani = new("Guarani",
            "gn",
            "grn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Gujarati = new("Gujarati",
            "gu",
            "guj",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Gwichin = new("Gwich'in",
            twoLetterCode: null,
            "gwi",
            threeLetterCodeAlt: null);

        #endregion

        #region H

        public static readonly LanguageCode Haida = new("Haida",
            twoLetterCode: null,
            "hai",
            threeLetterCodeAlt: null);

        // Haitian; Haitian Creole
        public static readonly LanguageCode Haitian = new("Haitian",
            "ht",
            "hat",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Hausa = new("Hausa",
            "ha",
            "gau",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Hawaiian = new("Hawaiian",
            twoLetterCode: null,
            "haw",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Hebrew = new("Hebrew",
            "he",
            "heb",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Herero = new("Herero",
            "hz",
            "her",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Hiligaynon = new("Hiligaynon",
            twoLetterCode: null,
            "hil",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Hindi = new("Hindi",
            "hi",
            "hin",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode HiriMotu = new("Hiri Motu",
            twoLetterCode: null,
            "hmo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Hittite = new("Hittite",
            twoLetterCode: null,
            "hit",
            threeLetterCodeAlt: null);

        // Hmong; Mong
        public static readonly LanguageCode Hmong = new("Hmong",
            twoLetterCode: null,
            "hmn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Hungarian = new("Hungarian",
            "hu",
            "hun",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Hupa = new("Hupa",
            twoLetterCode: null,
            "hup",
            threeLetterCodeAlt: null);

        #endregion

        #region I

        public static readonly LanguageCode Iban = new("Iban",
            twoLetterCode: null,
            "iba",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Icelandic = new("Icelandic",
            "is",
            "ice",
            "isl");

        public static readonly LanguageCode Ido = new("Ido",
            "io",
            "ido",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Igbo = new("Igbo",
            "ig",
            "ibo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Iloko = new("Iloko",
            twoLetterCode: null,
            "ilo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode InariSami = new("Inari Sami",
            twoLetterCode: null,
            "smn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Indonesian = new("Indonesian",
            "id",
            "ind",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Ingush = new("Ingush",
            twoLetterCode: null,
            "inh",
            threeLetterCodeAlt: null);

        // Interlingua (International Auxiliary Language Association)
        public static readonly LanguageCode Interlingua = new("Interlingua",
            "ia",
            "ina",
            threeLetterCodeAlt: null);

        // Interlingue; Occidental
        public static readonly LanguageCode Interlingue = new("Interlingue",
            "ie",
            "ile",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Inuktitut = new("Inuktitut",
            "iu",
            "iku",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Inupiaq = new("Inupiaq",
            "ik",
            "ipk",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Irish = new("Irish",
            "ga",
            "gle",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode MiddleIrish = new("Irish, Middle (900-1200)",
            twoLetterCode: null,
            "mga",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode OldIrish = new("Irish, Old (to 900)",
            twoLetterCode: null,
            "sga",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Italian = new("Italian",
            "it",
            "ita",
            threeLetterCodeAlt: null);

        #endregion

        #region J

        public static readonly LanguageCode Japanese = new("Japanese",
            "ja",
            "jap",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Javanese = new("Javanese",
            "jv",
            "jav",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode JudeoArabic = new("Judeo-Arabic",
            twoLetterCode: null,
            "jrb",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode JudeoPersian = new("Judeo-Persian",
            twoLetterCode: null,
            "jpr",
            threeLetterCodeAlt: null);

        #endregion

        #region K

        public static readonly LanguageCode Kabardian = new("Kabardian",
            twoLetterCode: null,
            "kbd",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kabyle = new("Kabyle",
            twoLetterCode: null,
            "kab",
            threeLetterCodeAlt: null);

        // Kachin; Jingpho
        public static readonly LanguageCode Kachin = new("Kachin",
            twoLetterCode: null,
            "kac",
            threeLetterCodeAlt: null);

        // Kalaallisut; Greenlandic
        public static readonly LanguageCode Kalaallisut = new("Kalaallisut",
            "kl",
            "kal",
            threeLetterCodeAlt: null);

        // Kalmyk; Oirat
        public static readonly LanguageCode Kalmyk = new("Kalmyk",
            twoLetterCode: null,
            "xal",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kamba = new("Kamba",
            twoLetterCode: null,
            "kam",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kannada = new("Kannada",
            "kn",
            "kan",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kanuri = new("Kanuri",
            "kr",
            "kau",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode KarachayBalkar = new("Karachay-Balkar",
            twoLetterCode: null,
            "krc",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode KaraKalpak = new("Kara-Kalpak",
            twoLetterCode: null,
            "kaa",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Karelian = new("Karelian",
            twoLetterCode: null,
            "krl",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kashmiri = new("Kashmiri",
            "ks",
            "kas",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kashubian = new("Kashubian",
            twoLetterCode: null,
            "csb",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kawi = new("Kawi",
            twoLetterCode: null,
            "kaw",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kazakh = new("Kazakh",
            "kk",
            "kaz",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Khasi = new("Khasi",
            twoLetterCode: null,
            "kha",
            threeLetterCodeAlt: null);

        // Khotanese; Sakan
        public static readonly LanguageCode Khotanese = new("Khotanese",
            twoLetterCode: null,
            "kho",
            threeLetterCodeAlt: null);

        // Kikuyu; Gikuyu
        public static readonly LanguageCode Kikuyu = new("Kikuyu",
            "ki",
            "kik",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kimbundu = new("Kimbundu",
            twoLetterCode: null,
            "kmb",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kinyarwanda = new("Kinyarwanda",
            "rw",
            "kin",
            threeLetterCodeAlt: null);

        // Kirghiz; Kyrgyz
        public static readonly LanguageCode Kirghiz = new("Kirghiz",
            "ky",
            "kir",
            threeLetterCodeAlt: null);

        // Klingon; tlhIngan-Hol
        public static readonly LanguageCode Klingon = new("Klingon",
            twoLetterCode: null,
            "tlh",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Komi = new("Komi",
            "kv",
            "kom",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kongo = new("Kongo",
            "kg",
            "kon",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Konkani = new("Konkani",
            twoLetterCode: null,
            "kok",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Korean = new("Korean",
            "ko",
            "kor",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kosraean = new("Kosraean",
            twoLetterCode: null,
            "kos",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kpelle = new("Kpelle",
            twoLetterCode: null,
            "kpe",
            threeLetterCodeAlt: null);

        // Kuanyama; Kwanyama
        public static readonly LanguageCode Kuanyama = new("Kuanyama",
            "kj",
            "kua",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kumyk = new("Kumyk",
            twoLetterCode: null,
            "kum",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kurdish = new("Kurdish",
            "ku",
            "kur",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kurukh = new("Kurukh",
            twoLetterCode: null,
            "kru",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Kutenai = new("Kutenai",
            twoLetterCode: null,
            "kut",
            threeLetterCodeAlt: null);

        #endregion

        #region L

        public static readonly LanguageCode Ladino = new("Ladino",
            twoLetterCode: null,
            "lad",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Lahnda = new("Lahnda",
            twoLetterCode: null,
            "lah",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Lamba = new("Lamba",
            twoLetterCode: null,
            "lam",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Lao = new("Lao",
            "lo",
            "lao",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Latin = new("Latin",
            "la",
            "lat",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Latvian = new("Latvian",
            "lv",
            "lav",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Lezghian = new("Lezghian",
            twoLetterCode: null,
            "lez",
            threeLetterCodeAlt: null);

        // Limburgan; Limburger; Limburgish
        public static readonly LanguageCode Limburgan = new("Limburgan",
            "li",
            "lim",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Lingala = new("Lingala",
            "ln",
            "lin",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Lithuanian = new("Lithuanian",
            "lt",
            "lit",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Lojban = new("Lojban",
            twoLetterCode: null,
            "jbo",
            threeLetterCodeAlt: null);

        // Low German; Low Saxon
        public static readonly LanguageCode LowGerman = new("Low German",
            twoLetterCode: null,
            "nds",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode LowerSorbian = new("Lower Sorbian",
            twoLetterCode: null,
            "dsb",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Lozi = new("Lozi",
            twoLetterCode: null,
            "loz",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode LubaKatanga = new("Luba-Katanga",
            "lu",
            "lub",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode LubaLulua = new("Luba-Lulua",
            twoLetterCode: null,
            "lua",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Luiseno = new("Luiseno",
            twoLetterCode: null,
            "lui",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode LuleSami = new("Lule Sami",
            twoLetterCode: null,
            "smj",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Lunda = new("Lunda",
            twoLetterCode: null,
            "lun",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Luo = new("Luo (Kenya and Tanzania)",
            twoLetterCode: null,
            "luo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Lushai = new("Lushai",
            twoLetterCode: null,
            "lus",
            threeLetterCodeAlt: null);

        // Luxembourgish; Letzeburgesch
        public static readonly LanguageCode Luxembourgish = new("Luxembourgish",
            "lb",
            "ltz",
            threeLetterCodeAlt: null);

        #endregion

        #region M

        public static readonly LanguageCode Macedonian = new("Macedonian",
            "mk",
            "mac",
            "mkd");

        public static readonly LanguageCode Madurese = new("Madurese",
            twoLetterCode: null,
            "mad",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Magahi = new("Magahi",
            twoLetterCode: null,
            "mag",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Maithili = new("Maithili",
            twoLetterCode: null,
            "mai",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Makasar = new("Makasar",
            twoLetterCode: null,
            "mak",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Malagasy = new("Malagasy",
            "mg",
            "mlg",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Malay = new("Malay",
            "ms",
            "may",
            "msa");

        public static readonly LanguageCode Malayalam = new("Malayalam",
            "ml",
            "mal",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Maltese = new("Maltese",
            "mt",
            "mlt",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Manchu = new("Manchu",
            twoLetterCode: null,
            "mnc",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Mandar = new("Mandar",
            twoLetterCode: null,
            "mdr",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Mandingo = new("Mandingo",
            twoLetterCode: null,
            "man",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Manipuri = new("Manipuri",
            twoLetterCode: null,
            "mni",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Manx = new("Manx",
            "gv",
            "glv",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Maori = new("Maori",
            "mi",
            "mao",
            "mri");

        // Mapudungun; Mapuche
        public static readonly LanguageCode Mapudungun = new("Mapudungun",
            twoLetterCode: null,
            "arn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Marathi = new("Marathi",
            "mr",
            "mar",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Mari = new("Mari",
            twoLetterCode: null,
            "chm",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Marshallese = new("Marshallese",
            "mh",
            "mah",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Marwari = new("Marwari",
            twoLetterCode: null,
            "mwr",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Masai = new("Masai",
            twoLetterCode: null,
            "mas",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Mende = new("Mende",
            twoLetterCode: null,
            "men",
            threeLetterCodeAlt: null);

        // Mi'kmaq; Micmac
        public static readonly LanguageCode Mikmaq = new("Mi'kmaq",
            twoLetterCode: null,
            "mic",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Minangkabau = new("Minangkabau",
            twoLetterCode: null,
            "min",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Mirandese = new("Mirandese",
            twoLetterCode: null,
            "mwl",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Mohawk = new("Mohawk",
            twoLetterCode: null,
            "moh",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Moksha = new("Moksha",
            twoLetterCode: null,
            "mdf",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Mongo = new("Mongo",
            twoLetterCode: null,
            "lol",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Mongolian = new("Mongolian",
            "mn",
            "mon",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Montenegrin = new("Montenegrin",
            twoLetterCode: null,
            "cnr",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Mossi = new("Mossi",
            twoLetterCode: null,
            "mos",
            threeLetterCodeAlt: null);

        #endregion

        #region N

        public static readonly LanguageCode NKo = new("N'Ko",
            twoLetterCode: null,
            "nqo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Nauru = new("Nauru",
            "na",
            "nau",
            threeLetterCodeAlt: null);

        // Navajo; Navaho
        public static readonly LanguageCode Navajo = new("Navajo",
            "nv",
            "nav",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Ndonga = new("Ndonga",
            "ng",
            "ndo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Neapolitan = new("Neapolitan",
            twoLetterCode: null,
            "nap",
            threeLetterCodeAlt: null);

        // Nepal Bhasa; Newari
        public static readonly LanguageCode NepalBhasa = new("Nepal Bhasa",
            twoLetterCode: null,
            "new",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Nepali = new("Nepali",
            "ne",
            "nep",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Nias = new("Nias",
            twoLetterCode: null,
            "nia",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Niuean = new("Niuean",
            twoLetterCode: null,
            "niu",
            threeLetterCodeAlt: null);

        // Commented out to avoid confusion
        // public static readonly LanguageCode NoLinguisticContent = new("No linguistic content; Not applicable",
        //     twoLetterCode: null,
        //     "zxx",
        //     threeLetterCodeAlt: null);

        public static readonly LanguageCode Nogai = new("Nogai",
            twoLetterCode: null,
            "nog",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode OldNorse = new("Norse, Old",
            twoLetterCode: null,
            "non",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode NorthNdebele = new("North Ndebele",
            "nd",
            "nde",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode NorthernFrisian = new("Northern Frisian",
            twoLetterCode: null,
            "frr",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode NorthernSami = new("Northern Sami",
            "se",
            "sme",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Norwegian = new("Norwegian",
            "no",
            "nor",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode NorwegianBokmal = new("Norwegian Bokmål",
            "nb",
            "nob",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode NorwegianNynorsk = new("Norwegian Nynorsk",
            "nn",
            "nno",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Nyamwezi = new("Nyamwezi",
            twoLetterCode: null,
            "nym",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Nyankole = new("Nyankole",
            twoLetterCode: null,
            "nyn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Nyoro = new("Nyoro",
            twoLetterCode: null,
            "nyo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Nzima = new("Nzima",
            twoLetterCode: null,
            "nzi",
            threeLetterCodeAlt: null);

        #endregion

        #region O

        public static readonly LanguageCode Occitan = new("Occitan (post 1500)",
            "oc",
            "oci",
            threeLetterCodeAlt: null);

        // Occitan, Old (to 1500); Provençal, Old (to 1500)
        public static readonly LanguageCode OldOccitan = new("Occitan, Old (to 1500)",
            twoLetterCode: null,
            "pro",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Ojibwa = new("Ojibwa",
            "oj",
            "oji",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Oriya = new("Oriya",
            "or",
            "ori",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Oromo = new("Oromo",
            "om",
            "orm",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Osage = new("Osage",
            twoLetterCode: null,
            "osa",
            threeLetterCodeAlt: null);

        // Ossetian; Ossetic
        public static readonly LanguageCode Ossetian = new("Ossetian",
            "os",
            "oss",
            threeLetterCodeAlt: null);

        #endregion

        #region P

        public static readonly LanguageCode Pahlavi = new("Pahlavi",
            twoLetterCode: null,
            "pal",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Palauan = new("Palauan",
            twoLetterCode: null,
            "pau",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Pali = new("Pali",
            "pi",
            "pli",
            threeLetterCodeAlt: null);

        // Pampanga; Kapampangan
        public static readonly LanguageCode Pampanga = new("Pampanga",
            twoLetterCode: null,
            "pam",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Pangasinan = new("Pangasinan",
            twoLetterCode: null,
            "pag",
            threeLetterCodeAlt: null);

        // Panjabi; Punjabi
        public static readonly LanguageCode Panjabi = new("Panjabi",
            "pa",
            "pan",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Papiamento = new("Papiamento",
            twoLetterCode: null,
            "pap",
            threeLetterCodeAlt: null);

        // Pedi; Sepedi; Northern Sotho
        public static readonly LanguageCode Pedi = new("Pedi",
            twoLetterCode: null,
            "nso",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Persian = new("Persian",
            "fa",
            "per",
            "fas");

        public static readonly LanguageCode OldPersian = new("Persian, Old (ca.600-400 B.C.)",
            twoLetterCode: null,
            "peo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Phoenician = new("Phoenician",
            twoLetterCode: null,
            "phn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Polish = new("Polish",
            "pl",
            "pol",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Portuguese = new("Portuguese",
            "pt",
            "por",
            threeLetterCodeAlt: null);

        // Pushto; Pashto
        public static readonly LanguageCode Pushto = new("Pushto",
            "ps",
            "pus",
            threeLetterCodeAlt: null);

        #endregion

        #region Q

        // qaa-qtz: Reserved for local use

        public static readonly LanguageCode Quechua = new("Quechua",
            "qu",
            "que",
            threeLetterCodeAlt: null);

        #endregion

        #region R

        public static readonly LanguageCode Rajasthani = new("Rajasthani",
            twoLetterCode: null,
            "raj",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Rapanui = new("Rapanui",
            twoLetterCode: null,
            "rap",
            threeLetterCodeAlt: null);

        // Rarotongan; Cook Islands Maori
        public static readonly LanguageCode Rarotongan = new("Rarotongan",
            twoLetterCode: null,
            "rar",
            threeLetterCodeAlt: null);

        // Romanian; Moldavian; Moldovan
        public static readonly LanguageCode Romanian = new("Romanian",
            "ro",
            "rum",
            "ron");

        public static readonly LanguageCode Romansh = new("Romansh",
            "rm",
            "roh",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Romany = new("Romany",
            twoLetterCode: null,
            "rom",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Rundi = new("Rundi",
            "rn",
            "run",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Russian = new("Russian",
            "ru",
            "rus",
            threeLetterCodeAlt: null);

        #endregion

        #region S

        public static readonly LanguageCode SamaritanAramaic = new("Samaritan Aramaic",
            twoLetterCode: null,
            "sam",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Samoan = new("Samoan",
            "sm",
            "smo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sandawe = new("Sandawe",
            twoLetterCode: null,
            "sad",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sango = new("Sango",
            "sg",
            "sag",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sanskrit = new("Sanskrit",
            "sa",
            "san",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Santali = new("Santali",
            twoLetterCode: null,
            "sat",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sardinian = new("Sardinian",
            "sc",
            "srd",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sasak = new("Sasak",
            twoLetterCode: null,
            "sas",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Scots = new("Scots",
            twoLetterCode: null,
            "sco",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Selkup = new("Selkup",
            twoLetterCode: null,
            "sel",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Serbian = new("Serbian",
            "sr",
            "srp",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Serer = new("Serer",
            twoLetterCode: null,
            "srr",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Shan = new("Shan",
            twoLetterCode: null,
            "shn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Shona = new("Shona",
            "sn",
            "sna",
            threeLetterCodeAlt: null);

        // Sichuan Yi; Nuosu
        public static readonly LanguageCode SichuanYi = new("Sichuan Yi",
            "ii",
            "iii",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sicilian = new("Sicilian",
            twoLetterCode: null,
            "scn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sidamo = new("Sidamo",
            twoLetterCode: null,
            "sid",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SignLanguages = new("Sign Languages",
            twoLetterCode: null,
            "sgn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Siksika = new("Siksika",
            twoLetterCode: null,
            "bla",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sindhi = new("Sindhi",
            "sd",
            "snd",
            threeLetterCodeAlt: null);

        // Sinhala; Sinhalese
        public static readonly LanguageCode Sinhala = new("Sinhala",
            "si",
            "sin",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SkoltSami = new("Skolt Sami",
            twoLetterCode: null,
            "sms",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Slovak = new("Slovak",
            "sk",
            "slo",
            "slk");

        public static readonly LanguageCode Slovenian = new("Slovenian",
            "sl",
            "slv",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sogdian = new("Sogdian",
            twoLetterCode: null,
            "sog",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Somali = new("Somali",
            "so",
            "som",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Soninke = new("Soninke",
            twoLetterCode: null,
            "snk",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sotho = new("Sotho, Southern",
            "st",
            "sot",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SouthNdebele = new("South Ndebele",
            "nr",
            "nbl",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SouthernAltai = new("Southern Altai",
            twoLetterCode: null,
            "alt",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SouthernSami = new("Southern Sami",
            twoLetterCode: null,
            "sma",
            threeLetterCodeAlt: null);

        // Spanish; Castilian
        public static readonly LanguageCode Spanish = new("Spanish",
            "es",
            "spa",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SrananTongo = new("Sranan Tongo",
            twoLetterCode: null,
            "srn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode StandardMoroccanTamazight = new("Standard Moroccan Tamazight",
            twoLetterCode: null,
            "zgh",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sukuma = new("Sukuma",
            twoLetterCode: null,
            "suk",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sumerian = new("Sumerian",
            twoLetterCode: null,
            "sux",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Sundanese = new("Sundanese",
            "su",
            "sun",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Susu = new("Susu",
            twoLetterCode: null,
            "sus",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Swahili = new("Susu",
            "sw",
            "swa",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Swati = new("Swatio",
            "ss",
            "ssw",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Swedish = new("Swedish",
            "sv",
            "swe",
            threeLetterCodeAlt: null);

        // Swiss German; Alemannic; Alsatian
        public static readonly LanguageCode SwissGerman = new("Swiss German",
            twoLetterCode: null,
            "gsw",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Syriac = new("Syriac",
            twoLetterCode: null,
            "syr",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode ClassicalSyriac = new("Syriac, Classical",
            twoLetterCode: null,
            "syc",
            threeLetterCodeAlt: null);

        #endregion

        #region T

        public static readonly LanguageCode Tagalog = new("Tagalog",
            "tl",
            "tgl",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tahitian = new("Tahitian",
            "ty",
            "tah",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tajik = new("Tajik",
            "tg",
            "tgk",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tamashek = new("Tamashek",
            twoLetterCode: null,
            "tmh",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tamil = new("Tamil",
            "ta",
            "tam",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tatar = new("Tatar",
            "tt",
            "tat",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Telugu = new("Telugu",
            "te",
            "tel",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tereno = new("Tereno",
            twoLetterCode: null,
            "ter",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tetum = new("Tetum",
            twoLetterCode: null,
            "tet",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Thai = new("Thai",
            "th",
            "tha",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tibetan = new("Tibetan",
            "bo",
            "tib",
            "bod");

        public static readonly LanguageCode Tigre = new("Tigre",
            twoLetterCode: null,
            "tig",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tigrinya = new("Tigrinya",
            "ti",
            "tir",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Timne = new("Timne",
            twoLetterCode: null,
            "tem",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tiv = new("Tiv",
            twoLetterCode: null,
            "tiv",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tlingit = new("Tlingit",
            twoLetterCode: null,
            "tli",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode TokPisin = new("Tok Pisin",
            twoLetterCode: null,
            "tpi",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tokelau = new("Tokelau",
            twoLetterCode: null,
            "tkl",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode TongaNyasa = new("Tonga (Nyasa)",
            twoLetterCode: null,
            "tog",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode TongaIslands = new("Tonga (Tonga Islands)",
            "to",
            "ton",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tsimshian = new("Tsimshian",
            twoLetterCode: null,
            "tsi",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tsonga = new("Tsonga",
            "ts",
            "tso",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tswana = new("Tswana",
            "tn",
            "tsn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tumbuka = new("Tumbuka",
            twoLetterCode: null,
            "tum",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Turkish = new("Turkish",
            "tr",
            "tur",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode OttomanTurkish = new("Turkish, Ottoman (1500-1928)",
            twoLetterCode: null,
            "ota",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Turkmen = new("Turkmen",
            "tk",
            "tuk",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tuvalu = new("Tuvalu",
            twoLetterCode: null,
            "tvl",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Tuvinian = new("Tuvinian",
            twoLetterCode: null,
            "tyv",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Twi = new("Twi",
            "tw",
            "twi",
            threeLetterCodeAlt: null);

        #endregion

        #region U

        public static readonly LanguageCode Udmurt = new("Udmurt",
            twoLetterCode: null,
            "udm",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Ugaritic = new("Ugaritic",
            twoLetterCode: null,
            "uga",
            threeLetterCodeAlt: null);

        // Uighur; Uyghur
        public static readonly LanguageCode Uighur = new("Uighur",
            "ug",
            "uig",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Ukrainian = new("Ukrainian",
            "uk",
            "ukr",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Umbundu = new("Umbundu",
            twoLetterCode: null,
            "umb",
            threeLetterCodeAlt: null);

        // Commented out to avoid confusion
        // public static readonly LanguageCode Undetermined = new("Undetermined",
        //     twoLetterCode: null,
        //     "und",
        //     threeLetterCodeAlt: null);

        public static readonly LanguageCode UpperSorbian = new("Upper Sorbian",
            twoLetterCode: null,
            "hsb",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Urdu = new("Urdu",
            "ur",
            "urd",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Uzbek = new("Uzbek",
            "uz",
            "uzb",
            threeLetterCodeAlt: null);

        #endregion

        #region V

        public static readonly LanguageCode Vai = new("Vai",
            twoLetterCode: null,
            "vai",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Venda = new("Venda",
            "ve",
            "ven",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Vietnamese = new("Vietnamese",
            "vi",
            "vie",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Volapuk = new("Volapük",
            "vo",
            "vol",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Votic = new("Votic",
            twoLetterCode: null,
            "vot",
            threeLetterCodeAlt: null);

        #endregion

        #region W

        public static readonly LanguageCode Walloon = new("Walloon",
            "wa",
            "wln",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Waray = new("Waray",
            twoLetterCode: null,
            "war",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Washo = new("Washo",
            twoLetterCode: null,
            "was",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Welsh = new("Welsh",
            "cy",
            "wel",
            "cym");

        public static readonly LanguageCode WesternFrisian = new("Western Frisian",
            "fy",
            "fry",
            threeLetterCodeAlt: null);

        // Wolaitta; Wolaytta
        public static readonly LanguageCode Wolaitta = new("Wolaitta",
            twoLetterCode: null,
            "wal",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Wolof = new("Wolof",
            "wo",
            "wol",
            threeLetterCodeAlt: null);

        #endregion

        #region X

        public static readonly LanguageCode Xhosa = new("Xhosa",
            "xh",
            "xho",
            threeLetterCodeAlt: null);

        #endregion

        #region Y

        public static readonly LanguageCode Yakut = new("Yakut",
            twoLetterCode: null,
            "sah",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Yao = new("Yao",
            twoLetterCode: null,
            "yao",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Yapese = new("Yapese",
            twoLetterCode: null,
            "yap",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Yiddish = new("Yiddish",
            "yi",
            "yid",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Yoruba = new("Yoruba",
            "yo",
            "yor",
            threeLetterCodeAlt: null);

        #endregion

        #region Z

        public static readonly LanguageCode Zapotec = new("Zapotec",
            twoLetterCode: null,
            "zap",
            threeLetterCodeAlt: null);

        // Zaza; Dimili; Dimli; Kirdki; Kirmanjki; Zazaki
        public static readonly LanguageCode Zaza = new("Zaza",
            twoLetterCode: null,
            "zza",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Zenaga = new("Zenaga",
            twoLetterCode: null,
            "zen",
            threeLetterCodeAlt: null);

        // Zhuang; Chuang
        public static readonly LanguageCode Zhuang = new("Zhuang",
            "za",
            "zha",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Zulu = new("Zulu",
            "zu",
            "zul",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode Zuni = new("Zuni",
            twoLetterCode: null,
            "zun",
            threeLetterCodeAlt: null);

        #endregion

        #region Language Families

        /*
        public static readonly LanguageCode AfroAsiaticLanguages = new("Afro-Asiatic languages",
            twoLetterCode: null,
            "afa",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode AlgonquianLanguages = new("Algonquian languages",
            twoLetterCode: null,
            "alg",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode AltaicLanguages = new("Altaic languages",
            twoLetterCode: null,
            "tut",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode ApacheLanguages = new("Apache languages",
            twoLetterCode: null,
            "apa",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode ArtificialLanguages = new("Artificial languages",
            twoLetterCode: null,
            "art",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode AthapascanLanguages = new("Athapascan languages",
            twoLetterCode: null,
            "ath",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode AustralianLanguages = new("Australian languages",
            twoLetterCode: null,
            "aus",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode AustronesianLanguages = new("Austronesian languages",
            twoLetterCode: null,
            "map",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode BalticLanguages = new("Baltic languages",
            twoLetterCode: null,
            "bat",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode BamilekeLanguages = new("Bamileke languages",
            twoLetterCode: null,
            "bai",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode BandaLanguages = new("Banda languages",
            twoLetterCode: null,
            "bad",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode BantuLanguages = new("Bantu languages",
            twoLetterCode: null,
            "bnt",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode BatakLanguages = new("Batak languages",
            twoLetterCode: null,
            "btk",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode BerberLanguages = new("Berber languages",
            twoLetterCode: null,
            "ber",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode BihariLanguages = new("Bihari languages", TwoLetterCode = "bh",
            twoLetterCode: null,
            "bih",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode CaucasianLanguages = new("Caucasian languages",
            twoLetterCode: null,
            "cau",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode CelticLanguages = new("Celtic languages",
            twoLetterCode: null,
            "cel",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode CentralAmericanIndianLanguages = new("Central American Indian languages",
            twoLetterCode: null,
            "cai",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode ChamicLanguages = new("Chamic languages",
            twoLetterCode: null,
            "cmc",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode CushiticLanguages = new("Cushitic languages",
            twoLetterCode: null,
            "cus",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode DravidianLanguages = new("Dravidian languages",
            twoLetterCode: null,
            "dra",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode FinnoUgrianLanguages = new("Finno-Ugrian languages",
            twoLetterCode: null,
            "fiu",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode GermanicLanguages = new("Germanic languages",
            twoLetterCode: null,
            "gem",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode HimachaliLanguages = new("Himachali languages; Western Pahari languages",
            twoLetterCode: null,
            "him",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode IjoLanguages = new("Ijo languages",
            twoLetterCode: null,
            "ijo",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode IndicLanguages = new("Indic languages",
            twoLetterCode: null,
            "inc",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode IndoEuropeanLanguages = new("Indo-European languages",
            twoLetterCode: null,
            "ine",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode IranianLanguages = new("Iranian languages",
            twoLetterCode: null,
            "ira",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode IroquoianLanguages = new("Iroquoian languages",
            twoLetterCode: null,
            "iro",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode KarenLanguages = new("Karen languages",
            twoLetterCode: null,
            "kar",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode KhoisanLanguages = new("Khoisan languages",
            twoLetterCode: null,
            "khi",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode KruLanguages = new("Kru languages",
            twoLetterCode: null,
            "kro",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode LandDayakLanguages = new("Land Dayak languages",
            twoLetterCode: null,
            "day",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode ManoboLanguages = new("Manobo languages",
            twoLetterCode: null,
            "mno",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode MayanLanguages = new("Mayan languages",
            twoLetterCode: null,
            "myn",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode MonKhmerLanguages = new("Mon-Khmer languages",
            twoLetterCode: null,
            "mkh",
            threeLetterCodeAlt: null);

        // Commented out to avoid confusion
        //public static readonly LanguageCode //MultipleLanguages = new("Multiple languages",
            twoLetterCode: null,
            "mul",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode MundaLanguages = new("Munda languages",
            twoLetterCode: null,
            "mun",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode NahuatlLanguages = new("Nahuatl languages",
            twoLetterCode: null,
            "nah",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode NigerKordofanianLanguages = new("Niger-Kordofanian languages",
            twoLetterCode: null,
            "nic",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode NiloSaharanLanguages = new("Nilo-Saharan languages",
            twoLetterCode: null,
            "ssa",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode NorthAmericanIndianLanguages = new("North American Indian languages",
            twoLetterCode: null,
            "nai",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode NubianLanguages = new("Nubian languages",
            twoLetterCode: null,
            "nub",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode OtomianLanguages = new("Otomian languages",
            twoLetterCode: null,
            "oto",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode PapuanLanguages = new("Papuan languages",
            twoLetterCode: null,
            "paa",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode PhilippineLanguages = new("Philippine languages",
            twoLetterCode: null,
            "phi",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode PrakritLanguages = new("Prakrit languages",
            twoLetterCode: null,
            "pra",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode RomanceLanguages = new("Romance languages",
            twoLetterCode: null,
            "roa",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SalishanLanguages = new("Salishan languages",
            twoLetterCode: null,
            "sal",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SamiLanguages = new("Sami languages",
            twoLetterCode: null,
            "smi",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SemiticLanguages = new("Semitic languages",
            twoLetterCode: null,
            "sem",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SinoTibetanLanguages = new("Sino-Tibetan languages",
            twoLetterCode: null,
            "sit",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SiouanLanguages = new("Siouan languages",
            twoLetterCode: null,
            "sio",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SlavicLanguages = new("Slavic languages",
            twoLetterCode: null,
            "sla",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SonghaiLanguages = new("Songhai languages",
            twoLetterCode: null,
            "son",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SorbianLanguages = new("Sorbian languages",
            twoLetterCode: null,
            "wen",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode SouthAmericanIndianLanguages = new("South American Indian languages",
            twoLetterCode: null,
            "sai",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode TaiLanguages = new("Tai languages",
            twoLetterCode: null,
            "tai",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode TupiLanguages = new("Tupi languages",
            twoLetterCode: null,
            "tup",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode UncodedLanguages = new("Uncoded languages",
            twoLetterCode: null,
            "mis",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode WakashanLanguages = new("Wakashan languages",
            twoLetterCode: null,
            "wak",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode YupikLanguages = new("Yupik languages",
            twoLetterCode: null,
            "ypk",
            threeLetterCodeAlt: null);

        public static readonly LanguageCode ZandeLanguages = new("Zande languages",
            twoLetterCode: null,
            "znd",
            threeLetterCodeAlt: null);
        */

        #endregion

        #endregion

        #region Static Collections

        /// <summary>
        /// All languages
        /// </summary>
        public static readonly LanguageCode[] AllLanguages =
        [
            #region A

            Abkhazian,
            Achinese,
            Acoli,
            Adangme,
            Adyghe,
            Afar,
            Afrihili,
            Afrikaans,
            Ainu,
            Akan,
            Akkadian,
            Albanian,
            Aleut,
            Amharic,
            Angika,
            Arabic,
            Aragonese,
            Aramaic,
            Armenian,
            Arapaho,
            Aromanian,
            Arawak,
            Assamese,
            Asturian,
            Athapascan,
            Avaric,
            Avestan,
            Awadhi,
            Aymara,
            Azerbaijani,

            #endregion

            #region B

            Balinese,
            Baluchi,
            Bambara,
            Basa,
            Bashkir,
            Basque,
            Beja,
            Belarusian,
            Bemba,
            Bengali,
            Bhojpuri,
            Bikol,
            Bini,
            Bislama,
            Blin,
            Blissymbols,
            Bosnian,
            Braj,
            Breton,
            Buginese,
            Bulgarian,
            Buriat,
            Burmese,

            #endregion

            #region C

            Caddo,
            Catalan,
            Cebuano,
            CentralKhmer,
            Chagatai,
            Chamorro,
            Chechen,
            Cherokee,
            Cheyenne,
            Chibcha,
            Chichewa,
            Chinese,
            ChinookJargon,
            Chipewyan,
            Choctaw,
            ChurchSlavic,
            Chuukese,
            Chuvash,
            ClassicalNewari,
            Coptic,
            Cornish,
            Corsican,
            Cree,
            Creek,
            CreolesAndPidgins,
            EnglishCreole,
            FrenchCreole,
            PortugueseCreole,
            CrimeanTatar,
            Croatian,
            Czech,

            #endregion

            #region D

            Dakota,
            Danish,
            Dargwa,
            Delaware,
            Dinka,
            Divehi,
            Dogrib,
            Dogri,
            Duala,
            Dutch,
            MiddleDutch,
            Dyula,
            Dzongkha,

            #endregion

            #region E

            EasternFrisian,
            Efik,
            AncientEgyptian,
            Ekajuk,
            Elamite,
            English,
            OldEnglish,
            MiddleEnglish,
            Erzya,
            Esperanto,
            Estonian,
            Ewe,
            Ewondo,

            #endregion

            #region F

            Fang,
            Faroese,
            Fanti,
            Fijian,
            Filipino,
            Finnish,
            Fon,
            French,
            MiddleFrench,
            OldFrench,
            Friulian,
            Fulah,

            #endregion

            #region G

            Ga,
            Gaelic,
            GalibiCarib,
            Galician,
            Ganda,
            Gayo,
            Gbaya,
            Geez,
            Georgian,
            German,
            MiddleHighGerman,
            OldHighGerman,
            Gilbertese,
            Gondi,
            Gorontalo,
            Gothic,
            Grebo,
            Greek,
            AncientGreek,
            Guarani,
            Gujarati,
            Gwichin,

            #endregion

            #region H

            Haida,
            Haitian,
            Hausa,
            Hawaiian,
            Hebrew,
            Herero,
            Hiligaynon,
            Hindi,
            HiriMotu,
            Hittite,
            Hmong,
            Hungarian,
            Hupa,

            #endregion

            #region I

            Iban,
            Icelandic,
            Ido,
            Igbo,
            Iloko,
            InariSami,
            Indonesian,
            Ingush,
            Interlingua,
            Interlingue,
            Inuktitut,
            Inupiaq,
            Irish,
            MiddleIrish,
            OldIrish,
            Italian,

            #endregion

            #region J

            Japanese,
            Javanese,
            JudeoArabic,
            JudeoPersian,

            #endregion

            #region K

            Kabardian,
            Kabyle,
            Kachin,
            Kalaallisut,
            Kalmyk,
            Kamba,
            Kannada,
            Kanuri,
            KarachayBalkar,
            KaraKalpak,
            Karelian,
            Kashmiri,
            Kashubian,
            Kawi,
            Kazakh,
            Khasi,
            Khotanese,
            Kikuyu,
            Kimbundu,
            Kinyarwanda,
            Kirghiz,
            Klingon,
            Komi,
            Kongo,
            Konkani,
            Korean,
            Kosraean,
            Kpelle,
            Kuanyama,
            Kumyk,
            Kurdish,
            Kurukh,
            Kutenai,

            #endregion

            #region L

            Ladino,
            Lahnda,
            Lamba,
            Lao,
            Latin,
            Latvian,
            Lezghian,
            Limburgan,
            Lingala,
            Lithuanian,
            Lojban,
            LowGerman,
            LowerSorbian,
            Lozi,
            LubaKatanga,
            LubaLulua,
            Luiseno,
            LuleSami,
            Lunda,
            Luo,
            Lushai,
            Luxembourgish,

            #endregion

            #region M

            Macedonian,
            Madurese,
            Magahi,
            Maithili,
            Makasar,
            Malagasy,
            Malay,
            Malayalam,
            Maltese,
            Manchu,
            Mandar,
            Mandingo,
            Manipuri,
            Manx,
            Maori,
            Mapudungun,
            Marathi,
            Mari,
            Marshallese,
            Marwari,
            Masai,
            Mende,
            Mikmaq,
            Minangkabau,
            Mirandese,
            Mohawk,
            Moksha,
            Mongo,
            Mongolian,
            Montenegrin,
            Mossi,

            #endregion

            #region N

            NKo,
            Nauru,
            Navajo,
            Ndonga,
            Neapolitan,
            NepalBhasa,
            Nepali,
            Nias,
            Niuean,
            Nogai,
            OldNorse,
            NorthNdebele,
            NorthernFrisian,
            NorthernSami,
            Norwegian,
            NorwegianBokmal,
            NorwegianNynorsk,
            Nyamwezi,
            Nyankole,
            Nyoro,
            Nzima,

            #endregion

            #region O

            Occitan,
            OldOccitan,
            Ojibwa,
            Oriya,
            Oromo,
            Osage,
            Ossetian,

            #endregion

            #region P

            Pahlavi,
            Palauan,
            Pali,
            Pampanga,
            Pangasinan,
            Panjabi,
            Papiamento,
            Pedi,
            Persian,
            OldPersian,
            Phoenician,
            Polish,
            Portuguese,
            Pushto,

            #endregion

            #region Q

            Quechua,

            #endregion

            #region R

            Rajasthani,
            Rapanui,
            Rarotongan,
            Romanian,
            Romansh,
            Romany,
            Rundi,
            Russian,

            #endregion

            #region S

            SamaritanAramaic,
            Samoan,
            Sandawe,
            Sango,
            Sanskrit,
            Santali,
            Sardinian,
            Sasak,
            Scots,
            Selkup,
            Serbian,
            Serer,
            Shan,
            Shona,
            SichuanYi,
            Sicilian,
            Sidamo,
            SignLanguages,
            Siksika,
            Sindhi,
            Sinhala,
            SkoltSami,
            Slovak,
            Slovenian,
            Sogdian,
            Somali,
            Soninke,
            Sotho,
            SouthNdebele,
            SouthernAltai,
            SouthernSami,
            Spanish,
            SrananTongo,
            StandardMoroccanTamazight,
            Sukuma,
            Sumerian,
            Sundanese,
            Susu,
            Swahili,
            Swati,
            Swedish,
            SwissGerman,
            Syriac,
            ClassicalSyriac,

            #endregion

            #region T

            Tagalog,
            Tahitian,
            Tajik,
            Tamashek,
            Tamil,
            Tatar,
            Telugu,
            Tereno,
            Tetum,
            Thai,
            Tibetan,
            Tigre,
            Tigrinya,
            Timne,
            Tiv,
            Tlingit,
            TokPisin,
            Tokelau,
            TongaNyasa,
            TongaIslands,
            Tsimshian,
            Tsonga,
            Tswana,
            Tumbuka,
            Turkish,
            OttomanTurkish,
            Turkmen,
            Tuvalu,
            Tuvinian,
            Twi,

            #endregion

            #region U

            Udmurt,
            Ugaritic,
            Uighur,
            Ukrainian,
            Umbundu,
            UpperSorbian,
            Urdu,
            Uzbek,

            #endregion

            #region V

            Vai,
            Venda,
            Vietnamese,
            Volapuk,
            Votic,

            #endregion

            #region W

            Walloon,
            Waray,
            Washo,
            Welsh,
            WesternFrisian,
            Wolaitta,
            Wolof,

            #endregion

            #region X

            Xhosa,

            #endregion

            #region Y

            Yakut,
            Yao,
            Yapese,
            Yiddish,
            Yoruba,

            #endregion

            #region Z

            Zapotec,
            Zaza,
            Zenaga,
            Zhuang,
            Zulu,
            Zuni,

            #endregion
        ];

        #endregion
    }
}
