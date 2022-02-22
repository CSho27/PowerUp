namespace PowerUp.Entities.Players
{
  public enum PitcherType
  {
    [Abbrev("SM"), DisplayName("Swing-Man")]
    SwingMan,
    [Abbrev("SP"), DisplayName("Starter")]
    Starter,
    [Abbrev("RP"), DisplayName("Reliever")]
    Reliever,
    [Abbrev("CP"), DisplayName("Closer")]
    Closer
  }
}
