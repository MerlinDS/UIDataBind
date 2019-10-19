using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Converters;
using UIDataBind.Entitas.Components;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class PropertyUpdateSystem<T> : ReactiveSystem<UiBindEntity>, IInitializeSystem, ICleanupSystem
    {
        private readonly int _index;
        private readonly Type _targetType;

        private readonly UiBindContext _context;
        private readonly IConverters _converters;

        private readonly IMatcher<UiBindEntity> _matcher;
        private readonly List<UiBindEntity> _entities;

        private IGroup<UiBindEntity> _propertiesGroup;

        public PropertyUpdateSystem(UiBindContext context) : base(context)
        {
            _context = context;
            _converters = context.GetEngine().Converters;
            _targetType = typeof(T);

            _matcher = UiBindMatcher.AllOf(_index = context.GetEngine().GetPropertyIndex<T>());
            _entities = new List<UiBindEntity>();
        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.AllOf(UiBindMatcher.Binder, UiBindMatcher.BindingPath,
                                                        UiBindMatcher.Dirty));

        public void Initialize() =>
            _propertiesGroup = _context.GetGroup(UiBindMatcher.AllOf(UiBindMatcher.Property,
                                                                     UiBindMatcher.Dirty, _matcher));

        protected override bool Filter(UiBindEntity binderEntity) =>
            _context.GetEngine().HasProperty<T>(binderEntity.bindingPath.Value) && CanBeConverted(binderEntity);

        #region From binder to property

        protected override void Execute(List<UiBindEntity> entities)
        {
            foreach (var binderEntity in entities)
            {
                UpdateProperty(binderEntity);
                binderEntity.isDirty = false;
            }
        }

        private void UpdateProperty(UiBindEntity binderEntity)
        {
            var propertyEntity = _context.GetEntityWithModelPath(binderEntity.bindingPath.Value);
            var component = propertyEntity.GetComponent<T>(_index);
            var binder = binderEntity.AsValueBinder();
            component.Value = binder is IValueBinder<T> b ? b.Value : _converters.Convert<T>(binder.Value);
            propertyEntity.ReplaceComponent(_index, (IComponent)component);
            propertyEntity.isDirty = true; //Update other binders
        }

        #endregion

        #region From binder to property

        public void Cleanup()
        {
            if(_propertiesGroup.count == 0)
                return;

            _propertiesGroup.GetEntities(_entities);
            foreach (var propertyEntity in _entities)
            {
                var value = propertyEntity.GetComponent<T>(_index).Value;
                var binderEntities = _context.GetEntitiesWithBindingPath(propertyEntity.modelPath.Value);
                foreach (var binderEntity in binderEntities)
                    UpdateBinder(binderEntity.AsValueBinder(), value);

                propertyEntity.isDirty = false;
            }
            _entities.Clear();
        }

        private void UpdateBinder(IValueBinder binder, T value)
        {
            if (binder is IValueBinder<T> b)
                b.Value = value;
            else if(_converters.Has(_targetType, binder.ValueType))
                binder.Value = _converters.Convert(binder.ValueType, value);
        }

        #endregion

        #region Helpers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CanBeConverted(UiBindEntity binderEntity)
        {
            var sourceType = binderEntity.AsValueBinder().ValueType;
            return sourceType == _targetType || _converters.Has(sourceType, _targetType);
        }

        public override string ToString() => $"{GetType().Name}";

        #endregion
    }
}