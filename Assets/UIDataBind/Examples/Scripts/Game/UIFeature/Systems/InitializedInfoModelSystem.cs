using System.Collections.Generic;
using Entitas;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class InitializedInfoModelSystem  : ReactiveSystem<UiBindEntity>
    {
        private readonly UiBindContext _uiBind;

        public InitializedInfoModelSystem(UiBindContext uiBind) : base(uiBind) =>
            _uiBind = uiBind;

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.AllOf(UiBindMatcher.Model, UiBindMatcher.Initialized));

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsModel<InfoModel>();

        protected override void Execute(List<UiBindEntity> entities)
        {
            var model = _uiBind.GetModel<InfoModel>();
            model.Info = "This is the info tab.";
            _uiBind.Fetch(model);

            Debug.Log($"Fetch data to {nameof(InfoModel)} on init");
        }
    }
}