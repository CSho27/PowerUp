using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Teams;
using PowerUp.Generators;
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
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; set; }
    public bool CanEdit => SourceType.CanEdit();
    public int RosterId { get; set; }
    public string Name { get; set; }
    public IEnumerable<TeamDetails> Teams { get; set; }
    public IEnumerable<HitterDetails> FreeAgentHitters { get; set; }
    public IEnumerable<PitcherDetails> FreeAgentPitchers { get; set; }

    public RosterDetails(EntitySourceType sourceType, int rosterId, string name, IEnumerable<TeamDetails> teams, IEnumerable<HitterDetails> freeAgentHitters, IEnumerable<PitcherDetails> freeAgentPitchers)
    {
      SourceType = sourceType;
      RosterId = rosterId;
      Name = name;
      Teams = teams;
      FreeAgentHitters = freeAgentHitters;
      FreeAgentPitchers = freeAgentPitchers;
    }

    public static RosterDetails FromRoster(Roster roster)
    {
      var teams = roster.GetTeams().Select(kvp => TeamDetails.FromTeam(kvp.Key, kvp.Value));
      var freeAgents = roster.GetFreeAgentPlayers().ToList();

      var hitters = freeAgents.Where(p => p.PrimaryPosition != Position.Pitcher).OrderByDescending(p => p.Overall).Select(HitterDetails.FromPlayer);
      var pitchers = freeAgents.Where(p => p.PrimaryPosition == Position.Pitcher).OrderByDescending(p => p.Overall).Select(PitcherDetails.FromPlayer);
      return new RosterDetails(roster.SourceType, roster.Id!.Value, roster.Name, teams, hitters, pitchers);
    }
  }

  public class TeamDetails
  {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; set; }
    public bool CanEdit => SourceType.CanEdit();
    public int TeamId { get; set; }
    public string Name { get; set; }
    public string PowerProsName { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MLBPPTeam PowerProsTeam { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MLBPPDivision Division { get; set; }
    public IEnumerable<HitterDetails> Hitters { get; set; }
    public IEnumerable<PitcherDetails> Pitchers { get; set; }
    public int HittingRating { get; set; }
    public int PitchingRating { get; set; }
    public int OverallRating { get; set; }

    public TeamDetails(
      EntitySourceType sourceType, 
      int id, 
      string name, 
      MLBPPTeam ppTeam, 
      IEnumerable<HitterDetails> hitters,
      IEnumerable<PitcherDetails> pitchers, 
      int hittingRating,
      int pitchingRating,
      int overallRating
    )
    {
      SourceType = sourceType;
      TeamId = id;
      Name = name;
      PowerProsName = ppTeam.GetFullDisplayName();
      PowerProsTeam = ppTeam;
      Division = ppTeam.GetDivision();
      Hitters = hitters;
      Pitchers = pitchers;
      HittingRating = hittingRating;
      PitchingRating = pitchingRating;
      OverallRating = overallRating;
    }

    public static TeamDetails FromTeam(Team team, MLBPPTeam ppTeam)
    {
      var playersOnTeam = team.GetPlayers();
      var hitters = playersOnTeam.Where(p => p.PrimaryPosition != Position.Pitcher).OrderByDescending(p => p.Overall).Select(HitterDetails.FromPlayer);
      var pitchers = playersOnTeam.Where(p => p.PrimaryPosition == Position.Pitcher).OrderByDescending(p => p.Overall).Select(PitcherDetails.FromPlayer);

      return new TeamDetails(
        team.SourceType, 
        team.Id!.Value, 
        team.Name, 
        ppTeam, 
        hitters, 
        pitchers, 
        team.GetHittingRating().RoundDown(),
        team.GetPitchingRating().RoundDown(),
        team.GetOverallRating().RoundDown()
      );
    }
  }

  public class PlayerDetails
  {
    public int PlayerId { get; set; }
    public string SavedName { get; set; }
    public string UniformNumber { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Position Position { get; set; }
    public string PositionAbbreviation { get; set; }
    public int Overall { get; set; }
    public string BatsAndThrows { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; set; }
    public bool CanEdit => SourceType.CanEdit();
    public IEnumerable<GeneratorWarning> GeneratedPlayer_Warnings { get; set; }
    public bool GeneratedPlayer_IsUnedited { get; set; }

    public PlayerDetails(
      int id,
      string savedName,
      string uniformNumber,
      Position position,
      string positionAbbreviation,
      int overall,
      string batsAndThrows,
      EntitySourceType sourceType,
      IEnumerable<GeneratorWarning> generatorWarnings,
      bool isUneditedGeneratedPlayer
    )
    {
      PlayerId = id;
      SavedName = savedName;
      UniformNumber = uniformNumber;
      Position = position;
      PositionAbbreviation = positionAbbreviation;
      Overall = overall;
      BatsAndThrows = batsAndThrows;
      SourceType = sourceType;
      GeneratedPlayer_Warnings = generatorWarnings;
      GeneratedPlayer_IsUnedited = isUneditedGeneratedPlayer;
    }

    public static PlayerDetails FromPlayer(Player player)
    {
      return new PlayerDetails(
        id: player.Id!.Value,
        savedName: player.SavedName,
        uniformNumber: player.UniformNumber,
        position: player.PrimaryPosition,
        positionAbbreviation: player.PrimaryPosition.GetAbbrev(),
        overall: player.Overall.RoundDown(),
        batsAndThrows: player.BatsAndThrows,
        sourceType: player.SourceType,
        generatorWarnings: player.GeneratorWarnings,
        isUneditedGeneratedPlayer: player.GeneratedPlayer_IsUnedited
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
      int id,
      string savedName,
      string uniformNumber,
      Position position,
      string positionAbbreviation,
      int overall,
      string batsAndThrows,
      EntitySourceType sourceType,
      IEnumerable<GeneratorWarning> generatorWarnings,
      bool isUneditedGeneratedPlayer,
      int trajectory,
      int contact,
      int power,
      int runSpeed,
      int armStrength,
      int fielding,
      int errorResistance
    ) : base(id, savedName, uniformNumber, position, positionAbbreviation, overall, batsAndThrows, sourceType, generatorWarnings, isUneditedGeneratedPlayer)
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
        id: playerDetails.PlayerId,
        savedName: playerDetails.SavedName,
        uniformNumber: playerDetails.UniformNumber,
        position: playerDetails.Position,
        positionAbbreviation: playerDetails.PositionAbbreviation,
        overall: playerDetails.Overall,
        batsAndThrows: playerDetails.BatsAndThrows,
        sourceType: playerDetails.SourceType,
        generatorWarnings: playerDetails.GeneratedPlayer_Warnings,
        isUneditedGeneratedPlayer: playerDetails.GeneratedPlayer_IsUnedited,
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
      int id,
      string savedName,
      string uniformNumber,
      Position position,
      string positionAbbreviation,
      int overall,
      string batsAndThrows,
      EntitySourceType sourceType,
      IEnumerable<GeneratorWarning> generatorWarnings,
      bool isUneditedGeneratedPlayer,
      string pitcherType,
      int topSpeed,
      int control,
      int stamina,
      string? breakingBall1,
      string? breakingBall2,
      string? breakingBall3
    ) : base(id, savedName, uniformNumber, position, positionAbbreviation, overall, batsAndThrows, sourceType, generatorWarnings, isUneditedGeneratedPlayer)
    {
      PitcherType = pitcherType;
      TopSpeed = topSpeed;
      Control = control;
      Stamina = stamina;
      BreakingBall1 = breakingBall1;
      BreakingBall2 = breakingBall2;
      BreakingBall3 = breakingBall3;
      SourceType = sourceType;
    }

    public static new PitcherDetails FromPlayer(Player player)
    {
      var playerDetails = PlayerDetails.FromPlayer(player);
      var abilities = player.PitcherAbilities;
      var sortedArsenal = abilities
        .GetSortedArsenal()
        .Select(t => $"{t.type} ({t.movement})");
      
      return new PitcherDetails(
        id: playerDetails.PlayerId,
        savedName: playerDetails.SavedName,
        uniformNumber: playerDetails.UniformNumber,
        position: playerDetails.Position,
        positionAbbreviation: playerDetails.PositionAbbreviation,
        overall: playerDetails.Overall,
        batsAndThrows: playerDetails.BatsAndThrows,
        sourceType: playerDetails.SourceType,
        generatorWarnings: playerDetails.GeneratedPlayer_Warnings,
        isUneditedGeneratedPlayer: playerDetails.GeneratedPlayer_IsUnedited,
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

