using System;
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

        public Color Color;

        public string Icon;
        public Sprite Image;
        public string Label;
        public string HoveringAction;

        public void Refresh(RefreshType actionType, IProperties properties)
        {
            properties.RefreshProperty(actionType, nameof(Clicked), ref Clicked);
            properties.RefreshProperty(actionType, nameof(Hovered), ref Hovered);
            properties.RefreshProperty(actionType, nameof(ColorClicked), ref ColorClicked);

            properties.RefreshProperty(actionType, nameof(ClickedCount), ref ClickedCount);
            properties.RefreshProperty(actionType, nameof(HoveringAction), ref HoveringAction);

            properties.RefreshProperty(actionType, nameof(Visible), ref Visible);
            properties.RefreshProperty(actionType, nameof(Label), ref Label);
            properties.RefreshProperty(actionType, nameof(Toggle), ref Toggle);
            properties.RefreshProperty(actionType, nameof(ToggledCount), ref ToggledCount);
            properties.RefreshProperty(actionType, nameof(Icon), ref Icon);
            properties.RefreshProperty(actionType, nameof(Color), ref Color);
            properties.RefreshProperty(actionType, nameof(Image), ref Image);

        }
    }
}