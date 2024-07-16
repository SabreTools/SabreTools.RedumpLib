using System;
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
        public static async Task<bool> DownloadLastSubmitted(RedumpClient rc, string? outDir)
        {
            return await rc.CheckSingleWIPPage(Constants.WipDumpsUrl, outDir, false);
        }

        /// <summary>
        /// Download the specified range of WIP disc pages
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="minId">Starting ID for the range</param>
        /// <param name="maxId">Ending ID for the range (inclusive)</param>
        public static async Task<bool> DownloadWIPRange(RedumpClient rc, string? outDir, int minId = 0, int maxId = 0)
        {
            if (!rc.LoggedIn || !rc.IsStaff)
            {
                Console.WriteLine("WIP download functionality is only available to Redump moderators");
                return false;
            }

            for (int id = minId; id <= maxId; id++)
            {
                if (await rc.DownloadSingleWIPID(id, outDir, true))
                    DelayHelper.DelayRandom(); // Intentional sleep here so we don't flood the server
            }

            return true;
        }
    }
}