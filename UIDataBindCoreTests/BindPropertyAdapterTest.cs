using System;
using NSubstitute;
using NUnit.Framework;
using UIDataBindCore.Converters;
using UIDataBindCore.Properties;

namespace UIDataBindCoreTests
{
    [TestFixture]
    public class BindPropertyAdapterTest
    {
        [Test]
        public void Test()
        {
            var property = Substitute.ForPartsOf<BindProperty<int>>(1);
            var adapter = new BindPropertyAdapter<int, bool>(property, SafeConvert.ToBoolean, SafeConvert.ToInt32);
            Assert.That(adapter.ValueType, Is.EqualTo(typeof(bool)));
            Assert.That(adapter.Value, Is.True);

            var invokesCount = 0;

            void Handler(bool x)
            {
                Assert.That(x, Is.False);
                invokesCount++;
            }

            adapter.OnUpdate += Handler;
            property.Value = 0;
            adapter.OnUpdate -= Handler;
            Assert.That(invokesCount, Is.GreaterThan(0));
            Assert.That(adapter.Value, Is.False);

            adapter.Value = true;
            Assert.That(property.Value, Is.EqualTo(1));
            Assert.That(adapter.Value, Is.True);

            adapter.OnUpdate += Handler;
            adapter.Dispose();
            adapter.OnUpdate -= Handler;
            property.Value = 1;
            Assert.That(invokesCount, Is.Not.GreaterThan(1));
        }

        [Test]
        public void FormatExceptionTest()
        {
            var property = Substitute.ForPartsOf<BindProperty<int>>(1);
            bool ToSource(int i) => throw new FormatException();
            int ToTarget(bool i) => throw new FormatException();
            var adapter = new BindPropertyAdapter<int, bool>(property, ToTarget, ToSource) {Value = false};
            Assert.That(property.Value, Is.EqualTo(1));
            Assert.That(adapter.Value, Is.False);
            adapter.Value = true;
            Assert.That(property.Value, Is.EqualTo(1));
            Assert.That(adapter.Value, Is.False);
        }
    }
}