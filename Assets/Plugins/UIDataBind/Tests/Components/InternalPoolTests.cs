using System.Linq;
using NUnit.Framework;
using Plugins.UIDataBind.Components;
using UnityEngine;

namespace Plugins.UIDataBind.Tests.Components
{
    public class InternalPoolTests
    {
        private Transform _prefab;
        private Transform _container;

        [SetUp]
        public void SetUp()
        {
            _container = new GameObject("Container").transform;
            _prefab = new GameObject("Prefab").transform;
            _prefab.gameObject.AddComponent<TestComponent>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_container.gameObject);
            Object.DestroyImmediate(_prefab.gameObject);
        }

        [Test]
        public void SpawnTest()
        {
            var pool = new InternalPool();
            var instance = pool.Spawn(_prefab, _container);
            Assert.NotNull(instance);
            Assert.NotNull(instance.GetComponent<TestComponent>());
            Assert.AreSame(_container, instance.parent);
            Assert.IsTrue(instance.gameObject.activeInHierarchy);

            Assert.IsTrue(pool.Spawned.Contains(instance));
        }

        [Test]
        public void SpawnWithTypeTest()
        {
            var pool = new InternalPool();
            var instance = pool.Spawn<TestComponent>(_prefab, _container);
            Assert.NotNull(instance);
            Assert.NotNull(instance.GetComponent<TestComponent>());
            Assert.AreSame(_container, instance.transform.parent);
            Assert.IsTrue(instance.gameObject.activeInHierarchy);

            Assert.IsTrue(pool.Spawned.Contains(instance.transform));
        }

        [Test]
        public void DeSpawnTest()
        {
            var pool = new InternalPool();
            var instance = pool.Spawn(_prefab, _container);
            pool.DeSpawn(instance);

            Assert.AreSame(_container, instance.parent);
            Assert.IsFalse(instance.gameObject.activeInHierarchy);

            Assert.IsFalse(pool.Spawned.Contains(instance));
        }

        [Test]
        public void DeSpawnAllTest()
        {
            var pool = new InternalPool();
            pool.Spawn(_prefab, _container);
            pool.Spawn(_prefab, _container);
            pool.Spawn(_prefab, _container);

            var instance = pool.Spawn(_prefab, _container);
            pool.DeSpawnAll();

            Assert.AreSame(_container, instance.parent);
            Assert.IsFalse(instance.gameObject.activeInHierarchy);
            Assert.AreEqual(0, pool.Spawned.Count());
        }

        private class TestComponent : MonoBehaviour
        {

        }
    }
}