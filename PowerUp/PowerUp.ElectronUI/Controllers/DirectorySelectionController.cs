using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PowerUp.ElectronUI.Controllers
{
  public class DirectorySelectionController : Controller
  {
    private const string DirectorySelectionUrl = "/select-directory";

    [Route(DirectorySelectionUrl), HttpGet]
    public async Task<ActionResult> SelectDirectory()
    {
      var mainWindow = Electron.WindowManager.BrowserWindows.First();
      var result = await Electron.Dialog.ShowOpenDialogAsync(mainWindow, new OpenDialogOptions { Properties = new[] { OpenDialogProperty.openDirectory } });
      return new JsonResult(new { DirectoryPath = result.Single() });
    }
  }
}
