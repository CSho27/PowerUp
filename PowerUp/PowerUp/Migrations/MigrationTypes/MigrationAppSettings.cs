using PowerUp.Entities.AppSettings;

namespace PowerUp.Migrations.MigrationTypes
{
  [MigrationTypeFor(typeof(AppSettings))]
  public class MigrationAppSettings
  {
    public string? GameSaveManagerDirectoryPath { get; set; }
  }
}
