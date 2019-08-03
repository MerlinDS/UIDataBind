using NUnit.Framework;
using UIDataBindCore.Properties;

namespace Tests.UIDataBindCore.Properties
{
    [TestFixture]
    public class BindPropertyTest
    {
        [Test]
        public void Test()
        {
            var value = 10;
            var property = new BindProperty<int>(value);
            Assert.That(property.Value, Is.EqualTo(value));
            Assert.That(property.ValueType, Is.EqualTo(typeof(int)));

            var invokesCount = 0;

            void Handler(int x)
            {
                Assert.AreEqual(value, x);
                invokesCount++;
            }

            value = 5;
            property.OnUpdate += Handler;
            property.Value = value;
            Assert.That(property.Value, Is.EqualTo(value));
            Assert.That(invokesCount, Is.GreaterThan(0));
            property.Dispose();
        }
    }
}