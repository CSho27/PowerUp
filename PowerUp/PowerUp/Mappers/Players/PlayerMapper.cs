using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.GameSave.Api;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;

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
    Player MapToPlayer(GSPlayer gsPlayer, PlayerMappingParameters parameters);
    GSPlayer MapToGSPlayer(Player player, MLBPPTeam mlbPPTeam, int powerProsId);
  }

  public class PlayerMapper : IPlayerMapper
  {
    private readonly ISpecialSavedNameLibrary _savedNameLibrary;

    public PlayerMapper(ISpecialSavedNameLibrary savedNameLibrary)
    {
      _savedNameLibrary = savedNameLibrary;
    }

    public Player MapToPlayer(GSPlayer gsPlayer, PlayerMappingParameters parameters)
    {
      return new Player
      {
        SourceType = gsPlayer.IsEdited!.Value
          ? EntitySourceType.Custom
          : parameters.IsBase
            ? EntitySourceType.Base
            : EntitySourceType.Imported,
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
        PositonCapabilities = gsPlayer.GetPositionCapabilities(),
        HitterAbilities = gsPlayer.GetHitterAbilities(),
        PitcherAbilities = gsPlayer.GetPitcherAbilities(),
        SpecialAbilities = SpecialAbilitiesMapper.GetSpecialAbilities(gsPlayer)
      };
    }

    public GSPlayer MapToGSPlayer(Player player, MLBPPTeam mlbPPTeam, int powerProsId)
    {
      var gsPlayerNumber = player.UniformNumber.ToGSUniformNumber();
      var gsPitcherType = player.PitcherType.ToGSPitcherType();
      var positionCapabilities = player.PositonCapabilities;
      var hitterAbilities = player.HitterAbilities;
      var hotZones = player.HitterAbilities.HotZones;
      var pitcherAbilities = player.PitcherAbilities;
      var generalSpecialAbilities = player.SpecialAbilities.General;
      var hittingSpecialAbilities = player.SpecialAbilities.Hitter;
      var pitchingSpecialAbilities = player.SpecialAbilities.Pitcher;

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
        IsEdited = player.SourceType == EntitySourceType.Custom,
        PlayerNumber = gsPlayerNumber.uniformNumberValue,
        PlayerNumberNumberOfDigits = gsPlayerNumber.numberOfDigits,
        PrimaryPosition = (ushort)player.PrimaryPosition,
        IsStarter = gsPitcherType.isStarter,
        IsReliever = gsPitcherType.isReliever,
        IsCloser = gsPitcherType.isCloser,
        VoiceId = (ushort)player.VoiceId,
        BattingSide = (ushort)player.BattingSide,
        BattingForm = (ushort)player.BattingStanceId,
        ThrowsLefty = player.ThrowingArm == ThrowingArm.Left,
        PitchingForm = (ushort)player.PitchingMechanicsId,

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

        // Hitting
        // Situational
        HittingConsistency = (short)hittingSpecialAbilities.SituationalHitting.Consistency,
        HittingVersusLefty1 = (short)hittingSpecialAbilities.SituationalHitting.VersusLefty,
        HittingVersusLefty2 = (short)hittingSpecialAbilities.SituationalHitting.VersusLefty,
        IsTableSetter = hittingSpecialAbilities.SituationalHitting.IsTableSetter,
        IsBackToBackHitter = hittingSpecialAbilities.SituationalHitting.IsBackToBackHitter,
        IsHotHitter = hittingSpecialAbilities.SituationalHitting.IsHotHitter,
        IsRallyHitter = hittingSpecialAbilities.SituationalHitting.IsRallyHitter,
        BasesLoadedHitter = hittingSpecialAbilities.SituationalHitting.BasesLoadedHitter.HasValue
          ? (ushort)hittingSpecialAbilities.SituationalHitting.BasesLoadedHitter.Value
          : (ushort)0,
        WalkoffHitter = hittingSpecialAbilities.SituationalHitting.WalkOffHitter.HasValue
          ? (ushort)hittingSpecialAbilities.SituationalHitting.WalkOffHitter.Value
          : (ushort)0,
        ClutchHitter = (short)hittingSpecialAbilities.SituationalHitting.ClutchHitter,

        // Approach
        IsContactHitter = hittingSpecialAbilities.HittingApproach.ContactHitter,
        IsPowerHitter = hittingSpecialAbilities.HittingApproach.PowerHitter,
        SlugOrSlap = hittingSpecialAbilities.HittingApproach.SluggerOrSlapHitter.HasValue
          ?(short)hittingSpecialAbilities.HittingApproach.SluggerOrSlapHitter
          : (short)0,
        IsPushHitter = hittingSpecialAbilities.HittingApproach.PushHitter,
        IsPullHitter = hittingSpecialAbilities.HittingApproach.PullHitter,
        IsSprayHitter = hittingSpecialAbilities.HittingApproach.SprayHitter,
        IsFirstballHitter = hittingSpecialAbilities.HittingApproach.FirstballHitter,
        AggressiveOrPatientHitter = hittingSpecialAbilities.HittingApproach.AggressiveOrPatientHitter.HasValue
          ? (short)hittingSpecialAbilities.HittingApproach.AggressiveOrPatientHitter
          : (short)0,
        IsRefinedHitter = hittingSpecialAbilities.HittingApproach.Refined,
        IsToughOut = hittingSpecialAbilities.HittingApproach.ToughOut,
        IsIntimidatingHitter = hittingSpecialAbilities.HittingApproach.Intimidator,
        IsSparkplug = hittingSpecialAbilities.HittingApproach.Sparkplug,

        // Small Ball
        SmallBall = (short)hittingSpecialAbilities.SmallBall.SmallBall,
        Bunting = (ushort)hittingSpecialAbilities.SmallBall.Bunting,
        InfieldHitter = (ushort)hittingSpecialAbilities.SmallBall.InfieldHitting,

        // Base Running
        BaseRunning = (short)hittingSpecialAbilities.BaseRunning.BaseRunning,
        Stealing = (short)hittingSpecialAbilities.BaseRunning.Stealing,
        IsAggressiveBaserunner = hittingSpecialAbilities.BaseRunning.AggressiveRunner,
        AggressiveOrCautiousBaseStealer = (short)hittingSpecialAbilities.BaseRunning.AggressiveOrPatientBaseStealer,
        IsToughRunner = hittingSpecialAbilities.BaseRunning.ToughRunner,
        WillBreakupDoublePlay = hittingSpecialAbilities.BaseRunning.BreakupDoublePlay,
        WillSlideHeadFirst = hittingSpecialAbilities.BaseRunning.HeadFirstSlide,

        // Fielding
        IsGoldGlover = hittingSpecialAbilities.Fielding.GoldGlover,
        CanSpiderCatch = hittingSpecialAbilities.Fielding.SpiderCatch,
        CanBarehandCatch = hittingSpecialAbilities.Fielding.BarehandCatch,
        IsAggressiveFielder = hittingSpecialAbilities.Fielding.AggressiveFielder,
        IsPivotMan = hittingSpecialAbilities.Fielding.PivotMan,
        IsErrorProne = hittingSpecialAbilities.Fielding.ErrorProne,
        IsGoodBlocker = hittingSpecialAbilities.Fielding.GoodBlocker,
        Catching = (ushort)hittingSpecialAbilities.Fielding.CatchingAbility,
        Throwing = (short)hittingSpecialAbilities.Fielding.Throwing,
        HasCannonArm = hittingSpecialAbilities.Fielding.CannonArm,
        IsTrashTalker = hittingSpecialAbilities.Fielding.TrashTalk,

        // Pitching
        // Situational
        PitchingConsistency = (short)pitchingSpecialAbilities.SituationalPitching.Consistency,
        VersusLeftHandedBatter = (short)pitchingSpecialAbilities.SituationalPitching.VersusLefty,
        Poise = (short)pitchingSpecialAbilities.SituationalPitching.Poise,
        PoorVersusRunner = pitchingSpecialAbilities.SituationalPitching.PoorVersusRunner,
        WithRunnersInScoringPosition = (short)pitchingSpecialAbilities.SituationalPitching.WithRunnersInSocringPosition,
        IsSlowStarter = pitchingSpecialAbilities.SituationalPitching.SlowStarter,
        IsStarterFinisher = pitchingSpecialAbilities.SituationalPitching.StarterFinisher,
        IsChokeArtist = pitchingSpecialAbilities.SituationalPitching.ChokeArtist,
        IsSandbag = pitchingSpecialAbilities.SituationalPitching.Sandbag,
        DoctorK = pitchingSpecialAbilities.SituationalPitching.DoctorK,
        WalkProne = pitchingSpecialAbilities.SituationalPitching.Walk,
        Luck = (short)pitchingSpecialAbilities.SituationalPitching.Lucky,
        Recovery = (short)pitchingSpecialAbilities.SituationalPitching.Recovery,

        // Demeanor
        IsIntimidatingPitcher = pitchingSpecialAbilities.Demeanor.Intimidator,
        IsBattler = pitchingSpecialAbilities.Demeanor.BattlerPokerFace == BattlerPokerFace.Battler,
        HasPokerFace = pitchingSpecialAbilities.Demeanor.BattlerPokerFace == BattlerPokerFace.PokerFace,
        IsHotHead = pitchingSpecialAbilities.Demeanor.HotHead,

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
