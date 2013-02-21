using System.Threading;
using NUnit.Framework;

namespace tapdojo.tests.EAP
{
    [TestFixture]
    public class AdditionTests
    {
        [Test]
        public void AddingTwoNumbers()
        {
            var waitHandle = new ManualResetEventSlim(false);
            var result = 0;
            var addition = new Addition();
            addition.AdditionCompleted += (s, e) =>
                {
                    result = e.Result;
                    waitHandle.Set();
                };
            addition.AddAsync(1, 2);

            waitHandle.Wait();

            Assert.That(result, Is.EqualTo(3));
        }
    }
}