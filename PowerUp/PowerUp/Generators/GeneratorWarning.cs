namespace PowerUp.Generators
{
  public class GeneratorWarning
  {
    public string PropertyKey { get; set; }
    public string ErrorKey { get; set; }
    public string Message { get; set; }

    public GeneratorWarning(string propertyKey, string errorKey, string message)
    {
      PropertyKey = propertyKey;
      ErrorKey = errorKey;
      Message = message;
    }

    public static GeneratorWarning NoPitchingStats(string propertyKey) => new GeneratorWarning(propertyKey, "NoPitchingStats", "Pitching stats not found");
    public static GeneratorWarning InsufficientPitchingStats(string propertyKey) => new GeneratorWarning(propertyKey, "InsufficentPitchingStats", "Insufficient pitching stats found");

    public static GeneratorWarning NoHittingStats(string propertyKey) => new GeneratorWarning(propertyKey, "NoHittingStats", "Hitting stats not found");
    public static GeneratorWarning InsufficientHittingStats(string propertyKey) => new GeneratorWarning(propertyKey, "InsufficentHittingStats", "Insufficient hitting stats found");

    public static GeneratorWarning NoFieldingStats(string propertyKey) => new GeneratorWarning(propertyKey, "NoFieldingStats", "Fielding stats not found");
    public static GeneratorWarning InsufficientFieldingStats(string propertyKey) => new GeneratorWarning(propertyKey, "InsufficentFieldingStats", "Insufficient fielding stats found");
  }
}
