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
    public BattingStanceLibrary(string libraryFilePath) : base(libraryFilePath) { }

    IEnumerable<KeyValuePair<int, string>> IBattingStanceLibrary.GetAll() => GetAll().Select(kvp => new KeyValuePair<int, string>(kvp.Value, kvp.Key));

    protected override int OnKeyNotFound(string key) => throw new KeyNotFoundException(key);
    protected override string OnValueNotFound(int value) => throw new KeyNotFoundException(value.ToString());

    protected override string ParseKey(string keyString) => keyString;
    protected override int ParseValue(string valueString) => int.Parse(valueString);
  }
}
