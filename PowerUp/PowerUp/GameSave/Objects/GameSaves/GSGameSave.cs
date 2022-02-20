using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Players;
using PowerUp.GameSave.Objects.Teams;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.GameSave.Objects.GameSaves
{
  public class GSGameSave
  {
    public IEnumerable<GSPlayer> Players { get; set; } = Enumerable.Empty<GSPlayer>();
    public IEnumerable<GSTeam> Teams { get; set; } = Enumerable.Empty<GSTeam>();
    public IEnumerable<GSLineupDefinition> Lineups { get; set; } = Enumerable.Empty<GSLineupDefinition>();
  }
}
