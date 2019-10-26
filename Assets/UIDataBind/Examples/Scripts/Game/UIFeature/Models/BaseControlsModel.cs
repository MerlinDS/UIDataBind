using UIDataBind.Base;
using UIDataBind.Binders.Attributes;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    [ViewModel]
    public struct BaseControlsModel: IViewModel
    {
        public bool Visible;
        public bool ImagesVisible;
        public bool RawImagesVisible;
        public bool SpriteImagesVisible;

        public int Integer;
        public float ImagesAlpha;
        public float FirstImageAlpha;

        public ControlEvent ChangeInteger;

        public string Info;
        public string SampleText;

        public Texture Raw;
        public string RawString;

        public Sprite Image;
        public string ImageString;


        public void Refresh(IProperties properties)
        {
            properties.Refresh(nameof(Visible), ref Visible);
            properties.Refresh(nameof(ImagesVisible), ref ImagesVisible);
            properties.Refresh(nameof(RawImagesVisible), ref RawImagesVisible);
            properties.Refresh(nameof(SpriteImagesVisible), ref SpriteImagesVisible);

            properties.Refresh(nameof(ImagesAlpha), ref ImagesAlpha);
            properties.Refresh(nameof(FirstImageAlpha), ref FirstImageAlpha);

            properties.Refresh(nameof(Integer), ref Integer);
            properties.Refresh(nameof(ChangeInteger), ref ChangeInteger);

            properties.Refresh(nameof(Info), ref Info);
            properties.Refresh(nameof(SampleText), ref SampleText);

            properties.Refresh(nameof(Raw), ref Raw);
            properties.Refresh(nameof(RawString), ref RawString);
            properties.Refresh(nameof(Image), ref Image);
            properties.Refresh(nameof(ImageString), ref ImageString);
        }
    }
}