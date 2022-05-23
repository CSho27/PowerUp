using PowerUp.Entities.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Entities.Rosters.Api
{
  public interface IRosterApi
  {
    void ReplaceTeam(Roster roster, MLBPPTeam teamSlotToUse, Team teamToInsert);
  }

  public class RosterApi : IRosterApi
  {
    public void ReplaceTeam(Roster roster, MLBPPTeam teamSlotToUse, Team teamToInsert)
    {
      roster.TeamIdsByPPTeam.Remove(teamSlotToUse);
      roster.TeamIdsByPPTeam.Add(teamSlotToUse, teamToInsert.Id!.Value);
    }
  }
}
