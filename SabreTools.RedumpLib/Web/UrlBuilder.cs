using System.Text;
using SabreTools.RedumpLib.Data;

// TODO: Errors should validate number or number range (# or #-#)
// TODO: Sort needs to be an enum (added, region, system, version, edition, languages, serial, status, modified, title [none])

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// URL builder helper
    /// </summary>
    public static class UrlBuilder
    {
        /// <summary>
        /// Build a /discs/ path URL
        /// </summary>
        /// <param name="antimodchip">Anti-modchip status to filter, null to omit</param>
        /// <param name="barcode">Add no barcode search to filter, false to omit</param>
        /// <param name="category">Add category to filter, null to omit</param>
        /// <param name="comments">Marks search as comments field only, false to omit</param>
        /// <param name="contents">Marks search as contents field only, false to omit</param>
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
        /// <param name="page">Page number, null to omit</param>
        /// <param name="protection">Marks search as protection field only, false to omit</param>
        /// <param name="quicksearch">Generic text search to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="ringcode">Add ringcode to filter, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="sortDir">Add sorting direction, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="tracks">Track count up to 99, null to omit</param>
        /// <remarks>Does not check for incompatibilities</remarks>
        public static string BuildDiscsUrl(bool? antimodchip = null,
            bool barcode = false,
            DiscCategory? category = null,
            bool comments = false,
            bool contents = false,
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
            int? page = null,
            bool protection = false,
            string? quicksearch = null,
            Region? region = null,
            string? ringcode = null,
            string? sort = null,
            string? sortDir = null,
            DumpStatus? status = null,
            RedumpSystem? system = null,
            int? tracks = null)
        {
            var sb = new StringBuilder();

            sb.Append(Constants.SiteBaseUrl);
            sb.Append("discs/");

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

            // Comments Search
            if (comments)
                sb.Append("comments/only/");

            // Contents Search
            if (contents)
                sb.Append("contents/only/");

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

            // Protection Search
            if (protection)
                sb.Append("protection/only/");

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
            switch (sort?.ToLowerInvariant())
            {
                case "":
                case "title":
                    sb.Append("sort/");
                    break;

                case "added":
                case "region":
                case "system":
                case "version":
                case "edition":
                case "languages":
                case "serial":
                case "status":
                case "modified":
                    sb.Append($"sort/{sort}/");
                    break;

                default: break;
            }

            // Sort Direction
            switch (sortDir?.ToLowerInvariant())
            {
                case "asc":
                case "desc":
                    sb.Append($"dir/{sortDir}/");
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

            // Page Number - Has to be last
            if (page is not null)
                sb.Append($"?page={page}");

            return sb.ToString();
        }

        // TODO: Implement
        public static string BuildDiscsWipUrl()
        {
            return string.Empty;
        }

        // TODO: Implement
        public static string BuildDownloadsUrl(PackType packType)
        {
            return string.Empty;
        }

        // TODO: Implement
        public static string BuildListUrl()
        {
            return string.Empty;
        }

        // TODO: Implement
        public static string BuildNewDiscUrl()
        {
            return string.Empty;
        }
    }
}
