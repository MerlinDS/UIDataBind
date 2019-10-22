using System.Collections.Generic;
using Entitas;

namespace UIDataBind.Entitas.Features.PostProcessing
{
    internal sealed class CleanupDirtyEntitiesSystems : IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private readonly List<UiBindEntity> _entities;

        private IGroup<UiBindEntity> _dirtyGroup;

        public CleanupDirtyEntitiesSystems(UiBindContext context)
        {
            _context = context;
            _entities = new List<UiBindEntity>();
        }

        public void Initialize() =>
            _dirtyGroup = _context.GetGroup(UiBindMatcher.Dirty);

        public void Cleanup()
        {
            if(_dirtyGroup.count == 0)
                return;

            _dirtyGroup.GetEntities(_entities);
            foreach (var entity in _entities)
                entity.isDirty = false;
            _entities.Clear();
        }
    }
}