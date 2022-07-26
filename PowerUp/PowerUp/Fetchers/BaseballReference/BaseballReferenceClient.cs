using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.BaseballReference
{
  public interface IBaseballReferenceClient
  {
    Task<string?> GetBaseballReferenceIdFor(string firstName, string lastName, int debutYear);
  }

  public class BaseballReferenceClient : IBaseballReferenceClient
  {
    private IDictionary<string, PlayerInfo>? _cachedDictionary;
    private readonly ApiClient _client = new ApiClient();

    async public Task<string?> GetBaseballReferenceIdFor(string firstName, string lastName, int debutYear)
    {
      if(_cachedDictionary == null)
      {
        var response = await _client.GetContent("https://www.baseball-reference.com/short/inc/players_search_list.csv");
        _cachedDictionary = response
          .Split('\n')
          .Select(l => l.Split(','))
          .Where(l => l.Length > 1)
          .ToDictionary(
            l => l[0], 
            l => new PlayerInfo (
              playerId: l[0],
              informalDisplayName: l[1],
              debutYear: int.Parse(l[2].Split('-')[0])
            )
          );
      }

      var currentNumber = 1;
      var possiblePlayers = new List<PlayerInfo>();
      while(_cachedDictionary.TryGetValue(GetBaseballReferenceId(firstName, lastName, currentNumber), out var info))
      {
        possiblePlayers.Add(info);
        currentNumber++;
      }

      if(possiblePlayers.Count == 0)
      {
        var first5LettersOfLastName = new string(lastName.ToLower().Take(5).ToArray());
        possiblePlayers = possiblePlayers.Concat(_cachedDictionary.Where(kvp => kvp.Key.Contains(first5LettersOfLastName)).Select(kvp => kvp.Value)).ToList();        
      }

      return possiblePlayers.SingleOrDefault(p => p.InformalDisplayName == $"{firstName} {lastName}" && p.DebutYear == debutYear)?.PlayerId;
    }

    private string GetBaseballReferenceId(string firstName, string lastName, int number)
    {
      var lowercasedFirstName = firstName.ToLower();
      var lowercaedLastName = lastName.ToLower();
      var first2LettersOfFirstName = new string(lowercasedFirstName.Take(2).ToArray());
      var first5LettersOfLastName = new string(lowercaedLastName.Take(5).ToArray());
      var paddedNumber = number >= 10
        ? number.ToString()
        : $"0{number}";

      return $"{first5LettersOfLastName}{first2LettersOfFirstName}{paddedNumber}";
    }

    private class PlayerInfo
    {
      public string PlayerId { get; set; }
      public string InformalDisplayName { get; set; }
      public int DebutYear { get; set; }

      public PlayerInfo(string playerId, string informalDisplayName, int debutYear)
      {
        PlayerId = playerId;
        InformalDisplayName = informalDisplayName;
        DebutYear = debutYear;
      }
    }
  }

  
}
