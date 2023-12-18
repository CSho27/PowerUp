using System;
using System.Net.Http;
using System.Net.Http.Json;
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

    public async Task<string> PostContent(string url, object request)
    {
      var requestContent = JsonContent.Create(request);
      var response = await _client.PostAsync(url, requestContent);
      if (response.StatusCode != System.Net.HttpStatusCode.OK)
        throw new Exception(response.StatusCode.ToString());

      return await response.Content.ReadAsStringAsync();
    }

    public async Task<T> Post<T>(string url, object request)
    {
      var content = await PostContent(url, request);
      return JsonSerializer.Deserialize<T>(content)!;
    }
  }
}
