using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Converters
{
    /// <summary>
    /// Serialize RedumpSystem enum values
    /// </summary>
    public class SystemConverter : JsonConverter<RedumpSystem?>
    {
        public override bool CanRead { get { return true; } }

        public override RedumpSystem? ReadJson(JsonReader reader, Type objectType, RedumpSystem? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue;

            // Read the value
            string? value = reader.Value as string;
            if (value == null)
                return null;

            // Try to parse the value
            return Data.Extensions.ToRedumpSystem(value);
        }

        public override void WriteJson(JsonWriter writer, RedumpSystem? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value.ShortName() ?? string.Empty);
            t.WriteTo(writer);
        }
    }
}
