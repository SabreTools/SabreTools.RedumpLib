using System;
using System.Collections.Generic;
using System.Linq;
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
#if NET48
        public List<int> PartiallyMatchedIDs { get; set; }
#else
        public List<int>? PartiallyMatchedIDs { get; set; }
#endif

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
#if NET48
        public CommonDiscInfoSection CommonDiscInfo { get; set; } = new CommonDiscInfoSection();
#else
        public CommonDiscInfoSection? CommonDiscInfo { get; set; } = new CommonDiscInfoSection();
#endif

        [JsonProperty(PropertyName = "versions_and_editions", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public VersionAndEditionsSection VersionAndEditions { get; set; } = new VersionAndEditionsSection();
#else
        public VersionAndEditionsSection? VersionAndEditions { get; set; } = new VersionAndEditionsSection();
#endif

        [JsonProperty(PropertyName = "edc", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public EDCSection EDC { get; set; } = new EDCSection();
#else
        public EDCSection? EDC { get; set; } = new EDCSection();
#endif

        [JsonProperty(PropertyName = "parent_clone_relationship", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public ParentCloneRelationshipSection ParentCloneRelationship { get; set; } = new ParentCloneRelationshipSection();
#else
        public ParentCloneRelationshipSection? ParentCloneRelationship { get; set; } = new ParentCloneRelationshipSection();
#endif

        [JsonProperty(PropertyName = "extras", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public ExtrasSection Extras { get; set; } = new ExtrasSection();
#else
        public ExtrasSection? Extras { get; set; } = new ExtrasSection();
#endif

        [JsonProperty(PropertyName = "copy_protection", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public CopyProtectionSection CopyProtection { get; set; } = new CopyProtectionSection();
#else
        public CopyProtectionSection? CopyProtection { get; set; } = new CopyProtectionSection();
#endif

        [JsonProperty(PropertyName = "dumpers_and_status", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public DumpersAndStatusSection DumpersAndStatus { get; set; } = new DumpersAndStatusSection();
#else
        public DumpersAndStatusSection? DumpersAndStatus { get; set; } = new DumpersAndStatusSection();
#endif

        [JsonProperty(PropertyName = "tracks_and_write_offsets", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public TracksAndWriteOffsetsSection TracksAndWriteOffsets { get; set; } = new TracksAndWriteOffsetsSection();
#else
        public TracksAndWriteOffsetsSection? TracksAndWriteOffsets { get; set; } = new TracksAndWriteOffsetsSection();
#endif

        [JsonProperty(PropertyName = "size_and_checksums", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public SizeAndChecksumsSection SizeAndChecksums { get; set; } = new SizeAndChecksumsSection();
#else
        public SizeAndChecksumsSection? SizeAndChecksums { get; set; } = new SizeAndChecksumsSection();
#endif

        [JsonProperty(PropertyName = "dumping_info", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public DumpingInfoSection DumpingInfo { get; set; } = new DumpingInfoSection();
#else
        public DumpingInfoSection? DumpingInfo { get; set; } = new DumpingInfoSection();
#endif

        [JsonProperty(PropertyName = "artifacts", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public Dictionary<string, string> Artifacts { get; set; } = new Dictionary<string, string>();
#else
        public Dictionary<string, string>? Artifacts { get; set; } = new Dictionary<string, string>();
#endif

        public object Clone()
        {
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
                Artifacts = this.Artifacts?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            };
        }
    }

    /// <summary>
    /// Common disc info section of New Disc Form
    /// </summary>
    public class CommonDiscInfoSection : ICloneable
    {
        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_system", Required = Required.AllowNull)]
        [JsonConverter(typeof(SystemConverter))]
        public RedumpSystem? System { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_media", Required = Required.AllowNull)]
        [JsonConverter(typeof(DiscTypeConverter))]
        public DiscType? Media { get; set; }

        [JsonProperty(PropertyName = "d_title", Required = Required.AllowNull)]
#if NET48
        public string Title { get; set; }
#else
        public string? Title { get; set; }
#endif

        [JsonProperty(PropertyName = "d_title_foreign", DefaultValueHandling = DefaultValueHandling.Ignore)]
#if NET48
        public string ForeignTitleNonLatin { get; set; }
#else
        public string? ForeignTitleNonLatin { get; set; }
#endif

        [JsonProperty(PropertyName = "d_number", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string DiscNumberLetter { get; set; }
#else
        public string? DiscNumberLetter { get; set; }
#endif

        [JsonProperty(PropertyName = "d_label", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string DiscTitle { get; set; }
#else
        public string? DiscTitle { get; set; }
#endif

        [JsonProperty(PropertyName = "d_category", Required = Required.AllowNull)]
        [JsonConverter(typeof(DiscCategoryConverter))]
        public DiscCategory? Category { get; set; }

        [JsonProperty(PropertyName = "d_region", Required = Required.AllowNull)]
        [JsonConverter(typeof(RegionConverter))]
        public Region? Region { get; set; }

        [JsonProperty(PropertyName = "d_languages", Required = Required.AllowNull)]
        [JsonConverter(typeof(LanguageConverter))]
#if NET48
        public Language?[] Languages { get; set; }
#else
        public Language?[]? Languages { get; set; }
#endif

        [JsonProperty(PropertyName = "d_languages_selection", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(LanguageSelectionConverter))]
#if NET48
        public LanguageSelection?[] LanguageSelection { get; set; }
#else
        public LanguageSelection?[]? LanguageSelection { get; set; }
#endif

        [JsonProperty(PropertyName = "d_serial", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Serial { get; set; }
#else
        public string? Serial { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Ring { get; private set; }
#else
        public string? Ring { get; private set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_id", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string RingId { get; private set; }
#else
        public string? RingId { get; private set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ma1", Required = Required.AllowNull)]
#if NET48
        public string Layer0MasteringRing { get; set; }
#else
        public string? Layer0MasteringRing { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ma1_sid", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer0MasteringSID { get; set; }
#else
        public string? Layer0MasteringSID { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ts1", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer0ToolstampMasteringCode { get; set; }
#else
        public string? Layer0ToolstampMasteringCode { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_mo1_sid", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer0MouldSID { get; set; }
#else
        public string? Layer0MouldSID { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_mo1", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer0AdditionalMould { get; set; }
#else
        public string? Layer0AdditionalMould { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ma2", Required = Required.AllowNull)]
#if NET48
        public string Layer1MasteringRing { get; set; }
#else
        public string? Layer1MasteringRing { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ma2_sid", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer1MasteringSID { get; set; }
#else
        public string? Layer1MasteringSID { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ts2", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer1ToolstampMasteringCode { get; set; }
#else
        public string? Layer1ToolstampMasteringCode { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_mo2_sid", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer1MouldSID { get; set; }
#else
        public string? Layer1MouldSID { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_mo2", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer1AdditionalMould { get; set; }
#else
        public string? Layer1AdditionalMould { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ma3", Required = Required.AllowNull)]
#if NET48
        public string Layer2MasteringRing { get; set; }
#else
        public string? Layer2MasteringRing { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ma3_sid", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer2MasteringSID { get; set; }
#else
        public string? Layer2MasteringSID { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ts3", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer2ToolstampMasteringCode { get; set; }
#else
        public string? Layer2ToolstampMasteringCode { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ma4", Required = Required.AllowNull)]
#if NET48
        public string Layer3MasteringRing { get; set; }
#else
        public string? Layer3MasteringRing { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ma4_sid", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer3MasteringSID { get; set; }
#else
        public string? Layer3MasteringSID { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_ts4", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Layer3ToolstampMasteringCode { get; set; }
#else
        public string? Layer3ToolstampMasteringCode { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_offsets", NullValueHandling = NullValueHandling.Ignore)]
        public string RingOffsetsHidden { get { return "1"; } }

        [JsonProperty(PropertyName = "d_ring_0_0_id", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string RingZeroId { get; private set; }
#else
        public string? RingZeroId { get; private set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_0_density", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string RingZeroDensity { get; private set; }
#else
        public string? RingZeroDensity { get; private set; }
#endif

        [JsonProperty(PropertyName = "d_ring_0_0_value", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string RingWriteOffset { get; set; }
#else
        public string? RingWriteOffset { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ring_count", NullValueHandling = NullValueHandling.Ignore)]
        public string RingCount { get { return "1"; } }

        [JsonProperty(PropertyName = "d_barcode", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Barcode { get; set; }
#else
        public string? Barcode { get; set; }
#endif

        [JsonProperty(PropertyName = "d_date", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string EXEDateBuildDate { get; set; }
#else
        public string? EXEDateBuildDate { get; set; }
#endif

        [JsonProperty(PropertyName = "d_errors", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string ErrorsCount { get; set; }
#else
        public string? ErrorsCount { get; set; }
#endif

        [JsonProperty(PropertyName = "d_comments", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Comments { get; set; }
#else
        public string? Comments { get; set; }
#endif

        [JsonIgnore]
#if NET48
        public Dictionary<SiteCode?, string> CommentsSpecialFields { get; set; }
#else
        public Dictionary<SiteCode, string>? CommentsSpecialFields { get; set; }
#endif

        [JsonProperty(PropertyName = "d_contents", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Contents { get; set; }
#else
        public string? Contents { get; set; }
#endif

        [JsonIgnore]
#if NET48
        public Dictionary<SiteCode?, string> ContentsSpecialFields { get; set; }
#else
        public Dictionary<SiteCode, string>? ContentsSpecialFields { get; set; }
#endif

        public object Clone()
        {
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
                CommentsSpecialFields = this.CommentsSpecialFields?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                Contents = this.Contents,
                ContentsSpecialFields = this.ContentsSpecialFields?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            };
        }
    }

    /// <summary>
    /// Version and editions section of New Disc form
    /// </summary>
    public class VersionAndEditionsSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_version", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Version { get; set; }
#else
        public string? Version { get; set; }
#endif

        [JsonProperty(PropertyName = "d_version_datfile", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string VersionDatfile { get; set; }
#else
        public string? VersionDatfile { get; set; }
#endif

        [JsonProperty(PropertyName = "d_editions", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string[] CommonEditions { get; set; }
#else
        public string[]? CommonEditions { get; set; }
#endif

        [JsonProperty(PropertyName = "d_editions_text", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string OtherEditions { get; set; }
#else
        public string? OtherEditions { get; set; }
#endif

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
#if NET48
        public string ParentID { get; set; }
#else
        public string? ParentID { get; set; }
#endif

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
#if NET48
        public string PVD { get; set; }
#else
        public string? PVD { get; set; }
#endif

        [JsonProperty(PropertyName = "d_d1_key", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string DiscKey { get; set; }
#else
        public string? DiscKey { get; set; }
#endif

        [JsonProperty(PropertyName = "d_d2_key", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string DiscID { get; set; }
#else
        public string? DiscID { get; set; }
#endif

        [JsonProperty(PropertyName = "d_pic_data", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string PIC { get; set; }
#else
        public string? PIC { get; set; }
#endif

        [JsonProperty(PropertyName = "d_header", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Header { get; set; }
#else
        public string? Header { get; set; }
#endif

        [JsonProperty(PropertyName = "d_bca", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string BCA { get; set; }
#else
        public string? BCA { get; set; }
#endif

        [JsonProperty(PropertyName = "d_ssranges", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string SecuritySectorRanges { get; set; }
#else
        public string? SecuritySectorRanges { get; set; }
#endif

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
#if NET48
        public string LibCryptData { get; set; }
#else
        public string? LibCryptData { get; set; }
#endif

        [JsonProperty(PropertyName = "d_protection", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Protection { get; set; }
#else
        public string? Protection { get; set; }
#endif

        [JsonIgnore]
#if NET48
        public Dictionary<string, List<string>> FullProtections { get; set; }
#else
        public Dictionary<string, List<string>?>? FullProtections { get; set; }
#endif

        [JsonProperty(PropertyName = "d_securom", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string SecuROMData { get; set; }
#else
        public string? SecuROMData { get; set; }
#endif

        public object Clone()
        {
            return new CopyProtectionSection
            {
                AntiModchip = this.AntiModchip,
                LibCrypt = this.LibCrypt,
                LibCryptData = this.LibCryptData,
                Protection = this.Protection,
                FullProtections = this.FullProtections?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
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
#if NET48
        public string[] Dumpers { get; set; }
#else
        public string[]? Dumpers { get; set; }
#endif

        [JsonProperty(PropertyName = "d_dumpers_text", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string OtherDumpers { get; set; }
#else
        public string? OtherDumpers { get; set; }
#endif

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
#if NET48
        public string ClrMameProData { get; set; }
#else
        public string? ClrMameProData { get; set; }
#endif

        [JsonProperty(PropertyName = "d_cue", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string Cuesheet { get; set; }
#else
        public string? Cuesheet { get; set; }
#endif

        [JsonProperty(PropertyName = "d_offset", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public int[] CommonWriteOffsets { get; set; }
#else
        public int[]? CommonWriteOffsets { get; set; }
#endif

        [JsonProperty(PropertyName = "d_offset_text", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string OtherWriteOffsets { get; set; }
#else
        public string? OtherWriteOffsets { get; set; }
#endif

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
#if NET48
        public string PICIdentifier { get; set; }
#else
        public string? PICIdentifier { get; set; }
#endif

        [JsonProperty(PropertyName = "d_size", NullValueHandling = NullValueHandling.Ignore)]
        public long Size { get; set; }

        [JsonProperty(PropertyName = "d_crc32", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string CRC32 { get; set; }
#else
        public string? CRC32 { get; set; }
#endif

        [JsonProperty(PropertyName = "d_md5", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string MD5 { get; set; }
#else
        public string? MD5 { get; set; }
#endif

        [JsonProperty(PropertyName = "d_sha1", NullValueHandling = NullValueHandling.Ignore)]
#if NET48
        public string SHA1 { get; set; }
#else
        public string? SHA1 { get; set; }
#endif

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
        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_dumping_program", Required = Required.AllowNull)]
#if NET48
        public string DumpingProgram { get; set; }
#else
        public string? DumpingProgram { get; set; }
#endif

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_dumping_date", Required = Required.AllowNull)]
#if NET48
        public string DumpingDate { get; set; }
#else
        public string? DumpingDate { get; set; }
#endif

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_drive_manufacturer", Required = Required.AllowNull)]
#if NET48
        public string Manufacturer { get; set; }
#else
        public string? Manufacturer { get; set; }
#endif

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_drive_model", Required = Required.AllowNull)]
#if NET48
        public string Model { get; set; }
#else
        public string? Model { get; set; }
#endif

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_drive_firmware", Required = Required.AllowNull)]
#if NET48
        public string Firmware { get; set; }
#else
        public string? Firmware { get; set; }
#endif

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_reported_disc_type", Required = Required.AllowNull)]
#if NET48
        public string ReportedDiscType { get; set; }
#else
        public string? ReportedDiscType { get; set; }
#endif

        public object Clone()
        {
            return new DumpingInfoSection
            {
                DumpingProgram = this.DumpingProgram,
                DumpingDate = this.DumpingDate,
                Manufacturer = this.Manufacturer,
                Model = this.Model,
                Firmware = this.Firmware,
                ReportedDiscType = this.ReportedDiscType,
            };
        }
    }
}
