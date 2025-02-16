using Microsoft.Extensions.Logging;
using PowerUp.CSV;
using PowerUp.Databases;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;

namespace PowerUp.CommandLine.Commands.Csv
{
  public class CsvImportCommand(
    ILogger<CsvImportCommand> logger, 
    IPlayerCsvService csvService
  ): ICommand
  {
    public Command Build()
    {
      var command = new Command("csv-import")
      {
        new Option<string?>("--in-file", "File path to import the roster from"),
        new Option<string?>("--name", "Name of the roster being imported")
      };
      command.Handler = GetHandler();
      return command;
    }

    private ICommandHandler GetHandler()
    {
      return CommandHandler.Create(async (string? inFile, string? name) =>
      {
        if(string.IsNullOrEmpty(inFile))
        {
          logger.LogError($"--in-file must be provided");
          return;
        }

        var rosterName = string.IsNullOrEmpty(name)
          ? $"Imported Roster {DateTime.Now.ToFileTime()}"
          : name;
        
        using var file = File.OpenRead(inFile);
        var roster = await csvService.ImportRoster(file, rosterName);
        if (roster is not null)
          DatabaseConfig.Database.Save(roster);
      });
    }
  }
}
