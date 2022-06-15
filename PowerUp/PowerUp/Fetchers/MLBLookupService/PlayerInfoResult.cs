using PowerUp.Entities.Players;
using System;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class PlayerInfoResult
  {
    public long LSPlayerId { get; }
    public Position Position { get; }
    public string? NamePrefix { get; }
    public string FirstName { get; }
    public string FirstNameUsed { get; }
    public string? MiddleName { get; }
    public string LastName { get; }
    public string FormalDisplayName { get; }
    public string InformalDisplayName { get; }
    public string? NickName { get; }
    public string? UniformNumber { get; }
    public BattingSide BattingSide { get; }
    public ThrowingArm ThrowingArm { get; }
    public int? Weight { get; }
    public int? HeightFeet { get; }
    public int? HeightInches { get; }
    public DateTime? BirthDate { get; }
    public string? BirthCountry { get; }
    public string? BirthState { get; }
    public string? BirthCity { get; }
    public DateTime? DeathDate { get; }
    public string? DeathCountry { get; }
    public string? DeathState { get; }
    public string? DeathCity { get; }
    public int? Age { get; }
    public string? HighSchool { get; }
    public string? College { get; }
    public DateTime? ProDebutDate { get; }
    public DateTime? StartDate { get; }
    public DateTime? EndDate { get; }
    public int? ServiceYears { get; }
    public string? TeamName { get; }

    public PlayerInfoResult(LSPlayerInfoResult result)
    {
      LSPlayerId = long.Parse(result.player_id!);
      NamePrefix = result.name_prefix.StringIfNotEmpty();
      FirstName = result.name_first!;
      FirstNameUsed = result.name_use!;
      MiddleName = result.name_middle.StringIfNotEmpty();
      LastName = result.name_last!;
      FormalDisplayName = result.name_display_last_first!;
      InformalDisplayName = result.name_display_first_last!;
      NickName = result.name_nick.StringIfNotEmpty();
      UniformNumber = result.jersey_number.StringIfNotEmpty();
      Position = LookupServiceValueMapper.MapPosition(result.primary_position!);
      BattingSide = LookupServiceValueMapper.MapBatingSide(result.bats!);
      ThrowingArm = LookupServiceValueMapper.MapThrowingArm(result.throws!);
      Weight = result.weight.ParseIntIfNotEmpty();
      HeightFeet = result.height_feet.ParseIntIfNotEmpty();
      HeightInches = result.height_inches.ParseIntIfNotEmpty();
      BirthDate = result.birth_date.ParseDateTimeIfNotEmpty();
      BirthCountry = result.birth_country.StringIfNotEmpty();
      BirthState = result.birth_state.StringIfNotEmpty();
      BirthCity = result.birth_city.StringIfNotEmpty();
      DeathDate = result.death_date.ParseDateTimeIfNotEmpty();
      DeathCountry = result.death_country.StringIfNotEmpty();
      DeathState = result.death_state.StringIfNotEmpty();
      DeathCity = result.death_city.StringIfNotEmpty();
      Age = result.age.ParseIntIfNotEmpty();
      HighSchool = result.high_school.StringIfNotEmpty();
      College = result.college.StringIfNotEmpty();
      ProDebutDate = result.pro_debut_date.ParseDateTimeIfNotEmpty();
      StartDate = result.start_date.ParseDateTimeIfNotEmpty();
      EndDate = result.end_date.ParseDateTimeIfNotEmpty();
      ServiceYears = result.service_years.ParseIntIfNotEmpty();
      TeamName = result.team_name.StringIfNotEmpty();
    }
  }
}
