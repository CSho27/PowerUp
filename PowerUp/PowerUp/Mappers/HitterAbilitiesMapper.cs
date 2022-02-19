using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;

namespace PowerUp.Mappers
{
  public static class HitterAbilitiesMapper
  {
    public static HitterAbilities GetHitterAbilities(this GSPlayer gsPlayer)
    {
      return new HitterAbilities
      {
        Trajectory = gsPlayer.Trajectory!.Value,
        Contact = gsPlayer.Contact!.Value,
        Power = gsPlayer.Power!.Value,
        RunSpeed = gsPlayer.RunSpeed!.Value,
        ArmStrength = gsPlayer.ArmStrength!.Value,
        Fielding = gsPlayer.Fielding!.Value,
        ErrorResistance = gsPlayer.ErrorResistance!.Value,
        HotZones = gsPlayer.GetHotZones()
      };
    }

    public static HotZoneGrid GetHotZones(this GSPlayer gsPlayer)
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
