using System;
using System.Text;
using SabreTools.RedumpLib.Data;

// TODO: Errors should validate number or number range (# or #-#)

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
        private const string SiteBaseUrl = "http://redump.org/";

        #region Top-Level Paths

        /// <summary>
        /// Path for cuesheet pack downloads
        /// </summary>
        private const string CuesPath = @"cues/{0}/";

        /// <summary>
        /// Path for datfile downloads
        /// </summary>
        private const string DatfilePath = @"datfile/{0}/";

        /// <summary>
        /// Path for individual disc pages
        /// </summary>
        private const string DiscPath = @"disc/{0}/";

        /// <summary>
        /// Path for multi-disc pages
        /// </summary>
        private const string DiscsPath = "discs/";

        /// <summary>
        /// Path for WIP discs queue
        /// </summary>
        private const string DiscsWipPath = "discs-wip/";

        /// <summary>
        /// Path for decrypted key pack downloads
        /// </summary>
        private const string DkeysPath = @"dkeys/{0}/";

        /// <summary>
        /// Path for downloads page
        /// </summary>
        private const string DownloadsPath = "downloads/";

        /// <summary>
        /// Path for GDI pack downloads
        /// </summary>
        private const string GdiPath = @"gdi/{0}/";

        /// <summary>
        /// Path for key pack downloads
        /// </summary>
        private const string KeysPath = @"keys/{0}/";

        /// <summary>
        /// Path for per-user lists
        /// </summary>
        private const string ListPath = "list/";

        /// <summary>
        /// Path for LSD pack downloads
        /// </summary>
        private const string LsdPath = @"lsd/{0}/";

        /// <summary>
        /// Path for member promotion
        /// </summary>
        private const string MemberPromotionPath = "member2dumper/";

        /// <summary>
        /// Path for individual WIP disc pages
        /// </summary>
        private const string NewDiscPath = @"newdisc/{0}/";

        /// <summary>
        /// Path for SBI pack downloads
        /// </summary>
        private const string SbiPath = @"sbi/{0}/";

        /// <summary>
        /// Path for statistics
        /// </summary>
        private const string StatisticsPath = "statistics/";

        #endregion

        // TODO: Add filter statements for discs

        #endregion

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
                case DiscSubpath.Changes:
                case DiscSubpath.Cuesheet:
                case DiscSubpath.Edit:
                case DiscSubpath.GDI:
                case DiscSubpath.Key:
                case DiscSubpath.LSD:
                case DiscSubpath.MD5:
                case DiscSubpath.SBI:
                case DiscSubpath.SFV:
                case DiscSubpath.SHA1:
                    sb.Append($"{subpath.ShortName()}/");
                    break;

                default: break;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build a /discs/ path URL
        /// </summary>
        /// <param name="antimodchip">Anti-modchip status to filter, null to omit</param>
        /// <param name="barcode">Add no barcode search to filter, false to omit</param>
        /// <param name="category">Add category to filter, null to omit</param>
        /// <param name="discType">Disc type extension to filter, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edc">EDC status to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="errors">Add error count or range, null to omit</param>
        /// <param name="language">Add language to filter, null to omit</param>
        /// <param name="letter">Starts with letter or '~' for numbers, null to omit</param>
        /// <param name="libcrypt">LibCrypt status to filter, null to omit</param>
        /// <param name="media">Non-specific media type to filter, null to omit</param>
        /// <param name="offset">Write offset to filter, null to omit</param>
        /// <param name="quicksearch">Generic text search to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="ringcode">Add ringcode to filter, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="sortDir">Add sorting direction, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="tracks">Track count up to 99, null to omit</param>
        /// <param name="comments">Marks search as comments field only, false to omit; cannot be used with <paramref name="contents"/> or <paramref name="protection"/></param>
        /// <param name="contents">Marks search as contents field only, false to omit; cannot be used with <paramref name="comments"/> or <paramref name="protection"/></param>
        /// <param name="protection">Marks search as protection field only, false to omit; cannot be used with <paramref name="comments"/> or <paramref name="contents"/></param>
        /// <param name="page">Page number, null to omit</param>
        /// <remarks>Does not check for incompatibilities</remarks>
        public static string BuildDiscsUrl(bool? antimodchip = null,
            bool barcode = false,
            DiscCategory? category = null,
            DiscType? discType = null,
            string? dumper = null,
            YesNo? edc = null,
            string? edition = null,
            string? errors = null,
            Language? language = null,
            char? letter = null,
            bool? libcrypt = null,
            MediaType? media = null,
            int? offset = null,
            string? quicksearch = null,
            Region? region = null,
            string? ringcode = null,
            SortCategory? sort = null,
            SortDirection? sortDir = null,
            DumpStatus? status = null,
            RedumpSystem? system = null,
            int? tracks = null,
            bool comments = false,
            bool contents = false,
            bool protection = false,
            int? page = null)
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(DiscsPath);

            // Anti-modchip
            switch (antimodchip)
            {
                case false: sb.Append("antimodchip/1/"); break;
                case true: sb.Append("antimodchip/2/"); break;
                default: break;
            }

            // Barcode Search
            if (barcode)
                sb.Append("barcode/null/");

            // Category
            string? categoryName = category.LongName()?.ToLowerInvariant();
            if (categoryName is not null)
                sb.Append($"category/{categoryName}/");

            // Dumper
            if (dumper is not null)
                sb.Append($"dumper/{dumper}/");

            // EDC
            switch (edc)
            {
                case YesNo.Yes: sb.Append("edc/yes/"); break;
                case YesNo.No: sb.Append("edc/no/"); break;
                case YesNo.NULL: sb.Append("edc/unknown/"); break;
                default: break;
            }

            // Edition
            if (edition is not null)
                sb.Append($"edition/{edition}/");

            // Errors
            if (errors is not null)
                sb.Append($"errors/{errors}/");

            // Language
            string? languageName = language.ShortName();
            if (languageName is not null)
                sb.Append($"language/{languageName}/");

            // Starts With
            if (letter is not null)
            {
                letter = char.ToLowerInvariant(letter.Value);
                if (letter >= 'a' && letter <= 'z')
                    sb.Append($"letter/{letter}/");
                else if (letter >= '0' && letter <= '9')
                    sb.Append("letter/~/");
            }

            // LibCrypt
            switch (libcrypt)
            {
                case false: sb.Append("libcrypt/1/"); break;
                case true: sb.Append("libcrypt/2/"); break;
                default: break;
            }

