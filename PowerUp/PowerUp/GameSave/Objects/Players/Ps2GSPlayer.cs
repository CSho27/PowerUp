﻿using PowerUp.GameSave.IO;

namespace PowerUp.GameSave.Objects.Players
{
  public class Ps2GSPlayer : IGSPlayer
  {
    [GSUInt(0x00, bits: 16, translateToStartOfChunk: true)]
    public ushort? PowerProsId { get; set; }

    [GSString(0x02, stringLength: 10)]
    public string? SavedName { get; set; }

    [GSUInt(0x02, bits: 16, bitOffset: 0, translateToStartOfChunk: true)]
    public ushort? SpecialSavedNameId { get; set; }

    [GSString(0x16, stringLength: 14)]
    public string? LastName { get; set; }

    [GSString(0x32, stringLength: 14)]
    public string? FirstName { get; set; }

    // Not sure this bit exists on Ps2
    public bool? Unedited { get; set; }

    [GSBoolean(0x50, bitOffset: 3)]
    public bool? IsEdited { get; set; }

    [GSBytes(0x50, numberOfBytes: 5, traverseSequentially: true)]
    public byte[]? BytesToCheck { get; set; }

    [GSUInt(0x51, bits: 5, bitOffset: 6)]
    public ushort? PowerProsTeamId { get; set; }

    [GSUInt(0x52, bits: 10, bitOffset: 1, traverseBackwardsOnEvenOffset: true)]
    public ushort? PlayerNumber { get; set; }

    [GSUInt(0x53, bits: 2, bitOffset: 7)]
    public ushort? PlayerNumberNumberOfDigits { get; set; }

    [GSUInt(0x54, bits: 8, bitOffset: 0)]
    public ushort? Face { get; set; }

    [GSUInt(0x55, bits: 4, bitOffset: 0)]
    public ushort? SkinAndEyes { get; set; }

    [GSUInt(0x56, bits: 3, bitOffset: 5)]
    public ushort? Bat { get; set; }

    [GSUInt(0x56, bits: 3, bitOffset: 1)]
    public ushort? Glove { get; set; }

    [GSUInt(0x57, bits: 4, bitOffset: 5)]
    public ushort? RightWristband { get; set; }

    [GSUInt(0x57, bits: 4, bitOffset: 1)]
    public ushort? LeftWristband { get; set; }

    [GSUInt(0x58, bits: 5, bitOffset: 3)]
    public ushort? Hair { get; set; }

    [GSUInt(0x59, bits: 4, bitOffset: 7)]
    public ushort? HairColor { get; set; }

    [GSUInt(0x59, bits: 5, bitOffset: 2)]
    public ushort? FacialHair { get; set; }

    [GSUInt(0x5A, bits: 4, bitOffset: 6, traverseBackwardsOnEvenOffset: true)]
    public ushort? FacialHairColor { get; set; }

    [GSUInt(0x5A, bits: 2, bitOffset: 4)]
    public ushort? EarringSide { get; set; }

    [GSUInt(0x5b, bits: 4, bitOffset: 6)]
    public ushort? EarringColor { get; set; }

    // There is no 2. It jumps from eye black at 1, to first pair of glasses at 3
    [GSUInt(0x5b, bits: 4, bitOffset: 2)]
    public ushort? EyewearType { get; set; }

    [GSUInt(0x5c, bits: 4, bitOffset: 4)]
    public ushort? EyewearColor { get; set; }

    [GSUInt(0x5c, bits: 8, bitOffset: 4)]
    public ushort? BattingForm { get; set; }

    [GSUInt(0x5d, bits: 8, bitOffset: 4)]
    public ushort? PitchingForm { get; set; }

    [GSUInt(0x5e, bits: 4, bitOffset: 0)]
    public ushort? PrimaryPosition { get; set; }

    [GSUInt(0x5f, bits: 3, bitOffset: 5)]
    public ushort? PitcherCapability { get; set; }

    [GSUInt(0x5f, bits: 3, bitOffset: 2)]
    public ushort? CatcherCapability { get; set; }

    [GSUInt(0x60, bits: 3, bitOffset: 5)]
    public ushort? FirstBaseCapability { get; set; }

    [GSUInt(0x60, bits: 3, bitOffset: 2)]
    public ushort? SecondBaseCapability { get; set; }

    [GSUInt(0x61, bits: 3, bitOffset: 7)]
    public ushort? ThirdBaseCapability { get; set; }

    [GSUInt(0x61, bits: 3, bitOffset: 4)]
    public ushort? ShortstopCapability { get; set; }

