using PowerUp.Databases;
using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Players;
using PowerUp.GameSave.Objects.Teams;
using PowerUp.Libraries;
using PowerUp.Mappers;
using PowerUp.Mappers.Players;

namespace PowerUp.ElectronUI.Api
{
  public class LoadBaseGameSaveCommand : ICommand<LoadBaseRequest, LoadBaseResponse>
  {
    private readonly ICharacterLibrary _characterLibrary;
    private readonly IBaseGameSavePathProvider _baseGameSavePathProvider;
    private readonly IPlayerDatabase _playerDatabase;
    private readonly ITeamDatabase _teamDatabase;

    public LoadBaseGameSaveCommand(
      ICharacterLibrary characterLibrary,
      IBaseGameSavePathProvider gameSavePathProvider,
      IPlayerDatabase playerDatabase,
      ITeamDatabase teamDatabase
    )
    {
      _characterLibrary = characterLibrary;
      _baseGameSavePathProvider = gameSavePathProvider;
      _playerDatabase = playerDatabase;
      _teamDatabase = teamDatabase;
    }

    public LoadBaseResponse Execute(LoadBaseRequest request)
    {
      var baseGameSavePath = _baseGameSavePathProvider.GetPath();
      using var playerReader = new PlayerReader(_characterLibrary, baseGameSavePath);
      var keysById = new Dictionary<ushort, string>();

      for(int i=1; i<1500; i++)
      {
        var mappingParameters = new PlayerMappingParameters { IsImported = false };
        var gsPlayer = playerReader.Read(i);

        if (gsPlayer.PowerProsId == 0)
          continue;

        var player = gsPlayer.MapToPlayer(mappingParameters);
        keysById.Add((ushort)i, player.GetKey());

        Console.WriteLine($"Saving: {player.SavedName}");
        _playerDatabase.Save(player);
      }

      using var teamReader = new TeamReader(_characterLibrary, baseGameSavePath);
      using var lineupReader = new LineupReader(_characterLibrary, baseGameSavePath);

      for(int i=0; i<32; i++)
      {
        var mappingParameters = new TeamMappingParameters { IsImported = false, KeysByPPId = keysById };
        var lineup = lineupReader.Read(i);
        var team = teamReader.Read(i).MapToTeam(lineup, mappingParameters);

        Console.WriteLine($"Saving: {team.Name}");
        _teamDatabase.Save(team);
      }

      return new LoadBaseResponse { Success = true };
    }
  }

  public class LoadBaseRequest
  {
    public int Throwaway { get; set; }
  }

  public class LoadBaseResponse
  {
    public bool Success { get; set; }
  }
}
