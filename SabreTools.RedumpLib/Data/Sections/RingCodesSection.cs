using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Ring codes edit form section
    /// </summary>
    /// <remarks>Property names are guessed for now</remarks>
    public class RingCodesSection : ICloneable
    {
        #region Layer 0

        [JsonProperty(PropertyName = "layer_0_mastering_code", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Layer0MasteringCode { get; set; }

        [JsonProperty(PropertyName = "layer_0_mastering_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer0MasteringSID { get; set; }

        [JsonProperty(PropertyName = "layer_0_toolstamps", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer0Toolstamps { get; set; }

        [JsonProperty(PropertyName = "layer_0_mould_sids", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer0MouldSIDs { get; set; }

        [JsonProperty(PropertyName = "layer_0_additional_moulds", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer0AdditionalMoulds { get; set; }

        #endregion

        #region Layer 1

        [JsonProperty(PropertyName = "layer_1_mastering_code", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Layer1MasteringCode { get; set; }

        [JsonProperty(PropertyName = "layer_1_mastering_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer1MasteringSID { get; set; }

        [JsonProperty(PropertyName = "layer_1_toolstamps", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer1Toolstamps { get; set; }

        #endregion

        #region Layer 2

        [JsonProperty(PropertyName = "layer_2_mastering_code", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Layer2MasteringCode { get; set; }

        [JsonProperty(PropertyName = "layer_2_mastering_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer2MasteringSID { get; set; }

        [JsonProperty(PropertyName = "layer_2_toolstamps", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer2Toolstamps { get; set; }

        #endregion

        #region Layer 3

        [JsonProperty(PropertyName = "layer_3_mastering_code", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Layer3MasteringCode { get; set; }

        [JsonProperty(PropertyName = "layer_3_mastering_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer3MasteringSID { get; set; }

        [JsonProperty(PropertyName = "layer_3_toolstamps", NullValueHandling = NullValueHandling.Ignore)]
        public string? Layer3Toolstamps { get; set; }

        #endregion

        #region Label Side

        [JsonProperty(PropertyName = "label_side_mastering_code", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? LabelSideMasteringCode { get; set; }

        [JsonProperty(PropertyName = "label_side_mastering_sid", NullValueHandling = NullValueHandling.Ignore)]
        public string? LabelSideMasteringSID { get; set; }

        [JsonProperty(PropertyName = "label_side_toolstamps", NullValueHandling = NullValueHandling.Ignore)]
        public string? LabelSideToolstamps { get; set; }

        [JsonProperty(PropertyName = "label_side_mould_sids", NullValueHandling = NullValueHandling.Ignore)]
        public string? LabelSideMouldSIDs { get; set; }

        [JsonProperty(PropertyName = "label_side_additional_moulds", NullValueHandling = NullValueHandling.Ignore)]
        public string? LabelSideAdditionalMoulds { get; set; }

        #endregion

        #region Miscellaneous

        [JsonProperty(PropertyName = "write_offset", NullValueHandling = NullValueHandling.Ignore)]
        public string? WriteOffset { get; set; }

        [JsonProperty(PropertyName = "sample_start", NullValueHandling = NullValueHandling.Ignore)]
        public string? SampleStart { get; set; }

        #endregion

        public object Clone()
        {
            return new RingCodesSection
            {
                Layer0MasteringCode = this.Layer0MasteringCode,
                Layer0MasteringSID = this.Layer0MasteringSID,
                Layer0Toolstamps = this.Layer0Toolstamps,
                Layer0MouldSIDs = this.Layer0MouldSIDs,
                Layer0AdditionalMoulds = this.Layer0AdditionalMoulds,

                Layer1MasteringCode = this.Layer1MasteringCode,
                Layer1MasteringSID = this.Layer1MasteringSID,
                Layer1Toolstamps = this.Layer1Toolstamps,

                Layer2MasteringCode = this.Layer2MasteringCode,
                Layer2MasteringSID = this.Layer2MasteringSID,
                Layer2Toolstamps = this.Layer2Toolstamps,

                Layer3MasteringCode = this.Layer3MasteringCode,
                Layer3MasteringSID = this.Layer3MasteringSID,
                Layer3Toolstamps = this.Layer3Toolstamps,

                LabelSideMasteringCode = this.LabelSideMasteringCode,
                LabelSideMasteringSID = this.LabelSideMasteringSID,
                LabelSideToolstamps = this.LabelSideToolstamps,
                LabelSideMouldSIDs = this.LabelSideMouldSIDs,
                LabelSideAdditionalMoulds = this.LabelSideAdditionalMoulds,

                WriteOffset = this.WriteOffset,
                SampleStart = this.SampleStart,
            };
        }
    }
}
