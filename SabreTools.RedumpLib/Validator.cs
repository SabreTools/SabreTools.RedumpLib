using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.RedumpOrg;
using SabreTools.RedumpLib.RedumpOrg.Data;

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
                case MediaType.DVD5:
                case MediaType.DVD9:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.DVD9;
                    else
                        info.CommonDiscInfo.Media = MediaType.DVD5;
                    break;

                case MediaType.BD25:
                case MediaType.BD33:
                case MediaType.BD50:
                case MediaType.BD66:
                case MediaType.BD100:
                case MediaType.BD128:
                    // Extract the size from the hashes
                    long size = Extensions.ExtractSizeFromHashData(info.TracksAndWriteOffsets.ClrMameProData);

                    if (info.SizeAndChecksums.Layerbreak3 != default)
                        info.CommonDiscInfo.Media = MediaType.BD128;
                    else if (info.SizeAndChecksums.Layerbreak2 != default)
                        info.CommonDiscInfo.Media = MediaType.BD100;
                    else if (info.SizeAndChecksums.Layerbreak != default && info.SizeAndChecksums.PICIdentifier == "BDU")
                        info.CommonDiscInfo.Media = MediaType.BD66;
                    else if (info.SizeAndChecksums.Layerbreak != default && size > 50_050_629_632)
                        info.CommonDiscInfo.Media = MediaType.BD66;
                    else if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.BD50;
                    else if (info.SizeAndChecksums.PICIdentifier == "BDU")
                        info.CommonDiscInfo.Media = MediaType.BD33;
                    else if (size > 25_025_314_816)
                        info.CommonDiscInfo.Media = MediaType.BD33;
                    else
                        info.CommonDiscInfo.Media = MediaType.BD25;
                    break;

                case MediaType.HDDVDSL:
                case MediaType.HDDVDDL:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.HDDVDDL;
                    else
                        info.CommonDiscInfo.Media = MediaType.HDDVDSL;
                    break;

                case MediaType.UMDSL:
                case MediaType.UMDDL:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.UMDDL;
                    else
                        info.CommonDiscInfo.Media = MediaType.UMDSL;
                    break;

                // All other disc types are not processed
                default:
                    break;
            }
#pragma warning restore IDE0010
        }

        /// <summary>
        /// Validate a single track against Redump, if possible
        /// </summary>
        /// <param name="client">RedumpClient for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <param name="sha1">SHA-1 hash to check against</param>
        /// <returns>List of found values, if possible</returns>
        public static async Task<List<int>?> ValidateSingleTrack(Client client, SubmissionInfo info, string? sha1)
        {
            // Get all matching IDs for the track
            var newIds = await client.ListDiscsResults(quicksearch: sha1);

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
        /// <param name="client">RedumpClient for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <returns>List of found values, if possible</returns>
        public static async Task<List<int>?> ValidateUniversalHash(Client client, SubmissionInfo info)
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
            string universalHashQuery = universalHash[..^1];
#else
            string universalHashQuery = universalHash.Substring(0, universalHash.Length - 1);
#endif

            // Get all matching IDs for the hash
            var newIds = await client.ListDiscsResults(quicksearch: universalHashQuery, comments: true);

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
        /// <param name="client">RedumpClient for making the connection</param>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <param name="localCount">Local count of tracks for the current disc</param>
        /// <returns>True if the track count matches, false otherwise</returns>
        public static async Task<bool> ValidateTrackCount(Client client, int id, int localCount)
        {
            // If we can't pull the remote data, we can't match
            string? discData = await client.DownloadSingleSiteID(id);
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
