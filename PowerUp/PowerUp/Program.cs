using ElectronCgi.DotNet;
using PowerUp.DebugUtils;
using PowerUp.GameSave;
using System;

namespace PowerUp
{
  class Program
  {
    private const RunType RUN_TYPE = RunType.Application;
    private const string GAME_SAVE_PATH = "C:/Users/short/OneDrive/Documents/Dolphin Emulator/Wii/title/00010000/524d5045/data/pm2maus.dat";
    private const int PLAYER_ID = 20;

    static void Main(string[] args)
    {
      if (RUN_TYPE == RunType.Application)
      {
        var connection = new ConnectionBuilder()
          .WithLogging()
          .Build();

        // expects a request named "greeting" with a string argument and returns a string
        connection.On("greeting", (string name) =>
        {
          return $"Hello {name}!";
        });

        // wait for incoming requests
        connection.Listen();
      } 
      else if(RUN_TYPE == RunType.Analysis)
      {
        while (true) 
        {
          Console.ReadLine(); 
          using var loader = new PlayerReader(GAME_SAVE_PATH);
          var player = loader.Read(PLAYER_ID);
          var bitString = player.AccessoriesBytes!.ToBitString();
          var currentTime = DateTime.Now;
          Console.WriteLine($"Update {currentTime.ToShortDateString()} {currentTime.ToShortTimeString()}: {bitString}");
        }
      }
      

    }
  }

  public enum RunType
  {
    Analysis,
    Application
  }
}
