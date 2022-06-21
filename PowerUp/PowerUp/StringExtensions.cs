using System.Linq;

namespace PowerUp
{
  public static class StringExtensions
  {
    public static char FirstCharacter(this string @string) => @string.ToCharArray().First();

    public static int CharsInCommon(this string string1, string string2)
    {
      var matchCount = 0;
      for (int i = 0; i < string1.Length && i < string2.Length; i++)
      {
        if (string1[i] == string2[i])
          matchCount++;
      }
      return matchCount;
    }
  }
}
