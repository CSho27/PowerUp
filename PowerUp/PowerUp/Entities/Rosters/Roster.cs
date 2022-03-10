using PowerUp.Databases;
using PowerUp.Entities.Teams;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Rosters
{
  public class Roster : Entity<RosterKeyParams>
  {
    public EntitySourceType SourceType { get; set; }
    public string Name { get; set; } = "";
    public int? Year { get; set; }
    public string? ImportSource { get; set; }

    public IDictionary<MLBPPTeam, int> TeamIdsByPPTeam { get; set; } = new Dictionary<MLBPPTeam, int>();
    public IDictionary<Team, MLBPPTeam> GetTeams() => TeamIdsByPPTeam
      .ToDictionary(
        kvp => DatabaseConfig.TeamDatabase.Load(kvp.Value)!,
        kvp => kvp.Key
      );

    protected override RosterKeyParams GetKeyParams() => new RosterKeyParams
    {
      Type = SourceType.ToString().ToUpperInvariant(),
      Name = Name,
      ImportSource = ImportSource
    };
  }

  public class RosterKeyParams : KeyParams
  {
    public string? Type { get; set; }
    public string? Name { get; set; }
    public string? ImportSource { get; set; }
  }
}
