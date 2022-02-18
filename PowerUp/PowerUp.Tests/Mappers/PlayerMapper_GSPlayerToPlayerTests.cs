﻿using NUnit.Framework;
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
    private GSPlayer gsPlayer;

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

      gsPlayer = new GSPlayer { 
        IsEdited = false, 
        PrimaryPosition = 8,
        IsStarter = false,
        IsReliever = false,
        IsCloser = false,
        VoiceId = 0,
        BattingSide = 0,
        BattingForm = 0,
        ThrowsLefty = false,
        PitchingForm = 0,
        PitcherCapability = 0,
        CatcherCapability = 0,
        FirstBaseCapability = 0,
        SecondBaseCapability = 0,
        ThirdBaseCapability = 0,
        ShortstopCapability = 0,
        LeftFieldCapability = 0,
        CenterFieldCapability = 0,
        RightFieldCapability = 0,
      };
    }

    [Test] 
    public void MapToPlayer_ShouldMapLastName()
    {
      gsPlayer.LastName = "Sizemore";
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.LastName.ShouldBe("Sizemore");
    }

    [Test]
    public void MapToPlayer_ShouldMapFirstName()
    {
      gsPlayer.FirstName = "Grady";
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.FirstName.ShouldBe("Grady");
    }

    [Test]
    public void MapToPlayer_ShouldMapSavedName()
    {
      gsPlayer.SavedName = "Sizemore";
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
      gsPlayer.IsEdited = true;
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
    public void MapToPlayer_ShouldMapUniformNumber(ushort numberValue, ushort numberDigits, string expectedUniformNumber)
    {
      gsPlayer.PlayerNumber = numberValue;
      gsPlayer.PlayerNumberNumberOfDigits = numberDigits;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.UniformNumber.ShouldBe(expectedUniformNumber);
    }

    [Test]
    public void MapToPlayer_ShouldMapPrimaryPosition()
    {
      gsPlayer.PrimaryPosition = 8;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PrimaryPosition.ShouldBe(Position.CenterField);
    }

    [Test]
    [TestCase(false, false, false, PitcherType.SwingMan)]
    [TestCase(true, false, false, PitcherType.Starter)]
    [TestCase(false, true, false, PitcherType.Reliever)]
    [TestCase(false, false, true, PitcherType.Closer)]
    public void MapToPlayer_ShouldMapPitcherType(bool isStarter, bool isReliever, bool isCloser, PitcherType expectedPitcherType)
    {
      gsPlayer.IsStarter = isStarter;
      gsPlayer.IsReliever = isReliever;
      gsPlayer.IsCloser = isCloser;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PitcherType.ShouldBe(expectedPitcherType);
    }

    [Test]
    public void MapToPlayer_ShouldMapVoiceId()
    {
      gsPlayer.VoiceId = 35038;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.VoiceId.ShouldBe(35038);
    }

    [Test]
    public void MapToPlayer_ShouldMapBattingSide()
    {
      gsPlayer.BattingSide = 2;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.BattingSide.ShouldBe(BattingSide.Switch);
    }

    [Test]
    public void MapToPlayer_ShouldMapBattingStanceId()
    {
      gsPlayer.BattingForm = 3;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.BattingStanceId.ShouldBe(3);
    }

    [Test]
    public void MapToPlayer_ShouldMapThrowingSide()
    {
      gsPlayer.ThrowsLefty = true;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.ThrowingSide.ShouldBe(ThrowingSide.Left);
    }

    [Test]
    public void MapToPlayer_ShouldMapThrowingMechanicsId()
    {
      gsPlayer.PitchingForm = 3;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PitchingMechanicsId.ShouldBe(3);
    }

    [Test]
    public void MapToPlayer_ShouldMapPitcherCapability()
    {
      gsPlayer.PitcherCapability = 3;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PositonCapabilities.Pitcher.ShouldBe(Grade.E);
    }

    [Test]
    public void MapToPlayer_ShouldMapCatcherCapability()
    {
      gsPlayer.CatcherCapability = 2;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PositonCapabilities.Catcher.ShouldBe(Grade.F);
    }

    [Test]
    public void MapToPlayer_ShouldMapFirstBaseCapability()
    {
      gsPlayer.FirstBaseCapability = 1;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PositonCapabilities.FirstBase.ShouldBe(Grade.G);
    }

    [Test]
    public void MapToPlayer_ShouldMapSecondBaseCapability()
    {
      gsPlayer.SecondBaseCapability = 4;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PositonCapabilities.SecondBase.ShouldBe(Grade.D);
    }

    [Test]
    public void MapToPlayer_ShouldMapThirdBaseCapability()
    {
      gsPlayer.ThirdBaseCapability = 5;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PositonCapabilities.ThirdBase.ShouldBe(Grade.C);
    }

    [Test]
    public void MapToPlayer_ShouldMapShortstopCapability()
    {
      gsPlayer.ShortstopCapability = 6;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PositonCapabilities.Shortstop.ShouldBe(Grade.B);
    }

    [Test]
    public void MapToPlayer_ShouldMapLeftFieldCapability()
    {
      gsPlayer.LeftFieldCapability = 7;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PositonCapabilities.LeftField.ShouldBe(Grade.A);
    }

    [Test]
    public void MapToPlayer_ShouldMapCenterFieldCapability()
    {
      gsPlayer.CenterFieldCapability = 6;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PositonCapabilities.CenterField.ShouldBe(Grade.B);
    }

    [Test]
    public void MapToPlayer_ShouldMapRightFieldCapability()
    {
      gsPlayer.RightFieldCapability = 5;
      var result = gsPlayer.MapToPlayer(mappingParameters);
      result.PositonCapabilities.RightField.ShouldBe(Grade.C);
    }
  }
}
