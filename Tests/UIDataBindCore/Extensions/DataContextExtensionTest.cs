using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Extensions;
using UIDataBindCore.Properties;

namespace Tests.UIDataBindCore.Extensions
{
    [TestFixture]
    public class DataContextExtensionTest
    {
        private DataContext _context;
        [SetUp]
        public void SetUp() => _context = new DataContext();

        [TearDown]
        public void TearDown() => _context?.Unbind();

        [Test]
        public void FindPropertySameTypeTest()
        {
            var bindProperty = _context.FinProperty<bool>(nameof(DataContext.BindProperty));
            Assert.That(bindProperty, Is.Not.Null);
            Assert.That(bindProperty, Is.SameAs(_context.BindProperty));
        }

        [Test]
        public void FindPropertyNotExistingTest()
        {
            var bindProperty = _context.FinProperty<bool>("some property name");
            Assert.That(bindProperty, Is.Not.Null);
            Assert.That(bindProperty, Is.InstanceOf<IBindProperty<bool>>());
            Assert.That(bindProperty, Is.Not.SameAs(_context.BindProperty));
        }

        [Test]
        public void FindPropertyDiffTypeTest()
        {
            var bindProperty = _context.FinProperty<int>(nameof(DataContext.BindProperty));
            Assert.That(bindProperty, Is.Not.Null);
            Assert.That(bindProperty, Is.InstanceOf<BindPropertyAdapter<bool, int>>());
        }

        [Test]
        public void FindPropertyNotConvertibleTypeTest()
        {
            var bindProperty = _context.FinProperty<object>(nameof(DataContext.BindProperty));
            Assert.That(bindProperty, Is.Not.Null);
            Assert.That(bindProperty, Is.InstanceOf<IBindProperty<object>>());
            Assert.That(bindProperty, Is.Not.SameAs(_context.BindProperty));
        }

        [Test]
        public void FindPropertyInMultiContextsTest()
        {
            var secondContext = new DataContext();
            var a = _context.FinProperty<bool>(nameof(DataContext.BindProperty));
            var b = secondContext.FinProperty<bool>(nameof(DataContext.BindProperty));
            Assert.That(a, Is.Not.Null);
            Assert.That(b, Is.Not.Null);
            Assert.That(a, Is.Not.SameAs(b));
            Assert.That(a, Is.SameAs(_context.BindProperty));
            Assert.That(b, Is.SameAs(secondContext.BindProperty));
            secondContext.Unbind();
            Assert.Throws<InvalidOperationException>(
                () => secondContext.FinProperty<bool>(nameof(DataContext.BindProperty)));
        }

        [Test]
        public void FindPropertyArgumentNullExceptionTest()
        {
            IDataContext context = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(()=>context.FinProperty<bool>(nameof(DataContext.BindProperty)));
        }

        [Test]
        public void FindPropertyArgumentExceptionTest() =>
            Assert.Throws<ArgumentException>(()=>_context.FinProperty<bool>(null));

        [Test]
        public void FindPropertyKernelInvalidOperationExceptionTest()
        {
            var context = new DataContext();
            context.Unbind();
            Assert.Throws<InvalidOperationException>(() => context.FinProperty<bool>(nameof(DataContext.BindProperty)));
        }

        [Test]
        public void FindPropertyInvalidOperationExceptionTest()
        {
            var context = new DataContextWithException();
            context.Register();
            context.ThrowsError = true;
            Assert.Throws<InvalidOperationException>(() => context.FinProperty<bool>(nameof(DataContext.BindProperty)));
        }


        private class DataContext : IDataContext, IBinder
        {
            public DataContext() => Bind(this);
            public void Bind(IDataContext context) => context.Register();

            public void Unbind()
            {
                BindProperty.Dispose();
                this.Unregister();
            }

            [Bind]
            public readonly BindProperty<bool> BindProperty = new BindProperty<bool>(true);
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