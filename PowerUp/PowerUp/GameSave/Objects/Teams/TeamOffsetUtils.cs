using PowerUp.GameSave.Objects.Players;
using System;

namespace PowerUp.GameSave.Objects.Teams
{
  public class TeamOffsetUtils
  {
    private const long WII_TEAM_START_OFFSET = 0xaa2f4;
    private const long PS2_TEAM_START_OFFSET = 0xa99cb;
    private const long TEAM_LENGTH = 0x140;

    public static long GetTeamOffset(int powerProsTeamId, GameSaveFormat format) => GetStartOffset(format) + TEAM_LENGTH * GetOrderOnFile(powerProsTeamId);

    private static long GetStartOffset(GameSaveFormat format) => format switch
    {
      GameSaveFormat.Wii_2007 => WII_TEAM_START_OFFSET,
      GameSaveFormat.Ps2_2007 => PS2_TEAM_START_OFFSET,
      _ => throw new InvalidOperationException("Unsupported GameSaveFormat")
    };

    public static int GetOrderOnFile(int powerProsTeamId) => powerProsTeamId != 0
        ? powerProsTeamId - 1
        : 31;
  }
}
