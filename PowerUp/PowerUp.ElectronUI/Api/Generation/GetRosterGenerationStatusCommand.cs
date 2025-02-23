using PowerUp.Databases;
using PowerUp.Entities.GenerationResults;

namespace PowerUp.ElectronUI.Api.Generation
{
  public class GetRosterGenerationStatusCommand : ICommand<GetRosterGenerationStatusRequest, GetRosterGenerationStatusResponse>
  {
    public GetRosterGenerationStatusResponse Execute(GetRosterGenerationStatusRequest request)
    {
      var status = DatabaseConfig.Database.Load<RosterGenerationStatus>(request.GenerationStatusId)!;
      return new GetRosterGenerationStatusResponse(status);
    }
  }

  public class GetRosterGenerationStatusRequest
  {
    public int GenerationStatusId { get; set; }
  }

  public class GetRosterGenerationStatusResponse
  {
    public string CurrentTeamAction { get; }
    public string CurrentPlayerAction { get; }
    public int PercentCompletion { get; }
    public string EstimatedTimeToCompletion { get; }
    public bool IsComplete => CompletedRosterId.HasValue;
    public bool IsFailed { get; }
    public int? CompletedRosterId { get; }

    public GetRosterGenerationStatusResponse(RosterGenerationStatus status)
    {
      CurrentTeamAction = status.CurrentTeamAction ?? "";
      CurrentPlayerAction = status.CurrentPlayerAction ?? "";
      PercentCompletion = status.Progress?.PercentCompletion.ToPercent() ?? 0;
      EstimatedTimeToCompletion = status.Progress?.GetEstimatedTimeRemaining(DateTime.Now - status.StartedOn).ToDisplayString() ?? "";
      CompletedRosterId = status.RosterId;
      IsFailed = status.IsFailed;
    }
  }
}
