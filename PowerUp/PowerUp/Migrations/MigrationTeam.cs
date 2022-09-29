using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Migrations
{
  [MigrationTypeFor(typeof(Team))]
  public class MigrationTeam
  {
    public EntitySourceType? SourceType { get; set; }
    public string? Name { get; set; }
    public string? ImportSource { get; set; }
    public long? GeneratedTeam_LSTeamId { get; set; }
    public int? Year { get; set; }
    public IEnumerable<MigrationPlayerRoleDefinition>? PlayerDefinitions { get; set; } = Enumerable.Empty<MigrationPlayerRoleDefinition>();
    public IEnumerable<MigrationLineupSlot>? NoDHLineup { get; set; } = Enumerable.Empty<MigrationLineupSlot>();
    public IEnumerable<MigrationLineupSlot>? DHLineup { get; set; } = Enumerable.Empty<MigrationLineupSlot>();
  }

  [MigrationTypeFor(typeof(PlayerRoleDefinition))]
  public class MigrationPlayerRoleDefinition
  {
    public int? PlayerId { get; set; }
    public bool? IsAAA { get; set; }
    public bool? IsPinchHitter { get; set; }
    public bool? IsPinchRunner { get; set; }
    public bool? IsDefensiveReplacement { get; set; }
    public bool? IsDefensiveLiability { get; set; }
    public PitcherRole? PitcherRole { get; set; }
  }

  [MigrationTypeFor(typeof(LineupSlot))]
  public class MigrationLineupSlot
  {
    public int? PlayerId { get; set; }
    public Position? Position { get; set; }
  }
}
