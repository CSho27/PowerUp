using System;

namespace PowerUp
{
  public static class DateExtensions
  {
    public static int YearsElapsedSince(this DateTime firstDate, DateTime secondDate) => 
      firstDate.DayOfYear >= secondDate.DayOfYear
        ? firstDate.Year - secondDate.Year
        : firstDate.Year - secondDate.Year - 1;
  }
}
