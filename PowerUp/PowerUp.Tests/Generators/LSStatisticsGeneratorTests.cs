using NUnit.Framework;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Fetchers.MLBLookupService;
using PowerUp.Generators;
using PowerUp.Libraries;
using Shouldly;

namespace PowerUp.Tests.Generators
{
  public class LSStatisticsGeneratorTests
  {
    IPlayerGenerator _playerGenerator;
    IVoiceLibrary _voiceLibrary; 
    ISkinColorGuesser _skinColorGuesser;
    IMLBLookupServiceClient _mlbLookupApiClient;

    [SetUp]
    public void SetUp()
    {
      _playerGenerator = new PlayerGenerator(new PlayerApi(), new LSPlayerStatisticsFetcher(new MLBLookupServiceClient()));
      _voiceLibrary = TestConfig.VoiceLibrary.Value;
      _skinColorGuesser = new SkinColorGuesser(TestConfig.CountryAndSkinColorLibrary.Value);
      _mlbLookupApiClient = new MLBLookupServiceClient();
    }

    [Test]
    public void LSStatisticsAlgorithm_GeneratesCatcher()
    {
      var result = _playerGenerator.GeneratePlayer(110849, 1980, new LSStatistcsPlayerGenerationAlgorithm(_voiceLibrary, _skinColorGuesser));
      result.LastTeamForYear_LSTeamId.ShouldBe(113);

      var resultPlayer = result.Player;
      resultPlayer.SourceType.ShouldBe(EntitySourceType.Generated);
      resultPlayer.Year.ShouldBe(1980);
      resultPlayer.FirstName.ShouldBe("Johnny");
      resultPlayer.LastName.ShouldBe("Bench");
      resultPlayer.SavedName.ShouldBe("J.Bench");
      // They don't have his number listed
      resultPlayer.UniformNumber.ShouldBe("000");
      resultPlayer.PrimaryPosition.ShouldBe(Position.Catcher);
      resultPlayer.PitcherType.ShouldBe(PitcherType.SwingMan);
      resultPlayer.VoiceId.ShouldBe(_voiceLibrary.FindClosestTo("Johnny", "Bench").Key);

      var appearance = resultPlayer.Appearance;
      // Skin Color is non-deterministic
      // appearance.SkinColor.ShouldBe(SkinColor.One);

      var positionCapabilities = resultPlayer.PositionCapabilities;
      positionCapabilities.Pitcher.ShouldBe(Grade.G);
      positionCapabilities.Catcher.ShouldBe(Grade.A);
      positionCapabilities.FirstBase.ShouldBe(Grade.G);
      positionCapabilities.SecondBase.ShouldBe(Grade.G);
      positionCapabilities.ThirdBase.ShouldBe(Grade.G);
      positionCapabilities.Shortstop.ShouldBe(Grade.G);
      positionCapabilities.LeftField.ShouldBe(Grade.G);
      positionCapabilities.CenterField.ShouldBe(Grade.G);
      positionCapabilities.RightField.ShouldBe(Grade.G);

      var hitterAbilities = resultPlayer.HitterAbilities;
      hitterAbilities.Trajectory.ShouldBe(3);
      hitterAbilities.Contact.ShouldBe(7);
      hitterAbilities.Power.ShouldBe(198);
      hitterAbilities.RunSpeed.ShouldBe(7);
      hitterAbilities.ArmStrength.ShouldBe(9);
      hitterAbilities.Fielding.ShouldBe(8);
      hitterAbilities.ErrorResistance.ShouldBe(10);

      var pitcherAbilities = resultPlayer.PitcherAbilities;
      pitcherAbilities.Control.ShouldBe(0);
      pitcherAbilities.TopSpeedMph.ShouldBe(74);
    }

