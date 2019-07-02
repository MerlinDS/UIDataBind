using NSubstitute;
using NUnit.Framework;
using Plugins.UIDataBind.Components;
using UnityEngine;

namespace Plugins.UIDataBind.Tests.Base
{
    [TestFixture]
    public class ViewContextBindingTest
    {
        [Test]
        public void ExternalContextTest()
        {
            var gameObject = new GameObject();
            var expectedContext = Substitute.For<IViewContext>();
            var contextBinding = gameObject.AddComponent<ViewContextBinding>();
            contextBinding.Context = expectedContext;

            Assert.That(contextBinding.Context, Is.SameAs(expectedContext));
        }

        private sealed class InternalContextBinding : ViewContextBinding
        {
            public override IViewContext Context { get; set; } = Substitute.For<IViewContext>();
        }

        [Test]
        public void InternalContextTest()
        {
            var gameObject = new GameObject();
            var contextBinding = gameObject.AddComponent<ViewContextBinding>();

            Assert.That(contextBinding.Context, Is.Not.Null);
        }

        private sealed class SelfContextBinding : ViewContextBinding, IViewContext
        {
        }

        [Test]
        public void SelfContextTest()
        {
            var gameObject = new GameObject();
            var contextBinding = gameObject.AddComponent<ViewContextBinding>();

            Assert.That(contextBinding.Context, Is.Not.Null);
        }





    }
}