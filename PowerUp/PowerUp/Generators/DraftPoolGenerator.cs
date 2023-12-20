using PowerUp.Entities.Players;
using PowerUp.Fetchers.MLBLookupService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Generators
{
  public interface IDraftPoolGenerator
  {
    Task<IEnumerable<Player>> GenerateDraftPool(PlayerGenerationAlgorithm playerGenerationAlgorithm);
    Task<Player?> GenerateRandomPlayer(PlayerGenerationAlgorithm playerGenerationAlgorithm);
  }

  public class DraftPoolGenerator : IDraftPoolGenerator
  {
    public readonly int TeamsDrafting = 2;
    private readonly IDictionary<Position, int> MinPer25ByPosition = new Dictionary<Position, int>()
    {
      { Position.Pitcher, 12 },
      { Position.Catcher, 2 },
      { Position.FirstBase, 1 },
      { Position.SecondBase, 1 },
      { Position.ThirdBase, 1 },
      { Position.Shortstop, 1 },
      { Position.LeftField, 1 },
      { Position.RightField, 1 },
      { Position.CenterField, 1 },
    };

    private readonly IMLBLookupServiceClient _mlbLookupServiceClient;
    private readonly IPlayerGenerator _playerGenerator;

    public DraftPoolGenerator(
      IMLBLookupServiceClient mlbLookupServiceClient,
      IPlayerGenerator playerGenerator
    )
    {
      _mlbLookupServiceClient = mlbLookupServiceClient;
      _playerGenerator = playerGenerator;
    }

    public async Task<IEnumerable<Player>> GenerateDraftPool(PlayerGenerationAlgorithm playerGenerationAlgorithm)
    {
      var draftPool = new List<Player>();
      var teams = TeamsDrafting + 2;
      var draftPoolSize = teams * 25;
      while (draftPool.Count < draftPoolSize)
      {
        var player = await GenerateRandomPlayer(playerGenerationAlgorithm);
        if (player == null)
          continue;

        var unfulfilledMins = MinPer25ByPosition
          .Where(m => 
          {
            var playersOfPosition = draftPool.Count(p => p.PrimaryPosition == m.Key);
            var minForPoolSize = m.Value * teams;
            return playersOfPosition < minForPoolSize;
          });

        var allMinsFulfilled = !unfulfilledMins.Any();
        var playerFulfillsMin = unfulfilledMins.Any(m => m.Key == player.PrimaryPosition);
        if (player.Overall > 60 && (allMinsFulfilled || playerFulfillsMin))
        {
          draftPool.Add(player);
        }
      }

      return draftPool;
    }
    
    public async Task<Player?> GenerateRandomPlayer(PlayerGenerationAlgorithm playerGenerationAlgorithm)
    {
      var random = new Random();
      var randomYear = random.Range(1900, DateTime.UtcNow.Year);
      var teams = await _mlbLookupServiceClient.GetTeamsForYear(randomYear);
      if(!teams.Results.Any())
        return null;
      var randomTeam = random.GetRandomElement(teams.Results);
      var roster = await _mlbLookupServiceClient.GetTeamRosterForYear(randomTeam.LSTeamId, randomYear);
      if (!roster.Results.Any())
        return null;
      var randomPlayer = random.GetRandomElement(roster.Results);
      var generatedPlayer = _playerGenerator.GeneratePlayer(randomPlayer.LSPlayerId, randomYear, playerGenerationAlgorithm, randomPlayer.UniformNumber);
      return generatedPlayer.Player;
    }
  }
}
