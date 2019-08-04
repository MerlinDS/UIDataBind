using System;
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
        public void BindPropertyWithSameTypeTest()
        {
            var dataContext = new DataContext();
            var booleanBinder = new PropertyBinder<bool>(nameof(dataContext.BooleanProperty) );
            booleanBinder.Bind(dataContext);

            Assert.That(booleanBinder.HasProperty, Is.True);
            Assert.That(booleanBinder.Value, Is.EqualTo(dataContext.Boolean));
            dataContext.Boolean = false;
            Assert.That(booleanBinder.Value, Is.EqualTo(dataContext.Boolean));
            booleanBinder.Value = true;
            Assert.That(booleanBinder.Value, Is.EqualTo(dataContext.Boolean));
            booleanBinder.Unbind();
            dataContext.Boolean = false;
            Assert.That(booleanBinder.HasProperty, Is.False);

            dataContext.Unbind();
        }

        [Test]
        public void BindPropertyWithDiffTypeTest()
        {
            var dataContext = new DataContext();
            var booleanBinder = new PropertyBinder<bool>(nameof(dataContext.IntProperty) );
            booleanBinder.Bind(dataContext);

            Assert.That(booleanBinder.HasProperty, Is.True);
            Assert.That(booleanBinder.Value, Is.EqualTo(Convert.ToBoolean(dataContext.Int32)));
            dataContext.Int32 = 0;
            Assert.That(booleanBinder.Value, Is.EqualTo(Convert.ToBoolean(dataContext.Int32)));
            booleanBinder.Value = true;
            Assert.That(booleanBinder.Value, Is.EqualTo(Convert.ToBoolean(dataContext.Int32)));
            booleanBinder.Unbind();
            dataContext.Int32 = 1;
            Assert.That(booleanBinder.HasProperty, Is.False);

            dataContext.Unbind();
        }

        [Test]
        public void BindMethodTest()
        {
            var dataContext = new DataContext();
            var binder = new MethodBinder(nameof(dataContext.BindMethod) );
            binder.Bind(dataContext);
            Action expectedMethod = dataContext.BindMethod;
            dataContext.MethodInvoked = false;

            Assert.That(binder.HasMethod, Is.True);
            Assert.That(binder.Method.Target, Is.SameAs(expectedMethod.Target));
            binder.Invoke();
            Assert.That(dataContext.MethodInvoked, Is.True);
            binder.Unbind();
            Assert.That(binder.HasMethod, Is.False);

            dataContext.Unbind();
        }

        private class PropertyBinder<TValue> : IBinder
        {
            private readonly string _path;
            private IBindProperty<TValue> _property;

            public bool HasProperty=> _property != null;
            public TValue Value
            {
                get => _property.Value;
                set => _property.Value = value;
            }
            public PropertyBinder(string path) => _path = path;

            public void Bind(IDataContext context) => _property = context.FindProperty<TValue>(_path);

            public void Unbind() => _property = null;
        }

        private class MethodBinder : IBinder
        {
            private readonly string _path;
            private Action _method;

            public bool HasMethod=> _method != null;

            public void Invoke() => _method.Invoke();

            public MethodBinder(string path) => _path = path;

            public void Bind(IDataContext context) => _method = context.FindMethod(_path);

            public void Unbind() => _method = null;

            public Action Method => _method;
        }

        private class DataContext : IDataContext, IBinder
        {
            public DataContext() => Bind(this);
            public void Bind(IDataContext context) => context.Register();
            public bool MethodInvoked;

            public void Unbind()
            {
                BooleanProperty.Dispose();
                this.Unregister();
            }

            [Bind]
            public void BindMethod() => MethodInvoked = true;

            [Bind]
            public readonly BindProperty<bool> BooleanProperty = new BindProperty<bool>(true);

            public bool Boolean
            {
                get => BooleanProperty.Value;
                set => BooleanProperty.Value = value;
            }

            [Bind]
            public readonly BindProperty<int> IntProperty = new BindProperty<int>(1);

            public int Int32
            {
                get => IntProperty.Value;
                set => IntProperty.Value = value;
            }
        }
    }
}