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
    public class LanguageConverter : JsonConverter<LanguageCode?[]>
    {
        public override bool CanRead { get { return true; } }

        public override LanguageCode?[] ReadJson(JsonReader reader, Type objectType, LanguageCode?[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // If we have a value already, don't overwrite it
            if (hasExistingValue)
                return existingValue ?? [];

            // Get the current depth for checking
            int currentDepth = reader.Depth;

            // Read the array while it exists
            List<LanguageCode> languages = [];
            while (reader.Read() && reader.Depth > currentDepth)
            {
                if (reader.Value is not string value)
                    continue;

                LanguageCode? lang = value.ToLanguageCode();
                if (lang is not null)
                    languages.Add(lang);
            }

            return [.. languages];
        }

        public override void WriteJson(JsonWriter writer, LanguageCode?[]? value, JsonSerializer serializer)
        {
            if (value is null)
                return;

            JArray array = [];
            foreach (var val in value)
            {
                JToken t = JToken.FromObject(val?.TwoLetterCode ?? string.Empty);
                array.Add(t);
            }

            array.WriteTo(writer);
        }
    }
}
