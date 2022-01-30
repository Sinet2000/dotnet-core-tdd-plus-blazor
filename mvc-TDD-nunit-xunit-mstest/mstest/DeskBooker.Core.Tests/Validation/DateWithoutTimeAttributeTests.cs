using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DeskBooker.Core.Validation
{
  [TestClass]
  public class DateWithoutTimeAttributeTests
  {
    [DataRow(true, 0, 0, 0)]
    [DataRow(false, 1, 0, 0)]
    [DataRow(false, 0, 1, 0)]
    [DataRow(false, 0, 0, 1)]
    [DataTestMethod]
    public void ShouldReturnDateMustBeInTheFuture(bool expectedIsValid, int hour, int minute, int second)
    {
      var dateTime = new DateTime(2020, 1, 28, hour, minute, second);

      var attribute = new DateWithoutTimeAttribute();

      var isValid = attribute.IsValid(dateTime);

      Assert.AreEqual(expectedIsValid, isValid);
    }

    [TestMethod]
    public void ShouldHaveExpectedErrorMessage()
    {
      var attribute = new DateWithoutTimeAttribute();

      Assert.AreEqual("Date must not contain time", attribute.ErrorMessage);
    }
  }
}
