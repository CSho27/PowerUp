using NUnit.Framework;
using Shouldly;
using System.IO;

namespace PowerUp.Tests
{
  public class SolutionPathTests
  {
    [Test] 
    public void Root_ShouldGetTheRootOfTheSolution()
    {
      var projectRoot = SolutionPath.Root();
      projectRoot.ShouldBePath("C:/dev/PowerUp/PowerUp");
    }

    [Test]
    public void Relative_ShouldEvaluateRelativePath()
    {
      var path = SolutionPath.Relative("./Players/Cleveland/2003/TravisHafner");
      path.ShouldBePath("C:/dev/PowerUp/PowerUp/Players/Cleveland/2003/TravisHafner");
    }
  }

  public static class ShouldlyPathExtensions
  {
    public static void ShouldBePath(this string path1, string path2)
      => Path.GetFullPath(path1.TrimSlashes()).ShouldBe(Path.GetFullPath(path2.TrimSlashes()));

    private static string TrimSlashes(this string path) => path.TrimEnd('/', '\\');
  }
}
