using PowerUp.Entities.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
          return Position.DesignatedHitter;
        case "R":
        case "S":
        case "P":
          return Position.Pitcher;
        default:
          return (Position)int.Parse(positionId);
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
        case "R":
        case "":
          return ThrowingArm.Right;
        case "L":
          return ThrowingArm.Left;
        default:
          throw new ArgumentException(throws);
      }
    }
  }
}
