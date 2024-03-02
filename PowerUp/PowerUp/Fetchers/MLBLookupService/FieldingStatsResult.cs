using PowerUp.Entities.Players;
using PowerUp.Fetchers.MLBStatsApi;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class FieldingStatsResults
  {
    public int TotalResults { get; }
    public IEnumerable<FieldingStatsResult> Results { get; }

    public FieldingStatsResults(StatElement? stats)
    {
      var validSplits = stats?.Splits.Where(s => s.Team != null).ToList() ?? [];
      TotalResults = validSplits.Count;
      Results = validSplits
        .Select(r => new FieldingStatsResult(r))
        .ToList();
    }
  }

  public class FieldingStatsResult
  {
    public int LSPlayerId { get; }
    public int Year { get; }
    public int LSTeamId { get; }

    public Position Position { get; }
    public int? GamesPlayed { get; }
    public int? GamesStarted { get; }
    public double? Innings { get; }
    public int? TotalChances { get; }
    public int? Errors { get; }
    public int? Assists { get; }
    public int? PutOuts { get; }
    public int? DoublePlays { get; }
    public double? RangeFactor { get; }
    public double? FieldingPercentage { get; }
    public int? Catcher_RunnersThrownOut { get; }
    public int? Catcher_StolenBasesAllowed { get; }
    public int? Catcher_PastBalls { get; }
    public int? Catcher_WildPitches { get; }

    public FieldingStatsResult(Split split)
    {
      LSPlayerId = (int)split.Player!.Id;
      Year = split.Season.TryParseInt()!.Value;
      LSTeamId = (int)split.Team!.Id;

      var positionCode = split.Position?.Code.TryParseInt();
      Position = positionCode.HasValue
        ? (Position)positionCode
        : Position.DesignatedHitter;

      GamesPlayed = (int?)split.Stat?.GamesPlayed;
      GamesStarted = (int?)split.Stat?.GamesStarted;
      Innings = split.Stat?.Innings.TryParseDouble();
      TotalChances = (int?)split.Stat?.Chances;
      Errors = (int?)split.Stat?.Errors;
      Assists = (int?)split.Stat?.Assists;
      PutOuts = (int?)split.Stat?.PutOuts;
      DoublePlays = (int?)split.Stat?.DoublePlays;
      RangeFactor = split.Stat?.RangeFactorPerGame.TryParseDouble();
      FieldingPercentage = split.Stat?.Fielding.TryParseDouble();
      Catcher_RunnersThrownOut = (int?)split.Stat?.CaughtStealing;
      Catcher_StolenBasesAllowed = (int?)split.Stat?.StolenBases;
      Catcher_PastBalls = (int?)split.Stat?.PassedBalls;
      Catcher_WildPitches = (int?)split.Stat?.WildPitches;
    }
  }
}
