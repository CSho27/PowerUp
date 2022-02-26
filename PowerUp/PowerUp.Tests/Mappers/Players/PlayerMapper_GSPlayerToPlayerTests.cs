using NSubstitute;
using NUnit.Framework;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using PowerUp.Mappers.Players;
using Shouldly;
using System;

namespace PowerUp.Tests.Mappers.Players
{
  public class PlayerMapper_GSPlayerToPlayerTests
  {
    private PlayerMapper playerMapper;
    private PlayerMappingParameters mappingParameters;
    private GSPlayer gsPlayer;

    [SetUp]
    public void SetUp()
    {
      playerMapper = new PlayerMapper(Substitute.For<ISpecialSavedNameLibrary>());

      mappingParameters = new PlayerMappingParameters
      {
        IsBase = false,
        ImportSource = "Roster1",
      };

      gsPlayer = new GSPlayer
      {
        PowerProsId = 1,
        IsEdited = false,
        SavedName = "Charlie",
        PrimaryPosition = 8,
        IsStarter = false,
        IsReliever = false,
        IsCloser = false,
        VoiceId = 0,
        BattingSide = 0,
        BattingForm = 0,
        ThrowsLefty = false,
        PitchingForm = 0,
        PitcherCapability = 0,
        CatcherCapability = 0,
        FirstBaseCapability = 0,
        SecondBaseCapability = 0,
        ThirdBaseCapability = 0,
        ShortstopCapability = 0,
        LeftFieldCapability = 0,
        CenterFieldCapability = 0,
        RightFieldCapability = 0,
        Trajectory = 0,
        Contact = 0,
        Power = 0,
        RunSpeed = 0,
        ArmStrength = 0,
        Fielding = 0,
        ErrorResistance = 0,
        HotZoneUpAndIn = 0,
        HotZoneUp = 0,
        HotZoneUpAndAway = 0,
        HotZoneMiddleIn = 0,
        HotZoneMiddle = 0,
        HotZoneMiddleAway = 0,
        HotZoneDownAndIn = 0,
        HotZoneDown = 0,
        HotZoneDownAndAway = 0,
        TopThrowingSpeedKMH = 0,
        Control = 0,
        Stamina = 0,
        Slider1Type = 0,
        Slider1Movement = 0,
        Slider2Type = 0,
        Slider2Movement = 0,
        Curve1Type = 0,
        Curve1Movement = 0,
        Curve2Type = 0,
        Curve2Movement = 0,
        Fork1Type = 0,
        Fork1Movement = 0,
        Fork2Type = 0,
        Fork2Movement = 0,
        Sinker1Type = 0,
        Sinker1Movement = 0,
        Sinker2Type = 0,
        Sinker2Movement = 0,
        SinkingFastball1Type = 0,
        SinkingFastball1Movement = 0,
        SinkingFastball2Type = 0,
        SinkingFastball2Movement = 0,
      };
    }

    [Test]
    public void MapToPlayer_ShouldMapLastName()
    {
      gsPlayer.LastName = "Sizemore";
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.LastName.ShouldBe("Sizemore");
    }

    [Test]
    public void MapToPlayer_ShouldMapFirstName()
    {
      gsPlayer.FirstName = "Grady";
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.FirstName.ShouldBe("Grady");
    }

    [Test]
    public void MapToPlayer_ShouldMapSavedName()
    {
      gsPlayer.SavedName = "Sizemore";
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SavedName.ShouldBe("Sizemore");
    }

    [Test]
    [TestCase(true, EntitySourceType.Base)]
    [TestCase(false, EntitySourceType.Imported)]
    public void MapToPlayer_ShouldUseTypeFromParameters(bool isBase, EntitySourceType sourceType)
    {
      mappingParameters.IsBase = isBase;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SourceType.ShouldBe(sourceType);
    }

    [Test]
    [TestCase(true, "RosterBase", null)]
    [TestCase(false, "Roster1", "Roster1")]
    public void MapToPlayer_ShouldIncludeImportSourceOnlyForImported(bool isBase, string importSource, string expectedResult)
    {
      mappingParameters.IsBase = isBase;
      mappingParameters.ImportSource = importSource;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.ImportSource.ShouldBe(expectedResult);
    }

    [Test]
    public void MapToPlayer_ShouldMapSourcePowerProsId()
    {
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SourcePowerProsId.ShouldBe(1);
    }

