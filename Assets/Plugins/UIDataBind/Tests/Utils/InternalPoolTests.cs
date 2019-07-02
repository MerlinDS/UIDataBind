using System.Linq;
using NUnit.Framework;
using Plugins.UIDataBind.Utils;
using UnityEngine;

namespace Plugins.UIDataBind.Tests.Utils
{
    public class InternalPoolTests
    {
        private Transform _prefab;
        private Transform _container;
        private InternalPool _pool;

        [SetUp]
        public void SetUp()
        {
            _pool = new InternalPool();
            _container = new GameObject("Container").transform;
            _prefab = new GameObject("Prefab").transform;
            _prefab.gameObject.AddComponent<TestComponent>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_container.gameObject);
            Object.DestroyImmediate(_prefab.gameObject);
            _pool.Dispose();
        }

        [Test]
        public void SpawnTest()
        {
            var instance = _pool.Spawn(_prefab, _container);
            Assert.NotNull(instance);
            Assert.NotNull(instance.GetComponent<TestComponent>());
            Assert.AreSame(_container, instance.parent);
            Assert.IsTrue(instance.gameObject.activeInHierarchy);

            Assert.IsTrue(_pool.Spawned.Contains(instance));
        }

        [Test]
        public void SpawnWithTypeTest()
        {
            var instance = _pool.Spawn<TestComponent>(_prefab, _container);
            Assert.NotNull(instance);
            Assert.NotNull(instance.GetComponent<TestComponent>());
            Assert.AreSame(_container, instance.transform.parent);
            Assert.IsTrue(instance.gameObject.activeInHierarchy);

            Assert.IsTrue(_pool.Spawned.Contains(instance.transform));
        }

        [Test]
        public void DeSpawnTest()
        {
            var instance = _pool.Spawn(_prefab, _container);
            _pool.DeSpawn(instance);

            Assert.AreSame(_container, instance.parent);
            Assert.IsFalse(instance.gameObject.activeInHierarchy);

            Assert.IsFalse(_pool.Spawned.Contains(instance));

            Assert.That(()=>_pool.DeSpawn(_container), Throws.ArgumentException);
        }

        [Test]
        public void DeSpawnAllTest()
        {
            _pool.Spawn(_prefab, _container);
            _pool.Spawn(_prefab, _container);
            _pool.Spawn(_prefab, _container);

            var instance = _pool.Spawn(_prefab, _container);
            _pool.DeSpawnAll();

            Assert.AreSame(_container, instance.parent);
            Assert.IsFalse(instance.gameObject.activeInHierarchy);
            Assert.AreEqual(0, _pool.Spawned.Count());
        }

        private class TestComponent : MonoBehaviour
        {
        }
    }
}