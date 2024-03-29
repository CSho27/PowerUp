﻿using NSubstitute;
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
        Unedited = true,
        SavedName = "Charlie",
        PrimaryPosition = 8,
        IsStarter = false,
        IsReliever = false,
        IsCloser = false,
        VoiceId = 0,
        BirthDay = 1,
        BirthMonth = 1,
        BirthYear = 2000,
        YearsInMajors = 0,
        BattingSide = 0,
        BattingForm = 0,
        ThrowsLefty = false,
        PitchingForm = 0,
        BattingAveragePoints = 0,
        HomeRuns = 0,
        RunsBattedIn = 0,
        EarnedRunAverage = 0,
        Face = 0,
        SkinAndEyes = 0,
        Hair = 0,
        HairColor = 0,
        FacialHair = 0,
        FacialHairColor = 0,
        EyewearType = 0,
        EyewearColor = 0,
        EarringSide = 0,
        EarringColor = 0,
        Bat = 0,
        Glove = 0,
        RightWristband = 0,
        LeftWristband = 0,
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
        IsStar = false,
        Durability = 0,
        Morale = 0,
        GoodOrPoorDayGame = 0,
        GoodOrPoorRain = 0,
        HittingConsistency = 0,
        HittingVersusLefty1 = 0,
        HittingVersusLefty2 = 0,
        IsTableSetter = false,
        IsBackToBackHitter = false,
        IsHotHitter = false,
        IsRallyHitter = false,
        IsGoodPinchHitter = false,
        BasesLoadedHitter = 0,
        WalkoffHitter = 0,
        ClutchHitter = 0,
        IsContactHitter = false,
        IsPowerHitter = false,
        SlugOrSlap = 0,
        IsPushHitter = false,
        IsPullHitter = false,
        IsSprayHitter = false,
        IsFirstballHitter = false,
        AggressiveOrPatientHitter = 0,
        IsRefinedHitter = false,
        IsFreeSwinger = false,
        IsToughOut = false,
        IsIntimidatingHitter = false,
        IsSparkplug = false,
        SmallBall = 0,
        Bunting = 0,
        InfieldHitter = 0,
        BaseRunning = 0,
        Stealing = 0,
        IsAggressiveBaserunner = false,
        AggressiveOrCautiousBaseStealer = 0,
        IsToughRunner = false,
        WillBreakupDoublePlay = false,
        WillSlideHeadFirst = false,
        IsGoldGlover = false,
        CanSpiderCatch = false,
        CanBarehandCatch = false,
        IsAggressiveFielder = false,
        IsPivotMan = false,
        IsErrorProne = false,
        IsGoodBlocker = false,
        Catching = 0,
        Throwing = 0,
        HasCannonArm = false,
        IsTrashTalker = false,
        PitchingConsistency = 0,
        PitchingVersusLefty = 0,
        Poise = 0,
        PoorVersusRunner = false,
        WithRunnersInScoringPosition = 0,
        IsSlowStarter = false,
        IsStarterFinisher = false,
        IsChokeArtist = false,
        IsSandbag = false,
        DoctorK = false,
        IsWalkProne = false,
        Luck = 0,
        Recovery = 0,
        IsIntimidatingPitcher = false,
        IsBattler = false,
        HasPokerFace = false,
        IsHotHead = false,
        GoodDelivery = false,
        Release = 0,
        HasGoodPace = false,
        HasGoodReflexes = false,
        GoodPickoff = false,
        PowerOrBreakingBallPitcher = 0,
        FastballLife = 0,
        Spin = 0,
        SafeOrFatPitch = 0,
        GroundBallOrFlyBallPitcher = 0,
        GoodLowPitch = false,
        Gyroball = false,
        ShuttoSpin = false
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
    public void MapToPlayer_EditedPlayersShouldGetIsCustomBit()
    {
      gsPlayer.IsEdited = true;
      gsPlayer.Unedited = false;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.IsCustomPlayer.ShouldBeTrue();
    }

    [Test]
    [TestCase(0, 1, "0")]
    [TestCase(12, 2, "12")]
    [TestCase(99, 3, "099")]
    [TestCase(999, 3, "999")]
    public void MapToPlayer_ShouldMapUniformNumber(int numberValue, int numberDigits, string expectedUniformNumber)
    {
      gsPlayer.PlayerNumber = (ushort)numberValue;
      gsPlayer.PlayerNumberNumberOfDigits = (ushort)numberDigits;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.UniformNumber.ShouldBe(expectedUniformNumber);
    }

    [Test]
    [TestCase(1890, 117)]
    [TestCase(2000, 7)]
    [TestCase(1979, 28)]
    [TestCase(1945, 62)]
    public void MapToPlayer_ShouldMapBirthYear(int year, int expectedYear)
    {
      gsPlayer.BirthYear = (ushort)year;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Age.ShouldBe(expectedYear);
    }

    [Test]
    [TestCase(2, 2)]
    [TestCase(4, 4)]
    [TestCase(8, 8)]
    [TestCase(11, 11)]
    public void MapToPlayer_ShouldMapBirthMonth(int month, int expectedMonth)
    {
      gsPlayer.BirthMonth = (ushort)month;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.BirthMonth.ShouldBe(expectedMonth);
    }

    [Test]
    [TestCase(2, 2)]
    [TestCase(4, 4)]
    [TestCase(8, 8)]
    [TestCase(11, 11)]
    public void MapToPlayer_ShouldMapBirthDay(int day, int expectedDay)
    {
      gsPlayer.BirthDay = (ushort)day;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.BirthDay.ShouldBe(expectedDay);
    }

    [Test]
    [TestCase(2, 2)]
    [TestCase(4, 4)]
    [TestCase(8, 8)]
    [TestCase(11, 11)]
    public void MapToPlayer_ShouldMapYearsInMajors(int yearsInMajors, int expectedYearsInMajors)
    {
      gsPlayer.YearsInMajors = (ushort)yearsInMajors;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.YearsInMajors.ShouldBe(expectedYearsInMajors);
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
      result.ThrowingArm.ShouldBe(ThrowingArm.Left);
    }

    [Test]
    public void MapToPlayer_ShouldMapThrowingMechanicsId()
    {
      gsPlayer.PitchingForm = 3;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PitchingMechanicsId.ShouldBe(3);
    }

    [Test]
    [TestCase(690, .690)]
    [TestCase(1023, null)]
    public void MapToPlayer_ShouldMapBattingAveragePoints(int battingAveragePoints, double? expectedBattingAverage)
    {
      gsPlayer.BattingAveragePoints = (ushort)battingAveragePoints;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.BattingAverage.ShouldBe(expectedBattingAverage);
    }

    [Test]
    [TestCase(130, 130)]
    [TestCase(1023, null)]
    public void MapToPlayer_ShouldMapRunsBattedIn(int runsBattedIn, int? expectedRBI)
    {
      gsPlayer.RunsBattedIn = (ushort)runsBattedIn;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.RunsBattedIn.ShouldBe(expectedRBI);
    }

    [Test]
    [TestCase(40, 40)]
    [TestCase(1023, null)]
    public void MapToPlayer_ShouldMapHomeRuns(int homeRuns, int? expectedHomeRuns)
    {
      gsPlayer.HomeRuns = (ushort)homeRuns;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.HomeRuns.ShouldBe(expectedHomeRuns);
    }

    [Test]
    [TestCase(383, 3.83)]
    [TestCase(16383, null)]
    public void MapToPlayer_ShouldMapHomeRuns(int era, double? expectedEra)
    {
      gsPlayer.EarnedRunAverage = (ushort)era;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.EarnedRunAverage.ShouldBe(expectedEra);
    }

    [Test]
    [TestCase(5, 5)]
    [TestCase(196, 178)]
    public void MapToPlayer_ShouldMapFaceId(int ppFaceId, int expectedFaceId)
    {
      gsPlayer.Face = (ushort)ppFaceId;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.FaceId.ShouldBe(expectedFaceId);
    }

    [Test]
    [TestCase(5, null)]
    [TestCase(179, EyebrowThickness.Thick)]
    [TestCase(196, EyebrowThickness.Thin)]
    [TestCase(232, null)]
    public void MapToPlayer_ShouldMapEyebrowThickness(int ppFaceId, EyebrowThickness? expectedValue)
    {
      gsPlayer.Face = (ushort)ppFaceId;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.EyebrowThickness.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase( 0, 0, null)]
    [TestCase( 177, 0, SkinColor.One)]
    [TestCase( 200, 3, SkinColor.Four)]
    [TestCase( 232, 6, SkinColor.Two)]
    public void MapToPlayer_ShouldMapSkinColor(int faceId, int skinAndEyesValue, SkinColor? expectedValue)
    {
      gsPlayer.Face = (ushort)faceId;
      gsPlayer.SkinAndEyes = (ushort)skinAndEyesValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.SkinColor.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0, 0, null)]
    [TestCase(177, 0, EyeColor.Blue)]
    [TestCase(193, 3, null)]
    [TestCase(232, 6, EyeColor.Brown)]
    public void MapToPlayer_ShouldMapEyeColor(int faceId, int skinAndEyesValue, EyeColor? expectedValue)
    {
      gsPlayer.Face = (ushort)faceId;
      gsPlayer.SkinAndEyes = (ushort)skinAndEyesValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.EyeColor.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase( 0, null)]
    [TestCase( 3, HairStyle.FlatShort)]
    public void MapToPlayer_ShouldMapHairStyle(int gsValue, HairStyle? expectedValue)
    {
      gsPlayer.Hair = (ushort)gsValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.HairStyle.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0,  2, null)]
    [TestCase(3,  2, HairColor.DarkBlonde)]
    public void MapToPlayer_ShouldMapHairColor(int gsHairValue, int gsHairColorValue, HairColor? expectedValue)
    {
      gsPlayer.Hair = (ushort)gsHairValue;
      gsPlayer.HairColor = (ushort)gsHairColorValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.HairColor.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0, null)]
    [TestCase(3, FacialHairStyle.FuManchu)]
    public void MapToPlayer_ShouldMapFacialHairStyle(int gsValue, FacialHairStyle? expectedValue)
    {
      gsPlayer.FacialHair = (ushort)gsValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.FacialHairStyle.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0, 2, null)]
    [TestCase(3, 2, HairColor.DarkBlonde)]
    public void MapToPlayer_ShouldMapFacialHairColor(int gsHairValue, int gsHairColorValue, HairColor? expectedValue)
    {
      gsPlayer.FacialHair = (ushort)gsHairValue;
      gsPlayer.FacialHairColor = (ushort)gsHairColorValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.FacialHairColor.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0, null)]
    [TestCase(3, EyewearType.RectangleRecSpecs)]
    public void MapToPlayer_ShouldMapEyewearType(int gsValue, EyewearType? expectedValue)
    {
      gsPlayer.EyewearType = (ushort)gsValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.EyewearType.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0, 2, null)]
    [TestCase(1, 2, null)]
    [TestCase(3, 2, EyewearFrameColor.Black)]
    [TestCase(3, 5, EyewearFrameColor.Gray)]
    [TestCase(3, 7, EyewearFrameColor.Red)]
    public void MapToPlayer_ShouldMapEyewearFrameColor(int gsEyewearValue, int gsEyewearColorValue, EyewearFrameColor? expectedValue)
    {
      gsPlayer.EyewearType = (ushort)gsEyewearValue;
      gsPlayer.EyewearColor = (ushort)gsEyewearColorValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.EyewearFrameColor.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0, 2, null)]
    [TestCase(1, 2, null)]
    [TestCase(3, 2, EyewearLensColor.Black)]
    [TestCase(3, 3, EyewearLensColor.Clear)]
    [TestCase(3, 7, EyewearLensColor.Orange)]
    public void MapToPlayer_ShouldMapEyewearLensColor(int gsEyewearValue, int gsEyewearColorValue, EyewearLensColor? expectedValue)
    {
      gsPlayer.EyewearType = (ushort)gsEyewearValue;
      gsPlayer.EyewearColor = (ushort)gsEyewearColorValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.EyewearLensColor.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0, null)]
    [TestCase(3, EarringSide.Both)]
    public void MapToPlayer_ShouldMapEarringSide(int gsValue, EarringSide? expectedValue)
    {
      gsPlayer.EarringSide = (ushort)gsValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.EarringSide.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0, 2, null)]
    [TestCase(3, 2, AccessoryColor.Red)]
    public void MapToPlayer_ShouldMapEarringColor(int gsSideValue, int gsColorValue, AccessoryColor? expectedValue)
    {
      gsPlayer.EarringSide = (ushort)gsSideValue;
      gsPlayer.EarringColor = (ushort)gsColorValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.EarringColor.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0, null)]
    [TestCase(3, AccessoryColor.Red)]
    public void MapToPlayer_ShouldMapRightWristband(int gsValue, AccessoryColor? expectedValue)
    {
      gsPlayer.RightWristband = (ushort)gsValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.RightWristbandColor.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(0, null)]
    [TestCase(1, AccessoryColor.Black)]
    public void MapToPlayer_ShouldMapLeftWristband(int gsValue, AccessoryColor? expectedValue)
    {
      gsPlayer.LeftWristband = (ushort)gsValue;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.LeftWristbandColor.ShouldBe(expectedValue);
    }


    [Test]
    public void MapToPlayer_ShouldMapBatColor()
    {
      gsPlayer.Bat = 3;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.BatColor.ShouldBe(BatColor.Black_Natural);
    }

    [Test]
    public void MapToPlayer_ShouldMapGloveColor()
    {
      gsPlayer.Glove = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.Appearance.GloveColor.ShouldBe(GloveColor.Tan);
    }

    [Test]
    public void MapToPlayer_ShouldMapPitcherCapability()
    {
      gsPlayer.PitcherCapability = 3;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositionCapabilities.Pitcher.ShouldBe(Grade.E);
    }

    [Test]
    public void MapToPlayer_ShouldMapCatcherCapability()
    {
      gsPlayer.CatcherCapability = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositionCapabilities.Catcher.ShouldBe(Grade.F);
    }

    [Test]
    public void MapToPlayer_ShouldMapFirstBaseCapability()
    {
      gsPlayer.FirstBaseCapability = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositionCapabilities.FirstBase.ShouldBe(Grade.G);
    }

    [Test]
    public void MapToPlayer_ShouldMapSecondBaseCapability()
    {
      gsPlayer.SecondBaseCapability = 4;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositionCapabilities.SecondBase.ShouldBe(Grade.D);
    }

    [Test]
    public void MapToPlayer_ShouldMapThirdBaseCapability()
    {
      gsPlayer.ThirdBaseCapability = 5;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositionCapabilities.ThirdBase.ShouldBe(Grade.C);
    }

    [Test]
    public void MapToPlayer_ShouldMapShortstopCapability()
    {
      gsPlayer.ShortstopCapability = 6;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositionCapabilities.Shortstop.ShouldBe(Grade.B);
    }

    [Test]
    public void MapToPlayer_ShouldMapLeftFieldCapability()
    {
      gsPlayer.LeftFieldCapability = 7;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositionCapabilities.LeftField.ShouldBe(Grade.A);
    }

    [Test]
    public void MapToPlayer_ShouldMapCenterFieldCapability()
    {
      gsPlayer.CenterFieldCapability = 6;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositionCapabilities.CenterField.ShouldBe(Grade.B);
    }

    [Test]
    public void MapToPlayer_ShouldMapRightFieldCapability()
    {
      gsPlayer.RightFieldCapability = 5;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.PositionCapabilities.RightField.ShouldBe(Grade.C);
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
    [TestCase(120, 74)]
    [TestCase(141, 87)]
    [TestCase(169, 105)]
    public void MapToPlayer_ShouldMapTopSpeedMph(int kmh, int mph)
    {
      gsPlayer.TopThrowingSpeedKMH = (ushort)kmh;
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

    [Test]
    public void MapToPlayer_ShouldMapIsStar()
    {
      gsPlayer.IsStar = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.General.IsStar.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapDurability()
    {
      gsPlayer.Durability = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.General.Durability.ShouldBe(Special2_4.Two);
    }

    [Test]
    public void MapToPlayer_ShouldMapMorale()
    {
      gsPlayer.Morale = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.General.Morale.ShouldBe(SpecialPositive_Negative.Positive);
    }

    [Test]
    public void MapToPlayer_ShouldMapGoodPoorDayGame()
    {
      gsPlayer.GoodOrPoorDayGame = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.General.DayGameAbility.ShouldBe(SpecialPositive_Negative.Positive);
    }

    [Test]
    public void MapToPlayer_ShouldMapGoodPoorRainGame()
    {
      gsPlayer.GoodOrPoorRain = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.General.InRainAbility.ShouldBe(SpecialPositive_Negative.Negative);
    }


    [Test]
    public void MapToPlayer_ShouldMapHittingConsistency()
    {
      gsPlayer.HittingConsistency = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.Consistency.ShouldBe(Special2_4.Two);
    }

    [Test]
    public void MapToPlayer_ShouldMapHittingVersusLefty1()
    {
      gsPlayer.HittingVersusLefty1 = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.VersusLefty.ShouldBe(Special1_5.Four);
    }

    [Test]
    public void MapToPlayer_ShouldMapHittingVersusLefty2()
    {
      gsPlayer.HittingVersusLefty2 = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.VersusLefty.ShouldBe(Special1_5.Five);
    }

    [Test]
    public void MapToPlayer_ShouldMapTableSetter()
    {
      gsPlayer.IsTableSetter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.IsTableSetter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapBackToBackHitter()
    {
      gsPlayer.IsBackToBackHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.IsBackToBackHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotHitter()
    {
      gsPlayer.IsHotHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.IsHotHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapRallyHitter()
    {
      gsPlayer.IsRallyHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.IsRallyHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapIsGoodPinchHitter()
    {
      gsPlayer.IsGoodPinchHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.IsGoodPinchHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapBasesLoadedHitter()
    {
      gsPlayer.BasesLoadedHitter = 3;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.BasesLoadedHitter.ShouldBe(BasesLoadedHitter.HitsWellAndHomersOften);
    }

    [Test]
    public void MapToPlayer_ShouldMapWalkOffHitter()
    {
      gsPlayer.WalkoffHitter = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.WalkOffHitter.ShouldBe(WalkOffHitter.HomersOften);
    }

    [Test]
    public void MapToPlayer_ShouldMapClutchHitter()
    {
      gsPlayer.ClutchHitter = -3;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SituationalHitting.ClutchHitter.ShouldBe(Special1_5.Two);
    }

    [Test]
    public void MapToPlayer_ShouldMapContactHitter()
    {
      gsPlayer.IsContactHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsContactHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapPowerHitter()
    {
      gsPlayer.IsPowerHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsPowerHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapSlugOrSlapHitter()
    {
      gsPlayer.SlugOrSlap = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.SluggerOrSlapHitter.ShouldBe(SluggerOrSlapHitter.Slugger);
    }

    [Test]
    public void MapToPlayer_ShouldMapPushHitter()
    {
      gsPlayer.IsPushHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsPushHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapPullHitter()
    {
      gsPlayer.IsPullHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsPullHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapSprayHitter()
    {
      gsPlayer.IsSprayHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsSprayHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapFirstballHitter()
    {
      gsPlayer.IsFirstballHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsFirstballHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapAggressiveOrPatientHitter()
    {
      gsPlayer.AggressiveOrPatientHitter = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.AggressiveOrPatientHitter.ShouldBe(AggressiveOrPatientHitter.Patient);
    }

    [Test]
    public void MapToPlayer_ShouldMapRefined()
    {
      gsPlayer.IsRefinedHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsRefinedHitter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapFreeSwinger()
    {
      gsPlayer.IsFreeSwinger = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsFreeSwinger.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapToughOut()
    {
      gsPlayer.IsToughOut = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsToughOut.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapIntimidator()
    {
      gsPlayer.IsIntimidatingHitter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsIntimidator.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapSparkplug()
    {
      gsPlayer.IsSparkplug = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.HittingApproach.IsSparkplug.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapSmallBall()
    {
      gsPlayer.SmallBall = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SmallBall.SmallBall.ShouldBe(SpecialPositive_Negative.Positive);
    }

    [Test]
    public void MapToPlayer_ShouldMapBunting()
    {
      gsPlayer.Bunting = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SmallBall.Bunting.ShouldBe(BuntingAbility.BuntMaster);
    }

    [Test]
    public void MapToPlayer_ShouldMapInfieldHitting()
    {
      gsPlayer.InfieldHitter = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.SmallBall.InfieldHitting.ShouldBe(InfieldHittingAbility.GoodInfieldHitter);
    }

    [Test]
    public void MapToPlayer_ShouldMapBaseRunning()
    {
      gsPlayer.BaseRunning = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.BaseRunning.BaseRunning.ShouldBe(Special2_4.Four);
    }

    [Test]
    public void MapToPlayer_ShouldMapStealing()
    {
      gsPlayer.Stealing = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.BaseRunning.Stealing.ShouldBe(Special2_4.Two);
    }

    [Test]
    public void MapToPlayer_ShouldMapAggressiveRunner()
    {
      gsPlayer.IsAggressiveBaserunner = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.BaseRunning.IsAggressiveRunner.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapAggressiveOrCautiousBaseStealer()
    {
      gsPlayer.AggressiveOrCautiousBaseStealer = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.BaseRunning.AggressiveOrCautiousBaseStealer.ShouldBe(AggressiveOrCautiousBaseStealer.Aggressive);
    }

    [Test]
    public void MapToPlayer_ShouldMapIsToughRunner()
    {
      gsPlayer.IsToughRunner = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.BaseRunning.IsToughRunner.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapWillBreakupDoublePlay()
    {
      gsPlayer.WillBreakupDoublePlay = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.BaseRunning.WillBreakupDoublePlay.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapWillSlideHeadFirst()
    {
      gsPlayer.WillSlideHeadFirst = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.BaseRunning.WillSlideHeadFirst.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapGoldGlover()
    {
      gsPlayer.IsGoldGlover = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.IsGoldGlover.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapSpiderCatch()
    {
      gsPlayer.CanSpiderCatch = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.CanSpiderCatch.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapBarehandCatch()
    {
      gsPlayer.CanBarehandCatch = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.CanBarehandCatch.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapAggressiveFielder()
    {
      gsPlayer.IsAggressiveFielder = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.IsAggressiveFielder.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapPivotMan()
    {
      gsPlayer.IsPivotMan = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.IsPivotMan.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapErrorProne()
    {
      gsPlayer.IsErrorProne = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.IsErrorProne.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapGoodBlocker()
    {
      gsPlayer.IsGoodBlocker = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.IsGoodBlocker.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapCatchingAbility()
    {
      gsPlayer.Catching = 2;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.Catching.ShouldBe(CatchingAbility.GreatCatcher);
    }

    [Test]
    public void MapToPlayer_ShouldMapThrowing()
    {
      gsPlayer.Throwing = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.Throwing.ShouldBe(Special2_4.Two);
    }

    [Test]
    public void MapToPlayer_ShouldMapCannonArm()
    {
      gsPlayer.HasCannonArm = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.HasCannonArm.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapTrashTalk()
    {
      gsPlayer.IsTrashTalker = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Hitter.Fielding.IsTrashTalker.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapPitchingConsistency()
    {
      gsPlayer.PitchingConsistency = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.Consistency.ShouldBe(Special2_4.Four);
    }

    [Test]
    public void MapToPlayer_ShouldMapVersusLeftHanded()
    {
      gsPlayer.PitchingVersusLefty = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.VersusLefty.ShouldBe(Special2_4.Two);
    }

    [Test]
    public void MapToPlayer_ShouldMapPoise()
    {
      gsPlayer.Poise = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.Poise.ShouldBe(Special2_4.Four);
    }

    [Test]
    public void MapToPlayer_ShouldMapPoorVersusRunner()
    {
      gsPlayer.PoorVersusRunner = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.PoorVersusRunner.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapWithRunnersInScoringPosition()
    {
      gsPlayer.WithRunnersInScoringPosition = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.WithRunnersInSocringPosition.ShouldBe(Special2_4.Two);
    }

    [Test]
    public void MapToPlayer_ShouldMapIsSlowStarter()
    {
      gsPlayer.IsSlowStarter = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.IsSlowStarter.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapStarterFinisher()
    {
      gsPlayer.IsStarterFinisher = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.IsStarterFinisher.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapChokeArtist()
    {
      gsPlayer.IsChokeArtist = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.IsChokeArtist.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapSandbag()
    {
      gsPlayer.IsSandbag = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.IsSandbag.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapDoctorK()
    {
      gsPlayer.DoctorK = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.DoctorK.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapWalk()
    {
      gsPlayer.IsWalkProne = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.IsWalkProne.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapLucky()
    {
      gsPlayer.Luck = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.Luck.ShouldBe(SpecialPositive_Negative.Positive);
    }

    [Test]
    public void MapToPlayer_ShouldMapRecovery()
    {
      gsPlayer.Recovery = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.SituationalPitching.Recovery.ShouldBe(Special2_4.Two);
    }

    [Test]
    public void MapToPlayer_ShouldMapPitchingIntimidatorRecovery()
    {
      gsPlayer.IsIntimidatingPitcher = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.Demeanor.IsIntimidator.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapIsBattler()
    {
      gsPlayer.IsBattler = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.Demeanor.BattlerPokerFace.ShouldBe(BattlerPokerFace.Battler);
    }

    [Test]
    public void MapToPlayer_ShouldMapHasPokerFace()
    {
      gsPlayer.HasPokerFace = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.Demeanor.BattlerPokerFace.ShouldBe(BattlerPokerFace.PokerFace);
    }

    [Test]
    public void MapToPlayer_ShouldMapHotHead()
    {
      gsPlayer.IsHotHead = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.Demeanor.IsHotHead.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapGoodDelivery()
    {
      gsPlayer.GoodDelivery = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchingMechanics.GoodDelivery.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapRelease()
    {
      gsPlayer.Release = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchingMechanics.Release.ShouldBe(Special2_4.Four);
    }

    [Test]
    public void MapToPlayer_ShouldMapHasGoodPace()
    {
      gsPlayer.HasGoodPace = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchingMechanics.GoodPace.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapGoodReflexes()
    {
      gsPlayer.HasGoodReflexes = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchingMechanics.GoodReflexes.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapGoodPickoff()
    {
      gsPlayer.GoodPickoff = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchingMechanics.GoodPickoff.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapPowerOrBreakingBallPitcher()
    {
      gsPlayer.PowerOrBreakingBallPitcher = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchQuailities.PowerOrBreakingBallPitcher.ShouldBe(PowerOrBreakingBallPitcher.Power);
    }

    [Test]
    public void MapToPlayer_ShouldMapFastballLife()
    {
      gsPlayer.FastballLife = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchQuailities.FastballLife.ShouldBe(Special2_4.Two);
    }
    [Test]
    public void MapToPlayer_ShouldMapSpin()
    {
      gsPlayer.Spin = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchQuailities.Spin.ShouldBe(Special2_4.Four);
    }

    [Test]
    public void MapToPlayer_ShouldMapSafeOrFatPitch()
    {
      gsPlayer.SafeOrFatPitch = -1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchQuailities.SafeOrFatPitch.ShouldBe(SpecialPositive_Negative.Negative);
    }

    [Test]
    public void MapToPlayer_ShouldMapGroundballOrFlytBallPitcher()
    {
      gsPlayer.GroundBallOrFlyBallPitcher = 1;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchQuailities.GroundBallOrFlyBallPitcher.ShouldBe(SpecialPositive_Negative.Positive);
    }

    [Test]
    public void MapToPlayer_ShouldMapGoodLowPitch()
    {
      gsPlayer.GoodLowPitch = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchQuailities.GoodLowPitch.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapGyroball()
    {
      gsPlayer.Gyroball = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchQuailities.Gyroball.ShouldBe(true);
    }

    [Test]
    public void MapToPlayer_ShouldMapShuttoSpin()
    {
      gsPlayer.ShuttoSpin = true;
      var result = playerMapper.MapToPlayer(gsPlayer, mappingParameters);
      result.SpecialAbilities.Pitcher.PitchQuailities.ShuttoSpin.ShouldBe(true);
    }
  }
}