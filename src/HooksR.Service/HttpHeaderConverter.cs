using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HooksR.Service
{
  public class HttpHeaderConverter : JsonConverter<List<KeyValuePair<string, IEnumerable<string>>>>
  {
    public override List<KeyValuePair<string, IEnumerable<string>>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, List<KeyValuePair<string, IEnumerable<string>>> value, JsonSerializerOptions options)
    {
      if (value == null) return;

      if (writer == null)
      {
        throw new Exception("JSON Writer is NULL. Cannot continue");
      }
      if (value.Count == 0) return;
      writer.WriteStartObject();
      foreach (var item in value)
      {
        writer.WritePropertyName(item.Key);
        writer.WriteStringValue(string.Join(";", item.Value));
      }
      writer.WriteEndObject();
    }
  }
}