    [Test]
    [TestCase(false)]
    [TestCase(true)]
    public void MapToPlayer_EditedPlayersShouldBeCustomType(bool isBase)
    {
      gsPlayer.IsEdited = true;
      mappingParameters.IsBase = isBase;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SourceType.ShouldBe(EntitySourceType.Custom);
    }

    [Test]
    [TestCase((ushort)0, (ushort)1, "0")]
    [TestCase((ushort)12, (ushort)2, "12")]
    [TestCase((ushort)99, (ushort)3, "099")]
    [TestCase((ushort)999, (ushort)3, "999")]
    public void MapToPlayer_ShouldMapUniformNumber(ushort numberValue, ushort numberDigits, string expectedUniformNumber)
    {
      gsPlayer.PlayerNumber = numberValue;
      gsPlayer.PlayerNumberNumberOfDigits = numberDigits;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.UniformNumber.ShouldBe(expectedUniformNumber);
    }

    [Test]
    public void MapToPlayer_ShouldMapPrimaryPosition()
    {
      gsPlayer.PrimaryPosition = 8;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PrimaryPosition.ShouldBe(Position.CenterField);
    }

    [Test]
    [TestCase(false, false, false, PitcherType.SwingMan)]
    [TestCase(true, false, false, PitcherType.Starter)]
    [TestCase(false, true, false, PitcherType.Reliever)]
    [TestCase(false, false, true, PitcherType.Closer)]
    public void MapToPlayer_ShouldMapPitcherType(bool isStarter, bool isReliever, bool isCloser, PitcherType expectedPitcherType)
    {
      gsPlayer.IsStarter = isStarter;
      gsPlayer.IsReliever = isReliever;
      gsPlayer.IsCloser = isCloser;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherType.ShouldBe(expectedPitcherType);
    }

    [Test]
    public void MapToPlayer_ShouldMapVoiceId()
    {
      gsPlayer.VoiceId = 35038;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.VoiceId.ShouldBe(35038);
    }

    [Test]
    public void MapToPlayer_ShouldMapBattingSide()
    {
      gsPlayer.BattingSide = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.BattingSide.ShouldBe(BattingSide.Switch);
    }

    [Test]
    public void MapToPlayer_ShouldMapBattingStanceId()
    {
      gsPlayer.BattingForm = 3;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.BattingStanceId.ShouldBe(3);
    }

    [Test]
    public void MapToPlayer_ShouldMapThrowingSide()
    {
      gsPlayer.ThrowsLefty = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.ThrowingSide.ShouldBe(ThrowingSide.Left);
    }

    [Test]
    public void MapToPlayer_ShouldMapThrowingMechanicsId()
    {
      gsPlayer.PitchingForm = 3;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitchingMechanicsId.ShouldBe(3);
    }

    [Test]
    public void MapToPlayer_ShouldMapPitcherCapability()
    {
      gsPlayer.PitcherCapability = 3;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositonCapabilities.Pitcher.ShouldBe(Grade.E);
    }

    [Test]
    public void MapToPlayer_ShouldMapCatcherCapability()
    {
      gsPlayer.CatcherCapability = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositonCapabilities.Catcher.ShouldBe(Grade.F);
    }

    [Test]
    public void MapToPlayer_ShouldMapFirstBaseCapability()
    {
      gsPlayer.FirstBaseCapability = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositonCapabilities.FirstBase.ShouldBe(Grade.G);
    }

    [Test]
    public void MapToPlayer_ShouldMapSecondBaseCapability()
    {
      gsPlayer.SecondBaseCapability = 4;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositonCapabilities.SecondBase.ShouldBe(Grade.D);
    }

    [Test]
    public void MapToPlayer_ShouldMapThirdBaseCapability()
    {
      gsPlayer.ThirdBaseCapability = 5;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositonCapabilities.ThirdBase.ShouldBe(Grade.C);
    }

    [Test]
    public void MapToPlayer_ShouldMapShortstopCapability()
    {
      gsPlayer.ShortstopCapability = 6;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositonCapabilities.Shortstop.ShouldBe(Grade.B);
    }

    [Test]
    public void MapToPlayer_ShouldMapLeftFieldCapability()
    {
      gsPlayer.LeftFieldCapability = 7;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositonCapabilities.LeftField.ShouldBe(Grade.A);
    }

