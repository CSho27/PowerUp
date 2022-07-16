using PowerUp.Databases;
using PowerUp.Generators;

namespace PowerUp.Entities.GenerationResults
{
  public class TeamGenerationResult : Entity<TeamGenerationResult>
  {
    public int? TeamId { get; set; }
    public string? CurrentAction { get; set; }
    public int CurrentActionIndex { get; set; }
    public int TotalActions { get; set; }
    public ProgressUpdate? Progress => CurrentAction != null
      ? new ProgressUpdate(CurrentAction, CurrentActionIndex, TotalActions)
      : null;
  }
}
