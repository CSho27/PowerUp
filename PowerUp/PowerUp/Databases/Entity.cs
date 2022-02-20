using System.IO;
using System.Linq;

namespace PowerUp.Databases
{
  public abstract class Entity<TKeyParams> where TKeyParams : notnull
  {
    public static string KeyFor(TKeyParams keyParams) 
    {
        var @params = keyParams
          .GetType()
          .GetProperties()
          .Select(p => p.GetValue(keyParams))
          .Where(v => v != null);

        return ScrubKey(string.Join("_", @params));
    }

    private static string ScrubKey(string key)
    {
      var scrubbedKey = key;
      foreach (var @char in Path.GetInvalidFileNameChars().Concat(new[] { '.', ' ', '-', '\'' }))
        scrubbedKey = scrubbedKey.Replace(@char.ToString(), "");

      return scrubbedKey;
    }

    protected abstract TKeyParams GetKeyParams();
    public string GetKey() => KeyFor(GetKeyParams());
  }
}
