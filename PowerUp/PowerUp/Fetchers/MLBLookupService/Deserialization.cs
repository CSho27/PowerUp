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
  }
}
