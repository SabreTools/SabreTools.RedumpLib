using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// Contains logic for dealing with WIP queue
    /// </summary>
    public static class WIP
    {
        /// <summary>
        /// Download the last submitted WIP disc pages
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>All disc IDs in last submitted range, empty on error</returns>
        public static async Task<List<int>> DownloadLastSubmitted(this RedumpClient rc, string? outDir)
        {
            return await rc.CheckSingleWIPPage(Constants.WipDumpsUrl, outDir, false) ?? [];
        }

        /// <summary>
        /// Download the specified range of WIP disc pages
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="minId">Starting ID for the range</param>
        /// <param name="maxId">Ending ID for the range (inclusive)</param>
        /// <returns>All disc IDs in last submitted range, empty on error</returns>
        public static async Task<List<int>> DownloadWIPRange(this RedumpClient rc, string? outDir, int minId = 0, int maxId = 0)
        {
            List<int> ids = [];

            if (!rc.LoggedIn || !rc.IsStaff)
            {
                Console.WriteLine("WIP download functionality is only available to Redump moderators");
                return ids;
            }

            for (int id = minId; id <= maxId; id++)
            {
                ids.Add(id);
                if (await rc.DownloadSingleWIPID(id, outDir, true))
                    DelayHelper.DelayRandom(); // Intentional sleep here so we don't flood the server
            }

            return ids;
        }
    }
}
