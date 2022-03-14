using Microsoft.AspNetCore.Mvc;
using PowerUp.ElectronUI.Api.Rosters;
using PowerUp.GameSave.Api;

namespace PowerUp.ElectronUI.Controllers
{
  public class GameSaveImportController : Controller
  {
    public const string GameSaveImportUrl = "/import";

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
        ImportSource = formData["importSource"]
      };
      var result = _rosterImportApi.ImportRoster(parameters);
      var rosterDetails = RosterDetails.FromRosterTeamsAndPlayers(result.Roster!, result.Teams, result.Players);
      return new JsonResult(new RosterEditorResponse(rosterDetails));
    }
  }
}
