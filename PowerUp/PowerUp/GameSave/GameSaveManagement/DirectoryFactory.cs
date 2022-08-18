using System.IO;
using System.Linq;

namespace PowerUp.GameSave.GameSaveManagement
{
  public static class DirectoryFactory
  {
    public static void CreateDirectoriesForPathIfNeeded(string directoryPath)
    {
      var pathParts = Path.GetFullPath(directoryPath).Split(Path.PathSeparator);
      var path = pathParts.FirstOrDefault() ?? "";
      foreach(var part in pathParts)
      {
        path = Path.Combine(path, part);
        if(!Directory.Exists(path))
          Directory.CreateDirectory(path);
      }
    }
  }
}
