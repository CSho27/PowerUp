using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Mappers.Players
{
  public static class AppearanceMapper
  {
    public static Appearance GetAppearance(GSPlayer player)
    {
      var ppFaceId = player.Face!.Value;
      var faceHasThinEyebrows = ppFaceId > 194 && ppFaceId < 213;
      var faceType = FaceTypeHelpers.GetFaceType(ppFaceId);

      return new Appearance
      {
        // Adjusts thin eyebrow ids to be their thick eyebrow alternatives so that we can store all that info in separate values
        FaceId = faceHasThinEyebrows
          ? ppFaceId - 18
          : ppFaceId,
        EyebrowThickness = faceType == FaceType.Standard || faceType == FaceType.StandardWithoutEyeColor
          ? faceHasThinEyebrows ? EyebrowThickness.Thin : EyebrowThickness.Thick
          : null,
        SkinColor = faceType == FaceType.Player || faceType == FaceType.Other
          ? null
          : (SkinColor)(player.SkinAndEyes!.Value % 5),
        EyeColor = faceType == FaceType.Standard || faceType == FaceType.Anime
          ? player.SkinAndEyes > 4 ? EyeColor.Brown : EyeColor.Blue
          : null,
        HairStyle = player.Hair != 0
          ? (HairStyle)player.Hair!.Value
          : null,
        HairColor = player.Hair != 0
          ? (HairColor)player.HairColor!.Value
          : null,
        FacialHairStyle = player.FacialHair != 0
          ? (FacialHairStyle)player.FacialHair!.Value
          : null,
        FacialHairColor = player.FacialHair != 0
          ? (HairColor)player.FacialHairColor!.Value
          : null,
        BatColor = (BatColor)player.Bat!.Value,
        GloveColor = (GloveColor)player.Glove!.Value,
        EyewearType = player.EyewearType != 0
          ? (EyewearType)player.EyewearType!.Value
          : null,
        EyewearFrameColor = player.EyewearType > 1
          ? (EyewearFrameColor)(player.EyewearColor!.Value/3)
          : null,
        EyewearLensColor = player.EyewearType > 1 
          ? (EyewearLensColor)(player.EyewearColor!.Value%3)
          : null,
        EarringSide = player.EarringSide != 0
          ? (EarringSide)player.EarringSide!.Value
          : null,
        EarringColor = player.EarringSide != 0
          ? (AccessoryColor)player.EarringColor!.Value
          : null,
        RightWristbandColor = player.RightWristband != 0
          ? (AccessoryColor)(player.RightWristband!.Value - 1)
          : null,
        LeftWristbandColor = player.LeftWristband != 0
          ? (AccessoryColor)(player.LeftWristband!.Value -1)
          : null
      };
    }
  }
}
