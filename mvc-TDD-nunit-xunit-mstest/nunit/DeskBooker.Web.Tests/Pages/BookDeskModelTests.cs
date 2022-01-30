using DeskBooker.Core.Domain;
using DeskBooker.Core.Processor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DeskBooker.Web.Pages
{
  [TestFixture]
  public class BookDeskModelTests
  {
    private Mock<IDeskBookingRequestProcessor> _processorMock;
    private BookDeskModel _bookDeskModel;
    private DeskBookingResult _deskBookingResult;

    [SetUp]
    public void Setup()
    {
      _processorMock = new Mock<IDeskBookingRequestProcessor>();

      _bookDeskModel = new BookDeskModel(_processorMock.Object)
      {
        DeskBookingRequest = new DeskBookingRequest()
      };

      _deskBookingResult = new DeskBookingResult
      {
        Code = DeskBookingResultCode.Success
      };

      _processorMock.Setup(x => x.BookDesk(_bookDeskModel.DeskBookingRequest))
       .Returns(_deskBookingResult);
    }

    [TestCase(1, true)]
    [TestCase(0, false)]
    public void ShouldCallBookDeskMethodOfProcessorIfModelIsValid(
      int expectedBookDeskCalls, bool isModelValid)
    {
      // Arrange
      if (!isModelValid)
      {
        _bookDeskModel.ModelState.AddModelError("JustAKey", "AnErrorMessage");
      }

      // Act
      _bookDeskModel.OnPost();

      // Assert
      _processorMock.Verify(x => x.BookDesk(_bookDeskModel.DeskBookingRequest),
        Times.Exactly(expectedBookDeskCalls));
    }

    [Test]
    public void ShouldAddModelErrorIfNoDeskIsAvailable()
    {
      // Arrange
      _deskBookingResult.Code = DeskBookingResultCode.NoDeskAvailable;

      // Act
      _bookDeskModel.OnPost();

      // Assert
      Assert.IsTrue(_bookDeskModel.ModelState.TryGetValue("DeskBookingRequest.Date", out ModelStateEntry modelStateEntry));
      Assert.AreEqual(1, modelStateEntry.Errors.Count);
      var modelError = modelStateEntry.Errors[0];
      Assert.AreEqual("No desk available for selected date", modelError.ErrorMessage);
    }

    [Test]
    public void ShouldNotAddModelErrorIfDeskIsAvailable()
    {
      // Arrange
      _deskBookingResult.Code = DeskBookingResultCode.Success;

      // Act
      _bookDeskModel.OnPost();

      // Assert
      Assert.IsFalse(_bookDeskModel.ModelState.TryGetValue("DeskBookingRequest.Date", out ModelStateEntry modelStateEntry));
    }

    [TestCase(typeof(PageResult), false, null)]
    [TestCase(typeof(PageResult), true, DeskBookingResultCode.NoDeskAvailable)]
    [TestCase(typeof(RedirectToPageResult), true, DeskBookingResultCode.Success)]
    public void ShouldReturnExpectedActionResult(Type expectedActionResultType,
      bool isModelValid, DeskBookingResultCode? deskBookingResultCode)
    {
      // Arrange
      if (!isModelValid)
      {
        _bookDeskModel.ModelState.AddModelError("JustAKey", "AnErrorMessage");
      }

      if (deskBookingResultCode.HasValue)
      {
        _deskBookingResult.Code = deskBookingResultCode.Value;
      }

      // Act
      IActionResult actionResult = _bookDeskModel.OnPost();

      // Assert
      Assert.IsInstanceOf(expectedActionResultType, actionResult);
    }

    [Test]
    public void ShouldRedirectToBookDeskConfirmationPage()
    {
      // Arrange
      _deskBookingResult.Code = DeskBookingResultCode.Success;
      _deskBookingResult.DeskBookingId = 7;
      _deskBookingResult.FirstName = "Thomas";
      _deskBookingResult.Date = new DateTime(2020, 1, 28);

      // Act
      IActionResult actionResult = _bookDeskModel.OnPost();

      // Assert
      var redirectToPageResult = (RedirectToPageResult)actionResult;
      Assert.AreEqual("BookDeskConfirmation", redirectToPageResult.PageName);

      IDictionary<string, object> routeValues = redirectToPageResult.RouteValues;
      Assert.AreEqual(3, routeValues.Count);

      Assert.IsTrue(routeValues.TryGetValue("DeskBookingId", out object deskBookingId));
      Assert.AreEqual(_deskBookingResult.DeskBookingId, deskBookingId);

      Assert.IsTrue(routeValues.TryGetValue("FirstName", out object firstName));
      Assert.AreEqual(_deskBookingResult.FirstName, firstName);

      Assert.IsTrue(routeValues.TryGetValue("Date", out object date));
      Assert.AreEqual(_deskBookingResult.Date, date);
    }
  }
}
