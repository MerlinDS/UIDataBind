using NUnit.Framework;
using Plugins.UIDataBind.Base;
using UnityEngine;

namespace Plugins.UIDataBind.Tests.Base
{
    [TestFixture]
    public class BaseBehaviourTest
    {
        [Test]
        public void ActivationAndDeactivationTest()
        {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<TestBehaviour>();
            Assert.That(behaviour.IsActivated, Is.True);
            gameObject.SetActive(false);
            Assert.That(behaviour.IsActivated, Is.False);
            gameObject.SetActive(true);
            Assert.That(behaviour.IsActivated, Is.True);
            Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void ReactivationTest()
        {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<TestBehaviour>();
            behaviour.Reactivate();
            Assert.That(behaviour.IsActivated, Is.True);
            Object.DestroyImmediate(gameObject);
        }

        private class TestBehaviour : BaseBehaviour
        {
            public bool IsActivated { get; private set; }
            protected override void Activate() => IsActivated = true;
            protected override void Deactivate() => IsActivated = false;
        }
    }
}