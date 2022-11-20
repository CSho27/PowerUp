using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Teams;
using System.Collections.Generic;

namespace PowerUp.Migrations
{
  public static class RosterLateMappers
  {
    public class TeamIdsByPPTeamLateMapper : LateMapper<Roster>
    {
      public override void Perform(MigrationIdDictionary migrationIdDictionary, Roster importedRoster, Roster savedRoster)
      {
        var savedTeamIdsByImportedId = migrationIdDictionary[typeof(Team)];
        savedRoster.TeamIdsByPPTeam = new Dictionary<MLBPPTeam, int>();
        foreach(var entry in importedRoster.TeamIdsByPPTeam)
        {
          savedTeamIdsByImportedId.TryGetValue(entry.Value, out var savedTeamId);
          savedRoster.TeamIdsByPPTeam.Add(entry.Key, savedTeamId);
        }
      }
    }

    public class FreeAgentPlayerIdsLateMapper : LateMapper<Roster>
    {
      public override void Perform(MigrationIdDictionary migrationIdDictionary, Roster importedRoster, Roster savedRoster)
      {
        var savedPlayerIdsByImportedId = migrationIdDictionary[typeof(Player)];
        var faPlayerIds = new List<int>();
        foreach (var importedPlayerId in importedRoster.FreeAgentPlayerIds)
          faPlayerIds.Add(savedPlayerIdsByImportedId[importedPlayerId]);
        savedRoster.FreeAgentPlayerIds = faPlayerIds;
      }
    }
  }
}
