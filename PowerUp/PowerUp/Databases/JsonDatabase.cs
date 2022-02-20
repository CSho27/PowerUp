using System;
using System.IO;
using System.Text.Json;

namespace PowerUp.Databases
{
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
