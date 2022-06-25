﻿using PowerUp.Entities.Players;
using PowerUp.Libraries;
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

    public LSStatistcsPlayerGenerationAlgorithm(IVoiceLibrary voiceLibrary)
    {
      SetProperty("FirstName", (player, data) => player.FirstName = data.PlayerInfo!.FirstNameUsed);
      SetProperty("LastName", (player, data) => player.LastName = data.PlayerInfo!.LastName);
      SetProperty(new SavedName());
      SetProperty(new UniformNumber());
      SetProperty("PrimaryPosition", (player, data) => player.PrimaryPosition = data.PlayerInfo!.PrimaryPosition);
      SetProperty(new PitcherTypeSetter());
      SetProperty("VoiceId", (player, data) => player.VoiceId = voiceLibrary.FindClosestTo(data.PlayerInfo!.FirstNameUsed, data.PlayerInfo!.LastName).Key);
      SetProperty("BattingSide", (player, data) => player.BattingSide = data.PlayerInfo!.BattingSide);
      SetProperty("ThrowingArm", (player, data) => player.ThrowingArm = data.PlayerInfo!.ThrowingArm);
      SetProperty(new PitcherCapabilitySetter());
      SetProperty(new CatcherCapabilitySetter());
      SetProperty(new FirstBaseCapabilitySetter());
      SetProperty(new SecondBaseCapabilitySetter());
      SetProperty(new ThirdBaseCapabilitySetter());
      SetProperty(new ShortstopCapabilitySetter());
      SetProperty(new LeftFieldCapabilitySetter());
      SetProperty(new CenterFieldCapabilitySetter());
      SetProperty(new RightFieldCapabilitySetter());
      // TODO: Do Appearance
      // TODO: Do Position Capabilities
      // TODO: Do Hitter Abilities
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

    public abstract class PositionCapabilitySetter : PlayerPropertySetter
    {
      protected Grade GetGradeForPosition(Position position, PlayerGenerationData datasetCollection)
      {
        var primaryPosition = datasetCollection.PlayerInfo!.PrimaryPosition;
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
}
