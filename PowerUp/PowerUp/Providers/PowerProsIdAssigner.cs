using PowerUp.Libraries;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Providers
{
  public interface IPowerProsIdAssigner
  {
    IDictionary<int, int> AssignIds(IEnumerable<PowerProsIdParameters> parameters, IEnumerable<PlayerSalaryDetails> playerContracts);
  }

  public class PowerProsIdParameters
  {
    public int PlayerId { get; set; }
    public int YearsInMajors { get; set; }
    public double Overall { get; set; }
  }


  public class PowerProsIdAssigner : IPowerProsIdAssigner
  {
    private const int PRE_ARB_YEARS = 6;

    public IDictionary<int, int> AssignIds(IEnumerable<PowerProsIdParameters> parameters, IEnumerable<PlayerSalaryDetails> playerContracts)
    {
      var powerProsIdsByPlayerId = new Dictionary<int, int>();

      IList<PlayerSalaryDetails> remainingContracts = playerContracts.OrderByDescending(s => s.PowerProsPointsPerYear).ToList();
      var nextPlayerId = remainingContracts.Select(c => c.PlayerId).Max() + 1;
      var allPlayersRankedByOverall = parameters.DistinctBy(p => p.PlayerId).OrderByDescending(p => p.Overall).ToList();
      var postArbPlayersRankedByOverall = allPlayersRankedByOverall.Where(p => p.YearsInMajors >= PRE_ARB_YEARS).ToArray();
      
      foreach(var player in postArbPlayersRankedByOverall)
      {
        var assignedContract = remainingContracts.RemoveFirstOrDefault();
        if (assignedContract == null)
        {
          powerProsIdsByPlayerId.Add(player.PlayerId, nextPlayerId);
          nextPlayerId++;
          continue;
        }
        
        powerProsIdsByPlayerId.Add(player.PlayerId, assignedContract.PlayerId);
      }

      var groupedRookieContracts = remainingContracts.Where(s => s.IsRookieDeal).GroupBy(s => s.YearsUntilFreeAgency).ToDictionary(s => s.Key, s => s.ToList());
      var rookieContractPlayers = allPlayersRankedByOverall.Where(p => p.YearsInMajors < PRE_ARB_YEARS).ToList();
      var leftoverRookies = new List<PowerProsIdParameters>();
      foreach (var player in rookieContractPlayers)
      {
        var yearsUntilFreeAgency = PRE_ARB_YEARS - player.YearsInMajors;
        groupedRookieContracts.TryGetValue(yearsUntilFreeAgency, out var contractsWithRightLength);
        var assignedContract = (contractsWithRightLength ?? new List<PlayerSalaryDetails>()).RemoveFirstOrDefault();
        if (assignedContract == null)
        {
          leftoverRookies.Add(player);
          continue;
        }

        powerProsIdsByPlayerId.Add(player.PlayerId, assignedContract!.PlayerId);
        remainingContracts = remainingContracts.Where(c => c.PlayerId != assignedContract.PlayerId).ToList();
      }

      foreach(var player in leftoverRookies)
      {
        var assignedContract = remainingContracts.RemoveFirstOrDefault();
        if (assignedContract == null)
        {
          powerProsIdsByPlayerId.Add(player.PlayerId, nextPlayerId);
          nextPlayerId++;
          continue;
        }

        powerProsIdsByPlayerId.Add(player.PlayerId, assignedContract.PlayerId);
      }

      return powerProsIdsByPlayerId;
    }
  }
}