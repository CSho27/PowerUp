using PowerUp.GameSave;
using System;

namespace PowerUp
{
  class Program
  {
    static void Main(string[] args)
    {
      var filePath = "C:/dev/PowerUp/SaveFileAnalysis/pm2maus_after.dat";
      using var loader = new PlayerLoader(filePath);

      for(int i=1; i<30; i++)
      {
        var player = loader.Load(playerId: i);
        Console.WriteLine($@"{player.PowerProsId}: ({player.SavedName}) {player.LastName}, {player.FirstName}
          IsEdited: {player.IsEdited}
        ");
      }
    }
  }
}
