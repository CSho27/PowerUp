namespace PowerUp.Entities.Teams
{
  public class PlayerRoleDefinition
  {
    public int PlayerId { get; set; }
    public bool IsAAA { get; set; }
    public bool IsPinchHitter { get; set; }
    public bool IsPinchRunner { get; set; }
    public bool IsDefensiveReplacement { get; set; }
    public bool IsDefensiveLiability { get; set; }

    public PitcherRole PitcherRole { get; set; }

    public PlayerRoleDefinition(int playerId)
    {
      PlayerId = playerId;
    }
  }

  public enum PitcherRole
  {
    Starter,
    [DisplayName("Swing Man")]
    SwingMan,
    [DisplayName("Long Reliever")]
    LongReliever,
    [DisplayName("Middle Reliever")]
    MiddleReliever,
    [DisplayName("Situational Lefty")]
    SituationalLefty,
    [DisplayName("Mop-up Man")]
    MopUpMan,
    [DisplayName("Setup Man")]
    SetupMan,
    Closer
  }
}
