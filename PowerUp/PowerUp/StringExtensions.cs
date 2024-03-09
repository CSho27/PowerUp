using System.Linq;

namespace PowerUp
{
  public static class StringExtensions
  {
    public static char FirstCharacter(this string @string) => @string.ToCharArray().First();

    public static int BeginningCharsInCommon(this string string1, string string2)
    {
      var index = 0;
      while(index < string1.Length && index < string2.Length && string1[index] == string2[index])
        index++;

      return index;
    }

    public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);
    public static bool IsNullOrWhiteSpace(this string? value) => string.IsNullOrWhiteSpace(value);
    public static bool IsNotNullOrEmpty(this string? value) => !string.IsNullOrEmpty(value);
    public static bool IsNotNullOrWhiteSpace(this string? value) => !string.IsNullOrWhiteSpace(value);
  }
}
