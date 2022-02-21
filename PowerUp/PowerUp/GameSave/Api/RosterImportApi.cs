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

    public RosterImportApi(ICharacterLibrary characterLibrary)
    {
      _characterLibrary = characterLibrary;
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

        var playerKeysByPPId = new Dictionary<ushort, string>();
        var players = new List<Player>();

        foreach (var gsPlayer in gsPlayers)
        {
          var player = gsPlayer.MapToPlayer(PlayerMappingParameters.FromRosterImport(parameters));
          playerKeysByPPId.Add(gsPlayer.PowerProsId!.Value, player.GetKey());
          players.Add(player);
          DatabaseConfig.JsonDatabase.Save(player);
        }

        var gsTeams = gameSave.Teams.ToList();
        var gsLineups = gameSave.Lineups.ToList();
        if (gsTeams.Count != gsLineups.Count)
          throw new InvalidOperationException("Number of teams and lineups must match");

        var teamKeysByPPTeam = new Dictionary<MLBPPTeam, string>();
        var teams = new List<Team>();

        for (int i=0; i<gsTeams.Count; i++)
        {
          var gsTeam = gsTeams[i];
          var gsLineup = gsLineups[i];
          var teamId = gsTeam.PlayerEntries!.First().PowerProsTeamId!.Value;

          var team = gsTeam.MapToTeam(gsLineup, TeamMappingParameters.FromImportParameters(parameters, playerKeysByPPId));
          teamKeysByPPTeam.Add((MLBPPTeam)teamId, team.GetKey());
          teams.Add(team);
          DatabaseConfig.JsonDatabase.Save(team);
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
          TeamKeysByPPTeam = teamKeysByPPTeam
        };

        DatabaseConfig.JsonDatabase.Save(roster);

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
