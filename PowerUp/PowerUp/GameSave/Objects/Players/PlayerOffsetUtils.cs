using System;

namespace PowerUp.GameSave.Objects.Players
{
  public static class PlayerOffsetUtils
  {
    private const long WII_PLAYER_START_OFFSET = 0x68c74;
    private const long PS2_PLAYER_START_OFFSET = 0x6834b;
    private const long PLAYER_SIZE = 0xb0;

    public static long GetPlayerOffset(int powerProsId, GameSaveFormat format) => GetStartOffset(format) + PLAYER_SIZE * (powerProsId - 1);

    private static long GetStartOffset(GameSaveFormat format) => format switch
    {
      GameSaveFormat.Wii_2007 => WII_PLAYER_START_OFFSET,
      GameSaveFormat.Ps2_2007 => PS2_PLAYER_START_OFFSET,
      _ => throw new InvalidOperationException("Unsupported GameSaveFormat")
    };
  }

  public enum GameSaveFormat { Wii_2007, Ps2_2007 }
}
