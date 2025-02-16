using Microsoft.Extensions.Logging;
using PowerUp.CSV;
using PowerUp.Databases;
using PowerUp.Entities.Rosters;
using PowerUp.Libraries;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.CommandLine.NamingConventionBinder;
using System.CommandLine.Rendering;
using System.CommandLine.Rendering.Views;

namespace PowerUp.CommandLine.Commands.Rosters
{
  public class ListRostersCommand(
    ILogger<ListRostersCommand> logger
  ) : ICommand
  {
    public Command Build()
    {
      var command = new Command("list-rosters");
      command.Handler = GetHandler();
      return command;
    }

    private ICommandHandler GetHandler()
    {
      return CommandHandler.Create(() =>
      {
        var rosters = DatabaseConfig.Database.LoadAll<Roster>();
        var table = new TableView<Roster>() { Items = rosters.ToList() };
        table.AddColumn(r => r.Id, "Id");
        table.AddColumn(t => t.Name, "Roster");
        table.AddColumn(t => t.SourceType, "Type");
        var console = new SystemConsole();
        var renderer = new ConsoleRenderer(console);
        table.Render(renderer, Region.Scrolling);
      });
    }
  }
}
