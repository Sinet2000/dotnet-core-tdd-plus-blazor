using NUnit.Framework;
using System;

namespace DeskBooker.Core.Validation
{
  [TestFixture]
  public class DateInFutureAttributeTests
  {
    [TestCase(false, -1)]
    [TestCase(false, 0)]
    [TestCase(true, 1)]
    public void ShouldValidateDateIsInTheFuture(bool expectedIsValid, int secondsToAdd)
    {
      var dateTimeNow = new DateTime(2020, 1, 28);

      var attribute = new DateInFutureAttribute(() => dateTimeNow);

      var isValid = attribute.IsValid(dateTimeNow.AddSeconds(secondsToAdd));

      Assert.AreEqual(expectedIsValid, isValid);
    }

    [Test]
    public void ShouldHaveExpectedErrorMessage()
    {
      var attribute = new DateInFutureAttribute();

      Assert.AreEqual("Date must be in the future", attribute.ErrorMessage);
    }
  }
}