#pragma warning disable IDE0010 // Populate switch
            // Media Search
            switch (media)
            {
                case MediaType.CDROM: sb.Append("media/cd/"); break;
                case MediaType.DVD: sb.Append("media/dvd/"); break;
                default: break;
            }
#pragma warning restore IDE0010

            // Offset
            if (offset is not null)
                sb.Append($"offset/{offset}/");

            // Text Search
            if (quicksearch is not null)
                sb.Append($"quicksearch/{quicksearch}/");

            // Region
            string? regionName = region.ShortName();
            if (regionName is not null)
                sb.Append($"region/{regionName}/");

            // Ringcode Search
            if (ringcode is not null)
                sb.Append($"ringcode/{ringcode}/");

            // Sorting
            switch (sort)
            {
                case SortCategory.Title:
                    sb.Append("sort/");
                    break;

                case SortCategory.Added:
                case SortCategory.Region:
                case SortCategory.System:
                case SortCategory.Version:
                case SortCategory.Edition:
                case SortCategory.Languages:
                case SortCategory.Serial:
                case SortCategory.Status:
                case SortCategory.Modified:
                    sb.Append($"sort/{sort.ShortName()}/");
                    break;

                default: break;
            }

            // Sort Direction
            switch (sortDir)
            {
                case SortDirection.Ascending:
                case SortDirection.Descending:
                    sb.Append($"dir/{sortDir.ShortName()}/");
                    break;

                default: break;
            }

            // Status
            if (status is not null && status >= DumpStatus.UnknownGrey && status <= DumpStatus.TwoOrMoreGreen)
                sb.Append($"status/{(int)status}/");

            // System / Disc Type Search
            string? systemName = system.ShortName();
            if (systemName is not null)
            {
                string? discTypeName = discType.LongName()?.ToLowerInvariant();
                if (discType is not null)
                    systemName = $"{systemName}_{discTypeName}";

                sb.Append($"system/{systemName}/");
            }

            // Tracks
            if (tracks is not null && tracks >= 1 && tracks <= 99)
                sb.Append($"tracks/{tracks}/");

            // Field-specific
            if (comments)
                sb.Append("comments/only/");
            else if (contents)
                sb.Append("contents/only/");
            else if (protection)
                sb.Append("protection/only/");

            // Page Number - Has to be last
            if (page is not null)
                sb.Append($"?page={page}");

            return sb.ToString();
        }

        /// <summary>
        /// Build a /discs-wip/ path URL
        /// </summary>
        public static string BuildDiscsWipUrl()
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(DiscsWipPath);

            return sb.ToString();
        }

        /// <summary>
        /// Build a /downloads/ path URL
        /// </summary>
        public static string BuildDownloadsUrl()
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(DownloadsPath);

            return sb.ToString();
        }

        /// <summary>
        /// Build a /list/ path URL
        /// </summary>
        /// <param name="username">Username to use</param>
        /// <param name="have">True to show "have" discs, false to show "miss" discs, null to omit (acts like "have")</param>
        /// <param name="system">System for filtering, null to omit (acts on all systems)</param>
        /// <remarks>Does not check for invalid usernames</remarks>
        public static string BuildListUrl(string username,
            bool? have = null,
            RedumpSystem? system = null)
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(ListPath);

            if (have is not null)
            {
                string operation = have.Value ? "have" : "miss";
                sb.Append($"{operation}/");
            }

            sb.Append($"{username}/");

            string? systemName = system.ShortName();
            if (systemName is not null)
                sb.Append($"{systemName}/");

            return sb.ToString();
        }

        /// <summary>
        /// Build a /member2dumper/ path URL
        /// </summary>
        public static string BuildMemberPromotionUrl()
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(MemberPromotionPath);

            return sb.ToString();
        }

        /// <summary>
        /// Build a /newdisc/ path URL
        /// </summary>
        /// <param name="id">WIP disc ID</param>
        /// TODO: Add form URLs, not just IDs
        public static string BuildNewDiscUrl(int id)
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.AppendFormat(NewDiscPath, Math.Abs(id));

            return sb.ToString();
        }

        /// <summary>
        /// Build a direct-download path URL
        /// </summary>
        /// <param name="packType">Pack type</param>
        /// <param name="system">System for download</param>
        /// <remarks>Does not check for incompatibilities</remarks>
        public static string BuildPackUrl(PackType packType, RedumpSystem system)
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);

            string systemName = system.ShortName() ?? string.Empty;
            switch (packType)
            {
                case PackType.Cuesheets: sb.AppendFormat(CuesPath, systemName); break;
                case PackType.Datfile: sb.AppendFormat(DatfilePath, systemName); break;
                case PackType.DecryptedKeys: sb.AppendFormat(DkeysPath, systemName); break;
                case PackType.Gdis: sb.AppendFormat(GdiPath, systemName); break;
                case PackType.Keys: sb.AppendFormat(KeysPath, systemName); break;
                case PackType.Lsds: sb.AppendFormat(LsdPath, systemName); break;
                case PackType.Sbis: sb.AppendFormat(SbiPath, systemName); break;

                default: throw new ArgumentOutOfRangeException(nameof(packType));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build a /statistics/ path URL
        /// </summary>
        /// TODO: Determine if there are any parameters, when possible
        public static string BuildStatisticsUrl()
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(StatisticsPath);

            return sb.ToString();
        }
    }
}
