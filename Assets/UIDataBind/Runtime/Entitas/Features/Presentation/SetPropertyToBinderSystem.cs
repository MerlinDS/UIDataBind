using System.Linq;
using Entitas;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Components;

namespace UIDataBind.Entitas.Features.Presentation
{
    public class SetPropertyToBinderSystem : IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private IGroup<UiBindEntity> _modelsGroup;

        public SetPropertyToBinderSystem(UiBindContext context)
        {
            _context = context;
        }

        public void Initialize() =>
            _modelsGroup = _context.GetGroup(UiBindMatcher.AllOf(UiBindMatcher.Property, UiBindMatcher.Dirty));

        public void Cleanup()
        {
            if(_modelsGroup.count == 0)
                return;

            var indices = _context.GetEngine().PropertyIndices;
            var modelEntities = _modelsGroup.GetEntities();
            foreach (var modelEntity in modelEntities)
            {
                var componentIndex = -1;
                IComponent component = default;
                foreach (var index in indices)
                {
                    if (!modelEntity.HasComponent(index))
                        continue;

                    componentIndex = index;
                    component = modelEntity.GetComponent(componentIndex);
                    break;
                }

                if(componentIndex < 0)
                    continue;

                var binderEntities =  _context.GetEntitiesWithBindingPath(modelEntity.modelPath.Value)
                    .Where(e => e.HasValueBinder());

                foreach (var binderEntity in binderEntities)
                {
                    var valueType = binderEntity.AsValueBinder().ValueType;
                    binderEntity.ReplaceComponent(componentIndex, component);
                    binderEntity.ReplaceConvertTo(valueType);
                }
            }
        }
    }
}