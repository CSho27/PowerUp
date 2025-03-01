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
  public class ReplaceWithDraftedTeamsCommand : ICommand<ReplaceWithDraftedTeamsRequest, ResultResponse>
  {
    private readonly IRosterApi _rosterApi;
    private readonly ITeamApi _teamApi;

    public ReplaceWithDraftedTeamsCommand(IRosterApi rosterApi, ITeamApi teamApi)
    {
      _rosterApi = rosterApi;
      _teamApi = teamApi;
    }

    public Task<ResultResponse> Execute(ReplaceWithDraftedTeamsRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();
      var roster = DatabaseConfig.Database.Load<Roster>(request.RosterId)!;
      var teams = new List<Team>();
      foreach(var team in request.Teams)
      {
        var createdTeam = _teamApi.CreateFromPlayers(team.PlayerIds, team.TeamName);
        DatabaseConfig.Database.Save(createdTeam);
        _rosterApi.ReplaceTeam(roster, team.TeamToReplace, createdTeam);
      }
      DatabaseConfig.Database.Save(roster);
      tx.Commit();

      return Task.FromResult(ResultResponse.Succeeded());
    }
  }

  public class ReplaceWithDraftedTeamsRequest
  {
    public int RosterId { get; set; }
    public IEnumerable<DraftedTeam> Teams { get; set; }
  }

  public class DraftedTeam
  {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MLBPPTeam TeamToReplace { get; set; }
    public string TeamName { get; set; }
    public IEnumerable<int> PlayerIds { get; set; }
  }
}
