﻿using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.GameSave.Api;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using System;

namespace PowerUp.Mappers.Players
{
  public class PlayerMappingParameters
  {
    public bool IsBase { get; set; }
    public string? ImportSource { get; set; }

    public static PlayerMappingParameters FromRosterImport(RosterImportParameters importParameters)
      => new PlayerMappingParameters { IsBase = importParameters.IsBase, ImportSource = importParameters.ImportSource };
  }

  public interface IPlayerMapper
  {
    Player MapToPlayer(IGSPlayer gsPlayer, PlayerMappingParameters parameters);
    IGSPlayer MapToGSPlayer(Player player, MLBPPTeam mlbPPTeam, int powerProsId);
  }

  public class PlayerMapper : IPlayerMapper
  {
    private readonly ISpecialSavedNameLibrary _savedNameLibrary;
    private readonly DateTime OpeningDay07 = MLBSeasonUtils.GetEstimatedStartOfSeason(2007);

    public PlayerMapper(ISpecialSavedNameLibrary savedNameLibrary)
    {
      _savedNameLibrary = savedNameLibrary;
    }

    public Player MapToPlayer(IGSPlayer gsPlayer, PlayerMappingParameters parameters)
    {
      var inGameBirthDate = gsPlayer.BirthYear != 0
        ? new DateTime(gsPlayer.BirthYear!.Value, gsPlayer.BirthMonth!.Value, gsPlayer.BirthDay!.Value)
        : (DateTime?)null;

      return new Player
      {
        SourceType = parameters.IsBase
          ? EntitySourceType.Base
          : EntitySourceType.Imported,
        IsCustomPlayer = gsPlayer.IsEdited!.Value || !(gsPlayer.Unedited.HasValue && !gsPlayer.Unedited!.Value),
        LastName = gsPlayer.LastName!,
        FirstName = gsPlayer.FirstName!,
        SavedName = gsPlayer.SavedName!.Contains('*')
          ? _savedNameLibrary[(int)gsPlayer.SpecialSavedNameId!]
          : gsPlayer.SavedName,
        SpecialSavedNameId = gsPlayer.SavedName.Contains('*')
          ? gsPlayer.SpecialSavedNameId!
          : null,
        ImportSource = parameters.IsBase
          ? null
          : parameters.ImportSource,
        SourcePowerProsId = gsPlayer.PowerProsId!.Value,
        BirthMonth = inGameBirthDate?.Month ?? 1,
        BirthDay = inGameBirthDate?.Day ?? 1,
        Age = inGameBirthDate.HasValue
          ? OpeningDay07.YearsElapsedSince(inGameBirthDate.Value)
          : 29,
        YearsInMajors = gsPlayer.YearsInMajors!.Value,
        UniformNumber = UniformNumberMapper.ToUniformNumber(gsPlayer.PlayerNumberNumberOfDigits, gsPlayer.PlayerNumber),
        PrimaryPosition = (Position)gsPlayer.PrimaryPosition!,
        PitcherType = PitcherTypeMapper.ToPitcherType(gsPlayer.IsStarter!.Value, gsPlayer.IsReliever!.Value, gsPlayer.IsCloser!.Value),
        VoiceId = gsPlayer.VoiceId!.Value,
        BattingSide = (BattingSide)gsPlayer.BattingSide!,
        BattingStanceId = gsPlayer.BattingForm!.Value,
        ThrowingArm = gsPlayer.ThrowsLefty!.Value
          ? ThrowingArm.Left
          : ThrowingArm.Right,
        PitchingMechanicsId = gsPlayer.PitchingForm!.Value,
        BattingAverage = gsPlayer.BattingAveragePoints != 1023
          ? gsPlayer.BattingAveragePoints!.Value / 1000.0
          : null,
        RunsBattedIn = gsPlayer.RunsBattedIn != 1023
          ? gsPlayer.RunsBattedIn!.Value
          : null,
        HomeRuns = gsPlayer.HomeRuns != 1023
          ? gsPlayer.HomeRuns!.Value
          : null,
        EarnedRunAverage = gsPlayer.EarnedRunAverage != 16383
          ? gsPlayer.EarnedRunAverage!.Value / 100.0
          : null,
        Appearance = AppearanceMapper.GetAppearance(gsPlayer),
        PositionCapabilities = PositionCapabilitiesMapper.GetPositionCapabilities(gsPlayer),
        HitterAbilities = HitterAbilitiesMapper.GetHitterAbilities(gsPlayer),
        PitcherAbilities = PitcherAbilitiesMapper.GetPitcherAbilities(gsPlayer),
        SpecialAbilities = SpecialAbilitiesMapper.GetSpecialAbilities(gsPlayer),
      };
    }

