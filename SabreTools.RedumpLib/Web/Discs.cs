using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// Contains logic for dealing with disc pages
    /// </summary>
    public static class Discs
    {
        /// <summary>
        /// Download the disc pages associated with a given discs query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
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
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> DownloadDiscsResults(this RedumpClient client,
            string? outDir,
            bool? antimodchip = null,
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
            int limit = -1)
        {
            // Keep getting discs pages until there are none left
            List<int> ids = [];
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    if (limit > 0 && pageNumber > limit)
                        break;

                    var pageIds = await client.CheckSingleDiscsPage(
                        outDir,
                        antimodchip,
                        barcode,
                        category,
                        discType,
                        dumper,
                        edc,
                        edition,
                        errors,
                        language,
                        letter,
                        libcrypt,
                        media,
                        offset,
                        quicksearch,
                        region,
                        ringcode,
                        sort,
                        sortDir,
                        status,
                        system,
                        tracks,
                        comments,
                        contents,
                        protection,
                        pageNumber++);
                    if (pageIds is null)
                        return [];

                    ids.AddRange(pageIds);
                    if (pageIds.Count == 0)
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred while trying to log in: {ex}");
                return [];
            }

            return ids;
        }

        /// <summary>
        /// Download the specified range of site disc pages
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="minId">Starting ID for the range</param>
        /// <param name="maxId">Ending ID for the range (inclusive)</param>
        /// <returns>All disc IDs that successfully downloaded, empty on error</returns>
        public static async Task<List<int>> DownloadSiteRange(this RedumpClient client, string? outDir, int minId, int maxId)
        {
            List<int> ids = [];
            for (int id = minId; id <= maxId; id++)
            {
                bool downloaded = await client.DownloadSingleSiteID(id, outDir, rename: true);
                if (downloaded)
                {
                    ids.Add(id);
                    DelayHelper.DelayRandom();
                }
            }

            return ids;
        }

        /// <summary>
        /// Download the specified set of site disc pages
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="siteIds">Set of site IDs to download</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>All disc IDs that successfully downloaded, empty on error</returns>
        public static async Task<List<int>> DownloadSiteSet(this RedumpClient client, List<int> siteIds, string? outDir)
        {
            List<int> ids = [];
            foreach (int id in siteIds)
            {
                bool downloaded = await client.DownloadSingleSiteID(id, outDir, rename: true);
                if (downloaded)
                {
                    ids.Add(id);
                    DelayHelper.DelayRandom();
                }
            }

            return ids;
        }

        /// <summary>
        /// List the disc IDs associated with a given discs query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
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
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> ListDiscsResults(this RedumpClient client,
            bool? antimodchip = null,
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
            int limit = -1)
        {
            // Keep getting discs pages until there are none left
            List<int> ids = [];
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    if (limit > 0 && pageNumber > limit)
                        break;

                    var pageIds = await client.CheckSingleDiscsPage(antimodchip,
                        barcode,
                        category,
                        discType,
                        dumper,
                        edc,
                        edition,
                        errors,
                        language,
                        letter,
                        libcrypt,
                        media,
                        offset,
                        quicksearch,
                        region,
                        ringcode,
                        sort,
                        sortDir,
                        status,
                        system,
                        tracks,
                        comments,
                        contents,
                        protection,
                        pageNumber++);
                    if (pageIds is null)
                        return [];

                    ids.AddRange(pageIds);
                    if (pageIds.Count <= 1)
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred while trying to log in: {ex}");
                return [];
            }

            return ids;
        }
    }
}
