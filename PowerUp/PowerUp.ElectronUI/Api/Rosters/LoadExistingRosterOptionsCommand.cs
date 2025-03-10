﻿using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Rosters;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class LoadExistingRosterOptionsCommand : ICommand<LoadRosterOptionsRequest, IEnumerable<SimpleCode>>
  {
    public Task<IEnumerable<SimpleCode>> Execute(LoadRosterOptionsRequest request)
    {
      var rosters = DatabaseConfig.Database.LoadAll<Roster>();
      return Task.FromResult(rosters.Select(r => new SimpleCode(r.Id!.Value, $"{r.Name} - {r.Identifier} ({r.SourceType})")));
    }
  }

  public class LoadRosterOptionsRequest
  {
    public int RosterId { get; set; }
  }
}
