using PowerUp.Entities.Players;
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
  }
}
