using NUnit.Framework;
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
    public void LSStatisticsAlgorithm_GeneratesPlayer()
    {
      var result = _playerGenerator.GeneratePlayer(110849, new LSStatistcsPlayerGenerationAlgorithm());
      result.FirstName.ShouldBe("Johnny");
      result.LastName.ShouldBe("Bench");
      result.SavedName.ShouldBe("J.Bench");
    }
  }
}
