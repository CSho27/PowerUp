using PowerUp.GameSave.Api;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api
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
      var parameters = new RosterImportParameters
      {
        FilePath = _baseGameSavePathProvider.GetPath(),
        IsBase = true
      };
      var result = _rosterImportApi.ImportRoster(parameters);
      return new LoadBaseResponse { Success = true };
    }
  }

  public class LoadBaseRequest
  {
    public int Throwaway { get; set; }
  }

  public class LoadBaseResponse
  {
    public bool Success { get; set; }
  }
}
