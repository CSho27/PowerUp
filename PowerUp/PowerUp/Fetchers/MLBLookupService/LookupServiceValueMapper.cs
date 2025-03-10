﻿using Microsoft.Extensions.Logging;
using PowerUp.Entities.Players;
using System;
using System.Text.RegularExpressions;

namespace PowerUp.Fetchers.MLBLookupService
{
  public static class LookupServiceValueMapper
  {
    public static Position MapPosition(string? positionId)
    {
      if (positionId is null) return Position.DesignatedHitter;

      switch(positionId)
      {
        case "D":
        case "H":
        case "O":
        case "I":
        case "X":
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
          {
            Logging.Logger.LogWarning($"{positionId} is not a valid integer");
            return Position.DesignatedHitter;
          }

          return (Position)pos;
      }
    }

    public static BattingSide MapBatingSide(string? bats)
    {
      switch (bats)
      {
        case "R":
        case "":
        case null:
          return BattingSide.Right;
        case "L":
          return BattingSide.Left;
        case "S":
          return BattingSide.Switch;
        default:
          throw new ArgumentException(bats);
      }
    }

    public static ThrowingArm MapThrowingArm(string? throws)
    {
      switch (throws)
      {
        case "":
        case "R":
        case null:
          return ThrowingArm.Right;
        // The game doesn't support switch pitchers, so we'll choose to make them lefties
        case "S":
        case "L":
          return ThrowingArm.Left;
        default:
          throw new ArgumentException(throws);
      }
    }

    public static (int? heightFeet, int? heightInches)? ParseHeight(string? heightString)
    {
      if (heightString is null) return null;

      var heightRegex = new Regex("(?<heightFeet>\\d+)'\\s*(?<heightInches>\\d+)");
      var result = heightRegex.Match(heightString);
      if (!result.Success)
        return null;

      var heightFeet = result.Groups["heightFeet"]?.Value.TryParseInt();
      var heightInches = result.Groups["heightInches"]?.Value.TryParseInt();
      return (heightFeet, heightInches);
    }

    public static PlayerRosterStatus MapRosterStatus(string status)
    {
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
          var success = Enum.TryParse<PlayerRosterStatus>(status, out var value);
          if(!success)
            Logging.Logger.LogError($"Unhandled PlayerRosterStatus {status}");
          return success
            ? value
            : PlayerRosterStatus.Unhandled;
      }
    }
  }
}
