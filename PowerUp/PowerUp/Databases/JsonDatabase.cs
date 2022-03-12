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

    private JsonDatabaseMetadata<TKeyParams> _metadata = new JsonDatabaseMetadata<TKeyParams>();

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
        @object.Id = _metadata.IncrementId();

      var existingFilePath = _metadata.GetFilepathsFor("Id", @object.Id!.Value).SingleOrDefault();
      var newFilePath = Path.Combine(_databaseDirectory, KeysToFilename(@object.GetFileKeys()));

      if (existingFilePath != null && existingFilePath != newFilePath)
        File.Delete(existingFilePath);

      var stringObject = JsonSerializer.Serialize(@object, _serializerOptions);
      File.WriteAllText(newFilePath, stringObject);
      
      _metadata.UpdateIndexes(@object.GetFileKeys(), newFilePath);
      SaveMetadata();
    }

    public TEntity? Load(int id)
    {
      var filePath = _metadata.GetFilepathsFor("Id", id).SingleOrDefault();
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

    public IEnumerable<TEntity> LoadBy(string key, object value) => _metadata.GetFilepathsFor(key, value).Select(Load);

    private void LoadMetadata()
    {
      if (!File.Exists(_metadataPath))
        return;
      
      File.ReadAllTextAsync(_metadataPath).ContinueWith(stringObject =>
      {
        _metadata = JsonSerializer.Deserialize<JsonDatabaseMetadata<TKeyParams>>(stringObject.Result, _serializerOptions)!;
      });
    }
    private void SaveMetadata()
    {
      var stringObject = JsonSerializer.Serialize(_metadata, _serializerOptions);
      File.WriteAllTextAsync(_metadataPath, stringObject);
    }

    private static string KeysToFilename(TKeyParams keyObject) => $"{FilenameBuilder.Build(keyObject)}.json";
  }

  public class JsonDatabaseMetadata<TKeyParams> where TKeyParams : KeyParams
  {
    public int NextId { get; set; }
    public IDictionary<string, IDictionary<string, IEnumerable<string>>> Indexes { get; set; }

    public JsonDatabaseMetadata()
    {
      NextId = 1;
      Indexes = new Dictionary<string, IDictionary<string, IEnumerable<string>>>();
    }

    public int IncrementId()
    {
      var currentId = NextId;
      NextId = currentId + 1;
      return currentId;
    }

    public void UpdateIndexes(TKeyParams keyParams, string filename)
    {
      foreach(var kvp in keyParams.GetKeysAndValues())
      {
        var existing = GetFilepathsFor(kvp.Key, kvp.Value);
        if(!Indexes.ContainsKey(kvp.Key))
          Indexes.Add(kvp.Key, new Dictionary<string, IEnumerable<string>>());

        Indexes[kvp.Key][kvp.Value] = existing
          .Where(p => Path.GetFileName(p).Split('_')[0] != keyParams.Id.ToString())
          .Append(filename);
      }
    }

    public IEnumerable<string> GetFilepathsFor(string key, object value)
    {
      Indexes.TryGetValue(key, out var filePathDictionary);
      if(filePathDictionary == null)
        return Enumerable.Empty<string>();

      filePathDictionary.TryGetValue(value.ToString()!, out var filePaths);
      if (filePaths == null)
        return Enumerable.Empty<string>();

      return filePaths;
    } 
  }
}
