using NUnit.Framework;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.Mappers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Tests.Mappers.Teams
{
  public class TeamMapper_ToGSLineupDefinitionTests
  {
    private Team team;
    private Dictionary<int, ushort> ppIdsById;

    [SetUp]
    public void SetUp()
    {
      var sizemore = 24;
      var nixon = 7;
      var hafner = 48;
      var martinez = 41;
      var blake = 3;
      var dellucci = 20;
      var peralta = 2;
      var barfield = 17;
      var marte = 5;

      ppIdsById = new Dictionary<int, ushort>()
      {
        { sizemore, 1 },
        { nixon, 2 },
        { hafner, 3 },
        { martinez, 4 },
        { blake, 5 },
        { dellucci, 6 },
        { peralta, 7 },
        { barfield, 8 },
        { marte, 9 },
      };

      team = new Team()
      {
        NoDHLineup = new[]
        {
          new LineupSlot { PlayerId = sizemore, Position = Position.CenterField },
          new LineupSlot { PlayerId = nixon, Position = Position.RightField },
          new LineupSlot { PlayerId = hafner, Position = Position.FirstBase },
          new LineupSlot { PlayerId = martinez, Position = Position.Catcher },
          new LineupSlot { PlayerId = dellucci, Position = Position.LeftField },
          new LineupSlot { PlayerId = peralta, Position = Position.Shortstop },
          new LineupSlot { PlayerId = barfield, Position = Position.SecondBase },
          new LineupSlot { PlayerId = marte, Position = Position.ThirdBase },
          new LineupSlot { PlayerId = null, Position = Position.Pitcher  }
        },
        DHLineup = new[]
        {
          new LineupSlot { PlayerId = sizemore, Position = Position.CenterField },
          new LineupSlot { PlayerId = nixon, Position = Position.RightField },
          new LineupSlot { PlayerId = hafner, Position = Position.DesignatedHitter },
          new LineupSlot { PlayerId = martinez, Position = Position.Catcher },
          new LineupSlot { PlayerId = blake, Position = Position.FirstBase },
          new LineupSlot { PlayerId = dellucci, Position = Position.LeftField },
          new LineupSlot { PlayerId = peralta, Position = Position.Shortstop },
          new LineupSlot { PlayerId = barfield, Position = Position.SecondBase },
          new LineupSlot { PlayerId = marte, Position = Position.ThirdBase },
        }
      };
    }

    [Test]
    public void MapToGSLineup_MapsNoDHLineup()
    {
      var result = team.MapToGSLineup(ppIdsById);
      var noDH = result.NoDHLineup;

      noDH.ElementAt(0).PowerProsPlayerId.ShouldBe((ushort)1);
      noDH.ElementAt(0).Position.ShouldBe((ushort)8);

      noDH.ElementAt(1).PowerProsPlayerId.ShouldBe((ushort)2);
      noDH.ElementAt(1).Position.ShouldBe((ushort)9);


      noDH.ElementAt(2).PowerProsPlayerId.ShouldBe((ushort)3);
      noDH.ElementAt(2).Position.ShouldBe((ushort)3);


      noDH.ElementAt(3).PowerProsPlayerId.ShouldBe((ushort)4);
      noDH.ElementAt(3).Position.ShouldBe((ushort)2);


      noDH.ElementAt(4).PowerProsPlayerId.ShouldBe((ushort)6);
      noDH.ElementAt(4).Position.ShouldBe((ushort)7);


      noDH.ElementAt(5).PowerProsPlayerId.ShouldBe((ushort)7);
      noDH.ElementAt(5).Position.ShouldBe((ushort)6);


      noDH.ElementAt(6).PowerProsPlayerId.ShouldBe((ushort)8);
      noDH.ElementAt(6).Position.ShouldBe((ushort)4);


      noDH.ElementAt(7).PowerProsPlayerId.ShouldBe((ushort)9);
      noDH.ElementAt(7).Position.ShouldBe((ushort)5);


      noDH.ElementAt(8).PowerProsPlayerId.ShouldBe((ushort)0);
      noDH.ElementAt(8).Position.ShouldBe((ushort)0);
    }

    [Test]
    public void MapToGSLineup_MapsDHLineup()
    {
      var result = team.MapToGSLineup(ppIdsById);
      var dh = result.DHLineup;

      dh.ElementAt(0).PowerProsPlayerId.ShouldBe((ushort)1);
      dh.ElementAt(0).Position.ShouldBe((ushort)8);

      dh.ElementAt(1).PowerProsPlayerId.ShouldBe((ushort)2);
      dh.ElementAt(1).Position.ShouldBe((ushort)9);


      dh.ElementAt(2).PowerProsPlayerId.ShouldBe((ushort)3);
      dh.ElementAt(2).Position.ShouldBe((ushort)10);


      dh.ElementAt(3).PowerProsPlayerId.ShouldBe((ushort)4);
      dh.ElementAt(3).Position.ShouldBe((ushort)2);

      dh.ElementAt(4).PowerProsPlayerId.ShouldBe((ushort)5);
      dh.ElementAt(4).Position.ShouldBe((ushort)3);


      dh.ElementAt(5).PowerProsPlayerId.ShouldBe((ushort)6);
      dh.ElementAt(5).Position.ShouldBe((ushort)7);


      dh.ElementAt(6).PowerProsPlayerId.ShouldBe((ushort)7);
      dh.ElementAt(6).Position.ShouldBe((ushort)6);


      dh.ElementAt(7).PowerProsPlayerId.ShouldBe((ushort)8);
      dh.ElementAt(7).Position.ShouldBe((ushort)4);


      dh.ElementAt(8).PowerProsPlayerId.ShouldBe((ushort)9);
      dh.ElementAt(8).Position.ShouldBe((ushort)5);
    }
  }
}
