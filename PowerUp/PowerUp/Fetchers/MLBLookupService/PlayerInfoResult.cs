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
      NamePrefix = string.IsNullOrEmpty(result.name_prefix)
        ? null
        : result.name_prefix;
      FirstName = result.name_first!;
      FirstNameUsed = result.name_use!;
      MiddleName = string.IsNullOrEmpty(result.name_middle)
        ? null
        : result.name_middle;
      LastName = result.name_last!;
      FormalDisplayName = result.name_display_last_first!;
      InformalDisplayName = result.name_display_first_last!;
      NickName = string.IsNullOrEmpty(result.name_nick)
        ? null
        : result.name_nick;
      UniformNumber = string.IsNullOrEmpty(result.jersey_number)
        ? null
        : result.jersey_number;
      Position = LookupServiceValueMapper.MapPosition(result.primary_position!);
      BattingSide = LookupServiceValueMapper.MapBatingSide(result.bats!);
      ThrowingArm = LookupServiceValueMapper.MapThrowingArm(result.throws!);
      Weight = string.IsNullOrEmpty(result.weight)
        ? null
        : int.Parse(result.weight);
      HeightFeet = string.IsNullOrEmpty(result.height_feet)
        ? null
        : int.Parse(result.height_feet);
      HeightInches = string.IsNullOrEmpty(result.height_inches)
        ? null
        : int.Parse(result.height_inches);
      BirthDate = string.IsNullOrEmpty(result.birth_date)
        ? null
        : DateTime.Parse(result.birth_date);
      BirthCountry = string.IsNullOrEmpty(result.birth_country)
        ? null
        : result.birth_country;
      BirthState = string.IsNullOrEmpty(result.birth_state)
        ? null
        : result.birth_state;
      BirthCity = string.IsNullOrEmpty(result.birth_city)
        ? null
        : result.birth_city;
      DeathDate = string.IsNullOrEmpty(result.death_date)
        ? null
        : DateTime.Parse(result.death_date);
      DeathCountry = string.IsNullOrEmpty(result.death_country)
        ? null
        : result.death_country;
      DeathState = string.IsNullOrEmpty(result.death_state)
        ? null
        : result.death_state;
      DeathCity = string.IsNullOrEmpty(result.death_city)
        ? null
        : result.death_city;
      Age = string.IsNullOrEmpty(result.age)
        ? null
        : int.Parse(result.age);
      HighSchool = string.IsNullOrEmpty(result.high_school)
        ? null
        : result.high_school;
      College = string.IsNullOrEmpty(result.college)
        ? null
        : result.college;
      ProDebutDate = string.IsNullOrEmpty(result.pro_debut_date)
        ? null
        : DateTime.Parse(result.pro_debut_date);
      StartDate = string.IsNullOrEmpty(result.start_date)
        ? null
        : DateTime.Parse(result.start_date);
      EndDate = string.IsNullOrEmpty(result.end_date)
        ? null
        : DateTime.Parse(result.end_date);
      ServiceYears = string.IsNullOrEmpty(result.service_years)
        ? null
        : int.Parse(result.service_years);
      TeamName = string.IsNullOrEmpty(result.team_name)
        ? null
        : result.team_name;
    }
  }
}
