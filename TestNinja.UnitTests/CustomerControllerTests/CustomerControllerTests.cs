using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController _customerController;

        [SetUp]
        public void Setup()
        {
            _customerController = new CustomerController();
        }

        [Test]
        public void GetCustomer_IdIsZero_ReturnsNotFound()
        {
            // var customerController = new CustomerController();

            var result = _customerController.GetCustomer(0);

            // NotFound
            Assert.That(result, Is.TypeOf<NotFound>());

            // NotFound or one of its derivatives
            // Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetCustomer_IdIsNotZero_ReturnsOk(int a)
        {
            var result = _customerController.GetCustomer(a);

            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}