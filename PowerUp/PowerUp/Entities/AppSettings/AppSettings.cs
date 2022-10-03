using PowerUp.Databases;
using PowerUp.Migrations;

namespace PowerUp.Entities.AppSettings
{
  [MigrationIgnore]
  public class AppSettings : Entity<AppSettings>
  {
    public string? GameSaveManagerDirectoryPath { get; set; }
  }
}
