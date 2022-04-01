using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;

namespace PowerUp.Mappers.Players
{
  public static class SpecialAbilitiesMapper
  {
    public static SpecialAbilities GetSpecialAbilities(GSPlayer player)
    {
      return new SpecialAbilities
      {
        General = GetGeneralSpecialAbilities(player),
        Hitter = GetHitterSpecialAbilities(player),
        Pitcher = GetPitcherSpecialAbilities(player),
      };
    } 

    public static GeneralSpecialAbilities GetGeneralSpecialAbilities(GSPlayer player)
    {
      return new GeneralSpecialAbilities
      {
        IsStar = player.IsStar!.Value,
        Durability = (Special2_4)player.Durability!,
        Morale = (SpecialPositive_Negative)player.Morale!,
      };
    }

    public static HitterSpecialAbilities GetHitterSpecialAbilities(GSPlayer player)
    {
      return new HitterSpecialAbilities
      {
        SituationalHitting = GetSituationalHittingSpecialAbilities(player),
        HittingApproach = GetHittingApproachSpecialAbilities(player),
        SmallBall = GetSmallBallSpecialAbilities(player),
        BaseRunning = GetBaseRunningSpecialAbilities(player),
        Fielding = GetFieldingSpecialAbilities(player)
      };
    }

    public static SituationalHittingSpecialAbilities GetSituationalHittingSpecialAbilities(GSPlayer player)
    {
      return new SituationalHittingSpecialAbilities
      {
        Consistency = (Special2_4)player.HittingConsistency!,
        VersusLefty = player.HittingVersusLefty1 != 0
          ? (Special1_5)player.HittingVersusLefty1!
          : (Special1_5)player.HittingVersusLefty2!,
        IsTableSetter = player.IsTableSetter!.Value,
        IsBackToBackHitter = player.IsBackToBackHitter!.Value,
        IsHotHitter = player.IsHotHitter!.Value,
        IsRallyHitter = player.IsRallyHitter!.Value,
        BasesLoadedHitter = player.BasesLoadedHitter != 0
         ? (BasesLoadedHitter)player.BasesLoadedHitter!.Value
         : null,
        WalkOffHitter = player.WalkoffHitter != 0
          ? (WalkOffHitter)player.WalkoffHitter!.Value
          : null,
        ClutchHitter = (Special1_5)player.ClutchHitter!.Value
      };
    }

    public static HittingApproachSpecialAbilities GetHittingApproachSpecialAbilities(GSPlayer player)
    {
      return new HittingApproachSpecialAbilities
      {
        ContactHitter = player.IsContactHitter!.Value,
        PowerHitter = player.IsPowerHitter!.Value,
        SluggerOrSlapHitter = player.SlugOrSlap != 0
          ? (SluggerOrSlapHitter)player.SlugOrSlap!.Value
          : null,
        PushHitter = player.IsPushHitter!.Value,
        PullHitter = player.IsPullHitter!.Value,
        SprayHitter = player.IsSprayHitter!.Value,
        FirstballHitter = player.IsFirstballHitter!.Value,
        AggressiveOrPatientHitter = player.AggressiveOrPatientHitter != 0
          ? (AggressiveOrPatient)player.AggressiveOrPatientHitter!.Value
          : null,
        Refined = player.IsRefinedHitter!.Value,
        ToughOut = player.IsToughOut!.Value,
        Intimidator = player.IsIntimidatingHitter!.Value,
        Sparkplug = player.IsSparkplug!.Value
      };
    }

    public static SmallBallSpecialAbilities GetSmallBallSpecialAbilities(GSPlayer player)
    {
      return new SmallBallSpecialAbilities
      {
        SmallBall = (SpecialPositive_Negative)player.SmallBall!.Value,
        Bunting = player.Bunting != 0
          ? (BuntingAbility)player.Bunting!.Value
          : null,
        InfieldHitting = player.InfieldHitter != 0
          ? (InfieldHittingAbility)player.InfieldHitter!.Value
          : null,
      };
    }

    public static BaseRunningSpecialAbilities GetBaseRunningSpecialAbilities(GSPlayer player)
    {
      return new BaseRunningSpecialAbilities
      {
        BaseRunning = (Special2_4)player.BaseRunning!.Value,
        Stealing = (Special2_4)player.Stealing!.Value,
        AggressiveRunner = player.IsAggressiveBaserunner!.Value,
        AggressiveOrPatientBaseStealer = (AggressiveOrPatient)player.AggressiveOrCautiousBaseStealer!.Value,
        ToughRunner = player.IsToughRunner!.Value,
        BreakupDoublePlay = player.WillBreakupDoublePlay!.Value,
        HeadFirstSlide = player.WillSlideHeadFirst!.Value
      };
    }

