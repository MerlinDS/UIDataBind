using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Base;
using UIDataBindCore.Extensions;
using UIDataBindCoreTests.Utils;

namespace UIDataBindCoreTests.Extensions
{
    [TestFixture]
    public class DataContextReflectionExtensionTest
    {
        [Test]
        public void GetDataContextInfoExceptionTest()
        {
            IDataContext context = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => context.GetDataContextType());
        }

        [Test]
        public void GetDataContextInfoTest()
        {
            var context = new TestDataContext();
            var type = context.GetType();
            var contextType = context.GetDataContextType();

            Assert.That(contextType, Is.Not.EqualTo(default(DataContextInfo)));
            Assert.That(contextType.Name, Is.EqualTo(type.Name));
            Assert.That(contextType.IsInitializable, Is.False);
            Assert.That(contextType.Members, Is.Not.Empty);
            Assert.That(contextType.Members.Length, Is.EqualTo(2));

            Assert.That(contextType.Members.Any(m => m.Name == nameof(TestDataContext.BindMember)), Is.True);
            Assert.That(contextType.Members.Any(m => m.Name == nameof(TestDataContext.BindMethod)), Is.True);
        }

        [Test]
        public void GetInitializableDataContextInfoTest()
        {
            var context = new InitializableTestDataContext();
            var type = context.GetType();
            var contextType = context.GetDataContextType();

            Assert.That(contextType, Is.Not.EqualTo(default(DataContextInfo)));
            Assert.That(contextType.Name, Is.EqualTo(type.Name));
            Assert.That(contextType.IsInitializable, Is.True);
            Assert.That(contextType.Members, Is.Empty);
            context.Init();
        }

        [Test]
        public void GetDataContextReferencesExceptionTest()
        {
            IDataContext context = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => context.GetReferences(new DataContextInfo{Members = new MemberInfo[0]}));

            context = new TestDataContext();
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentException>(() => context.GetReferences(default));
        }

        [Test]
        public void GetDataContextReferencesTest()
        {
            var context = new TestDataContext();
            var contextReferences = context.GetReferences(context.GetDataContextType());

            Assert.That(contextReferences, Is.Not.EqualTo(default(DataContextInfo)));
            Assert.That(contextReferences.Instance, Is.SameAs(context));
            Assert.That(contextReferences.Properties, Is.Not.Empty);
            Assert.That(contextReferences.Methods, Is.Not.Empty);

            Assert.That(contextReferences.Properties.First().Key, Is.EqualTo(nameof(TestDataContext.BindMember)));
            Assert.That(contextReferences.Methods.First().Key, Is.EqualTo(nameof(TestDataContext.BindMethod)));

            Assert.That(contextReferences.Properties.First().Value, Is.SameAs(context.BindMember));
            context.IsBindMethodInvoked = false;
            contextReferences.Methods.First().Value.Invoke();
            Assert.That(context.IsBindMethodInvoked, Is.True);
        }

        private class InitializableTestDataContext : IDataContext, IInitializable
        {
            public void Init()
            {
            }
        }
    }
}