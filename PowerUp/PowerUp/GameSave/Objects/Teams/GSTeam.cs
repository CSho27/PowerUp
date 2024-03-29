﻿using PowerUp.GameSave.IO;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.GameSave.Objects.Teams
{
  public class GSTeam : IGSTeam
  {
    [GSArray(offset: 0, itemLength: 0x8, arrayLength: 40)]
    public IEnumerable<GSTeamPlayerEntry>? PlayerEntries { get; set; }
  }

  public class GSTeamPlayerEntry : IGSTeamPlayerEntry
  {
    public static GSTeamPlayerEntry Empty = new GSTeamPlayerEntry
    {
      PowerProsPlayerId = 0,
      PowerProsTeamId = 0,
      IsAAA = false,
      IsMLB = false,
      IsPinchHitter = false,
      IsPinchRunner = false,
      IsDefensiveReplacement = false,
      IsDefensiveLiability = false,
      PitcherRole = 0,
      OtherPlayerByte = new[] { new byte() }
    };

    [GSUInt(offset: 0, bits: 16)]
    public ushort? PowerProsPlayerId { get; set; }

    [GSBoolean(offset: 2, bitOffset: 0)]
    public bool? IsAAA { get; set; }

    [GSBoolean(offset: 2, bitOffset: 1)]
    public bool? IsMLB { get; set; }

    [GSUInt(offset: 2, bits: 5, bitOffset: 5)]
    public ushort? PowerProsTeamId { get; set; }

    [GSBoolean(offset: 5, bitOffset: 4)]
    public bool? IsPinchHitter { get; set; }

    [GSBoolean(offset: 5, bitOffset: 5)]
    public bool? IsPinchRunner { get; set; }

    [GSBoolean(offset: 5, bitOffset: 6)]
    public bool? IsDefensiveReplacement { get; set; }

    [GSBoolean(offset: 5, bitOffset: 7)]
    public bool? IsDefensiveLiability { get; set; }

    [GSUInt(offset: 6, bits: 3, bitOffset: 3)]
    public ushort? PitcherRole { get; set; }


    [GSBytes(offset: 7, numberOfBytes: 1)]
    public byte[]? OtherPlayerByte { get; set; }
  }
}
