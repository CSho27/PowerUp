using PowerUp.Entities.Players;
using PowerUp.Libraries;
using System;

namespace PowerUp.Generators
{
  public interface ISkinColorGuesser
  {
    public SkinColor GuessSkinColor(int year, string? birthCountry);
  }

  public class SkinColorGuesser : ISkinColorGuesser
  {
    private readonly ICountryAndSkinColorLibrary _countryAndSkinColorLibrary;

    public SkinColorGuesser(ICountryAndSkinColorLibrary countryAndSkinColorLibrary)
    {
      _countryAndSkinColorLibrary = countryAndSkinColorLibrary;
    }
        
    public SkinColor GuessSkinColor(int year, string? birthCountry)
    {
      if (birthCountry == "United States of America" || birthCountry == "USA" || birthCountry == null)
        return GuessAmericanSkinColorForYear(year);

      var skinColorForCountry = _countryAndSkinColorLibrary[birthCountry];
      return skinColorForCountry.HasValue
        ? (SkinColor)(skinColorForCountry - 1)
        : SkinColor.Three;
    }

    private SkinColor GuessAmericanSkinColorForYear(int year)
    {
      var rand = Random.Shared.NextDouble();

      if (year < 1947)
        return SkinColor.One;
      else if (year < 1957)
        return ForPercentagesAndRandomNumber(.9, .06, .04, rand);
      else if (year < 1967)
        return ForPercentagesAndRandomNumber(.8, .15, .05, rand);
      else if (year < 1977)
        return ForPercentagesAndRandomNumber(.7, .2, .1, rand);
      else if (year < 1987)
        return ForPercentagesAndRandomNumber(.68, .22, .1, rand);
      else if (year < 1997)
        return ForPercentagesAndRandomNumber(.75, .15, .1, rand);
      else if (year < 2007)
        return ForPercentagesAndRandomNumber(.8, .08, .08, rand);
      else if (year < 2017)
        return ForPercentagesAndRandomNumber(.85, .07, .8, rand);
      else
        return ForPercentagesAndRandomNumber(.85, .05, .1, rand);
    }

    private SkinColor ForPercentagesAndRandomNumber(double white, double africanAmerican, double latino, double rand)
    {
      var rand2 = Random.Shared.NextDouble();

      if (rand < white)
        return SkinColor.One;
      else if (rand < white + africanAmerican)
        return rand2 > .5
          ? SkinColor.Five
          : SkinColor.Four;
      else if (rand < white + africanAmerican + latino)
        return rand2 > .5
          ? SkinColor.Three
          : SkinColor.Four;
      else
        return SkinColor.Two;
    }
  }
}
