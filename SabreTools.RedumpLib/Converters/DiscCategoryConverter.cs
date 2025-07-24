using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Converters
{
    /// <summary>
    /// Serialize DiscCategory enum values
    /// </summary>
    public class DiscCategoryConverter : JsonConverter<DiscCategory?>
    {
        public override bool CanRead { get { return true; } }

        public override DiscCategory? ReadJson(JsonReader reader, Type objectType, DiscCategory? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue;

            // Read the value
            string? value = reader.Value as string;
            if (value == null)
                return null;

            // Try to parse the value
            return Data.Extensions.ToDiscCategory(value);
        }

        public override void WriteJson(JsonWriter writer, DiscCategory? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value.LongName() ?? string.Empty);
            t.WriteTo(writer);
        }
    }
}
