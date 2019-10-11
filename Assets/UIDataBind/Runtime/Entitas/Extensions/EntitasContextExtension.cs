using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitasContextExtension
    {
        private static readonly Type InterfaceType = typeof(IPropertyComponent);
        private static readonly string InterfaceName = typeof(IPropertyComponent<>).Name;
        private static readonly Dictionary<Type, int> TypeToIndexMap = new Dictionary<Type, int>();

        private static void UpdateTypeToIndexMap(this IContext context)
        {
            if(TypeToIndexMap.Count != 0)
                return;

            var componentTypes = context.contextInfo.componentTypes;
            for (var index = 0; index < componentTypes.Length; index++)
            {
                var componentType = componentTypes[index];
                if(!InterfaceType.IsAssignableFrom(componentType))
                    continue;

                var valueType = componentType.GetInterface(InterfaceName).GetGenericArguments().First();
                TypeToIndexMap.Add(valueType, index);
            }
        }

        public static void AddPropertyComponent<TValue>(this IContext context, IEntity entity, TValue defaultValue)
        {
            var componentIndex = context.GetPropertyComponentIndex<TValue>();
            var componentType = context.contextInfo.componentTypes[componentIndex];
            var component = (IPropertyComponent<TValue>) entity.CreateComponent(componentIndex, componentType);
            component.Value = defaultValue;
            entity.AddComponent(componentIndex, component as IComponent);
        }

        public static int GetPropertyComponentIndex<TValue>(this IContext context)
        {
            context.UpdateTypeToIndexMap();
            int componentIndex;
            if(!TypeToIndexMap.TryGetValue(typeof(TValue), out componentIndex))
                throw new ArgumentException($"Cannot add PropertyComponent<{typeof(TValue)}>. Such a component was not generated!");
            return componentIndex;
        }
    }
}