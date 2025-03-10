﻿using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.Entities.Teams.Api;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class SaveTeamCommand : ICommand<SaveTeamRequest, ResultResponse>
  {
    private readonly ITeamApi _teamApi;

    public SaveTeamCommand(ITeamApi teamApi)
    {
      _teamApi = teamApi;
    }

    public Task<ResultResponse> Execute(SaveTeamRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var tempTeam = DatabaseConfig.Database.Load<TempTeam>(request.TempTeamId)!;
      if (tempTeam.Team!.Id != request.TeamId)
        throw new InvalidOperationException("Mismatching TeamId and TempTeamId");

      _teamApi.EditTeam(tempTeam.Team!, request.GetParameters());
      if (request.Persist)
        tempTeam.LastSaved = DateTime.Now;

      DatabaseConfig.Database.Save(tempTeam);

      if (request.Persist)
        DatabaseConfig.Database.Save(tempTeam.Team);

      tx.Commit();
      return Task.FromResult(ResultResponse.Succeeded());      
    }
  }

  public class SaveTeamRequest
  {
    public int TeamId { get; set; }
    public int TempTeamId { get; set; }
    public bool Persist { get; set; }

    public string? Name { get; set; }
    public IEnumerable<PlayerRoleRequest>? MLBPlayers { get; set; }
    public IEnumerable<PlayerRoleRequest>? AAAPlayers { get; set; }

    public TeamParameters GetParameters()
    {
      return new TeamParameters
      {
        Name = Name,
        MLBPlayers = MLBPlayers!.Select(p => p.GetParameters()),
        AAAPlayers = AAAPlayers!.Select(p => p.GetParameters())
      };
    }
  }

  public class PlayerRoleRequest
  {
    public int PlayerId { get; set; }
    public bool IsPinchHiiter { get; set; }
    public bool IsPinchRunner { get; set; }
    public bool IsDefensiveReplacement { get; set; }
    public bool IsDefensiveLiability { get; set; }
    public PitcherRole PitcherRole { get; set; }
    public int OrderInPitcherRole { get; set; }
    public int? OrderInNoDHLineup { get; set; }
    public Position? PositionInNoDHLineup { get; set; }
    public int? OrderInDHLineup { get; set; }
    public Position? PositionInDHLineup { get; set; }

    public PlayerRoleParameters GetParameters()
    {
      return new PlayerRoleParameters
      {
        PlayerId = PlayerId,
        IsPinchHitter = IsPinchHiiter,
        IsPinchRunner = IsPinchRunner,
        IsDefensiveReplacement = IsDefensiveReplacement,
        IsDefensiveLiability = IsDefensiveLiability,
        PitcherRole = PitcherRole,
        OrderInPitcherRole = OrderInPitcherRole,
        OrderInNoDHLineup = OrderInNoDHLineup,
        PositionInNoDHLineup = PositionInNoDHLineup,
        OrderInDHLineup = OrderInDHLineup,
        PositionInDHLineup = PositionInDHLineup
      };
    }
  }
}
