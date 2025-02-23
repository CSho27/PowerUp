using Microsoft.AspNetCore.Mvc;
using PowerUp.ElectronUI.Shared;

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
      return new ApplicationStartupResult(
        _webHostEnvironment, 
        COMMAND_URL, 
        null
      );
    }

    [Route(COMMAND_URL), HttpPost]
    public JsonResult ExecuteCommand([FromForm]CommandRequest commandRequest)
    {
      return new JsonResult(_commandRegistry.ExecuteCommand(commandRequest));
    }
  }
}
