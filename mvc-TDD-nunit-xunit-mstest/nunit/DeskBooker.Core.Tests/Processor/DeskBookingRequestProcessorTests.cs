using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeskBooker.Core.Processor
{
  [TestFixture]
  public class DeskBookingRequestProcessorTests
  {
    private DeskBookingRequest _request;
    private List<Desk> _availableDesks;
    private Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
    private Mock<IDeskRepository> _deskRepositoryMock;
    private DeskBookingRequestProcessor _processor;

    [SetUp]
    public void Setup()
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

    [Test]
    public void ShouldReturnDeskBookingResultWithRequestValues()
    {
      // Act
      DeskBookingResult result = _processor.BookDesk(_request);

      // Assert
      Assert.NotNull(result);
      Assert.AreEqual(_request.FirstName, result.FirstName);
      Assert.AreEqual(_request.LastName, result.LastName);
      Assert.AreEqual(_request.Email, result.Email);
      Assert.AreEqual(_request.Date, result.Date);
    }

    [Test]
    public void ShouldThrowExceptionIfRequestIsNull()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));

      Assert.AreEqual("request", exception.ParamName);
    }

    [Test]
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

      Assert.NotNull(savedDeskBooking);
      Assert.AreEqual(_request.FirstName, savedDeskBooking.FirstName);
      Assert.AreEqual(_request.LastName, savedDeskBooking.LastName);
      Assert.AreEqual(_request.Email, savedDeskBooking.Email);
      Assert.AreEqual(_request.Date, savedDeskBooking.Date);
      Assert.AreEqual(_availableDesks.First().Id, savedDeskBooking.DeskId);
    }

    [Test]
    public void ShouldNotSaveDeskBookingIfNoDeskIsAvailable()
    {
      _availableDesks.Clear();

      _processor.BookDesk(_request);

      _deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Never);
    }

    [TestCase(DeskBookingResultCode.Success, true)]
    [TestCase(DeskBookingResultCode.NoDeskAvailable, false)]
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

    [TestCase(5, true)]
    [TestCase(null, false)]
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
