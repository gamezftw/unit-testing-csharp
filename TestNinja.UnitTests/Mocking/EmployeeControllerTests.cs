using NUnit.Framework;

namespace Tests
{
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeRepository> _employeeRepository;
        private EmployeeController _employeeController;
        [SetUp]
        public void Setup()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _employeeController = new EmployeeController(_employeeRepository.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_EmployeeDeleted()
        {
            _employeeController.DeleteEmployee(1);

            _employeeRepository.Verify(er => er.DeleteEmployee(1));
        }

        [Test]
        public void DeleteEmployee_WhenCalled_ReturnRedirectResult()
        {
            var result = _employeeController.DeleteEmployee(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
    }
}