using PowerUp.Generators;

namespace PowerUp.Migrations
{
  [MigrationTypeFor(typeof(GeneratorWarning))]
  public class MigrationGeneratorWarning
  {
    public string PropertyKey { get; set; }
    public string ErrorKey { get; set; }
    public string Message { get; set; }
  }
}
