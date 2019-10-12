using System.Collections.Generic;
using System.Linq;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class UpdateModelFromBindersSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly UiBindContext _context;
        private readonly List<UiBindEntity> _binderEntities;
        private IGroup<UiBindEntity> _bindersGroup;

        public UpdateModelFromBindersSystem(UiBindContext context)
        {
            _context = context;
            _binderEntities = new List<UiBindEntity>();
        }

        public void Initialize()
        {
            _bindersGroup = _context.GetGroup(UiBindMatcher.AllOf(UiBindMatcher.Binder, UiBindMatcher.Dirty));
        }

        public void Execute()
        {
            if(_bindersGroup.count == 0)
                return;

            _bindersGroup.GetEntities(_binderEntities);
            foreach (var binderEntity in _binderEntities.Where(e=>!e.hasAction))
                Execute(binderEntity);
            _binderEntities.Clear();
        }

        private void Execute(IUiBindEntity boundedEntity)
        {
            var manager = (EntitasEntityManager)_context.EntityManager;
            var propertyEntity = manager.GetPropertyEntity(boundedEntity);
            if(propertyEntity == null)
                return;

            var sourceType = manager.GetComponentDataType(boundedEntity);
            var componentData = manager.GetComponentData(boundedEntity, sourceType);

            if (manager.HasComponent(propertyEntity, sourceType))
            {
                var componentIndex = manager.GetPropertyComponentIndex(sourceType);
                ((IEntity) propertyEntity).ReplaceComponent(componentIndex, componentData as IComponent);
                ((UiBindEntity) propertyEntity).isDirty = true;
//                manager.SetComponentData(propertyEntity, );
            }
//            var targetType = manager.GetComponentDataType(propertyEntity);


        }
    }
}