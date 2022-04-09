using PowerUp.ElectronUI.Api.Shared;

namespace PowerUp.ElectronUI
{
  public static class EnumExtensions
  {
    public static KeyedCode ToKeyedCode(this Enum value, bool useAbbrev = false) => new KeyedCode(value.ToString(), useAbbrev ? value.GetAbbrev() : value.GetDisplayName());
    public static IEnumerable<KeyedCode> GetKeyedCodeList<TEnum>(bool useAbbrev = false) where TEnum : struct, Enum 
      => Enum.GetValues<TEnum>().OrderBy(e => e.GetOrder()).Select(e => ToKeyedCode(e, useAbbrev));
  }
}
