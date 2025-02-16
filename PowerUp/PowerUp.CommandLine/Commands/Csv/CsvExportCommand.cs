using Microsoft.Extensions.Logging;
using PowerUp.CSV;
using PowerUp.Databases;
using PowerUp.Entities.Rosters;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;

namespace PowerUp.CommandLine.Commands.Csv
{
  public class CsvExportCommand : ICommand
  {
    private readonly ILogger<CsvExportCommand> _logger;
    private readonly IPlayerCsvService _csvService;

    public CsvExportCommand(
      ILogger<CsvExportCommand> logger,
      IPlayerCsvService csvService
    )
    {
      _logger = logger;
      _csvService = csvService;
    }

    public Command Build()
    {
      var command = new Command("csv-export")
      {
        new Option<string?>("--out-file", "File path to export the roster to"),
        new Option<int?>("--roster-id", "Id of the roster to export")
      };
      command.Handler = GetHandler();
      return command;
    }

    private ICommandHandler GetHandler()
    {
      return CommandHandler.Create(async (string? outFile, int? rosterId) =>
      {
        if (!rosterId.HasValue)
        {
          _logger.LogError($"--roster-id must be provided");
          return;
        }

        var roster = DatabaseConfig.Database.Load<Roster>(rosterId.Value);
        if (roster is null)
        {
          _logger.LogError($"roster not found");
          return;
        }

        var filePath = outFile ?? $"{roster.Name}.csv";
        using var file = File.OpenWrite(filePath);
        await _csvService.ExportRoster(file, roster);
        file.Close();
      });
    }
  }
}
