namespace TestNinja.UnitTests.Mocking;

public class ProductTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetPrice_GoldCustomer_Apply30PercentDiscount()
    {
        var product = new Product() { ListPrice = 100 };
        var result = product.GetPrice(new Customer() { IsGold = true });

        Assert.That(result, Is.EqualTo(70));
    }

    [Test]
    public void GetPrice_GoldCustomer_Apply30PercentDiscount_MoqAbuse()
    {
        var product = new Product() { ListPrice = 100 };
        var customer = new Mock<ICustomer>();
        customer.Setup(x => x.IsGold).Returns(true);
        var result = product.GetPrice(customer.Object);

        Assert.That(result, Is.EqualTo(70));
    }
}