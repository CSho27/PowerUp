using PowerUp.Databases;
using PowerUp.Entities.AppSettings;

namespace PowerUp.ElectronUI.Api.GameSaveManagement
{
  public class GetGameSaveManagerDirectoryCommand : ICommand<GetCurrentGameSaveManagerDirectoryRequest, GetCurrentGameSaveManagerDirectoryResponse>
  {
    public Task<GetCurrentGameSaveManagerDirectoryResponse> Execute(GetCurrentGameSaveManagerDirectoryRequest request)
    {
      var appSettings = DatabaseConfig.Database.LoadOnly<AppSettings>();
      return Task.FromResult(new GetCurrentGameSaveManagerDirectoryResponse { GameSaveManagerDirectoryPath = appSettings?.GameSaveManagerDirectoryPath });
    }
  }

  public class GetCurrentGameSaveManagerDirectoryRequest { }

  public class GetCurrentGameSaveManagerDirectoryResponse 
  {
    public string? GameSaveManagerDirectoryPath { get; set; }
    
  }
}
