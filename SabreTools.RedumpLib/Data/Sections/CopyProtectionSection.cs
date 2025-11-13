using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Converters;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Copy protection section of New Disc form
    /// </summary>
    public class CopyProtectionSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_protection_a", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(YesNoConverter))]
        public YesNo? AntiModchip { get; set; }

        [JsonProperty(PropertyName = "d_protection_1", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(YesNoConverter))]
        public YesNo? LibCrypt { get; set; }

        [JsonProperty(PropertyName = "d_libcrypt", NullValueHandling = NullValueHandling.Ignore)]
        public string? LibCryptData { get; set; }

        [JsonProperty(PropertyName = "d_protection", NullValueHandling = NullValueHandling.Ignore)]
        public string? Protection { get; set; }

        [JsonIgnore]
        public Dictionary<string, List<string>?>? FullProtections { get; set; }

        [JsonProperty(PropertyName = "d_securom", NullValueHandling = NullValueHandling.Ignore)]
        public string? SecuROMData { get; set; }

        public object Clone()
        {
            Dictionary<string, List<string>?>? fullProtections = null;
            if (this.FullProtections != null)
            {
                fullProtections = [];
                foreach (var kvp in this.FullProtections)
                {
                    fullProtections[kvp.Key] = kvp.Value;
                }
            }

            return new CopyProtectionSection
            {
                AntiModchip = this.AntiModchip,
                LibCrypt = this.LibCrypt,
                LibCryptData = this.LibCryptData,
                Protection = this.Protection,
                FullProtections = fullProtections,
                SecuROMData = this.SecuROMData,
            };
        }
    }
}
