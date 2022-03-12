using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.GameSave.Api;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class LoadBaseGameSaveCommand : ICommand<LoadBaseRequest, RosterEditorResponse>
  {
    private readonly IBaseGameSavePathProvider _baseGameSavePathProvider;
    private readonly IRosterImportApi _rosterImportApi;

    public LoadBaseGameSaveCommand(
      IBaseGameSavePathProvider gameSavePathProvider,
      IRosterImportApi rosterImportApi
    )
    {
      _baseGameSavePathProvider = gameSavePathProvider;
      _rosterImportApi = rosterImportApi;
    }

    public RosterEditorResponse Execute(LoadBaseRequest request)
    {
      var baseRoster = DatabaseConfig.RosterDatabase
        .LoadBy(k => k.ImportSource == EntitySourceType.Base.ToString())
        .SingleOrDefault();

      if(baseRoster == null)
      {
        var parameters = new RosterImportParameters
        {
          FilePath = _baseGameSavePathProvider.GetPath(),
          IsBase = true
        };
        var result = _rosterImportApi.ImportRoster(parameters);
        var rosterDetails = RosterDetails.FromRosterTeamsAndPlayers(result.Roster!, result.Teams, result.Players);
        return new RosterEditorResponse(rosterDetails);
      }
      else
      {
        var teams = baseRoster.GetTeams().Select(kvp => kvp.Key);
        var rosterDetails = RosterDetails.FromRosterTeamsAndPlayers(baseRoster, teams, teams.SelectMany(t => t.GetPlayers()));
        return new RosterEditorResponse(rosterDetails);
      }

    }
  }

  public class LoadBaseRequest { }
}
