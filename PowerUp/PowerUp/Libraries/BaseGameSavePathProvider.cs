using System.IO;

namespace PowerUp.Libraries
{
  public interface IBaseGameSavePathProvider
  {
    string GetPath();
  }

  internal class BaseGameSavePathProvider : IBaseGameSavePathProvider
  {
    private readonly string _path;

    public BaseGameSavePathProvider(string gameSavePath)
    {
      _path = gameSavePath;
    }

    public string GetPath() => _path;
  }
}
