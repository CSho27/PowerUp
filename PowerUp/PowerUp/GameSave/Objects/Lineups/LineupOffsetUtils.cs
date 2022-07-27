using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.GameSave.Objects.Teams;
using System;

namespace PowerUp.GameSave.Objects.Lineups
{
  public class LineupOffsetUtils
  {
    private const long WII_LINEUP_START_OFFSET = 0xa93b4;
    private const long PS2_LINEUP_START_OFFSET = 0xa8a8b;
    private const long LINEUP_LENGTH = 0x48;

    public static long GetLineupOffset(GameSaveFormat format, int powerProsTeamId) => GetStartOffset(format) + LINEUP_LENGTH * TeamOffsetUtils.GetOrderOnFile(powerProsTeamId);

    private static long GetStartOffset(GameSaveFormat format) => format switch
    {
      GameSaveFormat.Wii => WII_LINEUP_START_OFFSET,
      GameSaveFormat.Ps2 => PS2_LINEUP_START_OFFSET,
      _ => throw new InvalidOperationException("Unsupported GameSaveFormat")
    };
  }
}
