namespace PowerUp.Entities.Players
{
  public enum FaceType
  {
    Anime,
    Standard,
    StandardWithoutEyeColor,
    Player,
    Other
  }

  public static class FaceTypeHelpers
  {
    public static FaceType GetFaceType(int faceId)
    {
      if (faceId == 232)
        return FaceType.Anime;
      else if (faceId < 177)
        return FaceType.Player;
      else if ((faceId >= 177 && faceId < 190) || (faceId >= 196 && faceId < 209))
        return FaceType.Standard;
      else if ((faceId >= 190 && faceId < 196) || (faceId >= 209 && faceId < 213))
        return FaceType.StandardWithoutEyeColor;
      else
        return FaceType.Other;
    }
  }
}
