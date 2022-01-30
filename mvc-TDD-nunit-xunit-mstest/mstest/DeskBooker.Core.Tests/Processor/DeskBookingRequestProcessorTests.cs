using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeskBooker.Core.Processor
{
  [TestClass]
  public class DeskBookingRequestProcessorTests
  {
    private DeskBookingRequest _request;
    private List<Desk> _availableDesks;
    private Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
    private Mock<IDeskRepository> _deskRepositoryMock;
    private DeskBookingRequestProcessor _processor;

    [TestInitialize]
    public void Initialize()
    {
      _request = new DeskBookingRequest
      {
        FirstName = "Thomas",
        LastName = "Huber",
        Email = "thomas@thomasclaudiushuber.com",
        Date = new DateTime(2020, 1, 28)
      };

      _availableDesks = new List<Desk> { new Desk { Id = 7 } };

      _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
      _deskRepositoryMock = new Mock<IDeskRepository>();
      _deskRepositoryMock.Setup(x => x.GetAvailableDesks(_request.Date))
        .Returns(_availableDesks);

      _processor = new DeskBookingRequestProcessor(
        _deskBookingRepositoryMock.Object, _deskRepositoryMock.Object);
    }

    [TestMethod]
    public void ShouldReturnDeskBookingResultWithRequestValues()
    {
      // Act
      DeskBookingResult result = _processor.BookDesk(_request);

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(_request.FirstName, result.FirstName);
      Assert.AreEqual(_request.LastName, result.LastName);
      Assert.AreEqual(_request.Email, result.Email);
      Assert.AreEqual(_request.Date, result.Date);
    }

    [TestMethod]
    public void ShouldThrowExceptionIfRequestIsNull()
    {
      var exception = Assert.ThrowsException<ArgumentNullException>(() => _processor.BookDesk(null));

      Assert.AreEqual("request", exception.ParamName);
    }

    [TestMethod]
    public void ShouldSaveDeskBooking()
    {
      DeskBooking savedDeskBooking = null;
      _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
        .Callback<DeskBooking>(deskBooking =>
        {
          savedDeskBooking = deskBooking;
        });

      _processor.BookDesk(_request);

      _deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Once);

      Assert.IsNotNull(savedDeskBooking);
      Assert.AreEqual(_request.FirstName, savedDeskBooking.FirstName);
      Assert.AreEqual(_request.LastName, savedDeskBooking.LastName);
      Assert.AreEqual(_request.Email, savedDeskBooking.Email);
      Assert.AreEqual(_request.Date, savedDeskBooking.Date);
      Assert.AreEqual(_availableDesks.First().Id, savedDeskBooking.DeskId);
    }

    [TestMethod]
    public void ShouldNotSaveDeskBookingIfNoDeskIsAvailable()
    {
      _availableDesks.Clear();

      _processor.BookDesk(_request);

      _deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Never);
    }

    [DataTestMethod]
    [DataRow(DeskBookingResultCode.Success, true)]
    [DataRow(DeskBookingResultCode.NoDeskAvailable, false)]
    public void ShouldReturnExpectedResultCode(
      DeskBookingResultCode expectedResultCode, bool isDeskAvailable)
    {
      if (!isDeskAvailable)
      {
        _availableDesks.Clear();
      }

      var result = _processor.BookDesk(_request);

      Assert.AreEqual(expectedResultCode, result.Code);
    }

    [DataTestMethod]
    [DataRow(5, true)]
    [DataRow(null, false)]
    public void ShouldReturnExpectedDeskBookingId(
      int? expectedDeskBookingId, bool isDeskAvailable)
    {
      if (!isDeskAvailable)
      {
        _availableDesks.Clear();
      }
      else
      {
        _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
          .Callback<DeskBooking>(deskBooking =>
          {
            deskBooking.Id = expectedDeskBookingId.Value;
          });
      }

      var result = _processor.BookDesk(_request);

      Assert.AreEqual(expectedDeskBookingId, result.DeskBookingId);
    }
  }
}
