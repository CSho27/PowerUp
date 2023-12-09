using System.Net.Http.Json;

namespace MLB_StatsAPI.NET
{
  internal class PyPiApiClient
  {
    public async Task<PyPiPackageInfoResponse> GetPackageInfo(string packageName)
    {
      using HttpClient httpClient = new HttpClient();
      string apiUrl = $"https://pypi.org/pypi/{packageName}/json";
      HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

      if (!response.IsSuccessStatusCode)
        throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");

      var packageInfo = await response.Content.ReadFromJsonAsync<PyPiPackageInfoResponse>();
      if(packageInfo == null)
        throw new Exception($"No package info found for {packageName}");
      return packageInfo;
    }

    public async Task<Stream> FetchPackageAsync(string packageName, string version)
    {
      var packageInfo = await GetPackageInfo(packageName);
      var releases = packageInfo.Releases[version];
      var sdist = releases.FirstOrDefault(r => r.PackageType == "bdist_wheel");
      if (sdist is null)
        throw new Exception("No source distribution found for the supplied package and version");

      using HttpClient httpClient = new HttpClient();
      return await httpClient.GetStreamAsync(sdist.Url);
    }

    public class PyPiPackageInfoResponse
    {
      public PyPiPackageInfo Info { get; set; } = new PyPiPackageInfo();
      public IDictionary<string, IEnumerable<PyPiReleaseInfo>> Releases { get; set; } = new Dictionary<string, IEnumerable<PyPiReleaseInfo>>();
    }

    public class PyPiPackageInfo
    {
      public string Version { get; set; } = string.Empty;
    }

    public class PyPiReleaseInfo
    {
      public string Url { get; set; } = string.Empty;
      public string PackageType { get; set; } = string.Empty;
    }
  }
}