    public IGSPlayer MapToGSPlayer(Player player, MLBPPTeam mlbPPTeam, int powerProsId)
    {
      var gsPlayerNumber = player.UniformNumber.ToGSUniformNumber();
      var gsPitcherType = player.PitcherType.ToGSPitcherType();
      var appearance = player.Appearance;
      var positionCapabilities = player.PositionCapabilities;
      var hitterAbilities = player.HitterAbilities;
      var hotZones = player.HitterAbilities.HotZones;
      var pitcherAbilities = player.PitcherAbilities;
      var generalSpecialAbilities = player.SpecialAbilities.General;
      var hittingSpecialAbilities = player.SpecialAbilities.Hitter;
      var pitchingSpecialAbilities = player.SpecialAbilities.Pitcher;

      var skinColorValue = (int?)appearance.SkinColor ?? 0;
      var eyewearFrameColorValue = (int?)appearance.EyewearFrameColor ?? 0;
      var eyewearLensColorValue = (int?)appearance.EyewearLensColor ?? 0;

      var rightWristbandValue = (int?)appearance.RightWristbandColor ?? 0;
      var leftWristbandValue = (int?)appearance.LeftWristbandColor ?? 0;

      var inGameBirthDate = OpeningDay07.GetDateNYearsBefore(player.BirthMonth, player.BirthDay, player.Age);

      return new GSPlayer
      {
        PowerProsId = (ushort)powerProsId,
        PowerProsTeamId = (ushort)mlbPPTeam,

        LastName = player.LastName,
        FirstName = player.FirstName,
        SavedName = player.SpecialSavedNameId.HasValue
          ? null
          : player.SavedName,
        SpecialSavedNameId = (ushort?)player.SpecialSavedNameId,
        IsEdited = player.IsCustomPlayer,
        Unedited = !player.IsCustomPlayer,
        BirthYear = (ushort)inGameBirthDate.Year,
        BirthMonth = (ushort)inGameBirthDate.Month,
        BirthDay = (ushort)inGameBirthDate.Day,
        YearsInMajors = (ushort)player.YearsInMajors,
        PlayerNumber = gsPlayerNumber.uniformNumberValue,
        PlayerNumberNumberOfDigits = gsPlayerNumber.numberOfDigits,
        PrimaryPosition = player.PrimaryPosition == Position.DesignatedHitter
           ? (ushort)Position.FirstBase
           : (ushort)player.PrimaryPosition,
        IsStarter = gsPitcherType.isStarter,
        IsReliever = gsPitcherType.isReliever,
        IsCloser = gsPitcherType.isCloser,
        VoiceId = (ushort)player.VoiceId,
        BattingSide = (ushort)player.BattingSide,
        BattingForm = (ushort)player.BattingStanceId,
        ThrowsLefty = player.ThrowingArm == ThrowingArm.Left,
        PitchingForm = (ushort)player.PitchingMechanicsId,
        BattingAveragePoints = player.BattingAverage.HasValue
          ? (ushort)(player.BattingAverage.Value * 1000)
          : (ushort)1023,
        RunsBattedIn = (ushort)(player.RunsBattedIn ?? 1023),
        HomeRuns = (ushort)(player.HomeRuns ?? 1023),
        EarnedRunAverage = player.EarnedRunAverage.HasValue
          ? (ushort)(player.EarnedRunAverage.Value * 100)
          : (ushort)16383,

        // Appearance
        Face = appearance.EyebrowThickness == EyebrowThickness.Thin
          ? (ushort)(appearance.FaceId + AppearanceMapper.THICK_EYEBROW_OFFSET)
          : (ushort)appearance.FaceId,
        SkinAndEyes = appearance.EyeColor == EyeColor.Brown
          ? (ushort)(skinColorValue + AppearanceMapper.EYE_COLOR_OFFSET)
          : (ushort)skinColorValue,
        Hair = (ushort?)appearance.HairStyle ?? 0,
        HairColor = (ushort?)appearance.HairColor ?? 0,
        FacialHair = (ushort?)appearance.FacialHairStyle ?? 0,
        FacialHairColor = (ushort?)appearance.FacialHairColor ?? 0,
        Bat = (ushort)appearance.BatColor,
        Glove = (ushort)appearance.GloveColor,
        EyewearType = (ushort?)appearance.EyewearType ?? 0,
        EyewearColor = (ushort)(eyewearFrameColorValue * AppearanceMapper.EYEWEAR_OFFSET + eyewearLensColorValue),
        EarringSide = (ushort?)appearance.EarringSide ?? 0,
        EarringColor = (ushort?)appearance.EarringColor ?? 0,
        RightWristband = appearance.RightWristbandColor.HasValue
          ? (ushort)(rightWristbandValue + 1)
          : (ushort)0,
        LeftWristband = appearance.LeftWristbandColor.HasValue
          ? (ushort)(leftWristbandValue + 1)
          : (ushort)0,

        // Position Capabilities
        PitcherCapability = (ushort)positionCapabilities.Pitcher,
        CatcherCapability = (ushort)positionCapabilities.Catcher,
        FirstBaseCapability = (ushort)positionCapabilities.FirstBase,
        SecondBaseCapability = (ushort)positionCapabilities.SecondBase,
        ThirdBaseCapability = (ushort)positionCapabilities.ThirdBase,
        ShortstopCapability = (ushort)positionCapabilities.Shortstop,
        LeftFieldCapability = (ushort)positionCapabilities.LeftField,
        CenterFieldCapability = (ushort)positionCapabilities.CenterField,
        RightFieldCapability = (ushort)positionCapabilities.RightField,

        // Hitter Abilities
        Trajectory = (ushort)(hitterAbilities.Trajectory - 1),
        Contact = (ushort)hitterAbilities.Contact,
        Power = (ushort)hitterAbilities.Power,
        RunSpeed = (ushort)hitterAbilities.RunSpeed,
        ArmStrength = (ushort)hitterAbilities.ArmStrength,
        Fielding = (ushort)hitterAbilities.Fielding,
        ErrorResistance = (ushort)hitterAbilities.ErrorResistance,

        // Hot Zones
        HotZoneUpAndIn = (ushort)hotZones.UpAndIn,
        HotZoneUp = (ushort)hotZones.Up,
        HotZoneUpAndAway = (ushort)hotZones.UpAndAway,
        HotZoneMiddleIn = (ushort)hotZones.MiddleIn,
        HotZoneMiddle = (ushort)hotZones.Middle,
        HotZoneMiddleAway = (ushort)hotZones.MiddleAway,
        HotZoneDownAndIn = (ushort)hotZones.DownAndIn,
        HotZoneDown = (ushort)hotZones.Down,
        HotZoneDownAndAway = (ushort)hotZones.DownAndAway,

        // Pitcher Abilities
        TopThrowingSpeedKMH = pitcherAbilities.TopSpeedMph.ToKMH(),
        Control = (ushort)pitcherAbilities.Control,
        Stamina = (ushort)pitcherAbilities.Stamina,
        FourSeamType = 1,
        FourSeamMovement = 1,
        TwoSeamType = pitcherAbilities.HasTwoSeam
          ? PitcherAbilitiesMapper.TwoSeamType
          : (ushort)0,
        TwoSeamMovement = (ushort)(pitcherAbilities.TwoSeamMovement ?? 0),
        Slider1Type = (ushort)(pitcherAbilities.Slider1Type ?? 0),
        Slider1Movement = (ushort)(pitcherAbilities.Slider1Movement ?? 0),
        Slider2Type = (ushort)(pitcherAbilities.Slider2Type ?? 0),
        Slider2Movement = (ushort)(pitcherAbilities.Slider2Movement ?? 0),
        Curve1Type = (ushort)(pitcherAbilities.Curve1Type ?? 0),
        Curve1Movement = (ushort)(pitcherAbilities.Curve1Movement ?? 0),
        Curve2Type = (ushort)(pitcherAbilities.Curve2Type ?? 0),
        Curve2Movement = (ushort)(pitcherAbilities.Curve2Movement ?? 0),
        Fork1Type = (ushort)(pitcherAbilities.Fork1Type ?? 0),
        Fork1Movement = (ushort)(pitcherAbilities.Fork1Movement ?? 0),
        Fork2Type = (ushort)(pitcherAbilities.Fork2Type ?? 0),
        Fork2Movement = (ushort)(pitcherAbilities.Fork2Movement ?? 0),
        Sinker1Type = (ushort)(pitcherAbilities.Sinker1Type ?? 0),
        Sinker1Movement = (ushort)(pitcherAbilities.Sinker1Movement ?? 0),
        Sinker2Type = (ushort)(pitcherAbilities.Sinker2Type ?? 0),
        Sinker2Movement = (ushort)(pitcherAbilities.Sinker2Movement ?? 0),
        SinkingFastball1Type = (ushort)(pitcherAbilities.SinkingFastball1Type ?? 0),
        SinkingFastball1Movement = (ushort)(pitcherAbilities.SinkingFastball1Movement ?? 0),
        SinkingFastball2Type = (ushort)(pitcherAbilities.SinkingFastball2Type ?? 0),
        SinkingFastball2Movement = (ushort)(pitcherAbilities.SinkingFastball2Movement ?? 0),

        // Special Abilities
        // General
        IsStar = generalSpecialAbilities.IsStar,
        Durability = (short)generalSpecialAbilities.Durability,
        Morale = (short)generalSpecialAbilities.Morale,
        GoodOrPoorDayGame = (short)generalSpecialAbilities.DayGameAbility,
        GoodOrPoorRain = (short)generalSpecialAbilities.InRainAbility,

        // Hitting
        // Situational
        HittingConsistency = (short)hittingSpecialAbilities.SituationalHitting.Consistency,
        HittingVersusLefty1 = (short)hittingSpecialAbilities.SituationalHitting.VersusLefty,
        HittingVersusLefty2 = (short)hittingSpecialAbilities.SituationalHitting.VersusLefty,
        IsTableSetter = hittingSpecialAbilities.SituationalHitting.IsTableSetter,
        IsBackToBackHitter = hittingSpecialAbilities.SituationalHitting.IsBackToBackHitter,
        IsHotHitter = hittingSpecialAbilities.SituationalHitting.IsHotHitter,
        IsRallyHitter = hittingSpecialAbilities.SituationalHitting.IsRallyHitter,
        IsGoodPinchHitter = hittingSpecialAbilities.SituationalHitting.IsGoodPinchHitter,
        BasesLoadedHitter = hittingSpecialAbilities.SituationalHitting.BasesLoadedHitter.HasValue
          ? (ushort)hittingSpecialAbilities.SituationalHitting.BasesLoadedHitter.Value
          : (ushort)0,
        WalkoffHitter = hittingSpecialAbilities.SituationalHitting.WalkOffHitter.HasValue
          ? (ushort)hittingSpecialAbilities.SituationalHitting.WalkOffHitter.Value
          : (ushort)0,
        ClutchHitter = (short)hittingSpecialAbilities.SituationalHitting.ClutchHitter,

        // Approach
        IsContactHitter = hittingSpecialAbilities.HittingApproach.IsContactHitter,
        IsPowerHitter = hittingSpecialAbilities.HittingApproach.IsPowerHitter,
        SlugOrSlap = hittingSpecialAbilities.HittingApproach.SluggerOrSlapHitter.HasValue
          ?(short)hittingSpecialAbilities.HittingApproach.SluggerOrSlapHitter
          : (short)0,
        IsPushHitter = hittingSpecialAbilities.HittingApproach.IsPushHitter,
        IsPullHitter = hittingSpecialAbilities.HittingApproach.IsPullHitter,
        IsSprayHitter = hittingSpecialAbilities.HittingApproach.IsSprayHitter,
        IsFirstballHitter = hittingSpecialAbilities.HittingApproach.IsFirstballHitter,
        AggressiveOrPatientHitter = hittingSpecialAbilities.HittingApproach.AggressiveOrPatientHitter.HasValue
          ? (short)hittingSpecialAbilities.HittingApproach.AggressiveOrPatientHitter
          : (short)0,
        IsRefinedHitter = hittingSpecialAbilities.HittingApproach.IsRefinedHitter,
        IsFreeSwinger = hittingSpecialAbilities.HittingApproach.IsFreeSwinger,
        IsToughOut = hittingSpecialAbilities.HittingApproach.IsToughOut,
        IsIntimidatingHitter = hittingSpecialAbilities.HittingApproach.IsIntimidator,
        IsSparkplug = hittingSpecialAbilities.HittingApproach.IsSparkplug,

        // Small Ball
        SmallBall = (short)hittingSpecialAbilities.SmallBall.SmallBall,
        Bunting = hittingSpecialAbilities.SmallBall.Bunting.HasValue
          ? (ushort)hittingSpecialAbilities.SmallBall.Bunting.Value
          : (ushort)0,
        InfieldHitter = hittingSpecialAbilities.SmallBall.InfieldHitting.HasValue
          ? (ushort)hittingSpecialAbilities.SmallBall.InfieldHitting
          : (ushort)0,

        // Base Running
        BaseRunning = (short)hittingSpecialAbilities.BaseRunning.BaseRunning,
        Stealing = (short)hittingSpecialAbilities.BaseRunning.Stealing,
        IsAggressiveBaserunner = hittingSpecialAbilities.BaseRunning.IsAggressiveRunner,
        AggressiveOrCautiousBaseStealer = hittingSpecialAbilities.BaseRunning.AggressiveOrCautiousBaseStealer.HasValue
          ? (short)hittingSpecialAbilities.BaseRunning.AggressiveOrCautiousBaseStealer
          : (short)0,
        IsToughRunner = hittingSpecialAbilities.BaseRunning.IsToughRunner,
        WillBreakupDoublePlay = hittingSpecialAbilities.BaseRunning.WillBreakupDoublePlay,
        WillSlideHeadFirst = hittingSpecialAbilities.BaseRunning.WillSlideHeadFirst,

        // Fielding
        IsGoldGlover = hittingSpecialAbilities.Fielding.IsGoldGlover,
        CanSpiderCatch = hittingSpecialAbilities.Fielding.CanSpiderCatch,
        CanBarehandCatch = hittingSpecialAbilities.Fielding.CanBarehandCatch,
        IsAggressiveFielder = hittingSpecialAbilities.Fielding.IsAggressiveFielder,
        IsPivotMan = hittingSpecialAbilities.Fielding.IsPivotMan,
        IsErrorProne = hittingSpecialAbilities.Fielding.IsErrorProne,
        IsGoodBlocker = hittingSpecialAbilities.Fielding.IsGoodBlocker,
        Catching = hittingSpecialAbilities.Fielding.Catching.HasValue
          ? (ushort)hittingSpecialAbilities.Fielding.Catching
          : (ushort)0,
        Throwing = (short)hittingSpecialAbilities.Fielding.Throwing,
        HasCannonArm = hittingSpecialAbilities.Fielding.HasCannonArm,
        IsTrashTalker = hittingSpecialAbilities.Fielding.IsTrashTalker,

        // Pitching
        // Situational
        PitchingConsistency = (short)pitchingSpecialAbilities.SituationalPitching.Consistency,
        PitchingVersusLefty = (short)pitchingSpecialAbilities.SituationalPitching.VersusLefty,
        Poise = (short)pitchingSpecialAbilities.SituationalPitching.Poise,
        PoorVersusRunner = pitchingSpecialAbilities.SituationalPitching.PoorVersusRunner,
        WithRunnersInScoringPosition = (short)pitchingSpecialAbilities.SituationalPitching.WithRunnersInSocringPosition,
        IsSlowStarter = pitchingSpecialAbilities.SituationalPitching.IsSlowStarter,
        IsStarterFinisher = pitchingSpecialAbilities.SituationalPitching.IsStarterFinisher,
        IsChokeArtist = pitchingSpecialAbilities.SituationalPitching.IsChokeArtist,
        IsSandbag = pitchingSpecialAbilities.SituationalPitching.IsSandbag,
        DoctorK = pitchingSpecialAbilities.SituationalPitching.DoctorK,
        IsWalkProne = pitchingSpecialAbilities.SituationalPitching.IsWalkProne,
        Luck = (short)pitchingSpecialAbilities.SituationalPitching.Luck,
        Recovery = (short)pitchingSpecialAbilities.SituationalPitching.Recovery,

        // Demeanor
        IsIntimidatingPitcher = pitchingSpecialAbilities.Demeanor.IsIntimidator,
        IsBattler = pitchingSpecialAbilities.Demeanor.BattlerPokerFace == BattlerPokerFace.Battler,
        HasPokerFace = pitchingSpecialAbilities.Demeanor.BattlerPokerFace == BattlerPokerFace.PokerFace,
        IsHotHead = pitchingSpecialAbilities.Demeanor.IsHotHead,

        // Mechanics
        GoodDelivery = pitchingSpecialAbilities.PitchingMechanics.GoodDelivery,
        Release = (short)pitchingSpecialAbilities.PitchingMechanics.Release,
        HasGoodPace = pitchingSpecialAbilities.PitchingMechanics.GoodPace,
        HasGoodReflexes = pitchingSpecialAbilities.PitchingMechanics.GoodReflexes,
        GoodPickoff = pitchingSpecialAbilities.PitchingMechanics.GoodPickoff,

        // Pitch Qualities
        PowerOrBreakingBallPitcher = pitchingSpecialAbilities.PitchQuailities.PowerOrBreakingBallPitcher.HasValue
          ? (short)pitchingSpecialAbilities.PitchQuailities.PowerOrBreakingBallPitcher.Value
          : (short)0,
        FastballLife = (short)pitchingSpecialAbilities.PitchQuailities.FastballLife,
        Spin = (short)pitchingSpecialAbilities.PitchQuailities.Spin,
        SafeOrFatPitch = (short)pitchingSpecialAbilities.PitchQuailities.SafeOrFatPitch,
        GroundBallOrFlyBallPitcher = (short)pitchingSpecialAbilities.PitchQuailities.GroundBallOrFlyBallPitcher,
        GoodLowPitch = pitchingSpecialAbilities.PitchQuailities.GoodLowPitch,
        Gyroball = pitchingSpecialAbilities.PitchQuailities.Gyroball,
        ShuttoSpin = pitchingSpecialAbilities.PitchQuailities.ShuttoSpin
      };
    }

  }
}
