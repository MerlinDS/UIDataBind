using System;
using NUnit.Framework;
using UIDataBindCore.Converters;
using UIDataBindCore.Extensions;

namespace Tests.UIDataBindCore.Extensions
{
    [TestFixture]
    public class BuildInConvertersExtensionTest
    {
        private IConversionMethods _methods;

        [SetUp]
        public void SetUp() =>
            _methods = new ConversionMethods().RegisterBuildIn();

        [TearDown]
        public void TearDown() =>
            _methods.Dispose();

        [Test]
        public void ArgumentNullExceptionTest()
        {
            IConversionMethods c = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => c.RegisterBuildIn());
        }

        [Test]
        public void BooleanTest()
        {
            Test<bool, byte>();
            Test<bool, int>();
            Test<bool, float>();
            Test<bool, double>();
            Test<bool, string>();
        }

        [Test]
        public void ByteTest()
        {
            Test<byte, bool>();
            Test<byte, int>();
            Test<byte, float>();
            Test<byte, double>();
            Test<byte, string>();
        }


        [Test]
        public void SingleTest()
        {
            Test<float, bool>();
            Test<float, byte>();
            Test<float, int>();
            Test<float, double>();
            Test<float, string>();
        }

        [Test]
        public void DoubleTest()
        {
            Test<double, bool>();
            Test<double, byte>();
            Test<double, int>();
            Test<double, float>();
            Test<double, string>();
        }
        [Test]
        public void StringTest()
        {
            Test<string, bool>();
            Test<string, byte>();
            Test<string, int>();
            Test<string, float>();
            Test<string, double>();
        }

        private void Test<TValue0, TValue1>()
        {
            var converter = _methods.Retrieve(typeof(TValue0), typeof(TValue1));

            Assert.That(converter, Is.Not.Null);
            Assert.That(converter, Is.InstanceOf<Func<TValue0, TValue1>>());
        }
    }
}