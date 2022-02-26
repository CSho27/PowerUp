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
    GSPlayer MapToGSPlayer(Player player, MLBPPTeam mlbPPTeam);
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
        SavedName = gsPlayer.SavedName!.Contains("*")
          ? _savedNameLibrary[(int)gsPlayer.SpecialSavedNameId!]
          : gsPlayer.SavedName,
        SpecialSavedNameId = gsPlayer.SavedName.Contains('*')
          ? (int)gsPlayer.SpecialSavedNameId!
          : gsPlayer.SpecialSavedNameId,
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
        ThrowingSide = gsPlayer.ThrowsLefty!.Value
          ? ThrowingSide.Left
          : ThrowingSide.Right,
        PitchingMechanicsId = gsPlayer.PitchingForm!.Value,
        PositonCapabilities = gsPlayer.GetPositionCapabilities(),
        HitterAbilities = gsPlayer.GetHitterAbilities(),
        PitcherAbilities = gsPlayer.GetPitcherAbilities(),
      };
    }

    public GSPlayer MapToGSPlayer(Player player, MLBPPTeam mlbPPTeam)
    {
      var gsPlayerNumber = player.UniformNumber.ToGSUniformNumber();
      var gsPitcherType = player.PitcherType.ToGSPitcherType();
      var positionCapabilities = player.PositonCapabilities;
      var hitterAbilities = player.HitterAbilities;
      var hotZones = player.HitterAbilities.HotZones;
      var pitcherAbilities = player.PitcherAbilities;

      return new GSPlayer
      {
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
        ThrowsLefty = player.ThrowingSide == ThrowingSide.Left,
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
      };
    }

  }
}
