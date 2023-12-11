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

    public FieldingStatsResults(int totalResults, IEnumerable<LSFieldingStatsResult> results)
    {
      TotalResults = totalResults;
      Results = results.Select(r => new FieldingStatsResult(r));
    }

    public FieldingStatsResults(StatElement? stats)
    {
      var validSplits = stats?.Splits.Where(s => s.Team != null).ToList() ?? [];
      validSplits.Reverse();

      TotalResults = validSplits.Count;
      var teamSeqById = new Dictionary<long, int>();
      foreach (var split in validSplits)
        teamSeqById.TryAdd(split.Team!.Id, teamSeqById.Count + 1);

      Results = validSplits
        .Select(r => new FieldingStatsResult(r, teamSeqById[r.Team!.Id]))
        .ToList();
    }
  }

  public class FieldingStatsResult
  {
    public int LSPlayerId { get; }
    public int Year { get; }
    public int TeamSeq { get; }

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

    public FieldingStatsResult(LSFieldingStatsResult result)
    {
      LSPlayerId = int.Parse(result.player_id!);
      Year = int.Parse(result.season!);
      TeamSeq = int.Parse(result.team_seq!);
      Position = LookupServiceValueMapper.MapPosition(result.position!);
      GamesPlayed = result.g.TryParseInt();
      GamesStarted = result.gs.TryParseInt();
      Innings = result.inn.TryParseDouble();
      TotalChances = result.tc.TryParseInt();
      Errors = result.e.TryParseInt();
      Assists = result.a.TryParseInt();
      PutOuts = result.po.TryParseInt();
      DoublePlays = result.dp.TryParseInt();
      RangeFactor = result.rf.TryParseDouble();
      FieldingPercentage = result.fpct.TryParseDouble();
      Catcher_RunnersThrownOut = result.cs.TryParseInt();
      Catcher_StolenBasesAllowed = result.sb.TryParseInt();
      Catcher_PastBalls = result.pb.TryParseInt();
      Catcher_WildPitches = result.cwp.TryParseInt();
    }

    public FieldingStatsResult(Split split, int teamSeq)
    {
      LSPlayerId = (int)split.Player!.Id;
      Year = split.Season.TryParseInt()!.Value;
      TeamSeq = teamSeq;

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
