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
    private Dictionary<PlayerDatabaseKeys, ushort> idsByKey;

    [SetUp]
    public void SetUp()
    {
      var sizemore = PlayerDatabaseKeys.ForBasePlayer("Sizemore", "Grady");
      var nixon = PlayerDatabaseKeys.ForBasePlayer("Nixon", "Trot");
      var hafner = PlayerDatabaseKeys.ForBasePlayer("Hafner", "Travis");
      var martinez = PlayerDatabaseKeys.ForBasePlayer("Martinez", "Victor");
      var blake = PlayerDatabaseKeys.ForBasePlayer("Blake", "Casey");
      var dellucci = PlayerDatabaseKeys.ForBasePlayer("Dellucci", "David");
      var peralta = PlayerDatabaseKeys.ForBasePlayer("Peralta", "Jhonny");
      var barfield = PlayerDatabaseKeys.ForBasePlayer("Barfield", "Josh");
      var marte = PlayerDatabaseKeys.ForBasePlayer("Marte", "Andy");

      idsByKey = new Dictionary<PlayerDatabaseKeys, ushort>()
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
          (sizemore, Position.CenterField),
          (nixon, Position.RightField),
          (hafner, Position.FirstBase),
          (martinez, Position.Catcher),
          (dellucci, Position.LeftField),
          (peralta, Position.Shortstop),
          (barfield, Position.SecondBase),
          (marte, Position.ThirdBase),
          (null, Position.Pitcher)
        },
        DHLineup = new[]
        {
          (sizemore, Position.CenterField),
          (nixon, Position.RightField),
          (hafner, Position.DesignatedHitter),
          (martinez, Position.Catcher),
          (blake, Position.FirstBase),
          (dellucci, Position.LeftField),
          (peralta, Position.Shortstop),
          (barfield, Position.SecondBase),
          (marte, Position.ThirdBase),
        }
      };
    }

    [Test]
    public void MapToGSLineup_MapsNoDHLineup()
    {
      var result = team.MapToGSLineup(idsByKey);
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
      var result = team.MapToGSLineup(idsByKey);
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
