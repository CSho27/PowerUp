using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.GameSave
{
  public class CharacterLibrary
	{
		public static CharacterLibrary Default = new CharacterLibrary();

		private readonly IDictionary<ushort, char> _charsByShort;
		private readonly IDictionary<char, ushort> _shortsByChar;
		
		private CharacterLibrary()
		{
			var allLinesSplitByComma = File.ReadAllLines("C:/dev/PowerUp/PowerUp/PowerUp/Data/PP Character Table.csv")
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
			: (ushort?)null;
	}
}
