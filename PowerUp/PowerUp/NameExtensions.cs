using System.Linq;

namespace PowerUp
{
  public static class NameExtensions
  {
    public static string RemovePrefixesAndSuffixes(this string name)
    {
      return name
        .Replace("Jr.", "")
        .Replace("III", "")
        .Replace("De", "")
        .Replace("La", "")
        .Trim();
    }

    public static string ShortenNameToLength(this string name, int length)
    {
      if(name.Length <= length)
        return name;

      var nameWithoutPrefixesAndSuffixes = name.RemovePrefixesAndSuffixes();
      if (nameWithoutPrefixesAndSuffixes.Length <= length)
        return nameWithoutPrefixesAndSuffixes;

      return new string(name.Take(10).ToArray());
    }
  }
}
