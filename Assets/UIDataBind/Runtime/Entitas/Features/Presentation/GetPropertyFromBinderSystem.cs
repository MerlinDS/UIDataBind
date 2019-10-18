using System.Collections.Generic;
using Entitas;
using UIDataBind.Entitas.Components;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class GetPropertyFromBinderSystem : ReactiveSystem<UiBindEntity>
    {
        private readonly UiBindContext _context;

        public GetPropertyFromBinderSystem(UiBindContext context) : base(context)
        {
            _context = context;
        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.AllOf(UiBindMatcher.Binder, UiBindMatcher.BindingPath,
                                                        UiBindMatcher.Dirty));

        protected override bool Filter(UiBindEntity entity) =>
            entity.HasValueBinder() && _context.GetEntityWithModelPath(entity.bindingPath.Value) != null;

        protected override void Execute(List<UiBindEntity> entities)
        {
            foreach (var binderEntity in entities)
            {
                var propertyEntity = _context.GetEntityWithModelPath(binderEntity.bindingPath.Value);
                var binder = binderEntity.AsValueBinder();
                propertyEntity.ReplaceConverted(binder.Value);
            }
        }
    }
}