    public static FieldingSpecialAbilities GetFieldingSpecialAbilities(GSPlayer player)
    {
      return new FieldingSpecialAbilities
      {
        GoldGlover = player.IsGoldGlover!.Value,
        SpiderCatch = player.CanSpiderCatch!.Value,
        BarehandCatch = player.CanBarehandCatch!.Value,
        AggressiveFielder = player.IsAggressiveFielder!.Value,
        PivotMan = player.IsPivotMan!.Value,
        ErrorProne = player.IsErrorProne!.Value,
        GoodBlocker = player.IsGoodBlocker!.Value,
        CatchingAbility = (CatchingAbility)player.Catching!.Value,
        Throwing = (Special2_4)player.Throwing!.Value,
        CannonArm = player.HasCannonArm!.Value,
        TrashTalk = player.IsTrashTalker!.Value
      };
    }

    public static PitcherSpecialAbilities GetPitcherSpecialAbilities(GSPlayer player)
    {
      return new PitcherSpecialAbilities
      {
        SituationalPitching = GetSituationalPitchingSpecialAbilities(player),
        Demeanor = GetPitchingDemeanorSpecialAbilities(player),
        PitchingMechanics = GetPitchingMechanicsSpecialAbilities(player),
        PitchQuailities = GetPitchQuailitiesSpecialAbilities(player),
      };
    }

    public static SituationalPitchingSpecialAbilities GetSituationalPitchingSpecialAbilities(GSPlayer player)
    {
      return new SituationalPitchingSpecialAbilities
      {
        Consistency = (Special2_4)player.PitchingConsistency!.Value,
        VersusLefty = (Special2_4)player.VersusLeftHandedBatter!.Value,
        Poise = (Special2_4)player.Poise!.Value,
        PoorVersusRunner = player.PoorVersusRunner!.Value,
        WithRunnersInSocringPosition = (Special2_4)player.WithRunnersInScoringPosition!.Value,
        SlowStarter = player.IsSlowStarter!.Value,
        StarterFinisher = player.IsStarterFinisher!.Value,
        ChokeArtist = player.IsChokeArtist!.Value,
        Sandbag = player.IsSandbag!.Value,
        DoctorK = player.DoctorK!.Value,
        Walk = player.WalkProne!.Value,
        Lucky = (SpecialPositive_Negative)player.Luck!.Value,
        Recovery = (Special2_4)player.Recovery!.Value
      };
    }

    public static PitchingDemeanorSpecialAbilities GetPitchingDemeanorSpecialAbilities(GSPlayer player)
    {
      return new PitchingDemeanorSpecialAbilities
      {
        Intimidator = player.IsIntimidatingPitcher!.Value,
        BattlerPokerFace = player.IsBattler!.Value
          ? BattlerPokerFace.Battler
          : player.HasPokerFace!.Value
            ? BattlerPokerFace.PokerFace
            : null,
        HotHead = player.IsHotHead!.Value
      };
    }

    public static PitchingMechanicsSpecialAbilities GetPitchingMechanicsSpecialAbilities(GSPlayer player)
    {
      return new PitchingMechanicsSpecialAbilities
      {
        GoodDelivery = player.GoodDelivery!.Value,
        Release = (Special2_4)player.Release!.Value,
        GoodPace = player.HasGoodPace!.Value,
        GoodReflexes = player.HasGoodReflexes!.Value,
        GoodPickoff = player.GoodPickoff!.Value
      };
    }

    public static PitchQuailitiesSpecialAbilities GetPitchQuailitiesSpecialAbilities(GSPlayer player)
    {
      return new PitchQuailitiesSpecialAbilities
      {
        PowerOrBreakingBallPitcher = player.PowerOrBreakingBallPitcher != 0
          ? (PowerOrBreakingBallPitcher)player.PowerOrBreakingBallPitcher!.Value
          : null,
        FastballLife = (Special2_4)player.FastballLife!.Value,
        Spin = (Special2_4)player.Spin!.Value,
        SafeOrFatPitch = (SpecialPositive_Negative)player.SafeOrFatPitch!.Value,
        GroundBallOrFlyBallPitcher = (SpecialPositive_Negative)player.GroundBallOrFlyBallPitcher!.Value,
        GoodLowPitch = player.GoodLowPitch!.Value,
        Gyroball = player.Gyroball!.Value,
        ShuttoSpin = player.ShuttoSpin!.Value
      };
    }
  }
}
