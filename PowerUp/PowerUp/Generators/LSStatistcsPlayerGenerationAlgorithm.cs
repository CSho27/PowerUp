using PowerUp.Entities.Players;
using PowerUp.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Generators
{
  public class LSStatistcsPlayerGenerationAlgorithm : PlayerGenerationAlgorithm
  {
    public override HashSet<PlayerGenerationDataset> DatasetDependencies => new HashSet<PlayerGenerationDataset>() 
    { 
      PlayerGenerationDataset.LSPlayerInfo,
      PlayerGenerationDataset.LSHittingStats,
      PlayerGenerationDataset.LSFieldingStats,
      PlayerGenerationDataset.LSPitchingStats,
      PlayerGenerationDataset.BaseballReferenceIdDataset
    };

    public LSStatistcsPlayerGenerationAlgorithm(IVoiceLibrary voiceLibrary, ISkinColorGuesser skinColorGuesser) 
    {
      // Player Info
      SetProperty("FirstName", (player, data) => player.FirstName = data.PlayerInfo!.FirstNameUsed.ShortenNameToLength(14));
      SetProperty("LastName", (player, data) => player.LastName = data.PlayerInfo!.LastName.ShortenNameToLength(14));
      SetProperty(new SavedName());
      SetProperty(new UniformNumber());
      SetProperty("PrimaryPosition", (player, data) => player.PrimaryPosition = data.PrimaryPosition);
      SetProperty(new PitcherTypeSetter());
      SetProperty("VoiceId", (player, data) => player.VoiceId = voiceLibrary.FindClosestTo(data.PlayerInfo!.FirstNameUsed, data.PlayerInfo!.LastName).Key);
      SetProperty("BattingSide", (player, data) => player.BattingSide = data.PlayerInfo!.BattingSide);
      SetProperty("ThrowingArm", (player, data) => player.ThrowingArm = data.PlayerInfo!.ThrowingArm);
      SetProperty("GeneratedPlayer_FullFirstName", (player, data) => player.GeneratedPlayer_FullFirstName = data.PlayerInfo!.FirstNameUsed);
      SetProperty("GeneratedPlayer_FullLastName", (player, data) => player.GeneratedPlayer_FullLastName = data.PlayerInfo!.LastName);
      SetProperty("GeneratedPlayer_ProDebutDate", (player, data) => player.GeneratedPlayer_ProDebutDate = data.PlayerInfo!.ProDebutDate);
      SetProperty(new AgeSetter());
      SetProperty(new BirthMonthSetter());
      SetProperty(new BirthDaySetter());

      // Appearance
      SetProperty(new SkinColorSetter(skinColorGuesser));

      // Position Capabilities
      SetProperty(new PitcherCapabilitySetter());
      SetProperty(new CatcherCapabilitySetter());
      SetProperty(new FirstBaseCapabilitySetter());
      SetProperty(new SecondBaseCapabilitySetter());
      SetProperty(new ThirdBaseCapabilitySetter());
      SetProperty(new ShortstopCapabilitySetter());
      SetProperty(new LeftFieldCapabilitySetter());
      SetProperty(new CenterFieldCapabilitySetter());
      SetProperty(new RightFieldCapabilitySetter());

      // Hitter Abilities
      SetProperty(new TrajectorySetter());
      SetProperty(new ContactSetter());
      SetProperty(new PowerSetter());
      SetProperty(new RunSpeedSetter());
      SetProperty(new ArmStrengthSetter());
      SetProperty(new FieldingSetter());
      SetProperty(new ErrorResistanceSetter());

      // Pitcher Abilities
      SetProperty(new ControlSetter());
      SetProperty(new StaminaSetter());
      SetProperty(new TopSpeedSetter());
      SetProperty(new PitchArsenalSetter());

      // TODO: Do Special Abilities
    }

    public class SavedName : PlayerPropertySetter
    {
      public override string PropertyKey => "SavedName";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        var firstLetterOfFirstName = datasetCollection.PlayerInfo!.FirstNameUsed.FirstCharacter();
        var lastName = datasetCollection.PlayerInfo!.LastName;

        var firstDotLast = $"{firstLetterOfFirstName}.{lastName}";
        if(firstDotLast.Length <= 10)
        {
          player.SavedName = firstDotLast;
          return true;
        }
        
        player.SavedName = lastName.ShortenNameToLength(10);
        return true;
      }
    }

    public class UniformNumber : PlayerPropertySetter
    {
      public override string PropertyKey => "UniformNumber";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        var uniformNumber = datasetCollection.PlayerInfo!.UniformNumber;
        if(uniformNumber == null || !int.TryParse(uniformNumber, out var _))
          return false;

        player.UniformNumber = uniformNumber;
        return true;
      }
    }

    public class PitcherTypeSetter : PlayerPropertySetter
    {
      public override string PropertyKey => "PitcherRole";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        if (datasetCollection.PitchingStats == null)
        {
          if(datasetCollection.PrimaryPosition == Position.Pitcher)
            player.GeneratorWarnings.Add(GeneratorWarning.NoPitchingStats(PropertyKey));
          return false;
        }

        if (datasetCollection.PitchingStats.GamesStarted > 10)
          player.PitcherType = PitcherType.Starter;
        else if (datasetCollection.PitchingStats.SaveOpportunities > 20)
          player.PitcherType = PitcherType.Closer;
        else if(datasetCollection.PitchingStats.SaveOpportunities == null && datasetCollection.PitchingStats.GamesFinished > 30)
          player.PitcherType = PitcherType.Closer;
        else
          player.PitcherType = PitcherType.Reliever;

        return true;
      }
    }

    public class AgeSetter : PlayerPropertySetter
    {
      public override string PropertyKey => "Age";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        if (!datasetCollection.PlayerInfo!.BirthDate.HasValue)
        {
          player.GeneratorWarnings.Add(GeneratorWarning.NoBirthDate(PropertyKey));
          return false;
        }

        player.Age = MLBSeasonUtils.GetEstimatedStartOfSeason(datasetCollection.Year).YearsElapsedSince(datasetCollection.PlayerInfo.BirthDate.Value);
        return true;
      }
    }

    public class BirthMonthSetter : PlayerPropertySetter
    {
      public override string PropertyKey => "BirthMonth";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        if (!datasetCollection.PlayerInfo!.BirthDate.HasValue)
        {
          player.GeneratorWarnings.Add(GeneratorWarning.NoBirthDate(PropertyKey));
          return false;
        }

        player.BirthMonth = datasetCollection.PlayerInfo.BirthDate.Value.Month;
        return true;
      }
    }

    public class BirthDaySetter : PlayerPropertySetter
    {
      public override string PropertyKey => "BirthDay";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        if (!datasetCollection.PlayerInfo!.BirthDate.HasValue)
        {
          player.GeneratorWarnings.Add(GeneratorWarning.NoBirthDate(PropertyKey));
          return false;
        }

        player.BirthDay = datasetCollection.PlayerInfo.BirthDate.Value.Day;
        return true;
      }
    }

    public class SkinColorSetter : PlayerPropertySetter
    {
      private readonly ISkinColorGuesser _skinColorGuesser;

      public override string PropertyKey => "Appearance_SkinColor";


      public SkinColorSetter(ISkinColorGuesser skinColorGuesser)
      {
        _skinColorGuesser = skinColorGuesser;
      }

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        var skinColor = _skinColorGuesser.GuessSkinColor(datasetCollection.Year, datasetCollection.PlayerInfo!.BirthCountry);
        player.Appearance.SkinColor = skinColor;
        return true;
      }
    }

    public abstract class PositionCapabilitySetter : PlayerPropertySetter
    {
      protected Grade GetGradeForPosition(Position position, PlayerGenerationData datasetCollection)
      {
        var primaryPosition = datasetCollection.PrimaryPosition;
        if (position == primaryPosition)
          return Grade.A;

        LSFieldingStats? stats = null;
        datasetCollection.FieldingStats?.FieldingByPosition.TryGetValue(position, out stats);
        if (stats != null && stats.TotalChances > 75)
          return Grade.B;
        if (stats != null && stats.TotalChances > 50)
          return Grade.C;
        if (stats != null && stats.TotalChances > 25)
          return Grade.D;

        switch (position)
        {
          case Position.Pitcher:
            return Grade.G;
          case Position.Catcher:
            return Grade.G;
          case Position.FirstBase:
            return primaryPosition == Position.SecondBase || primaryPosition == Position.ThirdBase || primaryPosition == Position.Shortstop
              ? Grade.F
              : Grade.G;
          case Position.SecondBase:
            return primaryPosition == Position.ThirdBase || primaryPosition == Position.Shortstop
              ? Grade.E
              : primaryPosition == Position.FirstBase
                ? Grade.F
                : Grade.G;
          case Position.ThirdBase:
            return primaryPosition == Position.SecondBase || primaryPosition == Position.Shortstop
              ? Grade.E
              : primaryPosition == Position.FirstBase
                ? Grade.F
                : Grade.G;
          case Position.Shortstop:
            return primaryPosition == Position.SecondBase || primaryPosition == Position.ThirdBase
              ? Grade.E
              : primaryPosition == Position.FirstBase
                ? Grade.F
                : Grade.G;
          case Position.LeftField:
            return primaryPosition == Position.CenterField || primaryPosition == Position.RightField
              ? Grade.E
              : Grade.G;
          case Position.CenterField:
            return primaryPosition == Position.LeftField || primaryPosition == Position.RightField
              ? Grade.E
              : Grade.G;
          case Position.RightField:
            return primaryPosition == Position.LeftField || primaryPosition == Position.CenterField
              ? Grade.E
              : Grade.G;
          default:
            return Grade.G;
        }
      }
    }

    public class PitcherCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_Pitcher";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositionCapabilities.Pitcher = GetGradeForPosition(Position.Pitcher, datasetCollection);
        return true;
      }
    }

    public class CatcherCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_Catcher";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositionCapabilities.Catcher = GetGradeForPosition(Position.Catcher, datasetCollection);
        return true;
      }
    }

    public class FirstBaseCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_FirstBase";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositionCapabilities.FirstBase = GetGradeForPosition(Position.FirstBase, datasetCollection);
        return true;
      }
    }

    public class SecondBaseCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_SecondBase";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositionCapabilities.SecondBase = GetGradeForPosition(Position.SecondBase, datasetCollection);
        return true;
      }
    }


    public class ThirdBaseCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_ThirdBase";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositionCapabilities.ThirdBase = GetGradeForPosition(Position.ThirdBase, datasetCollection);
        return true;
      }
    }

    public class ShortstopCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_Shortstop";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositionCapabilities.Shortstop = GetGradeForPosition(Position.Shortstop, datasetCollection);
        return true;
      }
    }

    public class LeftFieldCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_LeftField";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositionCapabilities.LeftField = GetGradeForPosition(Position.LeftField, datasetCollection);
        return true;
      }
    }

    public class CenterFieldCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_CenterField";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositionCapabilities.CenterField = GetGradeForPosition(Position.CenterField, datasetCollection);
        return true;
      }
    }

    public class RightFieldCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_RightField";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositionCapabilities.RightField = GetGradeForPosition(Position.RightField, datasetCollection);
        return true;
      }
    }
  }

  public class TrajectorySetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_Trajcetory";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if(datasetCollection.HittingStats == null)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.NoHittingStats(PropertyKey));
        return false;
      }

      if (!datasetCollection.HittingStats.HomeRuns.HasValue)
      {

        return false;
      }

      player.HitterAbilities.Trajectory = GetTrajectoryForHomeRuns(datasetCollection.HittingStats.HomeRuns.Value);
      return true;
    }

    private int GetTrajectoryForHomeRuns(int homeRuns)
    {
      if (homeRuns < 2)
        return 1;
      else if (homeRuns < 14)
        return 2;
      else if (homeRuns < 35)
        return 3;
      else
        return 4;
    }
  }

  public class ContactSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_Contact";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if(datasetCollection.HittingStats == null)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.NoHittingStats(PropertyKey));
        return false;
      }

      if (datasetCollection.HittingStats.AtBats < 100 || !datasetCollection.HittingStats.BattingAverage.HasValue)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.InsufficientHittingStats(PropertyKey));
        return false;
      }

      player.HitterAbilities.Contact = GetContactForBattingAverage(datasetCollection.HittingStats.BattingAverage.Value);
      return true;
    }

    private int GetContactForBattingAverage(double battingAverage)
    {
      if (battingAverage < 0.15)
        return 1;
      else if (battingAverage < 0.175)
        return 2;
      else if (battingAverage < .2)
        return 3;
      else if (battingAverage < .23)
        return 4;
      else if (battingAverage < .24)
        return 5;
      else if (battingAverage < .25)
        return 6;
      else if (battingAverage < .263)
        return 7;
      else if (battingAverage < .282)
        return 8;
      else if (battingAverage < .295)
        return 9;
      else if (battingAverage < .31)
        return 10;
      else if (battingAverage < .32)
        return 11;
      else if (battingAverage < .335)
        return 12;
      else if (battingAverage < .345)
        return 13;
      else if (battingAverage < .35)
        return 14;
      else
        return 15;
    }
  }

  public class PowerSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_Power";
    private static readonly IEnumerable<(double atBatsPerHomerun, double power)> powerFunctionCoordinates = new (double x, double y)[]
    {
      (10.5, 250),
      (10.75, 245),
      (11, 240),
      (11.25, 235),
      (11.6, 230),
      (11.9, 225),
      (12.3, 220),
      (12.8, 215),
      (13.4, 210),
      (14, 205),
      (14.7, 200),
      (15.5, 195),
      (16.35, 190),
      (17.45, 185),
      (18.45, 180),
      (19.5, 175),
      (20.6, 170),
      (21.75, 165),
      (23.5, 160),
      (25, 155),
      (26.8, 150),
      (28.6, 145),
      (31, 140),
      (35, 135),
      (41, 130),
      (50, 125),
      (65, 120),
      (90, 115),
      (145, 110),
      (250, 105),
      (460, 100),
      (900, 95)
    };
    private static readonly Func<double, double> calculatePower = MathUtils.PiecewiseFunctionFor(powerFunctionCoordinates);

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if (datasetCollection.HittingStats == null)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.NoHittingStats(PropertyKey));
        return false;
      }

      if (!datasetCollection.HittingStats.AtBats.HasValue || datasetCollection.HittingStats.AtBats < 100 || !datasetCollection.HittingStats.HomeRuns.HasValue)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.InsufficientHittingStats(PropertyKey));
        return false;
      }

      if (datasetCollection.HittingStats.HomeRuns == 0)
      {
        if (player.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(new GeneratorWarning(PropertyKey, "NoHomeRuns", "No home-runs hit by player"));
        return false;
      }

      var atBatsPerHomeRun = (double) datasetCollection.HittingStats.AtBats.Value / datasetCollection.HittingStats.HomeRuns.Value;
      var power = calculatePower(atBatsPerHomeRun);
      player.HitterAbilities.Power = power.Round().MinAt(0).CapAt(255);
      return true;
    }
  }

  public class RunSpeedSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_RunSpeed";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      var baseRunSpeed = GetBaseRunSpeedForPosition(datasetCollection.PrimaryPosition);
      var stolenBaseBonus = .06 * (datasetCollection.HittingStats?.StolenBases ?? 0);
      var runsPerAtBatBonus = 7 * GetRunsPerAtBat(datasetCollection, warning => player.GeneratorWarnings.Add(warning));
      var runSpeed = baseRunSpeed + stolenBaseBonus + runsPerAtBatBonus;
      player.HitterAbilities.RunSpeed = runSpeed.Round().MinAt(1).CapAt(15);
      return true;
    }

    private double GetBaseRunSpeedForPosition(Position position) => position switch
    {
      Position.Pitcher => 3.8,
      Position.Catcher => 6,
      Position.FirstBase => 6.75,
      Position.SecondBase => 9.5,
      Position.ThirdBase => 8.4,
      Position.Shortstop => 9.8,
      Position.LeftField => 9,
      Position.CenterField => 10.4,
      Position.RightField => 9,
      Position.DesignatedHitter => 6.740740741,
      _ => 4
    };

    private double GetRunsPerAtBat(PlayerGenerationData datasetCollection, Action<GeneratorWarning> addWarning)
    {
      if (datasetCollection.HittingStats == null)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          addWarning(GeneratorWarning.NoHittingStats(PropertyKey));
        return 0;
      }

      if (!datasetCollection.HittingStats.AtBats.HasValue || datasetCollection.HittingStats.AtBats < 100 || !datasetCollection.HittingStats.Runs.HasValue)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          addWarning(GeneratorWarning.InsufficientHittingStats(PropertyKey));
        return 0;
      }

      return ((double)datasetCollection.HittingStats.Runs) / datasetCollection.HittingStats.AtBats!.Value;
    }
  }

  public class ArmStrengthSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_ArmStrength";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      var armStrength = GetArmStrength(datasetCollection, warning => player.GeneratorWarnings.Add(warning));
      player.HitterAbilities.ArmStrength = armStrength.Round().MinAt(1).CapAt(15);
      return true;
    }

    private double GetArmStrength(PlayerGenerationData datasetCollection, Action<GeneratorWarning> addWarning)
    {
      switch (datasetCollection.PrimaryPosition)
      {
        case Position.Pitcher:
          return 10;
        case Position.Catcher:
          return 9 + GetCatcherCaughtStealingPercentageBonus(datasetCollection, addWarning);
        case Position.FirstBase:
          return 9 + GetAssistsBonus(datasetCollection, addWarning);
        case Position.SecondBase:
          return 9 + GetAssistsBonus(datasetCollection, addWarning);
        case Position.ThirdBase:
          return 9 + GetAssistsBonus(datasetCollection, addWarning);
        case Position.Shortstop:
          return 10 + GetAssistsBonus(datasetCollection, addWarning);
        case Position.LeftField:
          return 9 + GetAssistsBonus(datasetCollection, addWarning);
        case Position.CenterField:
          return 9 + GetAssistsBonus(datasetCollection, addWarning);
        case Position.RightField:
          return 9 + GetAssistsBonus(datasetCollection, addWarning);
        case Position.DesignatedHitter:
          return 8;
        default:
          throw new InvalidOperationException("Invalid value for Position");
      }
    }

    private double GetCatcherCaughtStealingPercentageBonus(PlayerGenerationData datasetCollection, Action<GeneratorWarning> addWarning)
    {
      if(datasetCollection.FieldingStats == null)
      {
        addWarning(GeneratorWarning.NoFieldingStats(PropertyKey));
        return 0;
      }

      if (!datasetCollection.FieldingStats.OverallFielding.Catcher_StolenBasesAllowed.HasValue ||
        !datasetCollection.FieldingStats.OverallFielding.Catcher_RunnersThrownOut.HasValue
      )
      {
        addWarning(GeneratorWarning.InsufficientFieldingStats(PropertyKey));
        return 0;
      }

      var attempts = datasetCollection.FieldingStats.OverallFielding.Catcher_StolenBasesAllowed + datasetCollection.FieldingStats.OverallFielding.Catcher_RunnersThrownOut;
      if(attempts <  10)
      {
        addWarning(GeneratorWarning.InsufficientFieldingStats(PropertyKey));
        return 0;
      }

      var caughtStealingPercentage = datasetCollection.FieldingStats.OverallFielding.Catcher_RunnersThrownOut.Value / ((double)attempts);
      var linearGradient = MathUtils.BuildLinearGradientFunction(.5, .3, 6, 0.5);
      return linearGradient(caughtStealingPercentage);
    }

    private double GetAssistsBonus(PlayerGenerationData datasetCollection, Action<GeneratorWarning> addWarning)
    {
      if (datasetCollection.FieldingStats == null)
      {
        addWarning(GeneratorWarning.NoFieldingStats(PropertyKey));
        return 0;
      }

      var relevantAssists = GetAssistsForPrimaryAndComparable(datasetCollection.PrimaryPosition, datasetCollection.FieldingStats);
      if (!relevantAssists.HasValue)
      {
        addWarning(GeneratorWarning.InsufficientFieldingStats(PropertyKey));
        return 0;
      }

      var positionGradient = GetLinearGradientForPosition(datasetCollection.PrimaryPosition);
      return positionGradient(relevantAssists.Value);
    }

    public int? GetAssistsForPrimaryAndComparable(Position primaryPosition, LSFieldingStatDataset fieldingStats)
    {
      var validPositionStats = fieldingStats.FieldingByPosition.Where(kvp => kvp.Key.GetPositionType() == primaryPosition.GetPositionType());
      return validPositionStats.SumOrNull(r => r.Value.Assists);
    }

    public Func<double, double> GetLinearGradientForPosition(Position position) => position switch
    {
      Position.FirstBase => MathUtils.BuildLinearGradientFunction(116, 57.075, 4, .5),
      Position.SecondBase => MathUtils.BuildLinearGradientFunction(510, 273.5, 5, .25),
      Position.ThirdBase => MathUtils.BuildLinearGradientFunction(398, 165, 5, .75),
      Position.Shortstop => MathUtils.BuildLinearGradientFunction(492, 250, 5, .75),
      Position.LeftField => MathUtils.BuildLinearGradientFunction(13, 3.26, 5, .25),
      Position.CenterField => MathUtils.BuildLinearGradientFunction(13, 4.08, 5, 1),
      Position.RightField => MathUtils.BuildLinearGradientFunction(16, 4.33, 5, .5),
      _ => throw new InvalidOperationException("position not valid for this function")
    };
  }

  public class FieldingSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_Fielding";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if (datasetCollection.FieldingStats == null)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.NoFieldingStats(PropertyKey));
        return false;
      }

      if (datasetCollection.FieldingStats.OverallFielding.Innings < 30)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.InsufficientFieldingStats(PropertyKey));
        return false;
      }

      var relevantRF = datasetCollection.FieldingStats.FieldingByPosition[datasetCollection.PrimaryPosition]?.RangeFactor;
      if (!relevantRF.HasValue)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.InsufficientFieldingStats(PropertyKey));
        return false;
      }

      var linearGradient = GetRangeFactorGradientForPosition(datasetCollection.PrimaryPosition);
      var fielding = linearGradient(relevantRF.Value);
      player.HitterAbilities.Fielding = fielding.Round().MinAt(4).CapAt(15);
      return true;
    }

    private Func<double, double> GetRangeFactorGradientForPosition(Position position) => position switch
    {
      Position.Catcher => (value) => 8.5 + MathUtils.BuildLinearGradientFunction(8, 6.1, 5, 1)(value),
      Position.FirstBase => MathUtils.BuildLinearGradientFunction(10.2, 8.05, 14, 8.5),
      Position.SecondBase => MathUtils.BuildLinearGradientFunction(5.38, 4.3, 14, 9.5),
      Position.ThirdBase => MathUtils.BuildLinearGradientFunction(3.35, 12.62, 14, 10),
      Position.Shortstop => MathUtils.BuildLinearGradientFunction(4.88, 4.13, 14, 11.25),
      Position.LeftField => MathUtils.BuildLinearGradientFunction(2.73, 1.58, 14, 8.5),
      Position.CenterField => MathUtils.BuildLinearGradientFunction(2.72, 2.25, 14, 10),
      Position.RightField => MathUtils.BuildLinearGradientFunction(2.44, 1.75, 14, 8.5),
      Position.DesignatedHitter => value => 6,
      _ => value => 6
    };
  }

  public class ErrorResistanceSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_ErrorResistance";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if(datasetCollection.FieldingStats == null)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.NoFieldingStats(PropertyKey));
        return false;
      }

      if (datasetCollection.FieldingStats.OverallFielding.TotalChances < 100)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.InsufficientFieldingStats(PropertyKey));
        return false;
      }

      var relevantFpct = datasetCollection.FieldingStats.FieldingByPosition[datasetCollection.PrimaryPosition]?.FieldingPercentage;
      if (!relevantFpct.HasValue)
      {
        if (datasetCollection.PrimaryPosition != Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.InsufficientFieldingStats(PropertyKey));
        return false;
      }

      var linearGradient = GetFieldingPercentageGradientForPosition(datasetCollection.PrimaryPosition);
      var errorResistance = linearGradient(relevantFpct.Value);
      player.HitterAbilities.ErrorResistance = errorResistance.Round().MinAt(4).CapAt(15);
      return true;
    }

    private Func<double, double> GetFieldingPercentageGradientForPosition(Position position) => position switch
    {
      Position.Pitcher => ValueTuple => 5,
      Position.Catcher => MathUtils.BuildLinearGradientFunction(1, .992, 14, 10.75),
      Position.FirstBase => MathUtils.BuildLinearGradientFunction(1, .994, 14, 11.25),
      Position.SecondBase => MathUtils.BuildLinearGradientFunction(1, .983, 14, 9.5),
      Position.ThirdBase => MathUtils.BuildLinearGradientFunction(.987, .954, 14, 10.25),
      Position.Shortstop => MathUtils.BuildLinearGradientFunction(.993, .972, 14, 9.75),
      Position.LeftField => MathUtils.BuildLinearGradientFunction(1, .986, 14, 10.25),
      Position.CenterField => MathUtils.BuildLinearGradientFunction(1, .989, 14, 10.5),
      Position.RightField => MathUtils.BuildLinearGradientFunction(1, .982, 14, 10.25),
      Position.DesignatedHitter => value => 6,
      _ => value => 6
    };
  }

  public class ControlSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "PitcherAbilities_Control";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if(datasetCollection.PitchingStats == null)
      {
        if(datasetCollection.PrimaryPosition == Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.NoPitchingStats(PropertyKey));
        return false;
      }

      if (!datasetCollection.PitchingStats.WalksPer9.HasValue ||
        datasetCollection.PitchingStats.MathematicalInnings < 15
      )
      {
        if (datasetCollection.PrimaryPosition == Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.InsufficientPitchingStats(PropertyKey));
        return false;
      }

      var linearGradient = MathUtils.BuildLinearGradientFunction(0.9, 3.58, 190, 134.3);
      var control = linearGradient(datasetCollection.PitchingStats.WalksPer9.Value);
      player.PitcherAbilities.Control = control.Round().MinAt(60).CapAt(255);
      return true;
    }
  }

  public class StaminaSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "PitcherAbilities_Stamina";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if (player.PitcherType == PitcherType.SwingMan)
        return false;

      if (datasetCollection.PitchingStats == null)
      {
        player.GeneratorWarnings.Add(GeneratorWarning.NoPitchingStats(PropertyKey));
        return false;
      }

      if (
        !datasetCollection.PitchingStats.GamesPitched.HasValue ||
        !datasetCollection.PitchingStats.MathematicalInnings.HasValue ||
        datasetCollection.PitchingStats.MathematicalInnings < 15
      )
      {
        player.GeneratorWarnings.Add(GeneratorWarning.InsufficientPitchingStats(PropertyKey));
        return false;
      }

      var inningsPerGamePitched = datasetCollection.PitchingStats.MathematicalInnings.Value / datasetCollection.PitchingStats.GamesPitched.Value;
      var linearGradient = player.PitcherType == PitcherType.Starter
        ? MathUtils.BuildLinearGradientFunction(7.12, 5.71, 200, 167)
        : MathUtils.BuildLinearGradientFunction(6.11, 1.57, 155, 92);

      var stamina = linearGradient(inningsPerGamePitched);
      player.PitcherAbilities.Stamina = stamina.Round().MinAt(60).CapAt(255);
      return true;
    }
  }

  public class TopSpeedSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "PitcherAbilities_TopSpeedMph";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if (datasetCollection.PitchingStats == null)
      {
        if(datasetCollection.PrimaryPosition == Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.NoPitchingStats(PropertyKey));
        return false;
      }

      if (!datasetCollection.PitchingStats.StrikeoutsPer9.HasValue ||
        datasetCollection.PitchingStats.MathematicalInnings < 15
      )
      {
        if (datasetCollection.PrimaryPosition == Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.InsufficientPitchingStats(PropertyKey));
        return false;
      }

      var linearGradient = MathUtils.BuildLinearGradientFunction(12.89, 6.81, 100, 94.1);
      var topSpeed = linearGradient(datasetCollection.PitchingStats.StrikeoutsPer9.Value);
      player.PitcherAbilities.TopSpeedMph = topSpeed.MinAt(49).CapAt(105);
      return true;
    }
  }

  public class PitchArsenalSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "PitcherAbilities_PitchArsenal";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if (datasetCollection.PitchingStats == null)
      {
        if (datasetCollection.PrimaryPosition == Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.NoPitchingStats(PropertyKey));
        return false;
      }

      if (!datasetCollection.PitchingStats.BattingAverageAgainst.HasValue ||
        datasetCollection.PitchingStats.MathematicalInnings < 15
      )
      {
        if (datasetCollection.PrimaryPosition == Position.Pitcher)
          player.GeneratorWarnings.Add(GeneratorWarning.InsufficientPitchingStats(PropertyKey));
        return false;
      }

      var breakLinearGradient = MathUtils.BuildLinearGradientFunction(.107, .264, 6, 3);
      var firstPitchBreakCalc = breakLinearGradient(datasetCollection.PitchingStats.BattingAverageAgainst.Value);
      var secondPitchBreakCalc = firstPitchBreakCalc * .7;
      var thirdPitchBreakCalc = secondPitchBreakCalc * .7;

      var firstPitchBreak = firstPitchBreakCalc.Round().MinAt(1).CapAt(7);
      var secondPitchBreak = secondPitchBreakCalc.Round().MinAt(1).CapAt(7);
      var thirdPitchBeak = thirdPitchBreakCalc.Round().MinAt(1).CapAt(7);

      var sliderType = GetRandomSliderTypeByYear(datasetCollection.Year);
      var curveType = GetRandomCurveTypeByYear(datasetCollection.Year);
      var changeType = GetForkTypeForYear(datasetCollection.Year);

      if(player.PitcherType == PitcherType.Starter)
      {
        player.PitcherAbilities.Slider1Type = sliderType;
        player.PitcherAbilities.Slider1Movement = sliderType.HasValue
          ? firstPitchBreak
          : null;
        
        player.PitcherAbilities.Curve1Type = curveType;
        player.PitcherAbilities.Curve1Movement = sliderType.HasValue
          ? secondPitchBreak
          : firstPitchBreak;

        player.PitcherAbilities.Fork1Type = changeType;
        player.PitcherAbilities.Fork1Movement = sliderType.HasValue
          ? thirdPitchBeak
          : secondPitchBreak;
      }
      else
      {
        player.PitcherAbilities.Slider1Type = sliderType;
        player.PitcherAbilities.Slider1Movement = sliderType.HasValue
          ? firstPitchBreak
          : null;

        player.PitcherAbilities.Curve1Type = sliderType.HasValue
          ? null
          : curveType;
        player.PitcherAbilities.Curve1Movement = sliderType.HasValue
          ? null
          : firstPitchBreak;

        player.PitcherAbilities.Fork1Type = changeType;
        player.PitcherAbilities.Fork1Movement = secondPitchBreak;
      }

      return true;
    }

    private SliderType? GetRandomSliderTypeByYear(int year)
    {
      if (year < 1925)
        return null;

      if (year < 1955)
        return SliderType.Slider;

      var rand = Random.Shared.NextDouble();
      if (rand < .76)
        return SliderType.Slider;
      else if (rand < .9)
        return SliderType.Cutter;
      else 
        return SliderType.HardSlider;
    }

    private CurveType GetRandomCurveTypeByYear(int year)
    {
      if (year < 1945)
        return CurveType.Curve;

      var rand = Random.Shared.NextDouble();
      if (rand < .78)
        return CurveType.Curve;
      else if (rand < .9)
        return CurveType.DropCurve;
      else
        return CurveType.Slurve;
    }

    private ForkType GetForkTypeForYear(int year)
    {
      if (year < 1945)
        return ForkType.Palmball;
      else
        return ForkType.ChangeUp;
    }
  }
}
