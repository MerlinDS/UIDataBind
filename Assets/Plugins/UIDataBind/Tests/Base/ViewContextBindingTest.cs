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
            gameObject.SetActive(false);

            var expectedContext = Substitute.For<IViewContext>();
            var contextBinding = gameObject.AddComponent<ViewContextBinding>();
            contextBinding.Context = expectedContext;

            gameObject.SetActive(true);
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
            var contextBinding = gameObject.AddComponent<InternalContextBinding>();

            Assert.That(contextBinding.Context, Is.Not.Null);
        }

        private sealed class SelfContextBinding : ViewContextBinding, IViewContext
        {
        }

        [Test]
        public void SelfContextTest()
        {
            var gameObject = new GameObject();
            var contextBinding = gameObject.AddComponent<SelfContextBinding>();

            Assert.That(contextBinding.Context, Is.Not.Null);
        }


        [Test]
        public void EmptyContextTest()
        {
            var gameObject = new GameObject();
            gameObject.SetActive(false);
            var binding = gameObject.AddComponent<ViewContextBinding>();
            /*
             * Because of internal Unity assertion the test assertion fail on legal exception.
             * That's why we will reactivate the component directly
             */
            Assert.That(()=>binding.Reactivate(), Throws.InvalidOperationException);
        }

    }
}