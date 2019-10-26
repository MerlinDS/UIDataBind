using Entitas;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class InitializeViewModelsSystem  : IInitializeSystem
    {
        private readonly IContext<UiBindEntity> _context;

        public InitializeViewModelsSystem(IContext<UiBindEntity> context) =>
            _context = context;

        public void Initialize()
        {
            _context.InitModel(new BaseControlsModel{Visible = true});
            _context.InitModel(new CollectionsModel());
            _context.InitModel(new InfoModel());
            _context.InitModel(new MenuModel
            {
                BaseControlsColor = Color.gray,
                CollectionsColor = Color.green,
                InfoColor = Color.green,
            });
        }
    }
}