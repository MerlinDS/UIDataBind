using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Binders;
using UIDataBind.Binders.ValueBinders;

namespace UIDataBind.Entitas.Features.PostProcessing
{
    internal sealed class BindersValueUpdateSystem : IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private readonly List<UiBindEntity> _propertyEntities;

        private IGroup<UiBindEntity> _propertiesGroup;

        public BindersValueUpdateSystem(UiBindContext context)
        {
            _context = context;
            _propertyEntities = new List<UiBindEntity>();
        }

        public void Initialize() =>
            _propertiesGroup = _context.GetGroup(UiBindMatcher.AllOf(UiBindMatcher.Path, UiBindMatcher.Dirty));

        public void Cleanup()
        {
            if (_propertiesGroup.count == 0)
                return;

            _propertiesGroup.GetEntities(_propertyEntities);
            foreach (var propertyEntity in _propertyEntities)
                Execute(propertyEntity);
            _propertyEntities.Clear();
        }

        private void Execute(UiBindEntity propertyEntity)
        {
            var bindersEntities = _context.GetEntitiesWithBindingPath(propertyEntity.path.Value);
            foreach (var binderEntity in bindersEntities)
            {
                if (TryUpdateBooleanValue(propertyEntity, binderEntity))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    (binderEntity.binder.Value as IValueBinder).Refresh();
                    continue;
                }
            }
        }

        private static bool TryUpdateBooleanValue(UiBindEntity source, UiBindEntity target)
        {
            if(!source.hasBooleanProperty)
                return false;

            var value = source.booleanProperty.Value;
            if(target.hasBooleanProperty)
                target.ReplaceBooleanProperty(value);
            else
            {
                if(target.hasIntegerProperty)
                    target.ReplaceIntegerProperty(value?1:0);
            }

            return true;
        }
    }
}