using System.Collections.Generic;

namespace PowerUp.Entities.Players
{
  public class PositionCapabilities
  {
    public Grade Pitcher { get; set; }
    public Grade Catcher { get; set; }
    public Grade FirstBase { get; set; }
    public Grade SecondBase { get; set; }
    public Grade ThirdBase { get; set; }
    public Grade Shortstop { get; set; }
    public Grade LeftField { get; set; }
    public Grade CenterField { get; set; }
    public Grade RightField { get; set; }

    public IDictionary<Position, Grade> GetDictionary()
    {
      var dict = new Dictionary<Position, Grade>
      {
        { Position.Pitcher, Pitcher },
        { Position.Catcher, Catcher },
        { Position.FirstBase, FirstBase },
        { Position.SecondBase, SecondBase },
        { Position.ThirdBase, ThirdBase },
        { Position.Shortstop, Shortstop },
        { Position.LeftField, LeftField },
        { Position.CenterField, CenterField },
        { Position.RightField, RightField }
      };
      return dict;
    }
  }
}
