using System;
using System.Text;

namespace PowerUp
{
  public static class TimeSpanExtensions
  {
    public static string ToDisplayString(this TimeSpan timeSpan)
    {
      var sb = new StringBuilder();
      if(timeSpan > TimeSpan.FromHours(1))
        sb.Append($"{timeSpan.Hours}hr");
      if (timeSpan > TimeSpan.FromMinutes(1))
        sb.Append($"{timeSpan.Minutes}min");

      sb.Append($"{timeSpan.Seconds}sec");
      return sb.ToString();
    }

    public static string ToDisplayString(this TimeSpan? timeSpan)
    {
      if (!timeSpan.HasValue)
        return "";

      return timeSpan!.Value.ToDisplayString();
    }
  }
}
