using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class InitializeSampleViewSystem : IInitializeSystem
    {
        private readonly IProperties _properties;

        private SampleViewModel _viewModel;

        public InitializeSampleViewSystem(Contexts contexts)
        {
            _viewModel = new SampleViewModel();
            _properties = contexts.uiBind.GetProperties("SampleView");
        }

        public void Initialize()
        {
            _properties.UpdateModel(ref _viewModel);
            _viewModel.Visible = true;
            _properties.Fetch(_viewModel);

        }
    }
}