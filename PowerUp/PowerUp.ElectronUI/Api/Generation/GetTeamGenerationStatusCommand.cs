using PowerUp.Databases;
using PowerUp.Entities.GenerationResults;
using PowerUp.Generators;

namespace PowerUp.ElectronUI.Api.Generation
{
  public class GetTeamGenerationStatusCommand : ICommand<GetTeamGenerationStatusRequest, GetTeamGenerationStatusResponse>
  {
    public Task<GetTeamGenerationStatusResponse> Execute(GetTeamGenerationStatusRequest request)
    {
      var status = DatabaseConfig.Database.Load<TeamGenerationStatus>(request.GenerationStatusId)!;
      return Task.FromResult(new GetTeamGenerationStatusResponse(status));
    }
  }

  public class GetTeamGenerationStatusRequest
  {
    public int GenerationStatusId { get; set; }
  }

  public class GetTeamGenerationStatusResponse
  {
    public string CurrentAction { get; }
    public int PercentCompletion { get; }
    public string EstimatedTimeToCompletion { get; }
    public bool IsComplete => CompletedTeamId.HasValue;
    public int? CompletedTeamId { get; }

    public GetTeamGenerationStatusResponse(TeamGenerationStatus status)
    {
      CurrentAction = status.Progress?.CurrentAction ?? "";
      PercentCompletion = status.Progress?.PercentCompletion.ToPercent() ?? 0;
      EstimatedTimeToCompletion = status.Progress?.GetEstimatedTimeRemaining(DateTime.Now - status.StartedOn).ToDisplayString() ?? "";
      CompletedTeamId = status.TeamId;
    }
  }
}
