using NUnit.Framework;
using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Tests.GameSave.Objects.Players
{
  public class Ps2PlayerReaderTests
  {
    private readonly static string TEST_READ_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./BASLUS-21671_TEST");
    private const int JASON_GIAMBI_ID = 55;
    // Miguel Cairo in base roster
    private const int SAMMY_SPEEDSTER_ID = 20;
    // Roger Clemens in base roster
    private const int PAUL_PITCHER_ID = 32;
    // Hideki Matsui in base roster
    private const int PETE_SALTINE_ID = 583;

    private ICharacterLibrary _characterLibrary;

    [SetUp]
    public void SetUp()
    {
      _characterLibrary = TestConfig.CharacterLibrary.Value;
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, JASON_GIAMBI_ID)]
    [TestCase(SAMMY_SPEEDSTER_ID, SAMMY_SPEEDSTER_ID)]
    [TestCase(PAUL_PITCHER_ID, PAUL_PITCHER_ID)]
    [TestCase(PETE_SALTINE_ID, PETE_SALTINE_ID)]
    public void Reads_PowerProsId(int playerId, int powerProsId)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.PowerProsId.ShouldBe((ushort)powerProsId);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Giambi")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Speed")]
    [TestCase(PAUL_PITCHER_ID, "Pitch")]
    [TestCase(PETE_SALTINE_ID, "Salt")]
    public void Reads_SavedName(int playerId, string savedName)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.SavedName.ShouldBe(savedName);
    }

    [Test]
    [TestCase(28, 3840)]
    [TestCase(47, 3841)]
    [TestCase(54, 3842)]
    [TestCase(63, 3843)]
    public void Reads_SpecialSavedNameId(int playerId, int expectedId)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.SpecialSavedNameId.ShouldBe((ushort)expectedId);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Giambi")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Speedster")]
    [TestCase(PAUL_PITCHER_ID, "Pitcher")]
    [TestCase(PETE_SALTINE_ID, "Saltine")]
    public void Reads_LastName(int playerId, string lastName)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.LastName.ShouldBe(lastName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Jason")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Sammy")]
    [TestCase(PAUL_PITCHER_ID, "Paul")]
    [TestCase(PETE_SALTINE_ID, "Pete")]
    public void Reads_FirstName(int playerId, string firstName)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.FirstName.ShouldBe(firstName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsEdited(int playerId, bool isEdited)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsEdited.ShouldBe(isEdited);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)25)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)999)]
    [TestCase(PAUL_PITCHER_ID, (ushort)36)]
    [TestCase(PETE_SALTINE_ID, (ushort)1)]
    public void Reads_PlayerNumber(int playerId, ushort playerNumber)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.PlayerNumber.ShouldBe(playerNumber);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    [TestCase(PETE_SALTINE_ID, (ushort)1)]
    public void Reads_PlayerNumberNumberOfDigits(int playerId, ushort numberOfDigits)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.PlayerNumberNumberOfDigits.ShouldBe(numberOfDigits);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)102)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)180)]
    [TestCase(PAUL_PITCHER_ID, (ushort)206)]
    [TestCase(PETE_SALTINE_ID, (ushort)191)]
    public void Reads_Face(int playerId, ushort face)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Face.ShouldBe(face);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)6)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    [TestCase(PETE_SALTINE_ID, (ushort)4)]
    public void Reads_SkinAndEyes(int playerId, ushort skinAndEyes)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.SkinAndEyes.ShouldBe(skinAndEyes);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    [TestCase(PETE_SALTINE_ID, (ushort)4)]
    public void Reads_Bat(int playerId, ushort bat)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Bat.ShouldBe(bat);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)5)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    [TestCase(PETE_SALTINE_ID, (ushort)2)]
    public void Reads_Glove(int playerId, ushort glove)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Glove.ShouldBe(glove);
    }
  }
}
