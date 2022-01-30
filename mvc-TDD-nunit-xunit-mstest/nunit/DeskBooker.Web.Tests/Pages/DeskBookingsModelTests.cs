using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Moq;
using NUnit.Framework;

namespace DeskBooker.Web.Pages
{
  [TestFixture]
  public class DeskBookingsModelTests
  {
    [Test]
    public void ShouldGetAllDeskBookings()
    {
      // Arrange
      var deskBookings = new[]
      {
        new DeskBooking(),
        new DeskBooking(),
        new DeskBooking(),
      };

      var deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
      deskBookingRepositoryMock.Setup(x => x.GetAll())
        .Returns(deskBookings);

      var deskBookingsModel = new DeskBookingsModel(deskBookingRepositoryMock.Object);

      // Act
      deskBookingsModel.OnGet();

      // Assert
      Assert.AreEqual(deskBookings, deskBookingsModel.DeskBookings);
    }
  }
}
