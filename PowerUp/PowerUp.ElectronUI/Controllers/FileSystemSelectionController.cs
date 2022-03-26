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

      var options = new OpenDialogOptions
      {
        Properties = new[]
          {
            request.SelectionType == FileSystemSelectionType.File
              ? OpenDialogProperty.openFile
              : OpenDialogProperty.openDirectory
          },
        Filters = request.FileFilter != null
          ? request.FileFilter.ToFileFilters()
          : null
      };

      var result = await Electron.Dialog.ShowOpenDialogAsync(mainWindow, options);
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
    public FileFilterRequest? FileFilter { get; set; }
  }

  public class FileFilterRequest
  {
    public string? Name { get; set; }
    public IEnumerable<string>? AllowedExtensions { get; set; }

    public FileFilter[] ToFileFilters()
    {
      return new[]
      {
        new FileFilter
        {
          Name = Name!,
          Extensions = AllowedExtensions!.ToArray()
        }
      };
    }
  }

  public class FileSystemSelectionResponse
  {
    public string? Path { get; set; }
  }
}
