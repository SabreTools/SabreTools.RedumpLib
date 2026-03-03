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
        /// <param name="lastModified">True to sort by last modified descending, false for default sorting</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given user, empty on error</returns>
        public static async Task<List<int>> DownloadUser(this RedumpClient client,
            string? username,
            string? outDir,
            bool lastModified = false,
            int limit = -1)
        {
            return lastModified
                ? await client.DownloadDiscsResults(outDir, dumper: username, sort: SortCategory.Modified, sortDir: SortDirection.Descending, limit: limit)
                : await client.DownloadDiscsResults(outDir, dumper: username, limit: limit);
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
            return await client.ListDiscsResults(dumper: username, limit: limit);
        }
    }
}
