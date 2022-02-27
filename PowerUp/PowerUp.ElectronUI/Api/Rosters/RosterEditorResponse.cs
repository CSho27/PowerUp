﻿using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Teams;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class RosterEditorResponse
  {
    public IEnumerable<KeyedCode> DivisionOptions => EnumExtensions.GetKeyedCodeList<MLBPPDivision>();
    public RosterDetails RosterDetails { get; set; }

    public RosterEditorResponse(RosterDetails details)
    {
      RosterDetails = details; ;
    }
  }

  public class RosterDetails
  {
    public string Name { get; set; }
    public IEnumerable<TeamDetails> Teams { get; set; }

    public RosterDetails(string name, IEnumerable<TeamDetails> teams)
    {
      Name = name;
      Teams = teams;
    }

    public static RosterDetails FromRosterTeamsAndPlayers(Roster roster, IEnumerable<Team> allTeams, IEnumerable<Player> allPlayers)
    {
      var teams = allTeams.Select(t => TeamDetails.FromRosterTeamAndPlayers(roster, t, allPlayers));
      return new RosterDetails(roster!.Name, teams);
    }
  }

  public class TeamDetails
  {
    public string TeamKey { get; set; }
    public string Name { get; set; }
    public string PowerProsName { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MLBPPDivision Division { get; set; }
    public IEnumerable<HitterDetails> Hitters { get; set; }
    public IEnumerable<PitcherDetails> Pitchers { get; set; }
    public int Overall { get; set; }

    public TeamDetails(string key, string name, string powerProsName, MLBPPDivision division, IEnumerable<HitterDetails> hitters, IEnumerable<PitcherDetails> pitchers, int overall)
    {
      TeamKey = key;
      Name = name;
      PowerProsName = powerProsName;
      Division = division;
      Hitters = hitters;
      Pitchers = pitchers;
      Overall = overall;
    }

    public static TeamDetails FromRosterTeamAndPlayers(Roster roster, Team team, IEnumerable<Player> allPlayers)
    {
      var ppTeam = roster.TeamKeysByPPTeam.Single(m => m.Value == team.GetKey()).Key;
      var playersOnTeam = team.PlayerDefinitions.Select(pd => allPlayers.Single(p => pd.PlayerKey == p.GetKey())).ToList();
      var hitters = playersOnTeam.Where(p => p.PrimaryPosition != Position.Pitcher).Select(HitterDetails.FromPlayer);
      var pitchers = playersOnTeam.Where(p => p.PrimaryPosition == Position.Pitcher).Select(PitcherDetails.FromPlayer);

      return new TeamDetails(team.GetKey(), team.Name, ppTeam.GetFullDisplayName(), ppTeam.GetDivision(), hitters, pitchers, 0);
    }
  }

  public class PlayerDetails
  {
    public string PlayerKey { get; set; }
    public string SavedName { get; set; }
    public string UniformNumber { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PositionType PositionType { get; set; }
    public string Position { get; set; }
    public int Overall { get; set; }
    public string BatsAndThrows { get; set; }

    public PlayerDetails(
      string key,
      string savedName,
      string uniformNumber,
      PositionType positionType,
      string position,
      int overall,
      string batsAndThrows
    )
    {
      PlayerKey = key;
      SavedName = savedName;
      UniformNumber = uniformNumber;
      PositionType = positionType;
      Position = position;
      Overall = overall;
      BatsAndThrows = batsAndThrows;
    }

    public static PlayerDetails FromPlayer(Player player)
    {
      return new PlayerDetails(
        key: player.GetKey(),
        savedName: player.SavedName,
        uniformNumber: player.UniformNumber,
        positionType: player.PrimaryPosition.GetPositionType(),
        position: player.PrimaryPosition.GetAbbrev(),
        overall: player.GetOverallRating().RoundDown(),
        batsAndThrows: $"{player.BattingSide.GetAbbrev()}/{player.ThrowingArm.GetAbbrev()}"
      );
    }
  }

  public class HitterDetails : PlayerDetails
  {
    public int Trajectory { get; set; }
    public int Contact { get; set; }
    public int Power { get; set; }
    public int RunSpeed { get; set; }
    public int ArmStrength { get; set; }
    public int Fielding { get; set; }
    public int ErrorResistance { get; set; }

    public HitterDetails(
      string key,
      string savedName,
      string uniformNumber,
      PositionType positionType,
      string position,
      int overall,
      string batsAndThrows,
      int trajectory,
      int contact,
      int power,
      int runSpeed,
      int armStrength,
      int fielding,
      int errorResistance
    ) : base(key, savedName, uniformNumber, positionType, position, overall, batsAndThrows)
    {
      Trajectory = trajectory;
      Contact = contact;
      Power = power;
      RunSpeed = runSpeed;
      ArmStrength = armStrength;
      Fielding = fielding;
      ErrorResistance = errorResistance;
    }

    public static new HitterDetails FromPlayer(Player player)
    {
      var playerDetails = PlayerDetails.FromPlayer(player);
      var hitterAbilities = player.HitterAbilities;

      return new HitterDetails(
        key: playerDetails.PlayerKey,
        savedName: playerDetails.SavedName,
        uniformNumber: playerDetails.UniformNumber,
        positionType: playerDetails.PositionType,
        position: playerDetails.Position,
        overall: playerDetails.Overall,
        batsAndThrows: playerDetails.BatsAndThrows,
        trajectory: hitterAbilities.Trajectory,
        contact: hitterAbilities.Contact,
        power: hitterAbilities.Power,
        runSpeed: hitterAbilities.RunSpeed,
        armStrength: hitterAbilities.ArmStrength,
        fielding: hitterAbilities.Fielding,
        errorResistance: hitterAbilities.ErrorResistance
      );
    }
  }

  public class PitcherDetails : PlayerDetails
  {
    public string PitcherType { get; set; }
    public int TopSpeed { get; set; }
    public int Control { get; set; }
    public int Stamina { get; set; }
    public string? BreakingBall1 { get; set; }
    public string? BreakingBall2 { get; set; }
    public string? BreakingBall3 { get; set; }

    public PitcherDetails(
      string key,
      string savedName,
      string uniformNumber,
      PositionType positionType,
      string position,
      int overall,
      string batsAndThrows,
      string pitcherType,
      int topSpeed,
      int control,
      int stamina,
      string? breakingBall1,
      string? breakingBall2,
      string? breakingBall3
    ) : base(key, savedName, uniformNumber, positionType, position, overall, batsAndThrows)
    {
      PitcherType = pitcherType;
      TopSpeed = topSpeed;
      Control = control;
      Stamina = stamina;
      BreakingBall1 = breakingBall1;
      BreakingBall2 = breakingBall2;
      BreakingBall3 = breakingBall3;
    }

    public static new PitcherDetails FromPlayer(Player player)
    {
      var playerDetails = PlayerDetails.FromPlayer(player);
      var abilities = player.PitcherAbilities;

      var pitchArsenal = new (string? type, int? movement)[]
      {
        (abilities.HasTwoSeam ? "2SFB" : null, abilities.TwoSeamMovement),
        (abilities.Slider1Type?.GetAbbrev(), abilities.Slider1Movement),
        (abilities.Slider2Type?.GetAbbrev(), abilities.Slider2Movement),
        (abilities.Curve1Type?.GetAbbrev(), abilities.Curve1Movement),
        (abilities.Curve2Type?.GetAbbrev(), abilities.Curve2Movement),
        (abilities.Fork1Type?.GetAbbrev(), abilities.Fork1Movement),
        (abilities.Fork2Type?.GetAbbrev(), abilities.Fork2Movement),
        (abilities.Sinker1Type?.GetAbbrev(), abilities.Sinker1Movement),
        (abilities.Sinker2Type?.GetAbbrev(), abilities.Sinker2Movement),
        (abilities.SinkingFastball1Type?.GetAbbrev(), abilities.SinkingFastball1Movement),
        (abilities.SinkingFastball2Type?.GetAbbrev(), abilities.SinkingFastball2Movement)
      };

      var sortedArsenal = pitchArsenal
        .Where(t => t.type != null)
        .OrderByDescending(t => t.movement)
        .Select(t => $"{t.type} ({t.movement})")
        .ToList();

      return new PitcherDetails(
        key: playerDetails.PlayerKey,
        savedName: playerDetails.SavedName,
        uniformNumber: playerDetails.UniformNumber,
        positionType: playerDetails.PositionType,
        position: playerDetails.Position,
        overall: playerDetails.Overall,
        batsAndThrows: playerDetails.BatsAndThrows,
        pitcherType: player.PitcherType.GetAbbrev(),
        topSpeed: abilities.TopSpeedMph.RoundDown(),
        control: abilities.Control,
        stamina: abilities.Stamina,
        breakingBall1: sortedArsenal.FirstOrDefault(),
        breakingBall2: sortedArsenal.ElementAtOrDefault(1),
        breakingBall3: sortedArsenal.ElementAtOrDefault(2)
      );
    }
  }
}
