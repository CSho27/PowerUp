using Microsoft.AspNetCore.Mvc;
using PowerUp.ElectronUI.Shared;
using PowerUp.GameSave;

namespace PowerUp.ElectronUI.Controllers
{
  public class HomeController : Controller
  {
    private const string GAME_SAVE_PATH = "C:/dev/PowerUp/SaveFileAnalysis/pm2maus_after.dat";
    private const int PLAYER_ID = 20;

    public IActionResult Index()
    {
      using var reader = new PlayerReader(GAME_SAVE_PATH);
      var player = reader.Read(PLAYER_ID);
      return new ScreenBootstrappingResult(ProjectPath.Relative("electron/dist/index.html"), PlayerEditorDTO.FromGSPlayer(player));
    }

    [Route("/edit-player/save"), HttpPost]
    public JsonResult SavePlayer([FromBody]PlayerEditorDTO request)
    {
      using var writer = new PlayerWriter(GAME_SAVE_PATH);
      writer.Write(request.PowerProsId!.Value, request.ToGSPlayer());
      return new JsonResult(new { Result = "Great Success!" });
    }

    [Route("/Test"), HttpGet]
    public JsonResult Test()
    {
      using var loader = new PlayerReader(GAME_SAVE_PATH);
      var player = loader.Read(PLAYER_ID);
      return new JsonResult(new { Result = player.SavedName });
    }
  }

  public class PlayerEditorDTO
  {
    public int? PowerProsId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? SavedName { get; set; }
    public string? Position { get; set; }
    public string? PlayerNumber { get; set; }
    public static PlayerEditorDTO FromGSPlayer(GSPlayer player)
    {
      return new PlayerEditorDTO
      {
        PowerProsId = (int)player.PowerProsId!,
        FirstName = player.FirstName!,
        LastName = player.LastName!,
        SavedName = player.SavedName!,
        Position = "1B",
        PlayerNumber = player.PlayerNumberDisplay!
      };
    }

    public GSPlayer ToGSPlayer()
    {
      return new GSPlayer
      {
        FirstName = FirstName,
        LastName = LastName,
        SavedName = SavedName
      };
    }
  }
}
