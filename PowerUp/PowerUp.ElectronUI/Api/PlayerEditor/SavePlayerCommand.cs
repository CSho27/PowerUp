using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class SavePlayerCommand : ICommand<SavePlayerRequest, object>
  {
    private const string GAME_SAVE_PATH = "C:/dev/PowerUp/SaveFileAnalysis/pm2maus_after.dat";
    
    private readonly ICharacterLibrary _characterLibrary;

    public SavePlayerCommand(ICharacterLibrary characterLibrary)
    {
      _characterLibrary = characterLibrary;
    }

    public object Execute(SavePlayerRequest request)
    {
      using var writer = new PlayerWriter(_characterLibrary, GAME_SAVE_PATH);
      return new { };
    }
  }

  public class SavePlayerRequest
  {
    public string? Key { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? SavedName { get; set; }
    public string? Position { get; set; }
    public string? PlayerNumber { get; set; }
  }
}
