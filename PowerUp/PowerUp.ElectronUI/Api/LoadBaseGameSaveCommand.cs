using PowerUp.Databases;
using PowerUp.Entities.Players;
using PowerUp.GameSave;
using PowerUp.Libraries;
using PowerUp.Mappers;

namespace PowerUp.ElectronUI.Api
{
  public class LoadBaseGameSaveCommand : ICommand<LoadBaseRequest, LoadBaseResponse>
  {
    private readonly ICharacterLibrary _characterLibrary;
    private readonly IPlayerDatabase _playerDatabase;
    private readonly IBaseGameSavePathProvider _baseGameSavePathProvider;

    public LoadBaseGameSaveCommand(
      ICharacterLibrary characterLibrary,
      IPlayerDatabase playerDatabase,
      IBaseGameSavePathProvider gameSavePathProvider
    )
    {
      _characterLibrary = characterLibrary;
      _playerDatabase = playerDatabase;
      _baseGameSavePathProvider = gameSavePathProvider;
    }

    public LoadBaseResponse Execute(LoadBaseRequest request)
    {
      var reader = new PlayerReader(_characterLibrary, _baseGameSavePathProvider.GetPath());
      for(int i=1; i<971; i++)
      {
        var mappingParameters = new PlayerMappingParameters { PlayerType = PlayerType.Base };
        var player = reader.Read(i).MapToPlayer(mappingParameters);
        Console.WriteLine($"Saving: {player.SavedName}");
        _playerDatabase.Save(player);
      }

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
