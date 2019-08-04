using System;
using NUnit.Framework;

namespace Tests.UIDataBindCore
{
    [TestFixture]
    public class SampleBindingTestsFixture
    {
        [Test]
        public void BindPropertyWithSameTypeTest()
        {
            var dataContext = new SampleDataContext();
            var booleanBinder = new SamplePropertyBinder<bool>(nameof(dataContext.BooleanProperty) );
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
            var dataContext = new SampleDataContext();
            var booleanBinder = new SamplePropertyBinder<bool>(nameof(dataContext.IntProperty) );
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
            var dataContext = new SampleDataContext();
            var binder = new SampleMethodBinder(nameof(dataContext.BindMethod) );
            binder.Bind(dataContext);
            Action expectedMethod = dataContext.BindMethod;
            dataContext.Boolean = false;

            Assert.That(binder.HasMethod, Is.True);
            Assert.That(binder.Method.Target, Is.SameAs(expectedMethod.Target));
            binder.Invoke();
            Assert.That(dataContext.Boolean, Is.True);
            binder.Unbind();
            Assert.That(binder.HasMethod, Is.False);

            dataContext.Unbind();
        }
    }
}