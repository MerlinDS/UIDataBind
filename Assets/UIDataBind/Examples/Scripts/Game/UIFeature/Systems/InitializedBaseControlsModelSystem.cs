using Entitas;
using UIDataBind.Entitas.Helpers;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class InitializedBaseControlsModelSystem : InitModelSystem<BaseControlsModel>
    {
        public InitializedBaseControlsModelSystem(IContext<UiBindEntity> context): base(context)
        {
        }

        protected override void Execute(ref BaseControlsModel model)
        {
            model.Info = "This is a demo of base controls binding.";
            model.SampleText = $"This is a text that was got from bound {nameof(BaseControlsModel)} model";

            Debug.Log($"Fetch data to {nameof(BaseControlsModel)} on init");
        }
    }
}