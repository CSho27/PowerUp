﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp
{
  public static class EnumerableExtensions
  {
    public static int? SumOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
      if (source.Select(selector).All(v => !v.HasValue))
        return null;
      else
        return source.Sum(selector);
    }

    public static double? SumOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    {
      if (source.Select(selector).All(v => !v.HasValue))
        return null;
      else
        return source.Sum(selector);
    }

    public static int SumOrNullProrated<TSource>(this IEnumerable<TSource>? source, Func<TSource, int?> selector, double prorateBy)
    {
      return (source?.SumOrNull(selector) ?? 0 * prorateBy).Round();
    }

    public static double SumOrNullProrated<TSource>(this IEnumerable<TSource>? source, Func<TSource, double?> selector, double prorateBy)
    {
      return (source?.SumOrNull(selector) ?? 0 * prorateBy).Round();
    }

    public static double? CombineAverages<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> averageSelector, Func<TSource, double?> weightSelector)
    {
      if (source.Select(averageSelector).Any(v => !v.HasValue))
        return null;

      var average = source.Aggregate<TSource, double>(0, (acc, current) => AggregateAverage(acc, current, averageSelector, weightSelector));
      var sum = source.Sum(weightSelector);

      return average / sum;
    }

    public static TSource? MaxOrDefault<TSource>(this IEnumerable<TSource> source)
    {
      return source.Any()
        ? source.Max()
        : default(TSource);
    }

    private static double AggregateAverage<TSource>(double average, TSource source, Func<TSource, double?> averageSelector, Func<TSource, double?> weightSelector)
    {
      var weight = weightSelector(source)!.Value;
      var averageToAdd = weight * averageSelector(source)!.Value;
      return average + averageToAdd;
    }

    public static TSource Percentile<TSource, TOrder>(this IEnumerable<TSource> source, Func<TSource, TOrder> selector, double percentile)
    {
      var sortedData = source.OrderBy(selector).ToArray();
      int index = (int)Math.Ceiling(percentile * sortedData.Length) - 1;
      index = Math.Max(0, Math.Min(index, sortedData.Length - 1));
      return sortedData[index];
    }

    public static TSource? FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> elementMatches, out int? index)
    {
      var enumerator = source.GetEnumerator();
      var i = 0;
      while (enumerator.MoveNext())
      {
        if (elementMatches(enumerator.Current))
        {
          index = i;
          return enumerator.Current;
        }

        i++;
      }

      index = null;
      return default;
    }

    public static string StringJoin<TSource>(this IEnumerable<TSource> source, string separator = "")
    {
      return string.Join(separator, source);
    }

    public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> fn)
    {
      var results = new List<TResult>();
      var tasks = source.Select(fn);
      foreach (var task in tasks)
        results.Add(await task);
      return results;
    }
  }
}
