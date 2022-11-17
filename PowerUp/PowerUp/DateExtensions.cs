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
      var year = date.Month > month || (date.Month == month && date.Day > day)
        ? date.Year - yearsBefore
        : date.Year - yearsBefore - 1;

      try
      {
        return new DateTime(year, month, day);
      }
      catch (ArgumentOutOfRangeException e)
      {
        if (month == 2 && day > 28)
          return new DateTime(year, 2, 28);
        else
          throw e;
      }
    } 

  }
}
