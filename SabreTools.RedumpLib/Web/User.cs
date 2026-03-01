using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;

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
        /// <param name="forceDownload">True to force all downloads, false otherwise</param>
        /// <param name="forceContinue">Force continuation of download</param>
        /// <returns>All disc IDs for the given user, empty on error</returns>
        public static async Task<List<int>> DownloadUser(this RedumpClient client, string? username, string? outDir, bool forceDownload, bool forceContinue)
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
                string url = string.Format(Constants.UserDumpsUrl, username, pageNumber++);
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
        /// Download the last modified disc pages associated with the given user, until first failure
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="username">Username to check discs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="forceDownload">True to force all downloads, false otherwise</param>
        /// <param name="forceContinue">Force continuation of download</param>
        /// <returns>All disc IDs for the given user, empty on error</returns>
        public static async Task<List<int>> DownloadUserLastModified(this RedumpClient client, string? username, string? outDir, bool forceDownload, bool forceContinue)
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
                string url = string.Format(Constants.UserDumpsLastModifiedUrl, username, pageNumber++);
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
        /// List the disc IDs associated with the given user
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="username">Username to check discs for</param>
        /// <returns>All disc IDs for the given user, empty on error</returns>
        public static async Task<List<int>> ListUser(this RedumpClient client, string? username)
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
                    string url = string.Format(Constants.UserDumpsUrl, username, pageNumber++);
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
    }
}
