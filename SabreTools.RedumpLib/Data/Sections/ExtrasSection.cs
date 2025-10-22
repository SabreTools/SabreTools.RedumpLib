using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
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
}
