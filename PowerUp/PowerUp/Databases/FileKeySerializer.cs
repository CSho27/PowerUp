using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Databases
{
  public static class FileKeySerializer
  {
    public static string Serialize<T>(T @object)
    {
      var properties = typeof(T).GetProperties();
      
      if(properties.Any(p => HasUnsupportedPropertyType(p.PropertyType)))
        throw new InvalidOperationException("FileKey object can only contain string, int, or Enum types");

      var values = properties.Select(p => p.GetValue(@object)?.ToString() ?? "");
      return string.Join('_', values);
    }

    public static T Deserialize<T>(string fileKeyString)
    {
      var returnType = typeof(T);
      var properties = returnType.GetProperties().ToArray();

      if (properties.Any(p => HasUnsupportedPropertyType(p.PropertyType)))
        throw new InvalidOperationException("FileKey object can only contain string, int, or Enum types");

      var valueStrings = fileKeyString.Split('_').ToArray();

      var returnObject = returnType.GetConstructors().First().Invoke(null);
      for(int i=0; i<properties.Length; i++)
      {
        var property = properties[i];
        property.SetValue(returnObject, GetValueFromString(property.PropertyType, valueStrings[i]));
      }

      return (T)returnObject;
    }

    private static object? GetValueFromString(Type type, string valueString) 
    {
      if (string.IsNullOrEmpty(valueString))
        return null;

      return type switch
      {
        var t when IsString(t) => valueString,
        var t when IsInt(t) => int.Parse(valueString),
        var t when IsEnum(t) => Enum.Parse(type, valueString),
        _ => throw new InvalidOperationException("Invalid type")
      };
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
