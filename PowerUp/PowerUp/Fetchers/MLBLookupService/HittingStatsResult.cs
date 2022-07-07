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
    public int TeamSeq { get; }
    
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
    public double? BattingAverage { get; }
    public double? SluggingPercentage { get; }
    public double? OnBasePercentage { get; }
    public double? OnBasePlusSluggingPercentage { get; }
    public double? BattingAverageOnBallsInPlay { get; }
    public double? PitchesPerPlateAppearance { get; }
    public int StolenBases { get; }
    public int CaughtStealing { get; }

    public HittingStatsResult(LSHittingStatsResult result)
    {
      try
      {
        LSPlayerId = int.Parse(result.player_id!);
        Year = int.Parse(result.season!);
        TeamSeq = (int)double.Parse(result.team_seq!);
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
        IntentionalWalks = result.ibb.TryParseInt();
        HitByPitches = int.Parse(result.hbp!);
        RunsBattedIn = int.Parse(result.rbi!);
        RunnersLeftOnBase = result.lob.TryParseInt();
        Runs = int.Parse(result.r!);
        Strikeouts = int.Parse(result.so!);
        GroundOuts = result.go.TryParseInt();
        AirOuts = result.ao.TryParseInt();
        HardGrounders = result.hgnd.TryParseInt();
        HardLineDrives = result.hldr.TryParseInt();
        HardFlyBalls = result.hfly.TryParseInt();
        HardPopUps = result.hpop.TryParseInt();
        GroundedIntoDoublePlay = result.gidp.TryParseInt();
        SacrificeFlies = result.sf.TryParseInt();
        SacrificeBunts = result.sac.TryParseInt();
        ReachedOnErrors = result.roe.TryParseInt();
        BattingAverage = result.avg.TryParseDouble();
        SluggingPercentage = result.slg.TryParseDouble();
        OnBasePercentage = result.obp.TryParseDouble();
        OnBasePlusSluggingPercentage = result.ops.TryParseDouble();
        BattingAverageOnBallsInPlay = result.babip.TryParseDouble();
        PitchesPerPlateAppearance = result.ppa.TryParseDouble();
        StolenBases = int.Parse(result.sb!);
        CaughtStealing = int.Parse(result.cs!);
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
