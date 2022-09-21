using NSubstitute;
using NUnit.Framework;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Libraries;
using PowerUp.Mappers.Players;
using Shouldly;
using System;

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
    public void MapToGSPlayer_ShouldBeMarkedAsEditedForCustomPlayers()
    {
      player.IsCustomPlayer = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsEdited.ShouldBe(true);
      result.Unedited.ShouldBe(false);
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
    public void MapToGSPlayer_ShoudMapBirthDate()
    {
      player.BirthDate = new DateTime(1998, 4, 9);
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.BirthYear!.Value.ShouldBe((ushort)1998);
      result.BirthMonth!.Value.ShouldBe((ushort)4);
      result.BirthDay!.Value.ShouldBe((ushort)9);
    }

    [Test]
    public void MapToGSPlayer_ShoudMapYearsInMajors()
    {
      player.YearsInMajors = 25;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.YearsInMajors.ShouldBe((ushort)25);
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
    [TestCase(.179, (ushort)179)]
    [TestCase(null, (ushort)1023)]
    public void MapToGSPlayer_ShouldMapBattingAverage(double? battingAverage, ushort expectedBattingAveragePoints)
    {
      player.BattingAverage = battingAverage;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.BattingAveragePoints.ShouldBe(expectedBattingAveragePoints);
    }

    [Test]
    [TestCase(179, (ushort)179)]
    [TestCase(null, (ushort)1023)]
    public void MapToGSPlayer_ShouldMapRunsBattedIn(int? rbi, ushort expectedRbi)
    {
      player.RunsBattedIn = rbi;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.RunsBattedIn.ShouldBe(expectedRbi);
    }

    [Test]
    [TestCase(23, (ushort)23)]
    [TestCase(null, (ushort)1023)]
    public void MapToGSPlayer_ShouldMapHomeRuns(int? hr, ushort expectedHr)
    {
      player.HomeRuns = hr;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HomeRuns.ShouldBe(expectedHr);
    }

    [Test]
    [TestCase(3.18, (ushort)318)]
    [TestCase(null, (ushort)16383)]
    public void MapToGSPlayer_ShouldMapEarnedRunAverage(double? era, ushort expectedEra)
    {
      player.EarnedRunAverage = era;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.EarnedRunAverage.ShouldBe(expectedEra);
    }

    [Test]
    [TestCase(5, null, (ushort)5)]
    [TestCase(179, EyebrowThickness.Thick, (ushort)179)]
    [TestCase(179, EyebrowThickness.Thin, (ushort)197)]
    public void MapToGSPlayer_ShouldMapFaceId(int faceId, EyebrowThickness? eyebrowThickness, ushort ppFaceId)
    {
      player.Appearance.FaceId = faceId;
      player.Appearance.EyebrowThickness = eyebrowThickness;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Face.ShouldBe(ppFaceId);
    }

    [Test]
    [TestCase(null, null, (ushort)0)]
    [TestCase(SkinColor.One, EyeColor.Brown, (ushort)5)]
    [TestCase(SkinColor.One, EyeColor.Blue, (ushort)0)]
    [TestCase(SkinColor.Five, EyeColor.Brown, (ushort)9)]
    [TestCase(SkinColor.Five, EyeColor.Blue, (ushort)4)]
    public void MapToGSPlayer_ShouldMapSkinAndEyes(SkinColor? skin, EyeColor? eyeColor, ushort exptectedValue)
    {
      player.Appearance.SkinColor = skin;
      player.Appearance.EyeColor = eyeColor;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SkinAndEyes.ShouldBe(exptectedValue);
    }

    [Test]
    [TestCase(null, (ushort)0)]
    [TestCase(HairStyle.Basic, (ushort)15)]
    public void MapToGSPlayer_ShouldMapHairStyle(HairStyle? hairStyle, ushort expectedValue)
    {
      player.Appearance.HairStyle = hairStyle;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Hair.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(null, (ushort)0)]
    [TestCase(HairColor.Brown, (ushort)7)]
    public void MapToGSPlayer_ShouldMapHairColor(HairColor? hairColor, ushort expectedValue)
    {
      player.Appearance.HairColor = hairColor;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HairColor.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(null, (ushort)0)]
    [TestCase(FacialHairStyle.KlingonGoatee, (ushort)23)]
    public void MapToGSPlayer_ShouldMapFacialHairStyle(FacialHairStyle? hairStyle, ushort expectedValue)
    {
      player.Appearance.FacialHairStyle = hairStyle;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.FacialHair.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(null, (ushort)0)]
    [TestCase(HairColor.Brown, (ushort)7)]
    public void MapToGSPlayer_ShouldMapFacialHairColor(HairColor? hairColor, ushort expectedValue)
    {
      player.Appearance.FacialHairColor = hairColor;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.FacialHairColor.ShouldBe(expectedValue);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBatColor()
    {
      player.Appearance.BatColor = BatColor.Black_Natural;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Bat.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGloveColor()
    {
      player.Appearance.GloveColor = GloveColor.Brown;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.Glove.ShouldBe((ushort)4);
    }

    [Test]
    [TestCase(null, (ushort)0)]
    [TestCase(EyewearType.HalfJacket, (ushort)11)]
    public void MapToGSPlayer_ShouldMapEyewearType(EyewearType? eyewearType, ushort expectedValue)
    {
      player.Appearance.EyewearType = eyewearType;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.EyewearType.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(null, null, (ushort)0)]
    [TestCase(EyewearFrameColor.Black, EyewearLensColor.Clear, (ushort)0)]
    [TestCase(EyewearFrameColor.Black, EyewearLensColor.Orange, (ushort)1)]
    [TestCase(EyewearFrameColor.Red, EyewearLensColor.Orange, (ushort)7)]
    [TestCase(EyewearFrameColor.Gold, EyewearLensColor.Black, (ushort)14)]
    public void MapToGSPlayer_ShouldMapEyewearColor(EyewearFrameColor? frame, EyewearLensColor? lens, ushort exptectedValue)
    {
      player.Appearance.EyewearFrameColor = frame;
      player.Appearance.EyewearLensColor = lens;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.EyewearColor.ShouldBe(exptectedValue);
    }

    [Test]
    [TestCase(null, (ushort)0)]
    [TestCase(EarringSide.Both, (ushort)3)]
    public void MapToGSPlayer_ShouldMapEarringSide(EarringSide? earringSide, ushort expectedValue)
    {
      player.Appearance.EarringSide = earringSide;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.EarringSide.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(null, (ushort)0)]
    [TestCase(AccessoryColor.DarkBlue, (ushort)3)]
    public void MapToGSPlayer_ShouldMapEarringColor(AccessoryColor? earringColor, ushort expectedValue)
    {
      player.Appearance.EarringColor = earringColor;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.EarringColor.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(null, (ushort)0)]
    [TestCase(AccessoryColor.Pink, (ushort)5)]
    public void MapToGSPlayer_ShouldMapRightWristbandColor(AccessoryColor? color, ushort expectedValue)
    {
      player.Appearance.RightWristbandColor = color;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.RightWristband.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(null, (ushort)0)]
    [TestCase(AccessoryColor.Yellow, (ushort)8)]
    public void MapToGSPlayer_ShouldMapLefttWristbandColor(AccessoryColor? color, ushort expectedValue)
    {
      player.Appearance.LeftWristbandColor = color;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.LeftWristband.ShouldBe(expectedValue);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPitcherCapability()
    {
      player.PositionCapabilities.Pitcher = Grade.G;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.PitcherCapability.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCatcherCapability()
    {
      player.PositionCapabilities.Catcher = Grade.F;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.CatcherCapability.ShouldBe((ushort)2);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFirstBaseCapability()
    {
      player.PositionCapabilities.FirstBase = Grade.E;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.FirstBaseCapability.ShouldBe((ushort)3);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSecondBaseCapability()
    {
      player.PositionCapabilities.SecondBase = Grade.D;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.SecondBaseCapability.ShouldBe((ushort)4);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapThirdBaseCapability()
    {
      player.PositionCapabilities.ThirdBase = Grade.C;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.ThirdBaseCapability.ShouldBe((ushort)5);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapShortstopCapability()
    {
      player.PositionCapabilities.Shortstop = Grade.B;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.ShortstopCapability.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapLeftFieldCapability()
    {
      player.PositionCapabilities.LeftField = Grade.A;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.LeftFieldCapability.ShouldBe((ushort)7);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCenterFieldCapability()
    {
      player.PositionCapabilities.CenterField = Grade.B;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.CenterFieldCapability.ShouldBe((ushort)6);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapRightFieldCapability()
    {
      player.PositionCapabilities.RightField = Grade.C;
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
      player.SpecialAbilities.General.IsStar = true;
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
    public void MapToGSPlayer_ShouldMapGoodPoorDayGame()
    {
      player.SpecialAbilities.General.DayGameAbility = SpecialPositive_Negative.Negative;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.GoodOrPoorDayGame!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGoodPoorInRain()
    {
      player.SpecialAbilities.General.InRainAbility = SpecialPositive_Negative.Positive;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.GoodOrPoorRain!.Value.ShouldBe((short)1);
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
      player.SpecialAbilities.Hitter.SituationalHitting.IsTableSetter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsTableSetter!.Value.ShouldBe(true);
    }


    [Test]
    public void MapToGSPlayer_ShouldMapBackToBackHitter()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.IsBackToBackHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsBackToBackHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapHotHitter()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.IsHotHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsHotHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapRallyHitter ()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.IsRallyHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsRallyHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapIsGoodPinchHitter()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.IsGoodPinchHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsGoodPinchHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBasesLoadedHitter()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.BasesLoadedHitter = BasesLoadedHitter.HitsWell;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.BasesLoadedHitter!.Value.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapWalkOffHitter()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.WalkOffHitter= WalkOffHitter.HitsWell;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.WalkoffHitter!.Value.ShouldBe((ushort)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapClutchHitter()
    {
      player.SpecialAbilities.Hitter.SituationalHitting.ClutchHitter = Special1_5.One;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.ClutchHitter!.Value.ShouldBe((short)-2);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapContactHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.IsContactHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsContactHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPowerHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.IsPowerHitter= true;
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
      player.SpecialAbilities.Hitter.HittingApproach.IsPushHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsPushHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPullHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.IsPullHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsPullHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSprayHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.IsSprayHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsSprayHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFirstballHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.IsFirstballHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsFirstballHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapAggressiveOrPatientHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.AggressiveOrPatientHitter = AggressiveOrPatientHitter.Aggressive;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.AggressiveOrPatientHitter!.Value.ShouldBe((short)1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapRefined()
    {
      player.SpecialAbilities.Hitter.HittingApproach.IsRefinedHitter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsRefinedHitter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapFreeSwinger()
    {
      player.SpecialAbilities.Hitter.HittingApproach.IsFreeSwinger = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsFreeSwinger!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapToughOut()
    {
      player.SpecialAbilities.Hitter.HittingApproach.IsToughOut = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsToughOut!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapIntimidatingHitter()
    {
      player.SpecialAbilities.Hitter.HittingApproach.IsIntimidator = true;
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
      player.SpecialAbilities.Hitter.BaseRunning.IsAggressiveRunner = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsAggressiveBaserunner!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapAggressiveOrCautiousBaseStealer()
    {
      player.SpecialAbilities.Hitter.BaseRunning.AggressiveOrCautiousBaseStealer = AggressiveOrCautiousBaseStealer.Cautious;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.AggressiveOrCautiousBaseStealer!.Value.ShouldBe((short)-1);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapToughRunner()
    {
      player.SpecialAbilities.Hitter.BaseRunning.IsToughRunner = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsToughRunner!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBreakupDoublePlay()
    {
      player.SpecialAbilities.Hitter.BaseRunning.WillBreakupDoublePlay = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.WillBreakupDoublePlay!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapWillSlideHeadFirst()
    {
      player.SpecialAbilities.Hitter.BaseRunning.WillSlideHeadFirst = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.WillSlideHeadFirst!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGoldGlover()
    {
      player.SpecialAbilities.Hitter.Fielding.IsGoldGlover = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsGoldGlover!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSpiderCatch()
    {
      player.SpecialAbilities.Hitter.Fielding.CanSpiderCatch= true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.CanSpiderCatch!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapBarehandCatch()
    {
      player.SpecialAbilities.Hitter.Fielding.CanBarehandCatch = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.CanBarehandCatch!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapAggressiveFielder()
    {
      player.SpecialAbilities.Hitter.Fielding.IsAggressiveFielder = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsAggressiveFielder!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPivotMan()
    {
      player.SpecialAbilities.Hitter.Fielding.IsPivotMan = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsPivotMan!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapErrorProne()
    {
      player.SpecialAbilities.Hitter.Fielding.IsErrorProne = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsErrorProne!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapGoodBlocker()
    {
      player.SpecialAbilities.Hitter.Fielding.IsGoodBlocker = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsGoodBlocker!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapCatching()
    {
      player.SpecialAbilities.Hitter.Fielding.Catching = CatchingAbility.GoodCatcher;
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
      player.SpecialAbilities.Hitter.Fielding.HasCannonArm = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.HasCannonArm!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapTrashTalker()
    {
      player.SpecialAbilities.Hitter.Fielding.IsTrashTalker = true;
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
      result.PitchingVersusLefty!.Value.ShouldBe((short)1);
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
      player.SpecialAbilities.Pitcher.SituationalPitching.IsSlowStarter = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsSlowStarter!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapIsStarterFinisher()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.IsStarterFinisher = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsStarterFinisher!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapChokeArtist()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.IsChokeArtist = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsChokeArtist!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSandbag()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.IsSandbag = true;
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
      player.SpecialAbilities.Pitcher.SituationalPitching.IsWalkProne = true;
      var result = playerMapper.MapToGSPlayer(player, MLBPPTeam.Indians, 1);
      result.IsWalkProne!.Value.ShouldBe(true);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapLucky()
    {
      player.SpecialAbilities.Pitcher.SituationalPitching.Luck = SpecialPositive_Negative.Negative;
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
      player.SpecialAbilities.Pitcher.Demeanor.IsIntimidator = true;
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
      player.SpecialAbilities.Pitcher.Demeanor.IsHotHead = true;
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

