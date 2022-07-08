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
      PlayerGenerationDataset.LSPitchingStats
    };

    public LSStatistcsPlayerGenerationAlgorithm(IVoiceLibrary voiceLibrary, ISkinColorGuesser skinColorGuesser) 
    {
      // Player Info
      SetProperty("FirstName", (player, data) => player.FirstName = data.PlayerInfo!.FirstNameUsed);
      SetProperty("LastName", (player, data) => player.LastName = data.PlayerInfo!.LastName);
      SetProperty(new SavedName());
      SetProperty(new UniformNumber());
      SetProperty("PrimaryPosition", (player, data) => player.PrimaryPosition = data.PrimaryPosition);
      SetProperty(new PitcherTypeSetter());
      SetProperty("VoiceId", (player, data) => player.VoiceId = voiceLibrary.FindClosestTo(data.PlayerInfo!.FirstNameUsed, data.PlayerInfo!.LastName).Key);
      SetProperty("BattingSide", (player, data) => player.BattingSide = data.PlayerInfo!.BattingSide);
      SetProperty("ThrowingArm", (player, data) => player.ThrowingArm = data.PlayerInfo!.ThrowingArm);

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

      // TODO: Do Pitcher Abilities
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
        
        if(lastName.Length <= 10)
        {
          player.SavedName = lastName;
          return true;
        }

        var lastNameWithoutVowels = new string(lastName.Where((c, i) => i == 0 || !c.IsVowel()).ToArray());
        if(lastNameWithoutVowels.Length <= 10)
        {
          player.SavedName = lastNameWithoutVowels;
          return true;
        }

        player.SavedName = new string(lastName.Take(10).ToArray());
        return true;
      }
    }

    public class UniformNumber : PlayerPropertySetter
    {
      public override string PropertyKey => "UniformNumber";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        var uniformNumber = datasetCollection.PlayerInfo!.UniformNumber;
        if(uniformNumber == null)
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
          return false;

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
        player.PositonCapabilities.Pitcher = GetGradeForPosition(Position.Pitcher, datasetCollection);
        return true;
      }
    }

    public class CatcherCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_Catcher";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositonCapabilities.Catcher = GetGradeForPosition(Position.Catcher, datasetCollection);
        return true;
      }
    }

    public class FirstBaseCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_FirstBase";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositonCapabilities.FirstBase = GetGradeForPosition(Position.FirstBase, datasetCollection);
        return true;
      }
    }

    public class SecondBaseCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_SecondBase";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositonCapabilities.SecondBase = GetGradeForPosition(Position.SecondBase, datasetCollection);
        return true;
      }
    }


    public class ThirdBaseCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_ThirdBase";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositonCapabilities.ThirdBase = GetGradeForPosition(Position.ThirdBase, datasetCollection);
        return true;
      }
    }

    public class ShortstopCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_Shortstop";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositonCapabilities.Shortstop = GetGradeForPosition(Position.Shortstop, datasetCollection);
        return true;
      }
    }

    public class LeftFieldCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_LeftField";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositonCapabilities.LeftField = GetGradeForPosition(Position.LeftField, datasetCollection);
        return true;
      }
    }

    public class CenterFieldCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_CenterField";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositonCapabilities.CenterField = GetGradeForPosition(Position.CenterField, datasetCollection);
        return true;
      }
    }

    public class RightFieldCapabilitySetter : PositionCapabilitySetter
    {
      public override string PropertyKey => "PositionCapabilities_RightField";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        player.PositonCapabilities.RightField = GetGradeForPosition(Position.RightField, datasetCollection);
        return true;
      }
    }
  }

  public class TrajectorySetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_Trajcetory";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if (datasetCollection.HittingStats == null || !datasetCollection.HittingStats.HomeRuns.HasValue)
        return false;

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
      if (datasetCollection.HittingStats == null || datasetCollection.HittingStats.AtBats < 100 || !datasetCollection.HittingStats.BattingAverage.HasValue)
        return false;

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
      (11, 240),
      (11.5, 230),
      (12, 220),
      (13, 210),
      (14, 200),
      (15, 190),
      (16, 180),
      (18, 170),
      (19, 162),
      (20.5, 154),
      (22, 148),
      (23, 143),
      (24, 139),
      (26, 135),
      (28, 131),
      (30, 127),
      (32, 123),
      (35, 120),
      (40, 117),
      (45, 114),
      (60, 105),
      (75, 95),
      (110, 85),
      (150, 75),
      (200, 70)
    };
    private static readonly Func<double, double> calculatePower = MathUtils.PiecewiseFunctionFor(powerFunctionCoordinates);

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if (
        datasetCollection.HittingStats == null ||
        !datasetCollection.HittingStats.AtBats.HasValue ||
        datasetCollection.HittingStats.AtBats < 100 ||
        !datasetCollection.HittingStats.HomeRuns.HasValue
      )
      {
        return false;
      }

      var atBatsPerHomeRun = (double) datasetCollection.HittingStats.AtBats.Value / datasetCollection.HittingStats.HomeRuns.Value;
      var power = calculatePower(atBatsPerHomeRun);
      player.HitterAbilities.Power = power.Round().CapAt(255);
      return true;
    }
  }

  public class RunSpeedSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_RunSpeed";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      var baseRunSpeed = GetBaseRunSpeedForPosition(datasetCollection.PrimaryPosition);
      var stolenBaseBonus = .08 * (datasetCollection.HittingStats?.StolenBases ?? 0);
      var runsPerAtBatBonus = 5 * GetRunsPerAtBat(datasetCollection);
      var runSpeed = baseRunSpeed + stolenBaseBonus + runsPerAtBatBonus;
      player.HitterAbilities.RunSpeed = runSpeed.Round().CapAt(15);
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

    private double GetRunsPerAtBat(PlayerGenerationData datasetCollection)
    {
      if (
       datasetCollection.HittingStats == null ||
       !datasetCollection.HittingStats.Runs.HasValue ||
       !datasetCollection.HittingStats.AtBats.HasValue ||
       datasetCollection.HittingStats.AtBats < 100
      ) return 0;

      return ((double)datasetCollection.HittingStats.Runs) / datasetCollection.HittingStats.AtBats!.Value;
    }
  }

  public class ArmStrengthSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_ArmStrength";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      var armStrength = GetArmStrength(datasetCollection);
      player.HitterAbilities.ArmStrength = armStrength.Round().CapAt(15);
      return true;
    }

    private double GetArmStrength(PlayerGenerationData datasetCollection)
    {
      switch (datasetCollection.PrimaryPosition)
      {
        case Position.Pitcher:
          return 10;
        case Position.Catcher:
          return 9 + GetCatcherCaughtStealingPercentageBonus(datasetCollection);
        case Position.FirstBase:
          return 7 + GetFirstBaseUtilityBonus(datasetCollection);
        case Position.SecondBase:
          return 9 + GetInfielderAssistsPerInningBonus(datasetCollection);
        case Position.ThirdBase:
          return 10 + GetInfielderAssistsPerInningBonus(datasetCollection);
        case Position.Shortstop:
          return 10 + GetInfielderAssistsPerInningBonus(datasetCollection);
        case Position.LeftField:
          return 9 + GetOutfieldAssistsPerInningBonus(datasetCollection);
        case Position.CenterField:
          return 11 + GetOutfieldAssistsPerInningBonus(datasetCollection);
        case Position.RightField:
          return 10 + GetOutfieldAssistsPerInningBonus(datasetCollection);
        case Position.DesignatedHitter:
          return 8;
        default:
          throw new InvalidOperationException("Invalid value for Position");
      }
    }

    private double GetCatcherCaughtStealingPercentageBonus(PlayerGenerationData datasetCollection)
    {
      if (
        datasetCollection.FieldingStats == null || 
        !datasetCollection.FieldingStats.OverallFielding.Catcher_StolenBasesAllowed.HasValue ||
        !datasetCollection.FieldingStats.OverallFielding.Catcher_RunnersThrownOut.HasValue
      ) return 0;

      var attempts = datasetCollection.FieldingStats.OverallFielding.Catcher_StolenBasesAllowed + datasetCollection.FieldingStats.OverallFielding.Catcher_RunnersThrownOut;
      if(attempts <  10)
        return 0;

      var caughtStealingPercentage = datasetCollection.FieldingStats.OverallFielding.Catcher_RunnersThrownOut.Value / ((double)attempts);
      var linearGradient = MathUtils.BuildLinearGradientFunction(.45, .2, 5, 1);
      return linearGradient(caughtStealingPercentage);
    }

    private double GetFirstBaseUtilityBonus(PlayerGenerationData datasetCollection)
    {
      var otherPositionsPlayed = datasetCollection.FieldingStats?.FieldingByPosition.Count(s => s.Key != Position.FirstBase) ?? 0;
      return otherPositionsPlayed * .5;
    }

    private double GetInfielderAssistsPerInningBonus(PlayerGenerationData datasetCollection)
    {
      if (
        datasetCollection.FieldingStats == null ||
        !datasetCollection.FieldingStats.OverallFielding.Assists.HasValue
      ) return 0;


      var positionGradient = GetLinearGradientForPosition(datasetCollection.PrimaryPosition);
      return positionGradient(datasetCollection.FieldingStats.OverallFielding.Assists.Value);
    }

    public Func<double, double> GetLinearGradientForPosition(Position position) => position switch
    {
      Position.SecondBase => MathUtils.BuildLinearGradientFunction(400, 190, 4, .5),
      Position.ThirdBase => MathUtils.BuildLinearGradientFunction(300, 175, 4, .5),
      Position.Shortstop => MathUtils.BuildLinearGradientFunction(450, 200, 4, .5),
      _ => throw new InvalidOperationException("Non infield position used")
    };

    private double GetOutfieldAssistsPerInningBonus(PlayerGenerationData datasetCollection)
    {
      if (
        datasetCollection.FieldingStats == null ||
        !datasetCollection.FieldingStats.OverallFielding.Assists.HasValue
      ) return 0;

      return (datasetCollection.FieldingStats.OverallFielding.Assists.Value) * .25;
    }
  }

  public class FieldingSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_Fielding";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if(
        datasetCollection.FieldingStats == null ||
        !datasetCollection.FieldingStats.OverallFielding.RangeFactor.HasValue ||
        datasetCollection.FieldingStats.OverallFielding.Innings < 30
      ) return false;

      var linearGradient = GetRangeFactorGradientForPosition(datasetCollection.PrimaryPosition);
      var fielding = linearGradient(datasetCollection.FieldingStats.OverallFielding.RangeFactor!.Value);
      player.HitterAbilities.Fielding = fielding.Round().CapAt(15);
      return true;
    }

    private Func<double, double> GetRangeFactorGradientForPosition(Position position) => position switch
    {
      Position.Catcher => MathUtils.BuildLinearGradientFunction(11, 8, 15, 9),
      Position.FirstBase => MathUtils.BuildLinearGradientFunction(9, 8, 15, 9),
      Position.SecondBase => MathUtils.BuildLinearGradientFunction(4.3, 2.25, 15, 9),
      Position.ThirdBase => MathUtils.BuildLinearGradientFunction(3, 1.5, 15, 9),
      Position.Shortstop => MathUtils.BuildLinearGradientFunction(4.7, 2.5, 15, 9),
      Position.LeftField => MathUtils.BuildLinearGradientFunction(3, 1, 15, 9),
      Position.CenterField => MathUtils.BuildLinearGradientFunction(4, 2, 15, 9),
      Position.RightField => MathUtils.BuildLinearGradientFunction(2.5, 1, 15, 9),
      Position.DesignatedHitter => value => 6,
      _ => value => 6
    };
  }

  public class ErrorResistanceSetter : PlayerPropertySetter
  {
    public override string PropertyKey => "HitterAbilities_ErrorResistance";

    public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
    {
      if (
        datasetCollection.FieldingStats == null ||
        !datasetCollection.FieldingStats.OverallFielding.FieldingPercentage.HasValue ||
        datasetCollection.FieldingStats.OverallFielding.Innings < 30
      ) return false;

      var linearGradient = GetFieldingPercentageGradientForPosition(datasetCollection.PrimaryPosition);
      var errorResistance = linearGradient(datasetCollection.FieldingStats.OverallFielding.FieldingPercentage!.Value);
      player.HitterAbilities.ErrorResistance = errorResistance.Round().CapAt(15);
      return true;
    }

    private Func<double, double> GetFieldingPercentageGradientForPosition(Position position) => position switch
    {
      Position.Catcher => MathUtils.BuildLinearGradientFunction(1, .95, 15, 9),
      Position.FirstBase => MathUtils.BuildLinearGradientFunction(1, .96, 15, 9),
      Position.SecondBase => MathUtils.BuildLinearGradientFunction(1, .95, 15, 9),
      Position.ThirdBase => MathUtils.BuildLinearGradientFunction(1, .95, 15, 9),
      Position.Shortstop => MathUtils.BuildLinearGradientFunction(1, .95, 15, 9),
      Position.LeftField => MathUtils.BuildLinearGradientFunction(1, .97, 15, 9),
      Position.CenterField => MathUtils.BuildLinearGradientFunction(1, .97, 15, 9),
      Position.RightField => MathUtils.BuildLinearGradientFunction(1, .97, 15, 9),
      Position.DesignatedHitter => value => 6,
      _ => value => 6
    };
  }
}
