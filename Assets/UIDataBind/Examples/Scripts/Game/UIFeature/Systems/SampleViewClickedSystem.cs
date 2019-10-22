using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class SampleViewClickedSystem : ReactiveSystem<UiBindEntity>
    {
        private readonly UiBindContext _context;
        private readonly OldBindingPath _modelPath;

        public SampleViewClickedSystem(UiBindContext context, OldBindingPath modelPath) : base(context)
        {
            _context = context;
            _modelPath = modelPath;
        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Event);

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsEventOf(_modelPath, ControlEvent.Click);

        protected override void Execute(List<UiBindEntity> entities)
        {
            //TODO: Add help
            var properties = _context.GetProperties(_modelPath);
            var viewModel = properties.GetModel<SampleViewModel>();

            viewModel.ClickedCount += viewModel.Clicked.IsInvoked() ? 1 : 0;
            if(viewModel.ColorClicked.IsInvoked())
                viewModel.Color = viewModel.Color == Color.green ? Color.yellow : Color.green;

            properties.Fetch(viewModel);
        }

    }
}