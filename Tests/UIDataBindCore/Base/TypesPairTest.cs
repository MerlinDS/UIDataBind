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
            var pair = TypesPair.Create<int, bool>();
            Assert.That(pair.A, Is.SameAs(typeof(int)));
            Assert.That(pair.B, Is.SameAs(typeof(bool)));
        }

        [Test]
        public void EqualsTest()
        {
            var a = TypesPair.Create<int, bool>();
            var b = TypesPair.Create<int, bool>();
            var c = TypesPair.Create<bool, int>();
            Assert.That(a.Equals(a), Is.True);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a.Equals(c), Is.False);

            Assert.That(a.Equals((object)a), Is.True);
            Assert.That(a.Equals((object)b), Is.True);
            Assert.That(a.Equals((object)c), Is.False);

            Assert.That(a.Equals(typeof(int), typeof(bool)), Is.True);
            Assert.That(a.Equals(typeof(bool), typeof(int)), Is.False);

            Assert.That(a.Equals<int, bool>(), Is.True);
            Assert.That(a.Equals<bool, bool>(), Is.False);
        }

        [Test]
        public void GetHashCodeTest()
        {
            var a = TypesPair.Create<int, bool>().GetHashCode();
            var b = TypesPair.Create<int, bool>().GetHashCode();
            var c = TypesPair.Create<bool, int>().GetHashCode();
            Assert.That(a, Is.EqualTo(a));
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a, Is.Not.EqualTo(c));
        }

        [Test]
        public void ToStringTest()
        {
            var a = TypesPair.Create<int, bool>().ToString();
            var expected = $"{nameof(TypesPair)}[{typeof(int)} {typeof(bool)}]";
            Assert.That(a, Is.EqualTo(expected));
        }
    }
}