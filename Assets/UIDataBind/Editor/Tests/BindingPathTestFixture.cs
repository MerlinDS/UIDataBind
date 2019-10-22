using System;
using System.Linq;
using NUnit.Framework;
using UIDataBind.Base;

namespace UIDataBind.Editor.Tests
{
    [TestFixture]
    public class BindingPathTestFixture
    {
        [Test]
        public void EqualsTest()
        {
            
            // ReSharper disable EqualExpressionComparison
            Assert.That(BPath.Empty == BPath.Empty, Is.True);
            Assert.That(BPath.Empty != BPath.Empty, Is.False);
            Assert.That(BPath.BuildFrom("Test") == BPath.BuildFrom("Test"), Is.True);
            Assert.That(BPath.BuildFrom("Test") != BPath.BuildFrom("Other"), Is.True);
            // ReSharper restore EqualExpressionComparison
            Assert.That(BPath.BuildFrom("Test"), Is.EqualTo(BPath.BuildFrom("Test")));
            Assert.That(BPath.BuildFrom("Test"), Is.Not.EqualTo(BPath.BuildFrom("Other")));

            Assert.That(BPath.BuildFrom("Other").CompareTo(BPath.BuildFrom("Test")), Is.EqualTo(1));
            Assert.That(BPath.BuildFrom("Test").CompareTo(BPath.BuildFrom("Other")), Is.EqualTo(-1));
            Assert.That(BPath.BuildFrom("Test").CompareTo(BPath.BuildFrom("Test")), Is.EqualTo(0));

            Assert.That(BPath.BuildFrom("Test").GetHashCode(), Is.Not.EqualTo(0));

        }

        [Test]
        public void BuildingExceptionsTest()
        {
            Assert.Throws<ArgumentException>(() => BPath.BuildFrom("Test", string.Empty));
            Assert.Throws<ArgumentException>(() => BPath.BuildFrom(string.Empty, "Test"));
            Assert.Throws<ArgumentException>(() => BPath.BuildFrom(string.Empty));
            Assert.Throws<ArgumentException>(() => BPath.BuildFrom(BPath.Empty, string.Empty));
            Assert.Throws<ArgumentException>(() => BPath.BuildFrom(BPath.BuildFrom("Test"), string.Empty));

            Assert.Throws<ArgumentOutOfRangeException>(() => BPath.BuildFrom(GetStrings(BPath.MaxLength + 1)));
            var parent = BPath.BuildFrom(GetStrings(BPath.MaxLength - 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => BPath.BuildFrom(parent, GetStrings(2)));
        }

        [Test]
        public void BuildingTest()
        {
            Assert.That(BPath.Empty.Lenght, Is.EqualTo(0));
            for (var i = 1; i < BPath.MaxLength; i++)
            {
                var strings = GetStrings(i);
                var path = BPath.BuildFrom(strings);
                Assert.That(path.Lenght, Is.EqualTo(i));
                Assert.That(path.ToString(), Is.EqualTo(strings.Aggregate((a, b) => $"{a}.{b}")));
            }
        }

        [Test]
        public void BuildingFromParentTest()
        {
            var path = BPath.Empty;
            var expString = string.Empty;
            for (var i = 1; i < BPath.MaxLength; i++)
            {
                var str = i.ToString();
                path = BPath.BuildFrom(path, str);
                expString += i > 1 ? $".{str}" : str;
                Assert.That(path.Lenght, Is.EqualTo(i));
                Assert.That(path.ToString(), Is.EqualTo(expString));
            }
        }

        [Test]
        public void GetParentTest()
        {
            var strings = GetStrings(BPath.MaxLength);
            var path = BPath.BuildFrom(strings);
            var i = path.Lenght;
            while (--i > 0)
            {
                path = BPath.GetParent(path);
                Assert.That(path.Lenght, Is.EqualTo(i));
                Assert.That(path.ToString(), Is.EqualTo(GetStrings(i).Aggregate((a, b) => $"{a}.{b}")));
            }
        }

        private static string[] GetStrings(int count)
        {
            var sources = new string[count];
            for (var i = 0; i < count; i++)
                sources[i] = i.ToString();
            return sources;
        }
    }
}