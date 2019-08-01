using System;
using NSubstitute;
using NUnit.Framework;
using UIDataBindCore.Converters;

namespace UIDataBindCoreTests.Converters
{
    [TestFixture]
    public class ConversionMethodsTests
    {
        [Test]
        public void Test()
        {
            var conversionMethods = new ConversionMethods();
            var fromBoolToInt = Substitute.For<Func<bool, int>>();
            var fromIntToBool = Substitute.For<Func<int, bool>>();
            conversionMethods.Register(fromIntToBool, fromBoolToInt);
            conversionMethods.Register(fromIntToBool, fromBoolToInt);//Do noting
            Assert.That(conversionMethods.Retrieve<bool, int>(), Is.SameAs(fromBoolToInt));
            Assert.That(conversionMethods.Retrieve<int, bool>(), Is.SameAs(fromIntToBool));
            Assert.That(conversionMethods.Retrieve<int, float>(), Is.Null);
            conversionMethods.Dispose();
            Assert.That(conversionMethods.Retrieve<int, bool>(), Is.Null);
        }
    }
}