using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Teams
{
  public static class TeamRatingCalculator
  {
    // Hitting
    private const double LINEUP_WEIGHT = .95;
    private const double BENCH_WEIGHT = .05;

    private const double H_HIGHEST_RATING = 85.25995593; // By this weighted average, the New York Yankees have the hight rating in the game at ~85.26
    private const double H_AVERAGE_RATING = 83.25333071; // By this weighted average, the average team has a rating of ~83.25
    private const double H_DESIRED_HIGHEST_RATING = 90; // In all of baseball history, I'll give the Yankees '06 offense a 90
    private const double H_DESIRED_AVERAGE_RATING = 80; // C average
    private static readonly Func<double, double> HittingLinearGradient = MathUtils.BuildLinearGradientFunction(
      max: H_HIGHEST_RATING,
      desiredMax: H_DESIRED_HIGHEST_RATING,
      avg: H_AVERAGE_RATING,
      desiredAverage: H_DESIRED_AVERAGE_RATING
    );

    // Pitching
    private const double BEST_5_WEIGHT = .75;
    private const double NEXT_3_WEIGHT = .20;
    private const double REMAINING_WEIGHT = .05;

    private const double P_HIGHEST_RATING = 87.79165814; // By this weighted average, the New York Yankees have the hight rating in the game at ~87.79
    private const double P_AVERAGE_RATING = 84.24676478; // By this weighted average, the average team has a rating of ~84.24
    private const double P_DESIRED_HIGHEST_RATING = 90; // In all of baseball history, I'll give the Yankees '06 pitching a 90
    private const double P_DESIRED_AVERAGE_RATING = 80; // C average
    private static readonly Func<double, double> PitchingLinearGradient = MathUtils.BuildLinearGradientFunction(
      max: P_HIGHEST_RATING,
      desiredMax: P_DESIRED_HIGHEST_RATING,
      avg: P_AVERAGE_RATING,
      desiredAverage: P_DESIRED_AVERAGE_RATING
    );

    private const double RATING_CAP = 99; // Cap ratings at 99

    public static double CalculateOverallRating(TeamRatingParameters parameters)
    {
      var totalRating = CalculateHittingRating(parameters.HitterRatings) + CalculatePitchingRating(parameters.PitcherRatings);
      return totalRating / 2;
    }

    public static double CalculateHittingRating(IEnumerable<double> hitterRatings)
    {
      var orderedHitters = hitterRatings.OrderByDescending(r => r);
      var lineupAverage = orderedHitters.Take(9).Average();
      var best2FromBenchAverage = orderedHitters.Skip(9).Take(2).Average();

      var weightedAverage = lineupAverage * LINEUP_WEIGHT + best2FromBenchAverage * BENCH_WEIGHT; 
      
      var gradientRating = HittingLinearGradient(weightedAverage);
      return gradientRating.CapAt(RATING_CAP);
    }

    public static double CalculatePitchingRating(IEnumerable<double> pitcherRatings)
    {
      var orderedPitchers = pitcherRatings.OrderByDescending(r => r);
      var best5Average = orderedPitchers.Take(5).Average();

      var next3 = orderedPitchers.Skip(5).Take(3);
      var next3Average = next3.Any()
        ? next3.Average()
        : 0;

      // If none, just use average from last set. In other words, not penalizing a team for not having more than 8 pitchers
      var remaining = orderedPitchers.Skip(8);
      var remainingAverage = remaining.Any()
        ? remaining.Average()
        : next3Average;

      var weightedAverage = best5Average * BEST_5_WEIGHT
        + next3Average * NEXT_3_WEIGHT
        + remainingAverage * REMAINING_WEIGHT;

      var gradientRating = PitchingLinearGradient(weightedAverage);
      return gradientRating.CapAt(RATING_CAP);
    }

  }

  public class TeamRatingParameters
  {
    public IEnumerable<double> HitterRatings { get; set; } = Enumerable.Empty<double>(); 
    public IEnumerable<double> PitcherRatings { get; set; } = Enumerable.Empty<double>();
  }
}
