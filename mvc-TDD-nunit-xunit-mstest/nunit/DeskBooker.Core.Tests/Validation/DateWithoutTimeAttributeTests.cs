using NUnit.Framework;
using System;

namespace DeskBooker.Core.Validation
{
  [TestFixture]
  public class DateWithoutTimeAttributeTests
  {
    [TestCase(true, 0, 0, 0)]
    [TestCase(false, 1, 0, 0)]
    [TestCase(false, 0, 1, 0)]
    [TestCase(false, 0, 0, 1)]
    public void ShouldReturnDateMustBeInTheFuture(bool expectedIsValid, int hour, int minute, int second)
    {
      var dateTime = new DateTime(2020, 1, 28, hour, minute, second);

      var attribute = new DateWithoutTimeAttribute();

      var isValid = attribute.IsValid(dateTime);

      Assert.AreEqual(expectedIsValid, isValid);
    }

    [Test]
    public void ShouldHaveExpectedErrorMessage()
    {
      var attribute = new DateWithoutTimeAttribute();

      Assert.AreEqual("Date must not contain time", attribute.ErrorMessage);
    }
  }
}
