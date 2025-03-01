using PowerUp.Databases;
using PowerUp.Entities.AppSettings;
using PowerUp.GameSave.GameSaveManagement;

namespace PowerUp.ElectronUI.Api.GameSaveManagement
{
  public class OpenGameSaveManagerCommand : ICommand<OpenGameSaveManagerRequest, OpenGameSaveManagerResponse>
  {
    private readonly IGameSaveManager _gameSaveManager;
    
    public OpenGameSaveManagerCommand(IGameSaveManager gameSaveManager)
    {
      _gameSaveManager = gameSaveManager;
    }

    public Task<OpenGameSaveManagerResponse> Execute(OpenGameSaveManagerRequest request)
    {
      var settings = DatabaseConfig.Database.LoadOnly<AppSettings>();
      if (settings == null || settings.GameSaveManagerDirectoryPath == null)
        throw new InvalidOperationException("Game Save Manager has not been initlialized");

      var currentState = _gameSaveManager.GetCurrentState(settings.GameSaveManagerDirectoryPath!);
      return Task.FromResult(new OpenGameSaveManagerResponse(currentState));
    }
  }

  public class OpenGameSaveManagerRequest { }

  public class OpenGameSaveManagerResponse
  {
    public int? ActiveGameSaveId { get; private set; }
    public IEnumerable<GameSaveDto>? GameSaveOptions { get; private set; }

    public OpenGameSaveManagerResponse(GameSaveManagerState state)
    {
      ActiveGameSaveId = state.ActiveGameSaveId;
      GameSaveOptions = state.GameSaveOptions.Select(o => new GameSaveDto(o));
    }
  }

  public class GameSaveDto
  {
    public int Id { get; }
    public string Name { get; }

    public GameSaveDto(GameSaveOption gameSave)
    {
      Id = gameSave.GameSaveId;
      Name = gameSave.Name;
    }
  }
}
