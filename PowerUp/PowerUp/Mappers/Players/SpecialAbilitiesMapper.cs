using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
      };
    } 

    public static GeneralSpecialAbilities GetGeneralSpecialAbilities(GSPlayer player)
    {
      return new GeneralSpecialAbilities
      {
        Star = player.IsStar!.Value,
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
        TableSetter = player.IsTableSetter!.Value,
        BackToBackHitter = player.IsBackToBackHitter!.Value,
        HotHitter = player.IsHotHitter!.Value
      };
    }

    public static HittingApproachSpecialAbilities GetHittingApproachSpecialAbilities(GSPlayer player)
    {
      return new HittingApproachSpecialAbilities
      {
        ContactHitter = player.IsContactHitter!.Value,
        PowerHitter = player.IsPowerHitter!.Value,
        SluggerOrSlapHitter = (SluggerOrSlapHitter)player.SlugOrSlap!.Value,
        PushHitter = player.IsPushHitter!.Value,
        PullHitter = player.IsPullHitter!.Value,
        SprayHitter = player.IsSprayHitter!.Value,
        FirstballHitter = player.IsFirstballHitter!.Value,
        AggressiveOrPatientHitter = (AggressiveOrPatient)player.AggressiveOrPatientHitter!.Value,
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
        Bunting = (BuntingAbility)player.Bunting!.Value,
        InfieldHitting = (InfieldHittingAbility)player.InfieldHitter!.Value,
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
  }
}
