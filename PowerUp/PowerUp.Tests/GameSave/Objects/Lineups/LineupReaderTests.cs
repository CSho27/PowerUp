using NUnit.Framework;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.Libraries;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Tests.GameSave.Objects.Lineups
{
  public class LineupReaderTests
  {
    private const string TEST_READ_GAME_SAVE_FILE_PATH = "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/pm2maus_TEST.dat";
    private const int INDIANS_ID = 7;

    private ICharacterLibrary _characterLibrary;

    [SetUp]
    public void SetUp()
    {
      _characterLibrary = TestConfigHelpers.GetCharacterLibrary();
    }

    [Test]
    public void Reads_NoDHLineup()
    {
      using var reader = new LineupReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH);
      var lineupDefinition = reader.Read(INDIANS_ID);
      var noDH = lineupDefinition.NoDHLineup;

      noDH.ElementAt(0).PowerProsPlayerId.ShouldBe((ushort)626);
      noDH.ElementAt(0).Position.ShouldBe((ushort)8);


      noDH.ElementAt(1).PowerProsPlayerId.ShouldBe((ushort)110);
      noDH.ElementAt(1).Position.ShouldBe((ushort)9);


      noDH.ElementAt(2).PowerProsPlayerId.ShouldBe((ushort)423);
      noDH.ElementAt(2).Position.ShouldBe((ushort)3);


      noDH.ElementAt(3).PowerProsPlayerId.ShouldBe((ushort)425);
      noDH.ElementAt(3).Position.ShouldBe((ushort)2);


      noDH.ElementAt(4).PowerProsPlayerId.ShouldBe((ushort)40);
      noDH.ElementAt(4).Position.ShouldBe((ushort)7);


      noDH.ElementAt(5).PowerProsPlayerId.ShouldBe((ushort)552);
      noDH.ElementAt(5).Position.ShouldBe((ushort)6);


      noDH.ElementAt(6).PowerProsPlayerId.ShouldBe((ushort)616);
      noDH.ElementAt(6).Position.ShouldBe((ushort)4);


      noDH.ElementAt(7).PowerProsPlayerId.ShouldBe((ushort)622);
      noDH.ElementAt(7).Position.ShouldBe((ushort)5);


      noDH.ElementAt(8).PowerProsPlayerId.ShouldBe((ushort)0);
      noDH.ElementAt(8).Position.ShouldBe((ushort)0);
    }

    [Test]
    public void Reads_DHLineup()
    {
      using var reader = new LineupReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH);
      var lineupDefinition = reader.Read(INDIANS_ID);
      var dh = lineupDefinition.DHLineup;

      dh.ElementAt(0).PowerProsPlayerId.ShouldBe((ushort)626);
      dh.ElementAt(0).Position.ShouldBe((ushort)8);


      dh.ElementAt(1).PowerProsPlayerId.ShouldBe((ushort)110);
      dh.ElementAt(1).Position.ShouldBe((ushort)9);


      dh.ElementAt(2).PowerProsPlayerId.ShouldBe((ushort)423);
      dh.ElementAt(2).Position.ShouldBe((ushort)10);


      dh.ElementAt(3).PowerProsPlayerId.ShouldBe((ushort)425);
      dh.ElementAt(3).Position.ShouldBe((ushort)2);

      dh.ElementAt(4).PowerProsPlayerId.ShouldBe((ushort)329);
      dh.ElementAt(4).Position.ShouldBe((ushort)3);


      dh.ElementAt(5).PowerProsPlayerId.ShouldBe((ushort)40);
      dh.ElementAt(5).Position.ShouldBe((ushort)7);


      dh.ElementAt(6).PowerProsPlayerId.ShouldBe((ushort)552);
      dh.ElementAt(6).Position.ShouldBe((ushort)6);


      dh.ElementAt(7).PowerProsPlayerId.ShouldBe((ushort)616);
      dh.ElementAt(7).Position.ShouldBe((ushort)4);


      dh.ElementAt(8).PowerProsPlayerId.ShouldBe((ushort)622);
      dh.ElementAt(8).Position.ShouldBe((ushort)5);
    }
  }
}
