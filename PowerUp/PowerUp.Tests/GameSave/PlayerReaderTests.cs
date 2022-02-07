﻿using NUnit.Framework;
using PowerUp.GameSave;
using Shouldly;

namespace PowerUp.Tests.GameSave
{
  public class PlayerReaderTests
  {
    private const string TEST_READ_GAME_SAVE_FILE_PATH = "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/pm2maus_TEST.dat";
    private const int JASON_GIAMBI_ID = 55;
    private const int SAMMY_SPEEDSTER_ID = 20;
    private const int PAUL_PITCHER_ID = 32;

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Giambi")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Speed")]
    [TestCase(PAUL_PITCHER_ID, "Pitch")]
    public void Loads_SavedName(int playerId, string savedName)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.SavedName.ShouldBe(savedName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Giambi")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Speedster")]
    [TestCase(PAUL_PITCHER_ID, "Pitcher")]
    public void Loads_LastName(int playerId, string lastName)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.LastName.ShouldBe(lastName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Jason")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Sammy")]
    [TestCase(PAUL_PITCHER_ID, "Paul")]
    public void Loads_FirstName(int playerId, string firstName)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.FirstName.ShouldBe(firstName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, true)]
    public void Loads_IsEdited(int playerId, bool isEdited)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.IsEdited.ShouldBe(isEdited);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)25)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)999)]
    [TestCase(PAUL_PITCHER_ID, (ushort)36)]
    public void Loads_PlayerNumber(int playerId, ushort playerNumber)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.PlayerNumber.ShouldBe(playerNumber);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Loads_PlayerNumberNumberOfDigits(int playerId, ushort numberOfDigits)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.PlayerNumberNumberOfDigits.ShouldBe(numberOfDigits);
    }


    [Test]
    [TestCase(JASON_GIAMBI_ID, "25")]
    [TestCase(SAMMY_SPEEDSTER_ID, "999")]
    [TestCase(PAUL_PITCHER_ID, "036")]
    public void Loads_PlayerNumberDisplay(int playerId, string playerNumberDisplay)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.PlayerNumberDisplay.ShouldBe(playerNumberDisplay);
    }

    // TODO: Fix face
    /*
    [Test]
    [TestCase(JASON_GIAMBI_ID, 122)]
    [TestCase(SAMMY_SPEEDSTER_ID, 5)]
    [TestCase(PAUL_PITCHER_ID, 13)]
    public void Loads_Face(int playerId, int face)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.Face.ShouldBe(face);
    }
    */

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Loads_Skin(int playerId, ushort skin)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Skin.ShouldBe(skin);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Loads_AreEyesBrown(int playerId, bool areEyesBrown)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.AreEyesBrown.ShouldBe(areEyesBrown);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Loads_Bat(int playerId, ushort bat)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Bat.ShouldBe(bat);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)5)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Loads_Glove(int playerId, ushort glove)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Glove.ShouldBe(glove);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)17)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)12)]
    [TestCase(PAUL_PITCHER_ID, (ushort)6)]
    public void Loads_Hair(int playerId, ushort hair)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Hair.ShouldBe(hair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Loads_HairColor(int playerId, ushort hairColor)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HairColor.ShouldBe(hairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)2)]
    public void Loads_FacialHair(int playerId, ushort facialHair)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.FacialHair.ShouldBe(facialHair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)8)]
    [TestCase(PAUL_PITCHER_ID, (ushort)13)]
    public void Loads_FacialHairColor(int playerId, ushort facialHairColor)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.FacialHairColor.ShouldBe(facialHairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Loads_GlassesType(int playerId, ushort glassesType)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.GlassesType.ShouldBe(glassesType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)7)]
    [TestCase(PAUL_PITCHER_ID, (ushort)2)]
    public void Loads_GlassesColor(int playerId, ushort glassesColor)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.GlassesColor.ShouldBe(glassesColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Loads_EarringType(int playerId, ushort earringType)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.EarringType.ShouldBe(earringType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)8)]
    public void Loads_EarringColor(int playerId, ushort earringColor)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.EarringColor.ShouldBe(earringColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)2)]
    public void Loads_RightWristband(int playerId, ushort rightWristband)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.RightWristband.ShouldBe(rightWristband);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)5)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Loads_LeftWristband(int playerId, ushort leftWristband)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.LeftWristband.ShouldBe(leftWristband);
    }
  }
}
