using System.Collections.Generic;
using Entitas;

namespace UIDataBind.Entitas.Features.PostProcessing
{
    internal sealed class CleanupEntitiesSystems : IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private readonly List<UiBindEntity> _entities;

        private IGroup<UiBindEntity> _cleanupGroup;

        public CleanupEntitiesSystems(UiBindContext context)
        {
            _context = context;
            _entities = new List<UiBindEntity>();
        }

        public void Initialize() =>
            _cleanupGroup = _context.GetGroup(UiBindMatcher.AnyOf(UiBindMatcher.Initialized, UiBindMatcher.Dirty));

        public void Cleanup()
        {
            if (_cleanupGroup.count == 0)
                return;

            var matchers = _cleanupGroup.matcher;
            _cleanupGroup.GetEntities(_entities);
            foreach (var entity in _entities)
                Cleanup(entity, matchers.indices);

            _entities.Clear();
        }

        private static void Cleanup(IEntity entity, IEnumerable<int> indices)
        {
            foreach (var index in indices)
            {
                if (entity.HasComponent(index))
                    entity.RemoveComponent(index);
            }
        }
    }
}