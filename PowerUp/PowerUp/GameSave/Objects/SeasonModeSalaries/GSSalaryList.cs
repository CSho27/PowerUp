using PowerUp.GameSave.IO;
using System.Collections.Generic;

namespace PowerUp.GameSave.Objects.SeasonModeSalaries
{
  public class GSSalaryList
  {
    [GSArray(offset: 0, itemLength: 0x20, arrayLength: 970)]
    public IEnumerable<GSSalaryEntry>? SalaryEntries { get; set; }
  }
}
