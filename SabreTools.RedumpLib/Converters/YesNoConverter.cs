using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Converters
{
    /// <summary>
    /// Serialize YesNo enum values
    /// </summary>
    public class YesNoConverter : JsonConverter<YesNo?>
    {
        public override bool CanRead { get { return true; } }

        public override YesNo? ReadJson(JsonReader reader, Type objectType, YesNo? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue;

            // Read the value
            if (reader.Value is bool bVal)
                return Data.Extensions.ToYesNo(bVal);
            else if (reader.Value is string sVal)
                return Data.Extensions.ToYesNo(sVal);

            return null;
        }

        public override void WriteJson(JsonWriter writer, YesNo? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value.LongName() ?? string.Empty);
            t.WriteTo(writer);
        }
    }
}