    [Test]
    public void MapToPlayer_ShouldMapCenterFieldCapability()
    {
      gsPlayer.CenterFieldCapability = 6;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositonCapabilities.CenterField.ShouldBe(Grade.B);
    }

    [Test]
    public void MapToPlayer_ShouldMapRightFieldCapability()
    {
      gsPlayer.RightFieldCapability = 5;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositonCapabilities.RightField.ShouldBe(Grade.C);
    }

    [Test]
    public void MapToPlayer_ShouldMapTrajectory()
    {
      gsPlayer.Trajectory = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.Trajectory.ShouldBe(3);
    }

    [Test]
    public void MapToPlayer_ShouldMapContact()
    {
      gsPlayer.Contact = 8;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.Contact.ShouldBe(8);
    }

    [Test]
    public void MapToPlayer_ShouldMapPower()
    {
      gsPlayer.Power = 222;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.Power.ShouldBe(222);
    }

    [Test]
    public void MapToPlayer_ShouldMapRunSpeed()
    {
      gsPlayer.RunSpeed = 5;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.RunSpeed.ShouldBe(5);
    }

    [Test]
    public void MapToPlayer_ShouldMapArmStrength()
    {
      gsPlayer.ArmStrength = 12;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.ArmStrength.ShouldBe(12);
    }

    [Test]
    public void MapToPlayer_ShouldMapFielding()
    {
      gsPlayer.Fielding = 10;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.Fielding.ShouldBe(10);
    }

