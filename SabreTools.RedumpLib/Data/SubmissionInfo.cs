using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// redump.info submission page information
    /// </summary>
    /// TODO: Fill in specific details from redump.info
    /// TODO: Copy in moderation-only sections, like <see cref="RedumpOrg.Sections.DumpingInfoSection"/>
    public class SubmissionInfo : ICloneable
    {
        /// <summary>
        /// Version of the current schema
        /// </summary>
        [JsonProperty(PropertyName = "schema_version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SchemaVersion { get; set; } = 1;

        public object Clone()
        {
            return new SubmissionInfo
            {
                SchemaVersion = this.SchemaVersion,
            };
        }
    }
}
