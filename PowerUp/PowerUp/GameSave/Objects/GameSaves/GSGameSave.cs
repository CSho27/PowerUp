using PowerUp.GameSave.Objects.FreeAgents;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Players;
using PowerUp.GameSave.Objects.Teams;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.GameSave.Objects.GameSaves
{
  public class GSGameSave
  { 
    public static long PowerUpIdOffset = 0xB3FF8;

    public short? PowerUpId { get; set; }
    public IEnumerable<IGSPlayer> Players { get; set; } = Enumerable.Empty<IGSPlayer>();
    public IEnumerable<IGSTeam> Teams { get; set; } = Enumerable.Empty<IGSTeam>();
    public IEnumerable<GSLineupDefinition> Lineups { get; set; } = Enumerable.Empty<GSLineupDefinition>();
    public GSFreeAgentList FreeAgents { get; set; } = new GSFreeAgentList();
  }
}
