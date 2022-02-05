using Microsoft.AspNetCore.Mvc;

namespace PowerUp.ElectronUI.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return base.PhysicalFile(ProjectPath.Relative("electron/dist/index.html"), "text/html");
    }

    [Route("/Test")]
    public JsonResult Test()
    {
      return new JsonResult(new { Result = "Great Success!" });
    }
  }

  public class ScreenBootstrappingResult : ViewResult
  {

  }
}
