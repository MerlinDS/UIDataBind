using UIDataBind.Base;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    public struct SampleViewModel : IViewModel
    {
        public bool Visible;
        public bool Toggle;

        public ControlEvent Clicked;
        public ControlEvent Hovered;
        public ControlEvent ColorClicked;

        public int ClickedCount;
        public int ToggledCount;
        public float Slider;

        public Color Color;
        public Texture Image;

        public string Icon;
        public string Label;
        public string HoveringAction;

        public void Refresh(IProperties properties)
        {
            properties.Refresh(nameof(Clicked), ref Clicked);
            properties.Refresh(nameof(Hovered), ref Hovered);
            properties.Refresh(nameof(ColorClicked), ref ColorClicked);

            properties.Refresh(nameof(ClickedCount), ref ClickedCount);
            properties.Refresh(nameof(HoveringAction), ref HoveringAction);

            properties.Refresh(nameof(Visible), ref Visible);
            properties.Refresh(nameof(Label), ref Label);
            properties.Refresh(nameof(Toggle), ref Toggle);
            properties.Refresh(nameof(ToggledCount), ref ToggledCount);
            properties.Refresh(nameof(Slider), ref Slider);
            properties.Refresh(nameof(Icon), ref Icon);
            properties.Refresh(nameof(Color), ref Color);
            properties.Refresh(nameof(Image), ref Image);
        }

    }
}