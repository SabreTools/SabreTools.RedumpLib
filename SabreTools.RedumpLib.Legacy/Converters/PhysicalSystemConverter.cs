using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.Legacy.Data;

namespace SabreTools.RedumpLib.Legacy.Converters
{
    /// <summary>
    /// Serialize PhysicalSystem enum values
    /// </summary>
    public class PhysicalSystemConverter : JsonConverter<PhysicalSystem?>
    {
        public override bool CanRead { get { return true; } }

        public override PhysicalSystem? ReadJson(JsonReader reader, Type objectType, PhysicalSystem? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue;

            // Read the value
            if (reader.Value is not string value)
                return null;

            // Try to parse the value
            return value.ToPhysicalSystem();
        }

        public override void WriteJson(JsonWriter writer, PhysicalSystem? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value.ShortName() ?? string.Empty);
            t.WriteTo(writer);
        }
    }
}
