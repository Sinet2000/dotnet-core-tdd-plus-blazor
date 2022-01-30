using DeskBooker.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DeskBooker.Core.Processor
{
  [TestClass]
  public class DeskBookingRequestProcessorTests
  {
    private DeskBookingRequestProcessor _processor;

    [TestInitialize]
    public void Initialize()
    {
      _processor = new DeskBookingRequestProcessor();
    }

    [TestMethod]
    public void ShouldReturnDeskBookingResultWithRequestValues()
    {
      // Arrange
      var request = new DeskBookingRequest
      {
        FirstName = "Thomas",
        LastName = "Huber",
        Email = "thomas@thomasclaudiushuber.com",
        Date = new DateTime(2020, 1, 28)
      };

      // Act
      DeskBookingResult result = _processor.BookDesk(request);

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(request.FirstName, result.FirstName);
      Assert.AreEqual(request.LastName, result.LastName);
      Assert.AreEqual(request.Email, result.Email);
      Assert.AreEqual(request.Date, result.Date);
    }

    [TestMethod]
    public void ShouldThrowExceptionIfRequestIsNull()
    {
      var exception = Assert.ThrowsException<ArgumentNullException>(() => _processor.BookDesk(null));

      Assert.AreEqual("request", exception.ParamName);
    }
  }
}
