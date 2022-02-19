using PowerUp.Entities.Players;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Teams
{
  public class Team
  {
    public string Name { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public IEnumerable<Player> Players { get; private set; } = Enumerable.Empty<Player>();
  }
}
