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
        /// List the disc IDs associated with a given discs query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="query">Query string to attempt to search for</param>
        /// <param name="convertForwardSlashes">Replace forward slashes with `-` in queries</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> ListDiscsResults(this RedumpClient client, string? query, bool convertForwardSlashes)
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
            if (convertForwardSlashes)
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
                    string url = string.Format(Constants.DiscsUrl, query, pageNumber++);
                    var pageIds = await client.CheckSingleSitePage(url);
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
        /// Download the disc pages associated with a given discs query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="query">Query string to attempt to search for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="forceDownload">True to force all downloads, false otherwise</param>
        /// <param name="forceContinue">Force continuation of download</param>
        /// <param name="convertForwardSlashes">Replace forward slashes with `-` in queries</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> DownloadDiscsResults(this RedumpClient client,
            string? query,
            string? outDir,
            bool forceDownload,
            bool forceContinue,
            bool convertForwardSlashes)
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
            if (convertForwardSlashes)
                query = query.Replace('/', '-');
            else
                query = query.TrimStart('/');

            // Lowercase is defined per language
            query = query.ToLowerInvariant();

            // Keep getting quicksearch pages until there are none left
            int pageNumber = 1;
            while (true)
            {
                string url = string.Format(Constants.DiscsUrl, query, pageNumber++);
                var pageIds = await client.CheckSingleSitePage(url, outDir, forceDownload, forceContinue);
                if (pageIds is null)
                    return [];

                ids.AddRange(pageIds);
                if (pageIds.Count == 0)
                    break;
            }

            return ids;
        }

        /// <summary>
        /// Download the last modified disc pages, until first failure
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="forceDownload">True to force all downloads, false otherwise</param>
        /// <param name="forceContinue">Force continuation of download</param>
        /// <returns>All disc IDs in last modified range, empty on error</returns>
        public static async Task<List<int>> DownloadLastModified(this RedumpClient client, string? outDir, bool forceDownload, bool forceContinue)
        {
            List<int> ids = [];

            // Keep getting last modified pages until there are none left
            int pageNumber = 1;
            while (true)
            {
                string url = string.Format(Constants.LastModifiedUrl, pageNumber++);
                var pageIds = await client.CheckSingleSitePage(url, outDir, forceDownload, forceContinue);
                if (pageIds is null)
                    return [];

                ids.AddRange(pageIds);
                if (pageIds.Count == 0)
                    break;
            }

            return ids;
        }

        /// <summary>
        /// Download the specified range of site disc pages
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="forceDownload">True to force all downloads, false otherwise</param>
        /// <param name="forceContinue">Force continuation of download</param>
        /// <param name="minId">Starting ID for the range</param>
        /// <param name="maxId">Ending ID for the range (inclusive)</param>
        /// <returns>All disc IDs in last modified range, empty on error</returns>
        public static async Task<List<int>> DownloadSiteRange(this RedumpClient client,
            string? outDir,
            bool forceDownload,
            bool forceContinue,
            int minId = 0,
            int maxId = 0)
        {
            List<int> ids = [];
            for (int id = minId; id <= maxId; id++)
            {
                bool downloaded = await client.DownloadSingleSiteID(id, outDir, rename: true, forceDownload, forceContinue);
                if (downloaded)
                {
                    ids.Add(id);
                    DelayHelper.DelayRandom();
                }
            }

            return ids;
        }
    }
}
