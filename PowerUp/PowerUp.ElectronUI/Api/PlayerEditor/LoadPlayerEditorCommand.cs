using PowerUp.Databases;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class LoadPlayerEditorCommand : ICommand<LoadPlayerEditorRequest, PlayerEditorResponse>
  {
    private readonly IVoiceLibrary _voiceLibrary;
    private readonly IBattingStanceLibrary _batttingStanceLibrary;
    private readonly IPitchingMechanicsLibrary _pitchingMechanicsLibrary;

    public LoadPlayerEditorCommand(
      IVoiceLibrary voiceLibrary,
      IBattingStanceLibrary battingStanceLibrary,
      IPitchingMechanicsLibrary pitchingMechanicsLibrary
    )
    {
      _voiceLibrary = voiceLibrary;
      _batttingStanceLibrary = battingStanceLibrary;
      _pitchingMechanicsLibrary = pitchingMechanicsLibrary;
    }

    public PlayerEditorResponse Execute(LoadPlayerEditorRequest request)
    {
      var player = DatabaseConfig.PlayerDatabase.Load(request.PlayerId!.Value);
      return new PlayerEditorResponse(_voiceLibrary, _batttingStanceLibrary, _pitchingMechanicsLibrary, player!);
    }
  }

  public class LoadPlayerEditorRequest
  {
    public int? PlayerId { get; set; }
  }

  
}
