using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Libraries
{
  public interface IVoiceLibrary
  {
    int this[string key] { get; }
    string this[int key] { get; }
    public IEnumerable<KeyValuePair<int, string>> GetAll();
    KeyValuePair<int, string> FindClosestTo(string firstName, string lastName);
  }

  public class VoiceLibrary : CsvKeyValueLibrary<string, int>, IVoiceLibrary
  {
    private readonly IDictionary<int, string> _firstNames;
    private readonly IDictionary<int, string> _firstNamesSoundexes;
    private readonly IDictionary<int, string> _lastNames;
    private readonly IDictionary<int, string> _lastNameSoundexes;

    public VoiceLibrary(string libraryFilePath) : base(libraryFilePath) 
    {
      var keyValuePairs = File.ReadAllLines(libraryFilePath)
        .Select(l => l.Split(','))
        .Select(l => new KeyValuePair<string, int>(l[0], int.Parse(l[1])));

      _firstNames = keyValuePairs.ToDictionary(kvp => kvp.Value, kvp => kvp.Key.Split("_")[0]);
      _firstNamesSoundexes = keyValuePairs.ToDictionary(kvp => kvp.Value, kvp => kvp.Key.Split("_")[0].GetSoundex());

      _lastNames = keyValuePairs.ToDictionary(kvp => kvp.Value, kvp => kvp.Key.Split("_")[1]);
      _lastNameSoundexes = keyValuePairs.ToDictionary(kvp => kvp.Value, kvp => kvp.Key.Split("_")[1].RemovePrefixesAndSuffixes().GetSoundex());
    }

    IEnumerable<KeyValuePair<int, string>> IVoiceLibrary.GetAll() => GetAll().Select(kvp => new KeyValuePair<int, string>(kvp.Value, kvp.Key));

    protected override int OnKeyNotFound(string key) => throw new KeyNotFoundException(key);
    protected override string OnValueNotFound(int value) => throw new KeyNotFoundException(value.ToString());

    protected override string ParseKey(string keyString) => keyString.Replace("_"," ");
    protected override int ParseValue(string valueString) =>  int.Parse(valueString);

    public KeyValuePair<int, string> FindClosestTo(string firstName, string lastName)
    {
      return _keysByValue.MaxBy(kvp => kvp, new VoiceSoundexSimilarityComparer(
        _firstNames,
        _firstNamesSoundexes,
        _lastNames,
        _lastNameSoundexes,
        firstName,
        lastName
      ));
    }

    public class VoiceSoundexSimilarityComparer : IComparer<KeyValuePair<int, string>>
    {
      private readonly IDictionary<int, string> _firstNames;
      private readonly IDictionary<int, string> _firstNamesSoundexes;
      private readonly IDictionary<int, string> _lastNames;
      private readonly IDictionary<int, string> _lastNameSoundexes;
      private readonly string _firstName;
      private readonly string _firstNameSoundex;
      private readonly string _lastName;
      private readonly string _lastNameSoundex;

      public VoiceSoundexSimilarityComparer(
        IDictionary<int, string> firstNames,
        IDictionary<int, string> firstNamesSoundexes,
        IDictionary<int, string> lastNames,
        IDictionary<int, string> lastNameSoundexes,
        string firstName, 
        string lastName)
      {
        _firstNames = firstNames;
        _firstNamesSoundexes = firstNamesSoundexes;
        _lastNames = lastNames;
        _lastNameSoundexes = lastNameSoundexes;
        _firstName = firstName;
        _firstNameSoundex = _firstName.GetSoundex();
        _lastName = lastName.RemovePrefixesAndSuffixes();
        _lastNameSoundex = _lastName.GetSoundex();
      }

      public int Compare(KeyValuePair<int, string> kvp1, KeyValuePair<int, string> kvp2)
      {
        var testFirst1 = _firstNames[kvp1.Key];
        var testFirst2 = _firstNames[kvp2.Key];

        var testLast1 = _lastNames[kvp1.Key];
        var testLast2 = _lastNames[kvp2.Key];

        var testFirstSoundex1 = _firstNamesSoundexes[kvp1.Key];
        var testFirstSoundex2 = _firstNamesSoundexes[kvp2.Key];

        var testLastSoundex1 = _lastNameSoundexes[kvp1.Key];
        var testLastSoundex2 = _lastNameSoundexes[kvp2.Key];

        var lastSoundexCharsInCommon1 = _lastNameSoundex.BeginningCharsInCommon(testLastSoundex1);
        var lastSoundexCharsInCommon2 = _lastNameSoundex.BeginningCharsInCommon(testLastSoundex2);

        if (lastSoundexCharsInCommon1 > lastSoundexCharsInCommon2)
          return 1;
        else if (lastSoundexCharsInCommon1 < lastSoundexCharsInCommon2)
          return -1;

        var firstSoundexCharsInCommon1 = _firstNameSoundex.BeginningCharsInCommon(testFirstSoundex1);
        var firstSoundexCharsInCommon2 = _firstNameSoundex.BeginningCharsInCommon(testFirstSoundex2);

        if (firstSoundexCharsInCommon1 > firstSoundexCharsInCommon2)
          return 1;
        else if (firstSoundexCharsInCommon1 < firstSoundexCharsInCommon2)
          return -1;


        var lastCharsInCommon1 = _lastName.BeginningCharsInCommon(testLast1);
        var lastCharsInCommon2 = _lastName.BeginningCharsInCommon(testLast2);

        if (lastCharsInCommon1 > lastCharsInCommon2)
          return 1;
        else if (lastCharsInCommon1 < lastCharsInCommon2)
          return -1;

        var firstCharsInCommon1 = _firstName.BeginningCharsInCommon(testFirst1);
        var firstCharsInCommon2 = _firstName.BeginningCharsInCommon(testFirst2);

        if (firstCharsInCommon1 > firstCharsInCommon2)
          return 1;
        else if (firstCharsInCommon1 < firstCharsInCommon2)
          return -1;
        
        return 0;
      }
    }
  }
}
