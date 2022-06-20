using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        private ErrorLogger _errorLogger;

        [SetUp]
        public void Setup()
        {
            _errorLogger = new ErrorLogger();
        }

        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {

            _errorLogger.Log("abc");

            Assert.That(_errorLogger.LastError, Is.EqualTo("abc"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidArgument_ThrowArgumentNullException(string error)
        {
            Assert.That(() => _errorLogger.Log(error), Throws.ArgumentNullException);
        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var id = Guid.Empty;

            _errorLogger.ErrorLogged += (sender, args) => { id = args; };
            _errorLogger.Log("abc");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}