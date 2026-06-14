using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.RedumpInfo.Data;
using SabreTools.RedumpLib.Web;

namespace SabreTools.RedumpLib.RedumpInfo
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
        /// <param name="comments">Add comments to filter, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="quicksearch">Generic text search to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="sortDir">Add sorting direction, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <param name="discSubpaths">Set of subpaths to download if available, null for all</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> DownloadDiscsResults(this Client client,
            string? outDir,
            string? comments,
            string? dumper = null,
            string? edition = null,
            string? quicksearch = null,
            Region? region = null,
            RedumpOrg.Data.SortCategory? sort = null,
            SortDirection? sortDir = null,
            DumpStatus? status = null,
            PhysicalSystem? system = null,
            int limit = -1,
            DiscSubpath[]? discSubpaths = null)
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
                        comments,
                        dumper,
                        edition,
                        quicksearch,
                        region,
                        sort,
                        sortDir,
                        status,
                        system,
                        pageNumber++,
                        discSubpaths);
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
        /// <param name="discSubpaths">Set of subpaths to download if available, null for all</param>
        /// <returns>All disc IDs that successfully downloaded, empty on error</returns>
        public static async Task<List<int>> DownloadSiteRange(this Client client,
            string? outDir,
            int minId,
            int maxId,
            DiscSubpath[]? discSubpaths = null)
        {
            List<int> ids = [];
            for (int id = minId; id <= maxId; id++)
            {
                bool downloaded = await client.DownloadSingleSiteID(id, outDir, rename: true, discSubpaths);
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
        /// <param name="discSubpaths">Set of subpaths to download if available, null for all</param>
        /// <returns>All disc IDs that successfully downloaded, empty on error</returns>
        public static async Task<List<int>> DownloadSiteSet(this Client client,
            List<int> siteIds,
            string? outDir,
            DiscSubpath[]? discSubpaths = null)
        {
            List<int> ids = [];
            foreach (int id in siteIds)
            {
                bool downloaded = await client.DownloadSingleSiteID(id, outDir, rename: true, discSubpaths);
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
        /// <param name="comments">Add comments to filter, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="quicksearch">Generic text search to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="sortDir">Add sorting direction, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="comments">Marks search as comments field only, false to omit; incompatible with <paramref name="contents"/> or <paramref name="protection"/></param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> ListDiscsResults(this Client client,
            string? comments = null,
            string? dumper = null,
            string? edition = null,
            string? quicksearch = null,
            Region? region = null,
            RedumpOrg.Data.SortCategory? sort = null,
            SortDirection? sortDir = null,
            DumpStatus? status = null,
            PhysicalSystem? system = null,
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
                        comments,
                        dumper,
                        edition,
                        quicksearch,
                        region,
                        sort,
                        sortDir,
                        status,
                        system,
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
