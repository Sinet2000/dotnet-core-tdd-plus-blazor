using DeskBooker.Core.Domain;
using System;
using Xunit;

namespace DeskBooker.Core.Processor
{
  public class DeskBookingRequestProcessorTests
  {
    private readonly DeskBookingRequestProcessor _processor;

    public DeskBookingRequestProcessorTests()
    {
      _processor = new DeskBookingRequestProcessor();
    }

    [Fact]
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
      Assert.NotNull(result);
      Assert.Equal(request.FirstName, result.FirstName);
      Assert.Equal(request.LastName, result.LastName);
      Assert.Equal(request.Email, result.Email);
      Assert.Equal(request.Date, result.Date);
    }

    [Fact]
    public void ShouldThrowExceptionIfRequestIsNull()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));

      Assert.Equal("request", exception.ParamName);
    }
  }
}
