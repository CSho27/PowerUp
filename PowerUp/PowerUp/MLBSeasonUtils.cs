using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp
{
  public class MLBSeasonUtils
  {
    public const int ESTIMATED_FIRST_MONTH_OF_SEASON = 4;
    public const int ESTIMATED_LAST_MONTH_OF_SEASON = 10;

    public static DateTime ESTIMATED_START_OF_CURRENT_SEASON => new DateTime(DateTime.Today.Year, ESTIMATED_FIRST_MONTH_OF_SEASON, 1);
    public static DateTime ESTIMATED_END_OF_CURRENT_SEASON => new DateTime(DateTime.Today.Year, ESTIMATED_LAST_MONTH_OF_SEASON, 1);
    public static TimeSpan ESTIMATED_LENGTH_OF_SEASON => ESTIMATED_END_OF_CURRENT_SEASON - ESTIMATED_START_OF_CURRENT_SEASON;
    public static TimeSpan ESTIMATED_PORTION_OF_SEASON_PLAYED => DateTime.Today - ESTIMATED_START_OF_CURRENT_SEASON;

    public static DateTime GetEstimatedStartOfSeason(int year) => new DateTime(year, ESTIMATED_FIRST_MONTH_OF_SEASON, 1);

    public static double GetFractionOfSeasonPlayed(int year) => DateTime.Today.Year == year
      ? GetFractionOfCurrentSeasonPlayed()
      : 1;
    public static double GetFractionOfCurrentSeasonPlayed() => ESTIMATED_PORTION_OF_SEASON_PLAYED / ESTIMATED_LENGTH_OF_SEASON;
  }
}
