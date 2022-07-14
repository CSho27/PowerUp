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
  }
}
