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
  }
}
