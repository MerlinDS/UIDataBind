using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Extensions;

namespace Tests.UIDataBindCore.Extensions
{
    [TestFixture]
    public class DataContextFindMethodExtensionTest
    {
        private DataContext _context;
        [SetUp]
        public void SetUp() => _context = new DataContext();

        [TearDown]
        public void TearDown() => _context?.Unbind();

        [Test]
        public void FindTest()
        {
            Action expectedMethod = _context.BindMethod;
            var bindMethod = _context.FindMethod(nameof(DataContext.BindMethod));
            Assert.That(bindMethod, Is.Not.Null);
            Assert.That(bindMethod.Target, Is.SameAs(expectedMethod.Target));
            bindMethod.Invoke();
        }

        [Test]
        public void NotExistingTest()
        {
            Action expectedMethod = _context.BindMethod;
            var bindMethod = _context.FindMethod("Some unknown method");
            Assert.That(bindMethod, Is.Not.Null);
            Assert.That(bindMethod.Target, Is.Not.SameAs(expectedMethod.Target));
            bindMethod.Invoke();
        }

        [Test]
        public void NotConvertibleTypeTest()
        {
            Action<int> expectedMethod = _context.ErrorBindMethod;
            var bindMethod = _context.FindMethod(nameof(DataContext.ErrorBindMethod));
            Assert.That(bindMethod, Is.Not.Null);
            Assert.That(bindMethod.Target, Is.Not.SameAs(expectedMethod.Target));
            expectedMethod.Invoke(1);
            bindMethod.Invoke();
        }

        [Test]
        public void InMultiContextsTest()
        {
            var secondContext = new DataContext();
            Action expectedAMethod = _context.BindMethod;
            Action expectedBMethod = secondContext.BindMethod;
            var a = _context.FindMethod(nameof(DataContext.BindMethod));
            var b = secondContext.FindMethod(nameof(DataContext.BindMethod));
            Assert.That(a, Is.Not.Null);
            Assert.That(b, Is.Not.Null);
            Assert.That(a.Target, Is.Not.SameAs(b.Target));
            Assert.That(a.Target, Is.SameAs(expectedAMethod.Target));
            Assert.That(b.Target, Is.SameAs(expectedBMethod.Target));
            secondContext.Unbind();
            Assert.Throws<InvalidOperationException>(
                () => secondContext.FindProperty<bool>(nameof(DataContext.BindMethod)));
        }

        [Test]
        public void ArgumentNullExceptionTest()
        {
            IDataContext context = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(()=>context.FindProperty<bool>(nameof(DataContext.BindMethod)));
        }

        [Test]
        public void ArgumentExceptionTest() =>
            Assert.Throws<ArgumentException>(()=>_context.FindProperty<bool>(null));


        [Test]
        public void KernelInvalidOperationExceptionTest()
        {
            var context = new DataContext();
            context.Unbind();
            Assert.Throws<InvalidOperationException>(() => context.FindProperty<bool>(nameof(DataContext.BindMethod)));
        }

        [Test]
        public void InvalidOperationExceptionTest()
        {
            var context = new DataContextWithException();
            context.Register();
            context.ThrowsError = true;
            Assert.Throws<InvalidOperationException>(() => context.FindProperty<bool>(nameof(DataContext.BindMethod)));
        }

        private class DataContext : IDataContext, IBinder
        {
            public IDataContext Context => this;
            public void Bind() => Context.Register();

            public DataContext() => Bind();

            public void Unbind() => this.Unregister();

            [Bind]
            public void BindMethod()
            {

            }

            [Bind]
            public void ErrorBindMethod(int args)
            {

            }
        }

        private class DataContextWithException : IDataContext
        {
            public bool ThrowsError;
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            public override int GetHashCode() => !ThrowsError ? RuntimeHelpers.GetHashCode(this) :
                throw new InvalidOperationException();
        }
    }
}