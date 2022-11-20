using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using System.Collections.Generic;

namespace PowerUp.Migrations
{
  public static class TeamLateMappers
  {
    public class PlayerDefinitionsLateMapper : LateMapper<Team>
    {
      public override void Perform(MigrationIdDictionary migrationIdDictionary, Team importedTeam, Team savedTeam)
      {
        var savedPlayerIdsByImportedId = migrationIdDictionary[typeof(Player)];
        var playerDefinitions = new List<PlayerRoleDefinition>();
        foreach (var def in importedTeam.PlayerDefinitions)
        {
          playerDefinitions.Add(new PlayerRoleDefinition(savedPlayerIdsByImportedId[def.PlayerId])
          {
            IsAAA = def.IsAAA,
            PitcherRole = def.PitcherRole,
            IsPinchHitter = def.IsPinchHitter,
            IsPinchRunner = def.IsPinchRunner,
            IsDefensiveReplacement = def.IsDefensiveReplacement,
            IsDefensiveLiability = def.IsDefensiveLiability
          });
        }
        savedTeam.PlayerDefinitions = playerDefinitions;
      }
    }

    public class NoDHLineupLateMapper : LateMapper<Team>
    {
      public override void Perform(MigrationIdDictionary migrationIdDictionary, Team importedTeam, Team savedTeam)
      {
        var savedPlayerIdsByImportedId = migrationIdDictionary[typeof(Player)];
        var noDHLineup = new List<LineupSlot>();
        foreach (var slot in importedTeam.NoDHLineup)
        {
          noDHLineup.Add(new LineupSlot
          {
            PlayerId = slot.Position != Position.Pitcher
             ? savedPlayerIdsByImportedId[slot.PlayerId!.Value]
             : null,
            Position = slot.Position
          });
        }
        savedTeam.NoDHLineup = noDHLineup;
      }
    }

    public class DHLineupLateMapper : LateMapper<Team>
    {
      public override void Perform(MigrationIdDictionary migrationIdDictionary, Team importedTeam, Team savedTeam)
      {
        var savedPlayerIdsByImportedId = migrationIdDictionary[typeof(Player)];
        var dhLineup = new List<LineupSlot>();
        foreach (var slot in importedTeam.DHLineup)
        {
          dhLineup.Add(new LineupSlot
          {
            PlayerId = savedPlayerIdsByImportedId[slot.PlayerId!.Value],
            Position = slot.Position
          });
        }
        savedTeam.DHLineup = dhLineup;
      }
    }
  }
}
