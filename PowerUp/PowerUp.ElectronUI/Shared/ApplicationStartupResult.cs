using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PowerUp.ElectronUI.Shared
{
  public class ApplicationStartupResult : ContentResult
  {
    private static string INDEX_PAGE_PATH = ProjectPath.Relative("electron/dist/index.html");

    public ApplicationStartupResult(string commandUrl, object? indexResponse)
    {
      var startupData = new ApplicationStartupData(commandUrl, indexResponse);
      var dataDiv = $@"
            <div id=""index-response-json-data"" data=""{JsonConvert.SerializeObject(startupData).Replace("\"", "'")}"" />
          ";

      Content = File.ReadAllText(INDEX_PAGE_PATH) + dataDiv;
      ContentType = "text/html";
    }
  }

  public class ApplicationStartupData
  {
    public string CommandUrl { get; }
    public object? IndexResponse { get; }

    public ApplicationStartupData(string commandUrl, object? indexResponse)
    {
      CommandUrl = commandUrl;
      IndexResponse = indexResponse;
    }
  }
}
