﻿using NUnit.Framework;
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
    public void LSStatisticsAlgorithm_GeneratesCatcher()
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

      var positionCapabilities = result.PositonCapabilities;
      positionCapabilities.Pitcher.ShouldBe(Grade.G);
      positionCapabilities.Catcher.ShouldBe(Grade.A);
      positionCapabilities.FirstBase.ShouldBe(Grade.G);
      positionCapabilities.SecondBase.ShouldBe(Grade.G);
      positionCapabilities.ThirdBase.ShouldBe(Grade.G);
      positionCapabilities.Shortstop.ShouldBe(Grade.G);
      positionCapabilities.LeftField.ShouldBe(Grade.G);
      positionCapabilities.CenterField.ShouldBe(Grade.G);
      positionCapabilities.RightField.ShouldBe(Grade.G);
    }

    [Test]
    public void LSStatisticsAlgorithm_GeneratesShortstop()
    {
      var result = _playerGenerator.GeneratePlayer(121222, 1990, new LSStatistcsPlayerGenerationAlgorithm());
      result.SourceType.ShouldBe(EntitySourceType.Generated);
      result.Year.ShouldBe(1990);
      result.FirstName.ShouldBe("Cal");
      result.LastName.ShouldBe("Ripken Jr.");
      result.SavedName.ShouldBe("Ripken Jr.");
      result.UniformNumber.ShouldBe("8");
      result.PrimaryPosition.ShouldBe(Position.Shortstop);
      result.PitcherType.ShouldBe(PitcherType.SwingMan);

      var positionCapabilities = result.PositonCapabilities;
      positionCapabilities.Pitcher.ShouldBe(Grade.G);
      positionCapabilities.Catcher.ShouldBe(Grade.G);
      positionCapabilities.FirstBase.ShouldBe(Grade.F);
      positionCapabilities.SecondBase.ShouldBe(Grade.E);
      positionCapabilities.ThirdBase.ShouldBe(Grade.E);
      positionCapabilities.Shortstop.ShouldBe(Grade.A);
      positionCapabilities.LeftField.ShouldBe(Grade.G);
      positionCapabilities.CenterField.ShouldBe(Grade.G);
      positionCapabilities.RightField.ShouldBe(Grade.G);
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

      var positionCapabilities = result.PositonCapabilities;
      positionCapabilities.Pitcher.ShouldBe(Grade.A);
      positionCapabilities.Catcher.ShouldBe(Grade.G);
      positionCapabilities.FirstBase.ShouldBe(Grade.G);
      positionCapabilities.SecondBase.ShouldBe(Grade.G);
      positionCapabilities.ThirdBase.ShouldBe(Grade.G);
      positionCapabilities.Shortstop.ShouldBe(Grade.G);
      positionCapabilities.LeftField.ShouldBe(Grade.G);
      positionCapabilities.CenterField.ShouldBe(Grade.G);
      positionCapabilities.RightField.ShouldBe(Grade.G);
    }
  }
}
