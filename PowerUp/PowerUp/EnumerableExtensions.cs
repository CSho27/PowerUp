using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp
{
  public static class EnumerableExtensions
  {
    public static int? SumOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
      if (source.Select(selector).Any(v => !v.HasValue))
        return null;
      else
        return source.Sum(selector);
    }

    public static double? CombineAverages<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> averageSelector, Func<TSource, int?> weightSelector)
    {
      if (source.Select(averageSelector).Any(v => !v.HasValue))
        return null;

      var average = source.Aggregate<TSource, double>(0, (acc, current) => AggregateAverage(acc, current, averageSelector, weightSelector));
      var sum = source.Sum(weightSelector);

      return average / sum;
    }

    private static double AggregateAverage<TSource>(double average, TSource source, Func<TSource, double?> averageSelector, Func<TSource, int?> weightSelector)
    {
      var weight = weightSelector(source)!.Value;
      var averageToAdd = weight * averageSelector(source)!.Value;
      return average + averageToAdd;
    }
  }
}
