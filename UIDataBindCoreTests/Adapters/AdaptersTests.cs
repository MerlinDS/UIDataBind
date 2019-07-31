using System;
using System.Globalization;
using NSubstitute;
using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Extensions;

namespace UIDataBindCoreTests.Adapters
{
    [TestFixture]
    public class AdaptersTests
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
        public void TestBoolean() => NumberTest(true);


        [Test]
        public void TestStrings()
        {
            var property = Substitute.For<IBindProperty<string>>();
            property.Value.Returns("true");
            TestConversion(property, () => Convert.ToBoolean(property.Value));
            property.Value.Returns("1");
            TestConversion(property, () => Convert.ToByte(property.Value));
            TestConversion(property, () => Convert.ToInt32(property.Value));
            property.Value.Returns("0,5");
            TestConversion(property, () => Convert.ToSingle(property.Value));
            property.Value.Returns("0,5");
            TestConversion(property, () => Convert.ToDouble(property.Value));
            property.Value.Returns("some string");
            TestConversion(property, () => Convert.ToString(property.Value));
            //Exceptions FormatException
            TestConversion(property, () => false);
            TestConversion(property, () => (byte)0);
            TestConversion(property, () => 0);
            TestConversion(property, () => 0F);
            TestConversion(property, () => 0D);

            property.Value.Returns(long.MaxValue.ToString(CultureInfo.CurrentCulture));
            //Exceptions OverflowException
            TestConversion(property, () => byte.MaxValue);
            TestConversion(property, () => int.MaxValue);
            TestConversion(property, () => float.MaxValue, true);
            TestConversion(property, () => double.MaxValue, true);

        }

        [Test]
        public void TestFormatException()
        {
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<bool>>().To<object>());
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<byte>>().To<object>());
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<int>>().To<object>());
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<float>>().To<object>());
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<double>>().To<object>());
            Assert.Throws(typeof(FormatException), ()=>Substitute.For<IBindProperty<string>>().To<object>());
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
            TestConversion(property, () => Convert.ToString(value));
        }


        private static void TestConversion<TTarget>(IBindProperty property, Func<TTarget> check, bool lessThanOrEqualTo = false)
        {

            var adapter = property.AsPropertyOf<TTarget>();
            Assert.That(adapter, Is.Not.Null);

            try
            {
                var expected = check.Invoke();
                if(!lessThanOrEqualTo)
                    Assert.That(adapter.Value, Is.EqualTo(expected));
                else
                    Assert.That(adapter.Value, Is.LessThanOrEqualTo(expected));
            }
#pragma warning disable 168
            catch (OverflowException e)
#pragma warning restore 168
            {
//                Assert.That(adapter.Value, Is.EqualTo(expected));
            }
        }
    }
}