using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class HittingStatsResults
  {
    public long TotalResults { get; }
    public IEnumerable<HittingStatsResult> Results { get; }

    public HittingStatsResults(int totalResults, IEnumerable<LSHittingStatsResult> results)
    {
      TotalResults = totalResults;
      Results = results.Select(r => new HittingStatsResult(r));
    }
  }

  public class HittingStatsResult
  {
    public int LSPlayerId { get; }
    public int Year { get; }
    public double TeamSeq { get; }
    
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
    public int HitByPitches { get; }
    public int RunsBattedIn { get; }
    public int? RunnersLeftOnBase { get; }
    public int Runs { get; }
    public int Strikeouts { get; }
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
    public double BattingAverage { get; }
    public double SluggingPercentage { get; }
    public double OnBasePercentage { get; }
    public double OnBasePlusSluggingPercentage { get; }
    public double? BattingAverageOnBallsInPlay { get; }
    public double? PitchesPerPlateAppearance { get; }
    public int StolenBases { get; }
    public int CaughtStealing { get; }

    public HittingStatsResult(LSHittingStatsResult result)
    {
      LSPlayerId = int.Parse(result.player_id!);
      Year = int.Parse(result.season!);
      TeamSeq = double.Parse(result.team_seq!);
      GamesPlayed = int.Parse(result.g!);
      AtBats = int.Parse(result.ab!);
      PlateAppearances = int.Parse(result.tpa!);
      Hits = int.Parse(result.h!);
      Doubles = int.Parse(result.d!);
      Triples = int.Parse(result.t!);
      HomeRuns = int.Parse(result.hr!);
      ExtraBaseHits = int.Parse(result.xbh!);
      TotalBases = int.Parse(result.tb!);
      Walks = int.Parse(result.bb!);
      IntentionalWalks = result.ibb.ParseIntIfNotEmpty();
      HitByPitches = int.Parse(result.hbp!);
      RunsBattedIn = int.Parse(result.rbi!);
      RunnersLeftOnBase = result.lob.ParseIntIfNotEmpty();
      Runs = int.Parse(result.r!);
      Strikeouts = int.Parse(result.so!);
      GroundOuts = result.go.ParseIntIfNotEmpty();
      AirOuts = result.ao.ParseIntIfNotEmpty();
      HardGrounders = result.hgnd.ParseIntIfNotEmpty();
      HardLineDrives = result.hldr.ParseIntIfNotEmpty();
      HardFlyBalls = result.hfly.ParseIntIfNotEmpty();
      HardPopUps = result.hpop.ParseIntIfNotEmpty();
      GroundedIntoDoublePlay = result.gidp.ParseIntIfNotEmpty();
      SacrificeFlies = result.sf.ParseIntIfNotEmpty();
      SacrificeBunts = result.sac.ParseIntIfNotEmpty();
      ReachedOnErrors = result.roe.ParseIntIfNotEmpty();
      BattingAverage = double.Parse(result.avg!);
      SluggingPercentage = double.Parse(result.slg!);
      OnBasePercentage = double.Parse(result.obp!);
      OnBasePlusSluggingPercentage = double.Parse(result.ops!);
      BattingAverageOnBallsInPlay = result.babip.ParseDoubleIfNotEmpty();
      PitchesPerPlateAppearance = result.ppa.ParseDoubleIfNotEmpty();
      StolenBases = int.Parse(result.sb!);
      CaughtStealing = int.Parse(result.cs!);
    }
  }
}
