using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// Contains logic for dealing with searches
    /// </summary>
    public static class Search
    {
        /// <summary>
        /// List the disc IDs associated with a given quicksearch query
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="query">Query string to attempt to search for</param>
        /// <param name="noSlash">Don't replace slashes with `-` in queries</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> ListSearchResults(this RedumpClient rc, string? query, bool noSlash)
        {
            // If the query is invalid
            if (string.IsNullOrEmpty(query))
                return [];

            List<int> ids = [];

            // Strip quotes
            query = query!.Trim('"', '\'');

            // Special characters become dashes
            query = query.Replace(' ', '-');
            query = query.Replace('\\', '-');
            if (!noSlash)
                query = query.Replace('/', '-');
            else
                query = query.TrimStart('/');

            // Lowercase is defined per language
            query = query.ToLowerInvariant();

            // Keep getting quicksearch pages until there are none left
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    var pageIds = await rc.CheckSingleSitePage(string.Format(Constants.QuickSearchUrl, query, pageNumber++));
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

        /// <summary>
        /// Download the disc pages associated with a given quicksearch query
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="query">Query string to attempt to search for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="noSlash">Don't replace slashes with `-` in queries</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> DownloadSearchResults(this RedumpClient rc, string? query, string? outDir, bool noSlash)
        {
            List<int> ids = [];

            // If the query is invalid
            if (string.IsNullOrEmpty(query))
                return ids;

            // Strip quotes
            query = query!.Trim('"', '\'');

            // Special characters become dashes
            query = query.Replace(' ', '-');
            query = query.Replace('\\', '-');
            if (!noSlash)
                query = query.Replace('/', '-');
            else
                query = query.TrimStart('/');

            // Lowercase is defined per language
            query = query.ToLowerInvariant();

            // Keep getting quicksearch pages until there are none left
            int pageNumber = 1;
            while (true)
            {
                var pageIds = await rc.CheckSingleSitePage(string.Format(Constants.QuickSearchUrl, query, pageNumber++), outDir, false);
                if (pageIds is null)
                    return [];

                ids.AddRange(pageIds);
                if (pageIds.Count == 0)
                    break;
            }

            return ids;
        }
    }
}
