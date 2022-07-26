using PowerUp.GameSave.IO;
using System.Collections.Generic;

namespace PowerUp.GameSave.Objects.FreeAgents
{
  public class GSFreeAgentList
  {
    [GSArray(offset: 0x0, itemLength: 0x8, arrayLength: 15)]
    public IEnumerable<GSFreeAgent>? FreeAgents { get; set; }
  }

  public class GSFreeAgent
  {
    [GSUInt(0x0, bits: 16, bitOffset: 0)]
    public ushort? PowerProsPlayerId { get; set; }
  }
}
