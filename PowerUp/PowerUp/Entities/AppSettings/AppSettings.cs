using PowerUp.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Entities.AppSettings
{
  public class AppSettings : Entity<AppSettings>
  {
    public string? GameSaveManagerDirectoryPath { get; set; }
  }
}
