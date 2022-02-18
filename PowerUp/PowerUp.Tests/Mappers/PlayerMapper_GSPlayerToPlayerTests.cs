using NUnit.Framework;
using PowerUp.Entities.Players;
using PowerUp.GameSave;
using PowerUp.Mappers;
using Shouldly;
using System;

namespace PowerUp.Tests.Mappers
{
  public class PlayerMapper_GSPlayerToPlayerTests
  {
    private PlayerMappingParameters mappingParameters;

    [SetUp]
    public void SetUp()
    {
      mappingParameters = new PlayerMappingParameters
      {
        PlayerType = PlayerType.Base,
        ImportSource = "Roster1",
        Year = 2007,
        BirthDate = DateOnly.Parse("1/1/1980"),
      };
    }

    [Test] 
    public void MapToPlayer_ShouldMapLastName()
    {
      var gsPlayer = new GSPlayer { LastName = "Sizemore" };
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.LastName.ShouldBe("Sizemore");
    }

    [Test]
    public void MapToPlayer_ShouldMapFirstName()
    {
      var gsPlayer = new GSPlayer { FirstName = "Grady" };
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.FirstName.ShouldBe("Grady");
    }

    [Test]
    public void MapToPlayer_ShouldMapSavedName()
    {
      var gsPlayer = new GSPlayer { SavedName = "Sizemore" };
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.SavedName.ShouldBe("Sizemore");
    }

    [Test]
    [TestCase(PlayerType.Base)]
    [TestCase(PlayerType.Imported)]
    [TestCase(PlayerType.Generated)]
    [TestCase(PlayerType.Custom)]
    public void MapToPlayer_ShouldUseTypeFromParameters(PlayerType playerType)
    {
      var gsPlayer = new GSPlayer();
      mappingParameters.PlayerType = playerType;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PlayerType.ShouldBe(playerType);
    }

    [Test]
    [TestCase(PlayerType.Base, null)]
    [TestCase(PlayerType.Imported, "Roster1")]
    [TestCase(PlayerType.Generated, null)]
    [TestCase(PlayerType.Custom, null)]
    public void MapToPlayer_ShouldIncludeImportSourceOnlyForImported(PlayerType playerType, string importSource)
    {
      var gsPlayer = new GSPlayer();
      mappingParameters.PlayerType = playerType;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.ImportSource.ShouldBe(importSource);
    }

    [Test]
    [TestCase(PlayerType.Base, null)]
    [TestCase(PlayerType.Imported, "Roster1")]
    [TestCase(PlayerType.Generated, null)]
    [TestCase(PlayerType.Custom, null)]
    public void MapToPlayer_EditedPlayersShouldBeCustomType(PlayerType playerType, string importSource)
    {
      var gsPlayer = new GSPlayer() { IsEdited = true };
      mappingParameters.PlayerType = playerType;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.ImportSource.ShouldBe(importSource);
      result.PlayerType.ShouldBe(PlayerType.Custom);
    }

    [Test]
    [TestCase(PlayerType.Base, null)]
    [TestCase(PlayerType.Imported, null)]
    [TestCase(PlayerType.Generated, 2007)]
    [TestCase(PlayerType.Custom, null)]
    public void MapToPlayer_ShouldIncludeYearOnlyForGenerated(PlayerType playerType, int? year)
    {
      var gsPlayer = new GSPlayer();
      mappingParameters.PlayerType = playerType;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.Year.ShouldBe(year);
    }

    [Test]
    [TestCase(PlayerType.Base, null)]
    [TestCase(PlayerType.Imported, null)]
    [TestCase(PlayerType.Generated, "1/1/1980")]
    [TestCase(PlayerType.Custom, null)]
    public void MapToPlayer_ShouldIncludeBirthDateOnlyForGenerated(PlayerType playerType, string birthDateString)
    {
      var gsPlayer = new GSPlayer();
      mappingParameters.PlayerType = playerType;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      var birthDate = birthDateString != null
        ? DateOnly.Parse(birthDateString)
        : (DateOnly?)null;
      result.BirthDate.ShouldBe(birthDate);
    }

    [Test]
    [TestCase((ushort)0, (ushort)1, "0")]
    [TestCase((ushort)12, (ushort)2, "12")]
    [TestCase((ushort)99, (ushort)3, "099")]
    [TestCase((ushort)999, (ushort)3, "999")]
    public void MapToPlayer_ShoudMapUniformNumber(ushort numberValue, ushort numberDigits, string expectedUniformNumber)
    {
      var gsPlayer = new GSPlayer { PlayerNumber = numberValue, PlayerNumberNumberOfDigits = numberDigits };
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.UniformNumber.ShouldBe(expectedUniformNumber);
    }

    [Test]
    public void MapToPlayer_ShouldMapPrimaryPosition()
    {
      var gsPlayer = new GSPlayer { PrimaryPosition = 8 };
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PrimaryPosition.ShouldBe(Position.CenterField);
    }
  }
}
