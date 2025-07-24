﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Converters;

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
        public CommonDiscInfoSection? CommonDiscInfo { get; set; } = new CommonDiscInfoSection();

        [JsonProperty(PropertyName = "versions_and_editions", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public VersionAndEditionsSection? VersionAndEditions { get; set; } = new VersionAndEditionsSection();

        [JsonProperty(PropertyName = "edc", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EDCSection? EDC { get; set; } = new EDCSection();

        [JsonProperty(PropertyName = "parent_clone_relationship", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ParentCloneRelationshipSection? ParentCloneRelationship { get; set; } = new ParentCloneRelationshipSection();

        [JsonProperty(PropertyName = "extras", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ExtrasSection? Extras { get; set; } = new ExtrasSection();

        [JsonProperty(PropertyName = "copy_protection", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CopyProtectionSection? CopyProtection { get; set; } = new CopyProtectionSection();

        [JsonProperty(PropertyName = "dumpers_and_status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DumpersAndStatusSection? DumpersAndStatus { get; set; } = new DumpersAndStatusSection();

        [JsonProperty(PropertyName = "tracks_and_write_offsets", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TracksAndWriteOffsetsSection? TracksAndWriteOffsets { get; set; } = new TracksAndWriteOffsetsSection();

        [JsonProperty(PropertyName = "size_and_checksums", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SizeAndChecksumsSection? SizeAndChecksums { get; set; } = new SizeAndChecksumsSection();

        [JsonProperty(PropertyName = "dumping_info", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DumpingInfoSection? DumpingInfo { get; set; } = new DumpingInfoSection();

        [JsonProperty(PropertyName = "artifacts", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, string>? Artifacts { get; set; } = [];

        public object Clone()
        {
            Dictionary<string, string>? artifacts = null;
            if (this.Artifacts != null)
            {
                artifacts = new Dictionary<string, string>();
                foreach (var kvp in this.Artifacts)
                {
                    artifacts[kvp.Key] = kvp.Value;
                }
            }

            return new SubmissionInfo
            {
                SchemaVersion = this.SchemaVersion,
                FullyMatchedID = this.FullyMatchedID,
                PartiallyMatchedIDs = this.PartiallyMatchedIDs,
                Added = this.Added,
                LastModified = this.LastModified,
                CommonDiscInfo = this.CommonDiscInfo?.Clone() as CommonDiscInfoSection,
                VersionAndEditions = this.VersionAndEditions?.Clone() as VersionAndEditionsSection,
                EDC = this.EDC?.Clone() as EDCSection,
                ParentCloneRelationship = this.ParentCloneRelationship?.Clone() as ParentCloneRelationshipSection,
                Extras = this.Extras?.Clone() as ExtrasSection,
                CopyProtection = this.CopyProtection?.Clone() as CopyProtectionSection,
                DumpersAndStatus = this.DumpersAndStatus?.Clone() as DumpersAndStatusSection,
                TracksAndWriteOffsets = this.TracksAndWriteOffsets?.Clone() as TracksAndWriteOffsetsSection,
                SizeAndChecksums = this.SizeAndChecksums?.Clone() as SizeAndChecksumsSection,
                DumpingInfo = this.DumpingInfo?.Clone() as DumpingInfoSection,
                Artifacts = artifacts,
            };
        }
    }

    /// <summary>
    /// Common disc info section of New Disc Form
    /// </summary>
    public class CommonDiscInfoSection : ICloneable
    {
        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_system", DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(SystemConverter))]
        public RedumpSystem? System { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_media", DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(DiscTypeConverter))]
        public DiscType? Media { get; set; }

        [JsonProperty(PropertyName = "d_title", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Title { get; set; }

        [JsonProperty(PropertyName = "d_title_foreign", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? ForeignTitleNonLatin { get; set; }

        [JsonProperty(PropertyName = "d_number", NullValueHandling = NullValueHandling.Ignore)]
        public string? DiscNumberLetter { get; set; }

        [JsonProperty(PropertyName = "d_label", NullValueHandling = NullValueHandling.Ignore)]
        public string? DiscTitle { get; set; }

        [JsonProperty(PropertyName = "d_category", DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(DiscCategoryConverter))]
        public DiscCategory? Category { get; set; }

        [JsonProperty(PropertyName = "d_region", DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(RegionConverter))]
        public Region? Region { get; set; }

        [JsonProperty(PropertyName = "d_languages", DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(LanguageConverter))]
        public Language?[]? Languages { get; set; }

        [JsonProperty(PropertyName = "d_languages_selection", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(LanguageSelectionConverter))]
        public LanguageSelection?[]? LanguageSelection { get; set; }

        [JsonProperty(PropertyName = "d_serial", NullValueHandling = NullValueHandling.Ignore)]
        public string? Serial { get; set; }

        [JsonProperty(PropertyName = "d_ring", NullValueHandling = NullValueHandling.Ignore)]
        public string? Ring { get; private set; }

        [JsonProperty(PropertyName = "d_ring_0_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? RingId { get; private set; }

        [JsonProperty(PropertyName = "d_ring_0_ma1", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Layer0MasteringRing { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ma1_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer0MasteringSID { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ts1", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer0ToolstampMasteringCode { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_mo1_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer0MouldSID { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_mo1", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer0AdditionalMould { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ma2", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Layer1MasteringRing { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ma2_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer1MasteringSID { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ts2", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer1ToolstampMasteringCode { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_mo2_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer1MouldSID { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_mo2", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer1AdditionalMould { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ma3", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Layer2MasteringRing { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ma3_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer2MasteringSID { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ts3", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer2ToolstampMasteringCode { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ma4", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Layer3MasteringRing { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ma4_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer3MasteringSID { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_ts4", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer3ToolstampMasteringCode { get; set; }

        [JsonProperty(PropertyName = "d_ring_0_offsets", NullValueHandling = NullValueHandling.Ignore)]
        public string RingOffsetsHidden { get { return "1"; } }

        [JsonProperty(PropertyName = "d_ring_0_0_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? RingZeroId { get; private set; }

        [JsonProperty(PropertyName = "d_ring_0_0_density", NullValueHandling = NullValueHandling.Ignore)]
        public string? RingZeroDensity { get; private set; }

        [JsonProperty(PropertyName = "d_ring_0_0_value", NullValueHandling = NullValueHandling.Ignore)]
        public string? RingWriteOffset { get; set; }

        [JsonProperty(PropertyName = "d_ring_count", NullValueHandling = NullValueHandling.Ignore)]
        public string RingCount { get { return "1"; } }

        [JsonProperty(PropertyName = "d_barcode", NullValueHandling = NullValueHandling.Ignore)]
        public string? Barcode { get; set; }

        [JsonProperty(PropertyName = "d_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? EXEDateBuildDate { get; set; }

        [JsonProperty(PropertyName = "d_errors", NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrorsCount { get; set; }

        [JsonProperty(PropertyName = "d_comments", NullValueHandling = NullValueHandling.Ignore)]
        public string? Comments { get; set; }

        [JsonIgnore]
        public Dictionary<SiteCode, string>? CommentsSpecialFields { get; set; }

        [JsonProperty(PropertyName = "d_contents", NullValueHandling = NullValueHandling.Ignore)]
        public string? Contents { get; set; }

        [JsonIgnore]
        public Dictionary<SiteCode, string>? ContentsSpecialFields { get; set; }

        public object Clone()
        {
            Dictionary<SiteCode, string>? commentsSpecialFields = null;
            if (this.CommentsSpecialFields != null)
            {
                commentsSpecialFields = new Dictionary<SiteCode, string>();
                foreach (var kvp in this.CommentsSpecialFields)
                {
                    commentsSpecialFields[kvp.Key] = kvp.Value;
                }
            }

            Dictionary<SiteCode, string>? contentsSpecialFields = null;
            if (this.ContentsSpecialFields != null)
            {
                contentsSpecialFields = new Dictionary<SiteCode, string>();
                foreach (var kvp in this.ContentsSpecialFields)
                {
                    contentsSpecialFields[kvp.Key] = kvp.Value;
                }
            }

            return new CommonDiscInfoSection
            {
                System = this.System,
                Media = this.Media,
                Title = this.Title,
                ForeignTitleNonLatin = this.ForeignTitleNonLatin,
                DiscNumberLetter = this.DiscNumberLetter,
                DiscTitle = this.DiscTitle,
                Category = this.Category,
                Region = this.Region,
                Languages = this.Languages?.Clone() as Language?[],
                LanguageSelection = this.LanguageSelection?.Clone() as LanguageSelection?[],
                Serial = this.Serial,
                Ring = this.Ring,
                RingId = this.RingId,
                Layer0MasteringRing = this.Layer0MasteringRing,
                Layer0MasteringSID = this.Layer0MasteringSID,
                Layer0ToolstampMasteringCode = this.Layer0ToolstampMasteringCode,
                Layer0MouldSID = this.Layer0MouldSID,
                Layer0AdditionalMould = this.Layer0AdditionalMould,
                Layer1MasteringRing = this.Layer1MasteringRing,
                Layer1MasteringSID = this.Layer1MasteringSID,
                Layer1ToolstampMasteringCode = this.Layer1ToolstampMasteringCode,
                Layer1MouldSID = this.Layer1MouldSID,
                Layer1AdditionalMould = this.Layer1AdditionalMould,
                Layer2MasteringRing = this.Layer2MasteringRing,
                Layer2MasteringSID = this.Layer2MasteringSID,
                Layer2ToolstampMasteringCode = this.Layer2ToolstampMasteringCode,
                Layer3MasteringRing = this.Layer3MasteringRing,
                Layer3MasteringSID = this.Layer3MasteringSID,
                Layer3ToolstampMasteringCode = this.Layer3ToolstampMasteringCode,
                RingZeroId = this.RingZeroId,
                RingZeroDensity = this.RingZeroDensity,
                RingWriteOffset = this.RingWriteOffset,
                Barcode = this.Barcode,
                EXEDateBuildDate = this.EXEDateBuildDate,
                ErrorsCount = this.ErrorsCount,
                Comments = this.Comments,
                CommentsSpecialFields = commentsSpecialFields,
                Contents = this.Contents,
                ContentsSpecialFields = contentsSpecialFields,
            };
        }
    }

    /// <summary>
    /// Version and editions section of New Disc form
    /// </summary>
    public class VersionAndEditionsSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_version", NullValueHandling = NullValueHandling.Ignore)]
        public string? Version { get; set; }

        [JsonProperty(PropertyName = "d_version_datfile", NullValueHandling = NullValueHandling.Ignore)]
        public string? VersionDatfile { get; set; }

        [JsonProperty(PropertyName = "d_editions", NullValueHandling = NullValueHandling.Ignore)]
        public string[]? CommonEditions { get; set; }

        [JsonProperty(PropertyName = "d_editions_text", NullValueHandling = NullValueHandling.Ignore)]
        public string? OtherEditions { get; set; }

        public object Clone()
        {
            return new VersionAndEditionsSection
            {
                Version = this.Version,
                VersionDatfile = this.VersionDatfile,
                CommonEditions = this.CommonEditions,
                OtherEditions = this.OtherEditions,
            };
        }
    }

    /// <summary>
    /// EDC section of New Disc form (PSX only)
    /// </summary>
    public class EDCSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_edc", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(YesNoConverter))]
        public YesNo? EDC { get; set; }

        public object Clone()
        {
            return new EDCSection
            {
                EDC = this.EDC,
            };
        }
    }

    /// <summary>
    /// Parent/Clone relationship section of New Disc form
    /// </summary>
    public class ParentCloneRelationshipSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? ParentID { get; set; }

        [JsonProperty(PropertyName = "d_is_regional_parent", NullValueHandling = NullValueHandling.Ignore)]
        public bool RegionalParent { get; set; }

        public object Clone()
        {
            return new ParentCloneRelationshipSection
            {
                ParentID = this.ParentID,
                RegionalParent = this.RegionalParent,
            };
        }
    }

    /// <summary>
    /// Extras section of New Disc form
    /// </summary>
    public class ExtrasSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_pvd", NullValueHandling = NullValueHandling.Ignore)]
        public string? PVD { get; set; }

        [JsonProperty(PropertyName = "d_d1_key", NullValueHandling = NullValueHandling.Ignore)]
        public string? DiscKey { get; set; }

        [JsonProperty(PropertyName = "d_d2_key", NullValueHandling = NullValueHandling.Ignore)]
        public string? DiscID { get; set; }

        [JsonProperty(PropertyName = "d_pic_data", NullValueHandling = NullValueHandling.Ignore)]
        public string? PIC { get; set; }

        [JsonProperty(PropertyName = "d_header", NullValueHandling = NullValueHandling.Ignore)]
        public string? Header { get; set; }

        [JsonProperty(PropertyName = "d_bca", NullValueHandling = NullValueHandling.Ignore)]
        public string? BCA { get; set; }

        [JsonProperty(PropertyName = "d_ssranges", NullValueHandling = NullValueHandling.Ignore)]
        public string? SecuritySectorRanges { get; set; }

        public object Clone()
        {
            return new ExtrasSection
            {
                PVD = this.PVD,
                DiscKey = this.DiscKey,
                DiscID = this.DiscID,
                PIC = this.PIC,
                Header = this.Header,
                BCA = this.BCA,
                SecuritySectorRanges = this.SecuritySectorRanges,
            };
        }
    }

    /// <summary>
    /// Copy protection section of New Disc form
    /// </summary>
    public class CopyProtectionSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_protection_a", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(YesNoConverter))]
        public YesNo? AntiModchip { get; set; }

        [JsonProperty(PropertyName = "d_protection_1", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(YesNoConverter))]
        public YesNo? LibCrypt { get; set; }

        [JsonProperty(PropertyName = "d_libcrypt", NullValueHandling = NullValueHandling.Ignore)]
        public string? LibCryptData { get; set; }

        [JsonProperty(PropertyName = "d_protection", NullValueHandling = NullValueHandling.Ignore)]
        public string? Protection { get; set; }

        [JsonIgnore]
        public Dictionary<string, List<string>?>? FullProtections { get; set; }

        [JsonProperty(PropertyName = "d_securom", NullValueHandling = NullValueHandling.Ignore)]
        public string? SecuROMData { get; set; }

        public object Clone()
        {
            Dictionary<string, List<string>?>? fullProtections = null;
            if (this.FullProtections != null)
            {
                fullProtections = new Dictionary<string, List<string>?>();
                foreach (var kvp in this.FullProtections)
                {
                    fullProtections[kvp.Key] = kvp.Value;
                }
            }

            return new CopyProtectionSection
            {
                AntiModchip = this.AntiModchip,
                LibCrypt = this.LibCrypt,
                LibCryptData = this.LibCryptData,
                Protection = this.Protection,
                FullProtections = fullProtections,
                SecuROMData = this.SecuROMData,
            };
        }
    }

    /// <summary>
    /// Dumpers and status section of New Disc form (Moderator only)
    /// </summary>
    public class DumpersAndStatusSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_status", NullValueHandling = NullValueHandling.Ignore)]
        public DumpStatus Status { get; set; }

        [JsonProperty(PropertyName = "d_dumpers", NullValueHandling = NullValueHandling.Ignore)]
        public string[]? Dumpers { get; set; }

        [JsonProperty(PropertyName = "d_dumpers_text", NullValueHandling = NullValueHandling.Ignore)]
        public string? OtherDumpers { get; set; }

        public object Clone()
        {
            return new DumpersAndStatusSection
            {
                Status = this.Status,
                Dumpers = this.Dumpers?.Clone() as string[],
                OtherDumpers = this.OtherDumpers,
            };
        }
    }

    /// <summary>
    /// Tracks and write offsets section of New Disc form (CD/GD-based)
    /// </summary>
    public class TracksAndWriteOffsetsSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_tracks", NullValueHandling = NullValueHandling.Ignore)]
        public string? ClrMameProData { get; set; }

        [JsonProperty(PropertyName = "d_cue", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cuesheet { get; set; }

        [JsonProperty(PropertyName = "d_offset", NullValueHandling = NullValueHandling.Ignore)]
        public int[]? CommonWriteOffsets { get; set; }

        [JsonProperty(PropertyName = "d_offset_text", NullValueHandling = NullValueHandling.Ignore)]
        public string? OtherWriteOffsets { get; set; }

        public object Clone()
        {
            return new TracksAndWriteOffsetsSection
            {
                ClrMameProData = this.ClrMameProData,
                Cuesheet = this.Cuesheet,
                CommonWriteOffsets = this.CommonWriteOffsets?.Clone() as int[],
                OtherWriteOffsets = this.OtherWriteOffsets,
            };
        }
    }

    /// <summary>
    /// Size &amp; checksums section of New Disc form (DVD/BD/UMD-based)
    /// </summary>
    public class SizeAndChecksumsSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_layerbreak", NullValueHandling = NullValueHandling.Ignore)]
        public long Layerbreak { get; set; }

        [JsonProperty(PropertyName = "d_layerbreak_2", NullValueHandling = NullValueHandling.Ignore)]
        public long Layerbreak2 { get; set; }

        [JsonProperty(PropertyName = "d_layerbreak_3", NullValueHandling = NullValueHandling.Ignore)]
        public long Layerbreak3 { get; set; }

        [JsonProperty(PropertyName = "d_pic_identifier", NullValueHandling = NullValueHandling.Ignore)]
        public string? PICIdentifier { get; set; }

        [JsonProperty(PropertyName = "d_size", NullValueHandling = NullValueHandling.Ignore)]
        public long Size { get; set; }

        [JsonProperty(PropertyName = "d_crc32", NullValueHandling = NullValueHandling.Ignore)]
        public string? CRC32 { get; set; }

        [JsonProperty(PropertyName = "d_md5", NullValueHandling = NullValueHandling.Ignore)]
        public string? MD5 { get; set; }

        [JsonProperty(PropertyName = "d_sha1", NullValueHandling = NullValueHandling.Ignore)]
        public string? SHA1 { get; set; }

        public object Clone()
        {
            return new SizeAndChecksumsSection
            {
                Layerbreak = this.Layerbreak,
                Layerbreak2 = this.Layerbreak2,
                Layerbreak3 = this.Layerbreak3,
                PICIdentifier = this.PICIdentifier,
                Size = this.Size,
                CRC32 = this.CRC32,
                MD5 = this.MD5,
                SHA1 = this.SHA1,
            };
        }
    }

    /// <summary>
    /// Dumping info section for moderation
    /// </summary>
    public class DumpingInfoSection : ICloneable
    {
        // Name not defined by Redump -- Only used with MPF
        [JsonProperty(PropertyName = "d_frontend_version", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? FrontendVersion { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_dumping_program", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? DumpingProgram { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_dumping_date", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? DumpingDate { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_dumping_params", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? DumpingParameters { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_drive_manufacturer", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Manufacturer { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_drive_model", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Model { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_drive_firmware", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Firmware { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_reported_disc_type", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? ReportedDiscType { get; set; }

        // Name not defined by Redump -- Only used with Redumper
        [JsonProperty(PropertyName = "d_errors_c2", NullValueHandling = NullValueHandling.Ignore)]
        public string? C2ErrorsCount { get; set; }

        public object Clone()
        {
            return new DumpingInfoSection
            {
                FrontendVersion = this.FrontendVersion,
                DumpingProgram = this.DumpingProgram,
                DumpingDate = this.DumpingDate,
                DumpingParameters = this.DumpingParameters,
                Manufacturer = this.Manufacturer,
                Model = this.Model,
                Firmware = this.Firmware,
                ReportedDiscType = this.ReportedDiscType,
                C2ErrorsCount = this.C2ErrorsCount,
            };
        }
    }
}
