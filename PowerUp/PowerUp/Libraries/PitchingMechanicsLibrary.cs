using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Libraries
{
  public interface IPitchingMechanicsLibrary
  {
    int this[string key] { get; }
    string this[int key] { get; }
    public IEnumerable<KeyValuePair<int, string>> GetAll();
  }

  public class PitchingMechanicsLibrary : CsvKeyValueLibrary<string, int>, IPitchingMechanicsLibrary
  {
    public const string OVERHAND1A_KEY = "Overhand 1a";
    public const string OVERHAND1B_KEY = "Overhand 1b";
    public const string OVERHAND2_KEY = "Overhand 2";
    public const string OVERHAND3_KEY = "Overhand 3";
    public const string OVERHAND4_KEY = "Overhand 4";
    public const string OVERHAND5_KEY = "Overhand 5";
    public const string OVERHAND6_KEY = "Overhand 6";
    public const string OVERHAND7_KEY = "Overhand 7";
    public const string OVERHAND8_KEY = "Overhand 8";
    public const string OVERHAND9_KEY = "Overhand 9";
    public const string OVERHAND10_KEY = "Overhand 10";
    public const string OVERHAND11_KEY = "Overhand 11";
    public const string OVERHAND12_KEY = "Overhand 12";
    public const string OVERHAND13_KEY = "Overhand 13";
    public const string OVERHAND14_KEY = "Overhand 14";
    public const string OVERHAND15_KEY = "Overhand 15";
    public const string OVERHAND16_KEY = "Overhand 16";
    public const string OVERHAND17_KEY = "Overhand 17";
    public const string _3_QUARTERS1_KEY = "3-Quarters 1";
    public const string _3_QUARTERS2_KEY = "3-Quarters 2";
    public const string _3_QUARTERS3_KEY = "3-Quarters 3";
    public const string _3_QUARTERS4_KEY = "3-Quarters 4";
    public const string _3_QUARTERS5_KEY = "3-Quarters 5";
    public const string _3_QUARTERS6_KEY = "3-Quarters 6";
    public const string _3_QUARTERS7_KEY = "3-Quarters 7";
    public const string _3_QUARTERS8_KEY = "3-Quarters 8";
    public const string _3_QUARTERS9_KEY = "3-Quarters 9";
    public const string _3_QUARTERS10_KEY = "3-Quarters 10";
    public const string _3_QUARTERS11_KEY = "3-Quarters 11";
    public const string _3_QUARTERS12_KEY = "3-Quarters 12";
    public const string _3_QUARTERS13_KEY = "3-Quarters 13";
    public const string _3_QUARTERS14_KEY = "3-Quarters 14";
    public const string _3_QUARTERS15_KEY = "3-Quarters 15";
    public const string _3_QUARTERS16_KEY = "3-Quarters 16";
    public const string _3_QUARTERS17_KEY = "3-Quarters 17";
    public const string _3_QUARTERS18_KEY = "3-Quarters 18";
    public const string SIDEARM1_KEY = "Sidearm 1";
    public const string SIDEARM2_KEY = "Sidearm 2";
    public const string SIDEARM3_KEY = "Sidearm 3";
    public const string SIDEARM4_KEY = "Sidearm 4";
    public const string SIDEARM5_KEY = "Sidearm 5";
    public const string SIDEARM6_KEY = "Sidearm 6";
    public const string SIDEARM7_KEY = "Sidearm 7";
    public const string SIDEARM8_KEY = "Sidearm 8";
    public const string SIDEARM9_KEY = "Sidearm 9";
    public const string SIDEARM10_KEY = "Sidearm 10";
    public const string SUBMARINE1_KEY = "Submarine 1";

    public PitchingMechanicsLibrary(string libraryFilePath) : base(libraryFilePath) { }

    IEnumerable<KeyValuePair<int, string>> IPitchingMechanicsLibrary.GetAll() => GetAll().Select(kvp => new KeyValuePair<int, string>(kvp.Value, kvp.Key));

    protected override int OnKeyNotFound(string key) => throw new KeyNotFoundException(key);
    protected override string OnValueNotFound(int value) => throw new KeyNotFoundException(value.ToString());

    protected override string ParseKey(string keyString) => keyString;
    protected override int ParseValue(string valueString) => int.Parse(valueString);
  }
}
