using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.GameSave;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api
{
  public class LoadBaseGameSaveCommand : ICommand<LoadBaseRequest, LoadBaseResponse>
  {
    private const string GAME_SAVE_PATH = "C:/dev/PowerUp/SaveFileAnalysis/pm2maus_after.dat";

    private readonly ICharacterLibrary _characterLibrary;
    private readonly IPlayerDatabase _playerDatabase;

    public LoadBaseGameSaveCommand(ICharacterLibrary characterLibrary, IPlayerDatabase playerDatabase)
    {
      _characterLibrary = characterLibrary;
      _playerDatabase = playerDatabase;
    }

    public LoadBaseResponse Execute(LoadBaseRequest request)
    {
      var reader = new PlayerReader(_characterLibrary, GAME_SAVE_PATH);
      for(int i=1; i<971; i++)
      {
        var gsPlayer = reader.Read(i);
        var player = new Player
        {
          Type = PlayerType.Base,
          FirstName = gsPlayer.FirstName!,
          LastName = gsPlayer.LastName!,
          SavedName = gsPlayer.SavedName!,
        };
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
