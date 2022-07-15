using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Libraries
{
  public interface ICountryAndSkinColorLibrary
  {
    int? this[string key] { get; }
    public IEnumerable<KeyValuePair<string, int>> GetAll();
  }

  public class CountryAndSkinColorLibrary : ICountryAndSkinColorLibrary
  {
    private readonly Dictionary<string, int> _skinColorByCountry;

    public CountryAndSkinColorLibrary(string libraryFilePath)
    {
      var keyValuePairs = File.ReadAllLines(libraryFilePath)
        .Select(l => l.Split(','))
        .Select(l => new KeyValuePair<string, int>(l[0], int.Parse(l[1])));

      _skinColorByCountry = keyValuePairs.ToDictionary(p => p.Key, p => p.Value);
    }

    public int? this[string key]
    {
      get
      {
        _skinColorByCountry.TryGetValue(key, out var value);
        return value != 0
          ? value
          : null;
      }
    }

    public IEnumerable<KeyValuePair<string, int>> GetAll() => _skinColorByCountry.AsEnumerable();
  }
}
