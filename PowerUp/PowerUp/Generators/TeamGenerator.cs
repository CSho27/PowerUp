using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.Fetchers.MLBLookupService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Generators
{
  public interface ITeamGenerator
  {
    TeamGenerationResult GenerateTeam(long lsTeamId, int year, string name, PlayerGenerationAlgorithm playerGenerationAlgorithm, Action<ProgressUpdate>? onProgressUpdate = null);
  }

  public class TeamGenerationResult
  {
    public long LSTeamId { get; set; }
    public Team Team { get; set; }
    public IEnumerable<Player> PlayersOnTeam { get; set; }
    public IEnumerable<Player> PotentialFreeAgents { get; set; }
    public IEnumerable<GeneratorWarning> Warnings { get; set; } = Enumerable.Empty<GeneratorWarning>();

    public TeamGenerationResult(long lsTeamId, Team team, IEnumerable<Player> players, IEnumerable<Player> potentialFreeAgents, IEnumerable<GeneratorWarning> warnings)
    {
      LSTeamId = lsTeamId;
      Team = team;
      PlayersOnTeam = players;
      PotentialFreeAgents = potentialFreeAgents;
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

    public TeamGenerationResult GenerateTeam(long lsTeamId, int year, string name, PlayerGenerationAlgorithm algorithm, Action<ProgressUpdate>? onProgressUpdate = null)
    {
      var playerResults = Task.Run(() => _mlbLookupServiceClient.GetTeamRosterForYear(lsTeamId, year)).WaitAsync(TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();
      var teamPlayers = playerResults.Results.ToList();

      var generatedPlayers = new List<PlayerGenerationResult>();
      for(var i=0; i<teamPlayers.Count; i++)
      {
        var player = teamPlayers[i];
        if (onProgressUpdate != null)
          onProgressUpdate(new ProgressUpdate($"Generating {player.FormalDisplayName.GetInformalDisplayName()}", i, teamPlayers.Count + 1));

        var generatedPlayer = _playerGenerator.GeneratePlayer(player.LSPlayerId, year, algorithm);
        if(!generatedPlayer.LastTeamForYear_LSTeamId.HasValue || generatedPlayer.LastTeamForYear_LSTeamId == lsTeamId)
          generatedPlayers.Add(generatedPlayer);
      }

      if (onProgressUpdate != null)
        onProgressUpdate(new ProgressUpdate($"Setting rosters, lineups, and rotations", teamPlayers.Count, teamPlayers.Count + 1));

      var rosterParams = generatedPlayers.Select(p => new RosterParams(
        playerId: p.LSPlayerId,
        hitterRating: p.Player.HitterAbilities.GetHitterRating(),
        pitcherRating: p.Player.PitcherAbilities.GetPitcherRating(),
        contact: p.Player.HitterAbilities.Contact,
        power: p.Player.HitterAbilities.Power,
        runSpeed: p.Player.HitterAbilities.RunSpeed,
        primaryPosition: p.Player.PrimaryPosition,
        pitcherType: p.Player.PitcherType,
        positionCapabilityDictionary: p.Player.PositionCapabilities.GetDictionary()
      ));

      var rosterResults = RosterCreator.CreateRosters(rosterParams);

      // Save players on team
      var playersOnTeam = generatedPlayers
        .Where(p => rosterResults.FortyManRoster.Any(id => id == p.LSPlayerId))
        .Select(p => p.Player)
        .OrderByDescending(p => p.Overall);
      DatabaseConfig.Database.SaveAll(playersOnTeam);

      // Save Potential Free Agents
      var possibleFreeAgents = generatedPlayers
        .OrderByDescending(p => p.Player.Overall)
        .Where(p => !rosterResults.FortyManRoster.Any(id => id == p.LSPlayerId))
        .Take(5)
        .Select(p => p.Player);
      DatabaseConfig.Database.SaveAll(possibleFreeAgents);

      var team = new Team
      {
        Name = name,
        SourceType = EntitySourceType.Generated,
        GeneratedTeam_LSTeamId = lsTeamId,
        Year = year,
        PlayerDefinitions = playersOnTeam.Select(p => new PlayerRoleDefinition(p.Id!.Value)
        {
          IsAAA = !rosterResults.TwentyFiveManRoster.Contains(p.GeneratedPlayer_LSPLayerId!.Value),
          PitcherRole = rosterResults.Starters.Any(id => id == p.GeneratedPlayer_LSPLayerId)
            ? PitcherRole.Starter
            : rosterResults.Closer == p.GeneratedPlayer_LSPLayerId
              ? PitcherRole.Closer
              : PitcherRole.MiddleReliever
        }),
        NoDHLineup = rosterResults.NoDHLineup.Select(s => new LineupSlot { PlayerId = playersOnTeam.SingleOrDefault(p => p.GeneratedPlayer_LSPLayerId == s.PlayerId)?.Id, Position = s.Position }),
        DHLineup = rosterResults.DHLineup.Select(s => new LineupSlot { PlayerId = playersOnTeam.Single(p => p.GeneratedPlayer_LSPLayerId == s.PlayerId).Id, Position = s.Position })
      };

      return new TeamGenerationResult(lsTeamId, team, playersOnTeam, possibleFreeAgents, Enumerable.Empty<GeneratorWarning>());
    }
  }
}
