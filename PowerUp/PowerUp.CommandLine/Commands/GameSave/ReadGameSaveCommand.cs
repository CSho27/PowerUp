using Microsoft.Extensions.Logging;
using PowerUp.GameSave.Api;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;
using System.CommandLine;

namespace PowerUp.CommandLine.Commands.GameSave
{
  public class ReadGameSaveCommand(
    ILogger<ReadGameSaveCommand> logger,
    IRosterImportApi rosterImportApi
  ): ICommand
  {
    public Command Build()
    {
      var command = new Command("read-game-save")
      {
        new Option<string?>("--in-file", "File to use as the source for the game save"),
        new Option<string?>("--name", "Name of the roster being imported"),
      };
      command.Handler = GetHandler();
      return command;
    }

    private ICommandHandler GetHandler()
    {
      return CommandHandler.Create((string? inFile, string? name) =>
      {
        if (string.IsNullOrEmpty(inFile))
        {
          logger.LogError($"--in-file must be provided");
          return;
        }

        var rosterName = string.IsNullOrEmpty(name)
          ? $"Imported Roster {DateTime.Now.ToFileTime()}"
          : name;

        using var file = File.OpenRead(inFile);
        var parameters = new RosterImportParameters
        {
          Stream = file,
          ImportSource = string.IsNullOrEmpty(rosterName)
            ? Path.GetFileName(file.Name)
            : rosterName
        };
        var roster = rosterImportApi.ImportRoster(parameters);
        logger.LogInformation($"Successfully imported roster as {rosterName}");
      });
    }
  }
}
