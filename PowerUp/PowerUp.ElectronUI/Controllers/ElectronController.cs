using Microsoft.AspNetCore.Mvc;
using PowerUp.ElectronUI.Shared;
using PowerUp.GameSave.Api;

namespace PowerUp.ElectronUI.Controllers
{
  public class ElectronController : Controller
  {
    private const string COMMAND_URL = "/command";

    private readonly CommandRegistry _commandRegistry;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IBaseRosterInitializer _baseRosterInitializer; 

    public ElectronController(
      CommandRegistry commandRegistry, 
      IWebHostEnvironment webHostEnvironment,
      IBaseRosterInitializer baseRosterInitializer
    )
    {
      _commandRegistry = commandRegistry;
      _webHostEnvironment = webHostEnvironment;
      _baseRosterInitializer = baseRosterInitializer;
    }

    public IActionResult Index()
    {
      _baseRosterInitializer.Initialize();
      return new ApplicationStartupResult(
        _webHostEnvironment, 
        COMMAND_URL, 
        null
      );
    }

    [Route(COMMAND_URL), HttpPost]
    public JsonResult ExecuteCommand([FromBody]CommandRequest request)
    {
      return new JsonResult(_commandRegistry.ExecuteCommand(request));
    }
  }
}
