using NUnit.Framework;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Fetchers.MLBLookupService;
using PowerUp.Generators;
using Shouldly;

namespace PowerUp.Tests.Generators
{
  public class LSStatisticsGeneratorTests
  {
    IPlayerGenerator _playerGenerator;

    [SetUp]
    public void SetUp()
    {
      _playerGenerator = new PlayerGenerator(new PlayerApi(), new MLBLookupServiceClient());
    }

    [Test]
    public void LSStatisticsAlgorithm_GeneratesHitter()
    {
      var result = _playerGenerator.GeneratePlayer(110849, 1980, new LSStatistcsPlayerGenerationAlgorithm());
      result.SourceType.ShouldBe(EntitySourceType.Generated);
      result.Year.ShouldBe(1980);
      result.FirstName.ShouldBe("Johnny");
      result.LastName.ShouldBe("Bench");
      result.SavedName.ShouldBe("J.Bench");
      // They don't have his number listed
      result.UniformNumber.ShouldBe("000");
      result.PrimaryPosition.ShouldBe(Position.Catcher);
      result.PitcherType.ShouldBe(PitcherType.SwingMan);
    }

    [Test]
    public void LSStatisticsAlgorithm_GeneratesPitcher()
    {
      var result = _playerGenerator.GeneratePlayer(114756, 1963, new LSStatistcsPlayerGenerationAlgorithm());
      result.SourceType.ShouldBe(EntitySourceType.Generated);
      result.Year.ShouldBe(1963);
      result.FirstName.ShouldBe("Bob");
      result.LastName.ShouldBe("Gibson");
      result.SavedName.ShouldBe("B.Gibson");
      // They don't have his number listed
      result.UniformNumber.ShouldBe("000");
      result.PrimaryPosition.ShouldBe(Position.Pitcher);
      result.PitcherType.ShouldBe(PitcherType.Starter);
    }
  }
}
