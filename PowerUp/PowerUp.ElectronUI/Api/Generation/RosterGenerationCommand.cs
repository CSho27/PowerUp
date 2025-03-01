using PowerUp.Databases;
using PowerUp.Entities.GenerationResults;
using PowerUp.Generators;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.Generation
{
  public class RosterGenerationCommand : ICommand<RosterGenerationRequest, RosterGeneratioResponse>
  {
    private readonly IRosterGenerator _rosterGenerator;
    private readonly IVoiceLibrary _voiceLibrary;
    private readonly ISkinColorGuesser _skinColorGuesser;
    private readonly IBattingStanceGuesser _battingStanceGuesser;
    private readonly IPitchingMechanicsGuesser _pitchingMechanicsGuesser;

    public RosterGenerationCommand
    ( IRosterGenerator rosterGenerator
    , IVoiceLibrary voiceLibrary
    , ISkinColorGuesser skinColorGuesser
    , IBattingStanceGuesser battingStanceGuesser
    , IPitchingMechanicsGuesser pitchingMechanicsGuesser
    )
    {
      _rosterGenerator = rosterGenerator;
      _voiceLibrary = voiceLibrary;
      _skinColorGuesser = skinColorGuesser;
      _battingStanceGuesser = battingStanceGuesser;
      _pitchingMechanicsGuesser = pitchingMechanicsGuesser;
    }

    public Task<RosterGeneratioResponse> Execute(RosterGenerationRequest request)
    {
      var generationStatus = new RosterGenerationStatus(request.Year, DateTime.Now);
      DatabaseConfig.Database.Save(generationStatus);

      Task.Run(() => {
        try
        {
          var result = _rosterGenerator.GenerateRoster
          (year: request.Year
          , playerGenerationAlgorithm: new LSStatistcsPlayerGenerationAlgorithm
            (_voiceLibrary
            , _skinColorGuesser
            , _battingStanceGuesser
            , _pitchingMechanicsGuesser
            )
          , onTeamProgressUpdate: update => UpdateTeamProgressAndSave(update, generationStatus)
          , onPlayerProgressUpdate: update => UpdatePlayerProgressAndSave(update, generationStatus)
          , onFatalError: () => MarkFailedAndSave(generationStatus)
          );

          DatabaseConfig.Database.Save(result.Roster);
          generationStatus.Complete(result.Roster.Id!.Value);
          DatabaseConfig.Database.Save(generationStatus);
        }
        catch (Exception) { }
      });

      return Task.FromResult(new RosterGeneratioResponse { GenerationStatusId = generationStatus.Id!.Value });
    }

    private void MarkFailedAndSave(RosterGenerationStatus status)
    {
      status.IsFailed = true;
      DatabaseConfig.Database.Save(status);
    }

    private void UpdateTeamProgressAndSave(ProgressUpdate update, RosterGenerationStatus status)
    {
      status.UpdateTeamAction(update.CurrentAction, update.CurrentActionIndex, update.TotalActions);
      DatabaseConfig.Database.Save(status);
    }

    private void UpdatePlayerProgressAndSave(ProgressUpdate update, RosterGenerationStatus status)
    {
      status.UpdatePlayerAction(update.CurrentAction, update.CurrentActionIndex, update.TotalActions);
      DatabaseConfig.Database.Save(status);
    }
  }

  public class RosterGenerationRequest
  {
    public int Year { get; set; }
  }

  public class RosterGeneratioResponse
  {
    public int GenerationStatusId { get; set; }
  }
}
