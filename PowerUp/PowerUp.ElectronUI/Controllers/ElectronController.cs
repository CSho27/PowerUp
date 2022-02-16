using Microsoft.AspNetCore.Mvc;
using PowerUp.ElectronUI.Api;
using PowerUp.ElectronUI.Shared;
using PowerUp.GameSave;

namespace PowerUp.ElectronUI.Controllers
{
  public class ElectronController : Controller
  {
    private const string COMMAND_URL = "/command";

    private const string GAME_SAVE_PATH = "C:/dev/PowerUp/SaveFileAnalysis/pm2maus_after.dat";
    private const int PLAYER_ID = 20;

    private readonly CommandRegistry _commandRegistry;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ElectronController(CommandRegistry commandRegistry, IWebHostEnvironment webHostEnvironment)
    {
      _commandRegistry = commandRegistry;
      _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
      using var reader = new PlayerReader(GAME_SAVE_PATH);
      var player = reader.Read(PLAYER_ID);
      return new ApplicationStartupResult(_webHostEnvironment, COMMAND_URL, PlayerEditorDTO.FromGSPlayer(player));
    }

    [Route(COMMAND_URL), HttpPost]
    public JsonResult ExecuteCommand([FromBody]CommandRequest request)
    {
      return new JsonResult(_commandRegistry.ExecuteCommand(request));
    }
  }
}
