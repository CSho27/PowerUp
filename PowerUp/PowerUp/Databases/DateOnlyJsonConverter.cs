using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerUp.Databases
{
  public class DateOnlyJsonConverter : JsonConverter<DateOnly>
  {
    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            DateOnly.ParseExact(reader.GetString()!,
                "MMddyyyy", CultureInfo.InvariantCulture);

    public override void Write(
        Utf8JsonWriter writer,
        DateOnly dateTimeValue,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(dateTimeValue.ToString(
                "MMddyyyy", CultureInfo.InvariantCulture));
  }
}
