using NSubstitute;
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

    [Test]
    public void MapToGSPlayer_ShouldMapContactHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.ContactHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsContactHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPowerHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.PowerHitter= true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsPowerHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSlugOrSlapHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.SluggerOrSlapHitter = SluggerOrSlapHitter.SlapHitter;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SlugOrSlap!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPushHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.PushHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsPushHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPullHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.PullHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsPullHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSprayHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.SprayHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsSprayHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFirstballHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.FirstballHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsFirstballHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapAggressiveOrPatientHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.AggressiveOrPatientHitter = AggressiveOrPatient.Aggressive;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.AggressiveOrPatientHitter!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapRefined()
    {
      player.SpecialAbilities.Hitter.HittingApproach.Refined = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsRefinedHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapToughOut()
    {
      player.SpecialAbilities.Hitter.HittingApproach.ToughOut = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsToughOut!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapIntimidatingHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.Intimidator = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsIntimidatingHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSmallBall()
    {
      player.SpecialAbilities.Hitter.SmallBall.SmallBall = SpecialPositive_Negative.Negative;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SmallBall!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBunting()
    {
      player.SpecialAbilities.Hitter.SmallBall.Bunting = BuntingAbility.Good;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Bunting!.Value.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapInfielderHitting()
    {
      player.SpecialAbilities.Hitter.SmallBall.InfieldHitting = InfieldHittingAbility.GreatInfieldHitter;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.InfieldHitter!.Value.ShouldBe((ushort)2);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBaseRunning()
    {
      player.SpecialAbilities.Hitter.BaseRunning.BaseRunning = Special2_4.Two;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.BaseRunning!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapStealing()
    {
      player.SpecialAbilities.Hitter.BaseRunning.Stealing = Special2_4.Four;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Stealing!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapAggressiveBaserunner()
    {
      player.SpecialAbilities.Hitter.BaseRunning.AggressiveRunner = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsAggressiveBaserunner!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapAggressiveOrCautiousBaseStealer()
    {
      player.SpecialAbilities.Hitter.BaseRunning.AggressiveOrPatientBaseStealer = AggressiveOrPatient.Patient;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.AggressiveOrCautiousBaseStealer!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapToughRunner()
    {
      player.SpecialAbilities.Hitter.BaseRunning.ToughRunner = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsToughRunner!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBreakupDoublePlay()
    {
      player.SpecialAbilities.Hitter.BaseRunning.BreakupDoublePlay = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.WillBreakupDoublePlay!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapWillSlideHeadFirst()
    {
      player.SpecialAbilities.Hitter.BaseRunning.HeadFirstSlide = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.WillSlideHeadFirst!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGoldGlover()
    {
      player.SpecialAbilities.Hitter.Fielding.GoldGlover = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsGoldGlover!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSpiderCatch()
    {
      player.SpecialAbilities.Hitter.Fielding.SpiderCatch= true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.CanSpiderCatch!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBarehandCatch()
    {
      player.SpecialAbilities.Hitter.Fielding.BarehandCatch = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.CanBarehandCatch!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapAggressiveFielder()
    {
      player.SpecialAbilities.Hitter.Fielding.AggressiveFielder = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsAggressiveFielder!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPivotMan()
    {
      player.SpecialAbilities.Hitter.Fielding.PivotMan = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsPivotMan!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapErrorProne()
    {
      player.SpecialAbilities.Hitter.Fielding.ErrorProne = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsErrorProne!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGoodBlocker()
    {
      player.SpecialAbilities.Hitter.Fielding.GoodBlocker = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsGoodBlocker!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCatching()
    {
      player.SpecialAbilities.Hitter.Fielding.CatchingAbility = CatchingAbility.GoodCatcher;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Catching!.Value.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapThrowing()
    {
      player.SpecialAbilities.Hitter.Fielding.Throwing = Special2_4.Four;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Throwing!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHasCannonArm()
    {
      player.SpecialAbilities.Hitter.Fielding.CannonArm = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HasCannonArm!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapTrashTalker()
    {
      player.SpecialAbilities.Hitter.Fielding.TrashTalk = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsTrashTalker!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPitchingConsistency()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.Consistency = Special2_4.Two;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.PitchingConsistency!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapVersusLeftHandedBatter()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.VersusLefty = Special2_4.Four;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.VersusLeftHandedBatter!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPoise()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.Poise = Special2_4.Two;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Poise!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPoorVersusRunner()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.PoorVersusRunner = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.PoorVersusRunner!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapWithRunnersInScoringPosition()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.WithRunnersInSocringPosition = Special2_4.Four;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.WithRunnersInScoringPosition!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapIsSlowStarter()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.SlowStarter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsSlowStarter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapIsStarterFinisher()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.StarterFinisher = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsStarterFinisher!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapChokeArtist()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.ChokeArtist = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsChokeArtist!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSandbag()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.Sandbag = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsSandbag!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapDoctorK()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.DoctorK = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.DoctorK!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapWalkProne()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.Walk = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.WalkProne!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapLucky()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.Lucky = SpecialPositive_Negative.Negative;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Luck!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapRecovery()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.Recovery = Special2_4.Four;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Recovery!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPitchingIntimidator()
    {
      player.SpecialAbilities.Pitcher.Demeanor.Intimidator = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsIntimidatingPitcher!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBattler()
    {
      player.SpecialAbilities.Pitcher.Demeanor.BattlerPokerFace = BattlerPokerFace.Battler;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsBattler!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPokerFace()
    {
      player.SpecialAbilities.Pitcher.Demeanor.BattlerPokerFace = BattlerPokerFace.PokerFace;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HasPokerFace!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotHead()
    {
      player.SpecialAbilities.Pitcher.Demeanor.HotHead = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsHotHead!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGoodDelivery()
    {
      player.SpecialAbilities.Pitcher.PitchingMechanics.GoodDelivery = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.GoodDelivery!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapRelease()
    {
      player.SpecialAbilities.Pitcher.PitchingMechanics.Release = Special2_4.Two;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Release!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGoodPace()
    {
      player.SpecialAbilities.Pitcher.PitchingMechanics.GoodPace = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HasGoodPace!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGoodReflexes()
    {
      player.SpecialAbilities.Pitcher.PitchingMechanics.GoodReflexes = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HasGoodReflexes!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGoodPickoff()
    {
      player.SpecialAbilities.Pitcher.PitchingMechanics.GoodPickoff = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.GoodPickoff!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPowerOrBreakingBallPitcher()
    {
      player.SpecialAbilities.Pitcher.PitchQuailities.PowerOrBreakingBallPitcher = PowerOrBreakingBallPitcher.Power;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.PowerOrBreakingBallPitcher!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFastballLife()
    {
      player.SpecialAbilities.Pitcher.PitchQuailities.FastballLife = Special2_4.Four;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.FastballLife!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSpin()
    {
      player.SpecialAbilities.Pitcher.PitchQuailities.Spin = Special2_4.Two;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Spin!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSafeOrFatPitch()
    {
      player.SpecialAbilities.Pitcher.PitchQuailities.SafeOrFatPitch = SpecialPositive_Negative.Positive;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SafeOrFatPitch!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGroundBallOrFlyBallPitcher()
    {
      player.SpecialAbilities.Pitcher.PitchQuailities.GroundBallOrFlyBallPitcher = SpecialPositive_Negative.Negative;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.GroundBallOrFlyBallPitcher!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGoodLowPitch()
    {
      player.SpecialAbilities.Pitcher.PitchQuailities.GoodLowPitch = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.GoodLowPitch!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGyroball()
    {
      player.SpecialAbilities.Pitcher.PitchQuailities.Gyroball = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Gyroball!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapShuttoSpin()
    {
      player.SpecialAbilities.Pitcher.PitchQuailities.ShuttoSpin = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.ShuttoSpin!.Value.ShouldBe(true);
    }
  }
}

