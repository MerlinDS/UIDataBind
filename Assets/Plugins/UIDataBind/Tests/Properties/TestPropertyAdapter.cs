using NUnit.Framework;
using Plugins.UIDataBind.Properties;

namespace Plugins.UIDataBind.Tests.Properties
{
    public class TestPropertyAdapter
    {
        [Test]
        public void TestValueSetting()
        {
            var source = new BaseBindingProperty<int>(10);
            var adapter = new BindingPropertyAdapter<string>(source);

            Assert.AreEqual(source.Value.ToString(), adapter.Value);
            source.Value = 5;
            Assert.AreEqual(source.Value.ToString(), adapter.Value);
            adapter.Value = "10";
            Assert.AreEqual(source.Value.ToString(), adapter.Value);

        }

        [Test]
        public void TestHandling()
        {
            var source = new BaseBindingProperty<int>(10);
            var adapter = new BindingPropertyAdapter<string>(source);
            var invokesCount = 0;

            void Handler(string s)
            {
                Assert.AreEqual(source.Value.ToString(), s);
                invokesCount++;
            }

            adapter.OnUpdateValue += Handler;
            adapter.Value = "10";
            Assert.AreEqual(1, invokesCount);
            source.Value = 5;
            Assert.AreEqual(2, invokesCount);
            adapter.OnUpdateValue -= Handler;
            source.Value = 10;
            Assert.AreEqual(2, invokesCount);
        }

        [Test]
        public void TestValueConversionErrorHandling()
        {
            var source = new BaseBindingProperty<int>(10);
            var adapter = new BindingPropertyAdapter<string>(source) {Value = "name"};

            Assert.AreEqual(source.Value.ToString(), adapter.Value);
        }
    }
}