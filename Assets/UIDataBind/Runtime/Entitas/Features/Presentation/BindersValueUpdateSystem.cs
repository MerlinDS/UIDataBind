using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class BindersValueUpdateSystem : IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private readonly IEntityManager _entityManager;
        private readonly List<UiBindEntity> _propertyEntities;

        private IGroup<UiBindEntity> _propertiesGroup;
        private readonly Func<IUiBindEntity, IUiBindEntity, bool>[] _updateActions;

        public BindersValueUpdateSystem(UiBindContext context)
        {
            _context = context;
            _propertyEntities = new List<UiBindEntity>();

            //TODO: Move to converters
            _updateActions = new Func<IUiBindEntity, IUiBindEntity, bool>[]
            {
                TryUpdateValue<bool>,
                TryUpdateValue<int>,
                TryUpdateValue<float>,
                TryUpdateValue<string>,
            };
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

        private void Execute(IUiBindEntity propertyEntity)
        {
            var boundedEntities = _context.EntityManager.GetBoundedEntities(propertyEntity);
            foreach (var boundedEntity in boundedEntities)
            {
                var needRefresh = _updateActions.Any(action => action.Invoke(propertyEntity, boundedEntity));
                if (!needRefresh)
                    continue;

                //TODO:
                (((UiBindEntity) boundedEntity).binder.Value as IValueBinder)?.Refresh();
            }
        }

        //TODO: Move to converters
        private bool TryUpdateValue<TValue>(IUiBindEntity source, IUiBindEntity target)
        {
            var manager = _context.EntityManager;
            if (!manager.HasComponent<TValue>(source))
                return false;

            var value = manager.GetComponentData<TValue>(source);
            if (manager.HasComponent<TValue>(target))
                manager.SetComponentData(target, value);
            else
            {
                var targetType = manager.GetComponentDataType(target);
                if (targetType == typeof(bool))
                    manager.SetComponentData(target, Convert.ToBoolean(value));
                else if (targetType == typeof(int))
                    manager.SetComponentData(target, Convert.ToInt32(value));
                else if (targetType == typeof(float))
                    manager.SetComponentData(target, Convert.ToSingle(value));
                else if (targetType == typeof(string))
                    manager.SetComponentData(target, Convert.ToString(value));
                else
                {
                    throw new NotImplementedException();
                }
            }

            return true;
        }
    }
}