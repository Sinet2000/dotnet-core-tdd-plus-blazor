using DeskBooker.Core.Domain;
using NUnit.Framework;
using System;

namespace DeskBooker.Core.Processor
{
  [TestFixture]
  public class DeskBookingRequestProcessorTests
  {
    private DeskBookingRequestProcessor _processor;

    [SetUp]
    public void Setup()
    {
      _processor = new DeskBookingRequestProcessor();
    }

    [Test]
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

    [Test]
    public void ShouldThrowExceptionIfRequestIsNull()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));

      Assert.AreEqual("request", exception.ParamName);
    }
  }
}
