using System.Runtime.CompilerServices;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Wrappers;
using UIDataBind.Utils.Extensions;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitasPropertiesExtension
    {
        // ReSharper disable once UnusedParameter.Global
        public static IProperties GetProperties(this IECSEngine engine, BindingPath modelPath)
        {
            var properties = new EntitasProperties(modelPath);
            engine.CreateModelEntity(modelPath);
            return properties;
        }

        public static void UpdateModel<TViewModel>(this IProperties properties, ref TViewModel model)
            where TViewModel : IViewModel => model.Update(properties);

        public static void Fetch<TViewModel>(this IProperties properties, TViewModel model)
            where TViewModel : IViewModel => model.Fetch(properties);

        public static void ReadProperty<TValue>(this IProperties properties, BindingPath propertyName, ref TValue value)
        {
            propertyName = properties.BuildPath(propertyName);
            var engine = properties.GetEngine();

            if (engine.HasProperty<TValue>(propertyName))
                value = engine.GetPropertyValue<TValue>(propertyName);
        }

        public static void WriteProperty<TValue>(this IProperties properties, BindingPath propertyName, TValue value)
        {
            propertyName = properties.BuildPath(propertyName);
            var engine = properties.GetEngine();

            if (!engine.HasProperty<TValue>(propertyName))
            {
                engine.CreateProperty(propertyName);
                engine.SetProperty(propertyName, value);
                return;
            }
            var propertyValue = engine.GetPropertyValue<TValue>(propertyName);
            if(Equals(propertyValue, value))
                return;

            engine.SetProperty(propertyName, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static BindingPath BuildPath(this IProperties properties, BindingPath propertyName)
            => properties.ModelPath.BuildPath(propertyName);
    }
}