using UIDataBind.Entitas.Helpers;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class MenuTabChangedSystem : ReactivePropertySystem<MenuModel>
    {
        public MenuTabChangedSystem(UiBindContext context) : base(context, nameof(MenuModel.Tab))
        {
        }

        protected override void Execute(ref MenuModel model)
        {
            model.BaseControlsColor = model.Tab == nameof(BaseControlsModel) ? Color.gray : Color.green;
            model.CollectionsColor = model.Tab == nameof(CollectionsModel) ? Color.gray : Color.green;
            model.InfoColor = model.Tab == nameof(InfoModel) ? Color.gray : Color.green;
        }
    }
}