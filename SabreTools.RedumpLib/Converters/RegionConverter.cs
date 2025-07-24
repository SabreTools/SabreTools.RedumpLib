using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Converters
{
    /// <summary>
    /// Serialize Region enum values
    /// </summary>
    public class RegionConverter : JsonConverter<Region?>
    {
        public override bool CanRead { get { return true; } }

        public override Region? ReadJson(JsonReader reader, Type objectType, Region? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue;

            // Read the value
            string? value = reader.Value as string;
            if (value == null)
                return null;

            // Try to parse the value
            return Data.Extensions.ToRegion(value);
        }

        public override void WriteJson(JsonWriter writer, Region? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value.ShortName() ?? string.Empty);
            t.WriteTo(writer);
        }
    }
}
