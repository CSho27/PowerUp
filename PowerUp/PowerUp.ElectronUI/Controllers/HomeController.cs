using Microsoft.AspNetCore.Mvc;
using PowerUp.ElectronUI.Shared;
using PowerUp.GameSave;

namespace PowerUp.ElectronUI.Controllers
{
  public class HomeController : Controller
  {
    private const string GAME_SAVE_PATH = "C:/Users/short/OneDrive/Documents/Dolphin Emulator/Wii/title/00010000/524d5045/data/pm2maus.dat";
    private const int PLAYER_ID = 20;

    public IActionResult Index()
    {
      return new ScreenBootstrappingResult(ProjectPath.Relative("electron/dist/index.html"), new { Result = "Great Success!" });
    }

    [Route("/Test")]
    public JsonResult Test()
    {
      using var loader = new PlayerReader(GAME_SAVE_PATH);
      var player = loader.Read(PLAYER_ID);
      return new JsonResult(new { Result = player.SavedName });
    }
  }
}