    [GSUInt(0x61, bits: 3, bitOffset: 1)]
    public ushort? LeftFieldCapability { get; set; }

    [GSUInt(0x62, bits: 3, bitOffset: 6, traverseBackwardsOnEvenOffset: true)]
    public ushort? CenterFieldCapability { get; set; }

    [GSUInt(0x62, bits: 3, bitOffset: 3)]
    public ushort? RightFieldCapability { get; set; }

    [GSBoolean(0x62, bitOffset: 2)]
    public bool? IsStarter { get; set; }

    [GSBoolean(0x63, bitOffset: 7)]
    public bool? IsReliever { get; set; }

    [GSBoolean(0x63, bitOffset: 4)]
    public bool? IsCloser { get; set; }

    /// <summary>
    /// For switch hitters hot zones follow righty conventions
    /// </summary>
    [GSUInt(0x67, bits: 2, bitOffset: 3)]
    public ushort? HotZoneUpAndIn { get; set; }

    [GSUInt(0x67, bits: 2, bitOffset: 1)]
    public ushort? HotZoneUp { get; set; }

    [GSUInt(0x68, bits: 2, bitOffset: 6)]
    public ushort? HotZoneUpAndAway { get; set; }

    [GSUInt(0x68, bits: 2, bitOffset: 4)]
    public ushort? HotZoneMiddleIn { get; set; }

    [GSUInt(0x68, bits: 2, bitOffset: 2)]
    public ushort? HotZoneMiddle { get; set; }

    [GSUInt(0x68, bits: 2, bitOffset: 0)]
    public ushort? HotZoneMiddleAway { get; set; }

    [GSUInt(0x69, bits: 2, bitOffset: 6)]
    public ushort? HotZoneDownAndIn { get; set; }

    [GSUInt(0x69, bits: 2, bitOffset: 4)]
    public ushort? HotZoneDown { get; set; }

    [GSUInt(0x69, bits: 2, bitOffset: 2)]
    public ushort? HotZoneDownAndAway { get; set; }

    [GSUInt(0x69, bits: 2, bitOffset: 0)]
    public ushort? BattingSide { get; set; }

    [GSBoolean(0x6a, bitOffset: 7)]
    public bool? ThrowsLefty { get; set; }

    [GSSInt(0x6a, bits: 2, bitOffset: 4)]
    public short? Durability { get; set; }

    /// <summary>
    /// Game value is value loaded plus 1
    /// </summary>
    [GSUInt(0x6a, bits: 2, bitOffset: 2)]
    public ushort? Trajectory { get; set; }

    [GSUInt(0x6b, bits: 4, bitOffset: 5)]
    public ushort? Contact { get; set; }

    /// <summary>I think the latter 5 bits of this byte are just always empty</summary>
    [GSBytes(0x6b, numberOfBytes: 1)]
    public byte[]? UnknownByte_6b { get; set; }

    [GSUInt(0x6c, bits: 8, bitOffset: 0)]
    public ushort? Power { get; set; }

    [GSUInt(0x6d, bits: 4, bitOffset: 4)]
    public ushort? RunSpeed { get; set; }

    [GSUInt(0x6d, bits: 4, bitOffset: 0)]
    public ushort? ArmStrength { get; set; }

    [GSUInt(0x6e, bits: 4, bitOffset: 4)]
    public ushort? Fielding { get; set; }

    [GSUInt(0x6e, bits: 4, bitOffset: 0)]
    public ushort? ErrorResistance { get; set; }

    [GSSInt(0x6f, bits: 2, bitOffset: 6)]
    public short? HittingConsistency { get; set; }

    // These two properties are signed ints but -3 represents -1
    [GSSInt(0x6f, bits: 3, bitOffset: 0)]
    public short? HittingVersusLefty1 { get; set; }

    [GSSInt(0x6f, bits: 3, bitOffset: 3)]
    public short? HittingVersusLefty2 { get; set; }

    [GSSInt(0x70, bits: 3, bitOffset: 5)]
    public short? ClutchHitter { get; set; }

    [GSBoolean(0x70, bitOffset: 4)]
    public bool? IsTableSetter { get; set; }

    [GSSInt(0x70, bits: 2, bitOffset: 2)]
    public short? Morale { get; set; }

    [GSBoolean(0x70, bitOffset: 1)]
    public bool? IsSparkplug { get; set; }

    [GSBoolean(0x70, bitOffset: 0)]
    public bool? IsRallyHitter { get; set; }

