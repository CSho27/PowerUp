using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerUp.Fetchers
{
  public static class UrlBuilder
  {
    public static string Build(string url, IDictionary<string, string> parameters)
    {
      return parameters.Any()
        ? $"{url}?{string.Join("&", parameters.Select(kvp => $"{kvp.Key}={HttpUtility.UrlEncode(kvp.Value)}"))}"
        : url;
    }

    public static string Build(IEnumerable<string> urlParts, IDictionary<string, string> parameters)
      => Build(string.Join("/", urlParts), parameters);

    public static string Build(string url, object? parameters = null)
    {
      return parameters != null
        ? Build(url, ToDictionary(parameters))
        : Build(url, new Dictionary<string, string>());
    }

    public static string Build(IEnumerable<string> urlParts, object? parameters = null)
      => Build(string.Join("/", urlParts), parameters);

    private static IDictionary<string, string> ToDictionary(object parameterObject)
    {
      var props = parameterObject.GetType().GetProperties();
      return props
        .Where(x => x.GetValue(parameterObject) != null)
        .ToDictionary(x => x.Name, x => x.GetValue(parameterObject)?.ToString())!;
    }
  }
}
