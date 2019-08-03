using NUnit.Framework;
using UIDataBindCore.Base;

namespace Tests.UIDataBindCore.Base
{
    [TestFixture]
    public class TypesPairTest
    {
        [Test]
        public void AccessorsTest()
        {
            var typeA = typeof(int);
            var typeB = typeof(bool);
            var pair = new TypesPair(typeA, typeB);
            Assert.That(pair.A, Is.SameAs(typeA));
            Assert.That(pair.B, Is.SameAs(typeB));
        }

        [Test]
        public void EqualsTest()
        {
            var a = new TypesPair(typeof(int), typeof(bool));
            var b = new TypesPair(typeof(int), typeof(bool));
            var c = new TypesPair( typeof(bool), typeof(int));
            Assert.That(a.Equals(a), Is.True);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a.Equals(c), Is.False);

            Assert.That(a.Equals((object)a), Is.True);
            Assert.That(a.Equals((object)b), Is.True);
            Assert.That(a.Equals((object)c), Is.False);

            Assert.That(a.Equals(typeof(int), typeof(bool)), Is.True);
            Assert.That(a.Equals(typeof(bool), typeof(int)), Is.False);
        }

        [Test]
        public void GetHashCodeTest()
        {
            var a = new TypesPair(typeof(int), typeof(bool)).GetHashCode();
            var b = new TypesPair(typeof(int), typeof(bool)).GetHashCode();
            var c = new TypesPair( typeof(bool), typeof(int)).GetHashCode();
            Assert.That(a, Is.EqualTo(a));
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a, Is.Not.EqualTo(c));
        }

        [Test]
        public void ToStringTest()
        {
            var a = new TypesPair(typeof(int), typeof(bool)).ToString();
            var expected = $"{nameof(TypesPair)}[{typeof(int)} {typeof(bool)}]";
            Assert.That(a, Is.EqualTo(expected));
        }
    }
}