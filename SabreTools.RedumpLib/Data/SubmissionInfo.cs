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
    /// Disc identifiers:
    ///     - Disc Serials [serial]
    ///     - Editions [edition]
    ///     - Barcodes [barcode]
    ///     - Version [version]
    ///     - Error Count [error_count]
    ///     - EXE Date (YYYY-MM-DD) [error_count]
    ///     - EDC (Yes/No) [edc]
    ///     - Layerbreaks [layerbreaks]
    ///     - Disc ID [disc_id]
    ///     - Disc Key [disc_key]
    ///     - Universal Hash [universal_hash]
    ///
    /// Ring codes:
    ///     Ring Codes [ring_codes]
    ///
    /// Dump metadata:
    ///     - Comments [comments]
    ///     - Contents [contents]
    ///     - Protection (Includes LibCrypt detection) [protection]
    ///     - Sector Ranges [sector_ranges]
    ///     - SBI [sbi]
    ///     - PVD [pvd]
    ///     - Header [header]
    ///     - BCA [bca]
    ///     - PIC [pic]
    ///     - Cuesheet [cue]
    ///     - Dat [files_xml]
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
                // TODO: Add cloning for all submission form sections

                DumpingInfo = this.DumpingInfo?.Clone() as DumpingInfoSection ?? new DumpingInfoSection(),
                Artifacts = artifacts,
            };
        }
    }
}
