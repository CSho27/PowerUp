﻿using System;
using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public class PeopleResults
  {
    [JsonPropertyName("copyright")]
    public string Copyright { get; set; } = "";

    [JsonPropertyName("people")]
    public Person[] People { get; set; } = [];
  }

  public class Person
  {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = "";

    [JsonPropertyName("link")]
    public string Link { get; set; } = "";

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = "";

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = "";

    [JsonPropertyName("primaryNumber")]
    public string PrimaryNumber { get; set; } = "";

    [JsonPropertyName("birthDate")]
    public DateTime? BirthDate { get; set; }

    [JsonPropertyName("currentAge")]
    public long CurrentAge { get; set; }

    [JsonPropertyName("birthCity")]
    public string? BirthCity { get; set; }

    [JsonPropertyName("birthStateProvince")]
    public string? BirthStateProvince { get; set; }

    [JsonPropertyName("birthCountry")]
    public string? BirthCountry { get; set; }

    [JsonPropertyName("height")]
    public string? Height { get; set; }

    [JsonPropertyName("weight")]
    public long? Weight { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("currentTeam")]
    public CurrentTeam? CurrentTeam { get; set; }

    [JsonPropertyName("primaryPosition")]
    public PositionInfo? PrimaryPosition { get; set; }

    [JsonPropertyName("useName")]
    public string UseName { get; set; } = "";

    [JsonPropertyName("useLastName")]
    public string UseLastName { get; set; } = "";

    [JsonPropertyName("middleName")]
    public string MiddleName { get; set; } = "";

    [JsonPropertyName("boxscoreName")]
    public string BoxscoreName { get; set; } = "";

    [JsonPropertyName("nickName")]
    public string NickName { get; set; } = "";

    [JsonPropertyName("gender")]
    public string Gender { get; set; } = "";

    [JsonPropertyName("isPlayer")]
    public bool IsPlayer { get; set; }

    [JsonPropertyName("isVerified")]
    public bool IsVerified { get; set; }

    [JsonPropertyName("draftYear")]
    public long DraftYear { get; set; }

    [JsonPropertyName("stats")]
    public StatElement[] Stats { get; set; } = [];

    [JsonPropertyName("mlbDebutDate")]
    public DateTimeOffset MlbDebutDate { get; set; }

    [JsonPropertyName("batSide")]
    public Laterality? BatSide { get; set; }

    [JsonPropertyName("pitchHand")]
    public Laterality? PitchHand { get; set; }

    [JsonPropertyName("nameFirstLast")]
    public string NameFirstLast { get; set; } = "";

    [JsonPropertyName("nameSlug")]
    public string NameSlug { get; set; } = "";

    [JsonPropertyName("firstLastName")]
    public string FirstLastName { get; set; } = "";

    [JsonPropertyName("lastFirstName")]
    public string LastFirstName { get; set; } = "";

    [JsonPropertyName("lastInitName")]
    public string LastInitName { get; set; } = "";

    [JsonPropertyName("initLastName")]
    public string InitLastName { get; set; } = "";

    [JsonPropertyName("fullFMLName")]
    public string FullFmlName { get; set; } = "";

    [JsonPropertyName("fullLFMName")]
    public string FullLfmName { get; set; } = "";

    [JsonPropertyName("strikeZoneTop")]
    public double StrikeZoneTop { get; set; }

    [JsonPropertyName("strikeZoneBottom")]
    public double StrikeZoneBottom { get; set; }
  }

  public class Laterality
  {
    [JsonPropertyName("code")]
    public string Code { get; set; } = "";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";
  }

  public class CurrentTeam
  {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("link")]
    public string Link { get; set; } = "";
  }

  public class PositionInfo
  {
    [JsonPropertyName("code")]
    public string Code { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    [JsonPropertyName("abbreviation")]
    public string Abbreviation { get; set; } = "";
  }

  public class StatElement
  {
    [JsonPropertyName("type")]
    public Group? Type { get; set; }

    [JsonPropertyName("group")]
    public Group? Group { get; set; }

    [JsonPropertyName("exemptions")]
    public object[] Exemptions { get; set; } = [];

    [JsonPropertyName("splits")]
    public Split[] Splits { get; set; } = [];
  }

  public class Group
  {
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }
  }

  public class Split
  {
    [JsonPropertyName("season")]
    public string Season { get; set; } = "";

    [JsonPropertyName("stat")]
    public SplitStat? Stat { get; set; }

    [JsonPropertyName("team")]
    public CurrentTeam? Team { get; set; }

    [JsonPropertyName("player")]
    public PlayerDetails? Player { get; set; }

    [JsonPropertyName("league")]
    public CurrentTeam? League { get; set; }

    [JsonPropertyName("sport")]
    public Sport? Sport { get; set; }

    [JsonPropertyName("gameType")]
    public string GameType { get; set; } = "";

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("position")]
    public PositionInfo? Position { get; set; }
  }

  public class PlayerDetails
  {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = "";

    [JsonPropertyName("link")]
    public string Link { get; set; } = "";
  }

  public class Sport
  {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("link")]
    public string Link { get; set; } = "";

    [JsonPropertyName("abbreviation")]
    public string Abbreviation { get; set; } = "";
  }

  public class SplitStat
  {
    [JsonPropertyName("gamesPlayed")]
    public long GamesPlayed { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("groundOuts")]
    public long? GroundOuts { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("airOuts")]
    public long? AirOuts { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("runs")]
    public long? Runs { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("doubles")]
    public long? Doubles { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("triples")]
    public long? Triples { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("homeRuns")]
    public long? HomeRuns { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("strikeOuts")]
    public long? StrikeOuts { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("baseOnBalls")]
    public long? BaseOnBalls { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("intentionalWalks")]
    public long? IntentionalWalks { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("hits")]
    public long? Hits { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("hitByPitch")]
    public long? HitByPitch { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("avg")]
    public string? Avg { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("atBats")]
    public long? AtBats { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("obp")]
    public string? Obp { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("slg")]
    public string? Slg { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("ops")]
    public string? Ops { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("caughtStealing")]
    public long? CaughtStealing { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("stolenBases")]
    public long? StolenBases { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("stolenBasePercentage")]
    public string? StolenBasePercentage { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("groundIntoDoublePlay")]
    public long? GroundIntoDoublePlay { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("numberOfPitches")]
    public long? NumberOfPitches { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("plateAppearances")]
    public long? PlateAppearances { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("totalBases")]
    public long? TotalBases { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("rbi")]
    public long? Rbi { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("leftOnBase")]
    public long? LeftOnBase { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("sacBunts")]
    public long? SacBunts { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("sacFlies")]
    public long? SacFlies { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("babip")]
    public string? Babip { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("groundOutsToAirouts")]
    public string? GroundOutsToAirouts { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("catchersInterference")]
    public long? CatchersInterference { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("atBatsPerHomeRun")]
    public string? AtBatsPerHomeRun { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("gamesStarted")]
    public long? GamesStarted { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("assists")]
    public long? Assists { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("putOuts")]
    public long? PutOuts { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("errors")]
    public long? Errors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("chances")]
    public long? Chances { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("fielding")]
    public string? Fielding { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("position")]
    public PositionInfo? Position { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("rangeFactorPerGame")]
    public string? RangeFactorPerGame { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("rangeFactorPer9Inn")]
    public string? RangeFactorPer9Inn { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("innings")]
    public string? Innings { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("games")]
    public long? Games { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("doublePlays")]
    public long? DoublePlays { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("triplePlays")]
    public long? TriplePlays { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("throwingErrors")]
    public long? ThrowingErrors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("passedBall")]
    public long? PassedBalls { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("wildPitches")]
    public long? WildPitches { get; set; }
  }
}