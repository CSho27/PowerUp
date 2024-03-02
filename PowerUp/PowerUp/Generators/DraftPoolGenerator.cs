using PowerUp.Entities.Players;
using PowerUp.Fetchers.MLBLookupService;
using PowerUp.Fetchers.MLBStatsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Generators
{
  public interface IDraftPoolGenerator
  {
    Task<IEnumerable<Player>> GenerateDraftPool(PlayerGenerationAlgorithm playerGenerationAlgorithm, int size);
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
    private readonly IMLBStatsApiClient _mlbStatsApiClient;
    private readonly IPlayerGenerator _playerGenerator;

    public DraftPoolGenerator(
      IMLBLookupServiceClient mlbLookupServiceClient,
      IMLBStatsApiClient mlbStatsApiClient,
      IPlayerGenerator playerGenerator
    )
    {
      _mlbLookupServiceClient = mlbLookupServiceClient;
      _mlbStatsApiClient = mlbStatsApiClient;
      _playerGenerator = playerGenerator;
    }

    public async Task<IEnumerable<Player>> GenerateDraftPool(PlayerGenerationAlgorithm playerGenerationAlgorithm, int size)
    {
      var draftPool = new List<Player>();
      while (draftPool.Count < size)
      {
        var player = await GenerateRandomPlayer(playerGenerationAlgorithm);
        if (player == null)
          continue;

        var unfulfilledMins = MinPer25ByPosition
          .Where(m => 
          {
            var playersOfPosition = draftPool.Count(p => p.PrimaryPosition == m.Key);
            var minForPoolSize = m.Value * (size / 4);
            return playersOfPosition < minForPoolSize;
          });

        var allMinsFulfilled = !unfulfilledMins.Any();
        var playerFulfillsMin = unfulfilledMins.Any(m => m.Key == player.PrimaryPosition);
        var isValidOverall = AverageTargetingUtils.IsValueValid(
          targetMin: 75,
          targetMax: 85,
          minProbableValue: 70,
          maxProbablevalue: 80,
          valuesKnown: draftPool.Count,
          totalValues: size,
          currentSum: draftPool.Sum(p => p.Overall),
          value: player.Overall
        );
        if (isValidOverall && (allMinsFulfilled || playerFulfillsMin))
        {
          Console.WriteLine($"Adding: {player.InformalDisplayName}");
          draftPool.Add(player);
        }
        else
        {
          Console.WriteLine($"Rejecting: {player.InformalDisplayName}");
        }
      }

      Console.WriteLine($"Draft Pool Average: {draftPool.Average(p => p.Overall)}");
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
      var roster = await _mlbStatsApiClient.GetTeamRoster(randomTeam.LSTeamId, randomYear);
      if (!roster.Roster.Any())
        return null;
      var randomPlayer = random.GetRandomElement(roster.Roster);
      if (randomPlayer.Person is null) 
        return null;
      var generatedPlayer = _playerGenerator.GeneratePlayer(randomPlayer.Person.Id, randomYear, playerGenerationAlgorithm, randomPlayer.JerseyNumber);
      return generatedPlayer.Player;
    }
  }
}
