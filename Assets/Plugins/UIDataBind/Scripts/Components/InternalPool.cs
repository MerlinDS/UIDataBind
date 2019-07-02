using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    public class InternalPool
    {
        public IEnumerable<Transform> Spawned => throw new NotImplementedException();

        public T Spawn<T>(Transform prefab, Transform container) where T : class
        {
            throw new NotImplementedException();
        }

        public Transform Spawn(Transform prefab, Transform container)
        {
            throw new NotImplementedException();
        }

        public void DeSpawn(Transform instance)
        {
            throw new NotImplementedException();
        }

        public void DeSpawnAll()
        {
            throw new NotImplementedException();
        }
    }
}