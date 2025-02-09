using PowerUp.Fetchers.MLBStatsApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class HittingStatsResults
  {
    public long TotalResults { get; }
    public IEnumerable<HittingStatsResult> Results { get; }

    public HittingStatsResults(StatElement? stats)
    {
      var validSplits = stats?.Splits.Where(s => s.Team != null).ToList() ?? [];
      TotalResults = validSplits.Count;
      Results = validSplits
        .Select(r => new HittingStatsResult(r))
        .ToList();
    }
  }

  public class HittingStatsResult
  {
    public int LSPlayerId { get; }
    public int Year { get; }
    public int LSTeamId { get; }
    
    public int GamesPlayed { get; }
    public int AtBats { get; }
    public int PlateAppearances { get; }
    public int Hits { get; }
    public int Doubles { get; }
    public int Triples { get; }
    public int HomeRuns { get; }
    public int ExtraBaseHits { get; }
    public int TotalBases { get; }
    public int Walks { get; }
    public int? IntentionalWalks { get; }
    public int? HitByPitches { get; }
    public int RunsBattedIn { get; }
    public int? RunnersLeftOnBase { get; }
    public int? Runs { get; }
    public int? Strikeouts { get; }
    public int? GroundOuts { get; }
    public int? AirOuts { get; }
    public int? HardGrounders { get; }
    public int? HardLineDrives { get; }
    public int? HardFlyBalls { get; }
    public int? HardPopUps { get; }
    public int? GroundedIntoDoublePlay { get; }
    public int? SacrificeFlies { get; }
    public int? SacrificeBunts { get; }
    public int? ReachedOnErrors { get; }
    public double? BattingAverage { get; }
    public double? SluggingPercentage { get; }
    public double? OnBasePercentage { get; }
    public double? OnBasePlusSluggingPercentage { get; }
    public double? BattingAverageOnBallsInPlay { get; }
    public double? PitchesPerPlateAppearance { get; }
    public int? StolenBases { get; }
    public int? CaughtStealing { get; }

    public HittingStatsResult(Split split)
    {
      LSPlayerId = (int)split.Player!.Id;
      Year = split.Season.TryParseInt()!.Value;
      if (split.Stat is null)
        throw new InvalidOperationException("Stat is must have a value");
      
      LSTeamId = (int)split.Team!.Id;
      GamesPlayed = (int)split.Stat.GamesPlayed;
      AtBats = (int)split.Stat.AtBats!;
      var plateAppearances = (int)split.Stat.PlateAppearances!;
      PlateAppearances = plateAppearances;
      Hits = (int)split.Stat.Hits!;

      var doubles = (int)split.Stat.Doubles!;
      var triples = (int)split.Stat.Triples!;
      var homeRuns = (int)split.Stat.HomeRuns!;
      Doubles = doubles;
      Triples = triples;
      HomeRuns = homeRuns;
      ExtraBaseHits = doubles + triples + homeRuns;
      TotalBases = (int)split.Stat.TotalBases!;
      Walks = (int)split.Stat.BaseOnBalls!;
      IntentionalWalks = (int?)split.Stat.IntentionalWalks;
      HitByPitches = (int?)split.Stat.HitByPitch;
      RunsBattedIn = (int)split.Stat.Rbi!;
      RunnersLeftOnBase = (int?)split.Stat.LeftOnBase;
      Runs = (int?)split.Stat.Runs;
      Strikeouts = (int?)split.Stat.StrikeOuts;
      GroundOuts = (int?)split.Stat.GroundOuts;
      AirOuts = (int?)split.Stat.AirOuts;
      GroundedIntoDoublePlay = (int?)split.Stat.GroundIntoDoublePlay;
      SacrificeFlies = (int?)split.Stat.SacFlies;
      SacrificeBunts = (int?)split.Stat.SacBunts;
      ReachedOnErrors = (int?)split.Stat.Errors;
      BattingAverage = split.Stat.Avg.TryParseDouble();
      SluggingPercentage = split.Stat.Slg.TryParseDouble();
      OnBasePercentage = split.Stat.Obp.TryParseDouble();
      OnBasePlusSluggingPercentage = split.Stat.Ops.TryParseDouble();
      BattingAverageOnBallsInPlay = split.Stat.Babip.TryParseDouble();
      var numberOfPitches = (int)(split.Stat.NumberOfPitches ?? 0);
      PitchesPerPlateAppearance = numberOfPitches > 0 && plateAppearances > 0
        ? numberOfPitches / (double)plateAppearances
        : null;
      StolenBases = (int?)split.Stat.StolenBases;
      CaughtStealing = (int?)split.Stat.CaughtStealing;
    }
  }
}
