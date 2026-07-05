using System;
using System.Text;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// URL builder helper
    /// </summary>
    public static class UrlBuilder
    {
        #region Constants

        /// <summary>
        /// Redump site URL
        /// </summary>
        private const string SiteBaseUrl = "https://redump.info/";

        #region Top-Level Paths

        /// <summary>
        /// Path for about page
        /// </summary>
        private const string AboutPath = "about/";

        /// <summary>
        /// Path for BIOS datfile downloads
        /// </summary>
        private const string BiosPath = @"static/bios/{0}/";

        /// <summary>
        /// Path for cuesheet pack downloads
        /// </summary>
        private const string CuesPath = @"cues/{0}";

        /// <summary>
        /// Path for datfile downloads
        /// </summary>
        private const string DatfilePath = @"datfile/{0}";

        /// <summary>
        /// Path for individual disc pages
        /// </summary>
        private const string DiscPath = @"disc/{0}/";

        /// <summary>
        /// Path for multi-disc pages
        /// </summary>
        private const string DiscsPath = "discs/";

        /// <summary>
        /// Path for downloads page
        /// </summary>
        private const string DownloadsPath = "downloads/";

        /// <summary>
        /// Path for key pack downloads
        /// </summary>
        private const string KeysPath = @"keys/{0}";

        /// <summary>
        /// Path for discs queue
        /// </summary>
        private const string QueuePath = "queue/";

        /// <summary>
        /// Path for individual queue disc pages
        /// </summary>
        private const string QueueDiscPath = @"queue/{0}/";

        /// <summary>
        /// Path for SBI pack downloads
        /// </summary>
        private const string SbiPath = @"sbi/{0}";

        #endregion

        #region BIOS File Names

        /// <summary>
        /// Microsoft XBOX BIOS datfile filename
        /// </summary>
        public const string MicrosoftXboxBIOSFilename = "Microsoft%20-%20Xbox%20-%20BIOS%20Images%20%289%29%20%282026-06-16%29.dat";

        /// <summary>
        /// Nintendo GameCube BIOS datfile filename
        /// </summary>
        public const string NintendoGameCubeBIOSFilename = "Nintendo%20-%20GameCube%20-%20BIOS%20Images%20%2817%29%20%282026-06-16%29.dat";

        /// <summary>
        /// Sony PlayStation BIOS datfile filename
        /// </summary>
        public const string SonyPlayStationBIOSFilename = "Sony%20-%20PlayStation%20-%20BIOS%20Images%20%2824%29%20%282026-06-16%29.dat";

        /// <summary>
        /// Sony PlayStation 2 BIOS datfile filename
        /// </summary>
        public const string SonyPlayStation2BIOSFilename = "Sony%20-%20PlayStation%202%20-%20BIOS%20Datfile%20%28140%29%20%282026-06-16%29.dat";

        #endregion

        // TODO: Add filter statements for discs

        #endregion

        /// <summary>
        /// Build a /about/ path URL
        /// </summary>
        public static string BuildAboutUrl()
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(AboutPath);

            return sb.ToString();
        }

        /// <summary>
        /// Build a /static/bios/ path URL
        /// </summary>
        /// <param name="system">System to retrieve static BIOS datfile for, required</param>
        /// <remarks>Handles the non-BIOS variants of systems for compatibility</remarks>
        public static string BuildBiosUrl(PhysicalSystem system)
        {
            var sb = new StringBuilder();

#pragma warning disable IDE0072 // Add missing cases
            string? filename = system switch
            {
                PhysicalSystem.MicrosoftXbox => MicrosoftXboxBIOSFilename,
                PhysicalSystem.MicrosoftXboxBIOS => MicrosoftXboxBIOSFilename,

                PhysicalSystem.NintendoGameCube => NintendoGameCubeBIOSFilename,
                PhysicalSystem.NintendoGameCubeBIOS => NintendoGameCubeBIOSFilename,

                PhysicalSystem.SonyPlayStation => SonyPlayStationBIOSFilename,
                PhysicalSystem.SonyPlayStationBIOS => SonyPlayStationBIOSFilename,

                PhysicalSystem.SonyPlayStation2 => SonyPlayStation2BIOSFilename,
                PhysicalSystem.SonyPlayStation2BIOS => SonyPlayStation2BIOSFilename,

                _ => null,
            };
#pragma warning restore IDE0072 // Add missing cases

            // Ignore invalid BIOS systems
            if (filename is null)
                return string.Empty;

            sb.Append(SiteBaseUrl);
            sb.AppendFormat(BiosPath, filename);

            return sb.ToString();
        }

        /// <summary>
        /// Build a /disc/ path URL
        /// </summary>
        /// <param name="id">Disc ID, required</param>
        /// <param name="subpath">Disc page subpath, null to omit</param>
        public static string BuildDiscUrl(int id, DiscSubpath? subpath = null)
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.AppendFormat(DiscPath, Math.Abs(id));

            switch (subpath)
            {
                // Does not require trailing slash
                case DiscSubpath.Cuesheet:
                    sb.Append($"{subpath.ShortName()}");
                    break;

                // Requires trailing slash
                case DiscSubpath.Edit:
                    sb.Append($"{subpath.ShortName()}/");
                    break;

                // redump.org subpaths that don't have equivilent paths
                case DiscSubpath.Changes:
                case DiscSubpath.GDI:
                case DiscSubpath.Key:
                case DiscSubpath.LSD:
                case DiscSubpath.MD5:
                case DiscSubpath.SBI:
                case DiscSubpath.SFV:
                case DiscSubpath.SHA1:
                case DiscSubpath.WIP:
                    break;

                // History and null are invalid for a disc page
                case DiscSubpath.History:
                case null:
                default:
                    break;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build a /discs/ path URL
        /// </summary>
        /// <param name="advanced">Set advanced search status, null to omit</param>
        /// <param name="barcode">Add barcode to filter, null to omit</param>
        /// <param name="barcodeExact">Set exact barcode handling, null to omit</param>
        /// <param name="category">Add category to filter, null to omit</param>
        /// <param name="comments">Add comments to filter, null to omit</param>
        /// <param name="contents">Add contents to filter, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edc">Add EDC status to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="editionExact">Set exact edition handling, null to omit</param>
        /// <param name="errorsMax">Add maximum error count to filter, null to omit</param>
        /// <param name="errorsMin">Add minimum error count to filter, null to omit</param>
        /// <param name="language">Add language to filter, null to omit</param>
        /// <param name="letter">Starts with upper-case letter or '#' for numbers, null to omit</param>
        /// <param name="media">Add media type to filter, null to omit</param>
        /// <param name="offset">Add offset to filter, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="page">Page number, null to omit</param>
        /// <param name="protection">Add protection to filter, null to omit</param>
        /// <param name="query">Generic text query to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="ringcode">Add ringcode to filter, null to omit</param>
        /// <param name="serial">Add serial to filter, null to omit</param>
        /// <param name="serialExact">Set exact serial handling, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="title">Add title to filter, null to omit</param>
        /// <param name="titleExact">Set exact title handling, null to omit</param>
        /// <param name="titleForeign">Add foreign title to filter, null to omit</param>
        /// <param name="titleForeignExact">Set exact foreign title handling, null to omit</param>
        /// <param name="tracksMax">Add maximum track count to filter, null to omit</param>
        /// <param name="tracksMin">Add minimum track count to filter, null to omit</param>
        /// <remarks>Ordered according to site source code</remarks>
        public static string BuildDiscsUrl(
            bool? advanced = null,
            string? barcode = null,
            bool? barcodeExact = null,
            DiscCategory? category = null,
            string? comments = null,
            string? contents = null,
            string? dumper = null,
            YesNo? edc = null,
            string? edition = null,
            bool? editionExact = null,
            long? errorsMax = null,
            long? errorsMin = null,
            Language? language = null,
            char? letter = null,
            MediaType? media = null,
            long? offset = null,
            SortDirection? order = null,
            long? page = null,
            string? protection = null,
            string? query = null,
            Region? region = null,
            string? ringcode = null,
            string? serial = null,
            bool? serialExact = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            PhysicalSystem? system = null,
            string? title = null,
            bool? titleExact = null,
            string? titleForeign = null,
            bool? titleForeignExact = null,
            long? tracksMax = null,
            long? tracksMin = null)
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(DiscsPath);
            sb.Append('?');

            // System
            string? systemName = system.ShortName();
            if (systemName is not null)
                sb.Append($"system={systemName}&");

            // Region
            string? regionName = region.ShortName();
            if (regionName is not null)
                sb.Append($"region={regionName}&");

            // Language
            string? languageName = language.ShortName();
            if (languageName is not null)
                sb.Append($"language={languageName}&");

            // Media
            string? mediaName = media.ShortName();
            if (mediaName is not null)
                sb.Append($"media={mediaName}&");

            // Category
            string? categoryName = category.LongName();
            if (categoryName is not null)
                sb.Append($"category={categoryName}&");

            // Status
            string? statusName = status.LongName();
            if (statusName is not null)
                sb.Append($"status={statusName}&");

            // Letter
            if (letter is not null)
                sb.Append($"letter={char.ToUpperInvariant(letter.Value)}&");

            // Dumper
            if (dumper is not null)
                sb.Append($"dumper={dumper}&");

            // Title
            if (title is not null)
                sb.Append($"title={title}&");
            if (title is not null && titleExact is not null)
                sb.Append($"title_exact={titleExact.ToYesNo().LongName()}&");

            // Foreign Title
            if (titleForeign is not null)
                sb.Append($"title_foreign={titleForeign}&");
            if (titleForeign is not null && titleForeignExact is not null)
                sb.Append($"title_foreign_exact={titleForeignExact.ToYesNo().LongName()}&");

            // Foreign Title
            if (serial is not null)
                sb.Append($"serial={serial}&");
            if (serial is not null && serialExact is not null)
                sb.Append($"serial_exact={serialExact.ToYesNo().LongName()}&");

            // Edition
            if (edition is not null)
                sb.Append($"edition={edition}&");
            if (edition is not null && editionExact is not null)
                sb.Append($"edition_exact={editionExact.ToYesNo().LongName()}&");

            // Barcode
            if (barcode is not null)
                sb.Append($"barcode={barcode}&");
            if (barcode is not null && barcodeExact is not null)
                sb.Append($"barcode_exact={barcodeExact.ToYesNo().LongName()}&");

            // Track Count
            if (tracksMin is not null)
                sb.Append($"tracks_min={tracksMin}&");
            if (tracksMax is not null)
                sb.Append($"tracks_max={tracksMax}&");

            // Error Count
            if (errorsMin is not null)
                sb.Append($"errors_min={errorsMin}&");
            if (errorsMax is not null)
                sb.Append($"errors_max={errorsMax}&");

            // EDC
            if (edc is not null && edc != YesNo.NULL)
                sb.Append($"edc={edc.LongName()}&");

            // Protection
            if (protection is not null)
                sb.Append($"protection={protection}&");

            // Comments
            if (comments is not null)
                sb.Append($"comments={comments}&");

            // Contents
            if (contents is not null)
                sb.Append($"contents={contents}&");

            // Ringcode
            if (ringcode is not null)
                sb.Append($"ringcode={ringcode}&");

            // Ringcode
            if (offset is not null)
                sb.Append($"offset={offset}&");

            // General Query
            if (query is not null)
                sb.Append($"q={query}&");

            // Sorting
            switch (sort)
            {
                case SortCategory.Title:
                case SortCategory.Added:
                case SortCategory.Region:
                case SortCategory.System:
                case SortCategory.Version:
                case SortCategory.Edition:
                case SortCategory.Language:
                case SortCategory.Languages:
                case SortCategory.Serial:
                case SortCategory.Status:
                case SortCategory.Modified:
                    sb.Append($"sort={sort.ShortName()}&");
                    break;

                default: break;
            }

            // Sort Direction
            switch (order)
            {
                case SortDirection.Ascending:
                case SortDirection.Descending:
                    sb.Append($"order={order.ShortName()}&");
                    break;

                default: break;
            }

            // Page Number
            if (page is not null)
                sb.Append($"page={page}");

            // Advanced
            if (advanced is not null)
                sb.Append($"advanced={(advanced.Value ? "1" : "0")}&");

            return sb.ToString();
        }

        /// <summary>
        /// Build a /downloads/ path URL
        /// </summary>
        /// <param name="database">Target database download</param>
        public static string BuildDownloadsUrl(bool? database = null)
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(DownloadsPath);

            if (database == true)
                sb.Append("database");

            return sb.ToString();
        }

        /// <summary>
        /// Build a direct-download path URL
        /// </summary>
        /// <param name="packType">Pack type</param>
        /// <param name="system">System for download</param>
        /// <remarks>Does not check for invalid systems</remarks>
        /// TODO: Handle download_dat_variant?
        public static string BuildPackUrl(PackType packType, PhysicalSystem system)
        {
            // Hack to support the static BIOS sets
            if (packType == PackType.Datfile
                && (system == PhysicalSystem.MicrosoftXboxBIOS
                    || system == PhysicalSystem.NintendoGameCubeBIOS
                    || system == PhysicalSystem.SonyPlayStationBIOS
                    || system == PhysicalSystem.SonyPlayStation2BIOS))
            {
                return BuildBiosUrl(system);
            }

            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);

            string systemName = system.ShortName() ?? string.Empty;
            switch (packType)
            {
                case PackType.Cuesheets: sb.AppendFormat(CuesPath, systemName); break;
                case PackType.Datfile: sb.AppendFormat(DatfilePath, systemName); break;
                case PackType.DecryptedKeys: break; // Not supported
                case PackType.Gdis: break; // Not supported
                case PackType.Keys: sb.AppendFormat(KeysPath, systemName); break;
                case PackType.Lsds: break; // Not supported
                case PackType.Sbis: sb.AppendFormat(SbiPath, systemName); break;

                default: throw new ArgumentOutOfRangeException(nameof(packType));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build a /queue/ path URL
        /// </summary>
        /// <param name="discId">Add disc ID to filter, null to omit</param>
        /// <param name="isDiscHistory">Set disc history status, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="page">Page number, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="submitter">Add submitter name to filter, null to omit</param>
        /// <param name="subType">Add submission type to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <remarks>Ordered according to site source code</remarks>
        public static string BuildQueueUrl(
            long? discId = null,
            bool? isDiscHistory = null,
            SortDirection? order = null,
            long? page = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            string? submitter = null,
            SubmissionType? subType = null,
            PhysicalSystem? system = null)
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(QueuePath);
            sb.Append('?');

            // Status
            string? statusName = status.LongName();
            if (statusName is not null)
                sb.Append($"status={statusName}&");

            // Submission Type
            string? subTypeName = subType.ShortName();
            if (subTypeName is not null)
                sb.Append($"sub_type={subTypeName}&");

            // System
            string? systemName = system.ShortName();
            if (systemName is not null)
                sb.Append($"system={systemName}&");

            // Submitter
            if (submitter is not null)
                sb.Append($"submitter={submitter}&");

            // Disc ID
            if (discId is not null)
                sb.Append($"disc_id={discId}&");

            // Sorting
            switch (sort)
            {
                case SortCategory.Title:
                case SortCategory.Added:
                case SortCategory.Region:
                case SortCategory.System:
                case SortCategory.Version:
                case SortCategory.Edition:
                case SortCategory.Language:
                case SortCategory.Languages:
                case SortCategory.Serial:
                case SortCategory.Status:
                case SortCategory.Modified:
                    sb.Append($"sort={sort.ShortName()}&");
                    break;

                default: break;
            }

            // Sort Direction
            switch (order)
            {
                case SortDirection.Ascending:
                case SortDirection.Descending:
                    sb.Append($"order={order.ShortName()}&");
                    break;

                default: break;
            }

            // Page Number
            if (page is not null)
                sb.Append($"page={page}");

            // Is Disc History
            if (isDiscHistory is not null)
                sb.Append($"is_disc_history={isDiscHistory.ToYesNo().LongName()}&");

            return sb.ToString();
        }

        /// <summary>
        /// Build a /queue/ disc path URL
        /// </summary>
        /// <param name="id">Queue disc ID</param>
        /// TODO: Add form URLs, not just IDs
        public static string BuildQueueDiscUrl(int id)
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.AppendFormat(QueueDiscPath, Math.Abs(id));

            return sb.ToString();
        }
    }
}
