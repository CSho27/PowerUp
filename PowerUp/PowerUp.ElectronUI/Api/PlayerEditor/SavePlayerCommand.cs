using PowerUp.ElectronUI.Controllers;
using PowerUp.GameSave;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class SavePlayerCommand : ICommand<PlayerEditorDTO, object>
  {
    private const string GAME_SAVE_PATH = "C:/dev/PowerUp/SaveFileAnalysis/pm2maus_after.dat";

    public object Execute(PlayerEditorDTO request)
    {
      using var writer = new PlayerWriter(GAME_SAVE_PATH);
      writer.Write(request.PowerProsId!.Value, request.ToGSPlayer());
      return new { Result = "Great Success!" };
    }
  }
}
