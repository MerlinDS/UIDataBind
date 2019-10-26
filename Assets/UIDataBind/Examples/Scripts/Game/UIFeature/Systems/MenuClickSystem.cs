using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class MenuClickSystem : ReactiveSystem<UiBindEntity>
    {
        private readonly UiBindContext _uiBind;

        public MenuClickSystem(UiBindContext uiBind) : base(uiBind) =>
            _uiBind = uiBind;

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Event);

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsEventOf<MenuModel>(ControlEvent.Click);

        protected override void Execute(List<UiBindEntity> entities)
        {
            var menuModel = _uiBind.GetModel<MenuModel>();
            var baseControls = _uiBind.GetModel<BaseControlsModel>();
            var collections = _uiBind.GetModel<CollectionsModel>();
            var infoModel = _uiBind.GetModel<InfoModel>();

            baseControls.Visible = menuModel.BaseControls.IsInvoked();
            collections.Visible = menuModel.Collections.IsInvoked();
            infoModel.Visible = menuModel.Info.IsInvoked();

            _uiBind.Fetch(baseControls, nameof( baseControls.Visible));
            _uiBind.Fetch(collections, nameof( baseControls.Visible));
            _uiBind.Fetch(infoModel, nameof( baseControls.Visible));
        }
    }
}