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

      string lastName = "";
      for(int i=1; i<970 && lastName != "Giambi"; i++)
      {
        var player = loader.Load(playerId: i);
        lastName = player.LastName ?? "";
        Console.WriteLine($@"{player.PowerProsId}: ({player.SavedName}) {player.LastName}, {player.FirstName}
          IsEdited: {player.IsEdited}
        ");
      }
    }
  }
}
