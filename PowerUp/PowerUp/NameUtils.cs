using System;

namespace PowerUp
{
  public static class NameUtils
  {
    public static string GetFirstName(this string formalDisplayName)
    {
      if (!formalDisplayName.Contains(","))
        throw new InvalidOperationException("Name in incorrect input format");

      return formalDisplayName.Split(",")[0].Trim();
    }

    public static string GetLastName(this string formalDisplayName)
    {
      if (!formalDisplayName.Contains(","))
        throw new InvalidOperationException("Name in incorrect input format");

      return formalDisplayName.Split(",")[1].Trim();
    }

    public static string GetInformalDisplayName(this string formalDisplayName) => $"{formalDisplayName.GetFirstName()} {formalDisplayName.GetLastName()}";
  }
}
