using PowerUp.Databases;
using PowerUp.Entities.AppSettings;

namespace PowerUp.ElectronUI.Api.GameSaveManagement
{
  public class GetGameSaveManagerDirectoryCommand : ICommand<GetCurrentGameSaveManagerDirectoryRequest, GetCurrentGameSaveManagerDirectoryResponse>
  {
    public GetCurrentGameSaveManagerDirectoryResponse Execute(GetCurrentGameSaveManagerDirectoryRequest request)
    {
      var appSettings = DatabaseConfig.Database.LoadOnly<AppSettings>();
      return new GetCurrentGameSaveManagerDirectoryResponse { GameSaveManagerDirectoryPath = appSettings?.GameSaveManagerDirectoryPath };
    }
  }

  public class GetCurrentGameSaveManagerDirectoryRequest { }

  public class GetCurrentGameSaveManagerDirectoryResponse 
  {
    public string? GameSaveManagerDirectoryPath { get; set; }
    
  }
}
