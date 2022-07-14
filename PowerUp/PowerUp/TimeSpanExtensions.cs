using System;

namespace PowerUp
{
  public static class TimeSpanExtensions
  {
    public static string ToDisplayString(this TimeSpan timeSpan) => $"{timeSpan.Hours}hr {timeSpan.Minutes}min {timeSpan.Seconds}sec";
    public static string ToDisplayString(this TimeSpan? timeSpan)
    {
      if (!timeSpan.HasValue)
        return "";

      return timeSpan!.Value.ToDisplayString();
    }
  }
}
