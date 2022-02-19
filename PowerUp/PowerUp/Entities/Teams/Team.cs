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
    
    public IEnumerable<PlayerRoleDefinition> Players { get; set; } = Enumerable.Empty<PlayerRoleDefinition>();

    public IEnumerable<(string? playerKey, Position position)> NoDHLineup { get; set; } = Enumerable.Empty<(string?, Position)>();
    public IEnumerable<(string playerKey, Position position)> DHLineup { get; set; } = Enumerable.Empty<(string, Position)>();

    protected override TeamKeyParams GetKeyParams() => SourceType switch
    {
      EntitySourceType.Base => TeamKeyParams.ForBaseTeam(Name),
      EntitySourceType.Imported => TeamKeyParams.ForImportedTeam(ImportSource!, Name),
      EntitySourceType.Generated => TeamKeyParams.ForGeneratedTeam(Name, Year!.Value),
      EntitySourceType.Custom => TeamKeyParams.ForCustomTeam(Name),
      _ => throw new NotImplementedException()
    };
  }
}
