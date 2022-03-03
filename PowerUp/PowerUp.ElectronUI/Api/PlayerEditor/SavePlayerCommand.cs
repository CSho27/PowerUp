using PowerUp.Databases;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class SavePlayerCommand : ICommand<SavePlayerRequest, object>
  {
    private readonly IPlayerApi _playerApi;

    public SavePlayerCommand(IPlayerApi playerApi)
    {
      _playerApi = playerApi;
    }

    public object Execute(SavePlayerRequest request)
    {
      if(request.PlayerKey == null)
        throw new ArgumentNullException(nameof(request.PlayerKey));

      var player = DatabaseConfig.JsonDatabase.Load<Player>(request.PlayerKey);
      _playerApi.UpdatePlayer(player, request.GetParameters());
      DatabaseConfig.JsonDatabase.Save(player);

      return new { Result = "Great Success!" };
    }
  }

  public class SavePlayerRequest
  {
    public string? PlayerKey { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool UseSpecialSavedName { get; set; }
    public string? SavedName { get; set; }
    public string? UniformNumber { get; set; }
    public string? PositionKey  { get; set; }
    public string? PitcherTypeKey { get; set; }
    public int? VoiceId { get; set; }
    public string? BattingSideKey { get; set; }
    public int? BattingStanceId { get; set; }
    public string? ThrowingArmKey { get; set; }
    public int? PitchingMechanicsId { get; set; }

    public PlayerParameters GetParameters()
    {
      return new PlayerParameters
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
}
