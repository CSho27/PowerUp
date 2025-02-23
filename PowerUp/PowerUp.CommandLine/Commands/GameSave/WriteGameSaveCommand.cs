using Microsoft.Extensions.Logging;
using PowerUp.Databases;
using PowerUp.Entities.Rosters;
using PowerUp.GameSave.Api;
using PowerUp.Libraries;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;

namespace PowerUp.CommandLine.Commands.Rosters
{
  public class WriteGameSaveCommand(
    ILogger<WriteGameSaveCommand> logger,
    IRosterExportApi rosterExportApi,
    IBaseGameSavePathProvider gameSavePathProvider
  ) : ICommand
  {
    public Command Build()
    {
      var command = new Command("write-game-save")
      {
        new Option<int?>("--roster-id", "Id of the roster to export"),
        new Option<string?>("--in-file", "File to use as the source for the game save"),
        new Option<string?>("--out-file", "File path to export the roster to"),
        new Option<string?>("--out-dir", "Directory to export the roster to"),
      };
      command.Handler = GetHandler();
      return command;
    }

    private ICommandHandler GetHandler()
    {
      return CommandHandler.Create((
        int? rosterId,
        string? inFile,
        string? outFile, 
        string? outDir
      ) =>
      {
        if (!rosterId.HasValue)
        {
          logger.LogError($"--roster-id must be provided");
          return;
        }

        var roster = DatabaseConfig.Database.Load<Roster>(rosterId.Value);
        if (roster is null)
        {
          logger.LogError($"roster not found");
          return;
        }

        var defaultFileName = $"{DateTime.Now.ToFileTime()}_{roster.Name}_pm2maus.dat";
        var directory = string.IsNullOrEmpty(outDir)
          ? ""
          : outDir;

        var filePath = string.IsNullOrEmpty(outFile)
          ? Path.Combine(directory, defaultFileName)
          : outFile;
        File.Copy(inFile ?? gameSavePathProvider.GetPath(), filePath);
        logger.LogInformation($"Writing output to {filePath}");
        rosterExportApi.WriteRosterToFile(roster, filePath);
      });
    }
  }
}
