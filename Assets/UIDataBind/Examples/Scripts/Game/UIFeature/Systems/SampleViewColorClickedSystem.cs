using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class SampleViewColorClickedSystem : SampleViewSystem<SampleViewModel>
    {
        public SampleViewColorClickedSystem(UiBindContext context, BindingPath path) : base(context, path)
        {

        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Event);

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsEventOf(ModelPath, UIEventType.Click);

        protected override void Execute(ref SampleViewModel viewModel, UiBindEntity entity)
        {
            if(viewModel.ColorClicked)
                viewModel.Color = viewModel.Color == Color.green ? Color.yellow : Color.green;
        }
    }
}