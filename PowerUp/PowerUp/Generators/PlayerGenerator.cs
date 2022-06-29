﻿using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Fetchers.MLBLookupService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Generators
{
  public interface IPlayerGenerator
  {
    Player GeneratePlayer(int lsPlayerId, int year, PlayerGenerationAlgorithm generationAlgorithm);
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

    public Player GeneratePlayer(int lsPlayerId, int year, PlayerGenerationAlgorithm generationAlgorithm)
    {
      var data = new PlayerGenerationData();
      var fetchTasks = new List<Task>();

      data.Year = year;

      if (generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSPlayerInfo)) 
      {
        var fetchPlayerInfo = Task.Run(async () =>
        {
          var response = await _mlbLookupServiceClient.GetPlayerInfo(lsPlayerId);

          if (response == null)
            throw new InvalidOperationException("No Player Info Found for Id");

          data.PlayerInfo = new LSPlayerInfoDataset(response);
        });
        fetchTasks.Add(fetchPlayerInfo);
      }

      if (generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSHittingStats))
      {
        var fetchHittingStats = Task.Run(async () =>
        {
          var response = await _mlbLookupServiceClient.GetHittingStats(lsPlayerId, year);
          if (response.Results.Any())
            data.HittingStats = new LSHittingStatsDataset(response.Results);
        });
        fetchTasks.Add(fetchHittingStats);
      }

      if (generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSFieldingStats))
      {
        var fetchFieldingStats = Task.Run(async () =>
        {
          var response = await _mlbLookupServiceClient.GetFieldingStats(lsPlayerId, year);
          if (response.Results.Any())
            data.FieldingStats = new LSFieldingStatDataset(response.Results);
        });
        fetchTasks.Add(fetchFieldingStats);
      }

      if (generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSPitchingStats))
      {
        var fetchPitchingStats = Task.Run(async () =>
        {
          var response = await _mlbLookupServiceClient.GetPitchingStats(lsPlayerId, year);
          if(response.Results.Any())
            data.PitchingStats = new LSPitchingStatsDataset(response.Results);
        });
        fetchTasks.Add(fetchPitchingStats);
      }

      Task.WaitAll(fetchTasks.ToArray());

      var player = _playerApi.CreateDefaultPlayer(EntitySourceType.Generated, isPitcher: data.PlayerInfo!.PrimaryPosition == Position.Pitcher);
      player.Year = year;

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