    [GSBoolean(0x71, bitOffset: 7)]
    public bool? IsHotHitter { get; set; }

    [GSBoolean(0x71, bitOffset: 6)]
    public bool? IsBackToBackHitter { get; set; }

    [GSBoolean(0x71, bitOffset: 5)]
    public bool? IsToughOut { get; set; }

    [GSBoolean(0x71, bitOffset: 4)]
    public bool? IsPushHitter { get; set; }

    [GSBoolean(0x71, bitOffset: 3)]
    public bool? IsSprayHitter { get; set; }

    [GSUInt(0x71, bits: 2, bitOffset: 1)]
    public ushort? InfieldHitter { get; set; }

    [GSBoolean(0x71, bitOffset: 0)]
    public bool? IsContactHitter { get; set; }

    [GSBoolean(0x72, bitOffset: 7)]
    public bool? IsPowerHitter { get; set; }

    [GSBoolean(0x72, bitOffset: 6)]
    public bool? IsGoodPinchHitter { get; set; }

    [GSBoolean(0x72, bitOffset: 5)]
    public bool? IsFreeSwinger { get; set; }

    [GSBoolean(0x72, bitOffset: 4)]
    public bool? IsFirstballHitter { get; set; }

    [GSUInt(0x72, bits: 2, bitOffset: 2)]
    public ushort? Bunting { get; set; }

    [GSUInt(0x72, bits: 2, bitOffset: 0)]
    public ushort? WalkoffHitter { get; set; }

    [GSUInt(0x73, bits: 2, bitOffset: 6)]
    public ushort? BasesLoadedHitter { get; set; }

    [GSBoolean(0x73, bitOffset: 5)]
    public bool? IsRefinedHitter { get; set; }

    [GSBoolean(0x73, bitOffset: 4)]
    public bool? IsIntimidatingHitter { get; set; }

    [GSSInt(0x73, bits: 2, bitOffset: 2)]
    public short? Stealing { get; set; }

    [GSSInt(0x73, bits: 2, bitOffset: 0)]
    public short? BaseRunning { get; set; }

    [GSBoolean(0x74, bitOffset: 6)]
    public bool? WillSlideHeadFirst { get; set; }

    [GSBoolean(0x74, bitOffset: 5)]
    public bool? IsToughRunner { get; set; }

    [GSBoolean(0x74, bitOffset: 4)]
    public bool? WillBreakupDoublePlay { get; set; }

    [GSSInt(0x74, bits: 2, bitOffset: 2)]
    public short? Throwing { get; set; }

    [GSBoolean(0x74, bitOffset: 1)]
    public bool? IsGoldGlover { get; set; }

    [GSBoolean(0x74, bitOffset: 0)]
    public bool? CanBarehandCatch { get; set; }

    [GSBoolean(0x75, bitOffset: 7)]
    public bool? CanSpiderCatch { get; set; }

    [GSBoolean(0x75, bitOffset: 6)]
    public bool? IsErrorProne { get; set; }

    // no setting = 1, gd = 3, grt = 4
    [GSUInt(0x75, bits: 3, bitOffset: 3)]
    public ushort? Catching { get; set; }

    [GSBoolean(0x75, bitOffset: 2)]
    public bool? IsGoodBlocker { get; set; }

    [GSBoolean(0x75, bitOffset: 1)]
    public bool? IsTrashTalker { get; set; }

    [GSBoolean(0x75, bitOffset: 0)]
    public bool? HasCannonArm { get; set; }

    [GSBoolean(0x76, bitOffset: 5)]
    public bool? IsStar { get; set; }

    [GSSInt(0x76, bits: 2, bitOffset: 3)]
    public short? SmallBall { get; set; }

    [GSSInt(0x76, bits: 2, bitOffset: 1)]
    public short? SlugOrSlap { get; set; }

    [GSSInt(0x77, bits: 2, bitOffset: 7)]
    public short? AggressiveOrPatientHitter { get; set; }

    [GSSInt(0x77, bits: 2, bitOffset: 5)]
    public short? AggressiveOrCautiousBaseStealer { get; set; }

    [GSBoolean(0x77, bitOffset: 4)]
    public bool? IsAggressiveBaserunner { get; set; }

    [GSBoolean(0x77, bitOffset: 2)]
    public bool? IsAggressiveFielder { get; set; }

    [GSBoolean(0x78, bitOffset: 7)]
    public bool? IsPivotMan { get; set; }

    [GSBoolean(0x78, bitOffset: 6)]
    public bool? IsPullHitter { get; set; }

