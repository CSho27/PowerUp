using PowerUp.Entities.Players;
using System.Linq;

namespace PowerUp.Entities.Teams.Api
{
  public interface ITeamApi
  {
    void ReplacePlayer(Team team, Player playerToRemove, Player playerToInsert);
  }

  public class TeamApi : ITeamApi
  {
    public void ReplacePlayer(Team team, Player playerToRemove, Player playerToInsert)
    {
      var playerDefinition = team.PlayerDefinitions.Single(d => d.PlayerId == playerToRemove.Id);
      playerDefinition.PlayerId = playerToInsert.Id!.Value;
    }
  }
}
