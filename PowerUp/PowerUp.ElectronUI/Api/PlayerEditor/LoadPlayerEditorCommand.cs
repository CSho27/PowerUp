﻿using PowerUp.Databases;
using PowerUp.Entities.Players;
using PowerUp.Libraries;
using PowerUp.Providers;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class LoadPlayerEditorCommand : ICommand<LoadPlayerEditorRequest, PlayerEditorResponse>
  {
    private readonly IVoiceLibrary _voiceLibrary;
    private readonly IBattingStanceLibrary _batttingStanceLibrary;
    private readonly IPitchingMechanicsLibrary _pitchingMechanicsLibrary;
    private readonly IBaseballReferenceUrlProvider _baseballReferenceUrlProvider;
    private readonly IFaceLibrary _faceLibrary;

    public LoadPlayerEditorCommand(
      IVoiceLibrary voiceLibrary,
      IBattingStanceLibrary battingStanceLibrary,
      IPitchingMechanicsLibrary pitchingMechanicsLibrary,
      IFaceLibrary faceLibrary,
      IBaseballReferenceUrlProvider baseballReferenceUrlProvider
    )
    {
      _voiceLibrary = voiceLibrary;
      _batttingStanceLibrary = battingStanceLibrary;
      _pitchingMechanicsLibrary = pitchingMechanicsLibrary;
      _faceLibrary = faceLibrary;
      _baseballReferenceUrlProvider = baseballReferenceUrlProvider;
    }

    public Task<PlayerEditorResponse> Execute(LoadPlayerEditorRequest request)
    {
      var player = DatabaseConfig.Database.Load<Player>(request.PlayerId!.Value);
      return Task.FromResult(new PlayerEditorResponse(_voiceLibrary, _batttingStanceLibrary, _pitchingMechanicsLibrary, _faceLibrary, _baseballReferenceUrlProvider, player!));
    }
  }

  public class LoadPlayerEditorRequest
  {
    public int? PlayerId { get; set; }
  }
}
