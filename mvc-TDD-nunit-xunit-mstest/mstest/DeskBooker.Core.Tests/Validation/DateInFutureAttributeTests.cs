using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DeskBooker.Core.Validation
{
  [TestClass]
  public class DateInFutureAttributeTests
  {
    [DataRow(false, -1)]
    [DataRow(false, 0)]
    [DataRow(true, 1)]
    [DataTestMethod]
    public void ShouldValidateDateIsInTheFuture(bool expectedIsValid, int secondsToAdd)
    {
      var dateTimeNow = new DateTime(2020, 1, 28);

      var attribute = new DateInFutureAttribute(() => dateTimeNow);

      var isValid = attribute.IsValid(dateTimeNow.AddSeconds(secondsToAdd));

      Assert.AreEqual(expectedIsValid, isValid);
    }

    [TestMethod]
    public void ShouldHaveExpectedErrorMessage()
    {
      var attribute = new DateInFutureAttribute();

      Assert.AreEqual("Date must be in the future", attribute.ErrorMessage);
    }
  }
}
