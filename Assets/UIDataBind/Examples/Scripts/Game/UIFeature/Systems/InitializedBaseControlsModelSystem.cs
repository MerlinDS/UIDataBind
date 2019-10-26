using System.Collections.Generic;
using Entitas;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class InitializedBaseControlsModelSystem : ReactiveSystem<UiBindEntity>
    {
        private readonly UiBindContext _uiBind;

        public InitializedBaseControlsModelSystem(UiBindContext uiBind) : base(uiBind) =>
            _uiBind = uiBind;

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.AllOf(UiBindMatcher.Model, UiBindMatcher.Initialized));

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsModel(BaseControlsModel.Path);

        protected override void Execute(List<UiBindEntity> entities)
        {
            var model = _uiBind.GetModel<BaseControlsModel>();
            model.Info = "This is a demo of base controls binding.";
            model.SampleText = $"This is a text that was got from bound {nameof(BaseControlsModel)} model";
            _uiBind.Fetch(model);

            Debug.Log($"Fetch data to {nameof(BaseControlsModel)} on init");
        }
    }
}