using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PowerUp.ElectronUI.Shared
{
  public class ApplicationStartupResult : ContentResult
  {
    public ApplicationStartupResult(
      IWebHostEnvironment webHostEnvironment, 
      string commandUrl, 
      string rosterImportUrl,
      object? indexResponse)
    {
      var startupData = new ApplicationStartupData(commandUrl, rosterImportUrl, indexResponse);
      var dataDiv = $@"
        <div id=""index-response-json-data"" data=""{JsonConvert.SerializeObject(startupData).Replace("\"", "'")}"" />
      ";
      var indexPagePath = Path.Combine(webHostEnvironment.WebRootPath, "index.html");

      Content = File.ReadAllText(indexPagePath) + dataDiv;
      ContentType = "text/html";
    }
  }

  public class ApplicationStartupData
  {
    public string CommandUrl { get; }
    public string RosterImportUrl { get; }
    public object? IndexResponse { get; }

    public ApplicationStartupData(string commandUrl, string rosterImportUrl, object? indexResponse)
    {
      CommandUrl = commandUrl;
      RosterImportUrl = rosterImportUrl;
      IndexResponse = indexResponse;
    }
  }
}
