﻿using Microsoft.Extensions.DependencyInjection;
using System.CommandLine.NamingConventionBinder;
using System.CommandLine;
using PowerUp.CommandLine.Commands.Rosters;
using PowerUp.CommandLine.Commands.Csv;
using PowerUp.CommandLine.Commands.GameSave;

namespace PowerUp.CommandLine.Commands
{
  public static class CommandRegistry
  {
    public static IServiceCollection RegisterCommands(this IServiceCollection services)
    {
      services.AddTransient<ICommand, ListRostersCommand>();
      services.AddTransient<ICommand, CsvExportCommand>();
      services.AddTransient<ICommand, CsvImportCommand>();
      services.AddTransient<ICommand, WriteGameSaveCommand>();
      services.AddTransient<ICommand, ReadGameSaveCommand>();
      return services;
    }

    public static Command BuildRootCommand(IServiceProvider services, Action quit)
    {
      var commands = services.GetServices<ICommand>();
      var rootCommand = new RootCommand("PowerUp CLI");

      foreach (var command in commands)
        rootCommand.AddCommand(command.Build());

      rootCommand.AddCommand(BuildQuitCommand(quit));

      return rootCommand;
    }

    private static Command BuildQuitCommand(Action quit)
    {
      var quitCommand = new Command("quit")
      {
        Handler = CommandHandler.Create(quit)
      };

      return quitCommand;
    }
  }
}
