using System.Collections.Generic;

namespace PowerUp.Libraries
{
  public interface ICharacterLibrary
  {
    char this[ushort key] { get; }
    ushort this[char key] { get; }
  }

  public class CharacterLibrary : CsvKeyValueLibrary<ushort, char>, ICharacterLibrary
  {
    public CharacterLibrary(string libraryFilePath): base(libraryFilePath) { }

    protected override char OnKeyNotFound(ushort key) => '*';
    protected override ushort OnValueNotFound(char value) => throw new KeyNotFoundException();

    protected override ushort ParseKey(string keyString) => ushort.Parse(keyString);
    protected override char ParseValue(string valueString) => char.Parse(valueString);

    char ICharacterLibrary.this[ushort key] => this[key];
  }
}
