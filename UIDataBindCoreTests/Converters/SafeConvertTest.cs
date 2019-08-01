using System;
using System.Globalization;
using NUnit.Framework;
using UIDataBindCore.Converters;

namespace UIDataBindCoreTests.Converters
{
    [TestFixture]
    public class SafeConvertTest
    {
        private const string BigString =
            "111111111111111111111111111111111111111111111111111111111111111111";
        private const string BigFloatString =
            "111111111111111111111111111111111111111111111111111111111111111111,01";

        [Test]
        public void BooleanTest()
        {
            Assert.That(SafeConvert.ToBoolean((byte)1), Is.True);
            Assert.That(SafeConvert.ToBoolean((byte)0), Is.False);

            Assert.That(SafeConvert.ToBoolean(1), Is.True);
            Assert.That(SafeConvert.ToBoolean(0), Is.False);

            Assert.That(SafeConvert.ToBoolean(1F), Is.True);
            Assert.That(SafeConvert.ToBoolean(0F), Is.False);

            Assert.That(SafeConvert.ToBoolean(1D), Is.True);
            Assert.That(SafeConvert.ToBoolean(0D), Is.False);


            Assert.That(SafeConvert.ToBoolean(bool.TrueString), Is.True);
            Assert.That(SafeConvert.ToBoolean(bool.FalseString), Is.False);
            Assert.That(SafeConvert.ToBoolean(null), Is.False);
            Assert.That(SafeConvert.ToBoolean(string.Empty), Is.False);
        }

        [Test]
        public void ByteTest()
        {
            Assert.That(SafeConvert.ToByte(true), Is.EqualTo(1));
            Assert.That(SafeConvert.ToByte(false), Is.EqualTo(0));

            Assert.That(SafeConvert.ToByte(1), Is.EqualTo(1));
            Assert.That(SafeConvert.ToByte(int.MaxValue), Is.EqualTo(byte.MaxValue));
            Assert.That(SafeConvert.ToByte(int.MinValue), Is.EqualTo(byte.MinValue));

            Assert.That(SafeConvert.ToByte(1F), Is.EqualTo(1));
            Assert.That(SafeConvert.ToByte(float.MaxValue), Is.EqualTo(byte.MaxValue));
            Assert.That(SafeConvert.ToByte(float.MinValue), Is.EqualTo(byte.MinValue));

            Assert.That(SafeConvert.ToByte(1D), Is.EqualTo(1));
            Assert.That(SafeConvert.ToByte(double.MaxValue), Is.EqualTo(byte.MaxValue));
            Assert.That(SafeConvert.ToByte(double.MinValue), Is.EqualTo(byte.MinValue));

            Assert.That(SafeConvert.ToByte(1.ToString()), Is.EqualTo(1));
            Assert.That(SafeConvert.ToByte(BigString), Is.EqualTo(byte.MaxValue));

            Assert.That(SafeConvert.ToByte("FormatException"), Is.EqualTo(0));
        }

        [Test]
        public void IntTest()
        {
            Assert.That(SafeConvert.ToInt32(true), Is.EqualTo(1));
            Assert.That(SafeConvert.ToInt32(false), Is.EqualTo(0));
            Assert.That(SafeConvert.ToInt32(1), Is.EqualTo(1));
            Assert.That(SafeConvert.ToInt32(byte.MaxValue), Is.EqualTo(byte.MaxValue));
            Assert.That(SafeConvert.ToInt32(int.MaxValue), Is.EqualTo(int.MaxValue));

            Assert.That(SafeConvert.ToInt32(1F), Is.EqualTo(1));
            Assert.That(SafeConvert.ToInt32(float.MaxValue), Is.EqualTo(int.MaxValue));
            Assert.That(SafeConvert.ToInt32(float.MinValue), Is.EqualTo(int.MinValue));

            Assert.That(SafeConvert.ToInt32(1D), Is.EqualTo(1));
            Assert.That(SafeConvert.ToInt32(double.MaxValue), Is.EqualTo(int.MaxValue));
            Assert.That(SafeConvert.ToInt32(double.MinValue), Is.EqualTo(int.MinValue));

            Assert.That(SafeConvert.ToInt32(1.ToString()), Is.EqualTo(1));
            Assert.That(SafeConvert.ToInt32(BigString), Is.EqualTo(int.MaxValue));

            Assert.That(SafeConvert.ToInt32("FormatException"), Is.EqualTo(0));
        }

        [Test]
        public void SingleTest()
        {
            Assert.That(SafeConvert.ToSingle(true), Is.EqualTo(1));
            Assert.That(SafeConvert.ToSingle(false), Is.EqualTo(0));

            Assert.That(SafeConvert.ToSingle((byte)1), Is.EqualTo(1));
            Assert.That(SafeConvert.ToSingle(1), Is.EqualTo(1));

            Assert.That(SafeConvert.ToSingle(1D), Is.EqualTo(1));
            Assert.That(SafeConvert.ToSingle(double.MaxValue), Is.EqualTo(float.MaxValue));
            Assert.That(SafeConvert.ToSingle(double.MinValue), Is.EqualTo(float.MinValue));

            Assert.That(SafeConvert.ToSingle(1F.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(1));
            Assert.That(SafeConvert.ToSingle(BigFloatString), Is.EqualTo(float.MaxValue));

            Assert.That(SafeConvert.ToSingle("FormatException"), Is.EqualTo(0));
        }

        [Test]
        public void DoubleTest()
        {
            Assert.That(SafeConvert.ToDouble(true), Is.EqualTo(1));
            Assert.That(SafeConvert.ToDouble(false), Is.EqualTo(0));

            Assert.That(SafeConvert.ToDouble((byte)1), Is.EqualTo(1));
            Assert.That(SafeConvert.ToDouble(1), Is.EqualTo(1));
            Assert.That(SafeConvert.ToDouble(1F), Is.EqualTo(1));

            Assert.That(SafeConvert.ToDouble(1D.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(1));
            Assert.That(SafeConvert.ToDouble("FormatException"), Is.EqualTo(0));
        }

        [Test]
        public void StringTest()
        {
            Assert.That(SafeConvert.ToString(true), Is.EqualTo(bool.TrueString));
            Assert.That(SafeConvert.ToString(false), Is.EqualTo(bool.FalseString));

            Assert.That(SafeConvert.ToString((byte)1), Is.EqualTo(Convert.ToString((byte)1)));
            Assert.That(SafeConvert.ToString(1), Is.EqualTo(Convert.ToString(1)));
            Assert.That(SafeConvert.ToString(1F), Is.EqualTo(Convert.ToString(1F, CultureInfo.CurrentCulture)));
            Assert.That(SafeConvert.ToString(1D), Is.EqualTo(Convert.ToString(1D, CultureInfo.CurrentCulture)));
        }
    }
}