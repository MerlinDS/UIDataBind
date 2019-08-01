using System;
using NSubstitute;
using NUnit.Framework;
using UIDataBindCore.Base;
using UIDataBindCore.Utils;

namespace UIDataBindCoreTests.Base
{
    [TestFixture]
    public class PropertyConvertersCollectionTest
    {
        [Test]
        public void GetConverterTest()
        {
            var collection = new PropertyConvertersCollection();
            var expectedConverter = Substitute.For<IPropertyConverter<int, object>>();
            collection.Register(expectedConverter);
            collection.Register(expectedConverter);

            Assert.Throws<ArgumentNullException>(()=>collection.Register<int, object>(null));

            var converter = collection.Retrieve<int>(typeof(object));
            Assert.That(converter, Is.Not.Null);
            Assert.That(converter, Is.SameAs(expectedConverter));

            converter = collection.Retrieve<object>(typeof(int));
            Assert.That(converter, Is.Not.Null);
            Assert.That(converter, Is.SameAs(expectedConverter));
            collection.Dispose();
        }
    }
}