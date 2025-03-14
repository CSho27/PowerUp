﻿using PowerUp.Databases;
using PowerUp.Generators;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.Generation
{
  public class PlayerGenerationCommand : ICommand<PlayerGenerationRequest, PlayerGenerationResponse>
  {
    private readonly IPlayerGenerator _playerGenerator;
    private readonly IVoiceLibrary _voiceLibrary;
    private readonly ISkinColorGuesser _skinColorGuesser;
    private readonly IBattingStanceGuesser _batttingStanceGuesser;
    private readonly IPitchingMechanicsGuesser _pitchingMechanicsGuesser;

    public PlayerGenerationCommand
    ( IPlayerGenerator playerGenerator
    , IVoiceLibrary voiceLibrary
    , ISkinColorGuesser skinColorGuesser
    , IBattingStanceGuesser batttingStanceGuesser
    , IPitchingMechanicsGuesser pitchingMechanicsGuesser
    )
    {
      _playerGenerator = playerGenerator;
      _voiceLibrary = voiceLibrary;
      _skinColorGuesser = skinColorGuesser;
      _batttingStanceGuesser = batttingStanceGuesser;
      _pitchingMechanicsGuesser = pitchingMechanicsGuesser;
    }

    public Task<PlayerGenerationResponse> Execute(PlayerGenerationRequest request)
    {
      var result = _playerGenerator.GeneratePlayer(
        lsPlayerId: request.LSPlayerId,
        year: request.Year,
        generationAlgorithm: new LSStatistcsPlayerGenerationAlgorithm
        ( _voiceLibrary
        , _skinColorGuesser
        , _batttingStanceGuesser
        , _pitchingMechanicsGuesser
        )
      );

      DatabaseConfig.Database.Save(result.Player);
      return Task.FromResult(new PlayerGenerationResponse { PlayerId = result.Player.Id!.Value });
    }
  }

  public class PlayerGenerationRequest
  {
    public long LSPlayerId { get; set; }
    public int Year { get; set; }
  }

  public class PlayerGenerationResponse
  {
    public int PlayerId { get; set; }
  }
}
