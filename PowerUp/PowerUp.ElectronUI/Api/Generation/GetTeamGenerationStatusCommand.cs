using PowerUp.Databases;
using PowerUp.Entities.GenerationResults;
using PowerUp.Generators;

namespace PowerUp.ElectronUI.Api.Generation
{
  public class GetTeamGenerationStatusCommand : ICommand<GetTeamGenerationStatusRequest, GetTeamGenerationStatusResponse>
  {
    public GetTeamGenerationStatusResponse Execute(GetTeamGenerationStatusRequest request)
    {
      var status = DatabaseConfig.Database.Load<TeamGenerationStatus>(request.GenerationStatusId)!;
      return new GetTeamGenerationStatusResponse(status.Progress, status.TeamId);
    }
  }

  public class GetTeamGenerationStatusRequest
  {
    public int GenerationStatusId { get; set; }
  }

  public class GetTeamGenerationStatusResponse
  {
    public string CurrentAction { get; }
    public string PercentCompletion { get; }
    public bool IsComplete => CompletedTeamId.HasValue;
    public int? CompletedTeamId { get; }

    public GetTeamGenerationStatusResponse(ProgressUpdate? progress, int? completedTeamId)
    {
      CurrentAction = progress?.CurrentAction ?? "";
      PercentCompletion = progress?.PercentCompletion.ToPercentDisplay() ?? "";
      CompletedTeamId = completedTeamId;
    }
  }
}
