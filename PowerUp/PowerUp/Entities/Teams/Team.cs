using PowerUp.Entities.Players;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Teams
{
  public class Team
  {
    public EntitySourceType SourceType { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? FranchiseId { get; set; }
    public int? Year { get; set; }
    public string? ImportSource { get; set; }

    public IEnumerable<PlayerDatabaseKeys> PlayerKeys { get; set; } = Enumerable.Empty<PlayerDatabaseKeys>();
    
    public IEnumerable<PlayerRoleDefinition> PlayerRoles { get; set; } = Enumerable.Empty<PlayerRoleDefinition>();

    public IEnumerable<(PlayerDatabaseKeys playerKeys, Position position)> NoDHLineup { get; set; } = Enumerable.Empty<(PlayerDatabaseKeys, Position)>();
    public IEnumerable<(PlayerDatabaseKeys playerKeys, Position position)> DHLineup { get; set; } = Enumerable.Empty<(PlayerDatabaseKeys, Position)>();

  }
}
