using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data.Sections;

namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// redump.info submission page information
    /// </summary>
    /// TODO: Ensure all below information before creating
    ///
    /// See https://github.com/superg/vgindex/blob/main/templates/disc_edit.html
    ///
    /// Section (Name to be determined):
    ///     - System
    ///     - Media
    ///     - Category
    ///     - Title
    ///     - Foreign Title
    ///     - Disc Number
    ///     - Disc Title
    ///     - Filename Suffix (may be removed in the future)
    ///
    /// Section (Name to be determined):
    ///     - Regions
    ///     - Languages
    ///
    /// Section (Name to be determined) (Internal order to be determined):
    ///     - Disc Serials
    ///     - Editions
    ///     - Barcodes
    ///     - Version
    ///     - Error Count
    ///     - Universal Hash
    ///     - Layerbreaks
    ///     - EXE Date (YYYY-MM-DD)
    ///     - EDC (Yes/No)
    ///     - Disc ID
    ///     - Disc Key
    ///
    /// Section (Name to be determined):
    ///     Ring Codes
    ///
    /// Section (Name to be determined) (Internal order to be determined):
    ///     - Comments
    ///     - Contents
    ///     - Protection (Includes LibCrypt detection)
    ///     - SBI
    ///     - Sector Ranges
    ///     - PVD
    ///     - PIC
    ///     - Header
    ///     - Cuesheet
    ///     - Dat
    ///
    /// Section (Name to be determined):
    ///     - Dump Log
    ///     - Logs Archive URL
    ///     - Submission Comment
    ///     - Submit As
    public class SubmissionInfo : ICloneable
    {
        /// <summary>
        /// Version of the current schema
        /// </summary>
        [JsonProperty(PropertyName = "schema_version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SchemaVersion { get; set; } = 1;

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
                DumpingInfo = this.DumpingInfo?.Clone() as DumpingInfoSection ?? new DumpingInfoSection(),
                Artifacts = artifacts,
            };
        }
    }
}
