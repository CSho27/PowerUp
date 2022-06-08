using NUnit.Framework;
using Shouldly;
using System.Linq;

namespace PowerUp.Tests.Utils
{
  public class SetUtilsTests
  {
    [Test] 
    public void GetDiff_ShouldGetCorrectDiff()
    {
      var listA = new[] { 1, 2, 3, 4, 5 };
      var listB = new[] { 2, 4, 6, 8, 10 };

      var result = SetUtils.GetDiff(listA, listB, (a, b) => a == b);

      result.ANotInB.Count().ShouldBe(3);
      result.ANotInB.ShouldContain(1);
      result.ANotInB.ShouldContain(3);
      result.ANotInB.ShouldContain(5);

      result.BNotInA.Count().ShouldBe(3);
      result.BNotInA.ShouldContain(6);
      result.BNotInA.ShouldContain(8);
      result.BNotInA.ShouldContain(10);

      result.Matches.Count().ShouldBe(2);
      result.Matches.ShouldContain(match => match.a == 2 && match.b == 2);
      result.Matches.ShouldContain(match => match.a == 4 && match.b == 4);
    }
  }
}
