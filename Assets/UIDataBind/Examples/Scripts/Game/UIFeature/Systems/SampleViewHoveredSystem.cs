using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class SampleViewHoveredSystem : ReactiveSystem<UiBindEntity>
    {
        private readonly UiBindContext _context;
        private readonly BindingPath _modelPath;

        public SampleViewHoveredSystem(UiBindContext context, BindingPath modelPath) : base(context)
        {
            _context = context;
            _modelPath = modelPath;
        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Event);

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsEventOf(_modelPath, nameof(SampleViewModel.Hovered),
                             ControlEvent.Click | ControlEvent.PointerEnter | ControlEvent.PointerExit);

        protected override void Execute(List<UiBindEntity> entities)
        {
            //TODO: Add help
            var viewModel = _context.GetModel<SampleViewModel>(_modelPath, nameof(SampleViewModel.Hovered));
            viewModel.HoveringAction = $"{viewModel.Hovered}";
            _context.Fetch(_modelPath, viewModel, nameof(SampleViewModel.HoveringAction));
        }
    }
}