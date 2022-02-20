using PowerUp.Databases;
using System;
using System.Collections.Generic;

namespace PowerUp.Entities.Rosters
{
  public class Roster : Entity<RosterKeyParams>
  {
    public EntitySourceType SourceType { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string? ImportSource { get; set; }

    public IDictionary<MLBPPTeam, string> MappedTeams { get; set; } = new Dictionary<MLBPPTeam, string>();

    protected override RosterKeyParams GetKeyParams() => SourceType switch
    {
      EntitySourceType.Base => RosterKeyParams.ForBaseTeam(Name),
      EntitySourceType.Imported => RosterKeyParams.ForImportedTeam(ImportSource!, Name),
      EntitySourceType.Generated => RosterKeyParams.ForGeneratedTeam(Name, Year!.Value),
      EntitySourceType.Custom => RosterKeyParams.ForCustomTeam(Name),
      _ => throw new NotImplementedException()
    };
  }

  public class RosterKeyParams : KeyParams
  {
    public string Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string? ImportSource { get; set; }

    private RosterKeyParams(EntitySourceType type, string name, string? importSource, int? year)
    {
      Type = type.ToString().ToUpperInvariant();
      ImportSource = importSource;
      Year = year;
      Name = name;
    }

    public static RosterKeyParams ForBaseTeam(string name)
      => new RosterKeyParams(
        type: EntitySourceType.Base,
        name: name,
        importSource: null,
        year: null
      );

    public static RosterKeyParams ForImportedTeam(string importSource, string name)
      => new RosterKeyParams(
        type: EntitySourceType.Imported,
        name: name,
        importSource: importSource,
        year: null
      );

    public static RosterKeyParams ForGeneratedTeam(string name, int year)
      => new RosterKeyParams(
        type: EntitySourceType.Generated,
        name: name,
        importSource: null,
        year: year
      );

    public static RosterKeyParams ForCustomTeam(string name)
      => new RosterKeyParams(
        type: EntitySourceType.Custom,
        name: name,
        importSource: null,
        year: null
      );
  }
}
