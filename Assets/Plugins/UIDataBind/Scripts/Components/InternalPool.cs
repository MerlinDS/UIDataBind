using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    public class InternalPool : IDisposable
    {
        private readonly HashSet<Transform> _instances;
        private readonly List<Transform> _spawned;
        private readonly Queue<Transform> _despawned;

        public InternalPool()
        {
            _instances = new HashSet<Transform>();
            _spawned = new List<Transform>();
            _despawned = new Queue<Transform>();
        }

        #region API

        public IEnumerable<Transform> Spawned => _spawned;

        public T Spawn<T>(Transform prefab, Transform container) where T : class =>
            Spawn(prefab, container)?.GetComponent<T>();

        public Transform Spawn(Transform prefab, Transform container)
        {
            if (_despawned.Count == 0)
                Instantiate(prefab, container);

            var instance = _despawned.Dequeue();
            _spawned.Add(instance);
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void DeSpawn(Transform instance)
        {
            if (!_instances.Contains(instance))
                throw new AggregateException($"{instance} don't belongs to this pool!");

            if (_spawned.Contains(instance))
                _spawned.Remove(instance);

            DespawnInstance(instance);
        }

        public void DeSpawnAll()
        {
            foreach (var instance in _spawned)
                DespawnInstance(instance);
            _spawned.Clear();
        }

        #endregion

        /// <summary>
        /// Creating a new instance of item, then add it to the collection if despawned instances
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="container"></param>
        private void Instantiate(Transform prefab, Transform container)
        {
            var instance = UnityEngine.Object.Instantiate(prefab, container);
            instance.gameObject.SetActive(false);

            _instances.Add(instance);
            _despawned.Enqueue(instance);
        }

        private void DespawnInstance(Transform instance)
        {
            if (!_despawned.Contains(instance))
                _despawned.Enqueue(instance);

            instance.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _despawned.Clear();
            _instances.Clear();
            _spawned.Clear();
        }
    }
}