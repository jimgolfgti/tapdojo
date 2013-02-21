using NUnit.Framework;

namespace tapdojo.tests.APM
{
    [TestFixture]
    public class AdditionTests
    {
        [Test]
        public void AddingTwoNumbers()
        {
            var asyncResult = Addition.BeginAdd(1, 2, null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            var result = Addition.EndAdd(asyncResult);

            Assert.That(result, Is.EqualTo(3));
        }
    }
}