using NUnit.Framework;
using PowerUp.Entities.Players;
using PowerUp.Mappers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Tests.Mappers
{
  public class PlayerMapper_PlayerToGSPlayerTests
  {
    [Test]
    public void MapToGSPlayer_ShouldMapLastName()
    {
      var player = new Player { LastName = "Sizemore" };
      var result = player.MapToGSPlayer();
      result.LastName.ShouldBe("Sizemore");
    }

    [Test]
    public void MapToGsPlayer_ShouldMapFirstName()
    {
      var player = new Player { FirstName = "Grady" };
      var result = player.MapToGSPlayer();
      result.FirstName.ShouldBe("Grady");
    }

    [Test]
    public void MapToGSPlayer_ShouldMapSavedName()
    {
      var player = new Player { SavedName = "Sizemore" };
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
      var player = new Player { PlayerType = playerType };
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
      var player = new Player { UniformNumber = uniformNumber };
      var result = player.MapToGSPlayer();
      result.PlayerNumber.ShouldBe(expectedNumberValue);
      result.PlayerNumberNumberOfDigits.ShouldBe(expectedNumberDigits);
    }
  }
}
