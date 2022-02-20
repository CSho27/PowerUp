using System;
using System.IO;
using System.Text.Json;

namespace PowerUp.Databases
{
  public interface IJsonDatabase
  {
    void Save<TObject>(TObject @object) where TObject : IEntity;
    TObject Load<TObject>(string key) where TObject : IEntity;
  }

  public class JsonDatabase : IJsonDatabase 
  {
    private readonly string _dataDirectory;
    private readonly JsonSerializerOptions _serializerOptions;

    public JsonDatabase(string dataDirectory)
    {
      _dataDirectory = dataDirectory;
      _serializerOptions = new JsonSerializerOptions()
      {
        Converters = { new DateOnlyJsonConverter() }
      };
    }

    public void Save<TObject>(TObject @object) where TObject : IEntity
    {
      var directory = Path.Combine(_dataDirectory, GetDirectoryName<TObject>());
      Directory.CreateDirectory(directory);
      var filePath = Path.Combine(directory, KeyToFileName(@object.GetKey()));
      var stringObject = JsonSerializer.Serialize(@object, _serializerOptions);
      File.WriteAllText(filePath, stringObject);
    }

    public TObject Load<TObject>(string key) where TObject : IEntity
    {
      var filePath = Path.Join(_dataDirectory, GetDirectoryName<TObject>(), KeyToFileName(key));
      var stringObject = File.ReadAllText(filePath);
      var @object = JsonSerializer.Deserialize<TObject>(stringObject, _serializerOptions);
      if (@object == null)
        throw new Exception($"Failed to serialize object for {key}");

      return @object;
    }

    private static string KeyToFileName(string key) => $"{key}.json";
    private static string GetDirectoryName<TObject>() => $"{typeof(TObject).Name}s";
  }
}
