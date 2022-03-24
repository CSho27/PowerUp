using PowerUp.Entities;
using PowerUp.Entities.Rosters;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using PowerUp.Mappers;
using PowerUp.Mappers.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

      File.Copy(_baseGameSavePathProvider.GetPath(), rosterFilePath);

      using (var writer = new GameSaveWriter(_characterLibrary, rosterFilePath))
      {
        var teams = roster.GetTeams()
          .OrderBy(t => t.Value.GetDivision())
          .ThenBy(t => t.Value == MLBPPTeam.NationalLeagueAllStars)
          .ThenBy(t => t.Value);

        var players = teams
          .SelectMany(t => t.Key.GetPlayers().Select(p => (ppTeam: t.Value, player: p)))
          .Where(p => p.ppTeam.GetDivision() != MLBPPDivision.AllStars).ToList();

        var gsPlayers = new List<GSPlayer>();
        var ppIdsById = new Dictionary<int, ushort>();
        for(var i=0; i<players.Count; i++)
        {
          var player = players[i];
          var ppId = (ushort)(i + 1);
          ppIdsById.Add(player.player.Id!.Value, ppId);
          gsPlayers.Add(_playerMapper.MapToGSPlayer(player.player, player.ppTeam, ppId));
        }

        var gameSave = new GSGameSave
        {
          Players = gsPlayers,
          Teams = teams.Select(t => t.Key.MapToGSTeam(t.Value, ppIdsById)),
          Lineups = teams.Select(t => t.Key.MapToGSLineup(ppIdsById))
        };

        writer.Write(gameSave);
      }
    }
  }

  public class RosterExportParameters
  {
    public Roster? Roster { get; set; }
    public string? ExportDirectory { get; set; }
  }
}
