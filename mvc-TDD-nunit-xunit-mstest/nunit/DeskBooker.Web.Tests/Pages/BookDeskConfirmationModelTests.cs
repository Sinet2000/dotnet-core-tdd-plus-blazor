using NUnit.Framework;
using System;

namespace DeskBooker.Web.Pages
{
  [TestFixture]
  public class BookDeskConfirmationModelTests
  {
    [Test]
    public void ShouldStoreParameterValuesInProperties()
    {
      // Arrange
      const int deskBookingId = 7;
      const string firstName = "Thomas";
      var date = new DateTime(2020, 1, 28);

      var bookDeskConfirmationModel = new BookDeskConfirmationModel();

      // Act
      bookDeskConfirmationModel.OnGet(deskBookingId, firstName, date);

      // Assert
      Assert.AreEqual(deskBookingId, bookDeskConfirmationModel.DeskBookingId);
      Assert.AreEqual(firstName, bookDeskConfirmationModel.FirstName);
      Assert.AreEqual(date, bookDeskConfirmationModel.Date);
    }
  }
}
