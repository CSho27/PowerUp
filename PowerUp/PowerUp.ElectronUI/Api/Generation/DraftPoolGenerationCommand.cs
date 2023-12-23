using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Generators;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.Generation
{
  public class DraftPoolGenerationCommand : ICommand<DraftPoolGenerationRequest, DraftPoolGenerationResponse>
  {
    private readonly IDraftPoolGenerator _draftPoolGenerator;
    private readonly IVoiceLibrary _voiceLibrary;
    private readonly ISkinColorGuesser _skinColorGuesser;
    private readonly IBattingStanceGuesser _batttingStanceGuesser;
    private readonly IPitchingMechanicsGuesser _pitchingMechanicsGuesser;

    public DraftPoolGenerationCommand(
      IDraftPoolGenerator draftPoolGenerator,
      IVoiceLibrary voiceLibrary, 
      ISkinColorGuesser skinColorGuesser, 
      IBattingStanceGuesser batttingStanceGuesser, 
      IPitchingMechanicsGuesser pitchingMechanicsGuesser
    )
    {
      _draftPoolGenerator = draftPoolGenerator;
      _voiceLibrary = voiceLibrary;
      _skinColorGuesser = skinColorGuesser;
      _batttingStanceGuesser = batttingStanceGuesser;
      _pitchingMechanicsGuesser = pitchingMechanicsGuesser;
    }

    public DraftPoolGenerationResponse Execute(DraftPoolGenerationRequest request)
    {
      var algorithm = new LSStatistcsPlayerGenerationAlgorithm(
        _voiceLibrary,
        _skinColorGuesser,
        _batttingStanceGuesser,
        _pitchingMechanicsGuesser
      );
      var draftPool = _draftPoolGenerator.GenerateDraftPool(algorithm, request.Size)
        .GetAwaiter()
        .GetResult();
      DatabaseConfig.Database.SaveAll(draftPool);
      return new DraftPoolGenerationResponse
      {
        Players = draftPool.Select(p => new PlayerDetailsResponse(p))
      };
    }
  }

  public class DraftPoolGenerationRequest 
  {
    public int Size { get; set; }
  }

  public class DraftPoolGenerationResponse 
  {
    public IEnumerable<PlayerDetailsResponse> Players { get; init; } = Enumerable.Empty<PlayerDetailsResponse>();
  }
}
