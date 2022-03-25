using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Controllers
{
  public class FileSystemSelectionController : Controller
  {
    private const string FileSystemSelectionUrl = "/file-system-selection";

    [Route(FileSystemSelectionUrl), HttpPost]
    public async Task<FileSystemSelectionResponse> SelectDirectory([FromBody] FileSystemSelectionRequest request)
    {
      var mainWindow = Electron.WindowManager.BrowserWindows.First();
      var properties = new[]
      {
        request.SelectionType == FileSystemSelectionType.File
          ? OpenDialogProperty.openFile
          : OpenDialogProperty.openDirectory
      };
      var result = await Electron.Dialog.ShowOpenDialogAsync(mainWindow, new OpenDialogOptions { Properties = properties });
      return new FileSystemSelectionResponse { Path = result.SingleOrDefault() };
    }
  }

  public enum FileSystemSelectionType
  {
    File,
    Directory,
  }

  public class FileSystemSelectionRequest
  {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FileSystemSelectionType SelectionType { get; set; }
  }

  public class FileSystemSelectionResponse
  {
    public string? Path { get; set; }
  }
}
