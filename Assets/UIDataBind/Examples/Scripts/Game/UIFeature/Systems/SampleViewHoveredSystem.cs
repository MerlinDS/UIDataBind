using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class SampleViewHoveredSystem : SampleViewSystem<SampleViewModel>
    {

        public SampleViewHoveredSystem(UiBindContext context, BindingPath modelPath) : base(context, modelPath)
        {
        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Event);

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsEventOf(ModelPath, nameof(ViewModel.Hovered),
                             ControlEvent.Click | ControlEvent.PointerEnter | ControlEvent.PointerExit);

        protected override void Execute(ref SampleViewModel viewModel) =>
            viewModel.HoveringAction = $"{viewModel.Hovered}";
    }
}