using PowerUp.Entities.Players;
using System.Linq;

namespace PowerUp.Entities.Teams.Api
{
  public interface ITeamApi
  {
    void ReplacePlayer(Team team, Player playerToRemove, Player playerToInsert);
    void EditTeam(Team team, TeamParameters parameters);
  }

  public class TeamApi : ITeamApi
  {
    public void ReplacePlayer(Team team, Player playerToRemove, Player playerToInsert)
    {
      var playerDefinition = team.PlayerDefinitions.Single(d => d.PlayerId == playerToRemove.Id);
      var noDHLineupSlot = team.NoDHLineup.SingleOrDefault(s => s.PlayerId == playerToRemove.Id);
      var dhLienupSlot = team.DHLineup.SingleOrDefault(s => s.PlayerId == playerToRemove.Id);

      playerDefinition.PlayerId = playerToInsert.Id!.Value;
      
      if(noDHLineupSlot != null)
        noDHLineupSlot.PlayerId = playerToInsert.Id!.Value;

      if(dhLienupSlot != null)
        dhLienupSlot.PlayerId= playerToInsert.Id!.Value;
    }

    public void EditTeam(Team team, TeamParameters parameters)
    {
      new TeamParamtersValidator().Validate(parameters);

      team.Name = parameters.Name!;

      var allPlayers = parameters.MLBPlayers.Concat(parameters.AAAPlayers);
      var diffResult = SetUtils.GetDiff(allPlayers, team.PlayerDefinitions, (p, d) => p.PlayerId == d.PlayerId);

      foreach(var playerToAdd in diffResult.ANotInB)
      {
        var newRoleDefinition = new PlayerRoleDefinition(playerToAdd.PlayerId)
        {
          IsAAA = parameters.AAAPlayers.Any(p => playerToAdd.PlayerId == p.PlayerId),
          IsPinchHitter = playerToAdd.IsPinchHitter,
          IsPinchRunner = playerToAdd.IsPinchRunner,
          IsDefensiveReplacement = playerToAdd.IsDefensiveReplacement,
          IsDefensiveLiability = playerToAdd.IsDefensiveLiability,
          //PitcherRole = playerToAdd.PitcherRole,
        };
        team.PlayerDefinitions.Append(newRoleDefinition);
      }

      foreach(var (updatedParams, currentDef) in diffResult.Matches) {
        currentDef.IsAAA = parameters.AAAPlayers.Any(p => updatedParams.PlayerId == p.PlayerId);
        currentDef.IsPinchHitter = updatedParams.IsPinchHitter;
        currentDef.IsPinchRunner = updatedParams.IsPinchRunner;
        currentDef.IsDefensiveReplacement = updatedParams.IsDefensiveReplacement;
        currentDef.IsDefensiveLiability = updatedParams.IsDefensiveLiability;
        //currentDef.PitcherRole = updatedParams.PitcherRole;
      }

      foreach(var playerToRemove in diffResult.BNotInA)
        team.PlayerDefinitions = team.PlayerDefinitions.Where(d => d.PlayerId != playerToRemove.PlayerId);
    }
  }
}
