using NSubstitute;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Converters;
using UIDataBindCore.Extensions;

namespace UIDataBindCoreTests.Extensions
{
    [TestFixture]
    public class BindPropertyAdapterExtensionTest
    {
        [Test]
        public void Test()
        {
            var converters = new ConvertersCollection().RegisterBuildIn();
            var a = Substitute.For<IBindProperty<int>>();
            a.Value.Returns(1);
            a.ValueType.Returns(typeof(int));
            var b = converters.AsPropertyOf<bool>(a);
            Assert.That(b, Is.Not.Null);
            Assert.That(b.Value, Is.True);
            a.Value = 0;
            Assert.That(b.Value, Is.False);
        }
    }
}