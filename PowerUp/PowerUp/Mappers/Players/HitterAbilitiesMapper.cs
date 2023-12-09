using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;

namespace PowerUp.Mappers.Players
{
  public static class HitterAbilitiesMapper
  {
    public static HitterAbilities GetHitterAbilities(IGSPlayer gsPlayer)
    {
      return new HitterAbilities
      {
        Trajectory = gsPlayer.Trajectory!.Value + 1,
        Contact = gsPlayer.Contact!.Value,
        Power = gsPlayer.Power!.Value,
        RunSpeed = gsPlayer.RunSpeed!.Value,
        ArmStrength = gsPlayer.ArmStrength!.Value,
        Fielding = gsPlayer.Fielding!.Value,
        ErrorResistance = gsPlayer.ErrorResistance!.Value,
        HotZones = GetHotZones(gsPlayer)
      };
    }

    public static HotZoneGrid GetHotZones(IGSPlayer gsPlayer)
    {
      return new HotZoneGrid
      {
        UpAndIn = (HotZonePreference)gsPlayer.HotZoneUpAndIn!,
        Up = (HotZonePreference)gsPlayer.HotZoneUp!,
        UpAndAway = (HotZonePreference)gsPlayer.HotZoneUpAndAway!,
        MiddleIn = (HotZonePreference)gsPlayer.HotZoneMiddleIn!,
        Middle = (HotZonePreference)gsPlayer.HotZoneMiddle!,
        MiddleAway = (HotZonePreference)gsPlayer.HotZoneMiddleAway!,
        DownAndIn = (HotZonePreference)gsPlayer.HotZoneDownAndIn!,
        Down = (HotZonePreference)gsPlayer.HotZoneDown!,
        DownAndAway = (HotZonePreference)gsPlayer.HotZoneDownAndAway!
      };
    }
  }
}
