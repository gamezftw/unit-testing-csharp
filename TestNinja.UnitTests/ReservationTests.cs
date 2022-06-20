namespace TestNinja.UnitTests;

[TestFixture]
public class ReservationTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
    {
        // Arrange
        var reservation = new Reservation();

        // Act
        var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

        // Assert
        Assert.IsTrue(result);
    }
    [Test]
    public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
    {
        // Arrange
        var user = new User();
        var reservation = new Reservation() { MadeBy = user };

        // Act
        var result = reservation.CanBeCancelledBy(user);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void CanBeCancelledBy_OtherUserCancelling_ReturnsFalse()
    {
        // Arrange
        var user1 = new User();
        var user2 = new User();
        var reservation = new Reservation() { MadeBy = user1 };

        // Act
        var result = reservation.CanBeCancelledBy(user2);

        // Assert
        Assert.IsFalse(result);
    }
}