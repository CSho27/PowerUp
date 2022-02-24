using System.Collections.Generic;

namespace PowerUp.Libraries
{
  public interface ISpecialSavedNameLibrary
  {
    int this[string key] { get; }
    string this[int key] { get; }
  }

  public class SpecialSavedNameLibrary : CsvKeyValueLibrary<string, int>, ISpecialSavedNameLibrary
  {
    public SpecialSavedNameLibrary(string libraryFilePath): base(libraryFilePath) { }

    protected override int OnKeyNotFound(string key) => throw new KeyNotFoundException();
    protected override string OnValueNotFound(int value) => throw new KeyNotFoundException();

    protected override string ParseKey(string keyString) => keyString;
    protected override int ParseValue(string valueString) => int.Parse(valueString);
  }
}
