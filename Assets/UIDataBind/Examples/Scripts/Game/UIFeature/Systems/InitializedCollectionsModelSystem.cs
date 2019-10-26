using UIDataBind.Entitas.Helpers;
using UIDataBind.Examples.Game.UIFeature.Models;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class InitializedCollectionsModelSystem : InitModelSystem<CollectionsModel>
    {
        public InitializedCollectionsModelSystem(UiBindContext uiBind) : base(uiBind){}
        protected override void Execute(ref CollectionsModel model)
        {
            model.Info = "This is a demo of Collections & Hierarchy bindings.";
            Debug.Log($"Fetch data to {nameof(CollectionsModel)} on init");
        }
    }
}