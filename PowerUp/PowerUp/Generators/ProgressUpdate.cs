using System;

namespace PowerUp.Generators
{
  public class ProgressUpdate
  {
    public string CurrentAction { get; set; }
    public int CurrentActionIndex { get; set; }
    public int TotalActions { get; set; }
    public double PercentCompletion => CurrentActionIndex / (double)TotalActions;

    public ProgressUpdate(string currentAction, int currentIndex, int totalActions)
    {
      CurrentAction = currentAction;
      CurrentActionIndex = currentIndex;
      TotalActions = totalActions;
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
