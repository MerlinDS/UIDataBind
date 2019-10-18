using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Components;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Components;

namespace UIDataBind.Entitas.Features.Presentation
{
    public class SetDirectValueBinderSystem<T> : IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private readonly IMatcher<UiBindEntity> _matcher;
        private readonly List<UiBindEntity> _entities;
        private readonly int _index;

        private IGroup<UiBindEntity> _entitiesGroup;

        public SetDirectValueBinderSystem(UiBindContext context)
        {
            _context = context;
            _matcher = UiBindMatcher.AllOf(_index = context.GetEngine().GetPropertyIndex<T>());
            _entities = new List<UiBindEntity>();
        }

        public void Initialize() =>
            _entitiesGroup = _context.GetGroup(UiBindMatcher.AllOf(UiBindMatcher.Binder, UiBindMatcher.ConvertTo, _matcher));

        public void Cleanup()
        {
            if(_entitiesGroup.count == 0)
                return;

            _entitiesGroup.GetEntities(_entities);
            foreach (var entity in _entities)
            {
                if (!(entity.AsValueBinder() is IValueBinder<T> b))
                    continue;

                b.Value = ((IPropertyComponent<T>)entity.GetComponent(_index)).Value;
                Cleanup(entity);
            }
            _entities.Clear();
        }

        private void Cleanup(UiBindEntity entity)
        {
            entity.RemoveComponent(_index);
            entity.RemoveConvertTo();
        }
    }
}