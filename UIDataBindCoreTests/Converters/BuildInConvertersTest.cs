using NUnit.Framework;
using UIDataBindCore.Converters;

namespace UIDataBindCoreTests.Converters
{
    [TestFixture]
    public class BuildInConvertersTest
    {
        [Test]
        public void BooleanTest()
        {
            Test(new BooleanToByteConverter(), true, (byte)1);
            Test(new BooleanToByteConverter(), false, (byte)0);

            Test(new BooleanToIntConverter(), true, 1);
            Test(new BooleanToIntConverter(), false, 0);

            Test(new BooleanToSingleConverter(), true, 1);
            Test(new BooleanToSingleConverter(), false, 0);

            Test(new BooleanToDoubleConverter(), true, 1);
            Test(new BooleanToDoubleConverter(), false, 0);

            Test(new BooleanToStringConverter(), true, bool.TrueString);
            Test(new BooleanToStringConverter(), false, bool.FalseString);
        }

        [Test]
        public void ByteTest()
        {
            Test(new ByteToBooleanConverter(), (byte)1, true);
            Test(new ByteToBooleanConverter(), (byte)0, false);

            Test(new ByteToIntConverter(), (byte)20, 20);
            Test(new ByteToSingleConverter(), (byte)20, 20);
            Test(new ByteToDoubleConverter(), (byte)20, 20);
            Test(new ByteToStringConverter(), (byte)20, 20.ToString());
        }

        [Test]
        public void IntTest()
        {
            Test(new IntToBooleanConverter(), 1, true);
            Test(new IntToBooleanConverter(), 0, false);

            Test(new IntToByteConverter(), 20, (byte)20);
            Test(new IntToSingleConverter(), 20, 20);
            Test(new IntToDoubleConverter(), 20, 20);
            Test(new IntToStringConverter(), 20, 20.ToString());
        }

        [Test]
        public void SingleTest()
        {
            Test(new SingleToBooleanConverter(), 1, true);
            Test(new SingleToBooleanConverter(), 0, false);

            Test(new SingleToByteConverter(), 20, (byte)20);
            Test(new SingleToIntConverter(), 20, 20);
            Test(new SingleToDoubleConverter(), 20, 20);
            Test(new SingleToStringConverter(), 20, 20.ToString());
        }

        [Test]
        public void DoubleTest()
        {
            Test(new DoubleToBooleanConverter(), 1, true);
            Test(new DoubleToBooleanConverter(), 0, false);

            Test(new DoubleToByteConverter(), 20, (byte)20);
            Test(new DoubleToIntConverter(), 20, 20);
            Test(new DoubleToSingleConverter(), 20, 20);
            Test(new DoubleToStringConverter(), 20, 20.ToString());
        }

        [Test]
        public void StringTest()
        {
            Test(new StringToBooleanConverter(), bool.TrueString, true);
            Test(new StringToBooleanConverter(), bool.FalseString, false);

            Test(new StringToByteConverter(), "20", (byte)20);
            Test(new StringToIntConverter(), "20", 20);
            Test(new StringToSingleConverter(), "20", 20);
            Test(new StringToDoubleConverter(), "20", 20);
        }

        private static void Test<TValue0, TValue1>(IPropertyConverter<TValue0, TValue1> converter, TValue0 a, TValue1 b)
        {
            Assert.That(converter.Convert(a), Is.EqualTo(b));
            Assert.That(converter.Convert(b), Is.EqualTo(a));
        }
    }
}