    [GSSInt(0x78, bits: 2, bitOffset: 4)]
    public short? GoodOrPoorDayGame { get; set; }

    [GSSInt(0x78, bits: 2, bitOffset: 2)]
    public short? GoodOrPoorRain { get; set; }

    [GSUInt(0x79, bits: 8, bitOffset: 0)]
    public ushort? TopThrowingSpeedKMH { get; set; }

    [GSUInt(0x7a, bits: 8, bitOffset: 0)]
    public ushort? Control { get; set; }

    [GSUInt(0x7b, bits: 8, bitOffset: 0)]
    public ushort? Stamina { get; set; }

    [GSSInt(0x7c, bits: 2, bitOffset: 6)]
    public short? Recovery { get; set; }

    [GSSInt(0x7c, bits: 2, bitOffset: 4)]
    public short? PitchingConsistency { get; set; }

    [GSSInt(0x7c, bits: 2, bitOffset: 2)]
    public short? GroundBallOrFlyBallPitcher { get; set; }

    [GSSInt(0x7c, bits: 2, bitOffset: 0)]
    public short? SafeOrFatPitch { get; set; }

    [GSSInt(0x7d, bits: 2, bitOffset: 0)]
    public short? WithRunnersInScoringPosition { get; set; }

    [GSSInt(0x7d, bits: 2, bitOffset: 6)]
    public short? Spin { get; set; }

    [GSSInt(0x7d, bits: 2, bitOffset: 4)]
    public short? FastballLife { get; set; }

    [GSBoolean(0x7d, bitOffset: 3)]
    public bool? Gyroball { get; set; }

    [GSBoolean(0x7d, bitOffset: 2)]
    public bool? ShuttoSpin { get; set; }

    [GSSInt(0x7e, bits: 2, bitOffset: 6)]
    public short? Poise { get; set; }

    [GSSInt(0x7e, bits: 2, bitOffset: 4)]
    public short? Luck { get; set; }

    [GSSInt(0x7e, bits: 2, bitOffset: 2)]
    public short? Release { get; set; }

    [GSSInt(0x7e, bits: 2, bitOffset: 0)]
    public short? PitchingVersusLefty { get; set; }

    [GSBoolean(0x7f, bitOffset: 7)]
    public bool? PoorVersusRunner { get; set; }

    [GSBoolean(0x7f, bitOffset: 6)]
    public bool? GoodPickoff { get; set; }

    [GSBoolean(0x7f, bitOffset: 5)]
    public bool? GoodDelivery { get; set; }

    [GSBoolean(0x7f, bitOffset: 4)]
    public bool? GoodLowPitch { get; set; }

    [GSBoolean(0x7f, bitOffset: 3)]
    public bool? DoctorK { get; set; }

    [GSBoolean(0x7f, bitOffset: 2)]
    public bool? IsWalkProne { get; set; }

    [GSBoolean(0x7f, bitOffset: 1)]
    public bool? IsSandbag { get; set; }

    [GSBoolean(0x7f, bitOffset: 0)]
    public bool? HasPokerFace { get; set; }

    [GSBoolean(0x80, bitOffset: 7)]
    public bool? IsIntimidatingPitcher { get; set; }

    [GSBoolean(0x80, bitOffset: 6)]
    public bool? IsBattler { get; set; }

    [GSBoolean(0x80, bitOffset: 5)]
    public bool? IsHotHead { get; set; }

    [GSBoolean(0x80, bitOffset: 4)]
    public bool? IsSlowStarter { get; set; }

    [GSBoolean(0x80, bitOffset: 3)]
    public bool? IsStarterFinisher { get; set; }

    [GSBoolean(0x80, bitOffset: 2)]
    public bool? IsChokeArtist { get; set; }

    [GSBoolean(0x80, bitOffset: 1)]
    public bool? HasGoodReflexes { get; set; }

    [GSBoolean(0x81, bitOffset: 7)]
    public bool? HasGoodPace { get; set; }

    [GSSInt(0x81, bits: 2, bitOffset: 5)]
    public short? PowerOrBreakingBallPitcher { get; set; }

    [GSBytes(0x81, numberOfBytes: 3)]
    public byte[]? UnknownBytes_81_83 { get; set; }

    [GSUInt(0x85, bits: 11, bitOffset: 5)]
    public ushort? BirthYear { get; set; }

    [GSUInt(0x85, bits: 4, bitOffset: 1)]
    public ushort? BirthMonth { get; set; }

    [GSUInt(0x86, bits: 5, bitOffset: 4, traverseBackwardsOnEvenOffset: true)]
    public ushort? BirthDay { get; set; }

