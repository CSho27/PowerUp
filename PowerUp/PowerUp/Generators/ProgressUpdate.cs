using System;

namespace PowerUp.Generators
{
  public class ProgressUpdate
  {
    public string CurrentAction { get; set; }
    public int CurrentActionIndex { get; set; }
    public int TotalActions { get; set; }
    public ProgressUpdate? CurrentActionProgress { get; set; }
    private double CurrentActionPercentCompletion => CurrentActionProgress?.PercentCompletion ?? 0;
    public double PercentCompletion => TotalActions > 0
      ? (CurrentActionIndex / (double)TotalActions) + CurrentActionPercentCompletion * (1.0/TotalActions)
      : 0;

    public ProgressUpdate(string currentAction, int currentIndex, int totalActions, ProgressUpdate? currentActionProgress = null)
    {
      CurrentAction = currentAction;
      CurrentActionIndex = currentIndex;
      TotalActions = totalActions;
      CurrentActionProgress = currentActionProgress;
    }

    public TimeSpan? GetEstimatedTimeRemaining(TimeSpan timeElapsed)
    {
      var estTotalTime = PercentCompletion > 0
        ? timeElapsed * (1 / PercentCompletion)
        : (TimeSpan?)null;

      return estTotalTime.HasValue
        ? estTotalTime - timeElapsed
        : null;
    }
  }
}
