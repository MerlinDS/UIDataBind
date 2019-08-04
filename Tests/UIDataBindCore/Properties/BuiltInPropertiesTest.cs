using NUnit.Framework;
using UIDataBindCore.Properties;

namespace Tests.UIDataBindCore.Properties
{
    [TestFixture]
    public class BuiltInPropertiesTest
    {
        [Test]
        public void Test()
        {
            Assert.That(new BooleanProperty(true).Value, Is.True);
            Assert.That(new ByteProperty(1).Value, Is.EqualTo(1));
            Assert.That(new IntProperty(1).Value, Is.EqualTo(1));
            Assert.That(new FloatProperty(1).Value, Is.EqualTo(1));
            Assert.That(new DoubleProperty(1).Value, Is.EqualTo(1));
            Assert.That(new StringProperty("some string").Value, Is.EqualTo("some string"));
        }
    }
}