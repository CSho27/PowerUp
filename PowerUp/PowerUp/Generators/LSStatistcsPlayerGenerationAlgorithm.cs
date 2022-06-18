using PowerUp.Entities.Players;
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

    public LSStatistcsPlayerGenerationAlgorithm()
    {
      SetProperty("FirstName", (player, data) => player.FirstName = data.PlayerInfo!.FirstNameUsed);
      SetProperty("LastName", (player, data) => player.LastName = data.PlayerInfo!.LastName);
      SetProperty(new SavedName());
      SetProperty(new UniformNumber());
      SetProperty("PrimaryPosition", (player, data) => player.PrimaryPosition = data.PlayerInfo!.PrimaryPosition);
      SetProperty(new PitcherTypeSetter());
      // TODO: Set VoiceId
      SetProperty("BattingSide", (player, data) => player.BattingSide = data.PlayerInfo!.BattingSide);
      SetProperty("ThrowingArm", (player, data) => player.ThrowingArm = data.PlayerInfo!.ThrowingArm);
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
  }
}
