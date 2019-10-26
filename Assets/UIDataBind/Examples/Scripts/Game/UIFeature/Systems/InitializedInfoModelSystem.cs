using UIDataBind.Entitas.Helpers;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class InitializedInfoModelSystem: InitModelSystem<InfoModel>
    {
        public InitializedInfoModelSystem(UiBindContext uiBind) : base(uiBind){}
        protected override void Execute(ref InfoModel model)
        {
            model.Info = "This is the info tab.";

            Debug.Log($"Fetch data to {nameof(InfoModel)} on init");
        }
    }
}