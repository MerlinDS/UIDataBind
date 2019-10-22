using System;
using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Extensions
{
    public static class PropertyComponentExtension
    {
        public static IPropertyComponent<T> GetComponent<T>(this IEntity entity, int index) =>
            (IPropertyComponent<T>)entity.GetComponent(index);

        public static IPropertyComponent<T> CreateComponent<T>(this IEntity entity, int index, Type componentType) =>
            (IPropertyComponent<T>) entity.CreateComponent(index, componentType);
    }
}