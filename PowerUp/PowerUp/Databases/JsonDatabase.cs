using System;
using System.IO;
using System.Linq;
using System.Text.Json;

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

  public interface IJsonDatabase<TObject, TKeyParams>
    where TObject : Entity<TKeyParams>
    where TKeyParams : notnull
  {
    void Save(TObject @object);
    TObject Load(TKeyParams keyParams);
    TObject Load(string key);
  }

  public class JsonDatabase<TObject, TKeyParams> : IJsonDatabase<TObject, TKeyParams> 
    where TObject : Entity<TKeyParams> 
    where TKeyParams : notnull
  {
    private readonly string _folderPath;
    private readonly JsonSerializerOptions _serializerOptions;

    public JsonDatabase(string folderPath)
    {
      _folderPath = folderPath;
      _serializerOptions = new JsonSerializerOptions()
      {
        Converters = { new DateOnlyJsonConverter() }
      };
    }

    public void Save(TObject @object)
    {
      Directory.CreateDirectory(_folderPath);
      var filePath = Path.Combine(_folderPath, KeyToFileName(@object.GetKey()));
      var stringObject = JsonSerializer.Serialize(@object, _serializerOptions);
      File.WriteAllText(filePath, stringObject);
    }

    public TObject Load(TKeyParams keyParams) => Load(Entity<TKeyParams>.KeyFor(keyParams));

    public TObject Load(string key)
    {
      var filePath = Path.Combine(_folderPath, KeyToFileName(key));
      var stringObject = File.ReadAllText(filePath);
      var @object = JsonSerializer.Deserialize<TObject>(stringObject, _serializerOptions);
      if (@object == null)
        throw new Exception($"Failed to serialize object for {key}");

      return @object;
    }

    private string KeyToFileName(string key) => $"{key}.json";
  }
}
