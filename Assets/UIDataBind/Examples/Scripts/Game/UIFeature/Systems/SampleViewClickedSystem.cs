using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class SampleViewClickedSystem : SampleViewSystem<SampleViewModel>
    {
        public SampleViewClickedSystem(UiBindContext context, BindingPath modelPath) : base(context, modelPath)
        {
        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Event);

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsEventOf(ModelPath, ControlEvent.Click);

        protected override void Execute(ref SampleViewModel viewModel)
        {
            viewModel.ClickedCount += viewModel.Clicked.IsInvoked() ? 1 : 0;
            if(viewModel.ColorClicked.IsInvoked())
                viewModel.Color = viewModel.Color == Color.green ? Color.yellow : Color.green;
        }
    }
}