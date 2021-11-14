using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AcraBackend.Server.Utils
{
    /// <summary>
    /// Forces the JSON deserialiser to read an inner json structure as a string,
    /// otherwise it needs a structure to deserialise against.
    /// </summary>
    public class RawJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var raw = JRaw.Create(reader);
            return raw.ToString();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var s = (string)value;
            writer.WriteRawValue(s);
        }
    }
}