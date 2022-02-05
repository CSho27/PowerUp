using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PowerUp.ElectronUI.Shared
{
  public class ScreenBootstrappingResult : ContentResult
  {
    public ScreenBootstrappingResult(string fileName, object? indexResponse = null)
    {
      var serializerSettings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
      string dataDiv = indexResponse == null
        ? ""
        : $@"
            <div id=""index-response-json-data"" data=""{JsonConvert.SerializeObject(indexResponse, serializerSettings).Replace("\"", "'")}"" />
          ";

      Content = File.ReadAllText(fileName) + dataDiv;
      ContentType = "text/html";
    }
  }
}
