using System;
using NSubstitute;
using NUnit.Framework;
using UIDataBindCore;

namespace UIDataBindCoreTests
{
    [TestFixture]
    public class BindingKernelTestFixture
    {
        [TearDown]
        public void TearDown() =>
            BindingKernel.Instance.Dispose();

        [Test]
        public void SingletonTest()
        {
            var firstKernel = BindingKernel.Instance;
            Assert.That(firstKernel, Is.Not.Null);
            var secondKernel = BindingKernel.Instance;
            Assert.That(firstKernel, Is.SameAs(secondKernel));
        }

        [Test]
        public void ContextRegistrationTest()
        {
            var context = Substitute.For<IDataContext, IInitializable>();
            BindingKernel.Instance.Register(context);
            BindingKernel.Instance.Register(context);//Do nothing
            //TODO: Check that context was registered
        }
        [Test]
        public void UnregisterTest()
        {
            var firstContext = Substitute.For<ITestContext>();
            var secondContext = Substitute.For<ITestContext>();
            Assert.Throws<ArgumentException>(() => BindingKernel.Instance.Unregister(firstContext));
            BindingKernel.Instance.Register(firstContext);
            BindingKernel.Instance.Register(secondContext);
            BindingKernel.Instance.Unregister(firstContext);
            Assert.Throws<ArgumentException>(() => BindingKernel.Instance.Unregister(firstContext));
            BindingKernel.Instance.Unregister(secondContext);
            Assert.Throws<ArgumentException>(() => BindingKernel.Instance.Unregister(firstContext));
        }

        public interface ITestContext : IDataContext
        {

        }
    }
}