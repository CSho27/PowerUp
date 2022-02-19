using PowerUp.Entities;
using PowerUp.Entities.Teams;
using PowerUp.GameSave.Objects.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Mappers
{
  public class TeamMappingParameters
  {
    public EntitySourceType SourceType { get; set; }
    public string? ImportSource { get; set; }
    public int? Year { get; set; }
  }

  public static class TeamMapper
  {
    public static Team MapToTeam(this GSTeam gsTeam, TeamMappingParameters parameters)
    {
      return new Team
      {
        SourceType = parameters.SourceType,
        Name = gsTeam.Name!
      }
    }
  }
}
