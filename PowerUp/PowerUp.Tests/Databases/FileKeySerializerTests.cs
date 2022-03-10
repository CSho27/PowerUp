using NUnit.Framework;
using PowerUp.Databases;
using Shouldly;
using System;

namespace PowerUp.Tests.Databases
{
  public class FileKeySerializerTests
  {
    [Test]
    public void Serialize_ShouldSerializeSimpleObject()
    {
      var result = FileKeySerializer.Serialize(new { Id = 1, Name = "Bob", Type = TestEnum.One });
      result.ShouldBe("1_Bob_One");
    }

    [Test]
    public void Serialize_ShouldAlwaysSerializeWithIdFirst()
    {
      var result = FileKeySerializer.Serialize(new { Name = "Bob", Type = TestEnum.One, Id = 1 });
      result.ShouldBe("1_Bob_One");
    }

    [Test]
    public void Serialize_ShouldSerializePOCO()
    {
      var result = FileKeySerializer.Serialize(new TestClass 
      { 
        Id = 1, 
        Number = null,
        Name = "Bob", 
        Nickname = null,
        Type = TestEnum.One,
        AlternateType = null
      });
      result.ShouldBe("1__Bob__One_");
    }

    [Test]
    public void Serialize_ShouldThrowForObjectProp()
    {
      Should.Throw<InvalidOperationException>(() =>
      {
        FileKeySerializer.Serialize(new { Id = 1, Name = "Bob", Type = TestEnum.One, Obj = new { } });
      });
    }

    [Test]
    public void Deserialize_ShouldDeserializePOCO()
    {
      var result = FileKeySerializer.Deserialize<TestClass>("1__Bob__One_");
      result.Id.ShouldBe(1);
      result.Number.ShouldBeNull();
      result.Name.ShouldBe("Bob");
      result.Nickname.ShouldBeNull();
      result.Type.ShouldBe(TestEnum.One);
      result.AlternateType.ShouldBeNull();
    }
  }


  public class TestClass
  {
    public int Id { get; set; }
    public int? Number { get; set; }
    public string Name { get; set; }
    public string? Nickname { get; set; }
    public TestEnum Type { get; set; }
    public TestEnum? AlternateType { get; set; }
  }

  public enum TestEnum { One, Two, Three }
}
