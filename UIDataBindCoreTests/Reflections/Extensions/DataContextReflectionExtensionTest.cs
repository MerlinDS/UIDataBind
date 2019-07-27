using System;
using System.Linq;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Reflections.Extensions;
using UIDataBindCoreTests.Utils;

namespace UIDataBindCoreTests.Reflections.Extensions
{
    [TestFixture]
    public class DataContextReflectionExtensionTest
    {
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

            Assert.That(contextType.Members.Any(m=>m.Name == nameof(TestDataContext.BindMember)), Is.True);
            Assert.That(contextType.Members.Any(m=>m.Name == nameof(TestDataContext.BindMethod)), Is.True);
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
        public void GetDataContextInfoExceptionTest()
        {
            IDataContext context = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(()=>context.GetDataContextType());
        }

        private class InitializableTestDataContext : IDataContext, IInitializable
        {

            public void Init()
            {

            }
        }
    }
}