using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Converters
{
    /// <summary>
    /// Serialize Language enum values
    /// </summary>
    public class LanguageConverter : JsonConverter<Language?[]>
    {
        public override bool CanRead { get { return true; } }

        public override Language?[] ReadJson(JsonReader reader, Type objectType, Language?[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue ?? [];

            // Get the current depth for checking
            int currentDepth = reader.Depth;

            // Read the array while it exists
            List<Language> languages = [];
            while (reader.Read() && reader.Depth > currentDepth)
            {
                string? value = reader.Value as string;
                if (value == null)
                    continue;

                Language? lang = Data.Extensions.ToLanguage(value);
                if (lang != null)
                    languages.Add(lang.Value);
            }

            return [.. languages];
        }

        public override void WriteJson(JsonWriter writer, Language?[]? value, JsonSerializer serializer)
        {
            if (value == null)
                return;

            JArray array = new JArray();
            foreach (var val in value)
            {
                JToken t = JToken.FromObject(val.ShortName() ?? string.Empty);
                array.Add(t);
            }

            array.WriteTo(writer);
        }
    }
}
