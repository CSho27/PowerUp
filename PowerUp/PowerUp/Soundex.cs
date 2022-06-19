using System.Linq;
using System.Text;

namespace PowerUp
{
  public static class Soundex
  {
    public static string Of(string @string)
    {
      var words = @string.Split(' ', '-', '.', '~');
      var soundexes = words.Select(w => SoundexForWord(w));
      return string.Join(" ", soundexes);
    }

    private static string SoundexForWord(string word)
    {
      var upperCase = word.ToUpper().ToCharArray();
      var restOfString = upperCase.ToArray();

      var sb = new StringBuilder();
      for (int i = 0; sb.Length < 4; i++)
      {
        var hasCharAtIndex = i < restOfString.Length;
        if (hasCharAtIndex)
        {
          var thisChar = GetSoundexNumber(restOfString[i]);

          int nextNonHWYChar = 0;
          for (int j = i + 1; nextNonHWYChar == 0 && j < restOfString.Length; j++)
          {
            var soundex = GetSoundexNumber(restOfString[j]);
            if (soundex.HasValue)
              nextNonHWYChar = soundex.Value;
          }
          var isSameAsNext = thisChar == nextNonHWYChar;

          if (i == 0)
            sb.Append(restOfString[i]);
          else if (thisChar > 0 && !isSameAsNext)
            sb.Append(thisChar);

          if (i == 0 && isSameAsNext)
            i++;
        }
        else
        {
          sb.Append(0);
        }
      }

      return sb.ToString();
    }

    public static int? GetSoundexNumber(char @char)
    {
      switch(@char)
      {
        case 'A':
        case 'E':
        case 'I':
        case 'O':
        case 'U':
          return -1;
        case 'H':
        case 'W':
        case 'Y':
          return 0;
        case 'B':
        case 'F':
        case 'P': 
        case 'V':
          return 1;
        case 'C':
        case 'G':
        case 'J':
        case 'K':
        case 'Q':
        case 'S':
        case 'X':
        case 'Z':
          return 2;
        case 'D':
        case 'T':
          return 3;
        case 'L':
          return 4;
        case 'M':
        case 'N':
          return 5;
        case 'R':
          return 6;
        default:
          return null;
      }
    }
  }
}
