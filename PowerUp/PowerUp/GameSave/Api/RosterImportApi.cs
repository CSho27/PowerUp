using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Teams;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using PowerUp.Mappers;
using PowerUp.Mappers.Players;
using System;
using System.Collections.Generic;
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
      if(parameters.FilePath == null)
        throw new ArgumentNullException(nameof(parameters.FilePath));
      if (!parameters.IsBase && parameters.ImportSource == null)
        throw new ArgumentNullException(nameof(parameters.ImportSource));

      using (var reader = new GameSaveReader(_characterLibrary, parameters.FilePath))
      {
        var gameSave = reader.Read();
        var gsPlayers = gameSave.Players.Where(p => p.PowerProsId.HasValue && p.PowerProsId != 0);

        var playerIdsByPPId = new Dictionary<ushort, int>();
        var players = new List<Player>();

        foreach (var gsPlayer in gsPlayers)
        {
          var player = _playerMapper.MapToPlayer(gsPlayer, PlayerMappingParameters.FromRosterImport(parameters));
          DatabaseConfig.PlayerDatabase.Save(player);
          playerIdsByPPId.Add(gsPlayer.PowerProsId!.Value, player.Id!.Value);
          players.Add(player);
        }

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

          var team = gsTeam.MapToTeam(gsLineup, TeamMappingParameters.FromImportParameters(parameters, playerIdsByPPId));
          DatabaseConfig.TeamDatabase.Save(team);
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
          TeamIdsByPPTeam = teamKeysByPPTeam
        };

        DatabaseConfig.RosterDatabase.Save(roster);

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
    public string? FilePath { get; set; }
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
