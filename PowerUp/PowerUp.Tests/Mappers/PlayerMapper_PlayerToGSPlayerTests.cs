using NUnit.Framework;
using PowerUp.Entities.Players;
using PowerUp.Mappers;
using Shouldly;

namespace PowerUp.Tests.Mappers
{
  public class PlayerMapper_PlayerToGSPlayerTests
  {
    private Player player;

    [SetUp]
    public void SetUp()
    {
      player = new Player() { UniformNumber = "24" };
    }

    [Test]
    public void MapToGSPlayer_ShouldMapLastName()
    {
      player.LastName = "Sizemore";
      var result = player.MapToGSPlayer();
      result.LastName.ShouldBe("Sizemore");
    }

    [Test]
    public void MapToGsPlayer_ShouldMapFirstName()
    {
      player.FirstName = "Grady";
      var result = player.MapToGSPlayer();
      result.FirstName.ShouldBe("Grady");
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSavedName()
    {
      player.SavedName = "Sizemore";
      var result = player.MapToGSPlayer();
      result.SavedName.ShouldBe("Sizemore");
    }

    [Test]
    [TestCase(PlayerType.Base)]
    [TestCase(PlayerType.Imported)]
    [TestCase(PlayerType.Generated)]
    [TestCase(PlayerType.Custom)]
    public void MapToGSPlayer_ShouldBeMarkedAsEditedForCustomPlayers(PlayerType playerType)
    {
      player.PlayerType = playerType;
      var result = player.MapToGSPlayer();
      result.IsEdited.ShouldBe(playerType == PlayerType.Custom);
    }

    [Test]
    [TestCase("0", (ushort)0, (ushort)1)]
    [TestCase("12", (ushort)12, (ushort)2)]
    [TestCase("099", (ushort)99, (ushort)3)]
    [TestCase("999", (ushort)999, (ushort)3)]
    public void MapToGSPlayer_ShoudMapUniformNumber(string uniformNumber, ushort expectedNumberValue, ushort expectedNumberDigits)
    {
      player.UniformNumber = uniformNumber;
      var result = player.MapToGSPlayer();
      result.PlayerNumber.ShouldBe(expectedNumberValue);
      result.PlayerNumberNumberOfDigits.ShouldBe(expectedNumberDigits);
    }

    [Test]
    public void MapToGSPlayer_ShouldMapPrimaryPosition()
    {
      player.PrimaryPosition = Position.CenterField;
      var result = player.MapToGSPlayer();
      result.PrimaryPosition.ShouldBe((ushort)8);
    }
  }
}
