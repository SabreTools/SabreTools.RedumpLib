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
        /// Download the disc pages associated with a given discs query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="advanced">Set advanced search status, null to omit</param>
        /// <param name="barcode">Add barcode to filter, null to omit</param>
        /// <param name="barcodeExact">Set exact barcode handling, null to omit</param>
        /// <param name="category">Add category to filter, null to omit</param>
        /// <param name="comments">Add comments to filter, null to omit</param>
        /// <param name="contents">Add contents to filter, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edc">Add EDC status to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="editionExact">Set exact edition handling, null to omit</param>
        /// <param name="errorsMax">Add maximum error count to filter, null to omit</param>
        /// <param name="errorsMin">Add minimum error count to filter, null to omit</param>
        /// <param name="language">Add language to filter, null to omit</param>
        /// <param name="letter">Starts with upper-case letter or '#' for numbers, null to omit</param>
        /// <param name="media">Add media type to filter, null to omit</param>
        /// <param name="offset">Add offset to filter, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="protection">Add protection to filter, null to omit</param>
        /// <param name="query">Generic text query to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="ringcode">Add ringcode to filter, null to omit</param>
        /// <param name="serial">Add serial to filter, null to omit</param>
        /// <param name="serialExact">Set exact serial handling, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="title">Add title to filter, null to omit</param>
        /// <param name="titleExact">Set exact title handling, null to omit</param>
        /// <param name="titleForeign">Add foreign title to filter, null to omit</param>
        /// <param name="titleForeignExact">Set exact foreign title handling, null to omit</param>
        /// <param name="tracksMax">Add maximum track count to filter, null to omit</param>
        /// <param name="tracksMin">Add minimum track count to filter, null to omit</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <param name="discSubpaths">Set of subpaths to download if available, null for all</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> DownloadDiscsResults(this Client client,
            string? outDir,
            bool? advanced = null,
            string? barcode = null,
            bool? barcodeExact = null,
            DiscCategory? category = null,
            string? comments = null,
            string? contents = null,
            string? dumper = null,
            YesNo? edc = null,
            string? edition = null,
            bool? editionExact = null,
            long? errorsMax = null,
            long? errorsMin = null,
            LanguageCode? language = null,
            char? letter = null,
            MediaType? media = null,
            long? offset = null,
            SortDirection? order = null,
            string? protection = null,
            string? query = null,
            RegionCode? region = null,
            string? ringcode = null,
            string? serial = null,
            bool? serialExact = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            PhysicalSystem? system = null,
            string? title = null,
            bool? titleExact = null,
            string? titleForeign = null,
            bool? titleForeignExact = null,
            long? tracksMax = null,
            long? tracksMin = null,
            int limit = -1,
            DiscSubpath[]? discSubpaths = null)
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

                    var pageIds = await client.CheckSingleDiscsPage(
                        outDir,
                        advanced,
                        barcode,
                        barcodeExact,
                        category,
                        comments,
                        contents,
                        dumper,
                        edc,
                        edition,
                        editionExact,
                        errorsMax,
                        errorsMin,
                        language,
                        letter,
                        media,
                        offset,
                        order,
                        pageNumber++,
                        protection,
                        query,
                        region,
                        ringcode,
                        serial,
                        serialExact,
                        sort,
                        status,
                        system,
                        title,
                        titleExact,
                        titleForeign,
                        titleForeignExact,
                        tracksMax,
                        tracksMin,
                        discSubpaths);
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
        /// Download the specified range of site disc pages
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="minId">Starting ID for the range</param>
        /// <param name="maxId">Ending ID for the range (inclusive)</param>
        /// <param name="discSubpaths">Set of subpaths to download if available, null for all</param>
        /// <returns>All disc IDs that successfully downloaded, empty on error</returns>
        public static async Task<List<int>> DownloadSiteRange(this Client client,
            string? outDir,
            int minId,
            int maxId,
            DiscSubpath[]? discSubpaths = null)
        {
            List<int> ids = [];
            for (int id = minId; id <= maxId; id++)
            {
                bool downloaded = await client.DownloadSingleDiscPage(id, outDir, rename: true, discSubpaths);
                if (downloaded)
                {
                    ids.Add(id);
                    DelayHelper.DelayRandom();
                }
            }

            return ids;
        }

        /// <summary>
        /// Download the specified set of site disc pages
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="siteIds">Set of site IDs to download</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="discSubpaths">Set of subpaths to download if available, null for all</param>
        /// <returns>All disc IDs that successfully downloaded, empty on error</returns>
        public static async Task<List<int>> DownloadSiteSet(this Client client,
            List<int> siteIds,
            string? outDir,
            DiscSubpath[]? discSubpaths = null)
        {
            List<int> ids = [];
            foreach (int id in siteIds)
            {
                bool downloaded = await client.DownloadSingleDiscPage(id, outDir, rename: true, discSubpaths);
                if (downloaded)
                {
                    ids.Add(id);
                    DelayHelper.DelayRandom();
                }
            }

            return ids;
        }

        /// <summary>
        /// List the disc IDs associated with a given discs query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="advanced">Set advanced search status, null to omit</param>
        /// <param name="barcode">Add barcode to filter, null to omit</param>
        /// <param name="barcodeExact">Set exact barcode handling, null to omit</param>
        /// <param name="category">Add category to filter, null to omit</param>
        /// <param name="comments">Add comments to filter, null to omit</param>
        /// <param name="contents">Add contents to filter, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edc">Add EDC status to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="editionExact">Set exact edition handling, null to omit</param>
        /// <param name="errorsMax">Add maximum error count to filter, null to omit</param>
        /// <param name="errorsMin">Add minimum error count to filter, null to omit</param>
        /// <param name="language">Add language to filter, null to omit</param>
        /// <param name="letter">Starts with upper-case letter or '#' for numbers, null to omit</param>
        /// <param name="media">Add media type to filter, null to omit</param>
        /// <param name="offset">Add offset to filter, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="protection">Add protection to filter, null to omit</param>
        /// <param name="query">Generic text query to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="ringcode">Add ringcode to filter, null to omit</param>
        /// <param name="serial">Add serial to filter, null to omit</param>
        /// <param name="serialExact">Set exact serial handling, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="title">Add title to filter, null to omit</param>
        /// <param name="titleExact">Set exact title handling, null to omit</param>
        /// <param name="titleForeign">Add foreign title to filter, null to omit</param>
        /// <param name="titleForeignExact">Set exact foreign title handling, null to omit</param>
        /// <param name="tracksMax">Add maximum track count to filter, null to omit</param>
        /// <param name="tracksMin">Add minimum track count to filter, null to omit</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> ListDiscsResults(
            this Client client,
            bool? advanced = null,
            string? barcode = null,
            bool? barcodeExact = null,
            DiscCategory? category = null,
            string? comments = null,
            string? contents = null,
            string? dumper = null,
            YesNo? edc = null,
            string? edition = null,
            bool? editionExact = null,
            long? errorsMax = null,
            long? errorsMin = null,
            LanguageCode? language = null,
            char? letter = null,
            MediaType? media = null,
            long? offset = null,
            SortDirection? order = null,
            string? protection = null,
            string? query = null,
            RegionCode? region = null,
            string? ringcode = null,
            string? serial = null,
            bool? serialExact = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            PhysicalSystem? system = null,
            string? title = null,
            bool? titleExact = null,
            string? titleForeign = null,
            bool? titleForeignExact = null,
            long? tracksMax = null,
            long? tracksMin = null,
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

                    var pageIds = await client.CheckSingleDiscsPage(
                        advanced,
                        barcode,
                        barcodeExact,
                        category,
                        comments,
                        contents,
                        dumper,
                        edc,
                        edition,
                        editionExact,
                        errorsMax,
                        errorsMin,
                        language,
                        letter,
                        media,
                        offset,
                        order,
                        pageNumber++,
                        protection,
                        query,
                        region,
                        ringcode,
                        serial,
                        serialExact,
                        sort,
                        status,
                        system,
                        title,
                        titleExact,
                        titleForeign,
                        titleForeignExact,
                        tracksMax,
                        tracksMin);
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
