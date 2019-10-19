using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Components;
using UIDataBind.Converters;
using UIDataBind.Entitas.Wrappers;

namespace UIDataBind.Entitas
{
    public class EntitasEngine : IECSEngine
    {
        private readonly UiBindContext _context;
        private readonly Type[] _propertyTypes;
        private readonly Type[] _componentTypes;
        private readonly int[] _propertyIndices;

        public EntitasEngine()
        {
            _context = Contexts.sharedInstance.uiBind;
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

            _propertyTypes = types.ToArray();
            _propertyIndices = indices.ToArray();
            _componentTypes = cTypes.ToArray();
            Converters = new Converters.Converters();
        }

        public IConverters Converters { get; }

        public Type[] PropertyTypes => _propertyTypes;
        public Type[] ComponentTypes => _componentTypes;

        public int GetPropertyIndex<TValue>()
        {
            var index = GetPropertyTypeIndex<TValue>();
            return index < 0 ? -1 : _propertyIndices[index];
        }

        public IEntityProvider CreateBinderEntity(IBinder binder)
        {
            var entity = _context.CreateEntity();
            entity.AddBindingPath(binder.Path);
            entity.AddBinder(binder);
            return new EntitasProvider(entity);
        }

        public void CreateModelEntity(BindingPath bindingPath)
        {
            if (_context.GetEntityWithModelPath(bindingPath) == null)
                CreateEntity(bindingPath).isModel = true;
        }

        public void CreateProperty(BindingPath propertyName)
        {
            var entity = _context.GetEntityWithModelPath(propertyName);
            if (entity != null)
                throw new ArgumentException($"{propertyName} property already created!", nameof(propertyName));

            CreateEntity(propertyName).isProperty = true;
        }

        public bool HasProperty<TValue>(BindingPath propertyName) =>
            _context.GetEntityWithModelPath(propertyName)?.HasComponent(GetPropertyIndex<TValue>()) ?? false;

        public void SetProperty<TValue>(BindingPath propertyName, TValue value)
        {
            var entity = GetModeEntity(propertyName);
            var typeIndex = GetPropertyTypeIndex<TValue>();
            if (typeIndex < 0)
                return;

            var index = _propertyIndices[typeIndex];
            var component = entity.HasComponent(index)
                ? (IPropertyComponent<TValue>) entity.GetComponent(index)
                : (IPropertyComponent<TValue>) entity.CreateComponent(index, _componentTypes[typeIndex]);

            component.Value = value;
            entity.ReplaceComponent(index, component as IComponent);
            ((UiBindEntity) entity).isDirty = true;
        }

        public TValue GetPropertyValue<TValue>(BindingPath propertyName)
        {
            var entity = GetModeEntity(propertyName);
            var index = GetPropertyTypeIndex<TValue>();
            return !entity.HasComponent(index)
                ? ((IPropertyComponent<TValue>) entity.GetComponent(index)).Value
                : default;
        }

        #region Helpers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetPropertyTypeIndex<TValue>() =>
            Array.IndexOf(_propertyTypes, typeof(TValue));

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


    }
}

public sealed partial class UiBindContext : IEngineProvider
{
}