using Microsoft.AspNetCore.Mvc;

namespace PowerUp.ElectronUI.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
