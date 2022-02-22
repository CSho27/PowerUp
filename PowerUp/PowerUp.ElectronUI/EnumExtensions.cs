using PowerUp.ElectronUI.Api;

namespace PowerUp.ElectronUI
{
  public static class EnumExtensions
  {
    public static KeyedCode ToKeyedCode(this Enum value) => new KeyedCode(value.ToString(), value.GetDisplayName());
    public static IEnumerable<KeyedCode> GetKeyedCodeList<TEnum>() where TEnum : struct, Enum 
      => Enum.GetValues<TEnum>().Select(e => ToKeyedCode(e));

  }
}
