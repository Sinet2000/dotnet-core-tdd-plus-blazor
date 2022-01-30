using DeskBooker.Core.Domain;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeskBooker.DataAccess.Repositories
{
  [TestFixture]
  public class DeskBookingRepositoryTests
  {
    [Test]
    public void ShouldSaveTheDeskBooking()
    {
      // Arrange
      var options = new DbContextOptionsBuilder<DeskBookerContext>()
        .UseInMemoryDatabase(databaseName: "ShouldSaveTheDeskBooking")
        .Options;

      var deskBooking = new DeskBooking
      {
        FirstName = "Thomas",
        LastName = "Huber",
        Date = new DateTime(2020, 1, 25),
        Email = "thomas@thomasclaudiushuber.com",
        DeskId = 1
      };

      // Act
      using (var context = new DeskBookerContext(options))
      {
        var repository = new DeskBookingRepository(context);
        repository.Save(deskBooking);
      }

      // Assert
      using (var context = new DeskBookerContext(options))
      {
        var bookings = context.DeskBooking.ToList();
        Assert.AreEqual(1, bookings.Count);
        var storedDeskBooking = bookings[0];

        Assert.AreEqual(deskBooking.FirstName, storedDeskBooking.FirstName);
        Assert.AreEqual(deskBooking.LastName, storedDeskBooking.LastName);
        Assert.AreEqual(deskBooking.Email, storedDeskBooking.Email);
        Assert.AreEqual(deskBooking.DeskId, storedDeskBooking.DeskId);
        Assert.AreEqual(deskBooking.Date, storedDeskBooking.Date);
      }
    }

    [Test]
    public void ShouldGetAllOrderedByDate()
    {
      // Arrange
      var options = new DbContextOptionsBuilder<DeskBookerContext>()
        .UseInMemoryDatabase(databaseName: "ShouldGetAllOrderedByDate")
        .Options;

      var storedList = new List<DeskBooking>
      {
        CreateDeskBooking(1,new DateTime(2020, 1, 27)),
        CreateDeskBooking(2,new DateTime(2020, 1, 25)),
        CreateDeskBooking(3,new DateTime(2020, 1, 29))
      };

      var expectedList = storedList.OrderBy(x => x.Date).ToList();

      using (var context = new DeskBookerContext(options))
      {
        foreach (var deskBooking in storedList)
        {
          context.Add(deskBooking);
          context.SaveChanges();
        }
      }

      // Act
      List<DeskBooking> actualList;
      using (var context = new DeskBookerContext(options))
      {
        var repository = new DeskBookingRepository(context);
        actualList = repository.GetAll().ToList();
      }

      // Assert
      CollectionAssert.AreEqual(expectedList, actualList, new DeskBookingEqualityComparer());
    }

    private class DeskBookingEqualityComparer : IComparer
    {
      public int Compare(object x, object y)
      {
        var deskBooking1 = (DeskBooking)x;
        var deskBooking2 = (DeskBooking)y;
        if (deskBooking1.Id > deskBooking2.Id)
          return 1;
        if (deskBooking1.Id < deskBooking2.Id)
          return -1;
        else
          return 0;
      }
    }

    private DeskBooking CreateDeskBooking(int id, DateTime dateTime)
    {
      return new DeskBooking
      {
        Id = id,
        FirstName = "Thomas",
        LastName = "Huber",
        Date = dateTime,
        Email = "thomas@thomasclaudiushuber.com",
        DeskId = 1
      };
    }
  }
}