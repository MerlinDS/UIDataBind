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
            model.Image = Resources.Load<Sprite>(model.ImageString = "UIDataBind Icon");
            model.Raw = Resources.Load<Texture>(model.RawString = "UIDataBind Raw Icon");
            model.ImagesVisible = model.RawImagesVisible = model.SpriteImagesVisible = true;
            model.ImagesAlpha = model.FirstImageAlpha = 1F;

            Debug.Log($"Fetch data to {nameof(BaseControlsModel)} on init");
        }
    }
}