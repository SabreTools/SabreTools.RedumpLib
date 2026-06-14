using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Web;

namespace SabreTools.RedumpLib.RedumpInfo
{
    /// <summary>
    /// Contains logic for dealing with queue
    /// </summary>
    public static class Queue
    {
        /// <summary>
        /// Download the last submitted queued disc pages
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>All queue disc IDs in last submitted range, empty on error</returns>
        public static async Task<List<int>> DownloadLastSubmitted(this Client client, string? outDir)
        {
            return await client.CheckSingleWIPPage(outDir) ?? [];
        }

        /// <summary>
        /// Download the specified set of queued disc pages
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="wipIds">Set of queue IDs to download</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>All queue disc IDs in last submitted range, empty on error</returns>
        public static async Task<List<int>> DownloadQueueSet(this Client client, List<int> wipIds, string? outDir)
        {
            List<int> ids = [];
            foreach (int id in wipIds)
            {
                bool downloaded = await client.DownloadSingleWIPID(id, outDir, rename: true);
                if (downloaded)
                {
                    ids.Add(id);
                    DelayHelper.DelayRandom();
                }
            }

            return ids;
        }

        /// <summary>
        /// Download the specified range of queue disc pages
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="minId">Starting ID for the range</param>
        /// <param name="maxId">Ending ID for the range (inclusive)</param>
        /// <returns>All queue disc IDs that successfully downloaded, empty on error</returns>
        public static async Task<List<int>> DownloadQueueRange(this Client client, string? outDir, int minId, int maxId)
        {
            List<int> ids = [];
            for (int id = minId; id <= maxId; id++)
            {
                bool downloaded = await client.DownloadSingleWIPID(id, outDir, rename: true);
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
