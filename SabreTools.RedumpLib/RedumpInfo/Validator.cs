using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;
using SubmissionInfo = SabreTools.RedumpLib.RedumpOrg.SubmissionInfo;

namespace SabreTools.RedumpLib.RedumpInfo
{
    public static class Validator
    {
        /// <summary>
        /// Validate a single track against Redump, if possible
        /// </summary>
        /// <param name="client">RedumpClient for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <param name="sha1">SHA-1 hash to check against</param>
        /// <returns>List of found values, if possible</returns>
        public static async Task<List<int>?> ValidateSingleTrack(Client client, SubmissionInfo info, string? sha1)
        {
            // Get all matching IDs for the trackClient
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
            var newIds = await client.ListDiscsResults(comments: universalHashQuery);

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
    }
}
