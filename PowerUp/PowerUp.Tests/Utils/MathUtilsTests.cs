using NUnit.Framework;
using Shouldly;
using System;

namespace PowerUp.Tests.Utils
{
  public class MathUtilsTests
  {
    [Test]
    public void PiecewiseFunctionFor_ThrowsIfZeroCoordinates()
    {
      Assert.Throws<InvalidOperationException>(() =>
      {
        var piecewiseFunction = MathUtils.PiecewiseFunctionFor(new (double x, double y)[] { });
        piecewiseFunction(0);
      });
    }

    [Test]
    public void PiecewiseFunctionFor_ThrowsIfOneCoordinate()
    {
      Assert.Throws<InvalidOperationException>(() =>
      {
        var piecewiseFunction = MathUtils.PiecewiseFunctionFor(new[] { (0.0, 0.0) });
        piecewiseFunction(0);
      });
    }

    [Test]
    public void PiecewiseFunctionFor_ThrowsIfTwoCoordinatesHaveSameX()
    {
      var coordinates = new (double x, double y)[]
      {
        (0, 0),
        (0, 1)
      };

      var piecewiseFunction = MathUtils.PiecewiseFunctionFor(coordinates);
      Assert.Throws<InvalidOperationException>(() =>
      {
        var result = piecewiseFunction(0);
      });
    }

    [Test]
    public void PiecewiseFunctionFor_BuildsPiecewiseFunctionForTwoCoordinates()
    {
      var coordinates = new (double x, double y)[]
      {
        (0, 0),
        (1, 1)
      };

      var piecewiseFunction = MathUtils.PiecewiseFunctionFor(coordinates);
      var result = piecewiseFunction(0);
      result.ShouldBe(0);
    }

    [Test]
    public void PiecewiseFunctionFor_ShouldFollowLinePastFirstPoint()
    {
      var coordinates = new (double x, double y)[]
      {
        (0, 0),
        (1, 1)
      };

      var piecewiseFunction = MathUtils.PiecewiseFunctionFor(coordinates);
      var result = piecewiseFunction(-1);
      result.ShouldBe(-1);
    }

    [Test]
    public void PiecewiseFunctionFor_ShouldFollowLinePastLastPoint()
    {
      var coordinates = new (double x, double y)[]
      {
        (0, 0),
        (1, 1)
      };

      var piecewiseFunction = MathUtils.PiecewiseFunctionFor(coordinates);
      var result = piecewiseFunction(2);
      result.ShouldBe(2);
    }

    [Test]
    public void PiecewiseFunctionFor_BuildsPiecewiseFunctionForThreeCoordinates()
    {
      var coordinates = new (double x, double y)[]
      {
        (0, 0),
        (1, 1),
        (2, 1)
      };

      var piecewiseFunction = MathUtils.PiecewiseFunctionFor(coordinates);
      piecewiseFunction(1.5).ShouldBe(1);
      piecewiseFunction(3).ShouldBe(1);
    }

    [Test]
    public void PiecewiseFunctionFor_BuildsPiecewiseFunctionForMultipleCoordinates()
    {
      var coordinates = new (double x, double y)[]
      {
        (0, 0),
        (1, 1),
        (2, 1),
        (3, 3),
        (4, 2),
        (5, 1.5),
        (6, 2.5)
      };

      var piecewiseFunction = MathUtils.PiecewiseFunctionFor(coordinates);
      piecewiseFunction(-1).ShouldBe(-1);
      piecewiseFunction(-.5).ShouldBe(-.5);
      piecewiseFunction(0).ShouldBe(0);
      piecewiseFunction(0.5).ShouldBe(0.5);
      piecewiseFunction(1).ShouldBe(1);
      piecewiseFunction(1.5).ShouldBe(1);
      piecewiseFunction(2).ShouldBe(1);
      piecewiseFunction(2.5).ShouldBe(2);
      piecewiseFunction(3).ShouldBe(3);
      piecewiseFunction(3.5).ShouldBe(2.5);
      piecewiseFunction(4).ShouldBe(2);
      piecewiseFunction(4.5).ShouldBe(1.75);
      piecewiseFunction(5).ShouldBe(1.5);
      piecewiseFunction(5.5).ShouldBe(2);
      piecewiseFunction(6).ShouldBe(2.5);
      piecewiseFunction(6.5).ShouldBe(3);
      piecewiseFunction(7).ShouldBe(3.5);
    }

    [Test]
    public void PiecewiseFunctionFor_BuildsPiecewiseFunctionCorrectlyWhenCoordinatesOutOfOrder()
    {
      var coordinates = new (double x, double y)[]
      {
        (2, 1),
        (6, 2.5),
        (3, 3),
        (1, 1),
        (5, 1.5),
        (4, 2),
        (0, 0)
      };

      var piecewiseFunction = MathUtils.PiecewiseFunctionFor(coordinates);
      piecewiseFunction(-1).ShouldBe(-1);
      piecewiseFunction(-.5).ShouldBe(-.5);
      piecewiseFunction(0).ShouldBe(0);
      piecewiseFunction(0.5).ShouldBe(0.5);
      piecewiseFunction(1).ShouldBe(1);
      piecewiseFunction(1.5).ShouldBe(1);
      piecewiseFunction(2).ShouldBe(1);
      piecewiseFunction(2.5).ShouldBe(2);
      piecewiseFunction(3).ShouldBe(3);
      piecewiseFunction(3.5).ShouldBe(2.5);
      piecewiseFunction(4).ShouldBe(2);
      piecewiseFunction(4.5).ShouldBe(1.75);
      piecewiseFunction(5).ShouldBe(1.5);
      piecewiseFunction(5.5).ShouldBe(2);
      piecewiseFunction(6).ShouldBe(2.5);
      piecewiseFunction(6.5).ShouldBe(3);
      piecewiseFunction(7).ShouldBe(3.5);
    }
  }
}
