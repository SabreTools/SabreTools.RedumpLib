using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;

namespace SabreTools.RedumpLib
{
    public static class Validator
    {
        /// <summary>
        /// Adjust the disc type based on size and layerbreak information
        /// </summary>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <returns>Corrected disc type, if possible</returns>
        public static void NormalizeDiscType(SubmissionInfo info)
        {
            // If we have nothing valid, do nothing
            if (info?.CommonDiscInfo?.Media == null || info?.SizeAndChecksums == null)
                return;

            switch (info.CommonDiscInfo.Media)
            {
                case DiscType.BD25:
                case DiscType.BD33:
                case DiscType.BD50:
                case DiscType.BD66:
                case DiscType.BD100:
                case DiscType.BD128:
                    if (info.SizeAndChecksums.Layerbreak3 != default)
                        info.CommonDiscInfo.Media = DiscType.BD128;
                    else if (info.SizeAndChecksums.Layerbreak2 != default)
                        info.CommonDiscInfo.Media = DiscType.BD100;
                    else if (info.SizeAndChecksums.Layerbreak != default && info.SizeAndChecksums.PICIdentifier == SabreTools.Models.PIC.Constants.DiscTypeIdentifierROMUltra)
                        info.CommonDiscInfo.Media = DiscType.BD66;
                    else if (info.SizeAndChecksums.Layerbreak != default && info.SizeAndChecksums.Size > 50_050_629_632)
                        info.CommonDiscInfo.Media = DiscType.BD66;
                    else if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = DiscType.BD50;
                    else if (info.SizeAndChecksums.PICIdentifier == SabreTools.Models.PIC.Constants.DiscTypeIdentifierROMUltra)
                        info.CommonDiscInfo.Media = DiscType.BD33;
                    else if (info.SizeAndChecksums.Size > 25_025_314_816)
                        info.CommonDiscInfo.Media = DiscType.BD33;
                    else
                        info.CommonDiscInfo.Media = DiscType.BD25;
                    break;

                case DiscType.DVD5:
                case DiscType.DVD9:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = DiscType.DVD9;
                    else
                        info.CommonDiscInfo.Media = DiscType.DVD5;
                    break;

                case DiscType.HDDVDSL:
                case DiscType.HDDVDDL:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = DiscType.HDDVDDL;
                    else
                        info.CommonDiscInfo.Media = DiscType.HDDVDSL;
                    break;

                case DiscType.UMDSL:
                case DiscType.UMDDL:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = DiscType.UMDDL;
                    else
                        info.CommonDiscInfo.Media = DiscType.UMDSL;
                    break;

                // All other disc types are not processed
                default:
                    break;
            }
        }

