using Microsoft.AspNetCore.Mvc;
using PowerUp.Databases;
using PowerUp.ElectronUI.Api;
using PowerUp.ElectronUI.Shared;
using PowerUp.Entities;
using PowerUp.GameSave;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Controllers
{
  public class ElectronController : Controller
  {
    private const string COMMAND_URL = "/command";

    private const string GAME_SAVE_PATH = "C:/dev/PowerUp/SaveFileAnalysis/pm2maus_after.dat";
    private const int PLAYER_ID = 20;

    private readonly CommandRegistry _commandRegistry;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IPlayerDatabase _playerDatabase;
    private readonly ICharacterLibrary _characterLibrary;

    public ElectronController(
      CommandRegistry commandRegistry, 
      IWebHostEnvironment webHostEnvironment,
      IPlayerDatabase playerDatabase,
      ICharacterLibrary characterLibrary
    )
    {
      _commandRegistry = commandRegistry;
      _webHostEnvironment = webHostEnvironment;
      _playerDatabase = playerDatabase;
      _characterLibrary = characterLibrary;
    }

    public IActionResult Index()
    {
      var testplayer = new Player()
      {
        FirstName = "Chris",
        LastName = "Shorter",
        SavedName = "Shorter",
        Type = PlayerType.Custom
      };

      _playerDatabase.Save(testplayer);

      var databaseKeys = ((IHaveDatabaseKeys<PlayerDatabaseKeys>)testplayer).DatabaseKeys;
      var loadedTestPlayer = _playerDatabase.Load(databaseKeys);
      Console.WriteLine($"Loaded Player: {loadedTestPlayer.SavedName}");

      using var reader = new PlayerReader(_characterLibrary, GAME_SAVE_PATH);
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
