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

    private readonly CommandRegistry _commandRegistry;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ElectronController(
      CommandRegistry commandRegistry, 
      IWebHostEnvironment webHostEnvironment
    )
    {
      _commandRegistry = commandRegistry;
      _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
      return new ApplicationStartupResult(_webHostEnvironment, COMMAND_URL, null);
    }

    [Route(COMMAND_URL), HttpPost]
    public JsonResult ExecuteCommand([FromBody]CommandRequest request)
    {
      return new JsonResult(_commandRegistry.ExecuteCommand(request));
    }
  }
}