        /// <summary>
        /// List the disc IDs associated with a given quicksearch query
        /// </summary>
        /// <param name="wc">RedumpWebClient for making the connection</param>
        /// <param name="query">Query string to attempt to search for</param>
        /// <param name="filterForwardSlashes">True to filter forward slashes, false otherwise</param>
        /// <returns>All disc IDs for the given query, null on error</returns>
#if NETFRAMEWORK
        public async static Task<List<int>?> ListSearchResults(RedumpWebClient wc, string? query, bool filterForwardSlashes = true)
#else
        public async static Task<List<int>?> ListSearchResults(RedumpHttpClient wc, string? query, bool filterForwardSlashes = true)
#endif
        {
            // If there is an invalid query
            if (string.IsNullOrEmpty(query))
                return null;

            var ids = new List<int>();

            // Strip quotes
            query = query!.Trim('"', '\'');

            // Special characters become dashes
            query = query.Replace(' ', '-');
            if (filterForwardSlashes)
                query = query.Replace('/', '-');
            query = query.Replace('\\', '/');

            // Lowercase is defined per language
            query = query.ToLowerInvariant();

            // Keep getting quicksearch pages until there are none left
            try
            {
                int pageNumber = 1;
                while (true)
                {
#if NET40
                    List<int> pageIds = await Task.Factory.StartNew(() => wc.CheckSingleSitePage(string.Format(Constants.QuickSearchUrl, query, pageNumber++)));
#elif NETFRAMEWORK
                    List<int> pageIds = await Task.Run(() => wc.CheckSingleSitePage(string.Format(Constants.QuickSearchUrl, query, pageNumber++)));
#else
                    List<int> pageIds = await wc.CheckSingleSitePage(string.Format(Constants.QuickSearchUrl, query, pageNumber++));
#endif
                    ids.AddRange(pageIds);
                    if (pageIds.Count <= 1)
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred while trying to log in: {ex}");
                return null;
            }

            return ids;
        }

        /// <summary>
        /// Validate a single track against Redump, if possible
        /// </summary>
        /// <param name="wc">RedumpWebClient for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <param name="sha1">SHA-1 hash to check against</param>
        /// <returns>True if the track was found, false otherwise; List of found values, if possible</returns>
#if NETFRAMEWORK
        public async static Task<(bool, List<int>?, string?)> ValidateSingleTrack(RedumpWebClient wc, SubmissionInfo info, string sha1)
#else
        public async static Task<(bool, List<int>?, string?)> ValidateSingleTrack(RedumpHttpClient wc, SubmissionInfo info, string sha1)
#endif
        {
            // Get all matching IDs for the track
            var newIds = await ListSearchResults(wc, sha1);

            // If we got null back, there was an error
            if (newIds == null)
                return (false, null, "There was an unknown error retrieving information from Redump");

            // If no IDs match, just return
            if (!newIds.Any())
                return (false, null, $"There were no matching IDs for track with SHA-1 of '{sha1}'");

            // Join the list of found IDs to the existing list, if possible
            if (info.PartiallyMatchedIDs != null && info.PartiallyMatchedIDs.Any())
                info.PartiallyMatchedIDs.AddRange(newIds);
            else
                info.PartiallyMatchedIDs = newIds;

            return (true, newIds, $"There were matching ID(s) found for track with SHA-1 of '{sha1}'");
        }

        /// <summary>
        /// Validate a universal hash against Redump, if possible
        /// </summary>
        /// <param name="wc">RedumpWebClient for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <param name="resultProgress">Optional result progress callback</param>
        /// <returns>True if the track was found, false otherwise; List of found values, if possible</returns>
#if NETFRAMEWORK
        public async static Task<(bool, List<int>?, string?)> ValidateUniversalHash(RedumpWebClient wc, SubmissionInfo info)
#else
        public async static Task<(bool, List<int>?, string?)> ValidateUniversalHash(RedumpHttpClient wc, SubmissionInfo info)
#endif
        {
            // If we don't have special fields
            if (info.CommonDiscInfo?.CommentsSpecialFields == null)
                return (false, null, "Universal hash was missing");

            // If we don't have a universal hash
            string? universalHash = info.CommonDiscInfo.CommentsSpecialFields[SiteCode.UniversalHash];
            if (string.IsNullOrEmpty(universalHash))
                return (false, null, "Universal hash was missing");

            // Format the universal hash for finding within the comments
            string universalHashQuery = $"{universalHash.Substring(0, universalHash.Length - 1)}/comments/only";

            // Get all matching IDs for the hash
            var newIds = await ListSearchResults(wc, universalHashQuery, filterForwardSlashes: false);

            // If we got null back, there was an error
            if (newIds == null)
                return (false, null, "There was an unknown error retrieving information from Redump");

            // If no IDs match, just return
            if (!newIds.Any())
                return (false, null, $"There were no matching IDs for universal hash of '{universalHash}'");

            // Join the list of found IDs to the existing list, if possible
            if (info.PartiallyMatchedIDs != null && info.PartiallyMatchedIDs.Any())
                info.PartiallyMatchedIDs.AddRange(newIds);
            else
                info.PartiallyMatchedIDs = newIds;

            return (true, newIds, $"There were matching ID(s) found for universal hash of '{universalHash}'");
        }

        /// <summary>
        /// Validate that the current track count and remote track count match
        /// </summary>
        /// <param name="wc">RedumpWebClient for making the connection</param>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <param name="localCount">Local count of tracks for the current disc</param>
        /// <returns>True if the track count matches, false otherwise</returns>
#if NETFRAMEWORK
        public async static Task<bool> ValidateTrackCount(RedumpWebClient wc, int id, int localCount)
#else
        public async static Task<bool> ValidateTrackCount(RedumpHttpClient wc, int id, int localCount)
#endif
        {
            // If we can't pull the remote data, we can't match
#if NET40
            string? discData = await Task.Factory.StartNew(() => wc.DownloadSingleSiteID(id));
#elif NETFRAMEWORK
            string? discData = await Task.Run(() => wc.DownloadSingleSiteID(id));
#else
            string? discData = await wc.DownloadSingleSiteID(id);
#endif
            if (string.IsNullOrEmpty(discData))
                return false;

            // Discs with only 1 track don't have a track count listed
            var match = Constants.TrackCountRegex.Match(discData);
            if (!match.Success && localCount == 1)
                return true;
            else if (!match.Success)
                return false;

            // If the count isn't parseable, we're not taking chances
            if (!int.TryParse(match.Groups[1].Value, out int remoteCount))
                return false;

            // Finally check to see if the counts match
            return localCount == remoteCount;
        }
    }
}
