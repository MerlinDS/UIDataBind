using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class InitializeSampleViewSystem : IInitializeSystem
    {
        private readonly IProperties _properties;
        private SampleViewModel _viewModel;

        public InitializeSampleViewSystem(IEngineProvider context, BindingPath path)
        {
            _viewModel = new SampleViewModel();
            _properties = context.GetEngine().GetProperties(path);
        }

        public void Initialize()
        {
            _properties.UpdateModel(ref _viewModel);
            _viewModel.Visible = true;
            _viewModel.Label = "This is a text from model";
            _viewModel.HoveringAction = "None";
            _viewModel.Icon = "UIDataBind Icon";
            _properties.Fetch(_viewModel);
        }
    }
}