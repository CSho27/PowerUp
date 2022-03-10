using System.IO;
using System.Linq;

namespace PowerUp.Databases
{
  public abstract class Entity<TKeyParams> where TKeyParams : KeyParams
  {
    public int? Id { get; set; }
    protected abstract TKeyParams GetKeyParams();

    public TKeyParams GetFileKeys()
    {
      var @params = GetKeyParams();
      @params.Id = Id!.Value;
      return @params;
    }
  }

  public abstract class KeyParams
  {
    public int Id { get; set; }

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
