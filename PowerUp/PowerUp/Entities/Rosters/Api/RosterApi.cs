using PowerUp.Entities.Teams;

namespace PowerUp.Entities.Rosters.Api
{
  public interface IRosterApi
  {
    void EditRosterName(Roster roster, string name);
    void ReplaceTeam(Roster roster, MLBPPTeam teamSlotToUse, Team teamToInsert);
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
  }
}
