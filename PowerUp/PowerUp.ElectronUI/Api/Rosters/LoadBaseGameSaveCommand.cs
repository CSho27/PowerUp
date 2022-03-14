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
        .LoadAll()
        .SingleOrDefault(r => r.SourceType == EntitySourceType.Base);

      if(baseRoster == null)
      {
        using var stream = new FileStream(_baseGameSavePathProvider.GetPath(), FileMode.Open, FileAccess.Read);
        var parameters = new RosterImportParameters
        {
          Stream = stream,
          IsBase = true
        };
        var result = _rosterImportApi.ImportRoster(parameters);
        var rosterDetails = RosterDetails.FromRosterTeamsAndPlayers(result.Roster!, result.Teams, result.Players);
        return new RosterEditorResponse(rosterDetails);
      }
      else
      {
        var rosterDetails = RosterDetails.FromRoster(baseRoster);
        return new RosterEditorResponse(rosterDetails);
      }
    }
  }

  public class LoadBaseRequest { }
}
