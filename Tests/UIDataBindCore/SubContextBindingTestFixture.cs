using NUnit.Framework;
using Tests.UIDataBindCore.Utils;
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
        public void SubContextTest()
        {
            var parent = new TestParentContext();
            parent.Register();
            var child = BindingKernel.Instance.FindSubContext(parent, nameof(parent.ChildContext));
            Assert.That(child, Is.SameAs(parent.ChildContext));

            var childProperty = BindingKernel.Instance.FindProperty(child, nameof(parent.ChildContext.BindMember));
            Assert.That(childProperty, Is.SameAs(parent.ChildContext.BindMember));

            parent.Unregister();
            Assert.That(BindingKernel.Instance.FindSubContext(parent, nameof(parent.ChildContext)), Is.Null);
            Assert.That(BindingKernel.Instance.FindProperty(child, nameof(parent.ChildContext.BindMember)), Is.Null);
        }

        private class TestParentContext : IDataContext
        {
            [Bind]
            public readonly TestDataContext ChildContext = new TestDataContext();
        }
    }
}