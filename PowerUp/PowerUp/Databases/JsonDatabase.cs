using System;
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
      _metadataPath = Path.Combine(_databaseDirectory, "./Metadata");

      LoadMetadata();
    }

    public void Save(TEntity @object)
    {
      Directory.CreateDirectory(_databaseDirectory);

      if (!@object.Id.HasValue)
      {
        @object.Id = _metadata.IncrementId();
        SaveMetadata();
      }

      var existingFilePath = FindFilenameForId(@object.Id!.Value);
      var newFilePath = Path.Combine(_databaseDirectory, KeysToFilename(@object.GetFileKeys()));

      if (existingFilePath != null && existingFilePath != newFilePath)
        File.Delete(Path.Combine(_databaseDirectory, existingFilePath));

      var stringObject = JsonSerializer.Serialize(@object, _serializerOptions);
      File.WriteAllText(newFilePath, stringObject);
    }

    public TEntity? Load(int id)
    {
      var filePath = FindFilenameForId(id);
      if(filePath == null)
        return null;

      var stringObject = File.ReadAllText(filePath);
      var @object = JsonSerializer.Deserialize<TEntity>(stringObject, _serializerOptions);
      if (@object == null)
        throw new Exception("Failed to serialize object");

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

    private string? FindFilenameForId(int id)
      => Directory.EnumerateFiles(_databaseDirectory).Select(f => Path.GetFileName(f)).Where(n => n != "Metadata").SingleOrDefault(n => FilenameToKeys(n).Id == id);

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
