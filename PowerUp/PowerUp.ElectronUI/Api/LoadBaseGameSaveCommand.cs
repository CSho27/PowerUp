using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Teams;
using PowerUp.GameSave.Api;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api
{
  public class LoadBaseGameSaveCommand : ICommand<LoadBaseRequest, RosterDetails>
  {
    private readonly IBaseGameSavePathProvider _baseGameSavePathProvider;
    private readonly IRosterImportApi _rosterImportApi;

    public LoadBaseGameSaveCommand(
      IBaseGameSavePathProvider gameSavePathProvider,
      IRosterImportApi rosterImportApi
    )
    {
      _baseGameSavePathProvider = gameSavePathProvider;
      _rosterImportApi = rosterImportApi;
    }

    public RosterDetails Execute(LoadBaseRequest request)
    {
      var parameters = new RosterImportParameters
      {
        FilePath = _baseGameSavePathProvider.GetPath(),
        IsBase = true
      };
      var result = _rosterImportApi.ImportRoster(parameters);
      return RosterDetails.FromRosterTeamsAndPlayers(result.Roster!, result.Teams, result.Players);
    }
  }

  public class LoadBaseRequest { }

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
    public string Name { get; set; }
    public string PowerProsName { get; set; }
    public IEnumerable<HitterDetails> Hitters { get; set; }
    public IEnumerable<PitcherDetails> Pitchers { get; set; }
    public int Overall { get; set; }

    public TeamDetails(string name, string powerProsName, IEnumerable<HitterDetails> hitters, IEnumerable<PitcherDetails> pitchers, int overall)
    {
      Name = name;
      PowerProsName = powerProsName;
      Hitters = hitters;
      Pitchers = pitchers;
      Overall = overall;
    }

    public static TeamDetails FromRosterTeamAndPlayers(Roster roster, Team team, IEnumerable<Player> allPlayers)
    {
      var powerProsTeamName = roster.TeamKeysByPPTeam.Single(m => m.Value == team.GetKey()).Key.GetFullDisplayName();
      var playersOnTeam = team.PlayerDefinitions.Select(pd => allPlayers.Single(p => pd.PlayerKey == p.GetKey())).ToList();
      var hitters = playersOnTeam.Where(p => p.PrimaryPosition != Position.Pitcher).Select(HitterDetails.FromPlayer);
      var pitchers = playersOnTeam.Where(p => p.PrimaryPosition == Position.Pitcher).Select(PitcherDetails.FromPlayer);

      return new TeamDetails(team.Name, powerProsTeamName, hitters, pitchers, 0);
    }
  }

  public class PlayerDetails
  {
    public string SavedName { get; set; }
    public string UniformNumber { get; set; }
    public PositionType PositionType { get; set; }
    public string Position { get; set; }
    public int Overall { get; set; }
    public string BatsAndThrows { get; set; }

    public PlayerDetails(
      string savedName, 
      string uniformNumber,
      PositionType positionType,
      string position,
      int overall,
      string batsAndThrows
    )
    {
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
        savedName: player.SavedName,
        uniformNumber: player.UniformNumber,
        positionType: player.PrimaryPosition.GetPositionType(),
        position: player.PrimaryPosition.GetAbbrev(),
        overall: 0,
        batsAndThrows: $"{player.BattingSide.GetAbbrev()}/{player.ThrowingSide.GetAbbrev()}"
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
    ) :base(savedName, uniformNumber, positionType, position, overall, batsAndThrows)
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
    public int TopSpeed { get; set; }
    public int Control { get; set; }
    public int Stamina { get; set; }
    public string? BreakingBall1 { get; set; }
    public string? BreakingBall2 { get; set; }
    public string? BreakingBall3 { get; set; }

    public PitcherDetails(
      string savedName,
      string uniformNumber,
      PositionType positionType,
      string position,
      int overall,
      string batsAndThrows,
      int topSpeed,
      int control,
      int stamina,
      string? breakingBall1,
      string? breakingBall2,
      string? breakingBall3
    ) : base(savedName, uniformNumber, positionType, position, overall, batsAndThrows)
    {
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
        .Select(t => $"{t.type}-{t.movement}")
        .ToList();

      return new PitcherDetails(
        savedName: playerDetails.SavedName,
        uniformNumber: playerDetails.UniformNumber,
        positionType: playerDetails.PositionType,
        position: playerDetails.Position,
        overall: playerDetails.Overall,
        batsAndThrows: playerDetails.BatsAndThrows,
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
