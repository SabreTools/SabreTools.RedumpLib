using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// Contains logic for dealing with searches
    /// </summary>
    public static class Search
    {
        /// <summary>
        /// Download the disc pages associated with a given quicksearch query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="query">Query string to attempt to search for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> DownloadSearchResults(this RedumpClient client,
            string? query,
            string? outDir,
            int limit = -1)
        {
            // If the query is invalid
            if (string.IsNullOrEmpty(query))
                return [];

            // Keep getting quicksearch pages until there are none left
            List<int> ids = [];
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    if (limit > 0 && pageNumber >= limit)
                        break;

                    // Convert forward slashes implies a strict query
                    var pageIds = await client.CheckSingleDiscsPage(outDir, quicksearch: query, page: pageNumber++);
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
        /// List the disc IDs associated with a given quicksearch query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="query">Query string to attempt to search for</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> ListSearchResults(this RedumpClient client,
            string? query,
            int limit = -1)
        {
            // If the query is invalid
            if (string.IsNullOrEmpty(query))
                return [];

            // Keep getting quicksearch pages until there are none left
            List<int> ids = [];
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    if (limit > 0 && pageNumber >= limit)
                        break;

                    var pageIds = await client.CheckSingleDiscsPage(quicksearch: query, page: pageNumber++);
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
