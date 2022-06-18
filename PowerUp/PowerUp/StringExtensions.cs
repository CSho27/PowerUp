using System.Linq;

namespace PowerUp
{
  public static class StringExtensions
  {
    public static char FirstCharacter(this string @string) => @string.ToCharArray().First();
  }
}
