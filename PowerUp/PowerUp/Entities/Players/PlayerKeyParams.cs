using PowerUp.Databases;

namespace PowerUp.Entities.Players
{
  public class PlayerKeyParams : KeyParams
  {
    public string? Type { get; set; }
    public string? ImportSource { get; set; }
    public int? SourceId { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
  }
}
