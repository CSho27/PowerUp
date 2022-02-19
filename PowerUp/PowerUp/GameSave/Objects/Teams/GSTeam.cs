using PowerUp.GameSave.IO;
using System.Collections.Generic;

namespace PowerUp.GameSave.Objects.Teams
{
  public class GSTeam
  {
    [GSArray(offset: 0, itemLength: 0x8, arrayLength: 40)]
    public IEnumerable<GSTeamPlayerEntry>? PlayerEntries { get; set; }
  }

  public class GSTeamPlayerEntry
  {
    [GSUInt(offset: 0, bits: 16)]
    public ushort? PowerProsPlayerId { get; set; }

    [GSBytes(offset: 1, numberOfBytes: 7)]
    public byte[]? OtherPlayerBytes { get; set; }
  }
}
