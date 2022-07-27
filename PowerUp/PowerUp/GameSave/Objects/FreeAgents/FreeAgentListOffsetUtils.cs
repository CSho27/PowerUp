using PowerUp.GameSave.Objects.GameSaves;
using System;

namespace PowerUp.GameSave.Objects.FreeAgents
{
  public static class FreeAgentListOffsetUtils
  {
    public const long WII_FREEAGENT_START_OFFSET = 0xa9cb4;
    public const long PS2_FREEAGENT_START_OFFSET = 0xa938b;

    public static long GetFreeAgentListOffset(GameSaveFormat format) => format switch
    {
      GameSaveFormat.Wii => WII_FREEAGENT_START_OFFSET,
      GameSaveFormat.Ps2 => PS2_FREEAGENT_START_OFFSET,
      _ => throw new InvalidOperationException("Unsupported GameSaveFormat")
    };
  }
}
