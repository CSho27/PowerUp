using NUnit.Framework;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Fetchers.MLBLookupService;
using PowerUp.Generators;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Tests.Generators
{
  public class PlayerGeneratorTests
  {
    IPlayerGenerator _playerGenerator;

    [SetUp]
    public void SetUp()
    {
      _playerGenerator = new PlayerGenerator(new PlayerApi(), new MLBLookupServiceClient());
    }

    [Test]
    public void PlayerGenerator_GeneratesPlayer()
    {
      var result = _playerGenerator.GeneratePlayer(110849, new TestAlgorithm());
      result.FirstName.ShouldBe("Johnny");
    }

    public class TestAlgorithm : PlayerGenerationAlgorithm
    {
      public override HashSet<PlayerGenerationDataset> DatasetDependencies => new HashSet<PlayerGenerationDataset>() { PlayerGenerationDataset.LSPlayerInfo };

      public TestAlgorithm()
      {
        SetProperty("FirstName", (player, datasets) => player.FirstName = datasets.PlayerInfo!.FirstNameUsed);
        // If property is attempted to be set again, the first takes precedence
        SetProperty("FirstName", (player, datasets) => player.FirstName = "Billy");
      }

    }
  }
}
