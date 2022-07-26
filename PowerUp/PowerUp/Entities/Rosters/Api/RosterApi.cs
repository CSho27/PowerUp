using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using System.Linq;

namespace PowerUp.Entities.Rosters.Api
{
  public interface IRosterApi
  {
    void EditRosterName(Roster roster, string name);
    void ReplaceTeam(Roster roster, MLBPPTeam teamSlotToUse, Team teamToInsert);
    void ReplaceFreeAgent(Roster roster, Player playerToReplace, Player playerToInsert);
    Roster CreateCustomCopyOfRoster(Roster roster);
  }

  public class RosterApi : IRosterApi
  {
    public void EditRosterName(Roster roster, string name)
    {
      roster.Name = name;
    }

    public void ReplaceTeam(Roster roster, MLBPPTeam teamSlotToUse, Team teamToInsert)
    {
      roster.TeamIdsByPPTeam.Remove(teamSlotToUse);
      roster.TeamIdsByPPTeam.Add(teamSlotToUse, teamToInsert.Id!.Value);
    }

    public void ReplaceFreeAgent(Roster roster, Player playerToReplace, Player playerToInsert)
    {
      roster.FreeAgentPlayerIds = roster.FreeAgentPlayerIds.Where(id => id != playerToReplace.Id);
      roster.FreeAgentPlayerIds = roster.FreeAgentPlayerIds.Append(playerToInsert.Id!.Value);
    }

    public Roster CreateCustomCopyOfRoster(Roster roster)
    {
      return new Roster
      {
        SourceType = EntitySourceType.Custom,
        Name = roster.Name,
        TeamIdsByPPTeam = roster.TeamIdsByPPTeam,
        FreeAgentPlayerIds = roster.FreeAgentPlayerIds,
        Year = roster.Year,
      };
    }
  }
}
