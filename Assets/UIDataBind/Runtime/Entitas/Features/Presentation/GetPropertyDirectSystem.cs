using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Components;
using UIDataBind.Base.Extensions;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class GetPropertyDirectSystem<T> : ReactiveSystem<UiBindEntity>
    {
        private IMatcher<UiBindEntity> _matcher;
        private int _index;

        public GetPropertyDirectSystem(UiBindContext context) : base(context)
        {
        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context)
        {
            _matcher = UiBindMatcher.AllOf(_index = ((IEngineProvider) context).GetEngine().GetPropertyIndex<T>());
            return context.CreateCollector(UiBindMatcher.AllOf(UiBindMatcher.Property, _matcher,
                                                               UiBindMatcher.Converted));
        }

        protected override bool Filter(UiBindEntity entity) =>
            entity.isEnabled && entity.hasConverted && entity.converted.Value.GetType() == typeof(T);

        protected override void Execute(List<UiBindEntity> entities)
        {
            foreach (var entity in entities)
            {
                var component = (IPropertyComponent<T>)entity.GetComponent(_index);
                component.Value = (T) entity.converted.Value;
                entity.ReplaceComponent(_index, (IComponent) component);
                entity.RemoveConverted();
                entity.isDirty = true;
            }
        }
    }
}