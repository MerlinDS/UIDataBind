using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
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
        public Sprite Image;

        public string Icon;
        public string Label;
        public string HoveringAction;

        public void Refresh(IProperties properties)
        {
            properties.RefreshProperty(nameof(Clicked), ref Clicked);
            properties.RefreshProperty(nameof(Hovered), ref Hovered);
            properties.RefreshProperty(nameof(ColorClicked), ref ColorClicked);

            properties.RefreshProperty(nameof(ClickedCount), ref ClickedCount);
            properties.RefreshProperty(nameof(HoveringAction), ref HoveringAction);

            properties.RefreshProperty(nameof(Visible), ref Visible);
            properties.RefreshProperty(nameof(Label), ref Label);
            properties.RefreshProperty(nameof(Toggle), ref Toggle);
            properties.RefreshProperty(nameof(ToggledCount), ref ToggledCount);
            properties.RefreshProperty(nameof(Slider), ref Slider);
            properties.RefreshProperty(nameof(Icon), ref Icon);
            properties.RefreshProperty(nameof(Color), ref Color);
            properties.RefreshProperty(nameof(Image), ref Image);

        }
    }
}