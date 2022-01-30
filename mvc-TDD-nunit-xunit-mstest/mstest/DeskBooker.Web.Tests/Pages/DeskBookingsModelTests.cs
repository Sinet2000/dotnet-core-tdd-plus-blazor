using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DeskBooker.Web.Pages
{
  [TestClass]
  public class DeskBookingsModelTests
  {
    [TestMethod]
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
