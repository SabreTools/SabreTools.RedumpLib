using System;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Converters;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Regions and languages edit form section
    /// </summary>
    public class RegionsAndLanguagesSection : ICloneable
    {
        [JsonProperty(PropertyName = "regions", DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(RegionConverter))]
        public Region?[]? Regions { get; set; }

        [JsonProperty(PropertyName = "languages", DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(LanguageConverter))]
        public Language?[]? Languages { get; set; }

        /// <inheritdoc/>
        public object Clone()
        {
            return new RegionsAndLanguagesSection
            {
                Regions = this.Regions?.Clone() as Region?[],
                Languages = this.Languages?.Clone() as Language?[],
            };
        }
    }
}
