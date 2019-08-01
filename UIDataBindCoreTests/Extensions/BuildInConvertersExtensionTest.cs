using System;
using NUnit.Framework;
using UIDataBindCore.Converters;
using UIDataBindCore.Extensions;

namespace UIDataBindCoreTests.Extensions
{
    [TestFixture]
    public class BuildInConvertersExtensionTest
    {
        private ConvertersCollection _collection;

        [SetUp]
        public void SetUp()
        {
            _collection = new ConvertersCollection();
            _collection.RegisterBuildIn();
        }

        [TearDown]
        public void TearDown() =>
            _collection.Dispose();

        [Test]
        public void ArgumentNullExceptionTest()
        {
            ConvertersCollection c = null;
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
            var converter = _collection.Retrieve<TValue0>(typeof(TValue1));
            Assert.That(converter, Is.Not.Null);
            Assert.That(converter, Is.InstanceOf<IPropertyConverter<TValue0, TValue1>>()
                            .Or.InstanceOf<IPropertyConverter<TValue1, TValue0>>());
        }
    }
}