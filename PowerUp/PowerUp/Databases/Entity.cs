using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Databases
{
  public abstract class Entity<TKeyParams> where TKeyParams : KeyParams
  {
    public int? Id { get; set; }
    protected abstract TKeyParams GetKeyParams();

    public TKeyParams GetFileKeys()
    {
      var @params = GetKeyParams();
      @params.Id = Id!.Value;
      return @params;
    }
  }

  public abstract class KeyParams
  {
    public int Id { get; set; }

    public IEnumerable<KeyValuePair<string, string>> GetKeysAndValues()
    {
      var properties = GetType().GetProperties().OrderByDescending(p => p.Name == "Id");

      if (properties.Any(p => HasUnsupportedPropertyType(p.PropertyType)))
        throw new InvalidOperationException("FileKey object can only contain string, int, or Enum types");

      return properties.Select(p => new KeyValuePair<string, string>(p.Name, p.GetValue(this)?.ToString() ?? ""));
    }

    private static bool IsString(Type type) => type.IsAssignableTo(typeof(string));
    private static bool IsInt(Type type) => type.IsAssignableTo(typeof(int?));
    private static bool IsEnum(Type type) => type.IsAssignableTo(typeof(Enum))
      || (Nullable.GetUnderlyingType(type)?.IsAssignableTo(typeof(Enum)) ?? false);

    private static bool HasUnsupportedPropertyType(Type type)
    {
      return !IsString(type)
        && !IsInt(type)
        && !IsEnum(type);
    }
  }
}
