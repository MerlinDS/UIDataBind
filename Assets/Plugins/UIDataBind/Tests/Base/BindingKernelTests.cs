using NSubstitute;
using NUnit.Framework;
using Plugins.UIDataBind.Base;

namespace Plugins.UIDataBind.Tests.Base
{
    [TestFixture]
    public class BindingKernelTests
    {
        private BindingKernel _kernel;

        [SetUp]
        public void SetUp() => _kernel = BindingKernel.Instance;
        [TearDown]
        public void TearDown() => _kernel.Dispose();

        [Test]
        public void RegisterTest([Random(int.MinValue, int.MaxValue, 1)] int instanceId)
        {
            var expectedContext = Substitute.For<IViewContext>();
            _kernel.Register(instanceId, expectedContext);

            var context = _kernel.GetContext(instanceId);
            Assert.That(context, Is.Not.Null);
            Assert.That(context, Is.EqualTo(expectedContext));
        }

        [Test]
        public void UnregisterTest([Random(int.MinValue, int.MaxValue, 1)] int instanceId)
        {
            _kernel.Register(instanceId, Substitute.For<IViewContext>());
            _kernel.Unregister(instanceId);

            Assert.That(_kernel.GetContext(instanceId), Is.Null);
        }
    }
}