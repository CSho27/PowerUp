using PowerUp.Entities.Players;
using PowerUp.Fetchers.MLBStatsApi;
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
      LastName = result.name_display_roster!.Contains(",")
        ? result.name_last!
        : result.name_display_roster!;
      FormalDisplayName = result.name_display_last_first!;
      InformalDisplayName = result.name_display_first_last!;
      NickName = result.name_nick.StringIfNotEmpty();
      UniformNumber = result.jersey_number.StringIfNotEmpty();
      Position = LookupServiceValueMapper.MapPosition(result.primary_position!);
      BattingSide = LookupServiceValueMapper.MapBatingSide(result.bats!);
      ThrowingArm = LookupServiceValueMapper.MapThrowingArm(result.throws!);
      Weight = result.weight.TryParseInt();
      HeightFeet = result.height_feet.TryParseInt();
      HeightInches = result.height_inches.TryParseInt();
      BirthDate = result.birth_date.TryParseDateTime();
      BirthCountry = result.birth_country.StringIfNotEmpty();
      BirthState = result.birth_state.StringIfNotEmpty();
      BirthCity = result.birth_city.StringIfNotEmpty();
      DeathDate = result.death_date.TryParseDateTime();
      DeathCountry = result.death_country.StringIfNotEmpty();
      DeathState = result.death_state.StringIfNotEmpty();
      DeathCity = result.death_city.StringIfNotEmpty();
      Age = result.age.TryParseInt();
      HighSchool = result.high_school.StringIfNotEmpty();
      College = result.college.StringIfNotEmpty();
      ProDebutDate = result.pro_debut_date.TryParseDateTime();
      StartDate = result.start_date.TryParseDateTime();
      EndDate = result.end_date.TryParseDateTime();
      ServiceYears = result.service_years.TryParseInt();
      TeamName = result.team_name.StringIfNotEmpty();
    }

    public PlayerInfoResult(Person person)
    {
      LSPlayerId = person.Id;
      // NamePrefix
      FirstName = person.FirstName;
      FirstNameUsed = person.UseName;
      MiddleName = person.MiddleName;
      LastName = LastName ?? person.UseLastName;
      FormalDisplayName = person.LastFirstName;
      InformalDisplayName = person.FirstLastName;
      NickName = person.NickName;
      // UniformNumber
      Position = LookupServiceValueMapper.MapPosition(person.PrimaryPosition?.Code);
      BattingSide = LookupServiceValueMapper.MapBatingSide(person.BatSide?.Code);
      ThrowingArm = LookupServiceValueMapper.MapThrowingArm(person.PitchHand?.Code);
      Weight = person.Weight;
      var parsedHeight = LookupServiceValueMapper.ParseHeight(person.Height);
      HeightFeet = parsedHeight?.heightFeet;
      HeightInches = parsedHeight?.heightInches;
      BirthDate = person.BirthDate;
      BirthCountry = person.BirthCountry;
      BirthState = person.BirthStateProvince;
      BirthCity = person.BirthCity;
      DeathDate = person.DeathDate;
      DeathCountry = person.DeathCountry;
      DeathState = person.DeathStateProvince;
      DeathCity = person.DeathCity;
      Age = person.CurrentAge;
      // HighSchool
      // College
      ProDebutDate = person.MlbDebutDate;
      StartDate = person.MlbDebutDate;
      EndDate = person.LastPlayedDate;
      // ServiceYears
      TeamName = person.CurrentTeam?.Name;
    }
  }
}
