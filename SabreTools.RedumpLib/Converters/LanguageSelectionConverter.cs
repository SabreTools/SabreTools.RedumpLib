using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Converters
{
    /// <summary>
    /// Serialize LanguageSelection enum values
    /// </summary>
    public class LanguageSelectionConverter : JsonConverter<LanguageSelection?[]>
    {
        public override bool CanRead { get { return true; } }

        public override LanguageSelection?[] ReadJson(JsonReader reader, Type objectType, LanguageSelection?[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue ?? [];

            // Get the current depth for checking
            int currentDepth = reader.Depth;

            // Read the array while it exists
            List<LanguageSelection> selections = [];
            while (reader.Read() && reader.Depth > currentDepth)
            {
                string? value = reader.Value as string;
                if (value == null)
                    continue;

                LanguageSelection? sel = Data.Extensions.ToLanguageSelection(value);
                if (sel != null)
                    selections.Add(sel.Value);
            }

            return [.. selections];
        }

        public override void WriteJson(JsonWriter writer, LanguageSelection?[]? value, JsonSerializer serializer)
        {
            if (value == null)
                return;

            JArray array = new JArray();
            foreach (var val in value)
            {
                JToken t = JToken.FromObject(val.LongName() ?? string.Empty);
                array.Add(t);
            }

            array.WriteTo(writer);
        }
    }
}
