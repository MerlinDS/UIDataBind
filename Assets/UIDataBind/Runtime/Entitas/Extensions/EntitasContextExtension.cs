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

        public static void ReplacePropertyComponent<TValue>(this IContext context, IEntity entity, TValue defaultValue)
        {
            var componentIndex = context.GetPropertyComponentIndex<TValue>();
            var componentType = context.contextInfo.componentTypes[componentIndex];
            var component = (IPropertyComponent<TValue>) entity.CreateComponent(componentIndex, componentType);
            component.Value = defaultValue;
            entity.ReplaceComponent(componentIndex, component as IComponent);
        }

        public static bool HasPropertyComponent<TValue>(this IContext context, IEntity entity)
        {
            var componentIndex = context.GetPropertyComponentIndex<TValue>();
            return entity.HasComponent(componentIndex);
        }

        public static TValue GetPropertyComponent<TValue>(this IContext context, IEntity entity)
        {
            var componentIndex = context.GetPropertyComponentIndex<TValue>();
            if (entity.HasComponent(componentIndex))
                return default;

            var componentType = context.contextInfo.componentTypes[componentIndex];
            var component = (IPropertyComponent<TValue>) entity.CreateComponent(componentIndex, componentType);
            return component.Value;
        }

        public static int GetPropertyComponentIndex<TValue>(this IContext context) =>
            context.GetPropertyComponentIndex(typeof(TValue));

        public static int GetPropertyComponentIndex(this IContext context, Type propertyType)
        {
            context.UpdateTypeToIndexMap();
            int componentIndex;
            if(!TypeToIndexMap.TryGetValue(propertyType, out componentIndex))
                throw new ArgumentException($"Cannot add PropertyComponent<{propertyType}>. Such a component was not generated!");
            return componentIndex;
        }
    }
}