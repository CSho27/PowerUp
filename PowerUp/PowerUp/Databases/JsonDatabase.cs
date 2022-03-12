using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace PowerUp.Databases
{
  public abstract class JsonDatabase<TEntity, TKeyParams>
    where TEntity : Entity<TKeyParams>
    where TKeyParams : KeyParams
  {
    private readonly string _databaseDirectory;
    private readonly string _metadataPath;
    private readonly JsonSerializerOptions _serializerOptions;

    private JsonDatabaseMetadata _metadata = new JsonDatabaseMetadata();

    public JsonDatabase(string dataDirectory)
    {
      _databaseDirectory = Path.Combine(dataDirectory, $"{typeof(TEntity).Name}s");
      _serializerOptions = new JsonSerializerOptions()
      {
        Converters = { new DateOnlyJsonConverter() }
      };

      Directory.CreateDirectory(_databaseDirectory);
      _metadataPath = Path.Combine(_databaseDirectory, "./Metadata");

      LoadMetadata();
    }

    public void Save(TEntity @object)
    {
      if (!@object.Id.HasValue)
      {
        @object.Id = _metadata.IncrementId();
        SaveMetadata();
      }

      var existingFilePath = FindPathForId(@object.Id!.Value);
      var newFilePath = Path.Combine(_databaseDirectory, KeysToFilename(@object.GetFileKeys()));

      if (existingFilePath != null && existingFilePath != newFilePath)
        File.Delete(existingFilePath);

      var stringObject = JsonSerializer.Serialize(@object, _serializerOptions);
      File.WriteAllText(newFilePath, stringObject);
    }

    public TEntity? Load(int id)
    {
      var filePath = FindPathForId(id);
      if(filePath == null)
        return null;

      return Load(filePath);
    }

    private TEntity Load(string filePath)
    {
      var stringObject = File.ReadAllText(filePath);
      var @object = JsonSerializer.Deserialize<TEntity>(stringObject, _serializerOptions);
      if (@object == null)
        throw new Exception("Failed to serialize object");

      return @object;
    }

    public IEnumerable<TEntity> LoadBy(Func<TKeyParams, bool> conditionCallback)
    {
      return Directory.EnumerateFiles(_databaseDirectory).Where(p =>
      {
        var keys = GetKeysForFilePath(p);
        return keys != null
          ? conditionCallback(keys)
          : false;
      }).Select(Load);
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

    private string? FindPathForId(int id)
      => Directory.EnumerateFiles(_databaseDirectory).SingleOrDefault(p => FileMatchesId(p, id));

    private TKeyParams? GetKeysForFilePath(string filePath)
    {
      var filename = Path.GetFileName(filePath);
      if (filename == "Metadata")
        return null;

      return FilenameToKeys(filename);
    }

    private bool FileMatchesId(string filePath, int id)
    {
      var fileKeys = GetKeysForFilePath(filePath);
      return fileKeys?.Id == id;
    }

    private static string KeysToFilename(TKeyParams keyObject) => $"{FileKeySerializer.Serialize(keyObject)}.json";
    private static TKeyParams FilenameToKeys(string filename)
    {
      var keyString = filename.Replace(".json", "");
      return FileKeySerializer.Deserialize<TKeyParams>(keyString);
    } 
  }

  public class JsonDatabaseMetadata
  {
    public int NextId { get; set; } = 1;

    public int IncrementId()
    {
      var currentId = NextId;
      NextId = currentId + 1;
      return currentId;
    }
  }
}
