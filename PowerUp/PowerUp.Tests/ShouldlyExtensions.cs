using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Tests
{
  public static class ShouldlyExtensions
  {
    public static void ShouldBeWithTolerance(this double? value, double expected, double tolerance)
    {
      value.ShouldNotBeNull();
      value!.Value.ShouldBe(expected, new EqualityComparerWithTolerance(tolerance));
    }

    public class EqualityComparerWithTolerance : IEqualityComparer<double>
    {
      private readonly double _tolerance;

      public EqualityComparerWithTolerance(double tolerance)
      {
        _tolerance = tolerance;
      }

      public bool Equals(double x, double y) => Math.Abs(x - y) < _tolerance;
      public int GetHashCode([DisallowNull] double obj) => obj.GetHashCode();
    }
  }
}
