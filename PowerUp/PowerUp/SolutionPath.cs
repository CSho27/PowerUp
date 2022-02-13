using System;
using System.IO;
using System.Linq;

namespace PowerUp
{
  public static class SolutionPath
  {
    public static string Root() => FullPathCombine(Environment.CurrentDirectory, DirsUp(4));
    public static string Relative(string relativePath) => FullPathCombine(Root(), relativePath);

    private static string FullPathCombine(string path1, string path2)
      => Path.GetFullPath(Path.Combine(path1, path2));
    private static string DirsUp(int number) => string.Concat(Enumerable.Repeat("../", 4));
  }
}
