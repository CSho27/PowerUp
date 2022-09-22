using System;

namespace PowerUp
{
  public static class DateExtensions
  {
    public static int YearsElapsedSince(this DateTime firstDate, DateTime secondDate) => 
      firstDate.DayOfYear >= secondDate.DayOfYear
        ? firstDate.Year - secondDate.Year
        : firstDate.Year - secondDate.Year - 1;

    public static DateTime GetDateNYearsBefore(this DateTime date, int month, int day, int yearsBefore)
    {
      if (date.Month > month && date.Day > day)
        return new DateTime(date.Year-yearsBefore, month, day);
      else
        return new DateTime(date.Year-yearsBefore-1, month, day);
    } 

  }
}
