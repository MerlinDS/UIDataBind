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
            Assert.That(BindingPath.Empty == BindingPath.Empty, Is.True);
            Assert.That(BindingPath.Empty != BindingPath.Empty, Is.False);
            Assert.That(BindingPath.BuildFrom("Test") == BindingPath.BuildFrom("Test"), Is.True);
            Assert.That(BindingPath.BuildFrom("Test") != BindingPath.BuildFrom("Other"), Is.True);
            // ReSharper restore EqualExpressionComparison
            Assert.That(BindingPath.BuildFrom("Test"), Is.EqualTo(BindingPath.BuildFrom("Test")));
            Assert.That(BindingPath.BuildFrom("Test"), Is.Not.EqualTo(BindingPath.BuildFrom("Other")));

            Assert.That(BindingPath.BuildFrom("Other").CompareTo(BindingPath.BuildFrom("Test")), Is.EqualTo(1));
            Assert.That(BindingPath.BuildFrom("Test").CompareTo(BindingPath.BuildFrom("Other")), Is.EqualTo(-1));
            Assert.That(BindingPath.BuildFrom("Test").CompareTo(BindingPath.BuildFrom("Test")), Is.EqualTo(0));

            Assert.That(BindingPath.BuildFrom("Test").GetHashCode(), Is.Not.EqualTo(0));

        }

        [Test]
        public void BuildingExceptionsTest()
        {
            Assert.Throws<ArgumentException>(() => BindingPath.BuildFrom("Test", string.Empty));
            Assert.Throws<ArgumentException>(() => BindingPath.BuildFrom(string.Empty, "Test"));
            Assert.Throws<ArgumentException>(() => BindingPath.BuildFrom(string.Empty));
            Assert.Throws<ArgumentException>(() => BindingPath.BuildFrom(BindingPath.Empty, string.Empty));
            Assert.Throws<ArgumentException>(() => BindingPath.BuildFrom(BindingPath.BuildFrom("Test"), string.Empty));

            Assert.Throws<ArgumentOutOfRangeException>(() => BindingPath.BuildFrom(GetStrings(BindingPath.MaxLength + 1)));
            var parent = BindingPath.BuildFrom(GetStrings(BindingPath.MaxLength - 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => BindingPath.BuildFrom(parent, GetStrings(2)));
        }

        [Test]
        public void BuildingTest()
        {
            Assert.That(BindingPath.Empty.Length, Is.EqualTo(0));
            for (var i = 1; i < BindingPath.MaxLength; i++)
            {
                var strings = GetStrings(i);
                var path = BindingPath.BuildFrom(strings);
                Assert.That(path.Length, Is.EqualTo(i));
                Assert.That(path.ToString(), Is.EqualTo(strings.Aggregate((a, b) => $"{a}.{b}")));
            }
        }

        [Test]
        public void BuildingFromParentTest()
        {
            var path = BindingPath.Empty;
            var expString = string.Empty;
            for (var i = 1; i < BindingPath.MaxLength; i++)
            {
                var str = i.ToString();
                path = BindingPath.BuildFrom(path, str);
                expString += i > 1 ? $".{str}" : str;
                Assert.That(path.Length, Is.EqualTo(i));
                Assert.That(path.ToString(), Is.EqualTo(expString));
            }
        }

        [Test]
        public void GetParentTest()
        {
            var strings = GetStrings(BindingPath.MaxLength);
            var path = BindingPath.BuildFrom(strings);
            var i = path.Length;
            while (--i > 0)
            {
                path = BindingPath.GetParent(path);
                Assert.That(path.Length, Is.EqualTo(i));
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