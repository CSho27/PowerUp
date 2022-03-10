using PowerUp.Databases;
using PowerUp.Entities.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Teams
{
  public class Team : Entity<TeamKeyParams>
  {
    public EntitySourceType SourceType { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string? ImportSource { get; set; }
    
    public IEnumerable<PlayerRoleDefinition> PlayerDefinitions { get; set; } = Enumerable.Empty<PlayerRoleDefinition>();

    public IEnumerable<LineupSlot> NoDHLineup { get; set; } = Enumerable.Empty<LineupSlot>();
    public IEnumerable<LineupSlot> DHLineup { get; set; } = Enumerable.Empty<LineupSlot>();

    public override IDictionary<string, string> GetFileKeys() => new Dictionary<string, string>
    {
      { "SourceType", SourceType.ToString() },
      { "Name" , Name },
    };

    public IEnumerable<Player> GetPlayers() => PlayerDefinitions
      .Select(pd => DatabaseConfig.JsonDatabase.Load<Player>(pd.PlayerKey));
  }
}
