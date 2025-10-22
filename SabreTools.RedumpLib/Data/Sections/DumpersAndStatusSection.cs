using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
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
}
