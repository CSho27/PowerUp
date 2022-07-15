using NUnit.Framework;
using PowerUp.Fetchers;
using Shouldly;
using System.Collections.Generic;

namespace PowerUp.Tests
{
  public class UrlBuilderTests
  {
    [Test]
    public void Build_BuildsAUrlWithNoParameters()
    {
      var result = UrlBuilder.Build("http://testUrl", new Dictionary<string, string>());
      result.ShouldBe("http://testUrl");
    }

    [Test]
    public void Build_BuildsAUrlWithNoParameterObject()
    {
      var result = UrlBuilder.Build("http://testUrl");
      result.ShouldBe("http://testUrl");
    }

    [Test]
    public void Build_BuildsAUrlWith1Parameter()
    {
      var result = UrlBuilder.Build("http://testUrl", new Dictionary<string, string>() { { "param1", "value1" } });
      result.ShouldBe("http://testUrl?param1=value1");
    }

    [Test]
    public void Build_BuildsAUrlWith1ParameterObject()
    {
      var result = UrlBuilder.Build("http://testUrl", new { param1 = "value1" } );
      result.ShouldBe("http://testUrl?param1=value1");
    }

    [Test]
    public void Build_BuildsAUrlWithMultipleParameter()
    {
      var result = UrlBuilder.Build(
        "http://testUrl", 
        new Dictionary<string, string>() { 
          { "param1", "value1" },
          { "param2", "value2" },
          { "param3", "value3" }
        }
      );
      result.ShouldBe("http://testUrl?param1=value1&param2=value2&param3=value3");
    }

    [Test]
    public void Build_BuildsAUrlWithMultiParameterObject()
    {
      var result = UrlBuilder.Build(
        "http://testUrl", 
        new { 
          param1 = "value1",
          param2 = "value2",
          param3 = "value3"
        }
      );
      result.ShouldBe("http://testUrl?param1=value1&param2=value2&param3=value3");
    }

    [Test]
    public void Build_BuildsMultiPartUrl()
    {
      var result = UrlBuilder.Build(
        new[]
        {
          "http://testUrl",
          "testPart1",
          "testPart2"
        },
        new
        {
          param1 = "value1",
          param2 = "value2",
          param3 = "value3"
        }
      );
      result.ShouldBe("http://testUrl/testPart1/testPart2?param1=value1&param2=value2&param3=value3");
    }
  }
}
