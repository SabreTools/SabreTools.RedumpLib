namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// Represents a single region code
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2</remarks>
    public sealed class RegionCode
    {
        #region Properties

        /// <summary>
        /// Human-readable name of the region
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// ISO_3166-1 Code
        /// </summary>
        public readonly string Code;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private RegionCode(string name, string code)
        {
            Name = name;
            Code = code;
        }

        #endregion

        #region Static Instances

        #region Aggregate Regions

        public static readonly RegionCode Asia = new("Asia", "xa");

        public static readonly RegionCode Europe = new("Europe", "eu");

        public static readonly RegionCode Export = new("Export", "xp");

        public static readonly RegionCode LatinAmerica = new("Latin America", "xl");

        public static readonly RegionCode Scandinavia = new("Scandinavia", "xs");

        public static readonly RegionCode World = new("World", "un");

        #endregion

        #region A

        public static readonly RegionCode Afghanistan = new("Afghanistan", "af");

        public static readonly RegionCode AlandIslands = new("Åland Islands", "ax");

        public static readonly RegionCode Albania = new("Albania", "al");

        public static readonly RegionCode Algeria = new("Algeria", "dz");

        public static readonly RegionCode AmericanSamoa = new("American Samoa", "as");

        public static readonly RegionCode Andorra = new("Andorra", "ad");

        public static readonly RegionCode Angola = new("Angola", "ao");

        public static readonly RegionCode Anguilla = new("Anguilla", "ai");

        public static readonly RegionCode Antarctica = new("Antarctica", "aq");

        public static readonly RegionCode AntiguaAndBarbuda = new("Antigua and Barbuda", "ag");

        public static readonly RegionCode Argentina = new("Argentina", "ar");

        public static readonly RegionCode Armenia = new("Armenia", "am");

        public static readonly RegionCode Aruba = new("Aruba", "aw");

        public static readonly RegionCode AscensionIsland = new("Ascension Island", "ac");

        public static readonly RegionCode Australia = new("Australia", "au");

        public static readonly RegionCode Austria = new("Austria", "at");

        public static readonly RegionCode Azerbaijan = new("Azerbaijan", "az");

        #endregion

        #region B

        public static readonly RegionCode Bahamas = new("Bahamas", "bs");

        public static readonly RegionCode Bahrain = new("Bahrain", "bh");

        public static readonly RegionCode Bangladesh = new("Bangladesh", "bd");

        public static readonly RegionCode Barbados = new("Barbados", "bb");

        public static readonly RegionCode Belarus = new("Belarus", "by");

        public static readonly RegionCode Belgium = new("Belgium", "be");

        public static readonly RegionCode Belize = new("Belize", "bz");

        public static readonly RegionCode Benin = new("Benin", "bj");

        public static readonly RegionCode Bermuda = new("Bermuda", "bm");

        public static readonly RegionCode Bhutan = new("Bhutan", "bt");

        public static readonly RegionCode Bolivia = new("Bolivia", "bo");

        public static readonly RegionCode Bonaire = new("Bonaire, Sint Eustatius and Saba", "bq");

        public static readonly RegionCode BosniaAndHerzegovina = new("Bosnia and Herzegovina", "ba");

        public static readonly RegionCode Botswana = new("Botswana", "bw");

        public static readonly RegionCode BouvetIsland = new("Bouvet Island", "bv");

        public static readonly RegionCode Brazil = new("Brazil", "br");

        public static readonly RegionCode BritishIndianOceanTerritory = new("British Indian Ocean Territory", "io");

        public static readonly RegionCode BruneiDarussalam = new("Brunei Darussalam", "bn");

        public static readonly RegionCode Bulgaria = new("Bulgaria", "bg");

        public static readonly RegionCode BurkinaFaso = new("Burkina Faso", "bf");

        public static readonly RegionCode Burundi = new("Burundi", "bi");

        #endregion

        #region C

        public static readonly RegionCode CaboVerde = new("Cabo Verde", "cv");

        public static readonly RegionCode Cambodia = new("Cambodia", "kh");

        public static readonly RegionCode Cameroon = new("Cameroon", "cm");

        public static readonly RegionCode Canada = new("Canada", "ca");

        public static readonly RegionCode CanaryIslands = new("Canary Islands", "ic");

        public static readonly RegionCode CaymanIslands = new("Cayman Islands", "ky");

        public static readonly RegionCode CentralAfricanRepublic = new("Central African Republic", "cf");

        public static readonly RegionCode CeutaMelilla = new("Ceuta, Melilla", "ea");

        public static readonly RegionCode Chad = new("Chad", "td");

        public static readonly RegionCode Chile = new("Chile", "cl");

        public static readonly RegionCode China = new("China", "cn");

        public static readonly RegionCode ChristmasIsland = new("Christmas Island", "cx");

        public static readonly RegionCode ClippertonIsland = new("Clipperton Island", "cp");

        public static readonly RegionCode CocosIslands = new("Cocos (Keeling) Islands", "cc");

        public static readonly RegionCode Colombia = new("Colombia", "co");

        public static readonly RegionCode Comoros = new("Comoros", "km");

        public static readonly RegionCode Congo = new("Congo", "cg");

        public static readonly RegionCode CookIslands = new("Cook Islands", "ck");

        public static readonly RegionCode CostaRica = new("Costa Rica", "cr");

        public static readonly RegionCode CoteDIvoire = new("Côte d'Ivoire", "ci");

        public static readonly RegionCode Croatia = new("Croatia", "hr");

        public static readonly RegionCode Cuba = new("Cuba", "cu");

        public static readonly RegionCode Curacao = new("Curaçao", "cw");

        public static readonly RegionCode Cyprus = new("Cyprus", "cy");

        public static readonly RegionCode Czechia = new("Czechia", "cz");

        public static readonly RegionCode Czechoslovakia = new("Czechoslovakia", "cs");

        #endregion

        #region D

        // Zaire was "Zr"
        public static readonly RegionCode DemocraticRepublicOfTheCongo = new("Democratic Republic of the Congo (Zaire)", "cd");

        public static readonly RegionCode Denmark = new("Denmark", "dk");

        public static readonly RegionCode DiegoGarcia = new("Diego Garcia", "dg");

        public static readonly RegionCode Djibouti = new("Djibouti", "dj");

        public static readonly RegionCode Dominica = new("Dominica", "dm");

        public static readonly RegionCode DominicanRepublic = new("Dominican Republic", "do");

        #endregion

        #region E

        public static readonly RegionCode Ecuador = new("Ecuador", "ec");

        public static readonly RegionCode Egypt = new("Egypt", "eg");

        public static readonly RegionCode ElSalvador = new("El Salvador", "sv");

        public static readonly RegionCode EquatorialGuinea = new("Equatorial Guinea", "gq");

        public static readonly RegionCode Eritrea = new("Eritrea", "er");

        public static readonly RegionCode Estonia = new("Estonia", "ee");

        public static readonly RegionCode Eswatini = new("Eswatini", "sz");

        public static readonly RegionCode Ethiopia = new("Ethiopia", "et");

        // Commented out to avoid confusion
        //[HumanReadable(LongName = "European Union", ShortName="eu")]
        //EuropeanUnion,

        // Commented out to avoid confusion
        //[HumanReadable(LongName = "Eurozone", ShortName="ez")]
        //Eurozone,

        #endregion

        #region F

        public static readonly RegionCode FalklandIslands = new("Falkland Islands (Malvinas)", "fk");

        public static readonly RegionCode FaroeIslands = new("Faroe Islands", "fo");

        public static readonly RegionCode FederatedStatesOfMicronesia = new("Federated States of Micronesia", "fm");

        public static readonly RegionCode Fiji = new("Fiji", "fj");

        // Formerly "Sf"
        public static readonly RegionCode Finland = new("Finland", "fi");

        public static readonly RegionCode France = new("France", "fr");

        // Commented out to avoid confusion
        //[HumanReadable(LongName = "France, Metropolitan", ShortName="fx")]
        //FranceMetropolitan,

        public static readonly RegionCode FrenchGuiana = new("French Guiana", "gf");

        public static readonly RegionCode FrenchPolynesia = new("French Polynesia", "pf");

        public static readonly RegionCode FrenchSouthernTerritories = new("French Southern Territories", "tf");

        #endregion

        #region G

        public static readonly RegionCode Gabon = new("Gabon", "ga");

        public static readonly RegionCode Gambia = new("Gambia", "gm");

        public static readonly RegionCode Georgia = new("Georgia", "ge");

        public static readonly RegionCode Germany = new("Germany", "de");

        public static readonly RegionCode Ghana = new("Ghana", "gh");

        public static readonly RegionCode Gibraltar = new("Gibraltar", "gi");

        public static readonly RegionCode Greece = new("Greece", "gr");

        public static readonly RegionCode Greenland = new("Greenland", "gl");

        public static readonly RegionCode Grenada = new("Grenada", "gd");

        public static readonly RegionCode Guadeloupe = new("Guadeloupe", "gp");

        public static readonly RegionCode Guam = new("Guam", "gu");

        public static readonly RegionCode Guatemala = new("Guatemala", "gt");

        public static readonly RegionCode Guernsey = new("Guernsey", "gg");

        public static readonly RegionCode Guinea = new("Guinea", "gn");

        public static readonly RegionCode GuineaBissau = new("Guinea-Bissau", "gw");

        public static readonly RegionCode Guyana = new("Guyana", "gy");

        #endregion

        #region H

        public static readonly RegionCode Haiti = new("Haiti", "ht");

        public static readonly RegionCode HeardIslandAndMcDonaldIslands = new("Heard Island and McDonald Islands", "hm");

        public static readonly RegionCode HolySee = new("Holy See (Vatican City)", "va");

        public static readonly RegionCode Honduras = new("Honduras", "hn");

        public static readonly RegionCode HongKong = new("Hong Kong", "hk");

        public static readonly RegionCode Hungary = new("Hungary", "hu");

        #endregion

        #region I

        public static readonly RegionCode Iceland = new("Iceland", "is");

        public static readonly RegionCode India = new("India", "in");

        public static readonly RegionCode Indonesia = new("Indonesia", "id");

        public static readonly RegionCode Iran = new("Iran", "ir");

        public static readonly RegionCode Iraq = new("Iraq", "iq");

        public static readonly RegionCode Ireland = new("Ireland", "ie");

        public static readonly RegionCode IslandOfSark = new("Island of Sark", "cq");

        public static readonly RegionCode IsleOfMan = new("Isle of Man", "im");

        public static readonly RegionCode Israel = new("Israel", "il");

        public static readonly RegionCode Italy = new("Italy", "it");

        #endregion

        #region J

        public static readonly RegionCode Jamaica = new("Jamaica", "jm");

        public static readonly RegionCode Japan = new("Japan", "jp");

        public static readonly RegionCode Jersey = new("Jersey", "je");

        public static readonly RegionCode Jordan = new("Jordan", "jo");

        #endregion

        #region K

        public static readonly RegionCode Kazakhstan = new("Kazakhstan", "kz");

        public static readonly RegionCode Kenya = new("Kenya", "ke");

        public static readonly RegionCode Kiribati = new("Kiribati", "ki");

        public static readonly RegionCode NorthKorea = new("Korea (Democratic People's Republic of Korea)", "kp");

        public static readonly RegionCode SouthKorea = new("Korea (Republic of Korea)", "kr");

        public static readonly RegionCode Kuwait = new("Kuwait", "kw");

        public static readonly RegionCode Kyrgyzstan = new("Kyrgyzstan", "kg");

        #endregion

        #region L

        public static readonly RegionCode Laos = new("(Laos) Lao People's Democratic Republic", "la");

        public static readonly RegionCode Latvia = new("Latvia", "lv");

        public static readonly RegionCode Lebanon = new("Lebanon", "lb");

        public static readonly RegionCode Lesotho = new("Lesotho", "ls");

        public static readonly RegionCode Liberia = new("Liberia", "lr");

        public static readonly RegionCode Libya = new("Libya", "ly");

        public static readonly RegionCode Liechtenstein = new("Liechtenstein", "li");

        public static readonly RegionCode Lithuania = new("Lithuania", "lt");

        public static readonly RegionCode Luxembourg = new("Luxembourg", "lu");

        #endregion

        #region M

        public static readonly RegionCode Macao = new("Macao", "mo");

        public static readonly RegionCode Madagascar = new("Madagascar", "mg");

        public static readonly RegionCode Malawi = new("Malawi", "mw");

        public static readonly RegionCode Malaysia = new("Malaysia", "my");

        public static readonly RegionCode Maldives = new("Maldives", "mv");

        public static readonly RegionCode Mali = new("Mali", "ml");

        public static readonly RegionCode Malta = new("Malta", "mt");

        public static readonly RegionCode MarshallIslands = new("Marshall Islands", "mh");

        public static readonly RegionCode Martinique = new("Martinique", "mq");

        public static readonly RegionCode Mauritania = new("Mauritania", "mr");

        public static readonly RegionCode Mauritius = new("Mauritius", "mu");

        public static readonly RegionCode Mayotte = new("Mayotte", "yt");

        public static readonly RegionCode Mexico = new("Mexico", "mx");

        public static readonly RegionCode Monaco = new("Monaco", "mc");

        public static readonly RegionCode Mongolia = new("Mongolia", "mn");

        public static readonly RegionCode Montenegro = new("Montenegro", "me");

        public static readonly RegionCode Montserrat = new("Montserrat", "ms");

        public static readonly RegionCode Morocco = new("Morocco", "ma");

        public static readonly RegionCode Mozambique = new("Mozambique", "mz");

        // Burma was "Bu"
        public static readonly RegionCode Myanmar = new("Myanmar (Burma)", "mm");

        #endregion

        #region N

        public static readonly RegionCode Namibia = new("Namibia", "na");

        public static readonly RegionCode Nauru = new("Nauru", "nr");

        public static readonly RegionCode Nepal = new("Nepal", "np");

        public static readonly RegionCode Netherlands = new("Netherlands", "nl");

        public static readonly RegionCode NetherlandsAntilles = new("Netherlands Antilles", "an");

        // Commented out to avoid confusion
        //[HumanReadable(LongName = "Neutral Zone", ShortName="nt")]
        //NeutralZone,

        public static readonly RegionCode NewCaledonia = new("New Caledonia", "nc");

        public static readonly RegionCode NewZealand = new("New Zealand", "nz");

        public static readonly RegionCode Nicaragua = new("Nicaragua", "ni");

        public static readonly RegionCode Niger = new("Niger", "ne");

        public static readonly RegionCode Nigeria = new("Nigeria", "ng");

        public static readonly RegionCode Niue = new("Niue", "nu");

        public static readonly RegionCode NorfolkIsland = new("Norfolk Island", "nf");

        public static readonly RegionCode NorthMacedonia = new("North Macedonia", "mk");

        public static readonly RegionCode NorthernMarianaIslands = new("Northern Mariana Islands", "mp");

        public static readonly RegionCode Norway = new("Norway", "no");

        #endregion

        #region O

        public static readonly RegionCode Oman = new("Oman", "om");

        #endregion

        #region P

        public static readonly RegionCode Pakistan = new("Pakistan", "pk");

        public static readonly RegionCode Palau = new("Palau", "pw");

        public static readonly RegionCode Panama = new("Panama", "pa");

        public static readonly RegionCode PapuaNewGuinea = new("Papua New Guinea", "pg");

        public static readonly RegionCode Paraguay = new("Paraguay", "py");

        public static readonly RegionCode Peru = new("Peru", "pe");

        public static readonly RegionCode Philippines = new("Philippines", "ph");

        public static readonly RegionCode Pitcairn = new("Pitcairn", "pn");

        public static readonly RegionCode Poland = new("Poland", "pl");

        public static readonly RegionCode Portugal = new("Portugal", "pt");

        public static readonly RegionCode PuertoRico = new("Puerto Rico", "pr");

        #endregion

        #region Q

        public static readonly RegionCode Qatar = new("Qatar", "qa");

        #endregion

        #region R

        public static readonly RegionCode RepublicOfMoldova = new("Republic of Moldova", "md");

        public static readonly RegionCode Reunion = new("Réunion", "re");

        public static readonly RegionCode Romania = new("Romania", "ro");

        public static readonly RegionCode RussianFederation = new("Russian Federation", "ru");

        public static readonly RegionCode Rwanda = new("Rwanda", "rw");

        #endregion

        #region S

        public static readonly RegionCode SaintBarthelemy = new("Saint Barthélemy", "bl");

        public static readonly RegionCode SaintHelena = new("Saint Helena, Ascension and Tristan da Cunha", "sh");

        public static readonly RegionCode SaintKittsAndNevis = new("Saint Kitts and Nevis", "kn");

        public static readonly RegionCode SaintLucia = new("Saint Lucia", "lc");

        public static readonly RegionCode SaintMartin = new("Saint Martin", "mf");

        public static readonly RegionCode SaintPierreAndMiquelon = new("Saint Pierre and Miquelon", "pm");

        public static readonly RegionCode SaintVincentAndTheGrenadines = new("Saint Vincent and the Grenadines", "vc");

        public static readonly RegionCode Samoa = new("Samoa", "ws");

        public static readonly RegionCode SanMarino = new("San Marino", "sm");

        public static readonly RegionCode SaoTomeAndPrincipe = new("Sao Tome and Principe", "st");

        public static readonly RegionCode SaudiArabia = new("Saudi Arabia", "sa");

        public static readonly RegionCode Senegal = new("Senegal", "sn");

        public static readonly RegionCode Serbia = new("Serbia", "rs");

        public static readonly RegionCode Seychelles = new("Seychelles", "sc");

        public static readonly RegionCode SierraLeone = new("Sierra Leone", "sl");

        public static readonly RegionCode Singapore = new("Singapore", "sg");

        public static readonly RegionCode SintMaarten = new("Sint Maarten", "sx");

        public static readonly RegionCode Slovakia = new("Slovakia", "sk");

        public static readonly RegionCode Slovenia = new("Slovenia", "si");

        public static readonly RegionCode SolomonIslands = new("Solomon Islands", "sb");

        public static readonly RegionCode Somalia = new("Somalia", "so");

        public static readonly RegionCode SouthAfrica = new("South Africa", "za");

        public static readonly RegionCode SouthGeorgia = new("South Georgia and the South Sandwich Islands", "gs");

        public static readonly RegionCode SouthSudan = new("South Sudan", "ss");

        public static readonly RegionCode Spain = new("Spain", "es");

        public static readonly RegionCode SriLanka = new("Sri Lanka", "lk");

        public static readonly RegionCode StateOfPalestine = new("State of Palestine", "ps");

        public static readonly RegionCode Sudan = new("Sudan", "sd");

        public static readonly RegionCode Suriname = new("Suriname", "sr");

        public static readonly RegionCode SvalbardAndJanMayen = new("Svalbard and Jan Mayen", "sj");

        public static readonly RegionCode Sweden = new("Sweden", "se");

        public static readonly RegionCode Switzerland = new("Switzerland", "ch");

        public static readonly RegionCode SyrianArabRepublic = new("Syrian Arab Republic", "sy");

        #endregion

        #region T

        public static readonly RegionCode Taiwan = new("Taiwan", "tw");

        public static readonly RegionCode Tajikistan = new("Tajikistan", "tj");

        public static readonly RegionCode Thailand = new("Thailand", "th");

        // East Timor was "Tp"
        public static readonly RegionCode TimorLeste = new("Timor-Leste (East Timor)", "tl");

        public static readonly RegionCode Togo = new("Togo", "tg");

        public static readonly RegionCode Tokelau = new("Tokelau", "tk");

        public static readonly RegionCode Tonga = new("Tonga", "to");

        public static readonly RegionCode TrinidadAndTobago = new("Trinidad and Tobago", "tt");

        public static readonly RegionCode TristanDaCunha = new("Tristan da Cunha", "ta");

        public static readonly RegionCode Tunisia = new("Tunisia", "tn");

        public static readonly RegionCode Turkey = new("Turkey", "tr");

        public static readonly RegionCode Turkmenistan = new("Turkmenistan", "tm");

        public static readonly RegionCode TurksAndCaicosIslands = new("Turks and Caicos Islands", "tc");

        public static readonly RegionCode Tuvalu = new("Tuvalu", "tv");

        #endregion

        #region U

        public static readonly RegionCode Uganda = new("Uganda", "ug");

        // Should be both "Gb" and "Uk"
        // United Kingdom of Great Britain and Northern Ireland
        public static readonly RegionCode UnitedKingdom = new("UK", "gb");

        public static readonly RegionCode Ukraine = new("Ukraine", "ue");

        public static readonly RegionCode UnitedArabEmirates = new("United Arab Emirates", "ae");

        // Commented out to avoid confusion
        //[HumanReadable(LongName = "United Nations", ShortName="un")]
        //UnitedNations,

        public static readonly RegionCode UnitedRepublicOfTanzania = new("United Republic of Tanzania", "tz");

        public static readonly RegionCode UnitedStatesMinorOutlyingIslands = new("United States Minor Outlying Islands", "um");

        public static readonly RegionCode Uruguay = new("Uruguay", "uy");

        // United States of America
        public static readonly RegionCode UnitedStatesOfAmerica = new("USA", "us");

        public static readonly RegionCode USSR = new("USSR", "su");

        public static readonly RegionCode Uzbekistan = new("Uzbekistan", "uz");

        #endregion

        #region V

        public static readonly RegionCode Vanuatu = new("Vanuatu", "vu");

        public static readonly RegionCode Venezuela = new("Venezuela", "ve");

        public static readonly RegionCode VietNam = new("Viet Nam", "vn");

        public static readonly RegionCode BritishVirginIslands = new("Virgin Islands (British)", "vg");

        public static readonly RegionCode USVirginIslands = new("Virgin Islands (US)", "vi");

        #endregion

        #region W

        public static readonly RegionCode WallisAndFutuna = new("Wallis and Futuna", "wf");

        public static readonly RegionCode WesternSahara = new("Western Sahara", "eh");

        #endregion

        #region Y

        public static readonly RegionCode Yemen = new("Yemen", "ye");

        public static readonly RegionCode Yugoslavia = new("Yugoslavia", "yu");

        #endregion

        #region Z

        public static readonly RegionCode Zambia = new("Zambia", "zm");

        public static readonly RegionCode Zimbabwe = new("Zimbabwe", "zw");

        #endregion

        #endregion

        #region Static Collections

        /// <summary>
        /// All regions
        /// </summary>
        public static readonly RegionCode[] AllRegions =
        [
            #region Aggregate Regions

            Asia,
            Europe,
            Export,
            LatinAmerica,
            Scandinavia,
            World,

            #endregion

            #region A

            Afghanistan,
            AlandIslands,
            Albania,
            Algeria,
            AmericanSamoa,
            Andorra,
            Angola,
            Anguilla,
            Antarctica,
            AntiguaAndBarbuda,
            Argentina,
            Armenia,
            Aruba,
            AscensionIsland,
            Australia,
            Austria,
            Azerbaijan,

            #endregion

            #region B

            Bahamas,
            Bahrain,
            Bangladesh,
            Barbados,
            Belarus,
            Belgium,
            Belize,
            Benin,
            Bermuda,
            Bhutan,
            Bolivia,
            Bonaire,
            BosniaAndHerzegovina,
            Botswana,
            BouvetIsland,
            Brazil,
            BritishIndianOceanTerritory,
            BruneiDarussalam,
            Bulgaria,
            BurkinaFaso,
            Burundi,

            #endregion

            #region C

            CaboVerde,
            Cambodia,
            Cameroon,
            Canada,
            CanaryIslands,
            CaymanIslands,
            CentralAfricanRepublic,
            CeutaMelilla,
            Chad,
            Chile,
            China,
            ChristmasIsland,
            ClippertonIsland,
            CocosIslands,
            Colombia,
            Comoros,
            Congo,
            CookIslands,
            CostaRica,
            CoteDIvoire,
            Croatia,
            Cuba,
            Curacao,
            Cyprus,
            Czechia,
            Czechoslovakia,

            #endregion

            #region D

            DemocraticRepublicOfTheCongo,
            Denmark,
            DiegoGarcia,
            Djibouti,
            Dominica,
            DominicanRepublic,

            #endregion

            #region E

            Ecuador,
            Egypt,
            ElSalvador,
            EquatorialGuinea,
            Eritrea,
            Estonia,
            Eswatini,
            Ethiopia,

            #endregion

            #region F

            FalklandIslands,
            FaroeIslands,
            FederatedStatesOfMicronesia,
            Fiji,
            Finland,
            France,
            FrenchGuiana,
            FrenchPolynesia,
            FrenchSouthernTerritories,

            #endregion

            #region G

            Gabon,
            Gambia,
            Georgia,
            Germany,
            Ghana,
            Gibraltar,
            Greece,
            Greenland,
            Grenada,
            Guadeloupe,
            Guam,
            Guatemala,
            Guernsey,
            Guinea,
            GuineaBissau,
            Guyana,

            #endregion

            #region H

            Haiti,
            HeardIslandAndMcDonaldIslands,
            HolySee,
            Honduras,
            HongKong,
            Hungary,

            #endregion

            #region I

            Iceland,
            India,
            Indonesia,
            Iran,
            Iraq,
            Ireland,
            IslandOfSark,
            IsleOfMan,
            Israel,
            Italy,

            #endregion

            #region J

            Jamaica,
            Japan,
            Jersey,
            Jordan,

            #endregion

            #region K

            Kazakhstan,
            Kenya,
            Kiribati,
            NorthKorea,
            SouthKorea,
            Kuwait,
            Kyrgyzstan,

            #endregion

            #region L

            Laos,
            Latvia,
            Lebanon,
            Lesotho,
            Liberia,
            Libya,
            Liechtenstein,
            Lithuania,
            Luxembourg,

            #endregion

            #region M

            Macao,
            Madagascar,
            Malawi,
            Malaysia,
            Maldives,
            Mali,
            Malta,
            MarshallIslands,
            Martinique,
            Mauritania,
            Mauritius,
            Mayotte,
            Mexico,
            Monaco,
            Mongolia,
            Montenegro,
            Montserrat,
            Morocco,
            Mozambique,
            Myanmar,

            #endregion

            #region N

            Namibia,
            Nauru,
            Nepal,
            Netherlands,
            NetherlandsAntilles,
            NewCaledonia,
            NewZealand,
            Nicaragua,
            Niger,
            Nigeria,
            Niue,
            NorfolkIsland,
            NorthMacedonia,
            NorthernMarianaIslands,
            Norway,

            #endregion

            #region O

            Oman,

            #endregion

            #region P

            Pakistan,
            Palau,
            Panama,
            PapuaNewGuinea,
            Paraguay,
            Peru,
            Philippines,
            Pitcairn,
            Poland,
            Portugal,
            PuertoRico,

            #endregion

            #region Q

            Qatar,

            #endregion

            #region R

            RepublicOfMoldova,
            Reunion,
            Romania,
            RussianFederation,
            Rwanda,

            #endregion

            #region S

            SaintBarthelemy,
            SaintHelena,
            SaintKittsAndNevis,
            SaintLucia,
            SaintMartin,
            SaintPierreAndMiquelon,
            SaintVincentAndTheGrenadines,
            Samoa,
            SanMarino,
            SaoTomeAndPrincipe,
            SaudiArabia,
            Senegal,
            Serbia,
            Seychelles,
            SierraLeone,
            Singapore,
            SintMaarten,
            Slovakia,
            Slovenia,
            SolomonIslands,
            Somalia,
            SouthAfrica,
            SouthGeorgia,
            SouthSudan,
            Spain,
            SriLanka,
            StateOfPalestine,
            Sudan,
            Suriname,
            SvalbardAndJanMayen,
            Sweden,
            Switzerland,
            SyrianArabRepublic,

            #endregion

            #region T

            Taiwan,
            Tajikistan,
            Thailand,
            TimorLeste,
            Togo,
            Tokelau,
            Tonga,
            TrinidadAndTobago,
            TristanDaCunha,
            Tunisia,
            Turkey,
            Turkmenistan,
            TurksAndCaicosIslands,
            Tuvalu,

            #endregion

            #region U

            Uganda,
            UnitedKingdom,
            Ukraine,
            UnitedArabEmirates,
            UnitedRepublicOfTanzania,
            UnitedStatesMinorOutlyingIslands,
            Uruguay,
            UnitedStatesOfAmerica,
            USSR,
            Uzbekistan,

            #endregion

            #region V

            Vanuatu,
            Venezuela,
            VietNam,
            BritishVirginIslands,
            USVirginIslands,

            #endregion

            #region W

            WallisAndFutuna,
            WesternSahara,

            #endregion

            #region Y

            Yemen,
            Yugoslavia,

            #endregion

            #region Z

            Zambia,
            Zimbabwe,

            #endregion
        ];

        #endregion
    }
}
