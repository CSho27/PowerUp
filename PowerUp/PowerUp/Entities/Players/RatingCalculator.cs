using System;

namespace PowerUp.Entities.Players
{
  public static class RatingCalculator
  {
    private const double NORMALIZE_255_15 = 255 / 15.0; // 17
    private const double NORMALIZE_255_7 = 255 / 7.0;
    private const double NORMALIZE_255_3 = 255 / 3.0;

    // Hitter Abilities
    private const double CON_WEIGHT = 0.3;
    private const double POW_WEIGHT = 0.5;
    private const double SPD_WEIGHT = 0.1;
    private const double ARM_WEIGHT = 0.05;
    private const double FLD_WEIGHT = 0.025;
    private const double ERES_WEIGHT = 0.025;

    private const double H_HIGHEST_RATING = 85.87254902; // By this weighted average, Albert Pujols has the hight rating int the game at ~85.87
    private const double H_AVERAGE_RATING = 51.70664926; // By this weighted average, the average player has a rating of ~51.7
    private const double H_DESIRED_HIGHEST_RATING = 95; // In all of baseball history, I'll give Pujols '06 season a 95
    private const double H_DESIRED_AVERAGE_RATING = 80; // C average
    private static readonly Func<double, double> HittingLinearGradient = MathUtils.BuildLinearGradientFunction(
      max: H_HIGHEST_RATING,
      desiredMax: H_DESIRED_HIGHEST_RATING,
      avg: H_AVERAGE_RATING,
      desiredAverage: H_DESIRED_AVERAGE_RATING
    );

    // Pitcher Ability Weights
    private const double TOP_SPD_WEIGHT = 0.6;
    private const double CTRL_WEIGHT = 0.16;
    private const double STAM_WEIGHT = 0.1;
    private const double _2SFB_WEIGHT = 0.04;
    private const double SLD_WEIGHT = 0.04;
    private const double CRV_WEIGHT = 0.005;
    private const double FRK_WEIGHT = 0.02;
    private const double SNK_WEIGHT = 0.005;
    private const double SNKFB_WEIGHT = 0.03;

    private static readonly Func<double, double> TopSpeedLinearGradient = MathUtils.BuildLinearGradientFunction(
      max: 105,
      desiredMax: 255,
      avg: 93.67451994,
      desiredAverage: 225
    );

    private const double P_HIGHEST_RATING = 76.92430538; // By this weighted average, Albert Pujols has the hight rating int the game at ~85.87
    private const double P_AVERAGE_RATING = 67.36310524; // By this weighted average, the average player has a rating of ~51.7
    private const double P_DESIRED_HIGHEST_RATING = 93; // The best pitchers from 06 earn about a 93 in history in my mind
    private const double P_DESIRED_AVERAGE_RATING = 80; // C average
    private static readonly Func<double, double> PitchingLinearGradient = MathUtils.BuildLinearGradientFunction(
      max: P_HIGHEST_RATING,
      desiredMax: P_DESIRED_HIGHEST_RATING,
      avg: P_AVERAGE_RATING,
      desiredAverage: P_DESIRED_AVERAGE_RATING
    );

    private const double MAX_AT_100_from255 = 100.0 / 255; // Multiply by this value to make the max rating 100
    private const double RATING_CAP = 99; // Cap ratings at 99

    public static double CalculateHitterRating(HitterRatingParameters parameters)
    {
      // Caclulate weighted average
      var weightedAverage_255 = parameters.Contact * CON_WEIGHT * NORMALIZE_255_15
        + parameters.Power * POW_WEIGHT
        + parameters.RunSpeed * SPD_WEIGHT * NORMALIZE_255_15
        + parameters.ArmStrength * ARM_WEIGHT * NORMALIZE_255_15
        + parameters.Fielding * FLD_WEIGHT * NORMALIZE_255_15
        + parameters.ErrorResistance * ERES_WEIGHT * NORMALIZE_255_15;

      // Take our weighted maximum out off 255 and make it out of 100;
      var weightedAverage_100 = weightedAverage_255 * MAX_AT_100_from255;
      var gradientRating = HittingLinearGradient(weightedAverage_100);

      return gradientRating.CapAt(RATING_CAP);
    }

    public static double CalculatePitcherRating(PitcherRatingParamters parameters)
    {
      var weightedAverage_255 = TopSpeedLinearGradient(parameters.TopSpeedMph) * TOP_SPD_WEIGHT
        + parameters.Control * CTRL_WEIGHT
        + parameters.Stamina * STAM_WEIGHT
        + parameters.TwoSeamMovement * _2SFB_WEIGHT * NORMALIZE_255_3
        + parameters.SliderMovement * SLD_WEIGHT * NORMALIZE_255_7
        + parameters.CurveMovement * CRV_WEIGHT * NORMALIZE_255_7
        + parameters.ForkMovement * FRK_WEIGHT * NORMALIZE_255_7
        + parameters.SinkerMovement * SNK_WEIGHT * NORMALIZE_255_7
        + parameters.SinkingFastballMovement * SNKFB_WEIGHT * NORMALIZE_255_7;

      // Take our weighted maximum out off 255 and make it out of 100;
      var weightedAverage_100 = weightedAverage_255 * MAX_AT_100_from255;
      var gradientRating = PitchingLinearGradient(weightedAverage_100); 

      return gradientRating.CapAt(RATING_CAP);
    }
  }

  public class HitterRatingParameters
  {
    public int Contact { get; set; }
    public int Power { get; set; }
    public int RunSpeed { get; set; }
    public int ArmStrength { get; set; }
    public int Fielding { get; set; }
    public int ErrorResistance { get; set; }
  }

  public class PitcherRatingParamters
  {
    public double TopSpeedMph { get; set; }
    public int Control { get; set; }
    public int Stamina { get; set; }
    public int TwoSeamMovement { get; set; }
    public int SliderMovement { get; set; }
    public int CurveMovement { get; set; }
    public int ForkMovement { get; set; }
    public int SinkerMovement { get; set; }
    public int SinkingFastballMovement { get; set; }
  }
}
