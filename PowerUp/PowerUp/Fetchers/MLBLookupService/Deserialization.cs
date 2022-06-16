using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PowerUp.Fetchers.MLBLookupService
{
  public static class Deserialization
  {
    public static IEnumerable<T> SingleArrayOrNullToEnumerable<T>(JsonElement? element)
    {
      if (!element.HasValue)
        return Enumerable.Empty<T>();

      try
      {
        return JsonSerializer.Deserialize<IEnumerable<T>>(element.Value)!;
      }
      catch (JsonException)
      {
        return new[] { JsonSerializer.Deserialize<T>(element.Value)! };
      }
    }

    public static T? SingleOrNullToSingle<T>(JsonElement? element) where T : class
      => element.HasValue
        ? JsonSerializer.Deserialize<T?>(element.Value)
        : null;

    public static string? StringIfNotEmpty(this string? value) => string.IsNullOrEmpty(value)
      ? null
      : value;

    public static DateTime? TryParseDateTime(this string? value) {
      if (string.IsNullOrEmpty(value))
        return null;

      var success = DateTime.TryParse(value, out var datetime);
      return success
        ? datetime
        : null;
    }

    public static int? TryParseInt(this string? value)
    {
      if (string.IsNullOrEmpty(value))
        return null;
      var success = int.TryParse(value, out var integer);
      return success
        ? integer
        : null;
    }

    public static double? TryParseDouble(this string? value)
    {
      if (string.IsNullOrEmpty(value))
        return null;

      var success = double.TryParse(value, out var dub);
      return success
        ? dub
        : null;
    }
  }
}
