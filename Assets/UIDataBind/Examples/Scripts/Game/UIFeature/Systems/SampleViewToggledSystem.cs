using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class SampleViewToggledSystem : ReactiveSystem<UiBindEntity>
    {
        private readonly UiBindContext _context;
        private readonly OldBindingPath _modelPath;

        public SampleViewToggledSystem(UiBindContext context, OldBindingPath modelPath) : base(context)
        {
            _context = context;
            _modelPath = modelPath;
        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Event);

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsEventOf(_modelPath, nameof(SampleViewModel.Toggle), ControlEvent.Changed);

        protected override void Execute(List<UiBindEntity> entities)
        {
            //TODO: Add help 
            var properties = _context.GetProperties(_modelPath);
            var viewModel = properties.GetModel<SampleViewModel>(nameof(SampleViewModel.ToggledCount));

            viewModel.ToggledCount++;

            properties.Fetch(viewModel, nameof(viewModel.ToggledCount));
        }

    }
}