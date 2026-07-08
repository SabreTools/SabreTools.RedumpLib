using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// Contains logic for dealing with queue
    /// </summary>
    public static class Queue
    {
        /// <summary>
        /// Download the queue pages associated with a given queue query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="discId">Add disc ID to filter, null to omit</param>
        /// <param name="isDiscHistory">Set disc history status, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="submitter">Add submitter name to filter, null to omit</param>
        /// <param name="subType">Add submission type to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> DownloadQueueResults(this Client client,
            string? outDir,
            long? discId = null,
            bool? isDiscHistory = null,
            SortDirection? order = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            string? submitter = null,
            SubmissionType? subType = null,
            PhysicalSystem? system = null,
            int limit = -1)
        {
            // Keep getting discs pages until there are none left
            List<int> ids = [];
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    if (limit > 0 && pageNumber > limit)
                        break;

                    var pageIds = await client.CheckSingleQueuePage(
                        outDir,
                        discId,
                        isDiscHistory,
                        order,
                        pageNumber++,
                        sort,
                        status,
                        submitter,
                        subType,
                        system);
                    if (pageIds is null)
                        return [];

                    ids.AddRange(pageIds);
                    if (pageIds.Count == 0)
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

        /// <summary>
        /// Download the specified set of queued disc pages
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="queueIds">Set of queue IDs to download</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>All queue disc IDs in last submitted range, empty on error</returns>
        public static async Task<List<int>> DownloadQueueSet(this Client client, List<int> queueIds, string? outDir)
        {
            List<int> ids = [];
            foreach (int id in queueIds)
            {
                bool downloaded = await client.DownloadSingleQueuePage(id, outDir, rename: true);
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
                bool downloaded = await client.DownloadSingleQueuePage(id, outDir, rename: true);
                if (downloaded)
                {
                    ids.Add(id);
                    DelayHelper.DelayRandom();
                }
            }

            return ids;
        }

        /// <summary>
        /// List the queue IDs associated with a given submission queue query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="discId">Add disc ID to filter, null to omit</param>
        /// <param name="isDiscHistory">Set disc history status, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="submitter">Add submitter name to filter, null to omit</param>
        /// <param name="subType">Add submission type to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All queue IDs for the given query, empty on error</returns>
        public static async Task<List<int>> ListQueueResults(
            this Client client,
            long? discId = null,
            bool? isDiscHistory = null,
            SortDirection? order = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            string? submitter = null,
            SubmissionType? subType = null,
            PhysicalSystem? system = null,
            int limit = -1)
        {
            // Keep getting discs pages until there are none left
            List<int> ids = [];
            try
            {
                int pageNumber = 1;
                while (true)
                {
                    if (limit > 0 && pageNumber > limit)
                        break;

                    var pageIds = await client.CheckSingleQueuePage(
                        discId,
                        isDiscHistory,
                        order,
                        pageNumber++,
                        sort,
                        status,
                        submitter,
                        subType,
                        system);
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
