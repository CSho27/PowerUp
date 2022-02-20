using System.IO;
using System.Linq;

namespace PowerUp.Databases
{
  public interface IEntity 
  {
    string GetKey();
  }

  public abstract class Entity<TKeyParams> : IEntity where TKeyParams : KeyParams
  {
    protected abstract TKeyParams GetKeyParams();
    public string GetKey() => KeyFor(GetKeyParams());
    public static string KeyFor(TKeyParams keyParams) => keyParams;
  }

  public abstract class KeyParams
  {
    public static implicit operator string(KeyParams keyParams)
    {
      var @params = keyParams
        .GetType()
        .GetProperties()
        .Select(p => p.GetValue(keyParams))
        .Where(v => v != null);

      return ScrubKey(string.Join("_", @params)); ;
    }

    private static string ScrubKey(string key)
    {
      var scrubbedKey = key;
      foreach (var @char in Path.GetInvalidFileNameChars().Concat(new[] { '.', ' ', '-', '\'' }))
        scrubbedKey = scrubbedKey.Replace(@char.ToString(), "");

      return scrubbedKey;
    }
  }
}
