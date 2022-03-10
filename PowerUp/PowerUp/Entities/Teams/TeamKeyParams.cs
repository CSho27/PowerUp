using PowerUp.Databases;

namespace PowerUp.Entities.Teams
{
  public class TeamKeyParams : KeyParams
  {
    public string? Type { get; set; }
    public string? ImportSource { get; set; }
    public int? Year { get; set; }
    public string? Name { get; set; }
  }
}
