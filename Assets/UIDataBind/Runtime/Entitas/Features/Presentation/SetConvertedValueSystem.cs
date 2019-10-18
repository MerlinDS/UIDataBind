using System.Collections.Generic;
using Entitas;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Components;

namespace UIDataBind.Entitas.Features.Presentation
{
    public class SetConvertedValueSystem : IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private readonly List<UiBindEntity> _entities;
        private readonly int[] _indices;

        private IGroup<UiBindEntity> _entitiesGroup;

        public SetConvertedValueSystem(UiBindContext context)
        {
            _context = context;
            _indices = _context.GetEngine().PropertyIndices;
            _entities = new List<UiBindEntity>();
        }

        public void Initialize() =>
            _entitiesGroup = _context.GetGroup(UiBindMatcher.AllOf(UiBindMatcher.Binder, UiBindMatcher.Converted));

        public void Cleanup()
        {
            if (_entitiesGroup.count == 0)
                return;

            _entitiesGroup.GetEntities(_entities);
            foreach (var entity in _entities)
            {
                entity.AsValueBinder().Value = entity.converted.Value;
                Cleanup(entity);
            }
            _entities.Clear();
        }

        private void Cleanup(UiBindEntity entity)
        {
            foreach (var index in _indices)
            {
                if (!entity.HasComponent(index))
                    continue;

                entity.RemoveComponent(index);
                break;
            }

            entity.RemoveConverted();
        }
    }
}