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
        /// Download the last modified disc pages, until first failure
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="force">Force continuation of download</param>
        /// <returns>All disc IDs in last modified range, empty on error</returns>
        public static async Task<List<int>> DownloadLastModified(RedumpClient rc, string? outDir, bool force)
        {
            List<int> ids = [];

            // Keep getting last modified pages until there are none left
            int pageNumber = 1;
            while (true)
            {
                var pageIds = await rc.CheckSingleSitePage(string.Format(Constants.LastModifiedUrl, pageNumber++), outDir, !force);
                ids.AddRange(pageIds);
                if (pageIds.Count == 0)
                    break;
            }

            return ids;
        }

        /// <summary>
        /// Download the specified range of site disc pages
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="minId">Starting ID for the range</param>
        /// <param name="maxId">Ending ID for the range (inclusive)</param>
        /// <returns>All disc IDs in last modified range, empty on error</returns>
        public static async Task<List<int>> DownloadSiteRange(RedumpClient rc, string? outDir, int minId = 0, int maxId = 0)
        {
            List<int> ids = [];

            if (!rc.LoggedIn)
            {
                Console.WriteLine("Site download functionality is only available to Redump members");
                return ids;
            }

            for (int id = minId; id <= maxId; id++)
            {
                ids.Add(id);
                if (await rc.DownloadSingleSiteID(id, outDir, true))
                    DelayHelper.DelayRandom(); // Intentional sleep here so we don't flood the server
            }

            return ids;
        }
    }
}
