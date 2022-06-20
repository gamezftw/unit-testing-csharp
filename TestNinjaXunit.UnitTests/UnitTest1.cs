namespace TestNinjaXunit.UnitTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var reservation = new Reservation();

        // Act
        var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

        Assert.Equal(true, result);
        // Assert.True(result);
    }
}