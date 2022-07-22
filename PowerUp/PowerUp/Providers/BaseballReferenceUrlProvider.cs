using PowerUp.Fetchers.BaseballReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PowerUp.Providers
{
  public interface IBaseballReferenceUrlProvider
  {
    public string GetPlayerPageForUrl(string fullFirstName, string fullLastName, DateTime proDebutDate);
    public string GetSearchPageForUrl(string firstName, string lastName);
  }

  public class BaseballReferenceUrlProvider : IBaseballReferenceUrlProvider
  {
    private readonly IBaseballReferenceClient _client;

    public BaseballReferenceUrlProvider(IBaseballReferenceClient client)
    {
      _client = client;
    }

    public string GetPlayerPageForUrl(string fullFirstName, string fullLastName, DateTime proDebutDate)
    {
      var baseballReferenceId = Task.Run(() => _client.GetBaseballReferenceIdFor(
        fullFirstName,
        fullLastName,
        proDebutDate.Year
      )).GetAwaiter().GetResult();

      var firstLetterOfLastName = fullLastName.ToLower().FirstCharacter();
      return baseballReferenceId != null
        ? $"https://www.baseball-reference.com/players/{firstLetterOfLastName}/{baseballReferenceId}.shtml"
        : GetSearchPageForUrl(fullFirstName, fullLastName);
    }

    public string GetSearchPageForUrl(string firstName, string lastName)
    {
      return $"https://www.baseball-reference.com/search/search.fcgi?hint={firstName}+{lastName}&search={firstName}+{lastName}";
    }
  }
}
