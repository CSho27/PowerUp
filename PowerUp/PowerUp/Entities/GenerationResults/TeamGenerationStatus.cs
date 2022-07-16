using PowerUp.Databases;
using PowerUp.Generators;

namespace PowerUp.Entities.GenerationResults
{
  public class TeamGenerationStatus : Entity<TeamGenerationStatus>
  {
    public long LSTeamId { get; set; }
    public int Year { get; set; }
    public int? TeamId { get; set; }
    public string? CurrentAction { get; set; }
    public int CurrentActionIndex { get; set; }
    public int TotalActions { get; set; }
    public bool IsComplete => TotalActions > 0 && CurrentActionIndex == TotalActions;
    public ProgressUpdate? Progress => CurrentAction != null
      ? new ProgressUpdate(CurrentAction, CurrentActionIndex, TotalActions)
      : null;

    public TeamGenerationStatus(long lsTeamId, int year)
    {
      LSTeamId = lsTeamId;
      Year = year;
    }

    public void Update(string currentAction, int currentActionIndex, int totalActions)
    {
      CurrentAction = currentAction;
      CurrentActionIndex = currentActionIndex;
      TotalActions = totalActions;
    }

    public void Complete(int teamId)
    {
      CurrentAction = null;
      CurrentActionIndex = TotalActions;
      TeamId = teamId;
    }
  }
}
