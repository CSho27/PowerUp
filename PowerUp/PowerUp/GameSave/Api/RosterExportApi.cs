﻿using PowerUp.Entities;
using PowerUp.Entities.Rosters;
using PowerUp.GameSave.GameSaveManagement;
using PowerUp.GameSave.Objects.FreeAgents;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using PowerUp.Mappers;
using PowerUp.Mappers.Players;
using PowerUp.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.GameSave.Api
{
  public interface IRosterExportApi
  {
    void ExportRoster(RosterExportParameters parameters);
  }

  public class RosterExportApi : IRosterExportApi
  {
    private readonly ICharacterLibrary _characterLibrary;
    private readonly IPlayerMapper _playerMapper;
    private readonly IGameSaveManager _gameSaveManager;
    private readonly IPlayerSalariesLibrary _playerSalariesLibrary;
    private readonly IPowerProsIdAssigner _powerProsIdAssigner;

    public RosterExportApi(
      ICharacterLibrary characterLibrary,
      IPlayerMapper playerMapper,
      IGameSaveManager gameSaveManager,
      IPlayerSalariesLibrary playerSalariesLibrary,
      IPowerProsIdAssigner powerProsIdAssigner
    )
    {
      _characterLibrary = characterLibrary;
      _playerMapper = playerMapper;
      _gameSaveManager = gameSaveManager;
      _playerSalariesLibrary = playerSalariesLibrary;
      _powerProsIdAssigner = powerProsIdAssigner;
    }

    public void ExportRoster(RosterExportParameters parameters)
    {
      if(parameters.ExportDirectory == null)
        throw new ArgumentNullException(nameof(parameters.ExportDirectory));
      if (parameters.Roster == null)
        throw new ArgumentNullException(nameof(parameters.Roster));

      var roster = parameters.Roster!;
      var (rosterFilePath, gameSaveId) = _gameSaveManager.CreateNewGameSave(parameters.ExportDirectory, parameters.SourceGameSave, roster.Name);

      using (var writer = new GameSaveWriter(_characterLibrary, rosterFilePath))
      {
        var teams = roster.GetTeams()
          .OrderBy(t => t.Value.GetDivision())
          .ThenBy(t => t.Value == MLBPPTeam.NationalLeagueAllStars)
          .ThenBy(t => t.Value);

        var playersOnTeams = teams
          .SelectMany(t => t.Key.GetPlayers().Select(p => (ppTeam: t.Value, player: p)))
          .ToList();
        var freeAgents = roster.GetFreeAgentPlayers();
        var allPlayerParameters = playersOnTeams
          .Select(p => new PowerProsIdParameters { PlayerId = p.player.Id!.Value, YearsInMajors = p.player.YearsInMajors, Overall = p.player.Overall })
          .Concat(freeAgents.Select(p => new PowerProsIdParameters { PlayerId = p.Id!.Value, YearsInMajors = p.YearsInMajors, Overall = p.Overall }));

        var ppIdByPlayerId = _powerProsIdAssigner.AssignIds(allPlayerParameters, _playerSalariesLibrary.PlayerSalaries);

        var gsPlayers = new List<GSPlayer>();
        var ppIdsByTeamAndId = new Dictionary<MLBPPTeam, IDictionary<int, ushort>>();
        for(var i=0; i<playersOnTeams.Count; i++)
        {
          var player = playersOnTeams[i];
          var ppId = (ushort)ppIdByPlayerId[player.player.Id!.Value];
          ppIdsByTeamAndId.TryAdd(player.ppTeam, new Dictionary<int, ushort>());
          ppIdsByTeamAndId[player.ppTeam].Add(player.player.Id!.Value, ppId);

          // If player is on all-star team, look at regular teams to see which teams that player is on
          // If that player is on exactly one team, give him that team's jersey. Otherwise, give him the FA jersey
          var jerseyTeam = player.ppTeam;
          if (player.ppTeam.GetDivision() == MLBPPDivision.AllStars)
          {
            var onRegTeams = playersOnTeams.Where(p => p.player.Id == player.player.Id && p.ppTeam.GetDivision() != MLBPPDivision.AllStars);
            if (onRegTeams.Count() == 1)
              jerseyTeam = onRegTeams.Single().ppTeam;
            else
              jerseyTeam = MLBPPTeam.NationalLeagueAllStars;
          }

          gsPlayers.Add(_playerMapper.MapToGSPlayer(player.player, jerseyTeam, ppId));
        }

        var mappedFAs = freeAgents.Select(fa => _playerMapper.MapToGSPlayer(fa, MLBPPTeam.NationalLeagueAllStars, (ushort)ppIdByPlayerId[fa.Id!.Value]));
        gsPlayers.AddRange(mappedFAs);

        var blankFreeAgentSpots = new List<GSFreeAgent>();
        for (var i = freeAgents.Count(); i < 15; i++)
          blankFreeAgentSpots.Add(new GSFreeAgent() { PowerProsPlayerId = 0 });

        var gameSave = new GSGameSave
        {
          PowerUpId = (short)gameSaveId,
          Players = gsPlayers.OrderBy(p => p.PowerProsId),
          Teams = teams.Select(t => t.Key.MapToGSTeam(t.Value, ppIdsByTeamAndId[t.Value])),
          Lineups = teams.Select(t => t.Key.MapToGSLineup(ppIdsByTeamAndId[t.Value])),
          FreeAgents = new GSFreeAgentList 
          { 
            FreeAgents = mappedFAs
              .Select(fa => new GSFreeAgent { PowerProsPlayerId = fa.PowerProsId })
              .Concat(blankFreeAgentSpots)
          } 
        };

        writer.Write(gameSave);
      }
    }
  }

  public class RosterExportParameters
  {
    public Roster? Roster { get; set; }
    public string? SourceGameSave { get; set; }
    public string? ExportDirectory { get; set; }
  }
}
