using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Rosters.Api;
using PowerUp.Entities.Teams;
using PowerUp.Entities.Teams.Api;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class ReplaceTeamWithNewTeamCommand : ICommand<ReplaceTeamWithNewTeamRequest, ResultResponse>
  {
    private readonly ITeamApi _teamApi;
    private readonly IRosterApi _rosterApi;

    public ReplaceTeamWithNewTeamCommand(
      ITeamApi teamApi,
      IRosterApi rosterApi
    )
    {
      _teamApi = teamApi;
      _rosterApi = rosterApi;
    }

    public ResultResponse Execute(ReplaceTeamWithNewTeamRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var roster = DatabaseConfig.Database.Load<Roster>(request.RosterId)!;
      var teamToCopy = DatabaseConfig.Database.Load<Team>(roster.TeamIdsByPPTeam[request.MLBPPTeam])!;
      var teamToInsert = _teamApi.CreateDefaultTeam(EntitySourceType.Custom, player => DatabaseConfig.Database.Save(player));

      DatabaseConfig.Database.Save(teamToInsert);
      _rosterApi.ReplaceTeam(roster, request.MLBPPTeam, teamToInsert);

      DatabaseConfig.Database.Save(roster);
      tx.Commit();

      return ResultResponse.Succeeded();
    }
  }

  public class ReplaceTeamWithNewTeamRequest
  {
    public int RosterId { get; set; }
    public MLBPPTeam MLBPPTeam { get; set; }
  }
}
