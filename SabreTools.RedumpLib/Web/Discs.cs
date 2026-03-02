using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// <param name="query">Query string to attempt to search for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="convertForwardSlashes">Replace forward slashes with `-` in queries</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> DownloadDiscsResults(this RedumpClient client,
            string? query,
            string? outDir,
            bool convertForwardSlashes,
            int limit = -1)
        {
            // If the query is invalid
            if (string.IsNullOrEmpty(query))
                return [];

            // Keep getting discs pages until there are none left
            List<int> ids = [];
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    if (limit > 0 && pageNumber >= limit)
                        break;

                    var pageIds = await client.CheckSingleDiscsPage(query!, pageNumber++, outDir, convertForwardSlashes);
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
        /// Download the last modified disc pages, until first failure
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs in last modified range, empty on error</returns>
        public static async Task<List<int>> DownloadLastModified(this RedumpClient client,
            string? outDir,
            int limit = -1)
        {
            List<int> ids = [];

            // Keep getting last modified pages until there are none left
            int pageNumber = 1;
            while (true)
            {
                if (limit > 0 && pageNumber >= limit)
                    break;

                var pageIds = await client.CheckSingleDiscsLastModifiedPage(pageNumber++, outDir);
                if (pageIds is null)
                    return [];

                ids.AddRange(pageIds);
                if (pageIds.Count == 0)
                    break;
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
        /// List the disc IDs associated with a given discs query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="query">Query string to attempt to search for</param>
        /// <param name="convertForwardSlashes">Replace forward slashes with `-` in queries</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> ListDiscsResults(this RedumpClient client,
            string? query,
            bool convertForwardSlashes,
            int limit = -1)
        {
            // If the query is invalid
            if (string.IsNullOrEmpty(query))
                return [];

            // Keep getting discs pages until there are none left
            List<int> ids = [];
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    if (limit > 0 && pageNumber >= limit)
                        break;

                    var pageIds = await client.CheckSingleDiscsPage(query!, pageNumber++, convertForwardSlashes);
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
