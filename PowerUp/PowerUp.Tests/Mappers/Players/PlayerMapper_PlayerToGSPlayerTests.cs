﻿using NSubstitute;
using NUnit.Framework;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Libraries;
using PowerUp.Mappers.Players;
using Shouldly;

namespace PowerUp.Tests.Mappers.Players
{
  public class PlayerMapper_PlayerToGSPlayerTests
  {
    private Player player;
    private PlayerMapper playerMapper;

    [SetUp]
    public void SetUp()
    {
      player = new Player() { UniformNumber = "24" };
      playerMapper = new PlayerMapper(Substitute.For<ISpecialSavedNameLibrary>());
    }

    [Test]
    public void MapToGSPlayer_ShouldMapLastName()
    {
      player.LastName = "Sizemore";
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.LastName.ShouldBe("Sizemore");
    }

    [Test]
    public void MapToGsPlayer_ShouldMapFirstName()
    {
      player.FirstName = "Grady";
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.FirstName.ShouldBe("Grady");
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSavedName()
    {
      player.SavedName = "Sizemore";
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SavedName.ShouldBe("Sizemore");
    }

    [Test]
    [TestCase(EntitySourceType.Base)]
    [TestCase(EntitySourceType.Imported)]
    [TestCase(EntitySourceType.Generated)]
    [TestCase(EntitySourceType.Custom)]
    public void MapToGSPlayer_ShouldBeMarkedAsEditedForCustomPlayers(EntitySourceType playerType)
    {
      player.SourceType = playerType;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsEdited.ShouldBe(playerType == EntitySourceType.Custom);
    }

    [Test]
    [TestCase("0", (ushort)0, (ushort)1)]
    [TestCase("12", (ushort)12, (ushort)2)]
    [TestCase("099", (ushort)99, (ushort)3)]
    [TestCase("999", (ushort)999, (ushort)3)]
    public void MapToGSPlayer_ShoudMapUniformNumber(string uniformNumber, ushort expectedNumberValue, ushort expectedNumberDigits)
    {
      player.UniformNumber = uniformNumber;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.PlayerNumber.ShouldBe(expectedNumberValue);
      result.PlayerNumberNumberOfDigits.ShouldBe(expectedNumberDigits);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPrimaryPosition()
    {
      player.PrimaryPosition = Position.CenterField;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.PrimaryPosition.ShouldBe((ushort)8);
    }

    [Test]
    [TestCase(PitcherType.SwingMan, false, false, false)]
    [TestCase(PitcherType.Starter, true, false, false)]
    [TestCase(PitcherType.Reliever, false, true, false)]
    [TestCase(PitcherType.Closer, false, false, true)]
    public void MapToPlayer_ShouldMapPitcherType(PitcherType pitcherType, bool isStarter, bool isReliever, bool isCloser)
    {
      player.PitcherType = pitcherType;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsStarter.ShouldBe(isStarter);
      result.IsReliever.ShouldBe(isReliever);
      result.IsCloser.ShouldBe(isCloser);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapVoiceId()
    {
      player.VoiceId = 35038;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.VoiceId.ShouldBe((ushort)35038);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBattingSide()
    {
      player.BattingSide = BattingSide.Left;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.BattingSide.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBattingStanceId()
    {
      player.BattingStanceId = 3;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.BattingForm.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapThrowingSide()
    {
      player.ThrowingArm = ThrowingArm.Left;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.ThrowsLefty.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPitchingMechanicsId()
    {
      player.PitchingMechanicsId = 3;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.PitchingForm.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPitcherCapability()
    {
      player.PositonCapabilities.Pitcher = Grade.G;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.PitcherCapability.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCatcherCapability()
    {
      player.PositonCapabilities.Catcher = Grade.F;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.CatcherCapability.ShouldBe((ushort)2);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFirstBaseCapability()
    {
      player.PositonCapabilities.FirstBase = Grade.E;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.FirstBaseCapability.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSecondBaseCapability()
    {
      player.PositonCapabilities.SecondBase = Grade.D;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SecondBaseCapability.ShouldBe((ushort)4);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapThirdBaseCapability()
    {
      player.PositonCapabilities.ThirdBase = Grade.C;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.ThirdBaseCapability.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapShortstopCapability()
    {
      player.PositonCapabilities.Shortstop = Grade.B;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.ShortstopCapability.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapLeftFieldCapability()
    {
      player.PositonCapabilities.LeftField = Grade.A;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.LeftFieldCapability.ShouldBe((ushort)7);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCenterFieldCapability()
    {
      player.PositonCapabilities.CenterField = Grade.B;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.CenterFieldCapability.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapRightFieldCapability()
    {
      player.PositonCapabilities.RightField = Grade.C;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.RightFieldCapability.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapTrajectory()
    {
      player.HitterAbilities.Trajectory = 3;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Trajectory.ShouldBe((ushort)2);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapContact()
    {
      player.HitterAbilities.Contact = 9;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Contact.ShouldBe((ushort)9);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPower()
    {
      player.HitterAbilities.Power = 156;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Power.ShouldBe((ushort)156);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapRunSpeed()
    {
      player.HitterAbilities.RunSpeed = 6;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.RunSpeed.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapArmStrength()
    {
      player.HitterAbilities.ArmStrength = 10;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.ArmStrength.ShouldBe((ushort)10);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFielding()
    {
      player.HitterAbilities.Fielding = 5;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Fielding.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapErrorResistance()
    {
      player.HitterAbilities.ErrorResistance = 7;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.ErrorResistance.ShouldBe((ushort)7);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneUpAndIn()
    {
      player.HitterAbilities.HotZones.UpAndIn = HotZonePreference.Cold;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HotZoneUpAndIn.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneUp()
    {
      player.HitterAbilities.HotZones.Up = HotZonePreference.Cold;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HotZoneUp.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneUpAndAway()
    {
      player.HitterAbilities.HotZones.UpAndAway = HotZonePreference.Cold;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HotZoneUpAndAway.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneMiddleIn()
    {
      player.HitterAbilities.HotZones.MiddleIn = HotZonePreference.Cold;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HotZoneMiddleIn.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneMiddle()
    {
      player.HitterAbilities.HotZones.Middle = HotZonePreference.Hot;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HotZoneMiddle.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneMiddleAway()
    {
      player.HitterAbilities.HotZones.MiddleAway = HotZonePreference.Neutral;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HotZoneMiddleAway.ShouldBe((ushort)0);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneDwonAndIn()
    {
      player.HitterAbilities.HotZones.DownAndIn = HotZonePreference.Cold;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HotZoneDownAndIn.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneDown()
    {
      player.HitterAbilities.HotZones.Down = HotZonePreference.Cold;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HotZoneDown.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneDownAndAway()
    {
      player.HitterAbilities.HotZones.DownAndAway = HotZonePreference.Cold;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HotZoneDownAndAway.ShouldBe((ushort)3);
    }

    [Test]
    [TestCase(74.5645428, (ushort)120)]
    [TestCase(87.61333779, (ushort)141)]
    [TestCase(105.0117311, (ushort)169)]
    public void MapToGSPlayer_ShouldMapTopSpeedMph(double mph, ushort kmh)
    {
      player.PitcherAbilities.TopSpeedMph = mph;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.TopThrowingSpeedKMH.ShouldBe(kmh);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapControl()
    {
      player.PitcherAbilities.Control = 120;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Control.ShouldBe((ushort)120);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapStamina()
    {
      player.PitcherAbilities.Stamina = 78;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Stamina.ShouldBe((ushort)78);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHasTwoSeam()
    {
      player.PitcherAbilities.HasTwoSeam = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.TwoSeamType.ShouldBe((ushort)2);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHasTwoSeamMovement()
    {
      player.PitcherAbilities.TwoSeamMovement = 3;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.TwoSeamMovement.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSlider1Type()
    {
      player.PitcherAbilities.Slider1Type = SliderType.Cutter;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Slider1Type.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSlider1Movement()
    {
      player.PitcherAbilities.Slider1Movement = 1;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Slider1Movement.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSlider2Type()
    {
      player.PitcherAbilities.Slider2Type = SliderType.Slider;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Slider2Type.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSlider2Movement()
    {
      player.PitcherAbilities.Slider2Movement = 2;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Slider2Movement.ShouldBe((ushort)2);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCurve1Type()
    {
      player.PitcherAbilities.Curve1Type = CurveType.SlowCurve;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Curve1Type.ShouldBe((ushort)7);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCurve1Movement()
    {
      player.PitcherAbilities.Curve1Movement = 3;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Curve1Movement.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCurve2Type()
    {
      player.PitcherAbilities.Curve2Type = CurveType.KnuckleCurve;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Curve2Type.ShouldBe((ushort)11);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCurve2Movement()
    {
      player.PitcherAbilities.Curve2Movement = 4;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Curve2Movement.ShouldBe((ushort)4);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFork1Type()
    {
      player.PitcherAbilities.Fork1Type = ForkType.Forkball;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Fork1Type.ShouldBe((ushort)12);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFork1Movement()
    {
      player.PitcherAbilities.Fork1Movement = 5;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Fork1Movement.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFork2Type()
    {
      player.PitcherAbilities.Fork2Type = ForkType.Foshball;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Fork2Type.ShouldBe((ushort)19);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFork2Movement()
    {
      player.PitcherAbilities.Fork2Movement = 6;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Fork2Movement.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinker1Type()
    {
      player.PitcherAbilities.Sinker1Type = SinkerType.Sinker;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Sinker1Type.ShouldBe((ushort)20);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinker1Movement()
    {
      player.PitcherAbilities.Sinker1Movement = 7;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Sinker1Movement.ShouldBe((ushort)7);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinker2Type()
    {
      player.PitcherAbilities.Sinker2Type = SinkerType.HardSinker;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Sinker2Type.ShouldBe((ushort)21);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinker2Movement()
    {
      player.PitcherAbilities.Sinker2Movement = 6;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Sinker2Movement.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinkingFastball1Type()
    {
      player.PitcherAbilities.SinkingFastball1Type = SinkingFastballType.SinkingFastball;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SinkingFastball1Type.ShouldBe((ushort)25);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinkingFastball1Movement()
    {
      player.PitcherAbilities.SinkingFastball1Movement = 5;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SinkingFastball1Movement.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinkingFastball2Type()
    {
      player.PitcherAbilities.SinkingFastball2Type = SinkingFastballType.HardShuuto;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SinkingFastball2Type.ShouldBe((ushort)24);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinkingFastball2Movement()
    {
      player.PitcherAbilities.SinkingFastball2Movement = 4;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SinkingFastball2Movement.ShouldBe((ushort)4);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapIsStar()
    {
      player.SpecialAbilities.General.Star = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsStar!.Value.ShouldBe(true);
    }


    [Test]
    public void MapToGSPlayer_ShouldMapDurability()
    {
      player.SpecialAbilities.General.Durability = Special2_4.Four;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Durability!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapMorale()
    {
      player.SpecialAbilities.General.Morale = SpecialPositive_Negative.Negative;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Morale!.Value.ShouldBe((short)-1);
    }


    [Test]
    public void MapToGSPlayer_ShouldMapHittingConsistency()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.Consistency = Special2_4.Four;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HittingConsistency!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHittingVersusLefty()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.VersusLefty = Special1_5.Two;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HittingVersusLefty1!.Value.ShouldBe((short)-3);
      result.HittingVersusLefty2!.Value.ShouldBe((short)-3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapTableSetter()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.TableSetter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsTableSetter!.Value.ShouldBe(true);
    }


    [Test]
    public void MapToGSPlayer_ShouldMapBackToBackHitter()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.BackToBackHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsBackToBackHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotHitter()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.HotHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsHotHitter!.Value.ShouldBe(true);
    }
  }
}
