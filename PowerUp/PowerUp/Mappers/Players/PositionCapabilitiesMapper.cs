using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;

namespace PowerUp.Mappers.Players
{
  public static class PositionCapabilitiesMapper
  {
    public static PositonCapabilities GetPositionCapabilities(this GSPlayer gsPlayer)
    {
      return new PositonCapabilities
      {
        Pitcher = (Grade)gsPlayer.PitcherCapability!,
        Catcher = (Grade)gsPlayer.CatcherCapability!,
        FirstBase = (Grade)gsPlayer.FirstBaseCapability!,
        SecondBase = (Grade)gsPlayer.SecondBaseCapability!,
        ThirdBase = (Grade)gsPlayer.ThirdBaseCapability!,
        Shortstop = (Grade)gsPlayer.ShortstopCapability!,
        LeftField = (Grade)gsPlayer.LeftFieldCapability!,
        CenterField = (Grade)gsPlayer.CenterFieldCapability!,
        RightField = (Grade)gsPlayer.RightFieldCapability!
      };
    }
  }
}
