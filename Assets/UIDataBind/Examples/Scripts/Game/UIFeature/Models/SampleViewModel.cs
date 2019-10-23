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
        public Texture Image;

        public string Icon;
        public string Label;
        public string HoveringAction;

        public void Refresh(ModelQuery query)
        {
            query.Refresh(nameof(Clicked), ref Clicked);
            query.Refresh(nameof(Hovered), ref Hovered);
            query.Refresh(nameof(ColorClicked), ref ColorClicked);

            query.Refresh(nameof(ClickedCount), ref ClickedCount);
            query.Refresh(nameof(HoveringAction), ref HoveringAction);

            query.Refresh(nameof(Visible), ref Visible);
            query.Refresh(nameof(Label), ref Label);
            query.Refresh(nameof(Toggle), ref Toggle);
            query.Refresh(nameof(ToggledCount), ref ToggledCount);
            query.Refresh(nameof(Slider), ref Slider);
            query.Refresh(nameof(Icon), ref Icon);
            query.Refresh(nameof(Color), ref Color);
            query.Refresh(nameof(Image), ref Image);

        }
    }
}