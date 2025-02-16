using Microsoft.Extensions.Logging;
using PowerUp.CommandLine.Rendering;
using PowerUp.Databases;
using PowerUp.Entities.Rosters;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;

namespace PowerUp.CommandLine.Commands.Rosters
{
  public class ListRostersCommand(
    ILogger<ListRostersCommand> logger
  ) : ICommand
  {
    public Command Build()
    {
      var command = new Command("list-rosters")
      {
        Handler = GetHandler()
      };
      return command;
    }

    private ICommandHandler GetHandler()
    {
      return CommandHandler.Create(() =>
      {
        var rosters = DatabaseConfig.Database.LoadAll<Roster>();
        var table = new Table<Roster>([
          new Column<Roster>("Id", r => r.Id),
          new Column<Roster>("Roster", r => r.Name),
          new Column<Roster>("Type", r => r.SourceType), 
        ]);
        Console.WriteLine(table.Render(rosters));
      });
    }
  }
}
