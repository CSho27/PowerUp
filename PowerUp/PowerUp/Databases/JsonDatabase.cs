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
      if (!@object.Id.HasValue)
      {
        @object.Id = _metadata.IncrementId();
        SaveMetadata();
      }

      Directory.CreateDirectory(_databaseDirectory);
      var existingFilename = FindFilenameForId(@object.Id!.Value);
      var newFilename = KeysToFilename(@object.GetFileKeys());

      if (existingFilename != null && existingFilename != newFilename)
        File.Delete(Path.Combine(_databaseDirectory, existingFilename));

      var filePath = Path.Combine(_databaseDirectory, newFilename);
      var stringObject = JsonSerializer.Serialize(@object, _serializerOptions);
      File.WriteAllText(filePath, stringObject);
    }

    public TEntity? Load(int id)
    {
      var filename = FindFilenameForId(id);
      if(filename == null)
        return null;

      var stringObject = File.ReadAllText(Path.Combine(_databaseDirectory, filename));
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
      => Directory.EnumerateFiles(_databaseDirectory).SingleOrDefault(f => FilenameToKeys(f).Id == id);

    private static string KeysToFilename(object keyObject) => $"{FileKeySerializer.Serialize(keyObject)}.json";
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
