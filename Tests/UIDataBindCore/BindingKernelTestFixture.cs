using System;
using NSubstitute;
using NUnit.Framework;
using Tests.UIDataBindCore.Utils;
using UIDataBindCore;

namespace Tests.UIDataBindCore
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
        }
        [Test]
        public void UnregisterTest()
        {
            var firstContext = Substitute.For<IDataContext>();
            var secondContext = Substitute.For<IDataContext>();
            Assert.Throws<ArgumentException>(() => BindingKernel.Instance.Unregister(firstContext));
            BindingKernel.Instance.Register(firstContext);
            BindingKernel.Instance.Register(secondContext);
            BindingKernel.Instance.Unregister(firstContext);
            Assert.Throws<ArgumentException>(() => BindingKernel.Instance.Unregister(firstContext));
            BindingKernel.Instance.Unregister(secondContext);
            Assert.Throws<ArgumentException>(() => BindingKernel.Instance.Unregister(firstContext));
        }

        [Test]
        public void ContextFindPropertyTest()
        {
            var context = new TestDataContext();
            Assert.That(BindingKernel.Instance.FindProperty(context, nameof(TestDataContext.BindMember)), Is.Null);

            BindingKernel.Instance.Register(context);
            var property = BindingKernel.Instance.FindProperty(context, nameof(TestDataContext.BindMember));

            Assert.That(property, Is.Not.Null);
            Assert.That(property, Is.SameAs(context.BindMember));

            Assert.That(BindingKernel.Instance.FindProperty(context, "NONE"), Is.Null);

        }

        [Test]
        public void ContextFindMethodTest()
        {
            var context = new TestDataContext();
            Assert.That(BindingKernel.Instance.FindMethod(context, nameof(TestDataContext.BindMethod)), Is.Null);

            BindingKernel.Instance.Register(context);
            var action = BindingKernel.Instance.FindMethod(context, nameof(TestDataContext.BindMethod));

            Assert.That(action, Is.Not.Null);
            context.IsBindMethodInvoked = false;
            action.Invoke();
            Assert.That(context.IsBindMethodInvoked, Is.True);

            Assert.That(BindingKernel.Instance.FindMethod(context, "NONE"), Is.Null);

        }
    }
}