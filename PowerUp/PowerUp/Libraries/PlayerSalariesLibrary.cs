using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Libraries
{
  public interface IPlayerSalariesLibrary
  {
    IEnumerable<PlayerSalaryDetails> PlayerSalaries { get; } 
  }

  public class PlayerSalaryDetails
  {
    public int PlayerId { get; }
    public int PowerProsPointsPerYear { get; }
    public int YearsUntilFreeAgency { get; }
    public int GuaranteedPowerProsPoints => YearsUntilFreeAgency * PowerProsPointsPerYear;
    public bool IsRookieDeal => PowerProsPointsPerYear == 380;

    public PlayerSalaryDetails(
      int playerId,
      int powerProsPointsPerYear,
      int yearsUntilFreeAgency
    )
    {
      PlayerId = playerId;
      PowerProsPointsPerYear = powerProsPointsPerYear;
      YearsUntilFreeAgency = yearsUntilFreeAgency;
    }
  }

  public class PlayerSalariesLibrary : IPlayerSalariesLibrary
  {
    public IEnumerable<PlayerSalaryDetails> PlayerSalaries { get; }

    public PlayerSalariesLibrary(string libraryFilePath)
    {
      PlayerSalaries = File.ReadAllLines(libraryFilePath)
        .Select(l => l.Split(','))
        .Select(l => new PlayerSalaryDetails(
          playerId: int.Parse(l[0]),
          powerProsPointsPerYear: int.Parse(l[1]),
          yearsUntilFreeAgency: int.Parse(l[1])
        ));
    }
  }
}
