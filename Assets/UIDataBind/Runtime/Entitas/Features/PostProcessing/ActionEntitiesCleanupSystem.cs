using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace UIDataBind.Entitas.Features.PostProcessing
{
    internal sealed class ActionEntitiesCleanupSystem : IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private readonly List<UiBindEntity> _entities;

        private IGroup<UiBindEntity> _actionsGroup;

        public ActionEntitiesCleanupSystem(UiBindContext context)
        {
            _context = context;
            _entities = new List<UiBindEntity>();
        }

        public void Initialize() =>
            _actionsGroup = _context.GetGroup(UiBindMatcher.Action);

        public void Cleanup()
        {
            if(_actionsGroup.count == 0)
                return;

            _actionsGroup.GetEntities(_entities);
            foreach (var entity in _entities.Where(entity => !entity.isDirty))
                entity.Destroy();
            _entities.Clear();
        }
    }
}