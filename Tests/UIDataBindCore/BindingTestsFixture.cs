using NUnit.Framework;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Extensions;
using UIDataBindCore.Properties;

namespace Tests.UIDataBindCore
{
    [TestFixture]
    public class BindingTestsFixture
    {
        [Test]
        public void Test()
        {
            var dataContext = new DataContext();
            var booleanBinder = new BooleanPropertyBinder();
            booleanBinder.Bind(dataContext);

            Assert.That(booleanBinder.Property, Is.Not.Null);
            Assert.That(booleanBinder.Value, Is.EqualTo(dataContext.Boolean));
            dataContext.Boolean = false;
            Assert.That(booleanBinder.Value, Is.EqualTo(dataContext.Boolean));
            booleanBinder.Value = true;
            Assert.That(booleanBinder.Value, Is.EqualTo(dataContext.Boolean));
            booleanBinder.Unbind();
            dataContext.Boolean = false;
            Assert.That(booleanBinder.Property, Is.Null);

            dataContext.Unbind();
        }

        private class BooleanPropertyBinder : IBinder
        {
            private const string Path = "_booleanProperty";
            public IBindProperty<bool> Property;

            public bool Value
            {
                get => Property.Value;
                set => Property.Value = value;
            }
            public void Bind(IDataContext context)
            {
                Property = context.FinProperty<bool>(Path);
            }

            public void Unbind() => Property = null;
        }
        private class DataContext : IDataContext, IBinder
        {
            public DataContext() => Bind(this);
            public void Bind(IDataContext context) => context.Register();

            public void Unbind()
            {
                _booleanProperty.Dispose();
                this.Unregister();
            }

            [Bind]
            private readonly BindProperty<bool> _booleanProperty = new BindProperty<bool>(true);

            public bool Boolean
            {
                get => _booleanProperty.Value;
                set => _booleanProperty.Value = value;
            }
        }
    }
}