using PowerUp.Entities.Players;
using System;

namespace PowerUp.Fetchers.MLBLookupService
{
  public static class LookupServiceValueMapper
  {
    public static Position MapPosition(string positionId)
    {
      switch(positionId)
      {
        case "D":
        case "H":
        case "O":
        case "I":
          return Position.DesignatedHitter;
        // Y is the Ohtani value. Since Pitchers can still be put into lineups in the game, we'll make him a pitcher
        case "Y":
        case "R":
        case "S":
        case "P":
          return Position.Pitcher;
        default:
          var success = int.TryParse(positionId, out var pos);
          if (!success)
            throw new ArgumentException($"{positionId} is not a valid integer");
          return (Position)pos;
      }
    }

    public static BattingSide MapBatingSide(string bats)
    {
      switch (bats)
      {
        case "R":
        case "":
          return BattingSide.Right;
        case "L":
          return BattingSide.Left;
        case "S":
          return BattingSide.Switch;
        default:
          throw new ArgumentException(bats);
      }
    }

    public static ThrowingArm MapThrowingArm(string throws)
    {
      switch (throws)
      {
        case "":
        case "R":
          return ThrowingArm.Right;
        // The game doesn't support switch pitchers, so we'll choose to make them lefties
        case "S":
        case "L":
          return ThrowingArm.Left;
        default:
          throw new ArgumentException(throws);
      }
    }
    
    public static PlayerRosterStatus MapRosterStatus(string status)
    {
      if (string.IsNullOrEmpty(status))
        throw new ArgumentNullException();

      if (status.Contains("IL"))
        return PlayerRosterStatus.IL;

      switch (status)
      {
        case "Des. for Assignment":
          return PlayerRosterStatus.DFA;
        case "Free agent":
          return PlayerRosterStatus.FreeAgent;
        case "Temporary Inactive":
          return PlayerRosterStatus.TemporaryInactive;
        default:
          return Enum.Parse<PlayerRosterStatus>(status);
      }
    }
  }
}
