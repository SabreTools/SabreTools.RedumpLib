using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;

namespace SabreTools.RedumpLib.Tools
{
    /// <summary>
    /// Validation extension methods for <see cref="Client"/>
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validate a single track against Redump, if possible
        /// </summary>
        /// <param name="client">RedumpClient for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <param name="sha1">SHA-1 hash to check against</param>
        /// <returns>List of found values, if possible</returns>
        public static async Task<List<int>?> ValidateSingleTrack(this Client client, SubmissionInfo info, string? sha1)
        {
            // If we don't have a hash
            if (string.IsNullOrEmpty(sha1))
                return null;

            // Get all matching IDs for the hash, if possible
            var newIds = await client.ListDiscsResults(query: sha1);
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
        public static async Task<List<int>?> ValidateUniversalHash(this Client client, SubmissionInfo info)
        {
            // If we don't have a universal hash
            string? universalHash = info.DiscIdentifiers.UniversalHash;
            if (string.IsNullOrEmpty(universalHash))
                return null;

            // Get all matching IDs for the hash, if possible
            var newIds = await client.ListDiscsResults(universalHash: universalHash);
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
