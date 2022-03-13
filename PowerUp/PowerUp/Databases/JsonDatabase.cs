using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace PowerUp.Databases
{
  public abstract class JsonDatabase<TEntity> where TEntity : Entity
  {
    private const string METADATA_FILE_NAME = ".Metadata";

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
      _metadataPath = Path.Combine(_databaseDirectory, METADATA_FILE_NAME);

      LoadMetadata();
    }

    public void Save(TEntity @object)
    {
      if (!@object.Id.HasValue)
        @object.Id = _metadata.IncrementId();

      var stringObject = JsonSerializer.Serialize(@object, _serializerOptions);
      var filePath = GetFilePath(@object.Id.Value);
      File.WriteAllText(filePath, stringObject);
      
      SaveMetadata();
    }

    public TEntity? Load(int id)
    {
      var filePath = GetFilePath(id);
      if (filePath == null)
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

    public IEnumerable<TEntity> LoadAll () => Directory.EnumerateFiles(_databaseDirectory)
      .Where(p => Path.GetFileName(p) != METADATA_FILE_NAME)
      .Select(p => Load(p));

    private void LoadMetadata()
    {
      if (!File.Exists(_metadataPath))
        return;

      var stringObject = File.ReadAllText(_metadataPath);
      _metadata = JsonSerializer.Deserialize<JsonDatabaseMetadata>(stringObject, _serializerOptions)!;
    }

    private void SaveMetadata()
    {
      var stringObject = JsonSerializer.Serialize(_metadata, _serializerOptions);
      File.WriteAllText(_metadataPath, stringObject);
    }

    private string GetFilePath(int id) => Path.Combine(_databaseDirectory, $"{id}.json");
  }

  public class JsonDatabaseMetadata
  {
    public int NextId { get; set; }

    public JsonDatabaseMetadata()
    {
      NextId = 1;
    }

    public int IncrementId()
    {
      var currentId = NextId;
      NextId = currentId + 1;
      return currentId;
    }
  }
}
