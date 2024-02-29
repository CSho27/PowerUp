using PowerUp.Fetchers.MLBStatsApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class PitchingStatsResults
  {
    public long TotalResults { get; }
    public IEnumerable<PitchingStatsResult> Results { get; }

    public PitchingStatsResults(StatElement? stats)
    {
      var validSplits = stats?.Splits.Where(s => s.Team != null).ToList() ?? [];
      TotalResults = validSplits.Count;
      Results = validSplits
        .Select(r => new PitchingStatsResult(r))
        .ToList();
    }
  }

  public class PitchingStatsResult
  {
    public int LSPlayerId { get; }
    public int Year { get; }
    public int TeamSeq { get; }
    public int LSTeamId { get; }

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

    public PitchingStatsResult(Split split)
    {
      LSPlayerId = (int)split.Player!.Id;
      Year = split.Season.TryParseInt()!.Value;
      if (split.Stat is null)
        throw new InvalidOperationException("Stat is must have a value");

      LSTeamId = (int)split.Team!.Id;
      GamesPlayed = (int)split.Stat.GamesPlayed;
      GamesStarted = (int?)split.Stat.GamesStarted;
      GamesFinished = (int?)split.Stat.GamesFinished;
      CompleteGames = (int?)split.Stat.CompleteGames;
      ShutOuts = (int?)split.Stat.Shutouts;
      Wins = (int?)split.Stat.Wins;
      Losses = (int?)split.Stat.Losses;
      // QualityStarts = (int?)split.Stat
      Saves = (int?)split.Stat.Saves;
      InningsPitched = split.Stat.InningsPitched.TryParseDouble();
      SaveOpportunities = (int?)split.Stat.SaveOpportunities;
      AtBats = (int?)split.Stat.AtBats;
      Walks = (int?)split.Stat.BaseOnBalls;
      IntentionalWalks = (int?)split.Stat.IntentionalWalks;
      HitBatters = (int?)split.Stat.HitBatsmen;
      Strikeouts = (int?)split.Stat.StrikeOuts;
      Runs = (int?)split.Stat.Runs;
      EarnedRuns = (int?)split.Stat.EarnedRuns;

      Hits = (int?)split.Stat.Hits;
      Doubles = (int?)split.Stat.Doubles;
      Triples = (int?)split.Stat.Triples;
      HomeRuns = (int?)split.Stat.HomeRuns;
      TotalBasesAllowed = (int?)split.Stat.TotalBases;
      WildPitches = (int?)split.Stat.WildPitches;
      Balks = (int?)split.Stat.Balks;
      RunnersPickedOff = (int?)split.Stat.Pickoffs;
      var numberOfPitches = (int?)split.Stat.NumberOfPitches;
      NumberOfPitches = numberOfPitches;
      Strikes = (int?)split.Stat.Strikes;
      GroundOuts = (int?)split.Stat.GroundOuts;
      AirOuts = (int?)split.Stat.AirOuts;
      DoublePlays = (int?)split.Stat.DoublePlays;
      StrikeoutToWalkRatio = split.Stat.StrikeoutWalkRatio.TryParseDouble();
      WHIP = split.Stat.Whip.TryParseDouble();
      HitsPer9 = split.Stat.HitsPer9Inn.TryParseDouble();
      HomeRunsPer9 = split.Stat.HomeRunsPer9.TryParseDouble();
      RunScoredPer9 = split.Stat.RunsScoredPer9.TryParseDouble();
      WalksPer9 = split.Stat.WalksPer9Inn.TryParseDouble();
      StrikeoutsPer9 = split.Stat.StrikeoutsPer9Inn.TryParseDouble();
      BattingAverageAgainst = split.Stat.Avg.TryParseDouble();
      SluggingAgainst = split.Stat.Slg.TryParseDouble();
      OnBasePercentageAgainst = split.Stat.Obp.TryParseDouble();
      OnBasePlusSluggingAgainst = split.Stat.Ops.TryParseDouble();
      WinningPercentage = split.Stat.WinPercentage.TryParseDouble();
      EarnedRunAverage = split.Stat.Era.TryParseDouble();
      var plateAppearances = (int?)split.Stat.PlateAppearances;
      PitchesPerPlateAppearance = numberOfPitches > 0 && plateAppearances > 0
        ? numberOfPitches / (double)plateAppearances
        : null;
      StrikePercentage = split.Stat.StrikePercentage.TryParseDouble();
    }
  }
}
