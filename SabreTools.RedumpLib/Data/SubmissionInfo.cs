using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data.Sections;

namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// redump.info submission page information
    /// </summary>
    /// TODO: Fill section files for each of the following structures
    ///
    /// Submission controls:
    ///     - Dump Log [dump_log]
    ///     - Logs Archive URL [extra_upload_url]
    ///     - Review Comment [review_comment] (Hidden from submission)
    ///     - Submission Comment [submission_comment]
    ///     - Submit As [submit_as]
    public class SubmissionInfo : ICloneable
    {
        #region Model Information

        /// <summary>
        /// Version of the current schema
        /// </summary>
        /// <remarks>Versions 4 and above are wholly incompatible with versions 1-3</remarks>
        [JsonProperty(PropertyName = "schema_version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SchemaVersion { get; set; } = 4;

        #endregion

        #region Matching Information

        /// <summary>
        /// List of fully-matched Redump IDs
        /// </summary>
        /// <remarks>
        /// Media is considered to be a "full match" if all hash data is a match.
        /// This means that media may also be considered a "full match"
        /// even if it is missing a track.
        /// </remarks>
        [JsonIgnore]
        public List<int>? FullyMatchedIDs { get; set; }

        /// <summary>
        /// List of partially-matched Redump IDs
        /// </summary>
        /// <remarks>
        /// Media is considered to be a "partial match" if at least some hash data
        /// is a match to another. In cases where there are shared tracks, this
        /// can lead to a high number of partial matches.
        /// </remarks>
        [JsonIgnore]
        public List<int>? PartiallyMatchedIDs { get; set; }

        /// <summary>
        /// DateTime of when the disc was added
        /// </summary>
        [JsonIgnore]
        public DateTime? Added { get; set; }

        /// <summary>
        /// DateTime of when the disc was last modified
        /// </summary>
        [JsonIgnore]
        public DateTime? LastModified { get; set; }

        #endregion

        #region Submission Form

        /// <summary>
        /// Disc identity section
        /// </summary>
        [JsonProperty(PropertyName = "disc_identity", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DiscIdentitySection DiscIdentity { get; set; } = new DiscIdentitySection();

        /// <summary>
        /// Regions and languages section
        /// </summary>
        [JsonProperty(PropertyName = "disc_identity", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public RegionsAndLanguagesSection RegionsAndLanguages { get; set; } = new RegionsAndLanguagesSection();

        /// <summary>
        /// Disc identifiers section
        /// </summary>
        [JsonProperty(PropertyName = "disc_identifiers", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DiscIdentifiersSection DiscIdentifiers { get; set; } = new DiscIdentifiersSection();

        /// <summary>
        /// Ring codes section
        /// </summary>
        [JsonProperty(PropertyName = "ring_codes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public RingCodesSection RingCodes { get; set; } = new RingCodesSection();

        /// <summary>
        /// Dump metadata section
        /// </summary>
        [JsonProperty(PropertyName = "dump_metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DumpMetadataSection DumpMetadata { get; set; } = new DumpMetadataSection();

        // TODO: To be filled out when sections are added

        #endregion

        #region Moderator Information

        /// <summary>
        /// Dumping info section for moderation
        /// </summary>
        [JsonProperty(PropertyName = "dumping_info", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DumpingInfoSection DumpingInfo { get; set; } = new DumpingInfoSection();

        /// <summary>
        /// Proof-of-concept section to handle additional attachments such as encoded files
        /// </summary>
        [JsonProperty(PropertyName = "artifacts", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, string> Artifacts { get; set; } = [];

        #endregion

        /// <inheritdoc/>
        public object Clone()
        {
            Dictionary<string, string> artifacts = [];
            foreach (var kvp in this.Artifacts)
            {
                artifacts[kvp.Key] = kvp.Value;
            }

            return new SubmissionInfo
            {
                SchemaVersion = this.SchemaVersion,

                FullyMatchedIDs = this.FullyMatchedIDs,
                PartiallyMatchedIDs = this.PartiallyMatchedIDs,
                Added = this.Added,
                LastModified = this.LastModified,

                DiscIdentity = this.DiscIdentity?.Clone() as DiscIdentitySection ?? new DiscIdentitySection(),
                RegionsAndLanguages = this.RegionsAndLanguages?.Clone() as RegionsAndLanguagesSection ?? new RegionsAndLanguagesSection(),
                DiscIdentifiers = this.DiscIdentifiers?.Clone() as DiscIdentifiersSection ?? new DiscIdentifiersSection(),
                RingCodes = this.RingCodes?.Clone() as RingCodesSection ?? new RingCodesSection(),
                DumpMetadata = this.DumpMetadata?.Clone() as DumpMetadataSection ?? new DumpMetadataSection(),
                // TODO: Add cloning for all submission form sections

                DumpingInfo = this.DumpingInfo?.Clone() as DumpingInfoSection ?? new DumpingInfoSection(),
                Artifacts = artifacts,
            };
        }
    }
}
