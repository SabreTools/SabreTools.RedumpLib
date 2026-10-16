using System;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Converters;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Disc identity edit form section
    /// </summary>
    public class DiscIdentitySection : ICloneable
    {
        [JsonProperty(PropertyName = "system_code", DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(PhysicalSystemConverter))]
        public PhysicalSystem? System { get; set; }

        [JsonProperty(PropertyName = "media_type", DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(MediaTypeConverter))]
        public MediaType? Media { get; set; }

        [JsonProperty(PropertyName = "category", DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(DiscCategoryConverter))]
        public DiscCategory? Category { get; set; }

        [JsonProperty(PropertyName = "title", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Title { get; set; }

        [JsonProperty(PropertyName = "title_foreign", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? ForeignTitle { get; set; }

        [JsonProperty(PropertyName = "disc_number", NullValueHandling = NullValueHandling.Ignore)]
        public string? DiscNumber { get; set; }

        [JsonProperty(PropertyName = "disc_title", NullValueHandling = NullValueHandling.Ignore)]
        public string? DiscTitle { get; set; }

        [JsonProperty(PropertyName = "disc_title_foreign", NullValueHandling = NullValueHandling.Ignore)]
        public string? ForeignDiscTitle { get; set; }

        /// <remarks>May be removed in the future</remarks>
        [JsonProperty(PropertyName = "filename_suffix", NullValueHandling = NullValueHandling.Ignore)]
        public string? FilenameSuffix { get; set; }

        /// <inheritdoc/>
        public object Clone()
        {
            return new DiscIdentitySection
            {
                System = this.System,
                Media = this.Media,
                Category = this.Category,
                Title = this.Title,
                ForeignTitle = this.ForeignTitle,
                DiscNumber = this.DiscNumber,
                DiscTitle = this.DiscTitle,
                ForeignDiscTitle = this.ForeignDiscTitle,
                FilenameSuffix = this.FilenameSuffix,
            };
        }
    }
}
