using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.RedumpInfo.Data;

namespace SabreTools.RedumpLib.RedumpInfo.Converters
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
            if (reader.Value is not string value)
                return null;

            // Try to parse the value
            return value.ToRedumpSystem();
        }

        public override void WriteJson(JsonWriter writer, RedumpSystem? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value.ShortName() ?? string.Empty);
            t.WriteTo(writer);
        }
    }
}
