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
      Weight = searchResult.weight.TryParseInt();
      HeightFeet = searchResult.height_feet.TryParseInt();
      HeightInches = searchResult.height_inches.TryParseInt();
      BirthDate = searchResult.birth_date.TryParseDateTime();
      BirthCountry = searchResult.birth_country.StringIfNotEmpty();
      BirthState = searchResult.birth_state.StringIfNotEmpty();
      BirthCity = searchResult.birth_city.StringIfNotEmpty();
      HighSchool = searchResult.high_school.StringIfNotEmpty();
      College = searchResult.college.StringIfNotEmpty();
      ProDebutDate = searchResult.pro_debut_date.TryParseDateTime();
      ServiceYears = searchResult.service_years.TryParseInt();
      IsActive = searchResult.active_sw == "Y";
      TeamName = searchResult.team_full.StringIfNotEmpty();
    }
  }
}
