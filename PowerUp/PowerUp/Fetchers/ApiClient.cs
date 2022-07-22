using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PowerUp.Fetchers
{
  public class ApiClient
  {
    private readonly HttpClient _client = new HttpClient();

    public async Task<string> GetContent(string url)
    {
      var response = await _client.GetAsync(url);
      if(response.StatusCode != System.Net.HttpStatusCode.OK)
        throw new Exception(response.StatusCode.ToString());

      return await response.Content.ReadAsStringAsync();
    }

    public async Task<T> Get<T>(string url)
    {
      var content = await GetContent(url);
      return JsonSerializer.Deserialize<T>(content)!;
    }
  }
}
