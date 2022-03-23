using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class SavePlayerCommand : ICommand<SavePlayerRequest, ResultResponse>
  {
    private readonly IPlayerApi _playerApi;

    public SavePlayerCommand(IPlayerApi playerApi)
    {
      _playerApi = playerApi;
    }

    public ResultResponse Execute(SavePlayerRequest request)
    {
      if (!request.PlayerId.HasValue)
        throw new ArgumentNullException(nameof(request.PlayerId));

      var player = DatabaseConfig.PlayerDatabase.Load(request.PlayerId!.Value);
      _playerApi.UpdatePlayer(player, request.GetParameters());
      DatabaseConfig.PlayerDatabase.Save(player);

      return ResultResponse.Succeeded();
    }
  }

  public class SavePlayerRequest
  {
    public int? PlayerId { get; set; }
    public PersonalDetailsRequest? PersonalDetails { get; set; }
    public PositionCapabilitiesRequest? PositionCapabilities { get; set; }
    public HitterAbilitiesRequest? HitterAbilities { get; set; }

    public PlayerParameters GetParameters()
    {
      return new PlayerParameters
      {
        PersonalDetails = PersonalDetails!.GetParameters(),
        PositionCapabilities = PositionCapabilities!.GetParameters(),
        HitterAbilities = HitterAbilities!.GetParameters(),
      };
    }
  }

  public class PersonalDetailsRequest
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool UseSpecialSavedName { get; set; }
    public string? SavedName { get; set; }
    public string? UniformNumber { get; set; }
    public string? PositionKey { get; set; }
    public string? PitcherTypeKey { get; set; }
    public int? VoiceId { get; set; }
    public string? BattingSideKey { get; set; }
    public int? BattingStanceId { get; set; }
    public string? ThrowingArmKey { get; set; }
    public int? PitchingMechanicsId { get; set; }

    public PlayerPersonalDetailsParameters GetParameters()
    {
      return new PlayerPersonalDetailsParameters
      {
        FirstName = FirstName,
        LastName = LastName,
        KeepSpecialSavedName = UseSpecialSavedName,
        SavedName = SavedName,
        UniformNumber = UniformNumber,
        Position = Enum.Parse<Position>(PositionKey!),
        PitcherType = Enum.Parse<PitcherType>(PitcherTypeKey!),
        VoiceId = VoiceId,
        BattingSide = Enum.Parse<BattingSide>(BattingSideKey!),
        BattingStanceId = BattingStanceId,
        ThrowingArm = Enum.Parse<ThrowingArm>(ThrowingArmKey!),
        PitchingMechanicsId = PitchingMechanicsId,
      };
    }
  }

  public class PositionCapabilitiesRequest
  {
    public string? Pitcher { get; set; }
    public string? Catcher { get; set; }
    public string? FirstBase { get; set; }
    public string? SecondBase { get; set; }
    public string? ThirdBase { get; set; }
    public string? Shortstop { get; set; }
    public string? LeftField { get; set; }
    public string? CenterField { get; set; }
    public string? RightField { get; set; }

    public PlayerPositionCapabilitiesParameters GetParameters()
    {
      return new PlayerPositionCapabilitiesParameters()
      {
        Pitcher = Enum.Parse<Grade>(Pitcher!),
        Catcher = Enum.Parse<Grade>(Catcher!),
        FirstBase = Enum.Parse<Grade>(FirstBase!),
        SecondBase = Enum.Parse<Grade>(SecondBase!),
        ThirdBase = Enum.Parse<Grade>(ThirdBase!),
        Shortstop = Enum.Parse<Grade>(Shortstop!),
        LeftField = Enum.Parse<Grade>(LeftField!),
        CenterField = Enum.Parse<Grade>(CenterField!),
        RightField = Enum.Parse<Grade>(RightField!)
      };
    }
  }
  
  public class HitterAbilitiesRequest
  {
    public int Trajectory { get; set; }
    public int Contact { get; set; }
    public int Power { get; set; }
    public int RunSpeed { get; set; }
    public int ArmStrength { get; set; }
    public int Fielding { get; set; }
    public int ErrorResistance { get; set; }
    public HotZoneGridDto? HotZoneGrid { get; set; }

    public PlayerHitterAbilityParameters GetParameters()
    {
      return new PlayerHitterAbilityParameters
      {
        Trajectory = Trajectory,
        Contact = Contact,
        Power = Power,
        RunSpeed = RunSpeed,
        ArmStrength = ArmStrength,
        Fielding = Fielding,
        ErrorResistance = ErrorResistance,
        HotZoneGridParameters = HotZoneGrid?.GetParameters()
      };
    }
  }
}
