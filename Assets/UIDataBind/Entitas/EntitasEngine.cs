using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Entitas;
using JetBrains.Annotations;
using UIDataBind.Base;
using UIDataBind.Base.Components;
using UIDataBind.Converters;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Utils.Extensions;

namespace UIDataBind.Entitas
{
    [UsedImplicitly]
    internal sealed class EntitasEngine : IECSEngine
    {
        private readonly UiBindContext _context;
        private readonly EntitasProperties _properties;

        private readonly int[] _propertyIndices;

        public Type[] PropertyTypes { get; }

        public Type[] ComponentTypes { get; }
        public IConverters Converters { get; }

        public EntitasEngine()
        {
            _context = Contexts.sharedInstance.uiBind;
            _properties = new EntitasProperties(this);
            Converters = new Converters.TypeConverters();

            var types = new List<Type>();
            var cTypes = new List<Type>();
            var indices = new List<int>();

            var interfaceName = typeof(IPropertyComponent<>).Name;
            var componentTypes = _context.contextInfo.componentTypes;
            for (var index = 0; index < componentTypes.Length; index++)
            {
                var componentType = componentTypes[index];
                if (!typeof(IPropertyComponent).IsAssignableFrom(componentType))
                    continue;

                cTypes.Add(componentType);
                types.Add(componentType.GetInterface(interfaceName).GetGenericArguments().First());
                indices.Add(index);
            }

            _propertyIndices = indices.ToArray();
            PropertyTypes = types.ToArray();
            ComponentTypes = cTypes.ToArray();
        }

        public IEntityProvider CreateBinderEntity(IBinder binder)
        {
            var entity = _context.CreateEntity();
            entity.AddBinderPath(binder.Path);
            entity.AddBinder(binder);
            return new EntitasProvider(entity);
        }

        #region Properties

        public int GetPropertyIndex<TValue>()
        {
            var index = GetPropertyTypeIndex<TValue>();
            return index < 0 ? -1 : _propertyIndices[index];
        }

        public void CreateProperty(BindingPath propertyPath)
        {
            var entity = _context.GetEntityWithModelPath(propertyPath);
            if (entity != null)
                throw new ArgumentException($"{propertyPath} property already created!", nameof(propertyPath));

            CreateEntity(propertyPath).isProperty = true;
        }

        public bool HasProperty<TValue>(BindingPath propertyName) =>
            _context.GetEntityWithModelPath(propertyName)?.HasComponent(GetPropertyIndex<TValue>()) ?? false;

        public void SetProperty<TValue>(BindingPath propertyPath, TValue value)
        {
            var entity = GetModeEntity(propertyPath);
            var typeIndex = GetPropertyTypeIndex<TValue>();
            if (typeIndex < 0)
                return;

            var index = _propertyIndices[typeIndex];
            var component = !entity.HasComponent(index)
                ? entity.CreateComponent<TValue>(index, ComponentTypes[typeIndex])
                : entity.GetComponent<TValue>(index);

            component.Value = value;
            entity.ReplaceComponent(index, component as IComponent);
            ((UiBindEntity) entity).isDirty = true;
        }

        public TValue GetPropertyValue<TValue>(BindingPath propertyPath)
        {
            var entity = GetModeEntity(propertyPath);
            var index = GetPropertyTypeIndex<TValue>();
            index = _propertyIndices[index];
            return entity.HasComponent(index) ? entity.GetComponent<TValue>(index).Value : default;
        }

        #endregion

        #region Helpers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetPropertyTypeIndex<TValue>() =>
            Array.IndexOf(PropertyTypes, typeof(TValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private UiBindEntity CreateEntity(BindingPath path)
        {
            var entity = _context.CreateEntity();
            entity.AddModelPath(path);
            return entity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IEntity GetModeEntity(BindingPath path)
        {
            var entity = _context.GetEntityWithModelPath(path);
            if (entity == null)
                throw new ArgumentException($"{path} property was not created!", nameof(path));
            return entity;
        }

        #endregion

        #region Models Provider

        private static readonly Dictionary<BindingPath, IViewModel> ModelsCache =
            new Dictionary<BindingPath, IViewModel>();

        public TViewModel Init<TViewModel>(TViewModel model, ModelQuery query) where TViewModel : struct, IViewModel
        {
            if (_context.GetEntityWithModelPath(query.Path) == null)
                CreateEntity(query.Path).isModel = true;
            ModelsCache.Replace(query.Path, model);
            return Apply(model, query);
        }

        public TViewModel Get<TViewModel>(ModelQuery query) where TViewModel : struct, IViewModel
        {
            var path = query.Path;
            if (!ModelsCache.ContainsKey(path))
                ModelsCache.Add(path, Activator.CreateInstance<TViewModel>());

            var model = ModelsCache[path];
            if (!(model is TViewModel))
                model = ModelsCache.Replace(path, Activator.CreateInstance<TViewModel>());

            return Apply((TViewModel)model, query);
        }

        public TViewModel Apply<TViewModel>(TViewModel model, ModelQuery query)
            where TViewModel : struct, IViewModel
        {
            _properties.SetQuery(query);
            model.Refresh(_properties);
            return model;
        }

        #endregion
    }
}

