using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.Fetchers.MLBLookupService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Generators
{
  public interface ITeamGenerator
  {
    TeamGenerationResult GenerateTeam(long lsTeamId, int year, string name, PlayerGenerationAlgorithm playerGenerationAlgorithm);
  }

  public class TeamGenerationResult
  {
    public Team Team { get; set; }
    public IEnumerable<GeneratorWarning> Warnings { get; set; } = Enumerable.Empty<GeneratorWarning>();

    public TeamGenerationResult(Team team, IEnumerable<GeneratorWarning> warnings)
    {
      Team = team;
      Warnings = warnings;
    }
  }

  public class TeamGenerator : ITeamGenerator
  {
    private readonly IMLBLookupServiceClient _mlbLookupServiceClient;
    private readonly IPlayerGenerator _playerGenerator;

    public TeamGenerator(
      IMLBLookupServiceClient mlbLookupServiceClient,
      IPlayerGenerator playerGenerator
    )
    {
      _mlbLookupServiceClient = mlbLookupServiceClient;
      _playerGenerator = playerGenerator;
    }

    public TeamGenerationResult GenerateTeam(long lsTeamId, int year, string name, PlayerGenerationAlgorithm algorithm)
    {
      var playerResults = Task.Run(() => _mlbLookupServiceClient.GetTeamRosterForYear(lsTeamId, year)).GetAwaiter().GetResult();
      var generatedPlayers = playerResults.Results
        .Select(p => _playerGenerator.GeneratePlayer(p.LSPlayerId, year, algorithm))
        .Where(p => p.LastTeamForYear_LSTeamId == lsTeamId)
        .ToList();

      // Decide Best Lineups
      // Decide Pitcher Roles
      // Decide who makes the 25/40 man roster

      // Save generated players

      var hitters = generatedPlayers
        .Select(p => p.Player)
        .Where(p => p.PrimaryPosition != Position.Pitcher)
        .OrderByDescending(h => h.Overall);

      var pitchers = generatedPlayers
        .Select(p => p.Player)
        .Where(p => p.PrimaryPosition == Position.Pitcher)
        .OrderByDescending(h => h.Overall);

      var team = new Team
      {
        Name = name,
        SourceType = EntitySourceType.Generated,
        // Definitions
        // Lineups

      };

      return new TeamGenerationResult(team, Enumerable.Empty<GeneratorWarning>());

    }
  }
}
