using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Rosters.Api;
using PowerUp.Entities.Teams;
using PowerUp.Entities.Teams.Api;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class ReplaceTeamWithCopyCommand : ICommand<ReplaceTeamWithCopyRequest, ResultResponse>
  {
    private readonly ITeamApi _teamApi;
    private readonly IRosterApi _rosterApi;

    public ReplaceTeamWithCopyCommand(
      ITeamApi teamApi,
      IRosterApi rosterApi
    )
    {
      _teamApi = teamApi;
      _rosterApi = rosterApi;
    }

    public Task<ResultResponse> Execute(ReplaceTeamWithCopyRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var roster = DatabaseConfig.Database.Load<Roster>(request.RosterId)!;
      var teamToCopy = DatabaseConfig.Database.Load<Team>(roster.TeamIdsByPPTeam[request.MLBPPTeam])!;
      var teamToInsert = _teamApi.CreateCustomCopyOfTeam(teamToCopy);

      DatabaseConfig.Database.Save(teamToInsert);
      _rosterApi.ReplaceTeam(roster, request.MLBPPTeam, teamToInsert);

      DatabaseConfig.Database.Save(roster);
      tx.Commit();

      return Task.FromResult(ResultResponse.Succeeded());
    }
  }

  public class ReplaceTeamWithCopyRequest
  {
    public int RosterId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MLBPPTeam MLBPPTeam { get; set; }
  }
}
