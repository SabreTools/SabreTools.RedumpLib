using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data.Sections;

namespace SabreTools.RedumpLib.Data
{
    public class SubmissionInfo : ICloneable
    {
        /// <summary>
        /// Version of the current schema
        /// </summary>
        [JsonProperty(PropertyName = "schema_version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SchemaVersion { get; set; } = 3;

        /// <summary>
        /// Fully matched Redump ID
        /// </summary>
        [JsonIgnore]
        public int? FullyMatchedID { get; set; }

        /// <summary>
        /// List of partially matched Redump IDs
        /// </summary>
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

        [JsonProperty(PropertyName = "common_disc_info", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CommonDiscInfoSection CommonDiscInfo { get; set; } = new CommonDiscInfoSection();

        [JsonProperty(PropertyName = "versions_and_editions", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public VersionAndEditionsSection VersionAndEditions { get; set; } = new VersionAndEditionsSection();

        [JsonProperty(PropertyName = "edc", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EDCSection EDC { get; set; } = new EDCSection();

        [JsonProperty(PropertyName = "parent_clone_relationship", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ParentCloneRelationshipSection ParentCloneRelationship { get; set; } = new ParentCloneRelationshipSection();

        [JsonProperty(PropertyName = "extras", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ExtrasSection Extras { get; set; } = new ExtrasSection();

        [JsonProperty(PropertyName = "copy_protection", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CopyProtectionSection CopyProtection { get; set; } = new CopyProtectionSection();

        [JsonProperty(PropertyName = "dumpers_and_status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DumpersAndStatusSection DumpersAndStatus { get; set; } = new DumpersAndStatusSection();

        [JsonProperty(PropertyName = "tracks_and_write_offsets", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TracksAndWriteOffsetsSection TracksAndWriteOffsets { get; set; } = new TracksAndWriteOffsetsSection();

        [JsonProperty(PropertyName = "size_and_checksums", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SizeAndChecksumsSection SizeAndChecksums { get; set; } = new SizeAndChecksumsSection();

        [JsonProperty(PropertyName = "dumping_info", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DumpingInfoSection DumpingInfo { get; set; } = new DumpingInfoSection();

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
                FullyMatchedID = this.FullyMatchedID,
                PartiallyMatchedIDs = this.PartiallyMatchedIDs,
                Added = this.Added,
                LastModified = this.LastModified,
                CommonDiscInfo = this.CommonDiscInfo?.Clone() as CommonDiscInfoSection ?? new CommonDiscInfoSection(),
                VersionAndEditions = this.VersionAndEditions?.Clone() as VersionAndEditionsSection ?? new VersionAndEditionsSection(),
                EDC = this.EDC?.Clone() as EDCSection ?? new EDCSection(),
                ParentCloneRelationship = this.ParentCloneRelationship?.Clone() as ParentCloneRelationshipSection ?? new ParentCloneRelationshipSection(),
                Extras = this.Extras?.Clone() as ExtrasSection ?? new ExtrasSection(),
                CopyProtection = this.CopyProtection?.Clone() as CopyProtectionSection ?? new CopyProtectionSection(),
                DumpersAndStatus = this.DumpersAndStatus?.Clone() as DumpersAndStatusSection ?? new DumpersAndStatusSection(),
                TracksAndWriteOffsets = this.TracksAndWriteOffsets?.Clone() as TracksAndWriteOffsetsSection ?? new TracksAndWriteOffsetsSection(),
                SizeAndChecksums = this.SizeAndChecksums?.Clone() as SizeAndChecksumsSection ?? new SizeAndChecksumsSection(),
                DumpingInfo = this.DumpingInfo?.Clone() as DumpingInfoSection ?? new DumpingInfoSection(),
                Artifacts = artifacts,
            };
        }
    }
}
