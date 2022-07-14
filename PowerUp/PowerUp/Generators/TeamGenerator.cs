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

      var playerLineupParams = generatedPlayers.Select(p => new LineupParams(
        playerId: p.LSPlayerId,
        hitterRating: p.Player.HitterAbilities.GetHitterRating(),
        contact: p.Player.HitterAbilities.Contact,
        power: p.Player.HitterAbilities.Power,
        runSpeed: p.Player.HitterAbilities.RunSpeed,
        primaryPosition: p.Player.PrimaryPosition,
        positionCapabilityDictionary: p.Player.PositionCapabilities.GetDictionary()
      ));

      var dhLineup = LineupCreator.CreateLineup(playerLineupParams, useDH: true);
      var noDHLineup = LineupCreator.CreateLineup(playerLineupParams, useDH: false);
      
      var playersOrderedByHitterAbility = generatedPlayers.OrderByDescending(p => p.Player.HitterAbilities.GetHitterRating()).ToList();

      var playersOrderedByPitcherAbility = generatedPlayers.OrderByDescending(p => p.Player.PitcherAbilities.GetPitcherRating()).ToList();
      var starters = playersOrderedByPitcherAbility.Where(p => p.Player.PitcherType == PitcherType.Starter).Take(5);
      var closer = playersOrderedByPitcherAbility.Where(p => p.Player.PitcherType == PitcherType.Closer).FirstOrDefault();
      var relievers = playersOrderedByPitcherAbility.Where(p => !starters.Any(s => s.LSPlayerId == p.LSPlayerId && p.LSPlayerId != closer?.LSPlayerId));

      var mlbRosterPlayerIds = new HashSet<long>();

      // Add all players in lineups
      foreach (var player in dhLineup.Concat(noDHLineup))
      {
        if(player.PlayerId.HasValue)
          mlbRosterPlayerIds.Add(player.PlayerId!.Value);
      }

      // Add backup catcher
      var backupCatcher = playersOrderedByHitterAbility.Where(p => !mlbRosterPlayerIds.Any(id => id == p.LSPlayerId) && p.Player.PrimaryPosition == Position.Catcher).FirstOrDefault();
      if (backupCatcher != null)
        mlbRosterPlayerIds.Add(backupCatcher.LSPlayerId);

      // Add other bench players
      for(var i=0; i<playersOrderedByHitterAbility.Count && mlbRosterPlayerIds.Count < 12; i++)
        mlbRosterPlayerIds.Add(playersOrderedByHitterAbility[i].LSPlayerId);

      // Add starting pitchers
      foreach(var starter in starters)
        mlbRosterPlayerIds.Add(starter.LSPlayerId);

      // Add closer
      if(closer != null)
        mlbRosterPlayerIds.Add(closer.LSPlayerId);

      // Add relievers
      for (var i = 0; i < playersOrderedByPitcherAbility.Count && mlbRosterPlayerIds.Count < 25; i++)
        mlbRosterPlayerIds.Add(playersOrderedByPitcherAbility[i].LSPlayerId);

      var fortyManRoster = new HashSet<long>(mlbRosterPlayerIds);
      // Add hitters to 40 man roster
      for (var i = 0; i < playersOrderedByHitterAbility.Count && fortyManRoster.Count < 33; i++)
        fortyManRoster.Add(playersOrderedByHitterAbility[i].LSPlayerId);

      // Add pitchers to 40 man roster
      for (var i = 0; i < playersOrderedByPitcherAbility.Count && fortyManRoster.Count < 40; i++)
        fortyManRoster.Add(playersOrderedByPitcherAbility[i].LSPlayerId);

      // Save generated players
      var playersToSave = generatedPlayers.Where(p => fortyManRoster.Any(id => id == p.LSPlayerId)).Select(p => p.Player);
      DatabaseConfig.Database.SaveAll(playersToSave);

      var team = new Team
      {
        Name = name,
        SourceType = EntitySourceType.Generated,
        PlayerDefinitions = playersToSave.Select(p => new PlayerRoleDefinition(p.Id!.Value)
        {
          IsAAA = mlbRosterPlayerIds.Contains(p.GeneratedPlayer_LSPLayerId!.Value),
          PitcherRole = starters.Any(s => s.LSPlayerId == p.GeneratedPlayer_LSPLayerId)
            ? PitcherRole.Starter
            : closer?.LSPlayerId == p.GeneratedPlayer_LSPLayerId
              ? PitcherRole.Closer
              : PitcherRole.MiddleReliever
        }),
        NoDHLineup = noDHLineup.Select(s => new LineupSlot { PlayerId = playersToSave.SingleOrDefault(p => p.GeneratedPlayer_LSPLayerId == s.PlayerId)?.Id, Position = s.Position }),
        DHLineup = dhLineup.Select(s => new LineupSlot { PlayerId = playersToSave.Single(p => p.GeneratedPlayer_LSPLayerId == s.PlayerId).Id, Position = s.Position })
      };

      return new TeamGenerationResult(team, Enumerable.Empty<GeneratorWarning>());

    }
  }
}
