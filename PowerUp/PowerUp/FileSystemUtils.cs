using System.IO;

namespace PowerUp
{
  public static class FileSystemUtils
  {
    public static void CopyDirectoryRecursively(string sourcePath, string targetPath)
    {
      var source = Directory.CreateDirectory(sourcePath);
      var target = Directory.CreateDirectory(targetPath);
      foreach (FileInfo file in source.GetFiles())
        file.CopyTo(Path.Combine(target.FullName, file.Name), true);

      foreach (DirectoryInfo sourceSubDirectory in source.GetDirectories())
      {
        DirectoryInfo targetSubDirectory = target.CreateSubdirectory(sourceSubDirectory.Name);
        CopyDirectoryRecursively(sourceSubDirectory.FullName, targetSubDirectory.FullName);
      }
    }
  }
}
