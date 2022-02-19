using PowerUp.Databases;
using PowerUp.Entities.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Teams
{
  public class Team : IHaveDatabaseKeys<TeamDatabaseKeys>
  {
    public EntitySourceType SourceType { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string? ImportSource { get; set; }

    public IEnumerable<PlayerDatabaseKeys> PlayerKeys { get; set; } = Enumerable.Empty<PlayerDatabaseKeys>();
    
    public IEnumerable<PlayerRoleDefinition> PlayerRoles { get; set; } = Enumerable.Empty<PlayerRoleDefinition>();

    public IEnumerable<(PlayerDatabaseKeys playerKeys, Position position)> NoDHLineup { get; set; } = Enumerable.Empty<(PlayerDatabaseKeys, Position)>();
    public IEnumerable<(PlayerDatabaseKeys playerKeys, Position position)> DHLineup { get; set; } = Enumerable.Empty<(PlayerDatabaseKeys, Position)>();

    TeamDatabaseKeys IHaveDatabaseKeys<TeamDatabaseKeys>.DatabaseKeys => SourceType switch
    {
      EntitySourceType.Base => TeamDatabaseKeys.ForBaseTeam(Name),
      EntitySourceType.Imported => TeamDatabaseKeys.ForImportedTeam(ImportSource!, Name),
      EntitySourceType.Generated => TeamDatabaseKeys.ForGeneratedTeam(Name, Year!.Value),
      EntitySourceType.Custom => TeamDatabaseKeys.ForCustomTeam(Name),
      _ => throw new NotImplementedException()
    };
  }
}
