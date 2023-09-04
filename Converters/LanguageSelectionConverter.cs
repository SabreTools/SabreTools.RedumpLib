using System;
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
        public override bool CanRead { get { return false; } }

#if NET48
        public override LanguageSelection?[] ReadJson(JsonReader reader, Type objectType, LanguageSelection?[] existingValue, bool hasExistingValue, JsonSerializer serializer)
#else
        public override LanguageSelection?[] ReadJson(JsonReader reader, Type objectType, LanguageSelection?[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
#endif
        {
            throw new NotImplementedException();
        }

#if NET48
        public override void WriteJson(JsonWriter writer, LanguageSelection?[] value, JsonSerializer serializer)
#else
        public override void WriteJson(JsonWriter writer, LanguageSelection?[]? value, JsonSerializer serializer)
#endif
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