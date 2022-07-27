using Microsoft.AspNetCore.Mvc;
using PowerUp.GameSave.Api;
using PowerUp.GameSave.Objects.GameSaves;

namespace PowerUp.ElectronUI.Controllers
{
  public class GameSaveImportController : Controller
  {
    private const string GameSaveImportUrl = "/import";

    private readonly IRosterImportApi _rosterImportApi;

    public GameSaveImportController(IRosterImportApi rosterImportApi)
    {
      _rosterImportApi = rosterImportApi;
    }

    [Route(GameSaveImportUrl), HttpPost]
    public ActionResult Import(IFormCollection formData)
    {
      var parameters = new RosterImportParameters 
      { 
        Stream = formData.Files[0].OpenReadStream(),
        ImportSource = formData["importSource"],
        GameSaveFormat = Enum.Parse<GameSaveFormat>(formData["gameSaveFormat"])
      };
      var result = _rosterImportApi.ImportRoster(parameters);
      return new JsonResult(new { RosterId = result.Roster!.Id!.Value });
    }
  }
}
