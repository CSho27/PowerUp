using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Libraries
{
  public interface ICharacterLibrary
  {
    char this[ushort key] { get; }
    ushort? this[char key] { get; }
  }

  public class CharacterLibrary : ICharacterLibrary
  {
    private readonly IDictionary<ushort, char> _charsByShort;
    private readonly IDictionary<char, ushort> _shortsByChar;

    internal CharacterLibrary(string libraryFilePath)
    {
      var allLinesSplitByComma = File.ReadAllLines(libraryFilePath)
        .Select(l => l.Split(","))
        .Select(l => (@ushort: ushort.Parse(l[0]), @char: char.Parse(l[1])));

      _charsByShort = allLinesSplitByComma.ToDictionary(l => l.@ushort, l => l.@char);
      _shortsByChar = allLinesSplitByComma.ToDictionary(l => l.@char, l => l.@ushort);
    }

    public char this[ushort key] => _charsByShort.TryGetValue(key, out var @char)
      ? @char
      : '*';
    public ushort? this[char key] => _shortsByChar.TryGetValue(key, out var @ushort)
      ? @ushort
      : null;
  }
}
