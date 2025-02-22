using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Teams;
using PowerUp.Fetchers.MLBLookupService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Generators
{
  public interface IRosterGenerator
  {
    RosterGenerationResult GenerateRoster(
      int year,
      PlayerGenerationAlgorithm playerGenerationAlgorithm,
      Action<ProgressUpdate>? onTeamProgressUpdate = null,
      Action<ProgressUpdate>? onPlayerProgressUpdate = null,
      Action? onFatalError = null
    );
  }

  public class RosterGenerationResult
  {
    public Roster Roster { get; set; } 
    public IEnumerable<GeneratorWarning> Warnings { get; set; } = Enumerable.Empty<GeneratorWarning>();

    public RosterGenerationResult(Roster roster, IEnumerable<GeneratorWarning> warnings)
    {
      Roster = roster;
      Warnings = warnings;
    }
  }

  public class RosterGenerator : IRosterGenerator
  {
    private readonly IMLBLookupServiceClient _mlbLookupServiceClient;
    private readonly ITeamGenerator _teamGenerator;

    public RosterGenerator(
      IMLBLookupServiceClient mlbLookupServiceClient,
      ITeamGenerator teamGenerator
    )
    {
      _mlbLookupServiceClient = mlbLookupServiceClient;
      _teamGenerator = teamGenerator;
    }

    public RosterGenerationResult GenerateRoster(
      int year, 
      PlayerGenerationAlgorithm playerGenerationAlgorithm, 
      Action<ProgressUpdate>? onTeamProgressUpdate = null, 
      Action<ProgressUpdate>? onPlayerProgressUpdate = null,
      Action? onFatalError = null
    )
    {
      var teamResults = Task.Run(async () => {
        try
        {
          return await _mlbLookupServiceClient.GetTeamsForYear(year);
        }
        catch (Exception)
        {
          if (onFatalError is not null) onFatalError();
          return null;
        }
      }).GetAwaiter().GetResult();
      if (teamResults is null) throw new Exception($"Failed to fetch teams for {year}");

      var teams = teamResults.Results.ToList();
      var playerIdsByLSPlayerId = new Dictionary<long, int>();
      var freeAgents = new List<Player>();
      var alPlayers = new List<Player>();
      var nlPlayers = new List<Player>();

      var generatedTeams = new List<TeamGenerationResult>();
      for (var i = 0; i < teams.Count; i++)
      {
        var team = teams[i];
        if (onPlayerProgressUpdate != null)
          onPlayerProgressUpdate(new ProgressUpdate("--", 0, 0));
        if (onTeamProgressUpdate != null)
          onTeamProgressUpdate(new ProgressUpdate($"Generating {team.FullName}", i, teams.Count));

        var generatedTeam = _teamGenerator.GenerateTeam(team.LSTeamId, year, team.FullName, playerGenerationAlgorithm, onPlayerProgressUpdate);
        if(generatedTeam.PlayersOnTeam.Count() >= 10)
          generatedTeams.Add(generatedTeam);

        freeAgents.AddRange(generatedTeam.PotentialFreeAgents);

        if(team.League == "NL")
          nlPlayers.AddRange(generatedTeam.PlayersOnTeam);
        else
          alPlayers.AddRange(generatedTeam.PlayersOnTeam);
      }

      var ppTeams = Enum.GetValues<MLBPPTeam>().ToList();
      var ppTeamByTeamId = new Dictionary<long, MLBPPTeam>();

      // Match by lsTeamId
      foreach (var ppTeam in ppTeams)
      {
        var teamIdMatch = generatedTeams.SingleOrDefault(r => r.LSTeamId == ppTeam.GetLSTeamId());
        if (teamIdMatch != null)
          ppTeamByTeamId.Add(teamIdMatch.LSTeamId, ppTeam);
      }

      // Match by location or team name
      foreach(var team in teamResults.Results.Where(team => generatedTeams.Any(t => t.LSTeamId == team.LSTeamId) && !ppTeamByTeamId.ContainsKey(team.LSTeamId)))
      {
        var locationMatch = ppTeams
          .Where(t => !ppTeamByTeamId.ContainsValue(t))
          .Cast<MLBPPTeam?>()
          .FirstOrDefault(t => 
            t!.Value.GetTeamLocation() == team.City ||
            t!.Value.GetTeamLocation() == team.State ||
            t.Value.GetTeamLocation() == team.LocationName ||
            t.Value.GetDisplayName() == team.TeamName
          );
        if (locationMatch != null)
          ppTeamByTeamId.Add(team.LSTeamId, locationMatch.Value);
      }

      // Put rest of teams somewhere
      foreach (var team in generatedTeams.Where(team => !ppTeamByTeamId.ContainsKey(team.LSTeamId)))
      {
        var firstTeamNotInUse = ppTeams
          .OrderByDescending(p => p.GetDivision() != MLBPPDivision.AllStars)
          .Cast<MLBPPTeam?>()
          .FirstOrDefault(p => !ppTeamByTeamId.ContainsValue(p!.Value));
        if(firstTeamNotInUse != null)
          ppTeamByTeamId.Add(team.LSTeamId, firstTeamNotInUse.Value);
      }

      var teamsToUse = generatedTeams
        .Where(t => ppTeamByTeamId.ContainsKey(t.LSTeamId))
        .Select(t => t.Team)
        .ToList();

      Team? nlAllStars = null;
      if(!ppTeamByTeamId.ContainsValue(MLBPPTeam.NationalLeagueAllStars) && nlPlayers.Any())
      {
        nlAllStars = BuildAllStarTeam(nlPlayers, year, MLBPPTeam.NationalLeagueAllStars.GetFullDisplayName(), isAL: false);
        ppTeamByTeamId[nlAllStars.GeneratedTeam_LSTeamId!.Value] = MLBPPTeam.NationalLeagueAllStars;
        teamsToUse.Add(nlAllStars);
      }

      if (!ppTeamByTeamId.ContainsValue(MLBPPTeam.AmericanLeagueAllStars) && alPlayers.Any())
      {
        var alAllStars = BuildAllStarTeam(alPlayers, year, MLBPPTeam.AmericanLeagueAllStars.GetFullDisplayName(), isAL: true);
        ppTeamByTeamId[alAllStars.GeneratedTeam_LSTeamId!.Value] = MLBPPTeam.AmericanLeagueAllStars;
        teamsToUse.Add(alAllStars);
      }

      var notFirstTeamNl = nlAllStars != null
        ? nlPlayers.Where(p => !nlAllStars.PlayerDefinitions.Any(a => a.PlayerId == p.Id))
        : Enumerable.Empty<Player>();
      if (!ppTeamByTeamId.ContainsValue(MLBPPTeam.AmericanLeagueAllStars) && !alPlayers.Any() && notFirstTeamNl.Any())
      {
        var nlAllStars2 = BuildAllStarTeam(notFirstTeamNl, year, "Second Team All National League", isAL: true);
        ppTeamByTeamId[nlAllStars2.GeneratedTeam_LSTeamId!.Value] = MLBPPTeam.AmericanLeagueAllStars;
        teamsToUse.Add(nlAllStars2);
      }

      DatabaseConfig.Database.SaveAll(teamsToUse);

      var teamsByPPTeam = teamsToUse.ToDictionary(t => ppTeamByTeamId[t.GeneratedTeam_LSTeamId!.Value], t => t.Id!.Value);

      var teamsNotInList = ppTeams.Where(p => !ppTeamByTeamId.Any(t => t.Value == p));
      var baseRoster = DatabaseConfig.Database.Query<Roster>().Where(r => r.SourceType == EntitySourceType.Base).Single();

      foreach (var team in teamsNotInList)
        teamsByPPTeam.Add(team, baseRoster.TeamIdsByPPTeam[team]);

      var orderedFreeAgents = freeAgents.OrderByDescending(fa => fa.Overall);
      var freeAgentPitchers = orderedFreeAgents.Where(fa => fa.PrimaryPosition == Position.Pitcher).Take(10);
      var freeAgentHitters = orderedFreeAgents.Where(fa => fa.PrimaryPosition != Position.Pitcher).Take(5);

      // Add base roster teams for remaining PPTeams
      var roster = new Roster
      {
        SourceType = EntitySourceType.Generated,
        Name = $"{year} MLB Rosters",
        Year = year,
        TeamIdsByPPTeam = teamsByPPTeam,
        FreeAgentPlayerIds = freeAgentPitchers.Concat(freeAgentHitters).Select(fa => fa.Id!.Value)
      };

      return new RosterGenerationResult(roster, Enumerable.Empty<GeneratorWarning>());
    }

    private Team BuildAllStarTeam(IEnumerable<Player> players, int year, string name, bool isAL)
    {
      var rosterParams = players.Select(p => new RosterParams(
        playerId: p.Id!.Value,
        hitterRating: p.HitterAbilities.GetHitterRating(),
        pitcherRating: p.PitcherAbilities.GetPitcherRating(),
        contact: p.HitterAbilities.Contact,
        power: p.HitterAbilities.Power,
        runSpeed: p.HitterAbilities.RunSpeed,
        primaryPosition: p.PrimaryPosition,
        pitcherType: p.PitcherType,
        positionCapabilityDictionary: p.PositionCapabilities.GetDictionary()
      ));

      var rosterResults = RosterCreator.CreateRosters(rosterParams);

      var mlbPPTeam = isAL
        ? MLBPPTeam.AmericanLeagueAllStars
        : MLBPPTeam.NationalLeagueAllStars;

      return new Team
      {
        Name = name,
        SourceType = EntitySourceType.Generated,
        GeneratedTeam_LSTeamId = mlbPPTeam.GetLSTeamId(),
        Year = year,
        PlayerDefinitions = players.Where(p => rosterResults.FortyManRoster.Any(id => id == p.Id)).Select(p => new PlayerRoleDefinition(p.Id!.Value)
        {
          IsAAA = !rosterResults.TwentyFiveManRoster.Contains(p.Id!.Value),
          PitcherRole = rosterResults.Starters.Any(id => id == p.Id)
            ? PitcherRole.Starter
            : rosterResults.Closer == p.GeneratedPlayer_LSPLayerId
              ? PitcherRole.Closer
              : PitcherRole.MiddleReliever
        }),
        NoDHLineup = rosterResults.NoDHLineup.Select(s => new LineupSlot { PlayerId = (int?)s.PlayerId, Position = s.Position }),
        DHLineup = rosterResults.DHLineup.Select(s => new LineupSlot { PlayerId = (int?)s.PlayerId, Position = s.Position })
      };
    }
  }
}
