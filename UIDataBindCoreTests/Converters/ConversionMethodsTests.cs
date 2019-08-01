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
            conversionMethods.Register(fromBoolToInt, fromIntToBool);
            Assert.That(conversionMethods.Has(typeof(bool), typeof(int)), Is.True);
            Assert.That(conversionMethods.Has(typeof(int), typeof(bool)), Is.True);
            Assert.That(conversionMethods.Has(typeof(bool), typeof(float)), Is.False);
            conversionMethods.Register(fromBoolToInt, fromIntToBool);//Do noting
            Assert.That(conversionMethods.Retrieve(typeof(bool), typeof(int)), Is.SameAs(fromBoolToInt));
            Assert.That(conversionMethods.Retrieve(typeof(int), typeof(bool)), Is.SameAs(fromIntToBool));
            Assert.That(conversionMethods.Retrieve(typeof(int), typeof(float)), Is.Null);
            conversionMethods.Dispose();
            Assert.That(conversionMethods.Retrieve(typeof(int), typeof(bool)), Is.Null);
        }
    }
}