using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Moq;
using NUnit.Framework;

namespace DeskBooker.Web.Pages
{
  [TestFixture]
  public class DesksModelTests
  {
    [Test]
    public void ShouldGetAllDesks()
    {
      // Arrange
      var desks = new[]
      {
        new Desk(),
        new Desk(),
        new Desk(),
      };

      var deskRepositoryMock = new Mock<IDeskRepository>();
      deskRepositoryMock.Setup(x => x.GetAll())
          .Returns(desks);

      var desksModel = new DesksModel(deskRepositoryMock.Object);

      // Act
      desksModel.OnGet();

      // Assert
      Assert.AreEqual(desks, desksModel.Desks);
    }
  }
}
