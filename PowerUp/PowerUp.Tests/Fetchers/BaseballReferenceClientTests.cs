using NUnit.Framework;
using PowerUp.Fetchers.BaseballReference;
using Shouldly;
using System.Threading.Tasks;

namespace PowerUp.Tests.Fetchers
{
  public class BaseballReferenceClientTests
  {
    private readonly IBaseballReferenceClient _client = new BaseballReferenceClient();

    [Test]
    public void GetsPlayerList_GetsRipken()
    {
      Task.Run(async () =>
      {
        var result = await _client.GetBaseballReferenceIdFor("Cal", "Ripken Jr.", 1981);
        result.ShouldBe("ripkeca01");
      }).GetAwaiter().GetResult();
    }


    [Test]
    public void GetsPlayerList_GetsStanton()
    {
      Task.Run(async () =>
      {
        var result = await _client.GetBaseballReferenceIdFor("Giancarlo", "Stanton", 2010);
        result.ShouldBe("stantmi03");
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetsPlayerList_GetsBusterBray()
    {
      Task.Run(async () =>
      {
        var result = await _client.GetBaseballReferenceIdFor("Buster", "Bray", 1941);
        result.ShouldBe("braybu01");
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetsPlayerList_GetsMaxScherzer()
    {
      Task.Run(async () =>
      {
        var result = await _client.GetBaseballReferenceIdFor("Max", "Scherzer", 2008);
        result.ShouldBe("scherma01");
      }).GetAwaiter().GetResult();
    }
  }
}
