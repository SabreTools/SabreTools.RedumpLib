using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
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
}
