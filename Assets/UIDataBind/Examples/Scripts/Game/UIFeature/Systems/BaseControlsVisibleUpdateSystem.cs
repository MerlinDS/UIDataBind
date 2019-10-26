using UIDataBind.Entitas.Extensions;
using UIDataBind.Entitas.Helpers;
using UIDataBind.Examples.Game.UIFeature.Models;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class BaseControlsVisibleUpdateSystem : ReactivePropertySystem<BaseControlsModel>
    {
        private readonly UiBindContext _context;

        public BaseControlsVisibleUpdateSystem(UiBindContext context)
            : base(context, nameof(BaseControlsModel.Visible)) =>
            _context = context;

        protected override void Execute(ref BaseControlsModel model)
        {
            if (!model.Visible)
                return;

            var menuModel = _context.GetModel<MenuModel>();
            menuModel.InfoLabel = model.Info;
            menuModel.Tab = ModelPath.ToString();
            _context.Fetch( menuModel, nameof(MenuModel.InfoLabel), nameof(MenuModel.Tab));
        }
    }
}