    [Test]
    public void LSStatisticsAlgorithm_GeneratesShortstop()
    {
      var result = _playerGenerator.GeneratePlayer(121222, 1990, new LSStatistcsPlayerGenerationAlgorithm(_voiceLibrary, _skinColorGuesser));
      result.LastTeamForYear_LSTeamId.ShouldBe(110);

      var resultPlayer = result.Player;
      resultPlayer.SourceType.ShouldBe(EntitySourceType.Generated);
      resultPlayer.Year.ShouldBe(1990);
      resultPlayer.FirstName.ShouldBe("Cal");
      resultPlayer.LastName.ShouldBe("Ripken Jr.");
      resultPlayer.SavedName.ShouldBe("Ripken Jr.");
      resultPlayer.UniformNumber.ShouldBe("8");
      resultPlayer.PrimaryPosition.ShouldBe(Position.Shortstop);
      resultPlayer.PitcherType.ShouldBe(PitcherType.SwingMan);
      resultPlayer.VoiceId.ShouldBe(_voiceLibrary.FindClosestTo("Cal", "Ripken Jr.").Key);

      var appearance = resultPlayer.Appearance;
      // Skin Color is non-deterministic
      //appearance.SkinColor.ShouldBe(SkinColor.One);

      var positionCapabilities = resultPlayer.PositionCapabilities;
      positionCapabilities.Pitcher.ShouldBe(Grade.G);
      positionCapabilities.Catcher.ShouldBe(Grade.G);
      positionCapabilities.FirstBase.ShouldBe(Grade.F);
      positionCapabilities.SecondBase.ShouldBe(Grade.E);
      positionCapabilities.ThirdBase.ShouldBe(Grade.E);
      positionCapabilities.Shortstop.ShouldBe(Grade.A);
      positionCapabilities.LeftField.ShouldBe(Grade.G);
      positionCapabilities.CenterField.ShouldBe(Grade.G);
      positionCapabilities.RightField.ShouldBe(Grade.G);

      var hitterAbilities = resultPlayer.HitterAbilities;
      hitterAbilities.Trajectory.ShouldBe(3);
      hitterAbilities.Contact.ShouldBe(7);
      hitterAbilities.Power.ShouldBe(145);
      hitterAbilities.RunSpeed.ShouldBe(11);
      hitterAbilities.ArmStrength.ShouldBe(14);
      hitterAbilities.Fielding.ShouldBe(12);
      hitterAbilities.ErrorResistance.ShouldBe(15);

      var pitcherAbilities = resultPlayer.PitcherAbilities;
      pitcherAbilities.Control.ShouldBe(0);
      pitcherAbilities.TopSpeedMph.ShouldBe(74);
    }

    [Test]
    public void LSStatisticsAlgorithm_GeneratesOutfielder()
    {
      var result = _playerGenerator.GeneratePlayer(665742, 2021, new LSStatistcsPlayerGenerationAlgorithm(_voiceLibrary, _skinColorGuesser));
      result.LastTeamForYear_LSTeamId.ShouldBe(120);

      var resultPlayer = result.Player;
      resultPlayer.SourceType.ShouldBe(EntitySourceType.Generated);
      resultPlayer.Year.ShouldBe(2021);
      resultPlayer.FirstName.ShouldBe("Juan");
      resultPlayer.LastName.ShouldBe("Soto");
      resultPlayer.SavedName.ShouldBe("J.Soto");
      resultPlayer.UniformNumber.ShouldBe("22");
      resultPlayer.PrimaryPosition.ShouldBe(Position.RightField);
      resultPlayer.PitcherType.ShouldBe(PitcherType.SwingMan);
      resultPlayer.VoiceId.ShouldBe(_voiceLibrary.FindClosestTo("Juan", "Soto").Key);

      var appearance = resultPlayer.Appearance;
      // Skin Color is non-deterministic
      //appearance.SkinColor.ShouldBe(SkinColor.One);

      var positionCapabilities = resultPlayer.PositionCapabilities;
      positionCapabilities.Pitcher.ShouldBe(Grade.G);
      positionCapabilities.Catcher.ShouldBe(Grade.G);
      positionCapabilities.FirstBase.ShouldBe(Grade.G);
      positionCapabilities.SecondBase.ShouldBe(Grade.G);
      positionCapabilities.ThirdBase.ShouldBe(Grade.G);
      positionCapabilities.Shortstop.ShouldBe(Grade.G);
      positionCapabilities.LeftField.ShouldBe(Grade.E);
      positionCapabilities.CenterField.ShouldBe(Grade.E);
      positionCapabilities.RightField.ShouldBe(Grade.A);

      var hitterAbilities = resultPlayer.HitterAbilities;
      hitterAbilities.Trajectory.ShouldBe(3);
      hitterAbilities.Contact.ShouldBe(11);
      hitterAbilities.Power.ShouldBe(186);
      hitterAbilities.RunSpeed.ShouldBe(11);
      hitterAbilities.ArmStrength.ShouldBe(10);
      hitterAbilities.Fielding.ShouldBe(11);
      hitterAbilities.ErrorResistance.ShouldBe(10);

      var pitcherAbilities = resultPlayer.PitcherAbilities;
      pitcherAbilities.Control.ShouldBe(0);
      pitcherAbilities.TopSpeedMph.ShouldBe(74);
    }

