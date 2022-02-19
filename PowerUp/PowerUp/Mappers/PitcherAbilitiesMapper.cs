using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;
using System;

namespace PowerUp.Mappers
{
  public static class PitcherAbilitiesMapper
  {
    // MPH/KMH Conversion
    private const double MPH_KMH = 0.62137119;
    public static ushort TwoSeamType => 2;

    /// <summary>
    /// When going from KMH to MPH, we round down.
    /// When going from MPH to KMH, we round up.
    /// </summary>
    public static ushort ToKMH(this double mph) => (ushort)Math.Ceiling(mph / MPH_KMH);

    public static PitcherAbilities GetPitcherAbilities(this GSPlayer gsPlayer)
    {
      return new PitcherAbilities
      {
        TopSpeedMph = gsPlayer.TopThrowingSpeedKMH!.Value * MPH_KMH,
        Control = gsPlayer.Control!.Value,
        Stamina = gsPlayer.Stamina!.Value,
        HasTwoSeam = gsPlayer.TwoSeamType == TwoSeamType,
        TwoSeamMovement = gsPlayer.TwoSeamMovement != 0
          ? gsPlayer.TwoSeamMovement
          : null,
        Slider1Type = gsPlayer.Slider1Type != 0
          ? (SliderType?)gsPlayer.Slider1Type
          : null,
        Slider1Movement = gsPlayer.Slider1Movement != 0
          ? gsPlayer.Slider1Movement
          : null,
        Slider2Type = gsPlayer.Slider2Type != 0
          ? (SliderType?)gsPlayer.Slider2Type
          : null,
        Slider2Movement = gsPlayer.Slider2Movement != 0
          ? gsPlayer.Slider2Movement
          : null,
        Curve1Type = gsPlayer.Curve1Type != 0
          ? (CurveType?)gsPlayer.Curve1Type
          : null,
        Curve1Movement = gsPlayer.Curve1Movement != 0
          ? gsPlayer.Curve1Movement
          : null,
        Curve2Type = gsPlayer.Curve2Type != 0
          ? (CurveType?)gsPlayer.Curve2Type
          : null,
        Curve2Movement = gsPlayer.Curve2Movement != 0
          ? gsPlayer.Curve2Movement
          : null,
        Fork1Type = gsPlayer.Fork1Type != 0
          ? (ForkType?)gsPlayer.Fork1Type
          : null,
        Fork1Movement = gsPlayer.Fork1Movement != 0
          ? gsPlayer.Fork1Movement
          : null,
        Fork2Type = gsPlayer.Fork2Type != 0
          ? (ForkType?)gsPlayer.Fork2Type
          : null,
        Fork2Movement = gsPlayer.Fork2Movement != 0
          ? gsPlayer.Fork2Movement
          : null,
        Sinker1Type = gsPlayer.Sinker1Type != 0
          ? (SinkerType?)gsPlayer.Sinker1Type
          : null,
        Sinker1Movement = gsPlayer.Sinker1Movement != 0
          ? gsPlayer.Sinker1Movement
          : null,
        Sinker2Type = gsPlayer.Sinker2Type != 0
          ? (SinkerType?)gsPlayer.Sinker2Type
          : null,
        Sinker2Movement = gsPlayer.Sinker2Movement != 0
          ? gsPlayer.Sinker2Movement
          : null,
        SinkingFastball1Type = gsPlayer.SinkingFastball1Type != 0
          ? (SinkingFastballType?)gsPlayer.SinkingFastball1Type
          : null,
        SinkingFastball1Movement = gsPlayer.SinkingFastball1Movement != 0
          ? gsPlayer.SinkingFastball1Movement
          : null,
        SinkingFastball2Type = gsPlayer.SinkingFastball2Type != 0
          ? (SinkingFastballType?)gsPlayer.SinkingFastball2Type
          : null,
        SinkingFastball2Movement = gsPlayer.SinkingFastball2Movement != 0
          ? gsPlayer.SinkingFastball2Movement
          : null
      };
    }
  }
}
