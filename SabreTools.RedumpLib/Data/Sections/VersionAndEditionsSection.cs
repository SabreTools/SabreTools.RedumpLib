using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
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
}
