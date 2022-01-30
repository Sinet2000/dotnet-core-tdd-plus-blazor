using DeskBooker.Core.Domain;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeskBooker.DataAccess.Repositories
{
  [TestFixture]
  public class DeskRepositoryTests
  {
    [Test]
    public void ShouldReturnTheAvailableDesks()
    {
      // Arrange
      var date = new DateTime(2020, 1, 25);

      var options = new DbContextOptionsBuilder<DeskBookerContext>()
        .UseInMemoryDatabase(databaseName: "ShouldReturnTheAvailableDesks")
        .Options;

      using (var context = new DeskBookerContext(options))
      {
        context.Desk.Add(new Desk { Id = 1 });
        context.Desk.Add(new Desk { Id = 2 });
        context.Desk.Add(new Desk { Id = 3 });

        context.DeskBooking.Add(new DeskBooking { DeskId = 1, Date = date });
        context.DeskBooking.Add(new DeskBooking { DeskId = 2, Date = date.AddDays(1) });

        context.SaveChanges();
      }

      using (var context = new DeskBookerContext(options))
      {
        var repository = new DeskRepository(context);

        // Act
        var desks = repository.GetAvailableDesks(date);

        // Assert
        Assert.AreEqual(2, desks.Count());
        Assert.IsTrue(desks.Any(d => d.Id == 2));
        Assert.IsTrue(desks.Any(d => d.Id == 3));
        Assert.IsFalse(desks.Any(d => d.Id == 1));
      }
    }

    [Test]
    public void ShouldGetAll()
    {
      // Arrange
      var options = new DbContextOptionsBuilder<DeskBookerContext>()
        .UseInMemoryDatabase(databaseName: "ShouldGetAll")
        .Options;

      var storedList = new List<Desk>
      {
        new Desk(),
        new Desk(),
        new Desk()
      };

      using (var context = new DeskBookerContext(options))
      {
        foreach (var desk in storedList)
        {
          context.Add(desk);
          context.SaveChanges();
        }
      }

      // Act
      List<Desk> actualList;
      using (var context = new DeskBookerContext(options))
      {
        var repository = new DeskRepository(context);
        actualList = repository.GetAll().ToList();
      }

      // Assert
      Assert.AreEqual(storedList.Count(), actualList.Count());
    }
  }
}