using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;

namespace PowerUp.Mappers.Players
{
  public static class PositionCapabilitiesMapper
  {
    public static PositionCapabilities GetPositionCapabilities(IGSPlayer gsPlayer)
    {
      return new PositionCapabilities
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
