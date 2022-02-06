using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PowerUp.ElectronUI.Shared
{
  public class ScreenBootstrappingResult : ContentResult
  {
    public ScreenBootstrappingResult(string fileName, object? indexResponse = null)
    {
      string dataDiv = indexResponse == null
        ? ""
        : $@"
            <div id=""index-response-json-data"" data=""{JsonConvert.SerializeObject(indexResponse).Replace("\"", "'")}"" />
          ";

      Content = File.ReadAllText(fileName) + dataDiv;
      ContentType = "text/html";
    }
  }
}
