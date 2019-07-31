using System;
using NSubstitute;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Extensions;

namespace UIDataBindCoreTests.Extensions
{
    [TestFixture]
    public class BindPropertyConvertingExtensionTest
    {
        [Test]
        public void IntConversionTest()
        {
            var source = Substitute.For<IBindProperty<int>>();
            source.Value.Returns(5);
            source.ValueType.Returns(typeof(int));

            var @string = source.AsPropertyOf<string>();
            Assert.That(@string, Is.Not.Null);
            Assert.That(@string.Value, Is.EqualTo(source.Value.ToString()));

            var @float = source.AsPropertyOf<float>();
            Assert.That(@float, Is.Not.Null);
            Assert.That(@float.Value, Is.EqualTo(Convert.ToSingle(source.Value)));
        }
    }
}