using System.Collections.Generic;

namespace PowerUp
{
  public static class ListExtensions
  {
    public static TSource? RemoveFirstOrDefault<TSource>(this IList<TSource> source)
    {
      if(source.Count == 0)
        return default(TSource);

      var value = source[0];
      source.RemoveAt(0);
      return value;
    }
  }
}
