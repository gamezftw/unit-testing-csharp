using NUnit.Framework;
using Math = TestNinja.Fundamentals.Math;

namespace Tests
{
    [TestFixture]
    public class MathTests
    {

        private Math _math;
        [SetUp]
        public void Setup()
        {
            _math = new Math();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void Add_WhenCalled_ReturnsSum()
        {
            var math = new Math();

            var result = math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [Ignore("Refactored to use new parametrized test.")]
        public void Max_FirstArgumentIsGreater_ReturnFirstArgument()
        {

            var math = new Math();

            var result = _math.Max(2, 1);

            Assert.That(result, Is.EqualTo(2));
        }
        [Test]
        [Ignore("Refactored to use new parametrized test.")]
        public void Max_SecondArgumentIsGreater_ReturnSecondArgument()
        {

            var math = new Math();

            var result = _math.Max(1, 2);

            Assert.That(result, Is.EqualTo(2));
        }
        [Test]
        [Ignore("Refactored to use new parametrized test.")]
        public void Max_ArgumentsAreEqual_ReturnSame()
        {

            var math = new Math();

            var result = _math.Max(1, 1);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expected)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumberUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            // Assert.That(result, Is.Not.Empty);

            // Assert.That(result.Count(), Is.EqualTo(3));

            // Assert.That(result, Does.Contain(1));
            // Assert.That(result, Does.Contain(3));
            // Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            // Assert.That(result, Is.Ordered);
            // Assert.That(result, Is.Unique);
        }
    }
}