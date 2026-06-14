using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.RedumpInfo.Data;

namespace SabreTools.RedumpLib.RedumpInfo.Converters
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
            if (reader.Value is not string value)
                return null;

            // Try to parse the value
            return value.ToRegion();
        }

        public override void WriteJson(JsonWriter writer, Region? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value.ShortName() ?? string.Empty);
            t.WriteTo(writer);
        }
    }
}
