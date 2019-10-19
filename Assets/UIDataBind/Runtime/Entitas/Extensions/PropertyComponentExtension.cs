using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Extensions
{
    public static class PropertyComponentExtension
    {
        public static IPropertyComponent<T> GetComponent<T>(this IEntity entity, int index) =>
            (IPropertyComponent<T>)entity.GetComponent(index);
    }
}