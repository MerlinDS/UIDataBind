using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Components;
using UIDataBind.Entitas.Components;
using UnityEngine;

namespace UIDataBind.Entitas.Features.Presentation
{
    public abstract class ValueUpdateSystem<T> : ReactiveSystem<UiBindEntity>, IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private readonly IMatcher<UiBindEntity> _matcher;
        private readonly List<UiBindEntity> _modelEntities;
        private readonly int _index;

        private IGroup<UiBindEntity> _modelsGroup;

        protected ValueUpdateSystem(UiBindContext context) : base(context)
        {
            _context = context;
            _modelEntities = new List<UiBindEntity>();

            var interfaceName = typeof(IPropertyComponent<>).Name;
            var componentTypes = context.contextInfo.componentTypes;
            for (var index = 0; index < componentTypes.Length; index++)
            {
                var componentType = componentTypes[index];
                if (!typeof(IPropertyComponent).IsAssignableFrom(componentType))
                    continue;

                var type = componentType.GetInterface(interfaceName).GetGenericArguments().First();
                if (type != typeof(T))
                    continue;

                _matcher = UiBindMatcher.AllOf(_index = index);
                break;
            }
        }

        public void Initialize() =>
            _modelsGroup = _context.GetGroup(UiBindMatcher.AllOf(UiBindMatcher.Property,
                                                                 UiBindMatcher.Dirty, _matcher));

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.AllOf(UiBindMatcher.Binder, UiBindMatcher.BindingPath,
                                                        UiBindMatcher.Dirty));

        protected override bool Filter(UiBindEntity entity)
        {
            if (!entity.HasValueBinder())
                return false;

            var propertyEntity = _context.GetEntityWithModelPath(entity.bindingPath.Value);
            return _matcher.Matches(propertyEntity);
        }

        protected override void Execute(List<UiBindEntity> binderEntities)
        {
            foreach (var binderEntity in binderEntities)
            {
                var propertyEntity = _context.GetEntityWithModelPath(binderEntity.bindingPath.Value);
                var component = (IPropertyComponent<T>) propertyEntity.GetComponent(_index);
                var binder = binderEntity.AsValueBinder();
                if (binder is IValueBinder<T> b)
                    component.Value = b.Value;
                else
                {
                    T value;
                    if (!TryConvertTargetToSource(binder.Value, out value))
                        Debug.LogWarning($"Can't convert {binder.Value} to {typeof(T).Name}");
                    else
                        component.Value = value;
                }
                propertyEntity.ReplaceComponent(_index, component as IComponent);
            }
        }

        public void Cleanup()
        {
            if (_modelsGroup.count == 0)
                return;

            _modelsGroup.GetEntities(_modelEntities);
            foreach (var entity in _modelEntities)
            {
                var component = (IPropertyComponent<T>) entity.GetComponent(_index);
                foreach (var binder in GetBinders(entity.modelPath.Value))
                {
                    if (binder is IValueBinder<T> b)
                    {
                        b.Value = component.Value;
                        continue;
                    }

                    object value;
                    if (!TryConvertSourceToTarget(component.Value, binder.ValueType, out value))
                        Debug.LogWarning($"Can't convert {component.Value} to {binder.ValueType.Name}");
                    else
                        binder.Value = value;
                }
            }

            _modelEntities.Clear();
        }

        private IEnumerable<IValueBinder> GetBinders(BindingPath path) =>
            _context.GetEntitiesWithBindingPath(path)
                .Where(e => e.HasValueBinder())
                .Select(e => e.AsValueBinder());

        protected abstract bool TryConvertSourceToTarget(T sourceValue, Type targetType, out object result);

        protected abstract bool TryConvertTargetToSource(object targetValue, out T result);


    }
}