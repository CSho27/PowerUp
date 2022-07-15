using NUnit.Framework;
using Shouldly;

namespace PowerUp.Tests.Utils
{
  public class EnumerableExtensionsTests
  {
    [Test]
    public void CombineAverages_CombinesTwoWeightedAverage()
    {
      var numbers = new[] { (.100, 1), (.400, 2) };
      var combinedAverage = numbers.CombineAverages(n => n.Item1, n => n.Item2);
      combinedAverage.ShouldBe(.300);
    }

    [Test]
    public void CombineAverages_CombinesThreeWeightedAverage()
    {
      var numbers = new[] { (.100, 1), (.400, 2), (.200, 1) };
      var combinedAverage = numbers.CombineAverages(n => n.Item1, n => n.Item2);
      combinedAverage.ShouldBe(.275);
    }
  }
}
