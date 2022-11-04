using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class SpecialAbilitiesRequest
  {
    public GeneralSpecialAbilitiesRequest? GeneralSpecialAbilitiesRequest { get; set; }
    public HitterSpecialAbilitiesRequest? HitterSpecialAbilitiesRequest { get; set; }
    public PitcherSpecialAbilitiesRequest? PitcherSpecialAbilitiesRequest { get; set; }

    public SpecialAbilitiesParameters GetParameters()
    {
      return new SpecialAbilitiesParameters
      {
        General = GeneralSpecialAbilitiesRequest!.GetParameters(),
        Hitter = HitterSpecialAbilitiesRequest!.GetParameters(),
        Pitcher = PitcherSpecialAbilitiesRequest!.GetParameters(),
      };
    }
  }

  public class GeneralSpecialAbilitiesRequest
  {
    public bool IsStar { get; set; }
    public string? DurabilityKey { get; set; }
    public string? MoraleKey { get; set; }
    public string? DayGameAbilityKey { get; set; }
    public string? InRainAbilityKey { get; set; }

    public GeneralSpecialAbilitiesParameters GetParameters()
    {
      return new GeneralSpecialAbilitiesParameters
      {
        IsStar = IsStar,
        Durability = Enum.Parse<Special2_4>(DurabilityKey!),
        Morale = Enum.Parse<SpecialPositive_Negative>(MoraleKey!),
        DayGameAbility = Enum.Parse<SpecialPositive_Negative>(DayGameAbilityKey!),
        InRainAbility = Enum.Parse<SpecialPositive_Negative>(InRainAbilityKey!)
      };
    }
  }

  public class HitterSpecialAbilitiesRequest
  {
    public SituationalHittingSpecialAbilitiesRequest? Situational { get; set; }
    public HittingApproachSpecialAbilitiesRequest? Approach { get; set; }
    public SmallBallSpecialAbilitiesRequest? SmallBall { get; set; }
    public BaseRunningSpecialAbilitiesRequest? BaseRunning { get; set; }
    public FieldingSpecialAbilitiesRequest? Fielding { get; set; }

    public HitterSpecialAbilitiesParameters GetParameters()
    {
      return new HitterSpecialAbilitiesParameters
      {
        SituationalHitting = Situational!.GetParameters(),
        HittingApproach = Approach!.GetParameters(),
        SmallBall = SmallBall!.GetParameters(),
        BaseRunning = BaseRunning!.GetParameters(),
        Fielding = Fielding!.GetParameters()
      };
    }
  }

  public class SituationalHittingSpecialAbilitiesRequest
  {
    public string? ConsistencyKey { get; set; }
    public string? VersusLeftyKey { get; set; }
    public bool IsTableSetter { get; set; }
    public bool IsBackToBackHitter { get; set; }
    public bool IsHotHitter { get; set; }
    public bool IsRallyHitter { get; set; }
    public bool IsGoodPinchHitter { get; set; }
    public string? BasesLoadedHitterKey { get; set; }
    public string? WalkOffHitterKey { get; set; }
    public string? ClutchHitterKey { get; set; }

    public SituationalHittingSpecialAbilitiesParameters GetParameters()
    {
      return new SituationalHittingSpecialAbilitiesParameters
      {
        Consistency = Enum.Parse<Special2_4>(ConsistencyKey!),
        VersusLefty = Enum.Parse<Special1_5>(VersusLeftyKey!),
        IsTableSetter = IsTableSetter,
        IsBackToBackHitter = IsBackToBackHitter,
        IsHotHitter = IsHotHitter,
        IsRallyHitter = IsRallyHitter,
        IsGoodPinchHitter = IsGoodPinchHitter,
        BasesLoadedHitter = BasesLoadedHitterKey != null
          ? Enum.Parse<BasesLoadedHitter>(BasesLoadedHitterKey)
          : null,
        WalkOffHitter = WalkOffHitterKey != null
          ? Enum.Parse<WalkOffHitter>(WalkOffHitterKey)
          : null,
        ClutchHitter = Enum.Parse<Special1_5>(ClutchHitterKey!)
      };
    }
  }

  public class HittingApproachSpecialAbilitiesRequest
  {
    public bool IsContactHitter { get; set; }
    public bool IsPowerHitter { get; set; }
    public string? SluggerOrSlapHitterKey { get; set; }
    public bool IsPushHitter { get; set; }
    public bool IsPullHitter { get; set; }
    public bool IsSprayHitter { get; set; }
    public bool IsFirstballHitter { get; set; }
    public string? AggressiveOrPatientHitterKey { get; set; }
    public bool IsRefinedHitter { get; set; }
    public bool IsFreeSwinger { get; set; }
    public bool IsToughOut { get; set; }
    public bool IsIntimidator { get; set; }
    public bool IsSparkplug { get; set; }

    public HittingApproachSpecialAbilitiesParameters GetParameters()
    {
      return new HittingApproachSpecialAbilitiesParameters
      {
        IsContactHitter = IsContactHitter,
        IsPowerHitter = IsPowerHitter,
        SluggerOrSlapHitter = SluggerOrSlapHitterKey != null
          ? Enum.Parse<SluggerOrSlapHitter>(SluggerOrSlapHitterKey)
          : null,
        IsPushHitter = IsPushHitter,
        IsPullHitter = IsPullHitter,
        IsSprayHitter = IsSprayHitter,
        IsFirstballHitter = IsFirstballHitter,
        AggressiveOrPatientHitter = AggressiveOrPatientHitterKey != null
          ? Enum.Parse<AggressiveOrPatientHitter>(AggressiveOrPatientHitterKey)
          : null,
        IsRefinedHitter = IsRefinedHitter,
        IsFreeSwinger = IsFreeSwinger,
        IsToughOut = IsToughOut,
        IsIntimidator = IsIntimidator,
        IsSparkplug = IsSparkplug
      };
    }
  }

  public class SmallBallSpecialAbilitiesRequest
  {
    public string? SmallBallKey { get; set; }
    public string? BuntingKey { get; set; }
    public string? InfieldHittingKey { get; set; }

    public SmallBallSpecialAbilitiesParameters GetParameters()
    {
      return new SmallBallSpecialAbilitiesParameters
      {
        SmallBall = Enum.Parse<SpecialPositive_Negative>(SmallBallKey!),
        Bunting = BuntingKey != null  
          ? Enum.Parse<BuntingAbility>(BuntingKey)
          : null,
        InfieldHitting = InfieldHittingKey != null
          ? Enum.Parse<InfieldHittingAbility>(InfieldHittingKey)
          : null
      };
    }
  }

  public class BaseRunningSpecialAbilitiesRequest
  {
    public string? BaseRunningKey { get; set; }
    public string? StealingKey { get; set; }
    public bool IsAggressiveRunner { get; set; }
    public string? AggressiveOrCautiousBaseStealerKey { get; set; }
    public bool IsToughRunner { get; set; }
    public bool WillBreakupDoublePlay { get; set; }
    public bool WillSlideHeadFirst { get; set; }

    public BaseRunningSpecialAbilitiesParameters GetParameters()
    {
      return new BaseRunningSpecialAbilitiesParameters
      {
        BaseRunning = Enum.Parse<Special2_4>(BaseRunningKey!),
        Stealing = Enum.Parse<Special2_4>(StealingKey!),
        IsAggressiveRunner = IsAggressiveRunner,
        AggressiveOrCautiousBaseStealer = AggressiveOrCautiousBaseStealerKey != null
          ? Enum.Parse<AggressiveOrCautiousBaseStealer>(AggressiveOrCautiousBaseStealerKey)
          : null,
        IsToughRunner = IsToughRunner,
        WillBreakupDoublePlay = WillBreakupDoublePlay,
        WillSlideHeadFirst = WillSlideHeadFirst
      };
    }
  }

  public class FieldingSpecialAbilitiesRequest
  {
    public bool IsGoldGlover { get; set; }
    public bool CanSpiderCatch { get; set; }
    public bool CanBarehandCatch { get; set; }
    public bool IsAggressiveFielder { get; set; }
    public bool IsPivotMan { get; set; }
    public bool IsErrorProne { get; set; }
    public bool IsGoodBlocker { get; set; }
    public string? CatchingKey { get; set; }
    public string? ThrowingKey { get; set; }
    public bool HasCannonArm { get; set; }
    public bool IsTrashTalker { get; set; }

    public FieldingSpecialAbilitiesParameters GetParameters()
    {
      return new FieldingSpecialAbilitiesParameters
      {
        IsGoldGlover = IsGoldGlover,
        CanSpiderCatch = CanSpiderCatch,
        CanBarehandCatch = CanBarehandCatch,
        IsAggressiveFielder = IsAggressiveFielder,
        IsPivotMan = IsPivotMan,
        IsErrorProne = IsErrorProne,
        IsGoodBlocker = IsGoodBlocker,
        Catching = CatchingKey != null
          ? Enum.Parse<CatchingAbility>(CatchingKey)
          : null,
        Throwing = Enum.Parse<Special2_4>(ThrowingKey!),
        HasCannonArm = HasCannonArm,
        IsTrashTalker = IsTrashTalker
      };
    }
  }

  public class PitcherSpecialAbilitiesRequest
  {
    public SituationalPitchingSpecialAbilitiesRequest? Situational { get; set; }
    public PitchingDemeanorSpecialAbilitiesRequest? Demeanor { get; set; }
    public PitchingMechanicsSpecialAbilitiesRequest? Mechanics { get; set; }
    public PitchQualitiesSpecialAbilitiesRequest? PitchQualities { get; set; }

    public PitcherSpecialAbilitiesParameters GetParameters()
    {
      return new PitcherSpecialAbilitiesParameters
      {
        SituationalPitching = Situational!.GetParameters(),
        Demeanor = Demeanor!.GetParameters(),
        PitchingMechanics = Mechanics!.GetParameters(),
        PitchQuailities = PitchQualities!.GetParameters()
      };
    }

  }

  public class SituationalPitchingSpecialAbilitiesRequest
  {
    public string? ConsistencyKey { get; set; }
    public string? VersusLeftyKey { get; set; }
    public string? PoiseKey { get; set; }
    public bool PoorVersusRunner { get; set; }
    public string? WithRunnersInScoringPositionKey { get; set; }
    public bool IsSlowStarter { get; set; }
    public bool IsStarterFinisher { get; set; }
    public bool IsChokeArtist { get; set; }
    public bool IsSandbag { get; set; }
    public bool DoctorK { get; set; }
    public bool IsWalkProne { get; set; }
    public string? LuckKey { get; set; }
    public string? RecoveryKey { get; set; }

    public SituationalPitchingSpecialAbilitiesParameters GetParameters()
    {
      return new SituationalPitchingSpecialAbilitiesParameters
      {
        Consistency = Enum.Parse<Special2_4>(ConsistencyKey!),
        VersusLefty = Enum.Parse<Special2_4>(VersusLeftyKey!),
        Poise = Enum.Parse<Special2_4>(PoiseKey!),
        PoorVersusRunner = PoorVersusRunner,
        WithRunnersInSocringPosition = Enum.Parse<Special2_4>(WithRunnersInScoringPositionKey!),
        IsSlowStarter = IsSlowStarter,
        IsStarterFinisher = IsStarterFinisher,
        IsChokeArtist = IsChokeArtist,
        IsSandbag = IsSandbag,  
        DoctorK = DoctorK,  
        IsWalkProne = IsWalkProne,
        Luck = Enum.Parse<SpecialPositive_Negative>(LuckKey!),
        Recovery = Enum.Parse<Special2_4>(RecoveryKey!)
      };
    }
  }

  public class PitchingDemeanorSpecialAbilitiesRequest
  {
    public bool IsIntimidator { get; set; }
    public string? BattlerPokerFaceKey { get; set; }
    public bool IsHotHead { get; set; }

    public PitchingDemeanorSpecialAbilitiesParameters GetParameters()
    {
      return new PitchingDemeanorSpecialAbilitiesParameters
      {
        IsIntimidator = IsIntimidator,
        BattlerPokerFace = BattlerPokerFaceKey != null
          ? Enum.Parse<BattlerPokerFace>(BattlerPokerFaceKey)
          : null,
        IsHotHead = IsHotHead
      };
    }
  }

  public class PitchingMechanicsSpecialAbilitiesRequest
  {
    public bool GoodDelivery { get; set; }
    public string? ReleaseKey { get; set; }
    public bool GoodPace { get; set; }
    public bool GoodReflexes { get; set; }
    public bool GoodPickoff { get; set; }

    public PitchingMechanicsSpecialAbilitiesParameters GetParameters()
    {
      return new PitchingMechanicsSpecialAbilitiesParameters
      {
        GoodDelivery = GoodDelivery,
        Release = Enum.Parse<Special2_4>(ReleaseKey!),
        GoodPace = GoodPace,
        GoodReflexes = GoodReflexes,
        GoodPickoff = GoodPickoff
      };
    }
  }

  public class PitchQualitiesSpecialAbilitiesRequest
  {
    public string? PowerOrBreakingBallPitcherKey { get; set; }
    public string? FastballLifeKey { get; set; }
    public string? SpinKey { get; set; }
    public string? SafeOrFatPitchKey { get; set; }
    public string? GroundBallOrFlyBallPitcherKey { get; set; }
    public bool GoodLowPitch { get; set; }
    public bool Gyroball { get; set; }
    public bool ShuttoSpin { get; set; }

    public PitchQualitiesSpecialAbilitiesParameters GetParameters()
    {
      return new PitchQualitiesSpecialAbilitiesParameters
      {
        PowerOrBreakingBallPitcher = PowerOrBreakingBallPitcherKey != null
          ? Enum.Parse<PowerOrBreakingBallPitcher>(PowerOrBreakingBallPitcherKey)
          : null,
        FastballLife = Enum.Parse<Special2_4>(FastballLifeKey!),
        Spin = Enum.Parse<Special2_4>(SpinKey!),
        SafeOrFatPitch = Enum.Parse<SpecialPositive_Negative>(SafeOrFatPitchKey!),
        GroundBallOrFlyBallPitcher = Enum.Parse<SpecialPositive_Negative>(GroundBallOrFlyBallPitcherKey!),
        GoodLowPitch = GoodLowPitch,
        Gyroball = Gyroball,
        ShuttoSpin = ShuttoSpin
      };
    }
  }
}
