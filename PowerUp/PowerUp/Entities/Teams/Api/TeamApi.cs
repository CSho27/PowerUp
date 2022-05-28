using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Teams.Api
{
  public interface ITeamApi
  {
    Team CreateDefaultTeam(EntitySourceType sourceType, Action<Player> savePlayer);
    Team CreateCustomCopyOfTeam(Team team);
    void ReplacePlayer(Team team, Player playerToRemove, Player playerToInsert);
    void EditTeam(Team team, TeamParameters parameters);
  }

  public class TeamApi : ITeamApi
  {
    private readonly IPlayerApi _playerApi;

    public TeamApi(IPlayerApi playerApi)
    {
      _playerApi = playerApi;
    }

    public Team CreateDefaultTeam(EntitySourceType sourceType, Action<Player> savePlayer)
    {
      var players = new List<Player>();
      for(int i=0; i<25; i++)
      {
        var player = _playerApi.CreateDefaultPlayer(sourceType, isPitcher: i < 13);
        savePlayer(player);
        players.Add(player);
      }

      return new Team
      {
        SourceType = sourceType,
        Name = "(New!)",
        PlayerDefinitions = players.Select((p, i) => new PlayerRoleDefinition(p.Id!.Value) { PitcherRole = GetPitcherRoleForIndex(i) }),
        NoDHLineup = players
          .Where(p => p.PrimaryPosition != Position.Pitcher)
          .Take(8)
          .Select((p, i) => new LineupSlot 
            { 
              PlayerId = p.Id!.Value, 
              Position = (Position)(i+2)
            })
          .Append(new LineupSlot { Position = Position.Pitcher }),
        DHLineup = players
          .Where(p => p.PrimaryPosition != Position.Pitcher)
          .Take(9)
          .Select((p, i) => new LineupSlot
            {
              PlayerId = p.Id!.Value,
              Position = (Position)(i + 2)
            })
      };
    }

    private PitcherRole GetPitcherRoleForIndex(int index)
    {
      switch(index)
      {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
          return PitcherRole.Starter;
        case 6:
          return PitcherRole.SwingMan;
        case 7:
          return PitcherRole.LongReliever;
        case 8:
        case 9:
        case 10:
          return PitcherRole.MiddleReliever;
        case 11:
          return PitcherRole.SetupMan;
        case 12:
          return PitcherRole.Closer;
        default: 
          return PitcherRole.MopUpMan;
      }
    }

    public Team CreateCustomCopyOfTeam(Team team)
    {
      return new Team
      {
        SourceType = EntitySourceType.Custom,
        Name = team.Name,
        PlayerDefinitions = team.PlayerDefinitions,
        NoDHLineup = team.NoDHLineup,
        DHLineup = team.DHLineup,
      };
    }

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
          PitcherRole = playerToAdd.PitcherRole,
        };
        team.PlayerDefinitions = team.PlayerDefinitions.Append(newRoleDefinition);
      }

      foreach(var (updatedParams, currentDef) in diffResult.Matches) {
        currentDef.IsAAA = parameters.AAAPlayers.Any(p => updatedParams.PlayerId == p.PlayerId);
        currentDef.IsPinchHitter = updatedParams.IsPinchHitter;
        currentDef.IsPinchRunner = updatedParams.IsPinchRunner;
        currentDef.IsDefensiveReplacement = updatedParams.IsDefensiveReplacement;
        currentDef.IsDefensiveLiability = updatedParams.IsDefensiveLiability;
        currentDef.PitcherRole = updatedParams.PitcherRole;
      }

      foreach(var playerToRemove in diffResult.BNotInA)
        team.PlayerDefinitions = team.PlayerDefinitions.Where(d => d.PlayerId != playerToRemove.PlayerId);

      team.PlayerDefinitions = team.PlayerDefinitions
        .OrderBy(p => p.IsAAA)
        .OrderBy(p => p.PitcherRole)
        .ThenBy(p => allPlayers.Single(m => m.PlayerId == p.PlayerId).OrderInPitcherRole);

      var lineupWithoutPitcher = parameters.MLBPlayers
        .Where(p => p.OrderInNoDHLineup.HasValue)
        .OrderBy(p => p.OrderInNoDHLineup)
        .ToArray();

      var noDhLineup = new LineupSlot[9];
      for(var i = 0; i < 9; i++)
      {
        var player = parameters.MLBPlayers.SingleOrDefault(p => p.OrderInNoDHLineup == i + 1);
        if (player != null)
          noDhLineup[i] = new LineupSlot { PlayerId = player.PlayerId, Position = player.PositionInNoDHLineup!.Value };
        else
          noDhLineup[i] = new LineupSlot { Position = Position.Pitcher };
      }

      team.NoDHLineup = noDhLineup;

      team.DHLineup = parameters.MLBPlayers
        .Where(p => p.OrderInDHLineup.HasValue)
        .OrderBy(p => p.OrderInDHLineup)
        .Select(p => new LineupSlot { PlayerId = p.PlayerId, Position = p.PositionInDHLineup!.Value });
    }
  }
}
