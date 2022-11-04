using PowerUp.Databases;
using PowerUp.Generators;
using PowerUp.Migrations;
using System;

namespace PowerUp.Entities.GenerationResults
{
  [MigrationIgnore]
  public class RosterGenerationStatus : Entity<RosterGenerationStatus>
  {
    public int Year { get; set; }
    public int? RosterId { get; set; }
    public DateTime StartedOn { get; set; }
    public string? CurrentTeamAction { get; set; }
    public int CurrentTeamActionIndex { get; set; }
    public string? CurrentPlayerAction { get; set; }
    public int CurrentPlayerActionIndex { get; set; }
    public int TotalTeamActions { get; set; }
    public int TotalPlayerActions { get; set; }
    public bool IsComplete => TotalTeamActions > 0 && CurrentTeamActionIndex == TotalTeamActions;
    public ProgressUpdate? Progress => CurrentTeamAction != null
      ? new ProgressUpdate(
         currentAction: CurrentTeamAction, 
         currentIndex: CurrentTeamActionIndex,
         totalActions: TotalTeamActions, 
         currentActionProgress: CurrentPlayerAction != null
          ? new ProgressUpdate(CurrentPlayerAction, CurrentPlayerActionIndex, TotalPlayerActions)
          : null
        )
      : null;

    public RosterGenerationStatus(int year, DateTime startedOn)
    {
      StartedOn = startedOn;
      Year = year;
    }

    public void UpdateTeamAction(string currentAction, int currentActionIndex, int totalActions)
    {
      CurrentTeamAction = currentAction;
      CurrentTeamActionIndex = currentActionIndex;
      TotalTeamActions = totalActions;
    }

    public void UpdatePlayerAction(string currentAction, int currentActionIndex, int totalActions)
    {
      CurrentPlayerAction = currentAction;
      CurrentPlayerActionIndex = currentActionIndex;
      TotalPlayerActions = totalActions;
    }

    public void Complete(int rosterId)
    {
      CurrentTeamAction = null;
      CurrentTeamActionIndex = TotalTeamActions;
      CurrentPlayerAction = null;
      CurrentPlayerActionIndex = TotalPlayerActions;
      RosterId = rosterId;
    }
  }
}
