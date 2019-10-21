using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class InitializeSampleViewSystem : IInitializeSystem
    {
        private readonly IProperties _properties;

        public InitializeSampleViewSystem(IEngineProvider context, BindingPath path) =>
            _properties = context.GetProperties(path);

        public void Initialize()
        {
            var viewModel = _properties.GetModel<SampleViewModel>();

            viewModel.Visible = true;
            viewModel.Label = "This is a text from model";
            viewModel.HoveringAction = "None";
            viewModel.Icon = "UIDataBind Icon";
            viewModel.Color = Color.green;
            viewModel.Image = Resources.Load<Sprite>("UIDataBind Icon");
            viewModel.Fetch(_properties);

        }
    }
}