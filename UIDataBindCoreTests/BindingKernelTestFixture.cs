using NSubstitute;
using NUnit.Framework;
using UIDataBindCore;

namespace UIDataBindCoreTests
{
    [TestFixture]
    public class BindingKernelTestFixture
    {
        [Test]
        public void SingletonTest()
        {
            var firstKernel = BindingKernel.Instance;
            Assert.That(firstKernel, Is.Not.Null);
            var secondKernel = BindingKernel.Instance;
            Assert.That(firstKernel, Is.SameAs(secondKernel));
        }

        [Test]
        public void RegisterTest()
        {
            var context = Substitute.For<IDataContext>();
            BindingKernel.Instance.Register(context);
        }
        [Test]
        public void UnregisterTest()
        {
            var context = Substitute.For<IDataContext>();
            BindingKernel.Instance.Unregister(context);
        }
    }
}