    [Test]
    public void MapToPlayer_ShouldMapErrorResistance()
    {
      gsPlayer.ErrorResistance = 4;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.ErrorResistance.ShouldBe(4);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotZoneUpandIn()
    {
      gsPlayer.HotZoneUpAndIn = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.HotZones.UpAndIn.ShouldBe(HotZonePreference.Hot);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotZoneUp()
    {
      gsPlayer.HotZoneUp = 3;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.HotZones.Up.ShouldBe(HotZonePreference.Cold);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotZoneUpAndAway()
    {
      gsPlayer.HotZoneUpAndAway = 0;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.HotZones.UpAndAway.ShouldBe(HotZonePreference.Neutral);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotZoneMiddleIn()
    {
      gsPlayer.HotZoneMiddleIn = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.HotZones.MiddleIn.ShouldBe(HotZonePreference.Hot);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotZoneMiddle()
    {
      gsPlayer.HotZoneMiddle = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.HotZones.Middle.ShouldBe(HotZonePreference.Hot);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotZoneMiddleAway()
    {
      gsPlayer.HotZoneMiddleAway = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.HotZones.MiddleAway.ShouldBe(HotZonePreference.Hot);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotZoneDownAndIn()
    {
      gsPlayer.HotZoneDownAndIn = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.HotZones.DownAndIn.ShouldBe(HotZonePreference.Hot);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotZoneDown()
    {
      gsPlayer.HotZoneDown = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.HotZones.Down.ShouldBe(HotZonePreference.Hot);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotZoneDownAndAway()
    {
      gsPlayer.HotZoneDownAndAway = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HitterAbilities.HotZones.DownAndAway.ShouldBe(HotZonePreference.Hot);
    }

    [Test]
    [TestCase((ushort)120, 74)]
    [TestCase((ushort)141, 87)]
    [TestCase((ushort)169, 105)]
    public void MapToPlayer_ShouldMapTopSpeedMph(ushort kmh, int mph)
    {
      gsPlayer.TopThrowingSpeedKMH = kmh;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      var roundedTopSpeed = Math.Floor(result.PitcherAbilities.TopSpeedMph);
      roundedTopSpeed.ShouldBe(mph);
    }

    [Test]
    public void MapToPlayer_ShouldMapControl()
    {
      gsPlayer.Control = 178;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Control.ShouldBe(178);
    }

    [Test]
    public void MapToPlayer_ShouldMapStamina()
    {
      gsPlayer.Stamina = 150;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Stamina.ShouldBe(150);
    }

    [Test]
    public void MapToPlayer_ShouldMapHasTwoSeam()
    {
      gsPlayer.TwoSeamType = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.HasTwoSeam.ShouldBeTrue();
    }

    [Test]
    public void MapToPlayer_ShouldMapTwoSeamMovement()
    {
      gsPlayer.TwoSeamMovement = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.TwoSeamMovement.ShouldBe(2);
    }

    [Test]
    public void MapToPlayer_ShouldMapSlider1Type()
    {
      gsPlayer.Slider1Type = 4;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Slider1Type.ShouldBe(SliderType.HardSlider);
    }

    [Test]
    public void MapToPlayer_ShouldMapSlider1Movement()
    {
      gsPlayer.Slider1Movement = 7;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Slider1Movement.ShouldBe(7);
    }

    [Test]
    public void MapToPlayer_ShouldMapSlider2Type()
    {
      gsPlayer.Slider2Type = 3;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Slider2Type.ShouldBe(SliderType.Slider);
    }

    [Test]
    public void MapToPlayer_ShouldMapSlider2Movement()
    {
      gsPlayer.Slider2Movement = 4;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Slider2Movement.ShouldBe(4);
    }

    [Test]
    public void MapToPlayer_ShouldMapCurve1Type()
    {
      gsPlayer.Curve1Type = 9;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Curve1Type.ShouldBe(CurveType.DropCurve);
    }

    [Test]
    public void MapToPlayer_ShouldMapCurve1Movement()
    {
      gsPlayer.Curve1Movement = 6;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Curve1Movement.ShouldBe(6);
    }

    [Test]
    public void MapToPlayer_ShouldMapCurve2Type()
    {
      gsPlayer.Curve2Type = 6;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Curve2Type.ShouldBe(CurveType.Curve);
    }

    [Test]
    public void MapToPlayer_ShouldMapCurve2Movement()
    {
      gsPlayer.Curve2Movement = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Curve2Movement.ShouldBe(2);
    }

    [Test]
    public void MapToPlayer_ShouldMapFork1Type()
    {
      gsPlayer.Fork1Type = 13;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Fork1Type.ShouldBe(ForkType.Palmball);
    }

    [Test]
    public void MapToPlayer_ShouldMapFork1Movement()
    {
      gsPlayer.Fork1Movement = 4;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Fork1Movement.ShouldBe(4);
    }

    [Test]
    public void MapToPlayer_ShouldMapFork2Type()
    {
      gsPlayer.Fork2Type = 17;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Fork2Type.ShouldBe(ForkType.Knuckleball);
    }

    [Test]
    public void MapToPlayer_ShouldMapFork2Movement()
    {
      gsPlayer.Fork2Movement = 7;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Fork2Movement.ShouldBe(7);
    }

    [Test]
    public void MapToPlayer_ShouldMapSinker1Type()
    {
      gsPlayer.Sinker1Type = 20;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Sinker1Type.ShouldBe(SinkerType.Sinker);
    }

    [Test]
    public void MapToPlayer_ShouldMapSinker1Movement()
    {
      gsPlayer.Sinker1Movement = 4;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Sinker1Movement.ShouldBe(4);
    }

    [Test]
    public void MapToPlayer_ShouldMapSinker2Type()
    {
      gsPlayer.Sinker2Type = 22;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Sinker2Type.ShouldBe(SinkerType.Screwball);
    }

    [Test]
    public void MapToPlayer_ShouldMapSinker2Movement()
    {
      gsPlayer.Sinker2Movement = 7;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.Sinker2Movement.ShouldBe(7);
    }

    [Test]
    public void MapToPlayer_ShouldMapSinkingFastball1Type()
    {
      gsPlayer.SinkingFastball1Type = 25;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.SinkingFastball1Type.ShouldBe(SinkingFastballType.SinkingFastball);
    }

    [Test]
    public void MapToPlayer_ShouldMapSinkingFastball1Movement()
    {
      gsPlayer.SinkingFastball1Movement = 4;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.SinkingFastball1Movement.ShouldBe(4);
    }

    [Test]
    public void MapToPlayer_ShouldMapSinkingFastball2Type()
    {
      gsPlayer.SinkingFastball2Type = 23;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.SinkingFastball2Type.ShouldBe(SinkingFastballType.Shuuto);
    }

    [Test]
    public void MapToPlayer_ShouldMapSinkingFastball2Movement()
    {
      gsPlayer.SinkingFastball2Movement = 7;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitcherAbilities.SinkingFastball2Movement.ShouldBe(7);
    }
  }
}
