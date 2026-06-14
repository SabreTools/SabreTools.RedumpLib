using System;
using System.Text;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.RedumpInfo.Data;
using static SabreTools.RedumpLib.RedumpOrg.Data.Extensions;

namespace SabreTools.RedumpLib.RedumpInfo
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
        /// Path for BIOS datfile downloads
        /// </summary>
        private const string BiosPath = @"static/bios/{0}/";

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
        /// Path for downloads page
        /// </summary>
        private const string DownloadsPath = "downloads/";

        /// <summary>
        /// Path for key pack downloads
        /// </summary>
        private const string KeysPath = @"keys/{0}/";

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
        private const string SbiPath = @"sbi/{0}/";

        #endregion

        // TODO: Add filter statements for discs

        #endregion

        /// <summary>
        /// Build a /static/bios/ path URL
        /// </summary>
        /// <param name="filename">BIOS datfile filename, required</param>
        public static string BuildBiosUrl(string filename)
        {
            var sb = new StringBuilder();

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
        /// <param name="comments">Add comments to filter, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="letter">Starts with upper-case letter or '#' for numbers, null to omit</param>
        /// <param name="quicksearch">Generic text search to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="sortDir">Add sorting direction, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="page">Page number, null to omit</param>
        /// <remarks>Does not check for incompatibilities</remarks>
        public static string BuildDiscsUrl(
            string? comments = null,
            string? dumper = null,
            string? edition = null,
            char? letter = null,
            string? quicksearch = null,
            Region? region = null,
            SortCategory? sort = null,
            SortDirection? sortDir = null,
            DumpStatus? status = null,
            PhysicalSystem? system = null,
            int? page = null)
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(DiscsPath);
            sb.Append('?');

            // Comments
            if (comments is not null)
                sb.Append($"comments_q={comments}&");

            // Dumper
            if (dumper is not null)
                sb.Append($"dumper={dumper}&");

            // Edition
            if (edition is not null)
                sb.Append($"edition_q={edition}&");

            // Letter
            if (letter is not null)
                sb.Append($"letter={char.ToUpperInvariant(letter.Value)}&");

            // TODO: Text Search
            if (quicksearch is not null)
                sb.Append($"q={quicksearch}&");

            // Region
            string? regionName = region.ShortName();
            if (regionName is not null)
                sb.Append($"region={regionName}&");

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
            switch (sortDir)
            {
                case SortDirection.Ascending:
                case SortDirection.Descending:
                    sb.Append($"order={sortDir.ShortName()}&");
                    break;

                default: break;
            }

            // Status
            if (status is not null && status >= DumpStatus.UnknownGrey && status <= DumpStatus.Verified)
                sb.Append($"status={(int)status}&");

            // System
            string? systemName = system.ShortName();
            if (systemName is not null)
                sb.Append($"system={systemName}&");

            // TODO: Page Number - Has to be last?
            if (page is not null)
                sb.Append($"page={page}");

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
        /// Build a direct-download path URL
        /// </summary>
        /// <param name="packType">Pack type</param>
        /// <param name="system">System for download</param>
        /// <remarks>Does not check for invalid systems</remarks>
        public static string BuildPackUrl(PackType packType, PhysicalSystem system)
        {
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
        public static string BuildQueueUrl()
        {
            var sb = new StringBuilder();

            sb.Append(SiteBaseUrl);
            sb.Append(QueuePath);

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
