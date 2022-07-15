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
  }
}
