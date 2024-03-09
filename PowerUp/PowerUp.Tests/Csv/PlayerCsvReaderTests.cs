using NUnit.Framework;
using PowerUp.CSV;
using Shouldly;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Tests.Csv
{
  public class PlayerCsvReaderTests
  {
    private readonly static string TEST_READ_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "TestImport.csv");
    private IPlayerCsvReader _reader;

    [SetUp]
    public void Setup()
    {
      _reader = new PlayerCsvReader();          
    }

    [Test]
    public async Task ReadAllPlayers_ReadsPlayers()
    {
      // Arrange
      using var fileStream = File.OpenRead(TEST_READ_GAME_SAVE_FILE_PATH);

      // Act
      var players = await _reader.ReadAllPlayers(fileStream);

      // Assert
      var joseRamirez = players.ElementAt(0);
      joseRamirez.TeamId.ShouldBe(114);
      joseRamirez.FirstName.ShouldBe("Jose");
      joseRamirez.LastName.ShouldBe("Ramirez");
    }
  }
}
