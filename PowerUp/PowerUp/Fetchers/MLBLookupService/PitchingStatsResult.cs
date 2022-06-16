using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class PitchingStatsResults
  {
    public long TotalResults { get; }
    public IEnumerable<PitchingStatsResult> Results { get; }

    public PitchingStatsResults(int totalSize, IEnumerable<LSPitchingStatsResult> results)
    {
      TotalResults = totalSize;
      Results = results.Select(r => new PitchingStatsResult(r));
    }
  }

  public class PitchingStatsResult
  {
    public int LSPlayerId { get; }
    public int Year { get; }
    public int TeamSeq { get; }

    public int? GamesPlayed { get; }
    public int? GamesStarted { get; }
    public int? GamesFinished { get; }
    public int? CompleteGames { get; }
    public int? ShutOuts { get; }
    public int? Wins { get; }
    public int? Losses { get; }
    public int? QualityStarts { get; }
    public int? Saves { get; }
    public double? InningsPitched { get; }
    public int? SaveOpportunities { get; }
    public int? AtBats { get; }
    public int? Hits { get; }
    public int? Walks { get; }
    public int? IntentionalWalks { get; }
    public int? HitBatters { get; }
    public int? Strikeouts { get; }
    public int? Runs { get; }
    public int? EarnedRuns { get; }
    public int? Doubles { get; }
    public int? Triples { get; }
    public int? HomeRuns { get; }
    public int? TotalBasesAllowed { get; }
    public int? WildPitches { get; }
    public int? Balks { get; }
    public int? RunnersPickedOff { get; }
    public int? NumberOfPitches { get; }
    public int? Strikes { get; }
    public int? GroundOuts { get; }
    public int? AirOuts { get; }
    public int? DoublePlays { get; }
    public double? StrikeoutToWalkRatio { get; }
    public double? WHIP { get; }
    public double? HitsPer9 { get; }
    public double? HomeRunsPer9 { get; }
    public double? RunScoredPer9 { get; }
    public double? WalksPer9 { get; }
    public double? StrikeoutsPer9 { get; }
    public double? BattingAverageAgainst { get; }
    public double? SluggingAgainst { get; }
    public double? OnBasePercentageAgainst { get; }
    public double? OnBasePlusSluggingAgainst { get; }
    public double? WinningPercentage { get; }
    public double? EarnedRunAverage { get; }
    public double? PitchesPerPlateAppearance { get; }
    public double? StrikePercentage { get; }

    public PitchingStatsResult(LSPitchingStatsResult result)
    {
      LSPlayerId = int.Parse(result.player_id!);
      Year = int.Parse(result.season!);
      TeamSeq = (int)double.Parse(result.team_seq!);
      GamesPlayed = int.Parse(result.g!);
      GamesStarted = result.gs.TryParseInt();
      GamesFinished = result.gf.TryParseInt();
      CompleteGames = result.cg.TryParseInt();
      ShutOuts = result.sho.TryParseInt();
      Wins = result.w.TryParseInt();
      Losses = result.l.TryParseInt();
      QualityStarts = result.qs.TryParseInt();
      Saves = result.sv.TryParseInt();
      InningsPitched = result.ip.TryParseDouble();
      SaveOpportunities = result.svo.TryParseInt();
      AtBats = result.ab.TryParseInt();
      Hits = result.h.TryParseInt();
      Walks = result.bb.TryParseInt();
      IntentionalWalks = result.ibb.TryParseInt();
      HitBatters = result.hb.TryParseInt();
      Strikeouts = result.so.TryParseInt();
      Runs = result.r.TryParseInt();
      EarnedRuns = result.er.TryParseInt();
      Doubles = result.db.TryParseInt();
      Triples = result.tr.TryParseInt();
      HomeRuns = result.hr.TryParseInt();
      TotalBasesAllowed = result.tbf.TryParseInt();
      WildPitches = result.wp.TryParseInt();
      Balks = result.bk.TryParseInt();
      RunnersPickedOff = result.pk.TryParseInt();
      NumberOfPitches = result.np.TryParseInt();
      Strikes = result.s.TryParseInt();
      GroundOuts = result.go.TryParseInt();
      AirOuts = result.ao.TryParseInt();
      DoublePlays = result.gidp_opp.TryParseInt();
      StrikeoutToWalkRatio = result.kbb.TryParseDouble();
      WHIP = result.whip.TryParseDouble();
      HitsPer9 = result.h9.TryParseDouble();
      HomeRunsPer9 = result.hr9.TryParseDouble();
      RunScoredPer9 = result.rs9.TryParseDouble();
      WalksPer9 = result.bb9.TryParseDouble();
      StrikeoutsPer9 = result.k9.TryParseDouble();
      BattingAverageAgainst = result.avg.TryParseDouble();
      SluggingAgainst = result.slg.TryParseDouble();
      OnBasePercentageAgainst = result.obp.TryParseDouble();
      OnBasePlusSluggingAgainst = result.ops.TryParseDouble();
      WinningPercentage = result.wpct.TryParseDouble();
      EarnedRunAverage = result.era.TryParseDouble();
      PitchesPerPlateAppearance = result.ppa.TryParseDouble();
      StrikePercentage = result.spct.TryParseDouble();
    }
  }
}
