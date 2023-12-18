using PowerUp.Entities.Players;
using PowerUp.Fetchers.MLBLookupService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Fetchers.Statcast
{
  public class PlayerSearchResults
  {
    public long TotalResults { get; }
    public IEnumerable<PlayerSearchResult> Results { get; }

    public PlayerSearchResults(long totalResults, IEnumerable<PlayerInfoResult> results)
    {
      TotalResults = totalResults;
      Results = results.Select(r => new PlayerSearchResult(r));
    }
  }

  public class PlayerSearchResult
  {
    public long LSPlayerId { get; }
    public Position Position { get; }
    public string FirstName { get; }
    public string FirstNameUsed { get; }
    public string LastName { get; }
    public string FormalDisplayName { get; }
    public string InformalDisplayName { get; }
    public BattingSide BattingSide { get; }
    public ThrowingArm ThrowingArm { get; }
    public int? Weight { get; }
    public int? HeightFeet { get; }
    public int? HeightInches { get; }
    public DateTime? BirthDate { get; }
    public string? BirthCountry { get; }
    public string? BirthState { get; }
    public string? BirthCity { get; }
    public string? HighSchool { get; }
    public string? College { get; }
    public DateTime? ProDebutDate { get; }
    public int? ServiceYears { get; }
    public bool IsActive { get; }
    public string? TeamName { get; }

    public PlayerSearchResult(PlayerInfoResult searchResult)
    {
      LSPlayerId = searchResult.LSPlayerId;
      FirstName = searchResult.FirstName;
      FirstNameUsed = searchResult.FirstNameUsed;
      LastName = searchResult.LastName;
      FormalDisplayName = searchResult.FormalDisplayName;
      InformalDisplayName = searchResult.InformalDisplayName;
      Position = searchResult.Position;
      BattingSide = searchResult.BattingSide;
      ThrowingArm = searchResult.ThrowingArm;
      Weight = searchResult.Weight;
      HeightFeet = searchResult.HeightFeet;
      HeightInches = searchResult.HeightInches;
      BirthDate = searchResult.BirthDate;
      BirthCountry = searchResult.BirthCountry;
      BirthState = searchResult.BirthState;
      BirthCity = searchResult.BirthCity;
      HighSchool = searchResult.HighSchool;
      College = searchResult.College;
      ProDebutDate = searchResult.ProDebutDate;
      ServiceYears = searchResult.ServiceYears;
      IsActive = !searchResult.EndDate.HasValue;
      TeamName = searchResult.TeamName;
    }
  }
}
