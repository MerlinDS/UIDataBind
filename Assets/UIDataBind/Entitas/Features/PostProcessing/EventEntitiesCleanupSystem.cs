using System.Collections.Generic;
using System.Linq;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Components;

namespace UIDataBind.Entitas.Features.PostProcessing
{
    internal sealed class EventEntitiesCleanupSystem : IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private readonly List<UiBindEntity> _entities;
        private readonly IECSEngine _engine;

        private IGroup<UiBindEntity> _actionsGroup;

        public EventEntitiesCleanupSystem(UiBindContext context, IECSEngine engine)
        {
            _context = context;
            _engine = engine;
            _entities = new List<UiBindEntity>();
        }

        public void Initialize() =>
            _actionsGroup = _context.GetGroup(UiBindMatcher.Event);

        public void Cleanup()
        {
            if (_actionsGroup.count == 0)
                return;

            _actionsGroup.GetEntities(_entities);
            foreach (var entity in _entities.Where(entity => !entity.isDirty))
            {
                if(entity.@event.Value != ControlEvent.Changed)
                    Invalidate(entity);
                entity.RemoveEvent();
            }
            _entities.Clear();
        }

        private void Invalidate(UiBindEntity entity)
        {
            if (entity.AsValueBinder() is IValueBinder<ControlEvent> binder)
                binder.Value = ControlEvent.None;

            var path = entity.binderPath.Value;
            if (_engine.HasProperty<ControlEvent>(path))
                _engine.SetProperty(path, ControlEvent.None);
        }
    }
}