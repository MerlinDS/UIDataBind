using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class ChangeDemoViewSystem : ReactiveSystem<UiBindEntity>
    {
        private readonly UiBindContext _uiBind;

        public ChangeDemoViewSystem(UiBindContext uiBind) : base(uiBind)
        {
            _uiBind = uiBind;
        }

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Event);

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsEventOf(MenuModel.Path, ControlEvent.Click);

        protected override void Execute(List<UiBindEntity> entities)
        {
            var menuModel = _uiBind.GetModel<MenuModel>(MenuModel.Path);
            var baseControls = _uiBind.GetModel<BaseControlsModel>(BaseControlsModel.Path);
            var collections = _uiBind.GetModel<CollectionsModel>(CollectionsModel.Path);
            var infoModel = _uiBind.GetModel<InfoModel>(InfoModel.Path);

            baseControls.Visible = menuModel.BaseControls.IsInvoked();
            collections.Visible = menuModel.Collections.IsInvoked();
            infoModel.Visible = menuModel.Info.IsInvoked();

            menuModel.BaseControlsColor = baseControls.Visible ? Color.gray : Color.green;
            menuModel.CollectionsColor = collections.Visible ? Color.gray : Color.green;
            menuModel.InfoColor =  infoModel.Visible ? Color.gray : Color.green;

            menuModel.InfoLabel = baseControls.Visible ? baseControls.Info :
                collections.Visible ? collections.Info : infoModel.Info;

            _uiBind.Fetch(BaseControlsModel.Path, baseControls, nameof( baseControls.Visible));
            _uiBind.Fetch(CollectionsModel.Path, collections, nameof( baseControls.Visible));
            _uiBind.Fetch(InfoModel.Path, infoModel, nameof( baseControls.Visible));
            _uiBind.Fetch(MenuModel.Path, menuModel);
        }
    }
}