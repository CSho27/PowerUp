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
    }

    public class SavedName : PlayerPropertySetter
    {
      public override string PropertyKey => "SavedName";

      public override bool SetProperty(Player player, PlayerGenerationData datasetCollection)
      {
        var firstLetterOfFirstName = datasetCollection.PlayerInfo!.FirstNameUsed.ToCharArray().First();
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

        var lastName
      }
    }

  }
}
