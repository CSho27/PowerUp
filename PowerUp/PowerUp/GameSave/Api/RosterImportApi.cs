using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Teams;
using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using PowerUp.Mappers;
using PowerUp.Mappers.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.GameSave.Api
{
  public interface IRosterImportApi
  {
    RosterImportResult ImportRoster(RosterImportParameters parameters);
  }

  public class RosterImportApi : IRosterImportApi
  {
    private readonly ICharacterLibrary _characterLibrary;
    private readonly IPlayerMapper _playerMapper;

    public RosterImportApi(ICharacterLibrary characterLibrary, IPlayerMapper playerMapper)
    {
      _characterLibrary = characterLibrary;
      _playerMapper = playerMapper;
    }

    public RosterImportResult ImportRoster(RosterImportParameters parameters)
    {
      if(parameters.Stream == null)
        throw new ArgumentNullException(nameof(parameters.Stream));
      if (!parameters.IsBase && parameters.ImportSource == null)
        throw new ArgumentNullException(nameof(parameters.ImportSource));

      // CHRISTODO: Don't hard code this
      using (var reader = new GameSaveReader(_characterLibrary, parameters.Stream, ByteOrder.BigEndian))
      {
        var gameSave = reader.Read();
        var gsPlayers = gameSave.Players.Where(p => p.PowerProsId.HasValue && p.PowerProsId != 0);

        var players = new List<Player>();

        foreach (var gsPlayer in gsPlayers)
        {
          var player = _playerMapper.MapToPlayer(gsPlayer, PlayerMappingParameters.FromRosterImport(parameters));
          players.Add(player);
        }
        DatabaseConfig.Database.SaveAll(players);
        var playerIdsByPPId = players.ToDictionary(p => p.SourcePowerProsId!.Value, p => p.Id!.Value);

        var gsTeams = gameSave.Teams.ToList();
        var gsLineups = gameSave.Lineups.ToList();
        if (gsTeams.Count != gsLineups.Count)
          throw new InvalidOperationException("Number of teams and lineups must match");

        var teamKeysByPPTeam = new Dictionary<MLBPPTeam, int>();
        var teams = new List<Team>();

        for (int i=0; i<gsTeams.Count; i++)
        {
          var gsTeam = gsTeams[i];
          var gsLineup = gsLineups[i];
          var teamId = gsTeam.PlayerEntries!.First().PowerProsTeamId!.Value;

          var team = TeamMapper.MapToTeam(gsTeam, gsLineup, TeamMappingParameters.FromImportParameters(parameters, playerIdsByPPId));
          DatabaseConfig.Database.Save(team);
          teamKeysByPPTeam.Add((MLBPPTeam)teamId, team.Id!.Value);
          teams.Add(team);
        }

        var roster = new Roster
        {
          Name = parameters.IsBase
            ? "MLB Power Pros Base Roster"
            : parameters.ImportSource!,
          SourceType = parameters.IsBase
            ? EntitySourceType.Base
            : EntitySourceType.Imported,
          ImportSource = parameters.IsBase
            ? null
            : parameters.ImportSource,
          TeamIdsByPPTeam = teamKeysByPPTeam,
          FreeAgentPlayerIds = gameSave.FreeAgents.FreeAgents!.Select(fa => playerIdsByPPId[fa.PowerProsPlayerId!.Value]),
        };

        DatabaseConfig.Database.Save(roster);

        return new RosterImportResult
        {
          Success = true,
          Players = players,
          Teams = teams,
          Roster = roster
        };
      }
    }
  }

  public class RosterImportParameters
  {
    public Stream? Stream { get; set; }
    public bool IsBase { get; set; }
    public string? ImportSource { get; set; }
  }

  public class RosterImportResult
  {
    public bool Success { get; set; }
    public IEnumerable<Player> Players { get; set; } = Enumerable.Empty<Player>();
    public IEnumerable<Team> Teams { get; set; } = Enumerable.Empty<Team>();
    public Roster? Roster { get; set; }
  }
}
