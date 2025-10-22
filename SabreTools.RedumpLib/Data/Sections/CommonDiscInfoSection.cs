using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Converters;

namespace SabreTools.RedumpLib.Data.Sections
{
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
        public Dictionary<SiteCode, string> CommentsSpecialFields { get; set; } = [];

        [JsonProperty(PropertyName = "d_contents", NullValueHandling = NullValueHandling.Ignore)]
        public string? Contents { get; set; }

        [JsonIgnore]
        public Dictionary<SiteCode, string> ContentsSpecialFields { get; set; } = [];

        public object Clone()
        {
            Dictionary<SiteCode, string> commentsSpecialFields = [];
            foreach (var kvp in this.CommentsSpecialFields)
            {
                commentsSpecialFields[kvp.Key] = kvp.Value;
            }

            Dictionary<SiteCode, string> contentsSpecialFields = [];
            foreach (var kvp in this.ContentsSpecialFields)
            {
                contentsSpecialFields[kvp.Key] = kvp.Value;
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
}
