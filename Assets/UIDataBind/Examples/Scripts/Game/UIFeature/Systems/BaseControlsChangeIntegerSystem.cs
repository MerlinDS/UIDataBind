using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public class BaseControlsChangeIntegerSystem  : ReactiveSystem<UiBindEntity>
    {
        private readonly UiBindContext _context;

        public BaseControlsChangeIntegerSystem(UiBindContext context) : base(context) =>
            _context = context;

        protected override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.Event);

        protected override bool Filter(UiBindEntity entity) =>
            entity.IsEventOf<BaseControlsModel>(nameof(BaseControlsModel.ChangeInteger), ControlEvent.Click);

        protected override void Execute(List<UiBindEntity> entities)
        {
            var model = _context.GetModel<BaseControlsModel>();
            model.Integer = Random.Range(int.MinValue, int.MaxValue);
            _context.Fetch(model, nameof(BaseControlsModel.Integer));
        }
    }
}