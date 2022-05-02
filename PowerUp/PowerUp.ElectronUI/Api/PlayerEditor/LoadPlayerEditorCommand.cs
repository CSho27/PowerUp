using PowerUp.Databases;
using PowerUp.Entities.Players;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class LoadPlayerEditorCommand : ICommand<LoadPlayerEditorRequest, PlayerEditorResponse>
  {
    private readonly IVoiceLibrary _voiceLibrary;
    private readonly IBattingStanceLibrary _batttingStanceLibrary;
    private readonly IPitchingMechanicsLibrary _pitchingMechanicsLibrary;
    private readonly IFaceLibrary _faceLibrary;

    public LoadPlayerEditorCommand(
      IVoiceLibrary voiceLibrary,
      IBattingStanceLibrary battingStanceLibrary,
      IPitchingMechanicsLibrary pitchingMechanicsLibrary,
      IFaceLibrary faceLibrary
    )
    {
      _voiceLibrary = voiceLibrary;
      _batttingStanceLibrary = battingStanceLibrary;
      _pitchingMechanicsLibrary = pitchingMechanicsLibrary;
      _faceLibrary = faceLibrary;
    }

    public PlayerEditorResponse Execute(LoadPlayerEditorRequest request)
    {
      var player = DatabaseConfig.Database.Load<Player>(request.PlayerId!.Value);
      return new PlayerEditorResponse(_voiceLibrary, _batttingStanceLibrary, _pitchingMechanicsLibrary, _faceLibrary, player!);
    }
  }

  public class LoadPlayerEditorRequest
  {
    public int? PlayerId { get; set; }
  }

  
}