    [GSUInt(0x87, bits: 5, bitOffset: 7)]
    public ushort? YearsInMajors { get; set; }

    [GSBytes(0x87, numberOfBytes: 1)]
    public byte[]? UnknownByte_87 { get; set; }

    // 1023 means the stat has been 'cleared' (maxValue of a 10 bit int)
    [GSUInt(0x89, bits: 10, bitOffset: 6)]
    public ushort? BattingAveragePoints { get; set; }

    [GSUInt(0x8a, bits: 10, bitOffset: 4, traverseBackwardsOnEvenOffset: true)]
    public ushort? RunsBattedIn { get; set; }

    [GSUInt(0x8b, bits: 10, bitOffset: 2)]
    public ushort? HomeRuns { get; set; }

    // 16383 means the stat has been 'cleared' (maxValue of a 14 bit int)
    [GSUInt(0x8d, bits: 14, bitOffset: 2)]
    public ushort? EarnedRunAverage { get; set; }

    [GSBytes(0x8d, numberOfBytes: 5)]
    public byte[]? UnknownBytes_8d_92 { get; set; }

    [GSUInt(0x91, bits: 14, bitOffset: 2)]
    public ushort? VoiceId { get; set; }

    [GSUInt(0x94, bits: 5, bitOffset: 3)]
    public ushort? FourSeamType { get; set; }

    [GSUInt(0x94, bits: 3, bitOffset: 0)]
    public ushort? FourSeamMovement { get; set; }

    [GSUInt(0x95, bits: 5, bitOffset: 3)]
    public ushort? Slider1Type { get; set; }

    [GSUInt(0x95, bits: 3, bitOffset: 0)]
    public ushort? Slider1Movement { get; set; }

    [GSUInt(0x96, bits: 5, bitOffset: 3)]
    public ushort? Curve1Type { get; set; }

    [GSUInt(0x96, bits: 3, bitOffset: 0)]
    public ushort? Curve1Movement { get; set; }

    [GSUInt(0x97, bits: 5, bitOffset: 3)]
    public ushort? Fork1Type { get; set; }

    [GSUInt(0x97, bits: 3, bitOffset: 0)]
    public ushort? Fork1Movement { get; set; }

    [GSUInt(0x98, bits: 5, bitOffset: 3)]
    public ushort? Sinker1Type { get; set; }

    [GSUInt(0x98, bits: 3, bitOffset: 0)]
    public ushort? Sinker1Movement { get; set; }

    [GSUInt(0x99, bits: 5, bitOffset: 3)]
    public ushort? SinkingFastball1Type { get; set; }

    [GSUInt(0x99, bits: 3, bitOffset: 0)]
    public ushort? SinkingFastball1Movement { get; set; }

    [GSUInt(0x9a, bits: 5, bitOffset: 3)]
    public ushort? TwoSeamType { get; set; }

    [GSUInt(0x9a, bits: 3, bitOffset: 0)]
    public ushort? TwoSeamMovement { get; set; }

    [GSUInt(0x9b, bits: 5, bitOffset: 3)]
    public ushort? Slider2Type { get; set; }

    [GSUInt(0x9b, bits: 3, bitOffset: 0)]
    public ushort? Slider2Movement { get; set; }

    [GSUInt(0x9c, bits: 5, bitOffset: 3)]
    public ushort? Curve2Type { get; set; }

    [GSUInt(0x9c, bits: 3, bitOffset: 0)]
    public ushort? Curve2Movement { get; set; }

    [GSUInt(0x9d, bits: 5, bitOffset: 3)]
    public ushort? Fork2Type { get; set; }

    [GSUInt(0x9d, bits: 3, bitOffset: 0)]
    public ushort? Fork2Movement { get; set; }

    [GSUInt(0x9e, bits: 5, bitOffset: 3)]
    public ushort? Sinker2Type { get; set; }

    [GSUInt(0x9e, bits: 3, bitOffset: 0)]
    public ushort? Sinker2Movement { get; set; }

    [GSUInt(0x9f, bits: 5, bitOffset: 3)]
    public ushort? SinkingFastball2Type { get; set; }

    [GSUInt(0x9f, bits: 3, bitOffset: 0)]
    public ushort? SinkingFastball2Movement { get; set; }

    // I don't think these are used for anything but I am not 100% positive
    [GSBytes(0xA0, numberOfBytes: 0xB0 - 0xA0)]
    public byte[]? EmptyPlayerBytes { get; set; }
  }
}
