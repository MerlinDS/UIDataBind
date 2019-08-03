using System.Reflection;
using NUnit.Framework;
using Tests.UIDataBindCore.Utils;
using UIDataBindCore.Attributes;

namespace Tests.UIDataBindCore.Attributes
{
    [TestFixture]
    public class BindAttributeTest
    {
        [Test]
        public void TestFieldAttribute()
        {
            var contextType = new TestDataContext().GetType();
            var bindMemberField =
                contextType.GetField(nameof(TestDataContext.BindMember), BindingFlags.Instance | BindingFlags.Public);
            // ReSharper disable once AssignNullToNotNullAttribute
            var attribute = (BindAttribute) System.Attribute.GetCustomAttribute(bindMemberField, typeof(BindAttribute));
            attribute.Name = bindMemberField.Name;

            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Alias, Is.EqualTo(TestDataContext.BindMemberAlias));
            Assert.That(attribute.Help, Is.EqualTo(TestDataContext.BindMemberHelp));
            Assert.That(attribute.Name, Is.EqualTo(nameof(TestDataContext.BindMember)));
        }

        [Test]
        public void TestMethodAttribute()
        {
            var context = new TestDataContext();
            var contextType = context.GetType();
            var bindMethod = contextType.GetMethod(nameof(TestDataContext.BindMethod),
                                                   BindingFlags.Instance | BindingFlags.Public);
            // ReSharper disable once AssignNullToNotNullAttribute
            var attribute = (BindAttribute) System.Attribute.GetCustomAttribute(bindMethod, typeof(BindAttribute));
            attribute.Name = bindMethod.Name;

            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Alias, Is.EqualTo(TestDataContext.BindMethodAlias));
            Assert.That(attribute.Help, Is.EqualTo(TestDataContext.BindMethodHelp));
            Assert.That(attribute.Name, Is.EqualTo(nameof(TestDataContext.BindMethod)));
            context.BindMethod();
        }
    }
}