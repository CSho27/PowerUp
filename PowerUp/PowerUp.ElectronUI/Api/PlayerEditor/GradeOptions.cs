using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;
using System.Collections;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class GradeOptions : IEnumerable<KeyedCode>
  {
    public IEnumerator<KeyedCode> GetEnumerator()
      => Enum.GetValues<Grade>()
        .OrderByDescending(g => g)
        .Select(e => e.ToKeyedCode(true))
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
