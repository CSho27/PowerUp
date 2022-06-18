using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Fetchers.MLBLookupService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerUp.Generators
{
  public interface IPlayerGenerator
  {
    Player GeneratePlayer(int lsPlayerId, PlayerGenerationAlgorithm generationAlgorithm);
  }

  public class PlayerGenerator : IPlayerGenerator
  {
    private readonly IPlayerApi _playerApi;
    private readonly IMLBLookupServiceClient _mlbLookupServiceClient;

    public PlayerGenerator(
      IPlayerApi playerApi,
      IMLBLookupServiceClient mlbLookupServiceClient
    )
    {
      _playerApi = playerApi;
      _mlbLookupServiceClient = mlbLookupServiceClient;
    }

    public Player GeneratePlayer(int lsPlayerId, PlayerGenerationAlgorithm generationAlgorithm)
    {
      var data = new PlayerGenerationData();
      var fetchTasks = new List<Task>();

      if (generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSPlayerInfo)) 
      {
        var fetchPlayerInfo = Task.Run(async () =>
        {
          var response = await _mlbLookupServiceClient.GetPlayerInfo(lsPlayerId);
          data.PlayerInfo = new LSPlayerInfoDataset(response);
        });
        fetchTasks.Add(fetchPlayerInfo);
      }
      Task.WaitAll(fetchTasks.ToArray());

      if (data.PlayerInfo == null)
        throw new InvalidOperationException("No Player Info Found for Id");

      var player = _playerApi.CreateDefaultPlayer(EntitySourceType.Generated, isPitcher: data.PlayerInfo.PrimaryPosition == Position.Pitcher);

      var propertiesThatHaveBeenSet = new HashSet<string>();
      foreach(var setter in generationAlgorithm.PropertySetters)
      {
        if (propertiesThatHaveBeenSet.Contains(setter.PropertyKey))
          continue;

        var wasSet = setter.SetProperty(player, data);
        if (wasSet)
          propertiesThatHaveBeenSet.Add(setter.PropertyKey);
      }

      return player;
    }
  }

  public abstract class PlayerGenerationAlgorithm : GenerationAlgorithm<Player, PlayerGenerationDataset, PlayerGenerationData> { }
  public abstract  class PlayerPropertySetter : PropertySetter<Player, PlayerGenerationData> { }
}
