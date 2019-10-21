using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Wrappers;
using UIDataBind.Utils.Extensions;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitasPropertiesExtension
    {
        private static readonly Dictionary<BindingPath, IProperties> PropertiesCache = new Dictionary<BindingPath, IProperties>();
        private static readonly Dictionary<BindingPath, IViewModel> ModelsCache = new Dictionary<BindingPath, IViewModel>();

        public static IProperties GetProperties(this IEngineProvider engineProvider, BindingPath modelPath)
        {
            if (PropertiesCache.ContainsKey(modelPath))
                return PropertiesCache[modelPath];

            engineProvider.GetEngine().CreateModelEntity(modelPath);
            PropertiesCache.Add(modelPath, new EntitasProperties(modelPath));
            return PropertiesCache[modelPath];
        }

        public static TViewModel GetModel<TViewModel>(this IProperties properties)
            where TViewModel : struct, IViewModel
        {
            if (!ModelsCache.ContainsKey(properties.ModelPath))
                ModelsCache.Add(properties.ModelPath, Activator.CreateInstance<TViewModel>());

            var model = ModelsCache[properties.ModelPath];
            if (!(model is TViewModel))
            {
                model = Activator.CreateInstance<TViewModel>();
                ModelsCache[properties.ModelPath] = model;
            }

            model.Update(properties);
            return (TViewModel) model;
        }

        public static void ReadProperty<TValue>(this IProperties properties, BindingPath propertyName, ref TValue value)
        {
            var engine = properties.GetEngine();
            propertyName = properties.BuildPath(propertyName);

            if (engine.HasProperty<TValue>(propertyName))
                value = engine.GetPropertyValue<TValue>(propertyName);
        }

        public static void WriteProperty<TValue>(this IProperties properties, BindingPath propertyName, TValue value)
        {
            var engine = properties.GetEngine();
            propertyName = properties.BuildPath(propertyName);

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