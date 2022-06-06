using PowerUp.Databases;
using PowerUp.Entities.Players;

namespace PowerUp.ElectronUI.Api.PlayerDetails
{
  public class GetPlayerFlyoutDetailsCommand : ICommand<PlayerFlyoutDetailsRequest, PlayerFlyoutDetailsResponse>
  {
    public PlayerFlyoutDetailsResponse Execute(PlayerFlyoutDetailsRequest request)
    {
      var player = DatabaseConfig.Database.Load<Player>(request.PlayerId)!;
      return new PlayerFlyoutDetailsResponse(player);
    }
  }

  public class PlayerFlyoutDetailsRequest
  {
    public int PlayerId { get; set; }
  }

  public class PlayerFlyoutDetailsResponse
  {
    public int PlayerId { get; }
    public string SourceType { get; }
    public string PrimaryPosition { get; }
    public string SavedName { get; }
    public string InformalDisplayName { get; }
    public string UniformNumber { get; }
    public int Overall { get; }
    public HitterDetailsDto HitterDetails { get; }
    public PitcherDetailsDto PitcherDetails { get; }
    public PositionCapabilitiesDto PositionCapabilities { get; }

    public PlayerFlyoutDetailsResponse(Player player)
    {
      PlayerId = player.Id!.Value;
      SourceType = player.SourceType.ToString();
      PrimaryPosition = player.PrimaryPosition.ToString();
      SavedName = player.SavedName;
      InformalDisplayName = player.InformalDisplayName;
      UniformNumber = player.UniformNumber;
      Overall = player.Overall.RoundDown();
      HitterDetails = new HitterDetailsDto(player);
      PitcherDetails = new PitcherDetailsDto(player);
      PositionCapabilities = new PositionCapabilitiesDto(player.PositonCapabilities);
    }
  }

  public class HitterDetailsDto
  {
    public int Contact { get; }
    public int Power { get; }
    public int RunSpeed { get; }
    public int ArmStrength { get; }
    public int Fielding { get; }
    public int Trajectory { get; }
    public int ErrorResistance { get; }
    public string Positions { get; }
    public string BatsAndThrows { get; }

    public HitterDetailsDto(Player player)
    {
      var hitterAbilities = player.HitterAbilities;
      var positionList = player.PositonCapabilities.GetDictionary()
        .OrderByDescending(kvp => kvp.Value)
        .ThenByDescending(kvp => kvp.Key == player.PrimaryPosition)
        .Where(kvp => kvp.Value >= Grade.D)
        .Select(kvp => kvp.Key.GetAbbrev());

      Contact = hitterAbilities.Contact;
      Power = hitterAbilities.Power;
      RunSpeed = hitterAbilities.RunSpeed;
      ArmStrength = hitterAbilities.ArmStrength;
      Fielding = hitterAbilities.Fielding;
      Trajectory = hitterAbilities.Trajectory;
      ErrorResistance = hitterAbilities.ErrorResistance;
      Positions = string.Join(", ", positionList);
      BatsAndThrows = player.BatsAndThrows;
    }
  }

  public class PitcherDetailsDto
  {
    public int TopSpeed { get; }
    public string ThrowingArm { get; }
    public string PitcherType { get; }
    public int Control { get; }
    public int Stamina { get; }

    public string? TwoSeamType { get; }
    public int? TwoSeamMovement { get; }

    public string? Slider1Type { get; }
    public int? Slider1Movement { get; }

    public string? Slider2Type { get; }
    public int? Slider2Movement { get; }

    public string? Curve1Type { get; }
    public int? Curve1Movement { get; }

    public string? Curve2Type { get; }
    public int? Curve2Movement { get; }

    public string? Fork1Type { get; }
    public int? Fork1Movement { get; }

    public string? Fork2Type { get; }
    public int? Fork2Movement { get; }

    public string? Sinker1Type { get; }
    public int? Sinker1Movement { get; }

    public string? Sinker2Type { get; }
    public int? Sinker2Movement { get; set; }

    public string? SinkingFastball1Type { get; }
    public int? SinkingFastball1Movement { get; }

    public string? SinkingFastball2Type { get; }
    public int? SinkingFastball2Movement { get; }

    public PitcherDetailsDto(Player player)
    {
      var pitcherAbilities = player.PitcherAbilities;

      TopSpeed = pitcherAbilities.TopSpeedMph.RoundDown();
      ThrowingArm = player.ThrowingArm.GetDisplayName();
      PitcherType = player.PitcherType.GetDisplayName();
      Control = pitcherAbilities.Control;
      Stamina = pitcherAbilities.Stamina;

      TwoSeamType = pitcherAbilities.HasTwoSeam
        ? "2sfb"
        : null;
      TwoSeamMovement = pitcherAbilities.TwoSeamMovement;
      Slider1Type = pitcherAbilities.Slider1Type?.GetAbbrev();
      Slider1Movement = pitcherAbilities.Slider1Movement;
      Slider2Type = pitcherAbilities.Slider2Type?.GetAbbrev();
      Slider2Movement = pitcherAbilities.Slider2Movement;
      Curve1Type = pitcherAbilities.Curve1Type?.GetAbbrev();
      Curve1Movement = pitcherAbilities.Curve1Movement;
      Curve2Type = pitcherAbilities.Curve2Type?.GetAbbrev();
      Curve2Movement = pitcherAbilities.Curve2Movement;
      Fork1Type = pitcherAbilities.Fork1Type?.GetAbbrev();
      Fork1Movement = pitcherAbilities.Fork1Movement;
      Fork2Type = pitcherAbilities.Fork2Type?.GetAbbrev();
      Fork2Movement = pitcherAbilities.Fork2Movement;
      Sinker1Type = pitcherAbilities.Sinker1Type?.GetAbbrev();
      Sinker1Movement = pitcherAbilities.Sinker1Movement;
      Sinker2Type = pitcherAbilities.Sinker2Type?.GetAbbrev();
      Sinker2Movement = pitcherAbilities.Sinker2Movement;
      SinkingFastball1Type = pitcherAbilities.SinkingFastball1Type?.GetAbbrev();
      SinkingFastball1Movement = pitcherAbilities.SinkingFastball1Movement;
      SinkingFastball2Type = pitcherAbilities.SinkingFastball2Type?.GetAbbrev();
      SinkingFastball2Movement = pitcherAbilities.SinkingFastball2Movement;
    }
  }

  public class PositionCapabilitiesDto
  {
    public string Pitcher { get; }
    public string Catcher { get; }
    public string FirstBase { get; }
    public string SecondBase { get; }
    public string ThirdBase { get; }
    public string Shortstop { get; }
    public string LeftField { get; }
    public string CenterField { get; }
    public string RightField { get; }

    public PositionCapabilitiesDto(PositionCapabilities positionCapabilities)
    {
      Pitcher = positionCapabilities.Pitcher.ToString();
      Catcher = positionCapabilities.Catcher.ToString();
      FirstBase = positionCapabilities.FirstBase.ToString();
      SecondBase = positionCapabilities.SecondBase.ToString();
      ThirdBase = positionCapabilities.ThirdBase.ToString();
      Shortstop = positionCapabilities.Shortstop.ToString();
      LeftField = positionCapabilities.LeftField.ToString();
      CenterField = positionCapabilities.CenterField.ToString();
      RightField = positionCapabilities.RightField.ToString();
    }
  }
}
