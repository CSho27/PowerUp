using PowerUp.GameSave.Objects.GameSaves;
using System;

namespace PowerUp.GameSave.Objects.Players
{
  public static class PlayerOffsetUtils
  {
    private const long WII_PLAYER_START_OFFSET = 0x68c74;
    private const long PS2_PLAYER_START_OFFSET = 0x6834b;
    private const long PLAYER_SIZE = 0xb0;

    public static long GetPlayerOffset(GameSaveFormat format, int powerProsId) => GetStartOffset(format) + PLAYER_SIZE * (powerProsId - 1);

    private static long GetStartOffset(GameSaveFormat format) => format switch
    {
      GameSaveFormat.Wii => WII_PLAYER_START_OFFSET,
      GameSaveFormat.Ps2 => PS2_PLAYER_START_OFFSET,
      _ => throw new InvalidOperationException("Unsupported GameSaveFormat")
    };
  }
}
