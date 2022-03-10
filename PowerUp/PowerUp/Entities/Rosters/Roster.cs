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

    public IDictionary<MLBPPTeam, string> TeamKeysByPPTeam { get; set; } = new Dictionary<MLBPPTeam, string>();
    public IDictionary<Team, MLBPPTeam> GetTeams() => TeamKeysByPPTeam
      .ToDictionary(
        kvp => DatabaseConfig.JsonDatabase.Load<Team>(kvp.Value),
        kvp => kvp.Key
      );

    public override IDictionary<string, string> GetFileKeys() => new Dictionary<string, string>
    {
      { "SourceType", SourceType.ToString() },
      { "Name" , Name },
    };
  }

  public class RosterKeyParams : KeyParams
  {
    public override int Id { get;set; }
    public string Type { get; set; }
    public string? Name { get; set; }
    public int? Year { get; set; }
    public string? ImportSource { get; set; }

    private RosterKeyParams(int id, EntitySourceType type, string? name, string? importSource, int? year)
    {
      Id = id;
      Type = type.ToString().ToUpperInvariant();
      ImportSource = importSource;
      Year = year;
      Name = name;
    }

    public static RosterKeyParams ForBaseRoster()
      => new RosterKeyParams(
        id: 0,
        type: EntitySourceType.Base,
        name: null,
        importSource: null,
        year: null
      );

    public static RosterKeyParams ForImportedRoster(string importSource)
      => new RosterKeyParams(
        id: 0,
        type: EntitySourceType.Imported,
        name: null,
        importSource: importSource,
        year: null
      );

    public static RosterKeyParams ForGeneratedRoster(int year)
      => new RosterKeyParams(
        id: 0,
        type: EntitySourceType.Generated,
        name: null,
        importSource: null,
        year: year
      );

    public static RosterKeyParams ForCustomRoster(string name)
      => new RosterKeyParams(
        id: 0,
        type: EntitySourceType.Custom,
        name: name,
        importSource: null,
        year: null
      );
  }
}
