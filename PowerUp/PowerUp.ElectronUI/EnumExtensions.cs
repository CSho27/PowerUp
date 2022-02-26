using PowerUp.ElectronUI.Api.Rosters;
using PowerUp.ElectronUI.Api.Shared;

namespace PowerUp.ElectronUI
{
  public static class EnumExtensions
  {
    public static KeyedCode ToKeyedCode(this Enum value) => new KeyedCode(value.ToString(), value.GetDisplayName());
    public static IEnumerable<KeyedCode> GetKeyedCodeList<TEnum>() where TEnum : struct, Enum 
      => Enum.GetValues<TEnum>().Select(e => ToKeyedCode(e));

  }
}
