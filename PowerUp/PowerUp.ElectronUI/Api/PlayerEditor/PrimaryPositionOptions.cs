using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;
using System.Collections;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class PrimaryPositionOptions : IEnumerable<KeyedCode>
  {
    public IEnumerator<KeyedCode> GetEnumerator()
      => Enum.GetValues<Position>()
        .Select(e => e.ToKeyedCode(true))
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
