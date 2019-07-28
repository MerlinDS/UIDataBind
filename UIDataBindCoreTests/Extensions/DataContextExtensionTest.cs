using NUnit.Framework;
using UIDataBindCore.Extensions;
using UIDataBindCoreTests.Utils;

namespace UIDataBindCoreTests.Extensions
{
//    [TestFixture]
    public class DataContextExtensionTest
    {
//        [Test]
        public void RegisterTest()
        {
            var context = new TestDataContext();
            context.Register();
            Assert.Fail("Not implemented yet!");
        }

//        [Test]
        public void UnregisterTest()
        {
            var context = new TestDataContext();
            context.Register();
            context.Unregister();
            Assert.Fail("Not implemented yet!");
        }
    }
}