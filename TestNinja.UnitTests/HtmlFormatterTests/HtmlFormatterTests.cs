using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FormatAsBold_WhenCalled_ShouldReturnTheStringWIithStrongElement()
        {
            var htmlFormatter = new HtmlFormatter();

            var result = htmlFormatter.FormatAsBold("abc");

            // Specific
            Assert.That(result, Is.EqualTo("<strong>abc</strong>"));

            // More general
            // Assert.That(result, Does.StartWith("<strong>"));
            // Assert.That(result, Does.EndWith("</strong>"));
            // Assert.That(result, Does.Contain("abs"));
        }
    }
}