using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetOutput_NumberDivisibleByThreeOnly_ReturnsFizz()
        {
            var result = FizzBuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz"));
        }
        [Test]
        public void GetOutput_NumberDivisibleByFiveOnly_ReturnsBuzz()
        {
            var result = FizzBuzz.GetOutput(5);

            Assert.That(result, Is.EqualTo("Buzz"));
        }
        [Test]
        public void GetOutput_NumberDivisibleByThreeAndFive_ReturnsFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }
        [Test]
        public void GetOutput_NumberNotDivisibleByThreeOrFive_ReturnTheSameNumber()
        {
            var result = FizzBuzz.GetOutput(1);

            Assert.That(result, Is.EqualTo("1"));
        }

    }
}