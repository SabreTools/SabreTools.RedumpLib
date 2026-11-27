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
        /// <param name="additionalMould">Add additional mould to filter, null to omit</param>
        /// <param name="advanced">Set advanced search status, null to omit</param>
        /// <param name="barcode">Add barcode to filter, null to omit</param>
        /// <param name="barcodeMode">Set barcode handling, null to omit</param>
        /// <param name="category">Add category to filter, null to omit</param>
        /// <param name="comments">Add comments to filter, null to omit</param>
        /// <param name="contents">Add contents to filter, null to omit</param>
        /// <param name="discNumber">Add disc number to filter, null to omit</param>
        /// <param name="discTitle">Add disc title to filter, null to omit</param>
        /// <param name="discTitleMode">Set disc title handling, null to omit</param>
        /// <param name="discTitleForeign">Add foreign disc title to filter, null to omit</param>
        /// <param name="discTitleForeignMode">Set foreign disc title handling, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edc">Add EDC status to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="editionMode">Set edition handling, null to omit</param>
        /// <param name="errorsMax">Add maximum error count to filter, null to omit</param>
        /// <param name="errorsMin">Add minimum error count to filter, null to omit</param>
        /// <param name="exeDateMax">Add maximum EXE date to filter, null to omit</param>
        /// <param name="exeDateMin">Add minimum EXE date to filter, null to omit</param>
        /// <param name="filenameSuffix">Add filename suffix to filter, null to omit</param>
        /// <param name="filenameSuffixMode">Set filename suffix handling, null to omit</param>
        /// <param name="internalSerial">Add internal serial to filter, null to omit</param>
        /// <param name="internalSerialMode">Set internal serial handling, null to omit</param>
        /// <param name="language">Add language to filter, null to omit</param>
        /// <param name="letter">Starts with upper-case letter or '#' for numbers, null to omit</param>
        /// <param name="masteringCode">Add mastering code to filter, null to omit</param>
        /// <param name="masteringSid">Add mastering SID to filter, null to omit</param>
        /// <param name="media">Add media type to filter, null to omit</param>
        /// <param name="mouldSid">Add mould SID to filter, null to omit</param>
        /// <param name="notTags">Add not disc tags to filter, null to omit</param>
        /// <param name="offsetMax">Add maximum offset to filter, null to omit</param>
        /// <param name="offsetMin">Add minimum offset to filter, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="protection">Add protection to filter, null to omit</param>
        /// <param name="pvdDateMax">Add maximum PVD date to filter, null to omit</param>
        /// <param name="pvdDateMin">Add minimum PVD date to filter, null to omit</param>
        /// <param name="query">Generic text query to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="serial">Add serial to filter, null to omit</param>
        /// <param name="serialMode">Set serial handling, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="tags">Add disc tags to filter, null to omit</param>
        /// <param name="tagMode">Add disc tags mode to filter, null to omit</param>
        /// <param name="title">Add title to filter, null to omit</param>
        /// <param name="titleMode">Set title handling, null to omit</param>
        /// <param name="titleForeign">Add foreign title to filter, null to omit</param>
        /// <param name="titleForeignMode">Set foreign title handling, null to omit</param>
        /// <param name="toolstamp">Add toolstamp to filter, null to omit</param>
        /// <param name="tracksMax">Add maximum track count to filter, null to omit</param>
        /// <param name="tracksMin">Add minimum track count to filter, null to omit</param>
        /// <param name="universalHash">Add universal hash to filter, null to omit</param>
        /// <param name="version">Add version to filter, null to omit</param>
        /// <param name="versionMode">Set version handling, null to omit</param>
        /// <param name="volumeLabel">Add volume label to filter, null to omit</param>
        /// <param name="volumeLabelMode">Set volume label handling, null to omit</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <param name="discSubpaths">Set of subpaths to download if available, null for all</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> DownloadDiscsResults(
            this Client client,
            string? outDir,
            string? additionalMould = null,
            bool? advanced = null,
            string? barcode = null,
            string? barcodeMode = null,
            DiscCategory? category = null,
            string? comments = null,
            string? contents = null,
            string? discNumber = null,
            string? discTitle = null,
            string? discTitleMode = null,
            string? discTitleForeign = null,
            string? discTitleForeignMode = null,
            string? dumper = null,
            YesNo? edc = null,
            string? edition = null,
            string? editionMode = null,
            long? errorsMax = null,
            long? errorsMin = null,
            string? exeDateMax = null,
            string? exeDateMin = null,
            string? filenameSuffix = null,
            string? filenameSuffixMode = null,
            string? internalSerial = null,
            string? internalSerialMode = null,
            LanguageCode? language = null,
            char? letter = null,
            string? masteringCode = null,
            string? masteringSid = null,
            MediaType? media = null,
            string? mouldSid = null,
            DiscTag[]? notTags = null,
            long? offsetMax = null,
            long? offsetMin = null,
            SortDirection? order = null,
            string? protection = null,
            string? pvdDateMax = null,
            string? pvdDateMin = null,
            string? query = null,
            RegionCode? region = null,
            string? serial = null,
            string? serialMode = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            PhysicalSystem? system = null,
            string? tagMode = null,
            DiscTag[]? tags = null,
            string? title = null,
            string? titleMode = null,
            string? titleForeign = null,
            string? titleForeignMode = null,
            string? toolstamp = null,
            long? tracksMax = null,
            long? tracksMin = null,
            string? universalHash = null,
            string? version = null,
            string? versionMode = null,
            string? volumeLabel = null,
            string? volumeLabelMode = null,
            int limit = -1,
            DiscSubpath[]? discSubpaths = null)
        {
            // Keep getting discs pages until there are none left
            List<int> ids = [];

            int pageNumber = 1;
            while (true)
            {
                if (limit > 0 && pageNumber > limit)
                    break;

                var endpoint = new Endpoints.Discs
                {
                    System = system,
                    Region = region,
                    Language = language,
                    Media = media,
                    Category = category,
                    Tags = tags,
                    TagMode = tagMode,
                    NotTags = notTags,
                    Status = status,
                    Letter = letter,
                    Dumper = dumper,
                    Title = title,
                    TitleMode = titleMode,
                    TitleForeign = titleForeign,
                    TitleForeignMode = titleForeignMode,
                    DiscNumber = discNumber,
                    DiscTitle = discTitle,
                    DiscTitleMode = discTitleMode,
                    DiscTitleForeign = discTitleForeign,
                    DiscTitleForeignMode = discTitleForeignMode,
                    Serial = serial,
                    SerialMode = serialMode,
                    FilenameSuffix = filenameSuffix,
                    FilenameSuffixMode = filenameSuffixMode,
                    ExeDateMin = exeDateMin,
                    ExeDateMax = exeDateMax,
                    PvdDateMin = pvdDateMin,
                    PvdDateMax = pvdDateMax,
                    Edition = edition,
                    EditionMode = editionMode,
                    Barcode = barcode,
                    BarcodeMode = barcodeMode,
                    UniversalHash = universalHash,
                    TracksMin = tracksMin,
                    TracksMax = tracksMax,
                    ErrorsMin = errorsMin,
                    ErrorsMax = errorsMax,
                    Edc = edc,
                    Protection = protection,
                    Comments = comments,
                    Contents = contents,
                    MasteringCode = masteringCode,
                    MasteringSid = masteringSid,
                    Toolstamp = toolstamp,
                    MouldSid = mouldSid,
                    AdditionalMould = additionalMould,
                    OffsetMin = offsetMin,
                    OffsetMax = offsetMax,
                    VolumeLabel = volumeLabel,
                    VolumeLabelMode = volumeLabelMode,
                    InternalSerial = internalSerial,
                    InternalSerialMode = internalSerialMode,
                    Version = version,
                    VersionMode = versionMode,
                    Query = query,
                    Sort = sort,
                    Order = order,
                    Page = pageNumber++,
                    Advanced = advanced,
                };

                var pageIds = await client.CheckSingleDiscsEndpoint(outDir, endpoint, discSubpaths);
                if (pageIds is null)
                    return [];

                // If all page IDs are already found, assume the last page was reached
                if (pageIds.TrueForAll(ids.Contains))
                    break;

                ids.AddRange(pageIds);
                if (pageIds.Count == 0)
                    break;
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
                var endpoint = new Endpoints.Disc { Id = id };
                bool downloaded = await client.DownloadSingleDiscEndpoint(endpoint, outDir, rename: true, discSubpaths);
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
                var endpoint = new Endpoints.Disc { Id = id };
                bool downloaded = await client.DownloadSingleDiscEndpoint(endpoint, outDir, rename: true, discSubpaths);
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
        /// <param name="additionalMould">Add additional mould to filter, null to omit</param>
        /// <param name="advanced">Set advanced search status, null to omit</param>
        /// <param name="barcode">Add barcode to filter, null to omit</param>
        /// <param name="barcodeMode">Set barcode handling, null to omit</param>
        /// <param name="category">Add category to filter, null to omit</param>
        /// <param name="comments">Add comments to filter, null to omit</param>
        /// <param name="contents">Add contents to filter, null to omit</param>
        /// <param name="discNumber">Add disc number to filter, null to omit</param>
        /// <param name="discTitle">Add disc title to filter, null to omit</param>
        /// <param name="discTitleMode">Set disc title handling, null to omit</param>
        /// <param name="discTitleForeign">Add foreign disc title to filter, null to omit</param>
        /// <param name="discTitleForeignMode">Set foreign disc title handling, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edc">Add EDC status to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="editionMode">Set edition handling, null to omit</param>
        /// <param name="errorsMax">Add maximum error count to filter, null to omit</param>
        /// <param name="errorsMin">Add minimum error count to filter, null to omit</param>
        /// <param name="exeDateMax">Add maximum EXE date to filter, null to omit</param>
        /// <param name="exeDateMin">Add minimum EXE date to filter, null to omit</param>
        /// <param name="filenameSuffix">Add filename suffix to filter, null to omit</param>
        /// <param name="filenameSuffixMode">Set filename suffix handling, null to omit</param>
        /// <param name="internalSerial">Add internal serial to filter, null to omit</param>
        /// <param name="internalSerialMode">Set internal serial handling, null to omit</param>
        /// <param name="language">Add language to filter, null to omit</param>
        /// <param name="letter">Starts with upper-case letter or '#' for numbers, null to omit</param>
        /// <param name="masteringCode">Add mastering code to filter, null to omit</param>
        /// <param name="masteringSid">Add mastering SID to filter, null to omit</param>
        /// <param name="media">Add media type to filter, null to omit</param>
        /// <param name="mouldSid">Add mould SID to filter, null to omit</param>
        /// <param name="notTags">Add not disc tags to filter, null to omit</param>
        /// <param name="offsetMax">Add maximum offset to filter, null to omit</param>
        /// <param name="offsetMin">Add minimum offset to filter, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="protection">Add protection to filter, null to omit</param>
        /// <param name="pvdDateMax">Add maximum PVD date to filter, null to omit</param>
        /// <param name="pvdDateMin">Add minimum PVD date to filter, null to omit</param>
        /// <param name="query">Generic text query to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="serial">Add serial to filter, null to omit</param>
        /// <param name="serialMode">Set serial handling, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="tags">Add disc tags to filter, null to omit</param>
        /// <param name="tagMode">Add disc tags mode to filter, null to omit</param>
        /// <param name="title">Add title to filter, null to omit</param>
        /// <param name="titleMode">Set title handling, null to omit</param>
        /// <param name="titleForeign">Add foreign title to filter, null to omit</param>
        /// <param name="titleForeignMode">Set foreign title handling, null to omit</param>
        /// <param name="toolstamp">Add toolstamp to filter, null to omit</param>
        /// <param name="tracksMax">Add maximum track count to filter, null to omit</param>
        /// <param name="tracksMin">Add minimum track count to filter, null to omit</param>
        /// <param name="universalHash">Add universal hash to filter, null to omit</param>
        /// <param name="version">Add version to filter, null to omit</param>
        /// <param name="versionMode">Set version handling, null to omit</param>
        /// <param name="volumeLabel">Add volume label to filter, null to omit</param>
        /// <param name="volumeLabelMode">Set volume label handling, null to omit</param>
        /// <param name="limit">Limit number of retrieved result pages, non-positive for unlimited</param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> ListDiscsResults(
            this Client client,
            string? additionalMould = null,
            bool? advanced = null,
            string? barcode = null,
            string? barcodeMode = null,
            DiscCategory? category = null,
            string? comments = null,
            string? contents = null,
            string? discNumber = null,
            string? discTitle = null,
            string? discTitleMode = null,
            string? discTitleForeign = null,
            string? discTitleForeignMode = null,
            string? dumper = null,
            YesNo? edc = null,
            string? edition = null,
            string? editionMode = null,
            long? errorsMax = null,
            long? errorsMin = null,
            string? exeDateMax = null,
            string? exeDateMin = null,
            string? filenameSuffix = null,
            string? filenameSuffixMode = null,
            string? internalSerial = null,
            string? internalSerialMode = null,
            LanguageCode? language = null,
            char? letter = null,
            string? masteringCode = null,
            string? masteringSid = null,
            MediaType? media = null,
            string? mouldSid = null,
            DiscTag[]? notTags = null,
            long? offsetMax = null,
            long? offsetMin = null,
            SortDirection? order = null,
            string? protection = null,
            string? pvdDateMax = null,
            string? pvdDateMin = null,
            string? query = null,
            RegionCode? region = null,
            string? serial = null,
            string? serialMode = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            PhysicalSystem? system = null,
            string? tagMode = null,
            DiscTag[]? tags = null,
            string? title = null,
            string? titleMode = null,
            string? titleForeign = null,
            string? titleForeignMode = null,
            string? toolstamp = null,
            long? tracksMax = null,
            long? tracksMin = null,
            string? universalHash = null,
            string? version = null,
            string? versionMode = null,
            string? volumeLabel = null,
            string? volumeLabelMode = null,
            int limit = -1)
        {
            // Keep getting discs pages until there are none left
            List<int> ids = [];

            int pageNumber = 1;
            while (true)
            {
                if (limit > 0 && pageNumber > limit)
                    break;

                var endpoint = new Endpoints.Discs
                {
                    System = system,
                    Region = region,
                    Language = language,
                    Media = media,
                    Category = category,
                    Tags = tags,
                    TagMode = tagMode,
                    NotTags = notTags,
                    Status = status,
                    Letter = letter,
                    Dumper = dumper,
                    Title = title,
                    TitleMode = titleMode,
                    TitleForeign = titleForeign,
                    TitleForeignMode = titleForeignMode,
                    DiscNumber = discNumber,
                    DiscTitle = discTitle,
                    DiscTitleMode = discTitleMode,
                    DiscTitleForeign = discTitleForeign,
                    DiscTitleForeignMode = discTitleForeignMode,
                    Serial = serial,
                    SerialMode = serialMode,
                    FilenameSuffix = filenameSuffix,
                    FilenameSuffixMode = filenameSuffixMode,
                    ExeDateMin = exeDateMin,
                    ExeDateMax = exeDateMax,
                    PvdDateMin = pvdDateMin,
                    PvdDateMax = pvdDateMax,
                    Edition = edition,
                    EditionMode = editionMode,
                    Barcode = barcode,
                    BarcodeMode = barcodeMode,
                    UniversalHash = universalHash,
                    TracksMin = tracksMin,
                    TracksMax = tracksMax,
                    ErrorsMin = errorsMin,
                    ErrorsMax = errorsMax,
                    Edc = edc,
                    Protection = protection,
                    Comments = comments,
                    Contents = contents,
                    MasteringCode = masteringCode,
                    MasteringSid = masteringSid,
                    Toolstamp = toolstamp,
                    MouldSid = mouldSid,
                    AdditionalMould = additionalMould,
                    OffsetMin = offsetMin,
                    OffsetMax = offsetMax,
                    VolumeLabel = volumeLabel,
                    VolumeLabelMode = volumeLabelMode,
                    InternalSerial = internalSerial,
                    InternalSerialMode = internalSerialMode,
                    Version = version,
                    VersionMode = versionMode,
                    Query = query,
                    Sort = sort,
                    Order = order,
                    Page = pageNumber++,
                    Advanced = advanced,
                };

                var pageIds = await client.CheckSingleDiscsEndpoint(endpoint);
                if (pageIds is null)
                    return [];

                // If all page IDs are already found, assume the last page was reached
                if (pageIds.TrueForAll(ids.Contains))
                    break;

                ids.AddRange(pageIds);
                if (pageIds.Count <= 1)
                    break;
            }

            return ids;
        }

        /// <summary>
        /// List the disc IDs associated with a given discs API query
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="crc32">Use CRC-32 value for searching; incomplatible with <paramref name="md5"/> and <paramref name="sha1"/></param>
        /// <param name="md5">Use MD5 value for searching; incomplatible with <paramref name="crc32"/> and <paramref name="sha1"/></param>
        /// <param name="sha1">Use CRC-32 value for searching; incomplatible with <paramref name="crc32"/> and <paramref name="md5"/></param>
        /// <returns>All disc IDs for the given query, empty on error</returns>
        public static async Task<List<int>> ListDiscsApiResults(
            this Client client,
            string? crc32 = null,
            string? md5 = null,
            string? sha1 = null)
        {
            // If there is an invalid combination of hashes
            if (crc32 is null && md5 is null && sha1 is null)
                return [];
            else if (crc32 is not null && (md5 is not null || sha1 is not null))
                return [];
            else if (md5 is not null && (crc32 is not null || sha1 is not null))
                return [];
            else if (sha1 is not null && (crc32 is not null || md5 is not null))
                return [];

            var endpoint = new Endpoints.Api
            {
                Subpath = ApiSubpath.Discs,
                Crc32 = crc32,
                Md5 = md5,
                Sha1 = sha1,
            };

            return await client.CheckSingleDiscsApiEndpoint(endpoint) ?? [];
        }
    }
}
