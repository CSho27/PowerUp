using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Rosters.Api;
using PowerUp.Entities.Teams;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class ReplaceTeamWithExistingCommand : ICommand<ReplaceTeamWithExistingRequest, ResultResponse>
  {
    private readonly IRosterApi _rosterApi;

    public ReplaceTeamWithExistingCommand(IRosterApi rosterApi)
    {
      _rosterApi = rosterApi;
    }

    public Task<ResultResponse> Execute(ReplaceTeamWithExistingRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var teamToInsert = DatabaseConfig.Database.Load<Team>(request.TeamToInsertId)!;
      var roster = DatabaseConfig.Database.Load<Roster>(request.RosterId)!;
      _rosterApi.ReplaceTeam(roster, request.MLBPPTeamToReplace, teamToInsert);

      DatabaseConfig.Database.Save(roster);
      tx.Commit();

      return Task.FromResult(ResultResponse.Succeeded());
    }
  }

  public class ReplaceTeamWithExistingRequest
  {
    public int RosterId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MLBPPTeam MLBPPTeamToReplace { get; set; }
    public int TeamToInsertId { get; set; }
  }
}
