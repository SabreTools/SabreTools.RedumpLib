using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Converters
{
    /// <summary>
    /// Serialize DiscType enum values
    /// </summary>
    public class DiscTypeConverter : JsonConverter<DiscType?>
    {
        public override bool CanRead { get { return true; } }

        public override DiscType? ReadJson(JsonReader reader, Type objectType, DiscType? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue;

            // Read the value
            string? value = reader.Value as string;
            if (value == null)
                return null;

            // Try to parse the value
            return Data.Extensions.ToDiscType(value);
        }

        public override void WriteJson(JsonWriter writer, DiscType? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value.LongName() ?? string.Empty);
            t.WriteTo(writer);
        }
    }
}