    [Test]
    public void LSStatisticsAlgorithm_GeneratesPitcher()
    {
      var result = _playerGenerator.GeneratePlayer(114756, 1963, new LSStatistcsPlayerGenerationAlgorithm(_voiceLibrary, _skinColorGuesser));
      result.LastTeamForYear_LSTeamId.ShouldBe(138);

      var resultPlayer = result.Player;
      resultPlayer.SourceType.ShouldBe(EntitySourceType.Generated);
      resultPlayer.Year.ShouldBe(1963);
      resultPlayer.FirstName.ShouldBe("Bob");
      resultPlayer.LastName.ShouldBe("Gibson");
      resultPlayer.SavedName.ShouldBe("B.Gibson");
      // They don't have his number listed
      resultPlayer.UniformNumber.ShouldBe("000");
      resultPlayer.PrimaryPosition.ShouldBe(Position.Pitcher);
      resultPlayer.PitcherType.ShouldBe(PitcherType.Starter);
      resultPlayer.VoiceId.ShouldBe(_voiceLibrary.FindClosestTo("Bob", "Gibson").Key);

      var appearance = resultPlayer.Appearance;
      // Skin Color is non-deterministic
      // appearance.SkinColor.ShouldBe(SkinColor.Five);

      var positionCapabilities = resultPlayer.PositionCapabilities;
      positionCapabilities.Pitcher.ShouldBe(Grade.A);
      positionCapabilities.Catcher.ShouldBe(Grade.G);
      positionCapabilities.FirstBase.ShouldBe(Grade.G);
      positionCapabilities.SecondBase.ShouldBe(Grade.G);
      positionCapabilities.ThirdBase.ShouldBe(Grade.G);
      positionCapabilities.Shortstop.ShouldBe(Grade.G);
      positionCapabilities.LeftField.ShouldBe(Grade.G);
      positionCapabilities.CenterField.ShouldBe(Grade.G);
      positionCapabilities.RightField.ShouldBe(Grade.G);

      var hitterAbilities = resultPlayer.HitterAbilities;
      hitterAbilities.Trajectory.ShouldBe(2);
      hitterAbilities.Contact.ShouldBe(1);
      hitterAbilities.Power.ShouldBe(16);
      hitterAbilities.RunSpeed.ShouldBe(4);
      hitterAbilities.ArmStrength.ShouldBe(10);
      hitterAbilities.Fielding.ShouldBe(6);
      hitterAbilities.ErrorResistance.ShouldBe(5);

      var pitcherAbilities = resultPlayer.PitcherAbilities;
      pitcherAbilities.Control.ShouldBe(138);
      pitcherAbilities.Stamina.ShouldBe(199);
      pitcherAbilities.TopSpeedMph.ShouldBeInRange(94, 95);
      pitcherAbilities.Slider1Movement.ShouldBe(4);
      pitcherAbilities.Curve1Movement.ShouldBe(3);
      pitcherAbilities.Fork1Movement.ShouldBe(2);
    }
    
    /*
    [Test]
    public void LSStatisticsAlgorithm_Test()
    {
      Task.Run(async () =>
      {
        var searchResult = await _mlbLookupApiClient.SearchPlayer("Myles Straw");
        var player = searchResult.Results.Single(p => p.IsActive);
        var result = _playerGenerator.GeneratePlayer(player.LSPlayerId, 2021, new LSStatistcsPlayerGenerationAlgorithm(_voiceLibrary, _skinColorGuesser));
        result.HitterAbilities.ErrorResistance.ShouldBe(12);
      }).GetAwaiter().GetResult();
    }

    */
  }
}
