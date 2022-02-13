using System;
using System.IO;
using System.Text.Json;

namespace PowerUp.Database
{
  public class JsonDatabase<TObject>
  {
    private readonly string _folderPath;

    public JsonDatabase(string folderPath)
    {
      _folderPath = folderPath;
    }

    public void Save(string fileName, TObject @object)
    {
      var filePath = Path.Combine(_folderPath, fileName);
      var stringObject = JsonSerializer.Serialize(@object);
      File.WriteAllText(filePath, stringObject);
    }

    public TObject Load(string fileName)
    {
      var filePath = Path.Combine(_folderPath, fileName);
      var stringObject = File.ReadAllText(filePath);
      var @object = JsonSerializer.Deserialize<TObject>(stringObject);
      if (@object == null)
        throw new Exception($"Failed to serialize object for {fileName}");

      return @object;
    }
  }
}
