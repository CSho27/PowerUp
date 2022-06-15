using PowerUp.Entities.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class PlayerSearchResults
  {
    public long TotalResults { get; }
    public IEnumerable<PlayerSearchResult> Results { get; }

    public PlayerSearchResults(int totalResults, IEnumerable<LSPlayerSearchResult> results)
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

    public PlayerSearchResult(LSPlayerSearchResult searchResult)
    {
      LSPlayerId = long.Parse(searchResult.player_id!);
      FirstName = searchResult.name_first!;
      FirstNameUsed = searchResult.name_use!;
      LastName = searchResult.name_last!;
      FormalDisplayName = searchResult.name_display_last_first!;
      InformalDisplayName = searchResult.name_display_first_last!;
      Position = LookupServiceValueMapper.MapPosition(searchResult.position_id!);
      BattingSide = LookupServiceValueMapper.MapBatingSide(searchResult.bats!);
      ThrowingArm = LookupServiceValueMapper.MapThrowingArm(searchResult.throws!);
      Weight = string.IsNullOrEmpty(searchResult.weight)
        ? null
        : int.Parse(searchResult.weight);
      HeightFeet = string.IsNullOrEmpty(searchResult.height_feet)
        ? null
        : int.Parse(searchResult.height_feet);
      HeightInches = string.IsNullOrEmpty(searchResult.height_inches) 
        ? null
        : int.Parse(searchResult.height_inches);
      BirthDate = string.IsNullOrEmpty(searchResult.birth_date)
        ? null
        : DateTime.Parse(searchResult.birth_date);
      BirthCountry = string.IsNullOrEmpty(searchResult.birth_country)
        ? null
        : searchResult.birth_country;
      BirthState = string.IsNullOrEmpty(searchResult.birth_state)
        ? null
        : searchResult.birth_state;
      BirthCity = string.IsNullOrEmpty(searchResult.birth_city)
        ? null
        : searchResult.birth_city;
      HighSchool = string.IsNullOrEmpty(searchResult.high_school)
        ? null
        : searchResult.high_school;
      College = string.IsNullOrEmpty(searchResult.college)
        ? null
        : searchResult.college;
      ProDebutDate = string.IsNullOrEmpty(searchResult.pro_debut_date)
        ? null
        : DateTime.Parse(searchResult.pro_debut_date);
      ServiceYears = string.IsNullOrEmpty(searchResult.service_years)
        ? null
        : int.Parse(searchResult.service_years);
      IsActive = searchResult.active_sw == "Y";
      TeamName = string.IsNullOrEmpty(searchResult.team_full)
        ? null
        : searchResult.team_full;
    }
  }
}
