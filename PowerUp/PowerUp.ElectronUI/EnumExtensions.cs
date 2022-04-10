using PowerUp.ElectronUI.Api.Shared;

namespace PowerUp.ElectronUI
{
  public static class EnumExtensions
  {
    public static KeyedCode ToKeyedCode(this Enum value, bool useAbbrev = false) => new KeyedCode(value.ToString(), useAbbrev ? value.GetAbbrev() : value.GetDisplayName());
    public static KeyedCode ToNumberedKeyedCode<TEnum>(this TEnum value, bool useAbbrev = false, bool addOne = false) where TEnum : struct, Enum
    {
      var intValue = value as int?;
      var displayValue = addOne
        ? intValue + 1
        : intValue;

      var name = useAbbrev 
        ? value.GetAbbrev() 
        : value.GetDisplayName();

      return new KeyedCode(value.ToString(), $"{displayValue} - {name}");
    }


    public static IEnumerable<KeyedCode> GetKeyedCodeList<TEnum>(bool useAbbrev = false) where TEnum : struct, Enum 
      => GetKeyedCodeList<TEnum>(e => ToKeyedCode(e, useAbbrev));

    public static IEnumerable<KeyedCode> GetKeyedCodeList<TEnum>(Func<TEnum, KeyedCode> callack) where TEnum : struct, Enum
      => Enum.GetValues<TEnum>().OrderBy(e => e.GetOrder()).Select(callack);
  }
}
