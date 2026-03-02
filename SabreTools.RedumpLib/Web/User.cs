using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// Contains logic for dealing with users
    /// </summary>
    public static class User
    {
        /// <summary>
        /// Download the disc pages associated with the given user
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="username">Username to check discs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given user, empty on error</returns>
        public static async Task<List<int>> DownloadUser(this RedumpClient client,
            string? username,
            string? outDir,
            int limit = -1)
        {
            List<int> ids = [];
            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("A username must be specified!");
                return ids;
            }

            // Keep getting user pages until there are none left
            int pageNumber = 1;
            while (true)
            {
                if (limit > 0 && pageNumber >= limit)
                    break;

                var pageIds = await client.CheckSingleUserPage(username!, pageNumber++, outDir, lastModified: false);
                if (pageIds is null)
                    return [];

                ids.AddRange(pageIds);
                if (pageIds.Count == 0)
                    break;
            }

            return ids;
        }

        /// <summary>
        /// Download the last modified disc pages associated with the given user, until first failure
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="username">Username to check discs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given user, empty on error</returns>
        public static async Task<List<int>> DownloadUserLastModified(this RedumpClient client,
            string? username,
            string? outDir,
            int limit = -1)
        {
            List<int> ids = [];
            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("A username must be specified!");
                return ids;
            }

            // Keep getting last modified user pages until there are none left
            int pageNumber = 1;
            while (true)
            {
                if (limit > 0 && pageNumber >= limit)
                    break;

                var pageIds = await client.CheckSingleUserPage(username!, pageNumber++, outDir, lastModified: true);
                if (pageIds is null)
                    return [];

                ids.AddRange(pageIds);
                if (pageIds.Count == 0)
                    break;
            }

            return ids;
        }

        /// <summary>
        /// List the disc IDs associated with the given user
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="username">Username to check discs for</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given user, empty on error</returns>
        public static async Task<List<int>> ListUser(this RedumpClient client,
            string? username,
            int limit = -1)
        {
            List<int> ids = [];
            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("A username must be specified!");
                return ids;
            }

            // Keep getting user pages until there are none left
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    if (limit > 0 && pageNumber >= limit)
                        break;

                    var pageIds = await client.CheckSingleUserPage(username!, pageNumber++, lastModified: false);
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
