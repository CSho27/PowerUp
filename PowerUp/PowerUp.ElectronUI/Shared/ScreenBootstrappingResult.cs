using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PowerUp.ElectronUI.Shared
{
  public class ScreenBootstrappingResult : ContentResult
  {
    public ScreenBootstrappingResult(string fileName, object? indexResponse = null)
    {
      DefaultContractResolver contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
      var serializerSettings = new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml, ContractResolver = contractResolver };
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
