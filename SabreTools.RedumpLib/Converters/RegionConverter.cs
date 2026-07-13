using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Converters
{
    /// <summary>
    /// Serialize Region enum values
    /// </summary>
    public class RegionConverter : JsonConverter<RegionCode?[]>
    {
        public override bool CanRead { get { return true; } }

        public override RegionCode?[] ReadJson(JsonReader reader, Type objectType, RegionCode?[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue ?? [];

            // Get the current depth for checking
            int currentDepth = reader.Depth;

            // Read the array while it exists
            List<RegionCode> regions = [];
            while (reader.Read() && reader.Depth > currentDepth)
            {
                if (reader.Value is not string value)
                    continue;

                RegionCode? region = value.ToRegionCode();
                if (region is not null)
                    regions.Add(region);
            }

            return [.. regions];
        }

        public override void WriteJson(JsonWriter writer, RegionCode?[]? value, JsonSerializer serializer)
        {
            if (value is null)
                return;

            JArray array = [];
            foreach (var val in value)
            {
                JToken t = JToken.FromObject(val?.Code ?? string.Empty);
                array.Add(t);
            }

            array.WriteTo(writer);
        }
    }
}
