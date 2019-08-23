using NSubstitute;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Extensions;

namespace Tests.UIDataBindCore
{
    [TestFixture]
    public class SubContextBindingTestFixture
    {
        [TearDown]
        public void TearDown() =>
            BindingKernel.Instance.Dispose();

        [Test]
        public void SubContextRegistrationTest()
        {
            var parent = new TestParentContext();
            parent.Register();
            var child = BindingKernel.Instance.FindSubContext(parent, nameof(parent.ChildContext));
            Assert.That(child, Is.SameAs(parent.ChildContext));
        }

        private class TestParentContext : IDataContext
        {
            [Bind]
            public readonly IDataContext ChildContext = Substitute.For<IDataContext>();
        }
    }
}