using NUnit.Framework;
using Shouldly;
using System;

namespace PowerUp.Tests.Utils
{
  public class DateExtensionsTests
  {
    [Test]
    public void YearsElapsedSince_GetsCorrectNumber_WhenDayOfYearHasNotPassed()
    {
      var firstDate = new DateTime(2007, 6, 1);
      var secondDate = new DateTime(2000, 7, 1);

      var result = firstDate.YearsElapsedSince(secondDate);
      result.ShouldBe(6);
    }

    [Test]
    public void YearsElapsedSince_GetsCorrectNumber_WhenDayOfYearHasPassed()
    {
      var firstDate = new DateTime(2007, 6, 1);
      var secondDate = new DateTime(2000, 5, 1);

      var result = firstDate.YearsElapsedSince(secondDate);
      result.ShouldBe(7);
    }

    [Test]
    public void GetDateNYearsBefore_GetsCorrectNumber_WhenDayOfYearHasNotPassed()
    {
      var firstDate = new DateTime(2007, 6, 1);
      var secondDate = new DateTime(2000, 7, 1);

      var result = firstDate.GetDateNYearsBefore(7, 1, 6);
      result.ShouldBe(secondDate);
    }

    [Test]
    public void GetDateNYearsBefore_GetsCorrectNumber_WhenDayOfYearHasPassed()
    {
      var firstDate = new DateTime(2007, 6, 1);
      var secondDate = new DateTime(2000, 5, 1);

      var result = firstDate.YearsElapsedSince(secondDate);
      result.ShouldBe(7);
    }
  }
}
