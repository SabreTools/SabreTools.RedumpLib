using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Converters
{
    /// <summary>
    /// Serialize DiscTag enum values
    /// </summary>
    public class DiscTagConverter : JsonConverter<DiscTag[]>
    {
        public override bool CanRead { get { return true; } }

        public override DiscTag[] ReadJson(JsonReader reader, Type objectType, DiscTag[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue ?? [];

            // Get the current depth for checking
            int currentDepth = reader.Depth;

            // Read the array while it exists
            List<DiscTag> tags = [];
            while (reader.Read() && reader.Depth > currentDepth)
            {
                if (reader.Value is not string value)
                    continue;

                DiscTag? tag = value.ToDiscTag();
                if (tag is not null)
                    tags.Add(tag.Value);
            }

            return [.. tags];
        }

        public override void WriteJson(JsonWriter writer, DiscTag[]? value, JsonSerializer serializer)
        {
            if (value is null)
                return;

            JArray array = [];
            foreach (var val in value)
            {
                JToken t = JToken.FromObject(val.LongName() ?? string.Empty);
                array.Add(t);
            }

            array.WriteTo(writer);
        }
    }
}
