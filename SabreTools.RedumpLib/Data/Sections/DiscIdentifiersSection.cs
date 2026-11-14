using System;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Converters;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Disc identifiers edit form section
    /// </summary>
    public class DiscIdentifiersSection : ICloneable
    {
        [JsonProperty(PropertyName = "serial", NullValueHandling = NullValueHandling.Ignore)]
        public string? DiscSerials { get; set; }

        [JsonProperty(PropertyName = "edition", NullValueHandling = NullValueHandling.Ignore)]
        public string? Editions { get; set; }

        [JsonProperty(PropertyName = "barcode", NullValueHandling = NullValueHandling.Ignore)]
        public string? Barcodes { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public string? Version { get; set; }

        [JsonProperty(PropertyName = "error_count", NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrorCount { get; set; }

        /// <remarks>Should be in YYYY-MM-DD format</remarks>
        [JsonProperty(PropertyName = "exe_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? EXEDate { get; set; }

        [JsonProperty(PropertyName = "internal_serial", NullValueHandling = NullValueHandling.Ignore)]
        public string? InternalSerial { get; set; }

        [JsonProperty(PropertyName = "volume_label", NullValueHandling = NullValueHandling.Ignore)]
        public string? VolumeLabel { get; set; }

        [JsonProperty(PropertyName = "edc", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(YesNoConverter))]
        public YesNo? EDC { get; set; }

        /// <remarks>Single page field called `layerbreaks`</remarks>
        [JsonProperty(PropertyName = "layerbreaks_1", NullValueHandling = NullValueHandling.Ignore)]
        public long Layerbreak { get; set; }

        /// <remarks>Single page field called `layerbreaks`</remarks>
        [JsonProperty(PropertyName = "layerbreaks_2", NullValueHandling = NullValueHandling.Ignore)]
        public long Layerbreak2 { get; set; }

        /// <remarks>Single page field called `layerbreaks`</remarks>
        [JsonProperty(PropertyName = "layerbreaks_3", NullValueHandling = NullValueHandling.Ignore)]
        public long Layerbreak3 { get; set; }

        [JsonProperty(PropertyName = "disc_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? DiscID { get; set; }

        [JsonProperty(PropertyName = "disc_key", NullValueHandling = NullValueHandling.Ignore)]
        public string? DiscKey { get; set; }

        [JsonProperty(PropertyName = "universal_hash", NullValueHandling = NullValueHandling.Ignore)]
        public string? UniversalHash { get; set; }

        public object Clone()
        {
            return new DiscIdentifiersSection
            {
                DiscSerials = this.DiscSerials,
                Editions = this.Editions,
                Barcodes = this.Barcodes,
                Version = this.Version,
                ErrorCount = this.ErrorCount,
                EXEDate = this.EXEDate,
                InternalSerial = this.InternalSerial,
                VolumeLabel = this.VolumeLabel,
                EDC = this.EDC,
                Layerbreak = this.Layerbreak,
                Layerbreak2 = this.Layerbreak2,
                Layerbreak3 = this.Layerbreak3,
                DiscID = this.DiscID,
                DiscKey = this.DiscKey,
                UniversalHash = this.UniversalHash,
            };
        }
    }
}
