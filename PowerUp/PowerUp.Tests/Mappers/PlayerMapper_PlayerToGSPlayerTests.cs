using NUnit.Framework;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Mappers;
using Shouldly;

namespace PowerUp.Tests.Mappers
{
  public class PlayerMapper_PlayerToGSPlayerTests
  {
    private Player player;

    [SetUp]
    public void SetUp()
    {
      player = new Player() { UniformNumber = "24" };
    }

    [Test]
    public void MapToGSPlayer_ShouldMapLastName()
    {
      player.LastName = "Sizemore";
      var result = player.MapToGSPlayer();
      result.LastName.ShouldBe("Sizemore");
    }

    [Test]
    public void MapToGsPlayer_ShouldMapFirstName()
    {
      player.FirstName = "Grady";
      var result = player.MapToGSPlayer();
      result.FirstName.ShouldBe("Grady");
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSavedName()
    {
      player.SavedName = "Sizemore";
      var result = player.MapToGSPlayer();
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
      var result = player.MapToGSPlayer();
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
      var result = player.MapToGSPlayer();
      result.PlayerNumber.ShouldBe(expectedNumberValue);
      result.PlayerNumberNumberOfDigits.ShouldBe(expectedNumberDigits);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPrimaryPosition()
    {
      player.PrimaryPosition = Position.CenterField;
      var result = player.MapToGSPlayer();
      result.PrimaryPosition.ShouldBe((ushort)8);
    }

    [Test]
    [TestCase(PitcherType.SwingMan, false, false, false)]
    [TestCase(PitcherType.Starter, true, false, false)]
    [TestCase(PitcherType.Reliever, false, true, false)]
    [TestCase(PitcherType.Closer, false, false, true)]
    public void MapToPlayer_ShouldMapPitcherType(PitcherType pitcherType,bool isStarter, bool isReliever, bool isCloser)
    {
      player.PitcherType = pitcherType;
      var result = player.MapToGSPlayer();
      result.IsStarter.ShouldBe(isStarter);
      result.IsReliever.ShouldBe(isReliever);
      result.IsCloser.ShouldBe(isCloser);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapVoiceId()
    {
      player.VoiceId = 35038;
      var result = player.MapToGSPlayer();
      result.VoiceId.ShouldBe((ushort)35038);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBattingSide()
    {
      player.BattingSide = BattingSide.Left;
      var result = player.MapToGSPlayer();
      result.BattingSide.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBattingStanceId()
    {
      player.BattingStanceId = 3;
      var result = player.MapToGSPlayer();
      result.BattingForm.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapThrowingSide()
    {
      player.ThrowingSide = ThrowingSide.Left;
      var result = player.MapToGSPlayer();
      result.ThrowsLefty.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPitchingMechanicsId()
    {
      player.PitchingMechanicsId = 3;
      var result = player.MapToGSPlayer();
      result.PitchingForm.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPitcherCapability()
    {
      player.PositonCapabilities.Pitcher = Grade.G;
      var result = player.MapToGSPlayer();
      result.PitcherCapability.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCatcherCapability()
    {
      player.PositonCapabilities.Catcher = Grade.F;
      var result = player.MapToGSPlayer();
      result.CatcherCapability.ShouldBe((ushort)2);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFirstBaseCapability()
    {
      player.PositonCapabilities.FirstBase = Grade.E;
      var result = player.MapToGSPlayer();
      result.FirstBaseCapability.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSecondBaseCapability()
    {
      player.PositonCapabilities.SecondBase = Grade.D;
      var result = player.MapToGSPlayer();
      result.SecondBaseCapability.ShouldBe((ushort)4);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapThirdBaseCapability()
    {
      player.PositonCapabilities.ThirdBase = Grade.C;
      var result = player.MapToGSPlayer();
      result.ThirdBaseCapability.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapShortstopCapability()
    {
      player.PositonCapabilities.Shortstop = Grade.B;
      var result = player.MapToGSPlayer();
      result.ShortstopCapability.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapLeftFieldCapability()
    {
      player.PositonCapabilities.LeftField = Grade.A;
      var result = player.MapToGSPlayer();
      result.LeftFieldCapability.ShouldBe((ushort)7);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCenterFieldCapability()
    {
      player.PositonCapabilities.CenterField = Grade.B;
      var result = player.MapToGSPlayer();
      result.CenterFieldCapability.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapRightFieldCapability()
    {
      player.PositonCapabilities.RightField = Grade.C;
      var result = player.MapToGSPlayer();
      result.RightFieldCapability.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapTrajectory()
    {
      player.HitterAbilities.Trajectory = 3;
      var result = player.MapToGSPlayer();
      result.Trajectory.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapContact()
    {
      player.HitterAbilities.Contact = 9;
      var result = player.MapToGSPlayer();
      result.Contact.ShouldBe((ushort)9);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPower()
    {
      player.HitterAbilities.Power = 156;
      var result = player.MapToGSPlayer();
      result.Power.ShouldBe((ushort)156);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapRunSpeed()
    {
      player.HitterAbilities.RunSpeed = 6;
      var result = player.MapToGSPlayer();
      result.RunSpeed.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapArmStrength()
    {
      player.HitterAbilities.ArmStrength = 10;
      var result = player.MapToGSPlayer();
      result.ArmStrength.ShouldBe((ushort)10);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFielding()
    {
      player.HitterAbilities.Fielding = 5;
      var result = player.MapToGSPlayer();
      result.Fielding.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapErrorResistance()
    {
      player.HitterAbilities.ErrorResistance = 7;
      var result = player.MapToGSPlayer();
      result.ErrorResistance.ShouldBe((ushort)7);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneUpAndIn()
    {
      player.HitterAbilities.HotZones.UpAndIn = HotZonePreference.Cold;
      var result = player.MapToGSPlayer();
      result.HotZoneUpAndIn.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneUp()
    {
      player.HitterAbilities.HotZones.Up = HotZonePreference.Cold;
      var result = player.MapToGSPlayer();
      result.HotZoneUp.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneUpAndAway()
    {
      player.HitterAbilities.HotZones.UpAndAway = HotZonePreference.Cold;
      var result = player.MapToGSPlayer();
      result.HotZoneUpAndAway.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneMiddleIn()
    {
      player.HitterAbilities.HotZones.MiddleIn = HotZonePreference.Cold;
      var result = player.MapToGSPlayer();
      result.HotZoneMiddleIn.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneMiddle()
    {
      player.HitterAbilities.HotZones.Middle = HotZonePreference.Hot;
      var result = player.MapToGSPlayer();
      result.HotZoneMiddle.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneMiddleAway()
    {
      player.HitterAbilities.HotZones.MiddleAway = HotZonePreference.Neutral;
      var result = player.MapToGSPlayer();
      result.HotZoneMiddleAway.ShouldBe((ushort)0);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneDwonAndIn()
    {
      player.HitterAbilities.HotZones.DownAndIn = HotZonePreference.Cold;
      var result = player.MapToGSPlayer();
      result.HotZoneDownAndIn.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneDown()
    {
      player.HitterAbilities.HotZones.Down = HotZonePreference.Cold;
      var result = player.MapToGSPlayer();
      result.HotZoneDown.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotZoneDownAndAway()
    {
      player.HitterAbilities.HotZones.DownAndAway = HotZonePreference.Cold;
      var result = player.MapToGSPlayer();
      result.HotZoneDownAndAway.ShouldBe((ushort)3);
    }

    [Test]
    [TestCase(74.5645428, (ushort)120)]
    [TestCase(87.61333779, (ushort)141)]
    [TestCase(105.0117311, (ushort)169)]
    public void MapToGSPlayer_ShouldMapTopSpeedMph(double mph, ushort kmh)
    {
      player.PitcherAbilities.TopSpeedMph = mph;
      var result = player.MapToGSPlayer();
      result.TopThrowingSpeedKMH.ShouldBe(kmh);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapControl()
    {
      player.PitcherAbilities.Control = 120;
      var result = player.MapToGSPlayer();
      result.Control.ShouldBe((ushort)120);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapStamina()
    {
      player.PitcherAbilities.Stamina = 78;
      var result = player.MapToGSPlayer();
      result.Stamina.ShouldBe((ushort)78);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHasTwoSeam()
    {
      player.PitcherAbilities.HasTwoSeam = true;
      var result = player.MapToGSPlayer();
      result.TwoSeamType.ShouldBe((ushort)2);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHasTwoSeamMovement()
    {
      player.PitcherAbilities.TwoSeamMovement = 3;
      var result = player.MapToGSPlayer();
      result.TwoSeamMovement.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSlider1Type()
    {
      player.PitcherAbilities.Slider1Type = SliderType.Cutter;
      var result = player.MapToGSPlayer();
      result.Slider1Type.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSlider1Movement()
    {
      player.PitcherAbilities.Slider1Movement = 1;
      var result = player.MapToGSPlayer();
      result.Slider1Movement.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSlider2Type()
    {
      player.PitcherAbilities.Slider2Type = SliderType.Slider;
      var result = player.MapToGSPlayer();
      result.Slider2Type.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSlider2Movement()
    {
      player.PitcherAbilities.Slider2Movement = 2;
      var result = player.MapToGSPlayer();
      result.Slider2Movement.ShouldBe((ushort)2);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCurve1Type()
    {
      player.PitcherAbilities.Curve1Type = CurveType.SlowCurve;
      var result = player.MapToGSPlayer();
      result.Curve1Type.ShouldBe((ushort)7);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCurve1Movement()
    {
      player.PitcherAbilities.Curve1Movement = 3;
      var result = player.MapToGSPlayer();
      result.Curve1Movement.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCurve2Type()
    {
      player.PitcherAbilities.Curve2Type = CurveType.KnuckleCurve;
      var result = player.MapToGSPlayer();
      result.Curve2Type.ShouldBe((ushort)11);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCurve2Movement()
    {
      player.PitcherAbilities.Curve2Movement = 4;
      var result = player.MapToGSPlayer();
      result.Curve2Movement.ShouldBe((ushort)4);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFork1Type()
    {
      player.PitcherAbilities.Fork1Type = ForkType.Forkball;
      var result = player.MapToGSPlayer();
      result.Fork1Type.ShouldBe((ushort)12);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFork1Movement()
    {
      player.PitcherAbilities.Fork1Movement = 5;
      var result = player.MapToGSPlayer();
      result.Fork1Movement.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFork2Type()
    {
      player.PitcherAbilities.Fork2Type = ForkType.Foshball;
      var result = player.MapToGSPlayer();
      result.Fork2Type.ShouldBe((ushort)19);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFork2Movement()
    {
      player.PitcherAbilities.Fork2Movement = 6;
      var result = player.MapToGSPlayer();
      result.Fork2Movement.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinker1Type()
    {
      player.PitcherAbilities.Sinker1Type = SinkerType.Sinker;
      var result = player.MapToGSPlayer();
      result.Sinker1Type.ShouldBe((ushort)20);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinker1Movement()
    {
      player.PitcherAbilities.Sinker1Movement = 7;
      var result = player.MapToGSPlayer();
      result.Sinker1Movement.ShouldBe((ushort)7);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinker2Type()
    {
      player.PitcherAbilities.Sinker2Type = SinkerType.HardSinker;
      var result = player.MapToGSPlayer();
      result.Sinker2Type.ShouldBe((ushort)21);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinker2Movement()
    {
      player.PitcherAbilities.Sinker2Movement = 6;
      var result = player.MapToGSPlayer();
      result.Sinker2Movement.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinkingFastball1Type()
    {
      player.PitcherAbilities.SinkingFastball1Type = SinkingFastballType.SinkingFastball;
      var result = player.MapToGSPlayer();
      result.SinkingFastball1Type.ShouldBe((ushort)25);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinkingFastball1Movement()
    {
      player.PitcherAbilities.SinkingFastball1Movement = 5;
      var result = player.MapToGSPlayer();
      result.SinkingFastball1Movement.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinkingFastball2Type()
    {
      player.PitcherAbilities.SinkingFastball2Type = SinkingFastballType.HardShuuto;
      var result = player.MapToGSPlayer();
      result.SinkingFastball2Type.ShouldBe((ushort)24);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSinkingFastball2Movement()
    {
      player.PitcherAbilities.SinkingFastball2Movement = 4;
      var result = player.MapToGSPlayer();
      result.SinkingFastball2Movement.ShouldBe((ushort)4);
    }
  }
}
