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
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>All disc IDs in last submitted range, empty on error</returns>
        public static async Task<List<int>> DownloadLastSubmitted(this RedumpClient client, string? outDir)
        {
            return await client.CheckSingleWIPPage(Constants.WipDumpsUrl, outDir, false) ?? [];
        }

        /// <summary>
        /// Download the specified range of WIP disc pages
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="minId">Starting ID for the range</param>
        /// <param name="maxId">Ending ID for the range (inclusive)</param>
        /// <returns>All disc IDs in last submitted range, empty on error</returns>
        public static async Task<List<int>> DownloadWIPRange(this RedumpClient client, string? outDir, int minId = 0, int maxId = 0)
        {
            List<int> ids = [];
            for (int id = minId; id <= maxId; id++)
            {
                bool downloaded = await client.DownloadSingleWIPID(id, outDir, true);
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
