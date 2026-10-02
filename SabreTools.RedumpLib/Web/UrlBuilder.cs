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

        /// <summary>
        /// Build a /downloads/ path URL
        /// </summary>
        /// <param name="database">Target database download</param>
        public static string BuildDownloadsUrl(bool? database = null)
        {
            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = "downloads",
            };

            if (database == true)
                ub.Path += "/database";

            return ub.ToString();
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

            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
            };

            string systemName = system.Code ?? string.Empty;
            switch (packType)
            {
                case PackType.Cuesheets: ub.Path = $"cues/{systemName}"; break;
                case PackType.Datfile: ub.Path = $"datfile/{systemName}"; break;
                case PackType.Sbis: ub.Path = $"sbi/{systemName}"; break;

                // Invalid
                default: throw new ArgumentOutOfRangeException(nameof(packType));
            }

            return ub.ToString();
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
            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = "queue",
                Query = BuildQueueQuery(
                    discId,
                    isDiscHistory,
                    order,
                    page,
                    sort,
                    status,
                    submitter,
                    subType,
                    system
                ),
            };

            return ub.ToString();
        }

        /// <summary>
        /// Build a /queue/ disc path URL
        /// </summary>
        /// <param name="id">Queue disc ID</param>
        public static string BuildQueueDiscUrl(int id)
        {
            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = $"queue/{Math.Abs(id)}/",
            };

            return ub.ToString();
        }

        /// <summary>
        /// Build a /static/bios/ path URL
        /// </summary>
        /// <param name="system">System to retrieve static BIOS datfile for, required</param>
        /// <remarks>Handles the non-BIOS variants of systems for compatibility</remarks>
        private static string BuildBiosUrl(PhysicalSystem system)
        {
            string? filename;
            if (system == PhysicalSystem.MicrosoftXbox || system == PhysicalSystem.MicrosoftXboxBIOS)
                filename = MicrosoftXboxBIOSFilename;
            else if (system == PhysicalSystem.NintendoGameCube || system == PhysicalSystem.NintendoGameCubeBIOS)
                filename = NintendoGameCubeBIOSFilename;
            else if (system == PhysicalSystem.SonyPlayStation || system == PhysicalSystem.SonyPlayStationBIOS)
                filename = SonyPlayStationBIOSFilename;
            else if (system == PhysicalSystem.SonyPlayStation2 || system == PhysicalSystem.SonyPlayStation2BIOS)
                filename = SonyPlayStation2BIOSFilename;
            else
                filename = null;

            // Ignore invalid BIOS systems
            if (filename is null)
                return string.Empty;

            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = $"static/bios/{filename}",
            };

            return ub.ToString();
        }

        #region Query String Builders

        /// <summary>
        /// Build a /queue/ path query
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
        private static string BuildQueueQuery(
            long? discId,
            bool? isDiscHistory,
            SortDirection? order,
            long? page,
            SortCategory? sort,
            DumpStatus? status,
            string? submitter,
            SubmissionType? subType,
            PhysicalSystem? system)
        {
            var sb = new StringBuilder();

            // Status
            string? statusName = status.LongName();
            if (statusName is not null)
                sb.Append($"status={statusName}&");

            // Submission Type
            string? subTypeName = subType.ShortName();
            if (subTypeName is not null)
                sb.Append($"sub_type={subTypeName}&");

            // System
            string? systemName = system?.Code;
            if (systemName is not null)
                sb.Append($"system={systemName}&");

            // Submitter
            if (submitter is not null)
                sb.Append($"submitter={submitter}&");

            // Disc ID
            if (discId is not null)
                sb.Append($"disc_id={discId}&");

            // Sorting
            if (sort is not null && Enum.IsDefined(typeof(SortCategory), sort))
                sb.Append($"sort={sort.ShortName()}&");

            // Sort Direction
            if (order is not null && Enum.IsDefined(typeof(SortDirection), order))
                sb.Append($"order={order.ShortName()}&");

            // Page Number
            if (page is not null)
                sb.Append($"page={page}");

            // Is Disc History
            if (isDiscHistory is not null)
                sb.Append($"is_disc_history={isDiscHistory.ToYesNo().LongName()}&");

            return sb.ToString();
        }

        #endregion
    }
}
