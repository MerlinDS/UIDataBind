using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class SampleViewToggledSystem : SampleViewSystem<SampleViewModel>
    {
        private bool _previous;
        public SampleViewToggledSystem(UiBindContext context, BindingPath path) : base(context, path)
        {

        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Event);

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsEventOf(ModelPath, UIEventType.Changed);

        protected override void Execute(ref SampleViewModel viewModel, UiBindEntity entity)
        {
            if (_previous == viewModel.Toggle)
                return;

            viewModel.ToggledCount++;
            _previous = viewModel.Toggle;
        }
    }
}