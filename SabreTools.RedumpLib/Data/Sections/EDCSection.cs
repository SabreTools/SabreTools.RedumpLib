using System;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Converters;

namespace SabreTools.RedumpLib.Data.Sections
{
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
}
