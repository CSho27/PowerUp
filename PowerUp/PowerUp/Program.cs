using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Entities.Teams;
using PowerUp.Fetchers.Algolia;
using PowerUp.Fetchers.BaseballReference;
using PowerUp.Fetchers.MLBLookupService;
using PowerUp.Fetchers.MLBStatsApi;
using PowerUp.Fetchers.Statcast;
using PowerUp.GameSave.Api;
using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Players;
using PowerUp.GameSave.Objects.SeasonModeSalaries;
using PowerUp.GameSave.Objects.Teams;
using PowerUp.Generators;
using PowerUp.Libraries;
using PowerUp.Mappers.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp
{
  class Program
  {
    private const string GAME_SAVE_PATH = "C:/Users/short/OneDrive/Documents/Dolphin Emulator/Wii/title/00010000/524d5045/data/pm2maus.dat";
    private const string DATA_DIRECTORY = "./../../../../../PowerUp.ElectronUI/Data";
    private const int PLAYER_ID = 1;

    static void Main(string[] args)
    {
      var characterLibrary = new CharacterLibrary(Path.Combine(DATA_DIRECTORY, "./data/Character_Library.csv"));
      var voiceLibrary = new VoiceLibrary(Path.Combine(DATA_DIRECTORY, "./data/Voice_Library.csv"));
      var savedNameLibrary = new SpecialSavedNameLibrary(Path.Combine(DATA_DIRECTORY, "./data/SpecialSavedName_Library.csv"));
      var battingStanceLibrary = new BattingStanceLibrary(Path.Combine(DATA_DIRECTORY, "./data/BattingForm_Library.csv"));
      var pitchingMechanicsLibrary = new PitchingMechanicsLibrary(Path.Combine(DATA_DIRECTORY, "./data/PitchingForm_Library.csv"));
      var statcastClient = new StatcastClient();
      var algoliaClient = new AlgoliaClient();
      var mlbStatsApiClient = new MLBStatsApiClient();
      var mlbLookupServiceClient = new MLBLookupServiceClient(algoliaClient, mlbStatsApiClient);
      var baseballReferenceClient = new BaseballReferenceClient();
      var statsFetcher = new LSPlayerStatisticsFetcher(mlbLookupServiceClient);
      var playerApi = new PlayerApi();
      var playerGenerator = new PlayerGenerator(playerApi, statsFetcher, baseballReferenceClient);
      var countryAndSkinLibrary = new CountryAndSkinColorLibrary(Path.Combine(DATA_DIRECTORY, "./data/CountryAndSkinColor_Library.csv"));
      var skinColorGuesser = new SkinColorGuesser(countryAndSkinLibrary);
      var lsStatsAlgorithm = new LSStatistcsPlayerGenerationAlgorithm
        ( voiceLibrary
        , skinColorGuesser
        , new BattingStanceGuesser(battingStanceLibrary)
        , new PitchingMechanicsGuesser(pitchingMechanicsLibrary)
        );
      var teamGenerator = new TeamGenerator(mlbLookupServiceClient, playerGenerator);
      var rosterGenerator = new RosterGenerator(mlbLookupServiceClient, teamGenerator);

      //DatabaseConfig.Initialize(DATA_DIRECTORY);
      //AnalyzeGameSave(characterLibrary, savedNameLibrary);
      //PrintAllPlayers(characterLibrary, savedNameLibrary);
      //PrintAllTeams(characterLibrary);
      //PrintAllLineups(characterLibrary);
      //PrintRedsPlayers();
      //BuildPlayerValueLibrary(characterLibrary);
      //FindDuplicatesInLibrary();
      //FindPlayersByLastName();
      //CreateTeamRatingCSV(characterLibrary, savedNameLibrary);
      //CreateStatusesList(mlbLookupServiceClient);
      //CreatePlayerOutputCsv(mlbLookupServiceClient, statsFetcher, playerGenerator, lsStatsAlgorithm, voiceLibrary);
      //CreatePlayerDataComparisonCsv(mlbLookupServiceClient, statsFetcher, playerGenerator, lsStatsAlgorithm, voiceLibrary);
      //GetAllTeamsAndIds(mlbLookupServiceClient);
      //GetTeamsForMappingPPTeams(mlbLookupServiceClient);
      //TestGenerateTeam(teamGenerator, lsStatsAlgorithm);
      //TestGenerateRoster(rosterGenerator, lsStatsAlgorithm);
      //TestBuildBBRefDictionary();
      //ReadSalaryInfo(characterLibrary);
      //CopyDir();

      var results = new MLBStatsApiClient().GetFieldingStats(621035, 2016).GetAwaiter().GetResult();
    }

    static TimeSpan TimeAction(Action action)
    {
      var startTime = DateTime.Now;
      action();
      return DateTime.Now - startTime;
    }

    static void AnalyzeGameSave(ICharacterLibrary characterLibrary, ISpecialSavedNameLibrary specialSavedNameLibrary)
    {
      while (true)
      {
        var input = Console.ReadLine();
        int.TryParse(input, out var result);
        var playerId = result != 0
          ? result
          : 20;
        using var loader = new PlayerReader(characterLibrary, "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/BASLUS-21671_TEST", GameSaveFormat.Ps2_2007);
        var player = (Ps2GSPlayer)loader.Read(playerId);
        /*
        var mappedPlayer = new PlayerMapper(specialSavedNameLibrary).MapToPlayer(player, PlayerMappingParameters.FromRosterImport(new RosterImportParameters()));
        */
        var bitString = player.BytesToCheck!.ToBitString();
        var currentTime = DateTime.Now;
        Console.WriteLine($"Update {currentTime.ToShortDateString()} {currentTime.ToShortTimeString()}: {bitString}");
      }
    }

    static void PrintAllPlayers(ICharacterLibrary characterLibrary, ISpecialSavedNameLibrary specialSavedNameLibrary)
    {
      var playerReader = new PlayerReader(characterLibrary, GAME_SAVE_PATH, GameSaveFormat.Wii_2007);
      var playerMapper = new PlayerMapper(specialSavedNameLibrary);

      for (int id = 1; id < 1513; id++)
      {
        var player = playerReader.Read(id);
        var mappedPlayer = playerMapper.MapToPlayer(player, PlayerMappingParameters.FromRosterImport(new RosterImportParameters()));
        var position = (Position)player.PrimaryPosition!;
        var playerString = $"{id} {position.GetAbbrev()} {player.LastName}, {player.FirstName}";
        if(mappedPlayer.BirthMonth == 3 || mappedPlayer.BirthMonth == 4)
          Console.WriteLine($"{playerString}{new string(' ', 38 - playerString.Length)}{player.BirthMonth}/{player.BirthDay}/{player.BirthYear}");
      }
    }

    static void PrintAllTeams(ICharacterLibrary characterLibrary)
    {
      var teamReader = new TeamReader(characterLibrary, GAME_SAVE_PATH, GameSaveFormat.Wii_2007);
      var playerReader = new PlayerReader(characterLibrary, GAME_SAVE_PATH, GameSaveFormat.Wii_2007);

      for (int teamNum = 1; teamNum <= 32; teamNum++)
      {
        var team = teamReader.Read(teamNum);
        var playerEntries = team.PlayerEntries!.ToArray();
        Console.WriteLine($"Team {playerEntries.First().PowerProsTeamId}");
        for (int playerNum = 0; playerNum < playerEntries.Length; playerNum++)
        {
          var pe = playerEntries[playerNum];
          if (pe.PowerProsPlayerId == 0)
            continue;

          var player = playerReader.Read(pe.PowerProsPlayerId!.Value);
          var position = (Position)player.PrimaryPosition!;

          var playerString = $"{playerNum + 1} - {string.Format("{0:X4}", player.PowerProsId)} {position.GetAbbrev()} {player.LastName}, {player.FirstName}";
          //Console.WriteLine(playerString);
          Console.WriteLine($"{playerString}{new string(' ', 36 - playerString.Length)}{BinaryUtils.ToBitString(pe.OtherPlayerByte!, formatted: false)}");
        }
        Console.WriteLine();
      }
    }

    static void PrintAllLineups(ICharacterLibrary characterLibrary)
    {
      var lineupReader = new LineupReader(characterLibrary, GAME_SAVE_PATH, GameSaveFormat.Wii_2007);

      for (int teamNum = 1; teamNum <= 32; teamNum++)
      {
        var lineup = lineupReader.Read(teamNum);
        Console.WriteLine($"Team {teamNum}");
        Console.WriteLine("No DH:");
        PrintLineup(characterLibrary, lineup.NoDHLineup!);
        Console.WriteLine("DH:");
        PrintLineup(characterLibrary, lineup.DHLineup!);
        Console.WriteLine();
      }
    }

    static void PrintLineup(ICharacterLibrary characterLibrary, IEnumerable<GSLineupPlayer> lineup)
    {
      var playerReader = new PlayerReader(characterLibrary, GAME_SAVE_PATH, GameSaveFormat.Wii_2007);
      var lineupPlayers = lineup.ToArray();
      for (int playerNum = 0; playerNum < lineupPlayers.Length; playerNum++)
      {
        var lineupSlot = lineupPlayers[playerNum];
        string? playerString;
        if (lineupSlot.PowerProsPlayerId == 0)
        {
          playerString = "Pitcher";
        }
        else
        {
          var player = playerReader.Read(lineupSlot.PowerProsPlayerId!.Value);
          playerString = $"{player.LastName}, {player.FirstName}";
        }

        var position = lineupSlot.Position!.Value == 0
          ? Position.Pitcher
          : (Position)lineupSlot.Position!.Value;

        Console.WriteLine($"{playerNum+1}. {position.GetAbbrev()} {playerString}");
      }
    }

    static void BuildPlayerValueLibrary(ICharacterLibrary characterLibrary)
    {
      var playerReader = new PlayerReader(characterLibrary, Path.Combine(DATA_DIRECTORY, "./data/BASE.pm2maus.dat"), GameSaveFormat.Wii_2007);
      var playersAndValues = new Dictionary<string, int>();
      
      for (int id = 1; id < 1500; id++)
      {
        var player = playerReader.Read(id);
        if(player.PowerProsId != 0 )
        {
          playersAndValues.TryAdd($"{player.FirstName}_{player.LastName}", player.VoiceId!.Value);
        }
      };

      var csvLines = playersAndValues.OrderBy(kvp => kvp.Value).Select(p => $"{p.Key}, {p.Value}");
      File.WriteAllLines(Path.Combine(DATA_DIRECTORY, "./data/newVoice_Library.csv"), csvLines);
    }

    static void FindDuplicatesInLibrary()
    {
      var filePathToCheck = Path.Combine(DATA_DIRECTORY, "./data/newVoice_Library.csv");

      var keyValuePairs = File.ReadAllLines(filePathToCheck)
        .Select(l => l.Split(','))
        .Select(l => new KeyValuePair<string, string>(l[0], l[1]));

      var csvLines = keyValuePairs.GroupBy(p => p.Value).OrderBy(g => int.Parse(g.Key)).Select(g => $"{g.Key}, {string.Join(", ", g)}");
      File.WriteAllLines(Path.Combine(DATA_DIRECTORY, "./data/duplicates.csv"), csvLines);
    }

    static void CreateTeamRatingCSV(ICharacterLibrary characterLibrary, ISpecialSavedNameLibrary savedNameLibrary)
    {
      var rosterImportApi = new RosterImportApi(characterLibrary, new PlayerMapper(savedNameLibrary));
      var result = rosterImportApi.ImportRoster(new RosterImportParameters
      {
        IsBase = true,
        Stream = new FileStream(Path.Combine(DATA_DIRECTORY, "./data/BASE.pm2maus.dat"), FileMode.Open, FileAccess.Read)
      });

      var teamRatings = result.Teams.Select(t => {
        var hitterRatings = t.GetPlayers()
          .Where(p => p.PrimaryPosition != Position.Pitcher)
          .Select(p => p.Overall);

        var pitcherRatings = t.GetPlayers()
          .Where(p => p.PrimaryPosition == Position.Pitcher)
          .Select(p => p.Overall);

        return new
        {
          name = t.Name,
          hitting = TeamRatingCalculator.CalculateHittingRating(hitterRatings),
          pitching = TeamRatingCalculator.CalculatePitchingRating(pitcherRatings),
          overall = TeamRatingCalculator.CalculateOverallRating(new TeamRatingParameters { HitterRatings = hitterRatings, PitcherRatings = pitcherRatings }) 
        };
      });

      var csvLines = teamRatings.Select(r => $"{r.name}, {r.hitting}, {r.pitching}, {r.overall}");
      File.WriteAllLines(Path.Combine(DATA_DIRECTORY, "./data/teams.csv"), csvLines);
    }

    static void CreateStatusesList(MLBLookupServiceClient client)
    {
      Task.Run(async () =>
      {
        for (int year = 2018; year < 2022; year++)
        {
          var hashset = new HashSet<PlayerRosterStatus>();
          Console.WriteLine($"{year}:");
          var teams = await client.GetTeamsForYear(year);
          foreach (var team in teams.Results)
          {
            var players = await client.GetTeamRosterForYear(team.LSTeamId, year);
            var statuses = players.Results.Select(r => r.Status);

            foreach (var status in statuses)
              hashset.Add(status);
          }

          foreach(var status in hashset)
            Console.WriteLine(status);

          Console.WriteLine();
        }

      }).GetAwaiter().GetResult();
    }

    static void CreatePlayerOutputCsv(IMLBLookupServiceClient client, IPlayerGenerator playerGenerator, PlayerGenerationAlgorithm algorithm, IVoiceLibrary voiceLibrary)
    {
      var csvLines = new CSVList(
        "LSPlayerId",
        "LSName",
        "LSStatus",
        "FirstName",
        "LastName",
        "SavedName",
        "UniformNumber",
        "PrimaryPosition",
        "PitcherType",
        "VoiceId",
        "VoiceName",
        "BattingSide",
        "ThrowingArm",
        "SkinColor",
        "Pos_P",
        "Pos_C",
        "Pos_1B",
        "Pos_2B",
        "Pos_3B",
        "Pos_SS",
        "Pos_LF",
        "Pos_CF",
        "Pos_RF",
        "Htr_Trajectory",
        "Htr_Contact",
        "Htr_Power",
        "Htr_RunSpeed",
        "Htr_ArmStregnth",
        "Htr_Fielding",
        "Htr_ErrorResistance"
      );

      Task.Run(async () =>
      {
        var year = 2006;
        var teams = await client.GetTeamsForYear(year);
        foreach (var team in teams.Results)
        {
          Console.WriteLine($"Generating {team.FullName}");
          var players = await client.GetTeamRosterForYear(team.LSTeamId, year);
          foreach(var player in players.Results)
          {
            Console.WriteLine($"Generating {player.FormalDisplayName}");
            var generatedPlayer = playerGenerator.GeneratePlayer(player.LSPlayerId, year, algorithm).Player;
            var genPlayerPosCapabilities = generatedPlayer.PositionCapabilities;
            var genPlayerHitterAbilities = generatedPlayer.HitterAbilities;

            csvLines.AddLine(
              player.LSPlayerId,
              player.FormalDisplayName,
              player.Status,
              generatedPlayer.FirstName,
              generatedPlayer.LastName,
              generatedPlayer.SavedName,
              generatedPlayer.UniformNumber,
              generatedPlayer.PrimaryPosition,
              generatedPlayer.PitcherType,
              generatedPlayer.VoiceId,
              voiceLibrary[generatedPlayer.VoiceId],
              generatedPlayer.BattingSide,
              generatedPlayer.ThrowingArm,
              generatedPlayer.Appearance.SkinColor?.ToString() ?? "None",
              genPlayerPosCapabilities.Pitcher,
              genPlayerPosCapabilities.Catcher,
              genPlayerPosCapabilities.FirstBase,
              genPlayerPosCapabilities.SecondBase,
              genPlayerPosCapabilities.ThirdBase,
              genPlayerPosCapabilities.Shortstop,
              genPlayerPosCapabilities.LeftField,
              genPlayerPosCapabilities.CenterField,
              genPlayerPosCapabilities.RightField,
              genPlayerHitterAbilities.Trajectory,
              genPlayerHitterAbilities.Contact,
              genPlayerHitterAbilities.Power,
              genPlayerHitterAbilities.RunSpeed,
              genPlayerHitterAbilities.ArmStrength,
              genPlayerHitterAbilities.Fielding,
              genPlayerHitterAbilities.ErrorResistance
            );
          }
          Console.WriteLine($"");
        }
      }).GetAwaiter().GetResult();

      csvLines.WriteToFile(Path.Combine(DATA_DIRECTORY, "./data/PlayerOutput.csv"));
    }

    static void CreatePlayerDataComparisonCsv(IMLBLookupServiceClient client, IPlayerStatisticsFetcher statsFetcher, IPlayerGenerator playerGenerator, PlayerGenerationAlgorithm algorithm, IVoiceLibrary voiceLibrary)
    {
      var csvLines = new CSVList(
        "PlayerId",
        "Name",
        "PPId",
        "Trj",
        "Pow",
        "Con",
        "Arm",
        "Spd",
        "Fld",
        "eRes",
        "PP_PType",
        "Ctrl",
        "Stam",
        "Top Spd",
        "1st",
        "1st Mvt",
        "2nd",
        "2nd Mvt",
        "3rd",
        "3rd Mvt",
        "Status",
        "Pos",
        "AB",
        "HR",
        "SB",
        "R",
        "Inn",
        "TC",
        "A",
        "RF/9",
        "Fpct",
        "C_SB",
        "C_CS",
        "PType",
        "G",
        "Inn P",
        "BB/9",
        "K/9",
        "ERA",
        "WHIP",
        "BAA",
        "Non-1B Pos",
        "CalcPow"
      );

      Task.Run(async () =>
      {
        var year = 2006;
        var teams = await client.GetTeamsForYear(year);
        foreach (var team in teams.Results)
        {
          /*
          if (!team.Name.Contains("Marlins"))
            continue;
          */

          Console.WriteLine($"Fetching Stats for {team.FullName}");
          var players = await client.GetTeamRosterForYear(team.LSTeamId, year);
          foreach (var player in players.Results)
          {
            var loadedPlayer = DatabaseConfig.Database.Query<Player>().Where(p => p.FormalDisplayName == player.FormalDisplayName && p.PrimaryPosition == player.Position).SingleOrDefault();
            if (loadedPlayer == null)
              continue;

            Console.WriteLine($"Fetching Stats for {player.FormalDisplayName}");
            var playerStats = statsFetcher.GetStatistics(player.LSPlayerId, year);
            var data = new PlayerGenerationData
            {
              PlayerInfo = playerStats.PlayerInfo != null
                ? new LSPlayerInfoDataset(playerStats.PlayerInfo, player.UniformNumber)
                : null,
              HittingStats = playerStats.HittingStats != null
                ? new LSHittingStatsDataset(playerStats.HittingStats.Results)
                : null,
              FieldingStats = playerStats.FieldingStats != null
                ? new LSFieldingStatDataset(playerStats.FieldingStats.Results)
                : null,
              PitchingStats = playerStats.PitchingStats != null
                ? new LSPitchingStatsDataset(playerStats.PitchingStats.Results)
                : null
            };

            var genPlayer = playerGenerator.GeneratePlayer(player.LSPlayerId, year, algorithm).Player;

            var nameParts = player.FormalDisplayName.Split(",");
            var informalName = $"{nameParts[1].Trim()} {nameParts[0].Trim()}";

            var validPositionStats = data.FieldingStats?.FieldingByPosition.Where(kvp => kvp.Key.GetPositionType() == data.PrimaryPosition.GetPositionType());
            var relevantAssists = validPositionStats?.SumOrNull(r => r.Value.Assists) ?? 0;
            var sortedArsenal = loadedPlayer.PitcherAbilities.GetSortedArsenal();

            csvLines.AddLine(
              player.LSPlayerId,
              informalName,
              loadedPlayer.SourcePowerProsId,
              loadedPlayer.HitterAbilities.Trajectory,
              loadedPlayer.HitterAbilities.Power,
              loadedPlayer.HitterAbilities.Contact,
              loadedPlayer.HitterAbilities.ArmStrength,
              loadedPlayer.HitterAbilities.RunSpeed,
              loadedPlayer.HitterAbilities.Fielding,
              loadedPlayer.HitterAbilities.ErrorResistance,
              loadedPlayer.PitcherType,
              loadedPlayer.PitcherAbilities.Control,
              loadedPlayer.PitcherAbilities.Stamina,
              loadedPlayer.PitcherAbilities.TopSpeedMph,
              sortedArsenal.FirstOrDefault().type,
              sortedArsenal.FirstOrDefault().movement,
              sortedArsenal.ElementAtOrDefault(1).type,
              sortedArsenal.ElementAtOrDefault(1).movement,
              sortedArsenal.ElementAtOrDefault(2).type,
              sortedArsenal.ElementAtOrDefault(2).movement,
              player.Status,
              (int)data.PrimaryPosition,
              data.HittingStats?.AtBats,
              data.HittingStats?.HomeRuns,
              data.HittingStats?.StolenBases,
              data.HittingStats?.Runs,
              data.FieldingStats?.OverallFielding?.Innings,
              data.FieldingStats?.OverallFielding?.TotalChances,
              relevantAssists,
              data.FieldingStats?.FieldingByPosition[data.PrimaryPosition]?.RangeFactor,
              data.FieldingStats?.FieldingByPosition[data.PrimaryPosition]?.FieldingPercentage,
              data.FieldingStats?.OverallFielding?.Catcher_StolenBasesAllowed,
              data.FieldingStats?.OverallFielding?.Catcher_RunnersThrownOut,
              genPlayer.PitcherType,
              data.PitchingStats?.GamesPitched,
              data.PitchingStats?.MathematicalInnings,
              data.PitchingStats?.WalksPer9,
              data.PitchingStats?.StrikeoutsPer9,
              data.PitchingStats?.EarnedRunAverage,
              data.PitchingStats?.WHIP,
              data.PitchingStats?.BattingAverageAgainst,
              data.FieldingStats?.FieldingByPosition.Count(s => s.Key != Position.FirstBase) ?? 0,
              genPlayer.HitterAbilities.Power
            );
          }
          Console.WriteLine($"");
        }
      }).GetAwaiter().GetResult();

      csvLines.WriteToFile(Path.Combine(DATA_DIRECTORY, "./data/PlayerStatsComparison.csv"));
    }

    static void GetAllTeamsAndIds(IMLBLookupServiceClient client)
    {
      var teamResults = new List<TeamResult>();
      Task.Run(async () =>
      {
        for (int year = 1876; year <= 2022; year++)
        {
          Console.WriteLine($"Getting teams for {year}...");
          var teams = await client.GetAllStarTeamsForYear(year);
          foreach(var team in teams.Results)
          {
            Console.WriteLine($"Adding {team.FullName}");
            teamResults.Add(team);
          }
          Console.WriteLine();
        }
      }).GetAwaiter().GetResult();

      var teamEraResults = teamResults
        .GroupBy(r => r.LSTeamId)
        .SelectMany(g => g.GroupBy(t => t.FullName).Select(e => new { LSTeamId = e.First().LSTeamId, StartYear = e.First().Year, EndYear = e.Last().Year, Name = e.First().FullName }));

      var csvList = new CSVList("LSTeamId", "StartYear", "EndYear", "Name");
      foreach(var teamEra in teamEraResults)
        csvList.AddLine(teamEra.LSTeamId, teamEra.StartYear, teamEra.EndYear, teamEra.Name);

      csvList.WriteToFile(Path.Combine(DATA_DIRECTORY, "./data/TeamListASG.csv"));
    }

    static void GetTeamsForMappingPPTeams(IMLBLookupServiceClient client)
    {
      var csvList = new CSVList("LSTeamId", "Name");
      Task.Run(async () =>
      {
        var teams = await client.GetAllStarTeamsForYear(2006);
        foreach (var team in teams.Results)
          csvList.AddLine(team.LSTeamId, team.FullName);
      }).GetAwaiter().GetResult();

      csvList.WriteToFile(Path.Combine(DATA_DIRECTORY, "./data/TeamList06ASG.csv"));
    }

    static void TestGenerateTeam(ITeamGenerator teamGenerator, PlayerGenerationAlgorithm algorithm)
    {
      var team = MLBPPTeam.AmericanLeagueAllStars;
      var startTime = DateTime.Now;
      var result = teamGenerator.GenerateTeam(
        lsTeamId: 119, 
        year: 1884, 
        name: "Cleveland Indians", 
        playerGenerationAlgorithm: algorithm, 
        onProgressUpdate: update => Console.WriteLine($"{update.PercentCompletion.ToPercentDisplay()} {(DateTime.Now - startTime).ToDisplayString()} | rem. {update.GetEstimatedTimeRemaining(DateTime.Now - startTime).ToDisplayString()} | {update.CurrentAction}")
      );

      PrintRosterInfoFor(result.Team);
    }

    static void TestGenerateRoster(IRosterGenerator rosterGenerator, PlayerGenerationAlgorithm algorithm)
    {
      var year = 1948;
      var startTime = DateTime.Now;

      var result = rosterGenerator.GenerateRoster(
        year: year,
        playerGenerationAlgorithm: algorithm,
        onTeamProgressUpdate: update => {
          Console.WriteLine($"{update.PercentCompletion.ToPercentDisplay()} {(DateTime.Now - startTime).ToDisplayString()} | rem. {update.GetEstimatedTimeRemaining(DateTime.Now - startTime).ToDisplayString()}");
          Console.WriteLine();
          Console.WriteLine(update.CurrentAction);
        },
        onPlayerProgressUpdate: update => Console.WriteLine($"{update.CurrentAction}")
      );

      foreach(var team in result.Roster.GetTeams())
      {
        Console.WriteLine($"{team.Key.Name} ({team.Value.GetFullDisplayName()})");
        PrintRosterInfoFor(team.Key);
        Console.WriteLine();
      }
    }

    static void TestBuildBBRefDictionary()
    {
      Task.Run(async () =>
      {
        var result = await new BaseballReferenceClient().GetBaseballReferenceIdFor("Giancarlo", "Stanton", 2010);
      }).GetAwaiter().GetResult();
    }

    static void TestLoad()
    {
      var player = DatabaseConfig.Database.Load<Player>(1);
      Console.WriteLine($"Name: {player?.FormalDisplayName}");
    }

    static void PrintRosterInfoFor(Team team)
    {
      var players = team.GetPlayers().ToList();
      foreach (var player in players)
        Console.WriteLine($"{player.InformalDisplayName}: {player.Overall.RoundDown()}, {player.PrimaryPosition.GetAbbrev()}");

      Console.WriteLine();
      Console.WriteLine("No DH:");

      var noDHLineup = team.NoDHLineup.ToList();
      for (var i = 0; i < noDHLineup.Count; i++)
      {
        var slot = noDHLineup[i];
        if (!slot.PlayerId.HasValue)
        {
          Console.WriteLine($"{i + 1}. Pitcher");
          continue;
        }

        var player = players.Single(p => p.Id == slot.PlayerId);
        Console.WriteLine($"{i + 1}. {player.InformalDisplayName} {slot.Position.GetAbbrev()} {player.Overall.RoundDown()}");
      }

      Console.WriteLine();
      Console.WriteLine("DH:");

      var dhLineup = team.DHLineup.ToList();
      for (var i = 0; i < dhLineup.Count; i++)
      {
        var slot = dhLineup[i];
        var player = players.Single(p => p.Id == slot.PlayerId);
        Console.WriteLine($"{i + 1}. {player.InformalDisplayName} {slot.Position.GetAbbrev()} {player.Overall.RoundDown()}");
      }
    }

    static void ReadSalaryInfo(ICharacterLibrary characterLibrary)
    {
      var csvList = new CSVList("PlayerId", "First", "Last", "Salary", "Length");

      using var reader = new GameSaveObjectReader(characterLibrary, "C:/Users/short/OneDrive/Documents/Dolphin Emulator/Wii/title/00010000/524d5045/data/pm2se1us.dat", ByteOrder.BigEndian);
      var salaryDict = reader.Read<GSSalaryList>(0x20F4C).SalaryEntries!.ToDictionary(e => e.PowerProsPlayerId!.Value, e => e);

      var playerReader = new PlayerReader(characterLibrary, Path.Combine(DATA_DIRECTORY, "./data/BASE.pm2maus.dat"), GameSaveFormat.Wii_2007);
      for (int id = 1; id < 971; id++)
      {
        var player = playerReader.Read(id);
        var salaryEntry = salaryDict[(ushort)id];
;       csvList.AddLine(salaryEntry.PowerProsPlayerId, player.FirstName, player.LastName, salaryEntry.PowerProsPointsPerYear, salaryEntry.YearsUntilFreeAgency);
      }

      csvList.WriteToFile(Path.Combine(DATA_DIRECTORY, "./data/PlayerSalaries.csv"));
    }

    static void CopyDir()
    {
      FileSystemUtils.CopyDirectoryRecursively("C:/PowerUp_Demo2", "C:/PowerUp_Demo3");
    }
  }

  public class CSVList
  {
    private readonly List<string> lines;
    private readonly int headerCount;

    public CSVList(params string[] headers)
    {
      var line = string.Join(",", headers);
      lines = new List<string>() { line };
      headerCount = headers.Length;
    }

    public void AddLine(params object?[] values) 
    {
      if (values.Length != headerCount)
        throw new InvalidOperationException($"Number of values must be equal to header count of {headerCount}");

      var line = string.Join(",", values.Select(v => v?.ToString()?.Replace(",", "") ?? ""));
      lines.Add(line);
    }

    public void WriteToFile(string path) => File.WriteAllLines(path, lines);
  }
}
