using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;

namespace PowerUp.Mappers.Players
{
  public static class AppearanceMapper
  {
    public const int THICK_EYEBROW_OFFSET = 18;
    public const int EYE_COLOR_OFFSET = 5;
    public const int EYEWEAR_OFFSET = 3;

    public static Appearance GetAppearance(IGSPlayer player)
    {
      var ppFaceId = player.Face!.Value;
      var faceHasThinEyebrows = ppFaceId >= 195 && ppFaceId < 213;
      var faceType = FaceTypeHelpers.GetFaceType(ppFaceId);

      return new Appearance
      {
        // Adjusts thin eyebrow ids to be their thick eyebrow alternatives so that we can store all that info in separate values
        FaceId = faceHasThinEyebrows
          ? ppFaceId - THICK_EYEBROW_OFFSET
          : ppFaceId,
        EyebrowThickness = faceType == FaceType.Standard || faceType == FaceType.StandardWithoutEyeColor
          ? faceHasThinEyebrows ? EyebrowThickness.Thin : EyebrowThickness.Thick
          : null,
        SkinColor = faceType == FaceType.Player || faceType == FaceType.Other
          ? null
          : (SkinColor)(player.SkinAndEyes!.Value % EYE_COLOR_OFFSET),
        EyeColor = faceType == FaceType.Standard || faceType == FaceType.Anime
          ? player.SkinAndEyes >= EYE_COLOR_OFFSET ? EyeColor.Brown : EyeColor.Blue
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
          ? (EyewearFrameColor)(player.EyewearColor!.Value / EYEWEAR_OFFSET)
          : null,
        EyewearLensColor = player.EyewearType > 1 
          ? (EyewearLensColor)(player.EyewearColor!.Value % EYEWEAR_OFFSET)
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
