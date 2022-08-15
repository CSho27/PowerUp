using PowerUp.Entities;
using PowerUp.Entities.Rosters;
using PowerUp.GameSave.Objects.FreeAgents;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using PowerUp.Mappers;
using PowerUp.Mappers.Players;
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
    private readonly IBaseGameSavePathProvider _baseGameSavePathProvider;
    private readonly ICharacterLibrary _characterLibrary;
    private readonly IPlayerMapper _playerMapper;

    public RosterExportApi(
      IBaseGameSavePathProvider baseGameSavePathProvider,
      ICharacterLibrary characterLibrary,
      IPlayerMapper playerMapper
    )
    {
      _baseGameSavePathProvider = baseGameSavePathProvider;
      _characterLibrary = characterLibrary;
      _playerMapper = playerMapper;
    }

    public void ExportRoster(RosterExportParameters parameters)
    {
      if(parameters.ExportDirectory == null)
        throw new ArgumentNullException(nameof(parameters.ExportDirectory));
      if (parameters.Roster == null)
        throw new ArgumentNullException(nameof(parameters.Roster));

      var roster = parameters.Roster!;

      bool fileExists = true;
      string rosterFilePath = "";
      for(var i = 0; fileExists; i++)
      {
        var numString = i == 0
          ? ""
          : $"({i})";
        rosterFilePath = Path.Combine(parameters.ExportDirectory, $"{roster.Name}{numString}.pm2maus.dat");
        fileExists = File.Exists(rosterFilePath);
      }

      File.Copy(parameters.SourceGameSave ?? _baseGameSavePathProvider.GetPath(), rosterFilePath);

      using (var writer = new GameSaveWriter(_characterLibrary, rosterFilePath))
      {
        var teams = roster.GetTeams()
          .OrderBy(t => t.Value.GetDivision())
          .ThenBy(t => t.Value == MLBPPTeam.NationalLeagueAllStars)
          .ThenBy(t => t.Value);

        var playersOnTeams = teams
          .SelectMany(t => t.Key.GetPlayers().Select(p => (ppTeam: t.Value, player: p)))
          .ToList();

        var gsPlayers = new List<GSPlayer>();
        var ppIdsByTeamAndId = new Dictionary<MLBPPTeam, IDictionary<int, ushort>>();
        var ppId = (ushort)0;
        for(var i=0; i<playersOnTeams.Count; i++)
        {
          var player = playersOnTeams[i];
          ppId = (ushort)(i + 1);
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


        var freeAgents = roster.GetFreeAgentPlayers().Select((fa, i) => _playerMapper.MapToGSPlayer(fa, MLBPPTeam.NationalLeagueAllStars, ppId + i + 1));
        gsPlayers.AddRange(freeAgents);

        var blankFreeAgentSpots = new List<GSFreeAgent>();
        for (var i = freeAgents.Count(); i < 15; i++)
          blankFreeAgentSpots.Add(new GSFreeAgent() { PowerProsPlayerId = 0 });

        var gameSave = new GSGameSave
        {
          PowerUpId = (short)roster.Id!.Value,
          Players = gsPlayers,
          Teams = teams.Select(t => t.Key.MapToGSTeam(t.Value, ppIdsByTeamAndId[t.Value])),
          Lineups = teams.Select(t => t.Key.MapToGSLineup(ppIdsByTeamAndId[t.Value])),
          FreeAgents = new GSFreeAgentList 
          { 
            FreeAgents = freeAgents
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
