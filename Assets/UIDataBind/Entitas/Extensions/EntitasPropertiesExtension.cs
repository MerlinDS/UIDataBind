using System;
using System.Runtime.CompilerServices;
using UIDataBind.Base;
using UIDataBind.Runtime.Base.Extensions;

namespace UIDataBind.Entitas.Extensions
{
    [Obsolete]
    public static class EntitasPropertiesExtension
    {
        [Obsolete]
        public static void Refresh<TValue>(this ModelQuery properties,
            BindingPath propertyName, ref TValue value)
        {
            if (!properties.IsFiltered(propertyName))
                return;

            var engine = properties.GetEngine();
            propertyName = properties.BuildPath(propertyName);
            switch (properties.QueryType)
            {
                case QueryType.Update:
                {
                    if (engine.HasProperty<TValue>(propertyName))
                        value = engine.GetPropertyValue<TValue>(propertyName);
                    return;
                }
                case QueryType.Fetch:
                    if (!engine.HasProperty<TValue>(propertyName))
                    {
                        engine.CreateProperty(propertyName);
                        engine.SetProperty(propertyName, value);
                        return;
                    }

                    var propertyValue = engine.GetPropertyValue<TValue>(propertyName);
                    if (Equals(propertyValue, value))
                        return;

                    engine.SetProperty(propertyName, value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(properties.QueryType), properties.QueryType, null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static BindingPath BuildPath(this ModelQuery properties, BindingPath propertyName)
            => BindingPath.BuildFrom(properties.Path, propertyName);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsFiltered(this ModelQuery properties, BindingPath propertyName) =>
            properties.QueryType != QueryType.None &&
            (!(properties.Filter?.Length > 0) || Array.IndexOf(properties.Filter, propertyName) >= 0);
    }
}