using PowerUp.Databases;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Rosters
{
  public class Roster : Entity<Roster>
  {
    public string Identifier => $"R{Id}";
    public EntitySourceType SourceType { get; set; }
    public string Name { get; set; } = "";
    public int? Year { get; set; }
    public string? ImportSource { get; set; }

    public IDictionary<MLBPPTeam, int> TeamIdsByPPTeam { get; set; } = new Dictionary<MLBPPTeam, int>();
    public IDictionary<Team, MLBPPTeam> GetTeams() => TeamIdsByPPTeam
      .ToDictionary(
        kvp => DatabaseConfig.Database.Load<Team>(kvp.Value)!,
        kvp => kvp.Key
      );

    public IEnumerable<int> FreeAgentPlayerIds { get; set; } = Enumerable.Empty<int>();
    public IEnumerable<Player> GetFreeAgentPlayers() => FreeAgentPlayerIds.Select(id => DatabaseConfig.Database.Load<Player>(id)!);
  }
}
