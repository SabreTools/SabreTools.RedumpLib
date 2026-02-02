using System;
using System.Collections.Generic;
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
            if (info.CommonDiscInfo.Media is null || info.SizeAndChecksums == default)
                return;

#pragma warning disable IDE0010
            switch (info.CommonDiscInfo.Media)
            {
                case DiscType.DVD5:
                case DiscType.DVD9:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = DiscType.DVD9;
                    else
                        info.CommonDiscInfo.Media = DiscType.DVD5;
                    break;

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
                    else if (info.SizeAndChecksums.Layerbreak != default && info.SizeAndChecksums.PICIdentifier == "BDU")
                        info.CommonDiscInfo.Media = DiscType.BD66;
                    else if (info.SizeAndChecksums.Layerbreak != default && info.SizeAndChecksums.Size > 50_050_629_632)
                        info.CommonDiscInfo.Media = DiscType.BD66;
                    else if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = DiscType.BD50;
                    else if (info.SizeAndChecksums.PICIdentifier == "BDU")
                        info.CommonDiscInfo.Media = DiscType.BD33;
                    else if (info.SizeAndChecksums.Size > 25_025_314_816)
                        info.CommonDiscInfo.Media = DiscType.BD33;
                    else
                        info.CommonDiscInfo.Media = DiscType.BD25;
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
#pragma warning restore IDE0010
        }

        /// <summary>
        /// List the disc IDs associated with a given quicksearch query
        /// </summary>
        /// <param name="rc">RedumpClient for making the connection</param>
        /// <param name="query">Query string to attempt to search for</param>
        /// <param name="filterForwardSlashes">True to filter forward slashes, false otherwise</param>
        /// <returns>All disc IDs for the given query, null on error</returns>
        public static async Task<List<int>?> ListSearchResults(RedumpClient rc, string? query, bool filterForwardSlashes = true)
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
                    List<int> pageIds = await rc.CheckSingleSitePage(string.Format(Constants.QuickSearchUrl, query, pageNumber++));
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
        /// <param name="rc">RedumpClient for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <param name="sha1">SHA-1 hash to check against</param>
        /// <returns>List of found values, if possible</returns>
        public static async Task<List<int>?> ValidateSingleTrack(RedumpClient rc, SubmissionInfo info, string? sha1)
        {
            // Get all matching IDs for the track
            var newIds = await ListSearchResults(rc, sha1);

            // If we got null back, there was an error
            if (newIds is null)
                return null;

            // If no IDs match, return an empty list
            if (newIds.Count == 0)
                return [];

            // Join the list of found IDs to the existing list, if possible
            if (info.PartiallyMatchedIDs is not null && info.PartiallyMatchedIDs.Count > 0)
                info.PartiallyMatchedIDs.AddRange(newIds);
            else
                info.PartiallyMatchedIDs = newIds;

            return newIds;
        }

        /// <summary>
        /// Validate a universal hash against Redump, if possible
        /// </summary>
        /// <param name="rc">RedumpClient for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <returns>List of found values, if possible</returns>
        public static async Task<List<int>?> ValidateUniversalHash(RedumpClient rc, SubmissionInfo info)
        {
            // If we don't have special fields
            if (info.CommonDiscInfo.CommentsSpecialFields is null)
                return null;

            // If we don't have a universal hash
            string? universalHash = info.CommonDiscInfo.CommentsSpecialFields[SiteCode.UniversalHash];
            if (string.IsNullOrEmpty(universalHash))
                return null;

            // Format the universal hash for finding within the comments
#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
            string universalHashQuery = $"{universalHash[..^1]}/comments/only";
#else
            string universalHashQuery = $"{universalHash.Substring(0, universalHash.Length - 1)}/comments/only";
#endif

            // Get all matching IDs for the hash
            var newIds = await ListSearchResults(rc, universalHashQuery, filterForwardSlashes: false);

            // If we got null back, there was an error
            if (newIds is null)
                return null;

            // If no IDs match, just an empty list
            if (newIds.Count == 0)
                return [];

            // Join the list of found IDs to the existing list, if possible
            if (info.PartiallyMatchedIDs is not null && info.PartiallyMatchedIDs.Count > 0)
                info.PartiallyMatchedIDs.AddRange(newIds);
            else
                info.PartiallyMatchedIDs = newIds;

            return newIds;
        }

        /// <summary>
        /// Validate that the current track count and remote track count match
        /// </summary>
        /// <param name="rc">RedumpClient for making the connection</param>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <param name="localCount">Local count of tracks for the current disc</param>
        /// <returns>True if the track count matches, false otherwise</returns>
        public static async Task<bool> ValidateTrackCount(RedumpClient rc, int id, int localCount)
        {
            // If we can't pull the remote data, we can't match
            string? discData = await rc.DownloadSingleSiteID(id);
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
