using System;
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
        public static async Task<bool> DownloadLastModified(RedumpClient rc, string? outDir, bool force)
        {
            // Keep getting last modified pages until there are none left
            int pageNumber = 1;
            while (true)
            {
                if (!await rc.CheckSingleSitePage(string.Format(Constants.LastModifiedUrl, pageNumber++), outDir, !force))
                    break;
            }

            return true;
        }

        /// <summary>
        /// Download the specified range of site disc pages
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="minId">Starting ID for the range</param>
        /// <param name="maxId">Ending ID for the range (inclusive)</param>
        public static async Task<bool> DownloadSiteRange(RedumpClient rc, string? outDir, int minId = 0, int maxId = 0)
        {
            if (!rc.LoggedIn)
            {
                Console.WriteLine("Site download functionality is only available to Redump members");
                return false;
            }

            for (int id = minId; id <= maxId; id++)
            {
                if (await rc.DownloadSingleSiteID(id, outDir, true))
                    DelayHelper.DelayRandom(); // Intentional sleep here so we don't flood the server
            }

            return true;
        }
    }
}