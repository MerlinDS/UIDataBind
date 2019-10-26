using System.Collections.Generic;
using Entitas;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class InitializedCollectionsModelSystem : ReactiveSystem<UiBindEntity>
    {
        private readonly UiBindContext _uiBind;

        public InitializedCollectionsModelSystem(UiBindContext uiBind) : base(uiBind) =>
            _uiBind = uiBind;

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.AllOf(UiBindMatcher.Model, UiBindMatcher.Initialized));

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsModel<CollectionsModel>();

        protected override void Execute(List<UiBindEntity> entities)
        {
            var model = _uiBind.GetModel<CollectionsModel>();
            model.Info = "This is a demo of Collections & Hierarchy bindings .";
            _uiBind.Fetch(model);

            Debug.Log($"Fetch data to {nameof(CollectionsModel)} on init");
        }
    }
}