using System.IO;
using System.Linq;

namespace PowerUp.Databases
{
  public static class FilenameBuilder
  {
    public static string Build<TKeyParams>(TKeyParams @object) where TKeyParams : KeyParams
    {
      var values = @object.GetKeysAndValues().Select(kvp => Scrub(kvp.Value));
      return string.Join('_', values);
    }

    private static string Scrub(string value)
    {
      var scrubbedValue = value;
      foreach (var @char in Path.GetInvalidFileNameChars().Concat(new[] { '.', ' ', '-', '\'' }))
        scrubbedValue = scrubbedValue.Replace(@char.ToString(), "");

      return scrubbedValue;
    }
  }
}
