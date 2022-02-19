using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PowerUp.Databases
{
  public interface IHaveDatabaseKeys<TDatabaseKeys>
  {
    TDatabaseKeys DatabaseKeys { get; }
  }

  public interface IJsonDatabase<TObject, TDatabaseKeys>
    where TObject : IHaveDatabaseKeys<TDatabaseKeys>
    where TDatabaseKeys : notnull
  {
    void Save(TObject @object);
    TObject Load(TDatabaseKeys keys);
  }

  public class JsonDatabase<TObject, TDatabaseKeys> : IJsonDatabase<TObject, TDatabaseKeys> 
    where TObject : IHaveDatabaseKeys<TDatabaseKeys> 
    where TDatabaseKeys : notnull
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
      var filePath = Path.Combine(_folderPath, GetFileName(@object.DatabaseKeys));
      var stringObject = JsonSerializer.Serialize(@object, _serializerOptions);
      File.WriteAllText(filePath, stringObject);
    }

    public TObject Load(TDatabaseKeys keys)
    {
      var fileName = GetFileName(keys);
      var filePath = Path.Combine(_folderPath, fileName);
      var stringObject = File.ReadAllText(filePath);
      var @object = JsonSerializer.Deserialize<TObject>(stringObject, _serializerOptions);
      if (@object == null)
        throw new Exception($"Failed to serialize object for {fileName}");

      return @object;
    }

    private string GetFileName(TDatabaseKeys databaseKeys)
    {
      var keys = databaseKeys
        .GetType()
        .GetProperties()
        .Select(p => p.GetValue(databaseKeys))
        .Where(v => v != null);

      var fileName = ScrubFileName(string.Join("_", keys));
      return $"{fileName}.json";
    }

    private string ScrubFileName(string fileName)
    {
      var newFileName = fileName;
      foreach (var @char in Path.GetInvalidFileNameChars().Append('.').Append(' '))
        newFileName = newFileName.Replace(@char.ToString(), "");

      return newFileName;
    }
  }
}
