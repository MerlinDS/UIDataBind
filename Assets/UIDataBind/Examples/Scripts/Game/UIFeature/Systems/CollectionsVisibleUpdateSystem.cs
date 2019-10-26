using UIDataBind.Entitas.Extensions;
using UIDataBind.Entitas.Helpers;
using UIDataBind.Examples.Game.UIFeature.Models;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class CollectionsVisibleUpdateSystem  : ReactivePropertySystem<InfoModel>
    {
        private readonly UiBindContext _context;

        public CollectionsVisibleUpdateSystem(UiBindContext context)
            : base(context, nameof(InfoModel.Visible)) =>
            _context = context;

        protected override void Execute(ref InfoModel model)
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