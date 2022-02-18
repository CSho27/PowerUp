using NUnit.Framework;
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
    [TestCase(PlayerType.Base)]
    [TestCase(PlayerType.Imported)]
    [TestCase(PlayerType.Generated)]
    [TestCase(PlayerType.Custom)]
    public void MapToGSPlayer_ShouldBeMarkedAsEditedForCustomPlayers(PlayerType playerType)
    {
      player.PlayerType = playerType;
      var result = player.MapToGSPlayer();
      result.IsEdited.ShouldBe(playerType == PlayerType.Custom);
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
    public void MapToGSPlayer_ShouldMapHotZoneDownAndAwat()
    {
      player.HitterAbilities.HotZones.DownAndAway = HotZonePreference.Cold;
      var result = player.MapToGSPlayer();
      result.HotZoneDownAndAway.ShouldBe((ushort)3);
    }
  }
}
