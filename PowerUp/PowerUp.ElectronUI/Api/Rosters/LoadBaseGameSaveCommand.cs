using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.GameSave.Api;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class LoadBaseGameSaveCommand : ICommand<LoadBaseRequest, LoadBaseResponse>
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

    public LoadBaseResponse Execute(LoadBaseRequest request)
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
        baseRoster = _rosterImportApi.ImportRoster(parameters).Roster;
      }

      return new LoadBaseResponse
      {
        RosterId = baseRoster!.Id!.Value
      };
    }
  }

  public class LoadBaseRequest { }

  public class LoadBaseResponse 
  { 
    public int RosterId { get; set; }
  }
}
