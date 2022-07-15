using System.Linq;

namespace PowerUp
{
  public static class CharExtensions
  {
    public static bool IsVowel(this char @char)
    {
      var lowercaseChar = @char.ToString().ToLower().ToCharArray().Single();
      return lowercaseChar == 'a' || lowercaseChar == 'e' || lowercaseChar == 'i' || lowercaseChar == 'o' || lowercaseChar == 'u';
    }
  }
}
