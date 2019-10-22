using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Runtime.Base.Extensions;
using UIDataBind.Utils.Extensions;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitasPropertiesExtension
    {
        private static readonly Dictionary<BindingPath, IProperties> PropertiesCache =
            new Dictionary<BindingPath, IProperties>();

        private static readonly Dictionary<BindingPath, IViewModel> ModelsCache =
            new Dictionary<BindingPath, IViewModel>();

        public static IProperties GetProperties(this IEngineProvider engineProvider, BindingPath modelPath)
        {
            if (PropertiesCache.ContainsKey(modelPath))
                return PropertiesCache[modelPath];

            engineProvider.GetEngine().CreateModelEntity(modelPath);
            PropertiesCache.Add(modelPath, new EntitasProperties(modelPath));
            return PropertiesCache[modelPath];
        }

        [UsedImplicitly, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitModel(this IEngineProvider engineProvider, BindingPath modelPath, IViewModel model) =>
            engineProvider.GetProperties(modelPath).Fetch(ModelsCache.Replace(modelPath, model));

        [UsedImplicitly, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitModel(this IProperties properties, IViewModel model) =>
            properties.Fetch(ModelsCache.Replace(properties.ModelPath, model));

        [UsedImplicitly]
        public static TViewModel GetModel<TViewModel>(this IProperties properties, params BindingPath[] filter)
            where TViewModel : struct, IViewModel
        {
            if (!ModelsCache.ContainsKey(properties.ModelPath))
                ModelsCache.Add(properties.ModelPath, Activator.CreateInstance<TViewModel>());

            var model = ModelsCache[properties.ModelPath];
            if (!(model is TViewModel))
                model = ModelsCache.Replace(properties.ModelPath, Activator.CreateInstance<TViewModel>());

            return (TViewModel) properties.Update(model, filter);
        }

        [UsedImplicitly]
        public static IViewModel Update(this IProperties properties, IViewModel model, params BindingPath[] filter)
        {
            properties.Filter = filter;
            properties.RefreshType = RefreshType.Update;
            model.Refresh(properties);
            properties.RefreshType = RefreshType.None;
            properties.Filter = default;
            return model;
        }

        [UsedImplicitly]
        public static void Fetch(this IProperties properties, IViewModel model, params BindingPath[] filter)
        {
            properties.Filter = filter;
            properties.RefreshType = RefreshType.Fetch;
            model.Refresh(properties);
            properties.RefreshType = RefreshType.None;
            properties.Filter = default;
        }

        public static void RefreshProperty<TValue>(this IProperties properties,
            BindingPath propertyName, ref TValue value)
        {
            if (!properties.IsFiltered(propertyName))
                return;

            var engine = properties.GetEngine();
            propertyName = properties.BuildPath(propertyName);
            switch (properties.RefreshType)
            {
                case RefreshType.Update:
                {
                    if (engine.HasProperty<TValue>(propertyName))
                        value = engine.GetPropertyValue<TValue>(propertyName);
                    return;
                }
                case RefreshType.Fetch:
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
                    throw new ArgumentOutOfRangeException(nameof(properties.RefreshType), properties.RefreshType, null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static BindingPath BuildPath(this IProperties properties, BindingPath propertyName)
            => properties.ModelPath.BuildPath(propertyName);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsFiltered(this IProperties properties, BindingPath propertyName) =>
            properties.RefreshType != RefreshType.None &&
            (!(properties.Filter?.Length > 0) || Array.IndexOf(properties.Filter, propertyName) >= 0);
    }
}