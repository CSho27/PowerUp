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
  }
}
