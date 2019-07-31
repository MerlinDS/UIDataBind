using System;
using NSubstitute;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Extensions;
using UIDataBindCore.Properties.Adapters;

namespace UIDataBindCoreTests.Adapters
{
    [TestFixture]
    public class NumberAdaptersTests
    {
        [Test]
        public void Test([Random(byte.MinValue, byte.MaxValue, 10)] byte value) => NumberTest(value);

        [Test]
        public void Test([Random(int.MinValue, int.MaxValue, 10)] int value) => NumberTest(value);

        [Test]
        public void Test([Random(float.MinValue, float.MaxValue, 10)] float value) => NumberTest(value);

        [Test]
        public void Test([Random(float.MinValue, float.MaxValue, 10)] double value) => NumberTest(value);

        [Test]
        public void Test([Random(int.MinValue, int.MaxValue, 10)] decimal value) => NumberTest(value);

        [Test]
        public void TestFormatException()
        {
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<byte>>().To<object>());
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<int>>().To<object>());
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<float>>().To<object>());
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<double>>().To<object>());
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<decimal>>().To<object>());
        }


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

//            property.To<ushort>();
        }


        private static void TestConversion<TTarget>(IBindProperty property, Func<TTarget> check)
        {

            var adapter = property.AsPropertyOf<TTarget>();
            Assert.That(adapter, Is.Not.Null);

            try
            {
                var expected = check.Invoke();
                Assert.That(adapter.Value, Is.EqualTo(expected));
            }
            catch (OverflowException e)
            {
//                Assert.That(adapter.Value, Is.EqualTo(expected));
            }
        }
    }
}