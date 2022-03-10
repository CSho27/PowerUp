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
