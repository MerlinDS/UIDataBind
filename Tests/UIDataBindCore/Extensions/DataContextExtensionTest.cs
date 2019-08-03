using NUnit.Framework;
using Tests.UIDataBindCore.Utils;
using UIDataBindCore.Extensions;

namespace Tests.UIDataBindCore.Extensions
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