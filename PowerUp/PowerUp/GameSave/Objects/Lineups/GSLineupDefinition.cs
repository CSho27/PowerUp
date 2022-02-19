using PowerUp.GameSave.IO;
using System.Collections.Generic;

namespace PowerUp.GameSave.Objects.Lineups
{
  public class GSLineupDefinition
  {
    [GSArray(offset: 0x0, itemLength: 0x4, arrayLength: 9)]
    public IEnumerable<GSLineupPlayer>? NoDHLineup { get; set; }
    [GSArray(offset: 0x24, itemLength: 0x4, arrayLength: 9)]
    public IEnumerable<GSLineupPlayer>? DHLineup { get; set; }
  }

  public class GSLineupPlayer
  {
    [GSUInt(0x0, bits: 16, bitOffset: 0)]
    public ushort? PowerProsPlayerId { get; set; }

    [GSUInt(0x2, bits: 4, bitOffset: 0)]
    public ushort? Position { get; set; }
  }
}
