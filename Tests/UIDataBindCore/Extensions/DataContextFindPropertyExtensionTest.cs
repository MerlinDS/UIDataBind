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
    public class DataContextFindPropertyExtensionTest
    {
        private DataContext _context;
        [SetUp]
        public void SetUp() => _context = new DataContext();

        [TearDown]
        public void TearDown() => _context?.Unbind();

        [Test]
        public void SameTypeTest()
        {
            var bindProperty = _context.FindProperty<bool>(nameof(DataContext.BindProperty));
            Assert.That(bindProperty, Is.Not.Null);
            Assert.That(bindProperty, Is.SameAs(_context.BindProperty));
        }

        [Test]
        public void NotExistingTest()
        {
            var bindProperty = _context.FindProperty<bool>("some property name");
            Assert.That(bindProperty, Is.Not.Null);
            Assert.That(bindProperty, Is.InstanceOf<IBindProperty<bool>>());
            Assert.That(bindProperty, Is.Not.SameAs(_context.BindProperty));
        }

        [Test]
        public void DiffTypeTest()
        {
            var bindProperty = _context.FindProperty<int>(nameof(DataContext.BindProperty));
            Assert.That(bindProperty, Is.Not.Null);
            Assert.That(bindProperty, Is.InstanceOf<BindPropertyAdapter<bool, int>>());
        }

        [Test]
        public void NotConvertibleTypeTest()
        {
            var bindProperty = _context.FindProperty<object>(nameof(DataContext.BindProperty));
            Assert.That(bindProperty, Is.Not.Null);
            Assert.That(bindProperty, Is.InstanceOf<IBindProperty<object>>());
            Assert.That(bindProperty, Is.Not.SameAs(_context.BindProperty));
        }

        [Test]
        public void InMultiContextsTest()
        {
            var secondContext = new DataContext();
            var a = _context.FindProperty<bool>(nameof(DataContext.BindProperty));
            var b = secondContext.FindProperty<bool>(nameof(DataContext.BindProperty));
            Assert.That(a, Is.Not.Null);
            Assert.That(b, Is.Not.Null);
            Assert.That(a, Is.Not.SameAs(b));
            Assert.That(a, Is.SameAs(_context.BindProperty));
            Assert.That(b, Is.SameAs(secondContext.BindProperty));
            secondContext.Unbind();
            Assert.Throws<InvalidOperationException>(
                () => secondContext.FindProperty<bool>(nameof(DataContext.BindProperty)));
        }

        [Test]
        public void ArgumentNullExceptionTest()
        {
            IDataContext context = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(()=>context.FindProperty<bool>(nameof(DataContext.BindProperty)));
        }

        [Test]
        public void ArgumentExceptionTest() =>
            Assert.Throws<ArgumentException>(()=>_context.FindProperty<bool>(null));

        [Test]
        public void KernelInvalidOperationExceptionTest()
        {
            var context = new DataContext();
            context.Unbind();
            Assert.Throws<InvalidOperationException>(() => context.FindProperty<bool>(nameof(DataContext.BindProperty)));
        }

        [Test]
        public void InvalidOperationExceptionTest()
        {
            var context = new DataContextWithException();
            context.Register();
            context.ThrowsError = true;
            Assert.Throws<InvalidOperationException>(() => context.FindProperty<bool>(nameof(DataContext.BindProperty)));
        }


        private class DataContext : IDataContext, IBinder
        {
            public IDataContext Context => this;

            public DataContext() => Bind();
            public void Bind() => Context.Register();

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