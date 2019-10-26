using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class InitializeSampleViewSystem : IInitializeSystem
    {
        private readonly IContext<UiBindEntity> _context;
        private readonly BindingPath _path;

        public InitializeSampleViewSystem(IContext<UiBindEntity> context, BindingPath path)
        {
            _context = context;
            _path = path;
        }

        public void Initialize()
        {
            var model = new SampleViewModel
            {
                Visible = true,
                Image = Resources.Load<Texture>("UIDataBind Raw Icon"),
                Label = "This is a text from model",
                HoveringAction = "None",
                Icon = "UIDataBind Icon",
                Color = Color.green,
                Slider = 1F
            };
            _context.InitModel(_path, model);
            var child = new ControlsViewModel
            {
                Visible = true,
                Index =  10
            };
            _context.InitModel(BindingPath.BuildFrom(_path, ControlsViewModel.Path), child);
        }
    }
}