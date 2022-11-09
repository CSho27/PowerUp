using PowerUp.GameSave.IO;

namespace PowerUp.GameSave.Objects.SeasonModeSalaries
{
  public class GSSalaryEntry
  {

    [GSUInt(0x0, bits: 16, bitOffset: 0)]
    public ushort? PowerProsPlayerId { get; set; }

    [GSUInt(0x2, bits: 16, bitOffset: 0)]
    public ushort? PowerProsPointsPerYear { get; set; }

    [GSUInt(0xF, bits: 3, bitOffset: 4)]
    public ushort? YearsUntilFreeAgency { get; set; }
  }
}
