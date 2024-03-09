using System;
using System.Linq;
using System.Reflection;

namespace PowerUp
{
  public static class EnumExtensions
  {
    public static string GetDisplayName(this Enum value) => value.GetEnumAttribute<DisplayNameAttribute>()?.DisplayName ?? value.ToString();
    public static string GetAbbrev(this Enum value) => value.GetEnumAttribute<AbbrevAttribute>()?.Abbreviation ?? value.ToString();
    public static int? GetOrder(this Enum value) => value.GetEnumAttribute<OrderAttribute>()?.Order; 

    public static TEnum? ToEnum<TEnum>(this string? value) where TEnum : struct, Enum
    {
      Enum.TryParse<TEnum>(value, out var enumValue);
      return enumValue;
    }

    public static TEnum? FromAbbrev<TEnum>(this string? abbrev) where TEnum : struct, Enum
    {
      return Enum.GetValues<TEnum>().FirstOrDefault(e => e.GetAbbrev() == abbrev);
    }

    public static TAttribute? GetEnumAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
    {
      return value
        .GetType()
        .GetMember(value.ToString())
        .FirstOrDefault()
        ?.GetCustomAttribute<TAttribute>();
    }
  }

  [AttributeUsage(AttributeTargets.Field)]
  public class DisplayNameAttribute : Attribute
  {
    public string DisplayName { get; }

    public DisplayNameAttribute(string displayName)
    {
       DisplayName = displayName;
    }
  }

  [AttributeUsage(AttributeTargets.Field)]
  public class AbbrevAttribute : Attribute
  {
    public string Abbreviation { get; }

    public AbbrevAttribute(string abbreviatedDisplayName)
    {
      Abbreviation = abbreviatedDisplayName;
    }
  }

  [AttributeUsage(AttributeTargets.Field)]
  public class OrderAttribute : Attribute
  {
    public int Order { get; }

    public OrderAttribute(int order)
    {
      Order = order;
    }
  }
}
