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
      var dict = new Dictionary<Position, Grade>();
      dict.Add(Position.Pitcher, Pitcher);
      dict.Add(Position.Catcher, Catcher);
      dict.Add(Position.FirstBase, FirstBase);
      dict.Add(Position.SecondBase, SecondBase);
      dict.Add(Position.ThirdBase, ThirdBase);
      dict.Add(Position.Shortstop, Shortstop);
      dict.Add(Position.LeftField, LeftField);
      dict.Add(Position.CenterField, CenterField);
      dict.Add(Position.RightField, RightField);
      return dict;
    }
  }
}
