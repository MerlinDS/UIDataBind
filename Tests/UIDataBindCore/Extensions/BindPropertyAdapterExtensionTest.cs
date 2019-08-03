using System;
using NSubstitute;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Converters;
using UIDataBindCore.Extensions;

namespace Tests.UIDataBindCore.Extensions
{
    [TestFixture]
    public class BindPropertyAdapterExtensionTest
    {
        [Test]
        public void Test()
        {
            var converters = new ConversionMethods().RegisterBuildIn();
            var a = Substitute.For<IBindProperty<int>>();
            a.ValueType.Returns(typeof(int));
            a.Value.Returns(1);
            var b = converters.AsPropertyOf<bool>(a);
            Assert.That(b, Is.Not.Null);
            Assert.That(b.Value, Is.True);
            a.Value.Returns(0);
            var c = converters.AsPropertyOf<bool>(a);
            Assert.That(c, Is.Not.Null);
            Assert.That(c.Value, Is.False);
        }

        [Test]
        public void ConvertersArgumentNullExceptionTest()
        {
            IConversionMethods converters = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => converters.AsPropertyOf<bool>( Substitute.For<IBindProperty<int>>()));
        }

        [Test]
        public void SourceArgumentNullExceptionTest()
        {
            var converters = new ConversionMethods().RegisterBuildIn();
            Assert.Throws<ArgumentNullException>(() => converters.AsPropertyOf<bool>(null));
        }

        [Test]
        public void SamePropertyTest()
        {
            var converters = new ConversionMethods().RegisterBuildIn();
            var a = Substitute.For<IBindProperty<int>>();
            a.ValueType.Returns(typeof(int));
            var b = converters.AsPropertyOf<int>(a);
            Assert.That(b, Is.SameAs(a));
        }

        [Test]
        public void HasNoConversionTest()
        {
            var converters = new ConversionMethods();
            var a = Substitute.For<IBindProperty<int>>();

            var b = converters.AsPropertyOf<int>(a);
            Assert.That(b, Is.Null);
        }
    }
}