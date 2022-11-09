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

      IEnumerable<PlayerSalaryDetails> remainingContracts = playerContracts.OrderByDescending(s => s.GuaranteedPowerProsPoints);
      var nextPlayerId = remainingContracts.Select(c => c.PlayerId).Max() + 1;
      var postArbPlayersRankedByOverall = parameters.Where(p => p.YearsInMajors >= PRE_ARB_YEARS).OrderByDescending(p => p.Overall).ToArray();
      
      foreach(var player in postArbPlayersRankedByOverall)
      {
        if (!remainingContracts.Any())
        {
          powerProsIdsByPlayerId.Add(player.PlayerId, nextPlayerId);
          nextPlayerId++;
          continue;
        }

        var assignedContract = remainingContracts.First();
        remainingContracts = remainingContracts.Skip(1);
        powerProsIdsByPlayerId.Add(player.PlayerId, assignedContract.PlayerId);
      }

      var rookieContracts = remainingContracts.Where(s => s.IsRookieDeal);
      var rookieContractPlayers = parameters.Where(p => p.YearsInMajors < PRE_ARB_YEARS);
      var leftoverRookies = new List<PowerProsIdParameters>();
      foreach (var player in rookieContractPlayers)
      {
        var assignedContract = rookieContracts.FirstOrDefault(c => c.YearsUntilFreeAgency == PRE_ARB_YEARS - player.YearsInMajors);
        rookieContracts = rookieContracts.Where(c => c.PlayerId != assignedContract?.PlayerId);
        remainingContracts = remainingContracts.Where(c => c.PlayerId != assignedContract?.PlayerId);
        if(assignedContract != null)
          powerProsIdsByPlayerId.Add(player.PlayerId, assignedContract!.PlayerId);
        else
          leftoverRookies.Add(player);
      }

      foreach(var player in leftoverRookies.OrderByDescending(r => r.Overall))
      {
        if (!remainingContracts.Any())
        {
          powerProsIdsByPlayerId.Add(player.PlayerId, nextPlayerId);
          nextPlayerId++;
          continue;
        }

        var assignedContract = remainingContracts.First();
        remainingContracts = remainingContracts.Skip(1);
        powerProsIdsByPlayerId.Add(player.PlayerId, assignedContract.PlayerId);
      }

      return powerProsIdsByPlayerId;
    }
  }
}