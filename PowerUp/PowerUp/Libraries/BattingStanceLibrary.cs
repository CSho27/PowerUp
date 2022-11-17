using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Libraries
{
  public interface IBattingStanceLibrary
  {
    int this[string key] { get; }
    string this[int key] { get; }
    public IEnumerable<KeyValuePair<int, string>> GetAll();
  }

  public class BattingStanceLibrary : CsvKeyValueLibrary<string, int>, IBattingStanceLibrary
  {
    public const string STANDARD1_KEY = "Standard 1";
    public const string STANDARD2_KEY = "Standard 2";
    public const string STANDARD3_KEY = "Standard 3";
    public const string STANDARD4_KEY = "Standard 4";
    public const string STANDARD5_KEY = "Standard 5";
    public const string STANDARD6_KEY = "Standard 6";
    public const string STANDARD7_KEY = "Standard 7";
    public const string STANDARD8_KEY = "Standard 8";
    public const string STANDARD9_KEY = "Standard 9";
    public const string STANDARD10_KEY = "Standard 10";
    public const string STANDARD11_KEY = "Standard 11";
    public const string STANDARD12_KEY = "Standard 12";
    public const string STANDARD13_KEY = "Standard 13";
    public const string STANDARD14_KEY = "Standard 14";
    public const string OPEN1_KEY = "Open 1";
    public const string OPEN2_KEY = "Open 2";
    public const string OPEN3_KEY = "Open 3";
    public const string OPEN4_KEY = "Open 4";
    public const string OPEN5_KEY = "Open 5";
    public const string OPEN6_KEY = "Open 6";
    public const string OPEN7_KEY = "Open 7";
    public const string OPEN8_KEY = "Open 8";
    public const string OPEN9_KEY = "Open 9";
    public const string OPEN10_KEY = "Open 10";
    public const string OPEN11_KEY = "Open 11";
    public const string OPEN12_KEY = "Open 12";
    public const string OPEN13_KEY = "Open 13";
    public const string OPEN14_KEY = "Open 14";
    public const string OPEN15_KEY = "Open 15";
    public const string OPEN16_KEY = "Open 16";
    public const string OPEN17_KEY = "Open 17";
    public const string OPEN18_KEY = "Open 18";
    public const string OPEN19_KEY = "Open 19";
    public const string CLOSED1_KEY = "Closed 1";
    public const string CLOSED2_KEY = "Closed 2";
    public const string CLOSED3_KEY = "Closed 3";
    public const string CLOSED4_KEY = "Closed 4";
    public const string CLOSED5_KEY = "Closed 5";
    public const string CLOSED6_KEY = "Closed 6";
    public const string CROUCHING1_KEY = "Crouching 1";
    public const string CROUCHING2_KEY = "Crouching 2";
    public const string CROUCHING3_KEY = "Crouching 3";
    public const string CROUCHING4_KEY = "Crouching 4";

    public BattingStanceLibrary(string libraryFilePath) : base(libraryFilePath) { }

    IEnumerable<KeyValuePair<int, string>> IBattingStanceLibrary.GetAll() => GetAll().Select(kvp => new KeyValuePair<int, string>(kvp.Value, kvp.Key));

    protected override int OnKeyNotFound(string key) => throw new KeyNotFoundException(key);
    protected override string OnValueNotFound(int value) => throw new KeyNotFoundException(value.ToString());

    protected override string ParseKey(string keyString) => keyString;
    protected override int ParseValue(string valueString) => int.Parse(valueString);
  }
}
