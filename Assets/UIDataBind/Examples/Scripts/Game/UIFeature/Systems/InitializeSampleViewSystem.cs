using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Components;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class InitializeSampleViewSystem : ReactiveSystem<UiBindEntity>, IInitializeSystem
    {
        private readonly IProperties _properties;

        private SampleViewModel _viewModel;

        public InitializeSampleViewSystem(Contexts contexts) : base(contexts.uiBind)
        {
            _viewModel = new SampleViewModel();
            _properties = contexts.uiBind.GetEngine().GetProperties("SampleView");
        }

        public void Initialize()
        {
            _properties.UpdateModel(ref _viewModel);
            _viewModel.Visible = true;
            _viewModel.Label = "This is a text from model";
            _viewModel.ButtonLabel = "Button";
            _viewModel.Toggle = true;
            _viewModel.Icon = "UIDataBind Icon";
            _properties.Fetch(_viewModel);
        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Action);

        protected override bool Filter(UiBindEntity entity) =>
            entity.isEnabled;

        protected override void Execute(List<UiBindEntity> entities)
        {
            _properties.UpdateModel(ref _viewModel);
            foreach (var entity in entities)
            {
                if(entity.action.Type == ActionType.Click)
                    _viewModel.Index++;
                _viewModel.ButtonLabel = $"{entity.action.Type}";
            }

            _properties.Fetch(_viewModel);
        }
    }
}