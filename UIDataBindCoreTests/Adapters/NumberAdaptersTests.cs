using System;
using NSubstitute;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Extensions;

namespace UIDataBindCoreTests.Adapters
{
    [TestFixture]
    public class NumberAdaptersTests
    {
        [Test]
        public void Test([Random(byte.MinValue, byte.MaxValue, 1)] byte value) => NumberTest(value);

        [Test]
        public void Test([Random(int.MinValue, int.MaxValue, 1)] int value) => NumberTest(value);

        [Test]
        public void Test([Random(float.MinValue, float.MaxValue, 1)] float value) => NumberTest(value);

        [Test]
        public void Test([Random(double.MinValue, double.MaxValue, 1)] double value) => NumberTest(value);

        [Test]
        public void Test([Random(int.MinValue, int.MaxValue, 1)] decimal value) => NumberTest(value);


        private void NumberTest<TSource>(TSource value)
        {
            var property = Substitute.For<IBindProperty<TSource>>();
            property.Value.Returns(value);

            TestConversion(property, () => Convert.ToBoolean(value));
            TestConversion(property, () => Convert.ToByte(value));
            TestConversion(property, () => Convert.ToInt32(value));
            TestConversion(property, () => Convert.ToSingle(value));
            TestConversion(property, () => Convert.ToDouble(value));
            TestConversion(property, () => Convert.ToDecimal(value));
            TestConversion(property, () => Convert.ToString(value));
        }


        private static void TestConversion<TTarget>(IBindProperty property, Func<TTarget> check)
        {
            var adapter = property.AsPropertyOf<TTarget>();
            Assert.That(adapter, Is.Not.Null);
            Assert.That(adapter.Value, Is.EqualTo(check.Invoke()));
        }
    }
}