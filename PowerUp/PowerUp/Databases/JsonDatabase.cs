using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    private readonly string _metadataPath;
    private readonly JsonSerializerOptions _serializerOptions;

    private JsonDatabaseMetadata _metadata = new JsonDatabaseMetadata();

    public JsonDatabase(string dataDirectory)
    {
      _dataDirectory = dataDirectory;
      _serializerOptions = new JsonSerializerOptions()
      {
        Converters = { new DateOnlyJsonConverter() }
      };
      _metadataPath = Path.Combine(_dataDirectory, "./Metadata");

      LoadMetadata();
    }

    public void Save<TObject>(TObject @object) where TObject : IEntity
    {
      var dirName = GetDirectoryName<TObject>();

      if (!@object.Id.HasValue)
      {
        @object.Id = _metadata.IncrementId(dirName);
        SaveMetadata();
      }

      var directory = Path.Combine(_dataDirectory, dirName);
      Directory.CreateDirectory(directory);
      var filePath = Path.Combine(directory, KeysToFilename(@object.GetFileKeyObject()));
      var stringObject = JsonSerializer.Serialize(@object, _serializerOptions);
      File.WriteAllText(filePath, stringObject);
    }

    public TObject Load<TObject>(string key) where TObject : IEntity
    {
      var filePath = Path.Join(_dataDirectory, GetDirectoryName<TObject>(), KeysToFilename(key));
      var stringObject = File.ReadAllText(filePath);
      var @object = JsonSerializer.Deserialize<TObject>(stringObject, _serializerOptions);
      if (@object == null)
        throw new Exception($"Failed to serialize object for {key}");

      return @object;
    }

    private void LoadMetadata()
    {
      if (!File.Exists(_metadataPath))
        return;
      
      File.ReadAllTextAsync(_metadataPath).ContinueWith(stringObject =>
      {
        _metadata = JsonSerializer.Deserialize<JsonDatabaseMetadata>(stringObject.Result, _serializerOptions)!;
      });
    }
    private void SaveMetadata()
    {
      var stringObject = JsonSerializer.Serialize(_metadata, _serializerOptions);
      File.WriteAllTextAsync(_metadataPath, stringObject);
    }

    private string FindFileNameForId<TObject>(int Id)
    {
      var file = Directory.EnumerateFiles(_dataDirectory, GetDirectoryName<TObject>()).Single(f => true);
      return "";
    }

    private static string KeysToFilename(object keyObject) => $"{FileKeySerializer.Serialize(keyObject)}.json";
    private static dynamic GetKeysFromFilename(string filename)
    {
      var keyString = filename.Replace(".json", "");
      return FileKeySerializer.Deserialize<dynamic>(keyString);
    } 
    private static string GetDirectoryName<TObject>() => $"{typeof(TObject).Name}s";
  }

  public class JsonDatabaseMetadata
  {
    public IDictionary<string, int> IdCounts { get; set; } = new Dictionary<string, int>();

    public int IncrementId(string directoryName)
    {
      var currentId = GetNextId(directoryName);
      IdCounts[directoryName] = currentId + 1;
      return currentId;
    }

    private int GetNextId(string directoryName)
    {
      var exists = IdCounts.TryGetValue(directoryName, out var nextId);
      return exists
        ? nextId
        : 1;
    }
  }
}
