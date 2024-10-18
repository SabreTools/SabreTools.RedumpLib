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
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="username">Username to check discs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>All disc IDs for the given user, empty on error</returns>
        public static async Task<List<int>> DownloadUser(RedumpClient rc, string? username, string? outDir)
        {
            List<int> ids = [];

            if (!rc.LoggedIn || string.IsNullOrEmpty(username))
            {
                Console.WriteLine("User download functionality is only available to Redump members");
                return ids;
            }

            // Keep getting user pages until there are none left
            int pageNumber = 1;
            while (true)
            {
                var pageIds = await rc.CheckSingleSitePage(string.Format(Constants.UserDumpsUrl, username, pageNumber++), outDir, false);
                ids.AddRange(pageIds);
                if (pageIds.Count == 0)
                    break;
            }

            return ids;
        }

        /// <summary>
        /// Download the last modified disc pages associated with the given user, until first failure
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="username">Username to check discs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>All disc IDs for the given user, empty on error</returns>
        public static async Task<List<int>> DownloadUserLastModified(RedumpClient rc, string? username, string? outDir)
        {
            List<int> ids = [];

            if (!rc.LoggedIn || string.IsNullOrEmpty(username))
            {
                Console.WriteLine("User download functionality is only available to Redump members");
                return ids;
            }

            // Keep getting last modified user pages until there are none left
            int pageNumber = 1;
            while (true)
            {
                var pageIds = await rc.CheckSingleSitePage(string.Format(Constants.UserDumpsLastModifiedUrl, username, pageNumber++), outDir, true);
                ids.AddRange(pageIds);
                if (pageIds.Count == 0)
                    break;
            }

            return ids;
        }
    
        /// <summary>
        /// List the disc IDs associated with the given user
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="username">Username to check discs for</param>
        /// <returns>All disc IDs for the given user, empty on error</returns>
        public static async Task<List<int>> ListUser(RedumpClient rc, string? username)
        {
            List<int> ids = [];

            if (!rc.LoggedIn || string.IsNullOrEmpty(username))
            {
                Console.WriteLine("User download functionality is only available to Redump members");
                return ids;
            }

            // Keep getting user pages until there are none left
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    var pageIds = await rc.CheckSingleSitePage(string.Format(Constants.UserDumpsUrl, username, pageNumber++));
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