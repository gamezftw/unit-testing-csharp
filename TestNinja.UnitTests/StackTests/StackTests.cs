namespace Tests
{
    [TestFixture]
    public class StackTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Push_ArgumentIsNull_ThrowsArgumentNullException()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_SingleString_CountSouldBeOne()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();

            stack.Push("a");

            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_StackIsEmpty_ReturnZero()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_StackIsEmpty_ThrowsInvalidOperationException()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackIsNotEmpty_CountSouldBeZero()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();

            stack.Push("a");

            stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_StackIsNotEmpty_ReturnValueOnTop()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();

            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            var result = stack.Pop();

            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Pop_StackIsNotEmpty_RemoveValueOnTop()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();

            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            var result = stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_StackIsEmpty_ThrowsInvalidOperationException()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackIsNotEmpty_ReturnValueOnTop()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();

            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Peek_StackIsNotEmpty_DoesNotRemoveObjectOnTop()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();

            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            var result = stack.Peek();

            Assert.That(stack.Count, Is.EqualTo(3));
        }
    }
}