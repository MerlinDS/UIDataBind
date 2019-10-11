using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace UIDataBind.Examples.Utils.Entitas
{
    public class SystemsModule : List<Systems>
    {
        private readonly List<Type> _bindings;

        public SystemsModule()
        {
            _bindings = new List<Type>();
        }

        public SystemsModule Bind<TSystems>()
            where TSystems : Systems
        {
            var type = typeof(TSystems);
            if (_bindings.Contains(type))
                throw new ArgumentException($"{type.Name} already bounded");

            _bindings.Add(type);
            return this;
        }

        public void InstantiateAll(IContexts contexts)
        {
            Clear();
            foreach (var binding in _bindings)
            {
                try
                {
                    var instance = (Systems) Activator.CreateInstance(binding, contexts);
                    Add(instance);
                }
                catch (ArgumentException e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }
    }
}