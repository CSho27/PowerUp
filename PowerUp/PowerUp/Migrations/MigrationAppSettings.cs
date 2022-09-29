using PowerUp.Entities.AppSettings;

namespace PowerUp.Migrations
{
  [MigrationTypeFor(typeof(AppSettings))]
  public class MigrationAppSettings
  {
    public string? GameSaveManagerDirectoryPath { get; set; }
  }
}
