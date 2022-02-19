using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class SavePlayerCommand : ICommand<PlayerEditorDTO, object>
  {
    private const string GAME_SAVE_PATH = "C:/dev/PowerUp/SaveFileAnalysis/pm2maus_after.dat";
    
    private readonly ICharacterLibrary _characterLibrary;

    public SavePlayerCommand(ICharacterLibrary characterLibrary)
    {
      _characterLibrary = characterLibrary;
    }

    public object Execute(PlayerEditorDTO request)
    {
      using var writer = new PlayerWriter(_characterLibrary, GAME_SAVE_PATH);
      writer.Write(request.PowerProsId!.Value, request.ToGSPlayer());
      return new { Result = "Great Success!" };
    }
  }
}
