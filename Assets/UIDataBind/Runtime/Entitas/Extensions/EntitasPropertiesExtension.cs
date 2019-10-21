using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Wrappers;
using UIDataBind.Utils.Extensions;

namespace UIDataBind.Entitas.Extensions
{
    public enum RefreshType
    {
        Update,
        Fetch
    }

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

        [UsedImplicitly]
        public static void InitModel(this IEngineProvider engineProvider, BindingPath modelPath, IViewModel model) =>
            engineProvider.GetProperties(modelPath).Fetch(ModelsCache.Replace(modelPath, model));

        [UsedImplicitly]
        public static void InitModel(this IProperties properties, IViewModel model) =>
            properties.Fetch(ModelsCache.Replace(properties.ModelPath, model));

        public static TViewModel GetModel<TViewModel>(this IProperties properties)
            where TViewModel : struct, IViewModel
        {
            if (!ModelsCache.ContainsKey(properties.ModelPath))
                ModelsCache.Add(properties.ModelPath, Activator.CreateInstance<TViewModel>());

            var model = ModelsCache[properties.ModelPath];
            if (!(model is TViewModel))
                model = ModelsCache.Replace(properties.ModelPath, Activator.CreateInstance<TViewModel>());

            model.Refresh(RefreshType.Update, properties);
            return (TViewModel) model;
        }

        public static void Fetch(this IProperties properties, IViewModel model)=>
            model.Refresh(RefreshType.Fetch, properties);

        public static void RefreshProperty<TValue>(this IProperties properties, RefreshType actionType,
            BindingPath propertyName, ref TValue value)
        {
            var engine = properties.GetEngine();
            propertyName = properties.BuildPath(propertyName);
            switch (actionType)
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
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static BindingPath BuildPath(this IProperties properties, BindingPath propertyName)
            => properties.ModelPath.BuildPath(propertyName);
    }
}