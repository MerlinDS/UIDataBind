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
        private readonly BindingPath _modelPath;

        public SampleViewToggledSystem(UiBindContext context, BindingPath modelPath) : base(context)
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
            var viewModel = _context.GetModel<SampleViewModel>(_modelPath, nameof(SampleViewModel.ToggledCount));
            viewModel.ToggledCount++;
            _context.Fetch(_modelPath, viewModel, nameof(viewModel.ToggledCount));
        }

    }
}