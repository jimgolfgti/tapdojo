using NUnit.Framework;

namespace tapdojo.tests.TAP
{
    [TestFixture]
    public class AdditionTests
    {
        [Test]
        public async void AddingTwoNumbers()
        {
            var result = await Addition.AddAsync(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public async void AddingTwoNumbersFail()
        {
            var result = await Addition.AddAsync(2, 2);

            Assert.That(result, Is.EqualTo(4));
        }
    }
}
